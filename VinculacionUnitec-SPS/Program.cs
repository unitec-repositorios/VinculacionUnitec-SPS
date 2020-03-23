using System;
using System.Data.OleDb;
using System.Linq;
using System.Net;
using System.Net.Mail;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;

namespace LeerMensajesBotTelegram
{
    class Program
    {
        //Variables Globales necesarias para funcionamiento correcto
        private static readonly TelegramBotClient miBotTelegram = new TelegramBotClient("1099955313:AAE4MUcmOzK09op7z8K-K5VNANtumC2n9WQ");
        public static string nombreBot;
        private static int verf=0;
        private static string nCuenta="";

        public static void Main(string[] args)
        {
            //Escuchamos los mensajes enviados en los grupos donde esté el bot
            var me = miBotTelegram.GetMeAsync().Result;
            nombreBot = me.Username;
            Console.Title = $"Conectado a bot de Telegram @{nombreBot}...";

            //Asignamos los eventos de lectura de mensajes y captura de errores
            miBotTelegram.OnMessage += EventoBotTelegramLeerMensajes;
            miBotTelegram.OnReceiveError += EventoBotTelegramErrorRecibido;

            //Iniciamos la lectura de mensajes
            miBotTelegram.StartReceiving(Array.Empty<UpdateType>());
            Console.WriteLine($"Escuchando mensajes de @{nombreBot}...");

            //Si se pulsa INTRO en la consola se detiene la escucha de mensajes
            Console.ReadLine();
            miBotTelegram.StopReceiving();
        }
        //Evento que lee los mensajes de los grupos donde esté el bot de Telegram        
        private static async void EventoBotTelegramLeerMensajes(object sender, MessageEventArgs eventoArgumentosMensajeRecibido)
        {
            var mensaje = eventoArgumentosMensajeRecibido.Message;
            
            if (mensaje == null || mensaje.Type != MessageType.Text)
                return;

            Console.WriteLine($"Mensaje de @{mensaje.Chat.Username}:" + mensaje.Text);
            var mens = mensaje.Text;
            if(mens!="" && verf==0)
            {
                if (CuentaExiste(mens))
                {
                    await miBotTelegram.SendTextMessageAsync(mensaje.Chat.Id,$"1. Horas totales de vinculación\n2. Detalle de horas por proyecto");
                    nCuenta = mens;
                    verf = 1;
                }
                else
                {
                    await miBotTelegram.SendTextMessageAsync(mensaje.Chat.Id,"No se encontro Numero Cuenta");
                    await miBotTelegram.SendTextMessageAsync(mensaje.Chat.Id,
                            "Estimado estudiante: Bienvenido al Asistente de Vinculación UNITEC-SPS\n\nIngrese su número de cuenta para sus consultas");
                }

            }
            else if (mens!="" && verf!=0)
            {
                switch (mens)
                {
                    case "1":
                        Console.WriteLine("OPCION 1");
                        var ht = HorasTotales(nCuenta);
                        await miBotTelegram.SendTextMessageAsync(mensaje.Chat.Id, 
                            "Tienes un total de " +ht+ " horas a la fecha.\nPara consultas enviar correo a:\nvinculacionsps@unitec.edu ó andrea.orellana@unitec.edu.hn");
                        await miBotTelegram.SendTextMessageAsync(mensaje.Chat.Id, $"1. Horas totales de vinculación\n2. Detalle de horas por proyecto");
                        break;
                    case "2":
                        Console.WriteLine("OPCION 2");
                        var hd = HorasDetalle(nCuenta);
                        await miBotTelegram.SendTextMessageAsync(mensaje.Chat.Id,"Tu Informacion es la siguiente:\n"+hd+"\nPara consultas enviar correo a:\nvinculacionsps@unitec.edu ó andrea.orellana@unitec.edu.hn");
                        await miBotTelegram.SendTextMessageAsync(mensaje.Chat.Id, $"1. Horas totales de vinculación\n2. Detalle de horas por proyecto");
                        break;

                    case "3":
                        Console.WriteLine("SALIO");
                        verf = 0;
                        break;
                    default:
                        Console.WriteLine("OPCION INVALIDA");
                        await miBotTelegram.SendTextMessageAsync(mensaje.Chat.Id, "Opcion invalida");
                        await miBotTelegram.SendTextMessageAsync(mensaje.Chat.Id, $"1. Horas totales de vinculación\n2. Detalle de horas por proyecto");
                        break;
                }
            }
        }
        //Evento que  lee mensajes de error
        private static void EventoBotTelegramErrorRecibido(object sender, ReceiveErrorEventArgs eventoArgumentosErrorRecibidos)
        {
            Console.WriteLine("Error recibido: {0} — {1}",
                eventoArgumentosErrorRecibidos.ApiRequestException.ErrorCode,
                eventoArgumentosErrorRecibidos.ApiRequestException.Message);
        }
        //Funcion para enviar correos de confirmacion 
        private static bool EnviarCorreo(string correo)
        {
            var codigo = "2111";
            MailMessage email = new MailMessage();
            email.To.Add(new MailAddress("j.nunez@unitec.edu"));
            email.From = new MailAddress("nunez0467@gmail.com");
            email.Subject = "CONFIRMACION ( " + DateTime.Now.ToString("dd / MMM / yyy hh:mm:ss") + " ) ";
            email.Body = "El codigo de Confirmacion es : " + codigo;
            email.IsBodyHtml = true;
            email.Priority = MailPriority.Normal;

            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 25;
            smtp.EnableSsl = false;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential("nunez0467@gmail.com", "Novemberrain25");

            string output = null;
            try
            {
                smtp.Send(email);
                email.Dispose();
                Console.WriteLine("Correo Enviado con Exito");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        //Funcion de verificacion de cuenta existente 
        private static bool CuentaExiste(string nCuenta)
        {
            string lectura;
            string cadena = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\ItsJaan\Documents\BASE DATOS MODIFICADA 29 Ene.accdb";
            string cadena_com = "SELECT * FROM [Datos Alumno]";
            OleDbConnection con = new OleDbConnection(cadena);
            OleDbCommand com = new OleDbCommand(cadena_com, con);
            con.Open();

            OleDbDataReader leer = com.ExecuteReader();
            while (leer.Read())
            {
                lectura = leer.GetValue(0).ToString();
                if (lectura == nCuenta)
                {
                    con.Close();
                    return true;
                }
            }
            con.Close();
            return false;
        }
        //Funcion  que devuelve horas totales del alumno 
        private static string HorasTotales(string nCuenta)
        {
            int n = 0;
            string lectura;
            string cadena = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\ItsJaan\Documents\BASE DATOS MODIFICADA 29 Ene.accdb";
            string cadena_com = "SELECT * FROM [Tabla General]";
            OleDbConnection con = new OleDbConnection(cadena);
            OleDbCommand com = new OleDbCommand(cadena_com, con);
            con.Open();

            OleDbDataReader leer = com.ExecuteReader();
            while(leer.Read())
            {
                lectura = leer.GetValue(6).ToString();
                if(lectura==nCuenta)
                {
                    n += int.Parse(leer.GetValue(8).ToString());
                }   
            }
            con.Close();
            return n.ToString();
        }
        //Funcion que devuelve las horas en detalle del alumno
        private static string HorasDetalle(string nCuenta)
        {
            string detalles = "";
            string lectura;
            string cadena = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\ItsJaan\Documents\BASE DATOS MODIFICADA 29 Ene.accdb";
            string cadena_com = "SELECT * FROM [Tabla General]";
            OleDbConnection con = new OleDbConnection(cadena);
            OleDbCommand com = new OleDbCommand(cadena_com, con);
            con.Open();

            OleDbDataReader leer = com.ExecuteReader();
            while (leer.Read())
            {
                lectura = leer.GetValue(6).ToString();
                if (lectura == nCuenta)
                {
                    //PERIODO DE PROYECTO
                    detalles += "Periodo: " + leer.GetValue(2).ToString() + "\n";

                    //Extraccion de Campo Nombre Clase TABLA DATOS CLASE
                    string l1 = leer.GetValue(3).ToString();
                    string cadena_com1 = "SELECT * FROM [Datos Asignaturas]";
                    OleDbConnection con1 = new OleDbConnection(cadena);
                    OleDbCommand com1 = new OleDbCommand(cadena_com1, con1);
                    con1.Open();

                    OleDbDataReader leer1 = com1.ExecuteReader();
                    if (leer1.Read())
                    {
                        lectura = leer1.GetValue(0).ToString();
                        if (lectura == l1)
                        {
                            detalles += "Nombre Clase: " + leer.GetValue(1).ToString() + "\n";
                        }
                    }
                    con1.Close();
                    //Extraccion de Nombre Proyecto TABLA DATOS PROYECTOS
                    string l2 = leer.GetValue(1).ToString();
                    string cadena_com2 = "SELECT * FROM [Datos Proyectos]";
                    OleDbConnection con2 = new OleDbConnection(cadena);
                    OleDbCommand com2 = new OleDbCommand(cadena_com2, con2);
                    con2.Open();

                    OleDbDataReader leer2 = com2.ExecuteReader();
                    if (leer2.Read())
                    {
                        lectura = leer2.GetValue(0).ToString();
                        if (lectura == l2)
                        {
                            detalles += "Nombre Proyecto: " + leer.GetValue(1).ToString() + "\n";
                        }
                    }
                    con2.Close();
                    //HORAS DE PROYECTO 
                    detalles += "Horas de Proyecto: " + leer.GetValue(8).ToString() + "\n\n";
                }
            }
            con.Close();
            return detalles;
        }
    }
}