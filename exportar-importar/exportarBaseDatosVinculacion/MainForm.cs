using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using _Excel = Microsoft.Office.Interop.Excel;


namespace exportarBaseDatosVinculacion
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void exportButton_Click(object sender, EventArgs e)
        {
            progressBar.Visible = true;
            SqlConnection cnn;
            string connectionstring = null;
            string sql = null;

            _Excel.Application xlApp;
            _Excel.Workbook xlWorkBook;
            _Excel.Worksheet xlWorkSheet;
            object misValue = System.Reflection.Missing.Value;

            xlApp = new Microsoft.Office.Interop.Excel.Application();
            xlWorkBook = xlApp.Workbooks.Add(misValue);
            xlWorkSheet = (_Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);

            connectionstring = "Data Source=54.185.99.214,1433;Network Library=DBMSSOCN;Initial Catalog=HoursTracker;User ID=admin;Password=admin123;";//"Server=(localdb)\\MSSQLLocalDB;Database=HoursTrackerDemo;Trusted_Connection=True;";
            cnn = new SqlConnection(connectionstring);
            cnn.Open();

            sql = "SELECT * FROM facultades";

            SqlDataAdapter dscmd = new SqlDataAdapter(sql, cnn);
            DataSet ds = new DataSet();
            dscmd.Fill(ds);

            System.Data.DataTable miTabla = ds.Tables[0];

            for (int i = 1; i < miTabla.Columns.Count + 1; i++)
            {
                xlWorkSheet.Cells[1, i] = miTabla.Columns[i - 1].ColumnName;
            }

            for (int j = 0; j < miTabla.Rows.Count; j++)
            {
                for (int k = 0; k < miTabla.Columns.Count; k++)
                {
                    xlWorkSheet.Cells[j + 2, k + 1] = miTabla.Rows[j].ItemArray[k].ToString();
                }
            }

            xlApp.Visible = true;

            sql = "SELECT * FROM campuses";

            dscmd = null;
            ds = null;
            miTabla = null;

            _Excel.Sheets worksheets = xlWorkBook.Worksheets;
            var xlNewSheet = (_Excel.Worksheet)worksheets.Add(worksheets[1], Type.Missing, Type.Missing, Type.Missing);
            xlNewSheet.Name = "newsheet";

            xlNewSheet = (_Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
            dscmd = new SqlDataAdapter(sql, cnn);
            ds = new DataSet();
            dscmd.Fill(ds);

            miTabla = ds.Tables[0];

            for (int i = 1; i < miTabla.Columns.Count + 1; i++)
            {
                xlNewSheet.Cells[1, i] = miTabla.Columns[i - 1].ColumnName;
            }

            for (int j = 0; j < miTabla.Rows.Count; j++)
            {
                for (int k = 0; k < miTabla.Columns.Count; k++)
                {
                    xlNewSheet.Cells[j + 2, k + 1] = miTabla.Rows[j].ItemArray[k].ToString();
                }
            }

            xlApp.Visible = true;
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            DialogResult result;
            string caption = "Salir";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            result = MessageBox.Show("Esta seguro que desea salir?", caption, buttons);
            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void MainForm_FormClosed(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void importButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            ImportForm im = new ImportForm();
            im.Show();
        }
    }
}
