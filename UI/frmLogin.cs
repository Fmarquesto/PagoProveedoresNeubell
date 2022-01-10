using DAL;
using Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UI
{
    public partial class FrmLogin : Form
    {
        public FrmLogin()
        {
            InitializeComponent();
            ReadAppConfig();
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            string error;
            if (Validate(out error))
            {
                string sessionID = string.Empty;

                using (var connection = new Connection())
                {
                    sessionID = connection.getSessionId(new Company
                    {
                        CompanyDB = txtCompanyDb.Text,
                        DbUserName = txtDbUserName.Text,
                        DbPassword = txtPswDB.Text,
                        LicenseServer = txtLicenseServer.Text.Split(':')[0],
                        PortNumber = Convert.ToInt32(txtLicenseServer.Text.Split(':')[1]),
                        Server = txtServer.Text,
                        UserName = txtUserNameSAP.Text,
                        Password = txtPasswordSAP.Text,
                        ServerType = cmbTypeDB.Text
                    });
                }

                if (sessionID != "" && !sessionID.StartsWith("Error"))
                {
                    this.Hide();
                    Program.SessionId = sessionID;

                    if (cmbTypeDB.Text == "dst_HANADB")
                    {
                        Program.IsHana = true;
                    }

                    frmSeleccionOrders frmSeleccionOrders = new frmSeleccionOrders();
                    frmSeleccionOrders.ShowDialog();
                    this.Close();

                }
                else
                {
                    MessageBox.Show($"No se pudo conectar a la empresa - {sessionID}", "Pagos Proveedores", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            else
            {
                MessageBox.Show(error, "Pagos Proveedores", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private bool Validate(out string error)
        {

            if (string.IsNullOrEmpty(txtServer.Text))
            {
                error = "Por favor ingrese el servidor de Base de Datos";
                return false;
            }

            if (string.IsNullOrEmpty(txtCompanyDb.Text))
            {
                error = "Por favor ingrese la base de datos de la empresa";
                return false;
            }

            if (string.IsNullOrEmpty(cmbTypeDB.Text))
            {
                error = "Por favor ingrese el tipo de Base de Datos";
                return false;
            }

            if (string.IsNullOrEmpty(txtDbUserName.Text))
            {
                error = "Por favor ingrese el usuario de la base de datos";
                return false;
            }

            if (string.IsNullOrEmpty(txtPswDB.Text))
            {
                error = "Por favor ingrese el password de la base de datos";
                return false;
            }

            if (string.IsNullOrEmpty(txtUserNameSAP.Text))
            {
                error = "Por favor ingrese el usuario de SAP";
                return false;
            }

            if (string.IsNullOrEmpty(txtPasswordSAP.Text))
            {
                error = "Por favor ingrese el password de SAP";
                return false;
            }

            if (string.IsNullOrEmpty(txtLicenseServer.Text))
            {
                error = "Por favor ingrese el servidor de licencia con su puerto";
                return false;
            }

            error = string.Empty;
            return true;
        }

        private void ReadAppConfig()
        {
            txtServer.Text = System.Configuration.ConfigurationSettings.AppSettings["Server"];
            txtCompanyDb.Text = System.Configuration.ConfigurationSettings.AppSettings["Company"];
            cmbTypeDB.Text = System.Configuration.ConfigurationSettings.AppSettings["TypeDB"];
            txtDbUserName.Text = System.Configuration.ConfigurationSettings.AppSettings["UserDB"];
            txtPswDB.Text = System.Configuration.ConfigurationSettings.AppSettings["PSWDB"];
            txtUserNameSAP.Text = System.Configuration.ConfigurationSettings.AppSettings["UserSAP"];
            txtPasswordSAP.Text = System.Configuration.ConfigurationSettings.AppSettings["PswSAP"];
            txtLicenseServer.Text = System.Configuration.ConfigurationSettings.AppSettings["ServerLicenceAndPOrt"];
        }
    }
}
