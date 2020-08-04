using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace exportarBaseDatosVinculacion
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void panel1_Paint_1(object sender, PaintEventArgs e)
        {
            panelLogin.BackColor = Color.FromArgb(200, Color.White);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //validar que los campos esten llenos antes de ingresar
            if (userTxt.Text == "" || passwordTxt.Text == "")
            {
                MessageBox.Show("Por favor llenar todos los campos.");
                return;
            }

            this.Hide();
            MainForm fm = new MainForm();
            fm.Show();

            /*SqlConnection con = new SqlConnection();
            con.ConnectionString = "Server=(localdb)\\MSSQLLocalDB;Database=HoursTracker;Trusted_Connection=True;";
            con.Open();
            SqlCommand cmd = new SqlCommand("select usuario, contrasena from usuarios where usuario='" + userTxt.Text + "'and contrasena='" + passwordTxt.Text + "'", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                con.Close();
                this.Hide();
                MainForm fm = new MainForm();
                fm.Show();
            }
            else
            {
                MessageBox.Show("Invalid Login please check username and password");
                con.Close();
            }*/
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            DialogResult result;
            string caption = "Salir";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            result =  MessageBox.Show("Esta seguro que desea salir?", caption, buttons);
            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                this.Close();
            }
        }
    }
}
