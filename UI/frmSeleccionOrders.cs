using DAL;
using Model;
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

namespace UI
{
    public partial class frmSeleccionOrders : Form
    {
        private List<Order> listOrderInSAP;
        private List<Order> listOrderSelected;

        public frmSeleccionOrders()
        {
            InitializeComponent();
            IniObjects();
        }

        private void IniObjects()
        {
            listOrderInSAP = new List<Order>();
            listOrderSelected = new List<Order>();
            var objOVPMRepository = new OVPMRepository(Program.SessionId, Program.IsHana);

            //Here Call Get Docuemntes in SAP
            listOrderInSAP = objOVPMRepository.getDocuments(DateTime.Now);

            foreach (var item in listOrderInSAP)
            {
                listDocuments.Items.Add(item.DocNum);
            }
        }

        private void btnGenerateFile_Click(object sender, EventArgs e)
        {
            StreamWriter sw;

            string fileNameEnd = string.Empty;

            for (int agreement = 0; agreement <= 1; agreement++)
            {
                string fileName = string.Empty;
                string codeAgreement = string.Empty;

                //agreement = 0 -> 01
                if (agreement == 0)
                {
                    fileName = "PP50" + DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + ".txt";
                    codeAgreement = "01";
                    if(!chb50.Checked)
                    {
                        continue;
                    }
                }
                else
                {
                    fileName = "PP64" + DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + ".txt";
                    codeAgreement = "03";

                    if (!chb64.Checked)
                    {
                        continue;
                    }
                }

                if (!File.Exists("Pagos/" + fileName))
                {
                    File.Create("Pagos/" + fileName).Close();
                    sw = File.AppendText("Pagos/" + fileName);
                }
                else
                {
                    File.Delete("Pagos/" + fileName);
                    File.Create("Pagos/" + fileName).Close();
                    sw = File.AppendText("Pagos/" + fileName);
                }

                createHeader(sw, codeAgreement);

                double totalFileAmount = 0;
                long totalLines = 0;

                int i;
                for (i = 0; i <= (listDocuments.Items.Count - 1); i++)
                {
                    if (listDocuments.GetItemChecked(i))
                    {
                        double totalAmount = 0;

                        var objOVPMRepository = new OVPMRepository(Program.SessionId, Program.IsHana);

                        var orderToProccess = objOVPMRepository.getDocument(listDocuments.Items[i].ToString(), codeAgreement);

                        if (orderToProccess.DocNum != 0)
                        {

                            creteDetail(sw, listDocuments.Items[i].ToString(), out totalAmount, codeAgreement);

                            var objRetentionRepository = new RetentionRepository(Program.SessionId, Program.IsHana);

                            List<Retention> retentions = objRetentionRepository.getRetentionByDocEntry(Convert.ToInt64(listDocuments.Items[i].ToString()));

                            int retentionNum = 1;

                            foreach (var item in retentions)
                            {
                                createRetention(sw, listDocuments.Items[i].ToString(), retentionNum, item, codeAgreement, totalAmount);

                                retentionNum++;
                            }

                            totalLines++;
                            totalFileAmount += totalAmount;
                        }
                    }
                }

                createTrailer(sw, totalFileAmount, totalLines);

                sw.Close();

                if (totalFileAmount == 0)
                {
                    File.Delete("Pagos/" + fileName);
                }
            }

            string appPath = Path.GetDirectoryName(Application.ExecutablePath) + "/Pagos";

            MessageBox.Show($"Se genero los archivo {fileNameEnd} en la carpeta {appPath}", "Pagos Proveedores", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void createHeader(StreamWriter sw, string agreement)
        {
            StringBuilder header = new StringBuilder();
            header.Append("H");
            header.Append(System.Configuration.ConfigurationSettings.AppSettings["CUIT"]);
            header.Append("0");
            header.Append("010");
            header.Append(agreement);
            header.Append("007");
            header.Append("00001");
            header.Append("00000");
            header.Append("".PadRight(619, ' '));

            sw.WriteLine(header.ToString());

        }

        private void creteDetail(StreamWriter sw, string DocNum, out double totalAmount, string agreement)
        {


            var objOVPMRepository = new OVPMRepository(Program.SessionId, Program.IsHana);
            var objOCRDRepository = new OCRDRepository(Program.SessionId, Program.IsHana);

            var orderToProccess = objOVPMRepository.getDocument(DocNum, agreement);

            var bp = objOCRDRepository.getBp(orderToProccess.CardCode);

            totalAmount = orderToProccess.DocTotal;

            StringBuilder detail = new StringBuilder();
            detail.Append("D");//1
            detail.Append(" ");//2
            detail.Append("0");//3
            //detail.Append("000000000000000");//CardCode
            detail.Append(bp.CardCode.PadRight(15, ' '));//4
            detail.Append("OP");//5
            //detail.Append("000000000000000");//DocNum
            detail.Append(DocNum.PadRight(15, ' '));//6
            detail.Append("0000");//7
                                  //detail.Append("                              ");//CardName

            if (bp.CardFName.Length > 30)//8
            {
                detail.Append(bp.CardFName.Substring(0, 28).ToString().PadRight(30, ' '));
            }
            else
            {
                detail.Append(bp.CardFName.ToString().PadRight(30, ' '));
            }


            detail.Append("S/D                           ");//9
            detail.Append("S/D                 ");//10
            detail.Append(" ");//11
            detail.Append("01000");//12
            detail.Append("   ");//13
            detail.Append(" ");//14
            detail.Append("".PadRight(83, '0'));//15
            detail.Append("".PadRight(11, ' '));//16
            detail.Append(System.Configuration.ConfigurationSettings.AppSettings["CUIT"]);//CUIT 17
            detail.Append("".PadRight(45, ' '));//18
            detail.Append("".PadRight(18, ' '));//19
            detail.Append("".PadRight(15, ' '));//20
            detail.Append("".PadRight(15, ' '));//21
            detail.Append("".PadRight(60, ' '));//22
            detail.Append("001");//23
            detail.Append("001");//24
            detail.Append(agreement == "01" ? "002" : "011");//Ver con ellos - 25 Tipo de Emisión del cheque
            detail.Append("S");//26
            detail.Append(agreement == "01" ? "0054" : "0000");//27

            if (agreement == "01" && !string.IsNullOrEmpty(bp.CBU))
            {
                var cbu = bp.CBU;
                var cbuFirst = cbu.Substring(0, 8);
                var cbuLast = cbu.Substring(8, 14);
                var cbuToApplique = "0" + cbuFirst + "000" + cbuLast;
                detail.Append(agreement == "01" ? cbuToApplique : "00000000000000000000000000");//28
            }
            else
            {
                detail.Append("00000000000000000000000000");//28
            }


            detail.Append(orderToProccess.DocDueDate);//Fecha documentom 29
            detail.Append(orderToProccess.DocDueDate);//Fecha de Pago -30 Fecha de Pago
            //detail.Append("000000000000000"); //Importe sin coma
            detail.Append(orderToProccess.DocTotal.ToString("F").Replace(",", "").Replace(".", "").PadLeft(15, '0')); //31

            detail.Append(agreement == "01" ? "50" : "64");//32
            detail.Append("   ");//33
            detail.Append("00000000000");//34
            detail.Append("   ");//35
            detail.Append("00000000000");//36
            detail.Append("   ");//37
            detail.Append("00000000000");//38
            detail.Append("   ");//39

            detail.Append(DocNum.PadLeft(19, '0'));//40
            detail.Append("0");//41
            detail.Append("000");//42
            detail.Append("00");//43
            detail.Append(" ");//44
            detail.Append(bp.CardCode.PadRight(60, ' '));//CardCode 45

            detail.Append("".PadRight(59, ' ')); //46

            sw.WriteLine(detail.ToString());


        }

        private void createRetention(StreamWriter sw, string DocNum, int retentionNum, Retention retentionDoc, string agreement = "", double totalLine = 0)
        {
            var objOVPMRepository = new OVPMRepository(Program.SessionId, Program.IsHana);
            var objOCRDRepository = new OCRDRepository(Program.SessionId, Program.IsHana);
            var objOADMRepository = new OADMRepository(Program.SessionId, Program.IsHana);

            var orderToProccess = objOVPMRepository.getDocument(DocNum, agreement);
            var bp = objOCRDRepository.getBp(orderToProccess.CardCode);
            var bussinessInfo = objOADMRepository.getBussinessInfo();

            var retentionCode = GetCodeRetention(retentionDoc.WTCode);

            if (retentionCode != "")
            {
                StringBuilder retention = new StringBuilder();

                switch (retentionCode)
                {
                    case "001":
                        //Retencion IVA
                        retentionCode = "001";
                        retention.Append("I");//1
                        retention.Append(retentionCode);//Logica de Retencion//2
                        retention.Append(retentionNum.ToString().PadLeft(2, '0'));//3
                        retention.Append("01");//4
                        retention.Append("                   ");//5
                        retention.Append("001");//6
                        retention.Append("001");//7
                        retention.Append(orderToProccess.DocDueDate);//8
                        retention.Append("         "); //9
                        retention.Append(retentionNum.ToString().PadLeft(17, '0'));//10
                        retention.Append(bussinessInfo.TaxIdNum);//11
                        retention.Append("                                  ");//12
                        if (retentionDoc.WTName.Length > 30)//Descripcion Retencion - 29
                        {
                            retention.Append(retentionDoc.WTName.Substring(0, 28).ToString().PadRight(30, ' '));
                        }
                        else
                        {
                            retention.Append(retentionDoc.WTName.ToString().PadRight(30, ' '));
                        }
                        retention.Append("S/D                           ");//14
                        retention.Append(" ");//15
                        retention.Append("01000");//16
                        retention.Append("   ");//17
                        retention.Append("S/D                 ");//18
                        retention.Append(bp.LicTradNum.PadRight(11, ' '));//19
                        retention.Append("                                  ");//20
                        if (bp.CardFName.Length > 30)//21
                        {
                            retention.Append(bp.CardFName.Substring(0, 28).ToString().PadRight(30, ' '));
                        }
                        else
                        {
                            retention.Append(bp.CardFName.ToString().PadRight(30, ' '));
                        }
                        retention.Append("S/D                           ");//22
                        retention.Append(" ");//23
                        retention.Append("01000");//24
                        retention.Append("   ");//25
                        retention.Append("S/D                 ");//26
                        if (retentionDoc.WTName.Length > 40)//27
                        {
                            retention.Append(retentionDoc.WTName.Substring(0, 38).ToString().PadRight(40, ' '));
                        }
                        else
                        {
                            retention.Append(retentionDoc.WTName.ToString().PadRight(40, ' '));
                        }
                        retention.Append(orderToProccess.DocNum.ToString().PadRight(40, ' '));//28
                        retention.Append(totalLine.ToString("F").Replace(",", "").Replace(".", "").PadLeft(15, '0'));//29
                        retention.Append(retentionDoc.WTaxSum.ToString("F").Replace(",", "").Replace(".", "").PadLeft(15, '0'));//30
                        retention.Append("000000000000000000000000000000");//31
                        retention.Append("000000000");//32
                        retention.Append("000000000000000000000000000");//33
                        retention.Append(orderToProccess.DocDueDate);//34
                        retention.Append("000000000000000000000000000000000000000000000000");//35
                        retention.Append("00");//36
                        retention.Append("000000");//37
                        retention.Append("                    ");//38
                        retention.Append("                    ");//39
                        retention.Append("                                   ");//40
                        break;
                    case "002":
                        //Retencion Ganancia
                        retentionCode = "002";
                        retention.Append("I");//1
                        retention.Append(retentionCode);//Logica de Retencion//2
                        retention.Append(retentionNum.ToString().PadLeft(2, '0'));//3
                        retention.Append("01");//4
                        retention.Append("                   ");//5
                        retention.Append("001");//6
                        retention.Append("001");//7
                        retention.Append(orderToProccess.DocDueDate);//8
                        retention.Append("         ");//9
                        retention.Append(retentionNum.ToString().PadLeft(17, '0'));//10
                        retention.Append(bussinessInfo.TaxIdNum);//11
                        retention.Append("                                  ");//12
                        if (bussinessInfo.CompnyName.Length > 30)//13
                        {
                            retention.Append(bussinessInfo.CompnyName.Substring(0, 28).ToString().PadRight(30, ' '));//14
                        }
                        else
                        {
                            retention.Append(bussinessInfo.CompnyName.ToString().PadRight(30, ' '));
                        }
                        retention.Append("S/D                           ");//14
                        retention.Append(" ");//15
                        retention.Append("01000");//16
                        retention.Append("   ");//17
                        retention.Append("S/D                 ");//18
                        retention.Append(bp.LicTradNum.PadRight(11, ' '));//19
                        retention.Append("                                  ");//20
                        if (bp.CardFName.Length > 30)//21
                        {
                            retention.Append(bp.CardFName.Substring(0, 28).ToString().PadRight(30, ' '));
                        }
                        else
                        {
                            retention.Append(bp.CardFName.ToString().PadRight(30, ' '));
                        }
                        retention.Append("S/D                           ");//22
                        retention.Append(" ");//23
                        retention.Append("01000");//24
                        retention.Append("   ");//25
                        retention.Append("S/D                 ");//26
                        if (retentionDoc.WTName.Length > 40)//27
                        {
                            retention.Append(retentionDoc.WTName.Substring(0, 38).ToString().PadRight(40, ' '));
                        }
                        else
                        {
                            retention.Append(retentionDoc.WTName.ToString().PadRight(40, ' '));
                        }

                        retention.Append(orderToProccess.DocNum.ToString().PadRight(40, ' '));//28
                        retention.Append(totalLine.ToString("F").Replace(",", "").Replace(".", "").PadLeft(15, '0')); //29
                        retention.Append(retentionDoc.WTaxSum.ToString("F").Replace(",", "").Replace(".", "").PadLeft(15, '0')); //30
                        retention.Append(totalLine.ToString("F").Replace(",", "").Replace(".", "").PadLeft(15, '0'));//31
                        retention.Append(retentionDoc.WTaxSum.ToString("F").Replace(",", "").Replace(".", "").PadLeft(15, '0'));//32
                        retention.Append("000000000");//33
                        retention.Append("000000000000000000000000000");//34
                        retention.Append(orderToProccess.DocDueDate);//35
                        retention.Append("000000000000000000000000");//36
                        retention.Append(DateTime.Now.ToString("yyyyMM"));//37
                        retention.Append("00000000000000000000000000");//38
                        retention.Append("                    ");//39
                        retention.Append("                    ");//40
                        retention.Append("                                   ");//41
                        break;
                    case "003":
                        //Retencion Ingresos Brutos
                        retention.Append("I");//1
                        retention.Append(retentionCode);//Logica de Retencion//2
                        retention.Append(retentionNum.ToString().PadLeft(2, '0'));//3
                        retention.Append("01");//4
                        retention.Append("                   ");//5
                        retention.Append("001");//6
                        retention.Append("001");//7
                        retention.Append(orderToProccess.DocDueDate);//Fecha documentom 8
                        retention.Append("         "); //9
                        retention.Append(retentionNum.ToString().PadLeft(17, '0'));//10
                        retention.Append(bussinessInfo.TaxIdNum);//11
                        if (bussinessInfo.TaxIdNum.Length > 17)//12
                        {
                            retention.Append(bussinessInfo.TaxIdNum.Substring(0, 15).ToString().PadRight(17, ' '));
                        }
                        else
                        {
                            retention.Append(bussinessInfo.TaxIdNum.ToString().PadRight(17, ' '));
                        }
                        retention.Append(bussinessInfo.TaxIdNum.ToString().PadRight(17, ' '));//13
                        if (bussinessInfo.CompnyName.Length > 30)
                        {
                            retention.Append(bussinessInfo.CompnyName.Substring(0, 28).ToString().PadRight(30, ' '));//14
                        }
                        else
                        {
                            retention.Append(bussinessInfo.CompnyName.ToString().PadRight(30, ' '));
                        }
                        retention.Append("S/D                           ");//15
                        retention.Append(" ");//16
                        retention.Append("00000");//17
                        retention.Append("   ");//18
                        retention.Append("S/D                 ");//19
                        retention.Append(bp.LicTradNum.PadRight(11, ' '));//CUIT 20
                        retention.Append(bp.LicTradNum.ToString().PadRight(17, ' '));//21
                        retention.Append(bp.LicTradNum.ToString().PadRight(17, ' '));//22
                        if (bp.CardFName.Length > 30)//23
                        {
                            retention.Append(bp.CardFName.Substring(0, 28).ToString().PadRight(30, ' '));
                        }
                        else
                        {
                            retention.Append(bp.CardFName.ToString().PadRight(30, ' '));
                        }
                        retention.Append("S/D                           ");//24
                        retention.Append(" ");//25
                        retention.Append("00000");//26
                        retention.Append("   ");//27
                        retention.Append("S/D                 ");//28

                        if (retentionDoc.WTName.Length > 40)//Descripcion Retencion - 29
                        {
                            retention.Append(retentionDoc.WTName.Substring(0, 38).ToString().PadRight(40, ' '));
                        }
                        else
                        {
                            retention.Append(retentionDoc.WTName.ToString().PadRight(40, ' '));
                        }

                        retention.Append(orderToProccess.DocNum.ToString().PadRight(40, ' '));//30
                        retention.Append(totalLine.ToString("F").Replace(",", "").Replace(".", "").PadLeft(15, '0')); //31
                        retention.Append(retentionDoc.WTaxSum.ToString("F").Replace(",", "").Replace(".", "").PadLeft(15, '0')); //32
                        retention.Append("000000000000000000000000000000");//33
                        retention.Append("000000000");//34
                        retention.Append("000000000000000000000000000");//35
                        retention.Append(orderToProccess.DocDueDate);//36
                        retention.Append(orderToProccess.DocDueDate);//37
                        retention.Append("0000000000000000000000000000000000000000");//38
                        retention.Append("01");//39
                        retention.Append("01");//40
                        retention.Append("0000");//41
                        retention.Append("S/D                 ");//42
                        retention.Append("S/D                 ");//43
                        retention.Append("S/D                                ");//44
                        break;
                }



                sw.WriteLine(retention.ToString());
            }
        }

        private void createTrailer(StreamWriter sw, double total, double totalLines)
        {
            StringBuilder trailer = new StringBuilder();
            trailer.Append("T");
            trailer.Append("000000000000000");
            trailer.Append(total.ToString("F").Replace(",", "").Replace(".", "").PadLeft(15, '0'));
            trailer.Append(totalLines.ToString().PadLeft(7, '0'));
            trailer.Append("".PadRight(612, ' '));

            sw.WriteLine(trailer.ToString());
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            bool ac50 = chb50.Checked;
            bool ac64 = chb64.Checked;

            listDocuments.Items.Clear();

            listOrderInSAP = new List<Order>();
            listOrderSelected = new List<Order>();
            var objOVPMRepository = new OVPMRepository(Program.SessionId, Program.IsHana);

            var dateTime = Convert.ToDateTime(dtDate.Text);

            listOrderInSAP = objOVPMRepository.getDocuments(dateTime, txtCardCode.Text, txtSearch.Text, ac50, ac64);

            foreach (var item in listOrderInSAP)
            {
                listDocuments.Items.Add(item.DocNum);
            }

        }

        private int GetItemIndex(string item)
        {
            int index = 0;

            foreach (object o in listDocuments.Items)
            {
                if (item == o.ToString())
                {
                    return index;
                }

                index++;
            }

            return -1;
        }

        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < listDocuments.Items.Count; i++)
                listDocuments.SetItemChecked(i, true);
        }

        private string GetCodeRetention(string retentionCode)
        {
            string fileRetentionCode = "";
            switch (retentionCode)
            {
                case "RP05":
                case "RP06":
                case "RP08":
                case "RP70":
                case "RP34":
                    fileRetentionCode = "002";
                    break;
                case "RP33":
                    fileRetentionCode = "001";
                    break;
                case "RP41":
                case "RP42":
                    fileRetentionCode = "003";
                    break;
            }

            return fileRetentionCode;
        }


    }
}
