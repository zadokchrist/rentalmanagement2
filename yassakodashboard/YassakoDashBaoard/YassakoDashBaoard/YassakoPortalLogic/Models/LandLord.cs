using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YassakoPortalLogic.Models
{
    public class LandLord : GenericResponse
    {
        public string id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string tel { get; set; }
        public string creationdate { get; set; }
        public bool active { get; set; }
    }
}
