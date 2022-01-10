using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class Connection:IDisposable
    {
        private SBODI_Server.Node DIServerNode;

        public string getSessionId(Company company)
        {
            SBODI_Server.Node DISnode = null;
            string sSOAPans = null, sCmd = null;

            try
            {
                DISnode = new SBODI_Server.Node();

                sCmd = @"<?xml version=""1.0"" encoding=""UTF-16""?>";
                sCmd += @"<env:Envelope xmlns:env=""http://schemas.xmlsoap.org/soap/envelope/"">";
                sCmd += @"<env:Body><dis:Login xmlns:dis=""http://www.sap.com/SBO/DIS"">";
                sCmd += "<DatabaseServer>" + company.Server + "</DatabaseServer>";
                sCmd += "<DatabaseName>" + company.CompanyDB + "</DatabaseName>";
                sCmd += "<DatabaseType>" + company.ServerType + "</DatabaseType>";
                sCmd += "<DatabaseUsername>" + company.DbUserName + "</DatabaseUsername>";
                sCmd += "<DatabasePassword>" + company.DbPassword + "</DatabasePassword>";
                sCmd += "<CompanyUsername>" + company.UserName + "</CompanyUsername>";
                sCmd += "<CompanyPassword>" + company.Password + "</CompanyPassword>";
                sCmd += "<Language>" + "ln_English" + "</Language>";
                sCmd += "<LicenseServer>" + company.LicenseServer + ":" + company.PortNumber + "</LicenseServer>";
                sCmd += "</dis:Login></env:Body></env:Envelope>";

                sSOAPans = DISnode.Interact(sCmd);

                //  Parse the SOAP answer
                System.Xml.XmlValidatingReader xmlValid = null;
                string sRet = null;
                xmlValid = new System.Xml.XmlValidatingReader(sSOAPans, System.Xml.XmlNodeType.Document, null);
                while (xmlValid.Read())
                {
                    if (xmlValid.NodeType == System.Xml.XmlNodeType.Text)
                    {
                        if (sRet == null)
                        {
                            sRet += xmlValid.Value;
                        }
                        else
                        {
                            if (sRet.StartsWith("Error"))
                            {
                                sRet += " " + xmlValid.Value;
                            }
                            else
                            {
                                sRet = "Error " + sRet + " " + xmlValid.Value;
                            }
                        }
                    }
                }
                
                return sRet;
            }
            catch (Exception ex)
            {
                return "Error:" + ex.Message;
            }
            finally
            {
                ReleaseObject((object)DISnode);
            }



        }

        public string ExecuetQuery(string sessionId, string query)
        {
            DIServerNode = (SBODI_Server.Node)new SBODI_Server.Node();

            string soapRequest =
    @"<?xml version=""1.0""?>
<env:Envelope xmlns:env=""http://schemas.xmlsoap.org/soap/envelope/"">
 <env:Header>
  <SessionID>{0}</SessionID>
 </env:Header>
 <env:Body>
  <dis:ExecuteSQL xmlns:dis=""http://www.sap.com/SBO/DIS"">
   <DoQuery>{1}</DoQuery>
  </dis:ExecuteSQL>
 </env:Body>
</env:Envelope>";

            soapRequest = string.Format(soapRequest, sessionId, query);
            var aaaa = DIServerNode.Interact(soapRequest);
            return DIServerNode.Interact(soapRequest);
        }

        public void ReleaseObject(object pObj)
        {
            try
            {
                if (pObj != null)
                {
                    if (System.Runtime.InteropServices.Marshal.IsComObject(pObj))
                    {
                        System.Runtime.InteropServices.Marshal.ReleaseComObject(pObj);
                    }

                    pObj = null;
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                }

            }
            catch (Exception)
            {
            }
        }

        void IDisposable.Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
