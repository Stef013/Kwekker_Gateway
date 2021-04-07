using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Profile_Service.Models
{
    public class Profile
    {
        public int ID { get; set; }
        public int accountID { get; set; }
        public string profileName { get; set; }
        public string userTag { get; set; }
        public string bio { get; set; }
        public List<Profile> followers { get; set; }
        public List<Profile> following { get; set; }
    }
}
