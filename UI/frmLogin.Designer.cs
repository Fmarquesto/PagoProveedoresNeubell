namespace UI
{
    partial class FrmLogin
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
            this.btnConnect = new System.Windows.Forms.Button();
            this.txtServer = new System.Windows.Forms.TextBox();
            this.txtCompanyDb = new System.Windows.Forms.TextBox();
            this.cmbTypeDB = new System.Windows.Forms.ComboBox();
            this.txtDbUserName = new System.Windows.Forms.TextBox();
            this.txtPswDB = new System.Windows.Forms.TextBox();
            this.txtLicenseServer = new System.Windows.Forms.TextBox();
            this.txtUserNameSAP = new System.Windows.Forms.TextBox();
            this.txtPasswordSAP = new System.Windows.Forms.TextBox();
            this.lblServer = new System.Windows.Forms.Label();
            this.lblCompanyDb = new System.Windows.Forms.Label();
            this.lblTypeDB = new System.Windows.Forms.Label();
            this.lblDbUserName = new System.Windows.Forms.Label();
            this.lblPswDB = new System.Windows.Forms.Label();
            this.lblUserNameSAP = new System.Windows.Forms.Label();
            this.lblPasswordSAP = new System.Windows.Forms.Label();
            this.lblLicenseServer = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(7, 428);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(300, 47);
            this.btnConnect.TabIndex = 0;
            this.btnConnect.Text = "Conectarce";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.UseWaitCursor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // txtServer
            // 
            this.txtServer.Location = new System.Drawing.Point(8, 38);
            this.txtServer.Name = "txtServer";
            this.txtServer.Size = new System.Drawing.Size(300, 22);
            this.txtServer.TabIndex = 1;
            this.txtServer.UseWaitCursor = true;
            // 
            // txtCompanyDb
            // 
            this.txtCompanyDb.Location = new System.Drawing.Point(8, 86);
            this.txtCompanyDb.Name = "txtCompanyDb";
            this.txtCompanyDb.Size = new System.Drawing.Size(300, 22);
            this.txtCompanyDb.TabIndex = 2;
            this.txtCompanyDb.UseWaitCursor = true;
            // 
            // cmbTypeDB
            // 
            this.cmbTypeDB.FormattingEnabled = true;
            this.cmbTypeDB.Items.AddRange(new object[] {
            "dst_MSSQL",
            "dst_MSSQL2005",
            "dst_MSSQL2008",
            "dst_MSSQL2012",
            "dst_MSSQL2014",
            "dst_MSSQL2016",
            "dst_HANADB"});
            this.cmbTypeDB.Location = new System.Drawing.Point(8, 138);
            this.cmbTypeDB.Name = "cmbTypeDB";
            this.cmbTypeDB.Size = new System.Drawing.Size(299, 24);
            this.cmbTypeDB.TabIndex = 3;
            this.cmbTypeDB.UseWaitCursor = true;
            // 
            // txtDbUserName
            // 
            this.txtDbUserName.Location = new System.Drawing.Point(7, 187);
            this.txtDbUserName.Name = "txtDbUserName";
            this.txtDbUserName.Size = new System.Drawing.Size(300, 22);
            this.txtDbUserName.TabIndex = 4;
            this.txtDbUserName.UseWaitCursor = true;
            // 
            // txtPswDB
            // 
            this.txtPswDB.Location = new System.Drawing.Point(7, 236);
            this.txtPswDB.Name = "txtPswDB";
            this.txtPswDB.PasswordChar = '*';
            this.txtPswDB.Size = new System.Drawing.Size(300, 22);
            this.txtPswDB.TabIndex = 5;
            this.txtPswDB.UseWaitCursor = true;
            // 
            // txtLicenseServer
            // 
            this.txtLicenseServer.Location = new System.Drawing.Point(7, 386);
            this.txtLicenseServer.Name = "txtLicenseServer";
            this.txtLicenseServer.Size = new System.Drawing.Size(300, 22);
            this.txtLicenseServer.TabIndex = 6;
            this.txtLicenseServer.UseWaitCursor = true;
            // 
            // txtUserNameSAP
            // 
            this.txtUserNameSAP.Location = new System.Drawing.Point(7, 287);
            this.txtUserNameSAP.Name = "txtUserNameSAP";
            this.txtUserNameSAP.Size = new System.Drawing.Size(300, 22);
            this.txtUserNameSAP.TabIndex = 7;
            this.txtUserNameSAP.UseWaitCursor = true;
            // 
            // txtPasswordSAP
            // 
            this.txtPasswordSAP.Location = new System.Drawing.Point(7, 337);
            this.txtPasswordSAP.Name = "txtPasswordSAP";
            this.txtPasswordSAP.PasswordChar = '*';
            this.txtPasswordSAP.Size = new System.Drawing.Size(300, 22);
            this.txtPasswordSAP.TabIndex = 8;
            this.txtPasswordSAP.UseWaitCursor = true;
            // 
            // lblServer
            // 
            this.lblServer.AutoSize = true;
            this.lblServer.Location = new System.Drawing.Point(10, 18);
            this.lblServer.Name = "lblServer";
            this.lblServer.Size = new System.Drawing.Size(61, 17);
            this.lblServer.TabIndex = 9;
            this.lblServer.Text = "Servidor";
            this.lblServer.UseWaitCursor = true;
            // 
            // lblCompanyDb
            // 
            this.lblCompanyDb.AutoSize = true;
            this.lblCompanyDb.Location = new System.Drawing.Point(9, 66);
            this.lblCompanyDb.Name = "lblCompanyDb";
            this.lblCompanyDb.Size = new System.Drawing.Size(71, 17);
            this.lblCompanyDb.TabIndex = 10;
            this.lblCompanyDb.Text = "Compañia";
            this.lblCompanyDb.UseWaitCursor = true;
            // 
            // lblTypeDB
            // 
            this.lblTypeDB.AutoSize = true;
            this.lblTypeDB.Location = new System.Drawing.Point(9, 118);
            this.lblTypeDB.Name = "lblTypeDB";
            this.lblTypeDB.Size = new System.Drawing.Size(153, 17);
            this.lblTypeDB.TabIndex = 11;
            this.lblTypeDB.Text = "Tipo de Base de Datos";
            this.lblTypeDB.UseWaitCursor = true;
            // 
            // lblDbUserName
            // 
            this.lblDbUserName.AutoSize = true;
            this.lblDbUserName.Location = new System.Drawing.Point(10, 167);
            this.lblDbUserName.Name = "lblDbUserName";
            this.lblDbUserName.Size = new System.Drawing.Size(80, 17);
            this.lblDbUserName.TabIndex = 12;
            this.lblDbUserName.Text = "Usuario DB";
            this.lblDbUserName.UseWaitCursor = true;
            // 
            // lblPswDB
            // 
            this.lblPswDB.AutoSize = true;
            this.lblPswDB.Location = new System.Drawing.Point(10, 216);
            this.lblPswDB.Name = "lblPswDB";
            this.lblPswDB.Size = new System.Drawing.Size(69, 17);
            this.lblPswDB.TabIndex = 13;
            this.lblPswDB.Text = "Password";
            this.lblPswDB.UseWaitCursor = true;
            // 
            // lblUserNameSAP
            // 
            this.lblUserNameSAP.AutoSize = true;
            this.lblUserNameSAP.Location = new System.Drawing.Point(9, 266);
            this.lblUserNameSAP.Name = "lblUserNameSAP";
            this.lblUserNameSAP.Size = new System.Drawing.Size(88, 17);
            this.lblUserNameSAP.TabIndex = 14;
            this.lblUserNameSAP.Text = "Usuario SAP";
            this.lblUserNameSAP.UseWaitCursor = true;
            // 
            // lblPasswordSAP
            // 
            this.lblPasswordSAP.AutoSize = true;
            this.lblPasswordSAP.Location = new System.Drawing.Point(9, 315);
            this.lblPasswordSAP.Name = "lblPasswordSAP";
            this.lblPasswordSAP.Size = new System.Drawing.Size(153, 17);
            this.lblPasswordSAP.TabIndex = 15;
            this.lblPasswordSAP.Text = "Usuario SAP Password";
            this.lblPasswordSAP.UseWaitCursor = true;
            // 
            // lblLicenseServer
            // 
            this.lblLicenseServer.AutoSize = true;
            this.lblLicenseServer.Location = new System.Drawing.Point(9, 365);
            this.lblLicenseServer.Name = "lblLicenseServer";
            this.lblLicenseServer.Size = new System.Drawing.Size(194, 17);
            this.lblLicenseServer.TabIndex = 16;
            this.lblLicenseServer.Text = "Servidor de Licencia y Puerto";
            this.lblLicenseServer.UseWaitCursor = true;
            // 
            // FrmLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(321, 487);
            this.Controls.Add(this.lblLicenseServer);
            this.Controls.Add(this.lblPasswordSAP);
            this.Controls.Add(this.lblUserNameSAP);
            this.Controls.Add(this.lblPswDB);
            this.Controls.Add(this.lblDbUserName);
            this.Controls.Add(this.lblTypeDB);
            this.Controls.Add(this.lblCompanyDb);
            this.Controls.Add(this.lblServer);
            this.Controls.Add(this.txtPasswordSAP);
            this.Controls.Add(this.txtUserNameSAP);
            this.Controls.Add(this.txtLicenseServer);
            this.Controls.Add(this.txtPswDB);
            this.Controls.Add(this.txtDbUserName);
            this.Controls.Add(this.cmbTypeDB);
            this.Controls.Add(this.txtCompanyDb);
            this.Controls.Add(this.txtServer);
            this.Controls.Add(this.btnConnect);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FrmLogin";
            this.Text = "Login";
            this.UseWaitCursor = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.TextBox txtServer;
        private System.Windows.Forms.TextBox txtCompanyDb;
        private System.Windows.Forms.ComboBox cmbTypeDB;
        private System.Windows.Forms.TextBox txtDbUserName;
        private System.Windows.Forms.TextBox txtPswDB;
        private System.Windows.Forms.TextBox txtLicenseServer;
        private System.Windows.Forms.TextBox txtUserNameSAP;
        private System.Windows.Forms.TextBox txtPasswordSAP;
        private System.Windows.Forms.Label lblServer;
        private System.Windows.Forms.Label lblCompanyDb;
        private System.Windows.Forms.Label lblTypeDB;
        private System.Windows.Forms.Label lblDbUserName;
        private System.Windows.Forms.Label lblPswDB;
        private System.Windows.Forms.Label lblUserNameSAP;
        private System.Windows.Forms.Label lblPasswordSAP;
        private System.Windows.Forms.Label lblLicenseServer;
    }
}

