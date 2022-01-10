using Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace DAL
{
    public class RetentionRepository
    {
        private string sessionId { get; set; }
        private bool IsHANA { get; set; }
        private Connection connection;

        public RetentionRepository(string sessionId, bool IsHANA = false)
        {
            this.sessionId = sessionId;
            this.IsHANA = IsHANA;
            connection = new Connection();
        }

        public List<Retention> getRetentionByDocEntry(long docEntry)
        {
            List<Retention> listReturn = new List<Retention>();

            string query = $"select T0.WTaxAbsId,T1.WTCode,T1.WTName, T0.WTaxSum  from B1_WTaxView T0 inner join OWTD T1 on T1.AbsEntry = T0.WTaxAbsId where T0.DocEntry = (select top 1 DocEntry from OVPM where Docnum = {docEntry}) and T0.ObjType = 46 and T0.Factor = 1 and T0.WTaxSum > 0 ";
            if (IsHANA)
            {
                query = $"select T0.\"WTaxAbsId\",T1.\"WTCode\",T1.\"WTName\", T0.\"WTaxSum\"  from \"B1_WTaxView\" T0 inner join \"OWTD\" T1 on T1.\"AbsEntry\" = T0.\"WTaxAbsId\" where T0.\"DocEntry\" =  (select top 1 \"DocEntry\" from \"OVPM\" where \"DocNum\" = {docEntry}) and T0.\"ObjType\" = 46 and T0.\"Factor\" = 1 and T0.\"WTaxSum\" > 0 ";
            }

            NumberStyles style;
            CultureInfo provider;
            style = NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands;
            provider = new CultureInfo("en-US");

            XmlDocument xml = new XmlDocument();
            xml.LoadXml(connection.ExecuetQuery(sessionId, query));

            var nodes = xml.SelectNodes("//*[local-name()='row']");
            foreach (XmlNode row in nodes)
            {
                Retention retention = new Retention();

                decimal wTaxSum = decimal.Parse(row.SelectSingleNode(".//*[local-name()='WTaxSum']").InnerText, style, provider);
                retention.WTaxSum = Convert.ToDouble(Math.Round(wTaxSum, 2));
                retention.WTCode = row.SelectSingleNode(".//*[local-name()='WTCode']").InnerText;
                retention.WTName = row.SelectSingleNode(".//*[local-name()='WTName']").InnerText;

                listReturn.Add(retention);
            }

            return listReturn;
        }
    }
}
