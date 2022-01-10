using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace DAL
{
    public class OADMRepository
    {
        private string sessionId { get; set; }
        private bool IsHANA { get; set; }
        private Connection connection;
        public OADMRepository(string sessionId, bool IsHANA = false)
        {
            this.sessionId = sessionId;
            this.IsHANA = IsHANA;
            connection = new Connection();
        }


        public BussinessInfo getBussinessInfo()
        {
            BussinessInfo bussinessInfoReturn = new BussinessInfo();

            string query = $"select top 1 * from OADM";
            if (IsHANA)
            {
                query = $"Select top 1 * from \"OADM\"";
            }

            XmlDocument xml = new XmlDocument();
            xml.LoadXml(connection.ExecuetQuery(sessionId, query));

            var nodes = xml.SelectNodes("//*[local-name()='row']");
            foreach (XmlNode row in nodes)
            {
                bussinessInfoReturn.CompnyAddr = row.SelectSingleNode(".//*[local-name()='CompnyAddr']").InnerText;
                bussinessInfoReturn.CompnyName = row.SelectSingleNode(".//*[local-name()='CompnyName']").InnerText;
                bussinessInfoReturn.TaxIdNum = row.SelectSingleNode(".//*[local-name()='TaxIdNum']").InnerText;
            }

            return bussinessInfoReturn;
        }


    }
}
