using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Company
    {
        public string ServerType { get; set; }
        public string Server { get; set; }
        public string CompanyDB { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string DbUserName { get; set; }
        public string DbPassword { get; set; }
        public string LicenseServer { get; set; }
        public int PortNumber { get; set; }
    }
}
