namespace UI
{
    partial class frmSeleccionOrders
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
            this.listDocuments = new System.Windows.Forms.CheckedListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnGenerateFile = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.dtDate = new System.Windows.Forms.DateTimePicker();
            this.lblDocNum = new System.Windows.Forms.Label();
            this.lblDate = new System.Windows.Forms.Label();
            this.txtCardCode = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnSelectAll = new System.Windows.Forms.Button();
            this.chb50 = new System.Windows.Forms.CheckBox();
            this.chb64 = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // listDocuments
            // 
            this.listDocuments.FormattingEnabled = true;
            this.listDocuments.Location = new System.Drawing.Point(12, 29);
            this.listDocuments.Name = "listDocuments";
            this.listDocuments.Size = new System.Drawing.Size(891, 174);
            this.listDocuments.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(173, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "Pagos Realizados en SAP";
            // 
            // btnGenerateFile
            // 
            this.btnGenerateFile.Location = new System.Drawing.Point(625, 308);
            this.btnGenerateFile.Name = "btnGenerateFile";
            this.btnGenerateFile.Size = new System.Drawing.Size(278, 74);
            this.btnGenerateFile.TabIndex = 2;
            this.btnGenerateFile.Text = "Generar Archivo para enviar";
            this.btnGenerateFile.UseVisualStyleBackColor = true;
            this.btnGenerateFile.Click += new System.EventHandler(this.btnGenerateFile_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(12, 344);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(252, 38);
            this.btnSearch.TabIndex = 3;
            this.btnSearch.Text = "Buscar";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(13, 225);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(82, 22);
            this.txtSearch.TabIndex = 4;
            // 
            // dtDate
            // 
            this.dtDate.CustomFormat = "##/##/####";
            this.dtDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtDate.Location = new System.Drawing.Point(12, 270);
            this.dtDate.Name = "dtDate";
            this.dtDate.Size = new System.Drawing.Size(167, 22);
            this.dtDate.TabIndex = 5;
            // 
            // lblDocNum
            // 
            this.lblDocNum.AutoSize = true;
            this.lblDocNum.Location = new System.Drawing.Point(12, 207);
            this.lblDocNum.Name = "lblDocNum";
            this.lblDocNum.Size = new System.Drawing.Size(111, 17);
            this.lblDocNum.TabIndex = 6;
            this.lblDocNum.Text = "Nro Documento:";
            // 
            // lblDate
            // 
            this.lblDate.AutoSize = true;
            this.lblDate.Location = new System.Drawing.Point(12, 250);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(108, 17);
            this.lblDate.TabIndex = 7;
            this.lblDate.Text = "Fecha de Pago:";
            // 
            // txtCardCode
            // 
            this.txtCardCode.Location = new System.Drawing.Point(15, 316);
            this.txtCardCode.Name = "txtCardCode";
            this.txtCardCode.Size = new System.Drawing.Size(164, 22);
            this.txtCardCode.TabIndex = 8;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 296);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(126, 17);
            this.label2.TabIndex = 9;
            this.label2.Text = "Código Proveedor:";
            // 
            // btnSelectAll
            // 
            this.btnSelectAll.Location = new System.Drawing.Point(736, 209);
            this.btnSelectAll.Name = "btnSelectAll";
            this.btnSelectAll.Size = new System.Drawing.Size(167, 38);
            this.btnSelectAll.TabIndex = 10;
            this.btnSelectAll.Text = "Seleccionar Todos";
            this.btnSelectAll.UseVisualStyleBackColor = true;
            this.btnSelectAll.Click += new System.EventHandler(this.btnSelectAll_Click);
            // 
            // chb50
            // 
            this.chb50.AutoSize = true;
            this.chb50.Checked = true;
            this.chb50.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chb50.Location = new System.Drawing.Point(190, 281);
            this.chb50.Name = "chb50";
            this.chb50.Size = new System.Drawing.Size(70, 21);
            this.chb50.TabIndex = 11;
            this.chb50.Text = "Ac. 50";
            this.chb50.UseVisualStyleBackColor = true;
            // 
            // chb64
            // 
            this.chb64.AutoSize = true;
            this.chb64.Checked = true;
            this.chb64.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chb64.Location = new System.Drawing.Point(190, 308);
            this.chb64.Name = "chb64";
            this.chb64.Size = new System.Drawing.Size(70, 21);
            this.chb64.TabIndex = 12;
            this.chb64.Text = "Ac. 64";
            this.chb64.UseVisualStyleBackColor = true;
            // 
            // frmSeleccionOrders
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(915, 394);
            this.Controls.Add(this.chb64);
            this.Controls.Add(this.chb50);
            this.Controls.Add(this.btnSelectAll);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtCardCode);
            this.Controls.Add(this.lblDate);
            this.Controls.Add(this.lblDocNum);
            this.Controls.Add(this.dtDate);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.btnGenerateFile);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.listDocuments);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmSeleccionOrders";
            this.Text = "Generar Archivo - 260921";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckedListBox listDocuments;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnGenerateFile;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.DateTimePicker dtDate;
        private System.Windows.Forms.Label lblDocNum;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.TextBox txtCardCode;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnSelectAll;
        private System.Windows.Forms.CheckBox chb50;
        private System.Windows.Forms.CheckBox chb64;
    }
}