using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
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
            System.Data.DataTable miTabla = null;

            _Excel.Application xlApp;
            _Excel.Workbook xlWorkBook;
            _Excel.Worksheet xlWorkSheet;
            object misValue = System.Reflection.Missing.Value;

            xlApp = new Microsoft.Office.Interop.Excel.Application();
            xlWorkBook = xlApp.Workbooks.Add(misValue);
            xlWorkSheet = (_Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);

            _Excel.Sheets worksheets = xlWorkBook.Worksheets;
            var xlNewSheet = (_Excel.Worksheet)worksheets.Add(worksheets[1], Type.Missing, Type.Missing, Type.Missing);

            connectionstring = "Server=(localdb)\\MSSQLLocalDB;Database=HoursTracker2;Trusted_Connection=True;";
            cnn = new SqlConnection(connectionstring);
            cnn.Open();

            //CONTANDO TABLAS
            System.Data.DataTable schema = cnn.GetSchema("Tables");
            int tableCount = schema.AsEnumerable().Count(t => t.Field<string>("TABLE_TYPE") == "BASE TABLE");
            labelAmountTables.Text = "Tablas: 1" + "/" + tableCount;
            labelAmountTables.Visible = true;

            //GETTING TABLE NAMES
            List<string> TableNames = new List<string>();
            foreach (DataRow row in schema.Rows)
            {
                TableNames.Add(row[2].ToString());
            }

            SqlDataAdapter dscmd = new SqlDataAdapter(sql, cnn);
            DataSet ds = new DataSet();
            labelTitle.Text = "EXPORTANDO TABLA: ";

            for (int l = 0; l < tableCount; l++)
            {
                progressBar.Value = 0;
                labelNameTable.Text = TableNames[l];
                labelAmountTables.Text = "Tablas: " + l + "/" + tableCount;
                sql = "SELECT * FROM " + TableNames[l];
                dscmd = null;
                ds = null;
                miTabla = null;

                worksheets = xlWorkBook.Worksheets;
                xlNewSheet = (_Excel.Worksheet)worksheets.Add(worksheets[1], Type.Missing, Type.Missing, Type.Missing);
                xlNewSheet.Name = TableNames[l];

                xlNewSheet = (_Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
                dscmd = new SqlDataAdapter(sql, cnn);
                ds = new DataSet();
                dscmd.Fill(ds);
                miTabla = ds.Tables[0];
                progressBar.Maximum = miTabla.Rows.Count;

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
                    progressBar.Value = j;
                }

            }

            //AL TERMINAR EXPORTACIONES
            labelAmountTables.Text = "Tablas: " + tableCount + "/" + tableCount;
            progressBar.Value = progressBar.Maximum;
            labelTitle.Text = "EXPORTACION";
            labelNameTable.Text = "TERMINADA.";
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
                System.Windows.Forms.Application.Exit();
            }
        }

        private void MainForm_FormClosed(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        private void importButton_Click(object sender, EventArgs e)
        {

        }
    }
}
