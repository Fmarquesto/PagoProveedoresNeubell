using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace DAL
{
    public class OCRDRepository
    {
        private string sessionId { get; set; }
        private bool IsHANA { get; set; }
        private Connection connection;
        public OCRDRepository(string sessionId, bool IsHANA = false)
        {
            this.sessionId = sessionId;
            this.IsHANA = IsHANA;
            connection = new Connection();
        }

        public Bp getBp(string cardCode)
        {
            Bp bpReturn = new Bp();

            string query = $"select CardCode,CardName,LicTradNum,DflIBAN from OCRD where CardCode = '{cardCode}'";
            if (IsHANA)
            {
                query = $"Select \"CardCode\",\"CardName\",\"LicTradNum\", \"DflIBAN\" from \"OCRD\" where \"CardCode\" = '{cardCode}' ";
            }

            XmlDocument xml = new XmlDocument();
            xml.LoadXml(connection.ExecuetQuery(sessionId, query));

            var nodes = xml.SelectNodes("//*[local-name()='row']");
            foreach (XmlNode row in nodes)
            {
                bpReturn.CardCode = row.SelectSingleNode(".//*[local-name()='CardCode']").InnerText;
                bpReturn.CardFName = row.SelectSingleNode(".//*[local-name()='CardName']").InnerText;
                bpReturn.LicTradNum = row.SelectSingleNode(".//*[local-name()='LicTradNum']").InnerText;
                bpReturn.CBU = row.SelectSingleNode(".//*[local-name()='DflIBAN']").InnerText;
            }

            return bpReturn;
        }
    }
}
