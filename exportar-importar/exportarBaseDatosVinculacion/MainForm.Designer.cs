namespace exportarBaseDatosVinculacion
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.exportButton = new System.Windows.Forms.Button();
            this.labelTitle = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.labelNotClose = new System.Windows.Forms.Label();
            this.labelAmountTables = new System.Windows.Forms.Label();
            this.labelNameTable = new System.Windows.Forms.Label();
            this.importButton = new System.Windows.Forms.Button();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.closeButton = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // exportButton
            // 
            this.exportButton.BackColor = System.Drawing.SystemColors.Control;
            this.exportButton.ForeColor = System.Drawing.Color.Black;
            this.exportButton.Location = new System.Drawing.Point(61, 181);
            this.exportButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.exportButton.Name = "exportButton";
            this.exportButton.Size = new System.Drawing.Size(157, 49);
            this.exportButton.TabIndex = 0;
            this.exportButton.Text = "Exportar base datos";
            this.exportButton.UseVisualStyleBackColor = false;
            this.exportButton.Click += new System.EventHandler(this.exportButton_Click);
            // 
            // labelTitle
            // 
            this.labelTitle.AutoSize = true;
            this.labelTitle.Location = new System.Drawing.Point(35, 44);
            this.labelTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(151, 17);
            this.labelTitle.TabIndex = 1;
            this.labelTitle.Text = "Seleccione una opcion";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.labelNotClose);
            this.panel1.Controls.Add(this.labelAmountTables);
            this.panel1.Controls.Add(this.labelNameTable);
            this.panel1.Controls.Add(this.importButton);
            this.panel1.Controls.Add(this.progressBar);
            this.panel1.Controls.Add(this.closeButton);
            this.panel1.Controls.Add(this.labelTitle);
            this.panel1.Controls.Add(this.exportButton);
            this.panel1.Location = new System.Drawing.Point(179, 46);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(476, 304);
            this.panel1.TabIndex = 2;
            // 
            // labelNotClose
            // 
            this.labelNotClose.AutoSize = true;
            this.labelNotClose.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.labelNotClose.Location = new System.Drawing.Point(65, 14);
            this.labelNotClose.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelNotClose.Name = "labelNotClose";
            this.labelNotClose.Size = new System.Drawing.Size(329, 17);
            this.labelNotClose.TabIndex = 12;
            this.labelNotClose.Text = "Por favor no cerrar aplicacion durante exportacion.";
            this.labelNotClose.Visible = false;
            // 
            // labelAmountTables
            // 
            this.labelAmountTables.AutoSize = true;
            this.labelAmountTables.Location = new System.Drawing.Point(57, 128);
            this.labelAmountTables.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelAmountTables.Name = "labelAmountTables";
            this.labelAmountTables.Size = new System.Drawing.Size(46, 17);
            this.labelAmountTables.TabIndex = 11;
            this.labelAmountTables.Text = "label1";
            this.labelAmountTables.Visible = false;
            // 
            // labelNameTable
            // 
            this.labelNameTable.AutoSize = true;
            this.labelNameTable.Location = new System.Drawing.Point(215, 44);
            this.labelNameTable.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelNameTable.Name = "labelNameTable";
            this.labelNameTable.Size = new System.Drawing.Size(16, 17);
            this.labelNameTable.TabIndex = 10;
            this.labelNameTable.Text = "  ";
            // 
            // importButton
            // 
            this.importButton.BackColor = System.Drawing.SystemColors.Control;
            this.importButton.ForeColor = System.Drawing.Color.Black;
            this.importButton.Location = new System.Drawing.Point(236, 181);
            this.importButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.importButton.Name = "importButton";
            this.importButton.Size = new System.Drawing.Size(157, 49);
            this.importButton.TabIndex = 9;
            this.importButton.Text = "Importar base datos";
            this.importButton.UseVisualStyleBackColor = false;
            this.importButton.Click += new System.EventHandler(this.importButton_Click);
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(61, 79);
            this.progressBar.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.progressBar.Maximum = 10;
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(332, 46);
            this.progressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBar.TabIndex = 8;
            this.progressBar.Value = 1;
            this.progressBar.Visible = false;
            // 
            // closeButton
            // 
            this.closeButton.BackColor = System.Drawing.SystemColors.Control;
            this.closeButton.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.closeButton.Location = new System.Drawing.Point(276, 249);
            this.closeButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(157, 39);
            this.closeButton.TabIndex = 7;
            this.closeButton.Text = "Salir";
            this.closeButton.UseMnemonic = false;
            this.closeButton.UseVisualStyleBackColor = false;
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // MainForm
            // 
            this.AcceptButton = this.closeButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(820, 421);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Exportar Base de Datos ";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button exportButton;
        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button closeButton;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Button importButton;
        private System.Windows.Forms.Label labelNameTable;
        private System.Windows.Forms.Label labelAmountTables;
        private System.Windows.Forms.Label labelNotClose;
    }
}