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
    public class OVPMRepository
    {
        private string sessionId { get; set; }
        private bool IsHANA { get; set; }
        private Connection connection;
        public OVPMRepository(string sessionId, bool IsHANA = false)
        {
            this.sessionId = sessionId;
            this.IsHANA = IsHANA;
            connection = new Connection();
        }


        public List<Order> getDocuments(DateTime date, string businesspartner = "", string docNum = "", bool ac50 = true, bool ac64 = true)
        {
            List<Order> listRetrun = new List<Order>();

            string dateTime = date.ToString("yyyy-MM-dd");

            string ac50str = (IsHANA == true ? "\"TrsfrSum\" is not null and \"TrsfrSum\" > 0  and (Select  \"BankCode\" from \"OCRD\" where \"CardCode\" = \"OVPM\".\"CardCode\") = 72 )" : "TrsfrSum is not null and TrsfrSum > 0 and (Select BankCode from OCRD where CardCode = OVPM.CardCode) = 72 )");
            string ac64str = (IsHANA == true ? "\"CheckSum\" is not null and \"CheckSum\" > 0" : "CheckSum is not null and  CheckSum > 0");

            //string query = $"Select DocNum from OVPM where DocCurr = 'ARS' and Canceled = 'N' and ((CheckSum is not null and  CheckSum > 0) or (TrsfrSum is not null and TrsfrSum > 0 and (Select BankCode from OCRD where CardCode = OVPM.CardCode) = 72 )) and DocDate = '{dateTime}' and ({(businesspartner == "" ? "1 = 1" : "CardCode = '" + businesspartner + "'")}) and ({(docNum == "" ? "1 = 1" : "DocNum = " + docNum)})";
            //if (IsHANA)
            //{
            //    query = $"Select \"DocNum\" from \"OVPM\" where \"DocCurr\" = 'ARS' and \"Canceled\" = 'N' and ((\"CheckSum\" is not null and \"CheckSum\" > 0)  or (\"TrsfrSum\" is not null and \"TrsfrSum\" > 0  and (Select  \"BankCode\" from \"OCRD\" where \"CardCode\" = \"OVPM\".\"CardCode\") = 72 )) and \"DocDate\" = '{dateTime}' and ({(businesspartner == "" ? "1 = 1" : "\"CardCode\" = '" + businesspartner + "'")}) and ({(docNum == "" ? "1 = 1" : "\"DocNum\" = " + docNum)})";
            //}
            
            string query = $"Select DocNum from OVPM where DocCurr = 'ARS' and Canceled = 'N' and (({ (ac64 == true ? ac64str : "1 = 0")}) or ({ (ac50 == true ? ac50str : "1 = 0)")}) and DocDate = '{dateTime}' and ({(businesspartner == "" ? "1 = 1" : "CardCode = '" + businesspartner + "'")}) and ({(docNum == "" ? "1 = 1" : "DocNum = " + docNum)})";
            if (IsHANA)
            {
                query = $"Select \"DocNum\" from \"OVPM\" where \"DocCurr\" = 'ARS' and \"Canceled\" = 'N' and (({ (ac64 == true ? ac64str : "1 = 0")})  or ({ (ac50 == true ? ac50str : "1 = 0)")}) and \"DocDate\" = '{dateTime}' and ({(businesspartner == "" ? "1 = 1" : "\"CardCode\" = '" + businesspartner + "'")}) and ({(docNum == "" ? "1 = 1" : "\"DocNum\" = " + docNum)})";
            }

            XmlDocument xml = new XmlDocument();
            xml.LoadXml(connection.ExecuetQuery(sessionId, query));

            var nodes = xml.SelectNodes("//*[local-name()='row']");
            foreach (XmlNode row in nodes)
            {
                listRetrun.Add(new Order { DocNum = Convert.ToInt32(row.SelectSingleNode(".//*[local-name()='DocNum']").InnerText) });
            }

            return listRetrun;
        }

        public Order getDocument(string docNum, string agreement)
        {
            Order Docreturn = new Order();

            string fiterAgreement = string.Empty;

            if (agreement == "01")
            {
                fiterAgreement = "and TrsfrSum > 0 and (Select BankCode from OCRD where CardCode = OVPM.CardCode) = 72";
                if (IsHANA)
                {
                    fiterAgreement = "and \"TrsfrSum\" > 0 and (Select  \"BankCode\" from \"OCRD\" where \"CardCode\" = \"OVPM\".\"CardCode\") = 72";
                }
            }
            else
            {
                fiterAgreement = "and CheckSum > 0";
                if (IsHANA)
                {
                    fiterAgreement = "and \"CheckSum\" > 0";
                }
            }

            string query = $"select DocNum, DocTotal, DocDueDate, CardCode, CheckSum, TrsfrSum from OVPM where DocNum = {docNum} {fiterAgreement}";
            if (IsHANA)
            {
                query = $"Select \"DocNum\",\"DocTotal\",\"DocDueDate\", \"CardCode\", \"TrsfrSum\",\"CheckSum\" from \"OVPM\" where \"DocNum\" = {docNum} {fiterAgreement}";
            }


            XmlDocument xml = new XmlDocument();
            xml.LoadXml(connection.ExecuetQuery(sessionId, query));

            var nodes = xml.SelectNodes("//*[local-name()='row']");
            foreach (XmlNode row in nodes)
            {
                Docreturn.DocNum = Convert.ToInt32(row.SelectSingleNode(".//*[local-name()='DocNum']").InnerText);
                string field = string.Empty;
                if (agreement == "01")
                {
                    field = "TrsfrSum";
                }
                else
                {
                    field = "CheckSum";
                }

                NumberStyles style;
                CultureInfo provider;
                style = NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands;
                provider = new CultureInfo("en-US");

                decimal docTotal = Decimal.Parse(row.SelectSingleNode($".//*[local-name()='{field}']").InnerText, style, provider);
                Docreturn.DocTotal = Convert.ToDouble(Math.Round(docTotal, 2)); //Convert.ToInt32(docTotal);
                //Docreturn.test = Decimal.Parse(row.SelectSingleNode($".//*[local-name()='{field}']").InnerText, style, provider).ToString();
                Docreturn.DocDueDate = row.SelectSingleNode(".//*[local-name()='DocDueDate']").InnerText;
                Docreturn.CardCode = row.SelectSingleNode(".//*[local-name()='CardCode']").InnerText;
            }

            return Docreturn;
        }

    }
}
