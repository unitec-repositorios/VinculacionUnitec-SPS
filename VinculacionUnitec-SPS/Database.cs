using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VinculacionUnitec_SPS
{
    class Database
    {
        public SQLiteConnection myConnection;

        public Database()
        {
            myConnection = new SQLiteConnection("Data Source=database.sqlite3");
            if (!File.Exists("./database.sqlite3"))
            {
                SQLiteConnection.CreateFile("database.sqlite3");
            }
        }

        public void OpenConnection()
        {
            if (myConnection.State != System.Data.ConnectionState.Open)
            {
                myConnection.Open();
            }
        }

        public void CloseConnection()
        {
            if (myConnection.State != System.Data.ConnectionState.Closed)
            {
                myConnection.Close();
            }
        }

        public bool MainMenu()
        {
            Database db = new Database();

            Console.Clear();
            Console.WriteLine("Escoger una opción:");
            Console.WriteLine("1) Insertar un alumno");
            Console.WriteLine("2) Ver alumnos");
            Console.WriteLine("3) Buscar alumno por Id");
            Console.WriteLine("4) Buscar alumno por NoCuenta");
            Console.WriteLine("5) Editar alumno por Id");
            Console.WriteLine("6) Editar alumno por NoCuenta");
            Console.WriteLine("7) Borrar alumno por Id");
            Console.WriteLine("8) Borrar alumno por Numero de Cuenta");
            Console.WriteLine("9) Enviar codigo de verificacion");

            Console.WriteLine("x) Salir del Menu");
            Console.Write("\r\nSeleccione una opción: ");

            switch (Console.ReadLine())
            {
                case "1":
                    db.Insertar();
                    return true;
                case "2":
                    db.Leer();
                    return true;
                case "3":
                    db.LeerPorId();
                    return true;
                case "4":
                    db.LeerPorNoCuenta();
                    return true;
                case "5":
                    db.EditarPorId();
                    return true;
                case "6":
                    db.EditarPorNoCuenta();
                    return true;
                case "7":
                    db.BorrarPorId();
                    return true;
                case "8":
                    db.BorrarPorNoCuenta();
                    return true;
                case "9":
                    db.EnviarCorreo("61651242");
                    return true;
                case "x":
                    return false;
                default:
                    return true;
            }
        }
        //si el no cuenta no existe se agrega
        //y si existe un update, actualizando unicamente el correo electronico
        //subject Vinculación Unitec SPS
        //vinculacionunitecsps@gmail.com

        public bool CuentaVerificada(string numeroCuenta)
        {
            Database db = new Database();
            /*
            SELECT from Database
            */
            string selectQuery = "SELECT * from Alumno WHERE NumeroCuenta=@NumeroCuenta and Verificado = 1";
            SQLiteCommand selectCommand = new SQLiteCommand(selectQuery, db.myConnection);
            db.OpenConnection();
            selectCommand.Parameters.AddWithValue("@NumeroCuenta", numeroCuenta);

            SQLiteDataReader selectResult = selectCommand.ExecuteReader();
            if (selectResult.HasRows)
            {
                /*
                while(selectResult.Read()){
                Console.WriteLine("Id: {0} - NumeroCuenta: {1} - CodigoVerificacion: {2} - CorreoElectronico: {3}", selectResult["Id"], selectResult["NumeroCuenta"], selectResult["CodigoVerificacion"], selectResult["CorreoElectronico"]);
                }
                */
                return true;
            }
            else
            {
                return false;
            }
            db.CloseConnection();
        }

        public void CodigoRecibido(string codigoRecibido)
        {
            Database db = new Database();

            string updateQuery = "UPDATE Alumno set CodigoConfirmado = @CodigoConfirmado, Verificado = 1 WHERE CodigoVerificacion = @CodigoConfirmado";
            SQLiteCommand updateCommand = new SQLiteCommand(updateQuery, db.myConnection);
            db.OpenConnection();
            updateCommand.Parameters.AddWithValue("@CodigoConfirmado", codigoRecibido);
            var updateResult = updateCommand.ExecuteNonQuery();
            db.CloseConnection();
        }

        public void GenerarCodigo(string numeroCuenta)
        {
            Database db = new Database();
            Random generator = new Random();
            string random = generator.Next(0, 999999).ToString("D6");

            string updateQuery = "UPDATE Alumno set CodigoVerificacion = @CodigoVerificacion WHERE NumeroCuenta = @NumeroCuenta";
            SQLiteCommand updateCommand = new SQLiteCommand(updateQuery, db.myConnection);
            db.OpenConnection();
            updateCommand.Parameters.AddWithValue("@CodigoVerificacion", random);
            updateCommand.Parameters.AddWithValue("@NumeroCuenta", numeroCuenta);
            var updateResult = updateCommand.ExecuteNonQuery();
            db.CloseConnection();
        }
        /*
                     Random generator = new Random();
            string random = generator.Next(0, 999999).ToString("D6");
             */

        public bool EnviarCorreo(string numeroCuenta)
        {
            string correoEnviar;
            string codigoVerificacion;

            Database db = new Database();
            db.GenerarCodigo(numeroCuenta);
            // Console.WriteLine("Ingrese numero de cuenta");
            // string nCuenta = Console.ReadLine();
            string lectura;
            string cadena = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\Franco\source\repos\BDAccess.accdb";
            string cadena_com = "SELECT * FROM [Datos Alumno]";
            OleDbConnection con = new OleDbConnection(cadena);
            OleDbCommand com = new OleDbCommand(cadena_com, con);
            con.Open();

            OleDbDataReader leer = com.ExecuteReader();
            while (leer.Read())
            {
                lectura = leer.GetValue(0).ToString();
                if (lectura == numeroCuenta)
                {
                    string selectQuery = "SELECT * from Alumno WHERE NumeroCuenta=@NumeroCuenta";
                    SQLiteCommand selectCommand = new SQLiteCommand(selectQuery, db.myConnection);
                    db.OpenConnection();
                    selectCommand.Parameters.AddWithValue("@NumeroCuenta", numeroCuenta);
                    SQLiteDataReader selectResult = selectCommand.ExecuteReader();
                    if (selectResult.HasRows)
                    {
                        while (selectResult.Read())
                        {
                            correoEnviar = selectResult["CorreoElectronico"].ToString();
                            codigoVerificacion = selectResult["CodigoVerificacion"].ToString();
                            db.EnviarCorreo(correoEnviar, codigoVerificacion);
                        }
                    }
                    db.CloseConnection();
                    con.Close();
                    return true;
                }
            }
            con.Close();
            return false;
        }

        public void EnviarCorreo(string destinatario, string codigo)
        {
            System.Net.Mail.MailMessage correo = new System.Net.Mail.MailMessage();
            correo.To.Add(destinatario);
            correo.Subject = "Codigo de verificación";
            correo.SubjectEncoding = System.Text.Encoding.UTF8;
            //Enviar una copia del correo a otro destinatario
            //correo.Bcc.Add("midencefranco2@gmail.com");
            correo.Body = codigo;
            correo.BodyEncoding = System.Text.Encoding.UTF8;
            correo.IsBodyHtml = true;
            correo.From = new System.Net.Mail.MailAddress("midencefranco@gmail.com");

            System.Net.Mail.SmtpClient cliente = new System.Net.Mail.SmtpClient();
            cliente.Credentials = new System.Net.NetworkCredential("vinculacionunitecsps@gmail.com", "qwerty456123");
            cliente.Port = 587;
            cliente.EnableSsl = true;
            cliente.Host = "smtp.gmail.com"; // mail.tudominio.com

            try
            {
                cliente.Send(correo);
                Console.WriteLine("Se envio el correo :)");
            }
            catch (Exception)
            {
                Console.WriteLine("Error al enviar el correo :(");
            }

        }

        public void Insertar()
        {
            Database db = new Database();
            /*
            INSERT into Database
            */
            Random rand = new Random();
            int randomNumber = rand.Next();
            Random generator = new Random();
            int random = generator.Next(0, 999999);
            Console.WriteLine("Bienvenido a ingresar alumnos");
            Console.WriteLine("Ingrese numero de cuenta");
            int noCuenta = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Ingrese correo electrónico");
            string correoElectronico = Console.ReadLine();


            string insertQuery = "INSERT INTO Alumno (`NumeroCuenta`, `CodigoVerificacion`, `CorreoElectronico`) VALUES (@NumeroCuenta, @CodigoVerificacion, @CorreoElectronico)";
            SQLiteCommand insertCommand = new SQLiteCommand(insertQuery, db.myConnection);
            db.OpenConnection();
            insertCommand.Parameters.AddWithValue("@NumeroCuenta", noCuenta);
            insertCommand.Parameters.AddWithValue("@CodigoVerificacion", random);
            insertCommand.Parameters.AddWithValue("@CorreoElectronico", correoElectronico);
            var insertResult = insertCommand.ExecuteNonQuery();
            db.CloseConnection();
            Console.WriteLine("Se agrego el alumno: {0}", insertResult);
            Console.ReadKey();
        }

        public void Leer()
        {
            Database db = new Database();
            /*
            SELECT from Database
            */
            string selectQuery = "SELECT * from Alumno";
            SQLiteCommand selectCommand = new SQLiteCommand(selectQuery, db.myConnection);
            db.OpenConnection();

            SQLiteDataReader selectResult = selectCommand.ExecuteReader();
            if (selectResult.HasRows)
            {
                while (selectResult.Read())
                {
                    Console.WriteLine("Id: {0} - NumeroCuenta: {1} - CodigoVerificacion: {2} - CorreoElectronico: {3}", selectResult["Id"], selectResult["NumeroCuenta"], selectResult["CodigoVerificacion"], selectResult["CorreoElectronico"]);
                }
            }
            db.CloseConnection();
            Console.ReadKey();
        }

        public void LeerPorId()
        {
            Database db = new Database();
            /*
            SELECT from Database
            */
            Console.WriteLine("Ingrese el Id");
            int id = Convert.ToInt32(Console.ReadLine());
            string selectQuery = "SELECT * from Alumno WHERE Id=@Id";
            SQLiteCommand selectCommand = new SQLiteCommand(selectQuery, db.myConnection);
            db.OpenConnection();
            selectCommand.Parameters.AddWithValue("@Id", id);

            SQLiteDataReader selectResult = selectCommand.ExecuteReader();
            if (selectResult.HasRows)
            {
                while (selectResult.Read())
                {
                    Console.WriteLine("Id: {0} - NumeroCuenta: {1} - CodigoVerificacion: {2} - CorreoElectronico: {3}", selectResult["Id"], selectResult["NumeroCuenta"], selectResult["CodigoVerificacion"], selectResult["CorreoElectronico"]);
                }
            }
            db.CloseConnection();
            Console.ReadKey();
        }

        public void LeerPorNoCuenta()
        {
            Database db = new Database();
            /*
            SELECT from Database
            */
            Console.WriteLine("Ingrese el Numero de Cuenta");
            int noCuenta = Convert.ToInt32(Console.ReadLine());
            string selectQuery = "SELECT * from Alumno WHERE NumeroCuenta=@NumeroCuenta";
            SQLiteCommand selectCommand = new SQLiteCommand(selectQuery, db.myConnection);
            db.OpenConnection();
            selectCommand.Parameters.AddWithValue("@NumeroCuenta", noCuenta);

            SQLiteDataReader selectResult = selectCommand.ExecuteReader();
            if (selectResult.HasRows)
            {
                while (selectResult.Read())
                {
                    Console.WriteLine("Id: {0} - NumeroCuenta: {1} - CodigoVerificacion: {2} - CorreoElectronico: {3}", selectResult["Id"], selectResult["NumeroCuenta"], selectResult["CodigoVerificacion"], selectResult["CorreoElectronico"]);
                }
            }
            db.CloseConnection();
            Console.ReadKey();
        }

        public void EditarPorId()
        {
            Database db = new Database();
            /*
            UPDATE the Database
            */
            Random rand = new Random();
            int randomNumber = rand.Next();
            Console.WriteLine("Editar Alumno");
            Console.WriteLine("Ingrese Id");
            string id = Console.ReadLine();
            Console.WriteLine("Ingrese el nuevo numero de cuenta");
            int noCuenta = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Ingrese el nuevo correo electrónico");
            string correoElectronico = Console.ReadLine();

            string updateQuery = "UPDATE Alumno set NumeroCuenta = @NumeroCuenta, CodigoVerificacion = @CodigoVerificacion, CorreoElectronico = @CorreoElectronico WHERE Id = @Id";
            SQLiteCommand updateCommand = new SQLiteCommand(updateQuery, db.myConnection);
            db.OpenConnection();
            updateCommand.Parameters.AddWithValue("@Id", id);
            updateCommand.Parameters.AddWithValue("@NumeroCuenta", noCuenta);
            updateCommand.Parameters.AddWithValue("@CodigoVerificacion", randomNumber);
            updateCommand.Parameters.AddWithValue("@CorreoElectronico", correoElectronico);
            var updateResult = updateCommand.ExecuteNonQuery();
            db.CloseConnection();
            Console.WriteLine("Se actualizo el alumno");
            Console.ReadKey();
        }

        public void EditarPorNoCuenta()
        {
            Database db = new Database();
            /*
            UPDATE the Database
            */
            Random rand = new Random();
            int randomNumber = rand.Next();
            Console.WriteLine("Editar Alumno");
            Console.WriteLine("Ingrese numero de cuenta");
            string idNoCuenta = Console.ReadLine();
            Console.WriteLine("Ingrese el nuevo numero de cuenta");
            int noCuenta = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Ingrese el nuevo correo electrónico");
            string correoElectronico = Console.ReadLine();

            string updateQuery = "UPDATE Alumno set NumeroCuenta = @NumeroCuenta, CodigoVerificacion = @CodigoVerificacion, CorreoElectronico = @CorreoElectronico WHERE NumeroCuenta = @IdNoCuenta";
            SQLiteCommand updateCommand = new SQLiteCommand(updateQuery, db.myConnection);
            db.OpenConnection();
            updateCommand.Parameters.AddWithValue("@IdNoCuenta", idNoCuenta);
            updateCommand.Parameters.AddWithValue("@NumeroCuenta", noCuenta);
            updateCommand.Parameters.AddWithValue("@CodigoVerificacion", randomNumber);
            updateCommand.Parameters.AddWithValue("@CorreoElectronico", correoElectronico);
            var updateResult = updateCommand.ExecuteNonQuery();
            db.CloseConnection();
            Console.WriteLine("Se actualizo el alumno");
            Console.ReadKey();
        }

        public void BorrarPorId()
        {
            Database db = new Database();
            /*
            INSERT into Database
            */
            Random rand = new Random();
            int randomNumber = rand.Next();
            Console.WriteLine(randomNumber);
            Console.WriteLine("Eliminar alumno");
            Console.WriteLine("Ingrese id");
            string id = Console.ReadLine();

            string deleteQuery = "DELETE FROM Alumno WHERE Id = @Id";
            SQLiteCommand deleteCommand = new SQLiteCommand(deleteQuery, db.myConnection);
            db.OpenConnection();
            deleteCommand.Parameters.AddWithValue("@Id", id);
            var deleteResult = deleteCommand.ExecuteNonQuery();
            db.CloseConnection();
            Console.WriteLine("Se elimino el alumno");
            Console.ReadKey();
        }

        public void BorrarPorNoCuenta()
        {
            Database db = new Database();
            /*
            INSERT into Database
            */
            Random rand = new Random();
            int randomNumber = rand.Next();
            Console.WriteLine(randomNumber);
            Console.WriteLine("Eliminar alumno");
            Console.WriteLine("Ingrese Numero de Cuenta");
            string noCuenta = Console.ReadLine();

            string deleteQuery = "DELETE FROM Alumno WHERE NumeroCuenta = @NumeroCuenta";
            SQLiteCommand deleteCommand = new SQLiteCommand(deleteQuery, db.myConnection);
            db.OpenConnection();
            deleteCommand.Parameters.AddWithValue("@NumeroCuenta", noCuenta);
            var deleteResult = deleteCommand.ExecuteNonQuery();
            db.CloseConnection();
            Console.WriteLine("Se elimino el alumno");
            Console.ReadKey();
        }
    }
}
