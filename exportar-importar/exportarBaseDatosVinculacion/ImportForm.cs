using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Z.Dapper.Plus;

namespace exportarBaseDatosVinculacion
{
    public partial class ImportForm : Form
    {
        public ImportForm()
        {
            InitializeComponent();
        }

        private void cboSheet_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = tableCollection[cboSheet.SelectedItem.ToString()];
            dataGridView1.DataSource = dt;
        }

        DataTableCollection tableCollection;

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog() { Filter = "Excel Files| *.xls; *.xlsx" }) {

                if(openFileDialog.ShowDialog() == DialogResult.OK){
                    txtFilename.Text = openFileDialog.FileName;
                    using (var stream = File.Open(openFileDialog.FileName, FileMode.Open, FileAccess.Read))
                    {
                        using (IExcelDataReader reader = ExcelReaderFactory.CreateReader(stream))
                        {
                            DataSet result = reader.AsDataSet(new ExcelDataSetConfiguration()
                            {
                                ConfigureDataTable=(_)=> new ExcelDataTableConfiguration() { UseHeaderRow = true}
                            });
                            tableCollection = result.Tables;
                            cboSheet.Items.Clear();
                            foreach(DataTable table in tableCollection)
                            {
                                cboSheet.Items.Add(table.TableName);//agregar hoja al combobox
                            }
                        }
                    }
                }
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
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

        private void btnImport_Click(object sender, EventArgs e)
        {
            try
            {
                string selected = cboSheet.GetItemText(cboSheet.SelectedItem);
                MessageBox.Show(selected);
                //DapperPlusManager
                if(selected == "alumnos")
                {

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
