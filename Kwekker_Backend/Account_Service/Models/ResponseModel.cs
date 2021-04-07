using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Account_Service.Models
{
    public class ResponseModel
    {
        public bool success { get; set; }
        public string message { get; set; }
        public int accountID { get; set; }
    }
}
