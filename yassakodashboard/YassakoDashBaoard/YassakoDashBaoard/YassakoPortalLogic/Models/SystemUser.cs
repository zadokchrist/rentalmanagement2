using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YassakoPortalLogic.Models
{
    public class SystemUser
    {
        public string UserEmail { get; set; }
        public string Username { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string UserCompany { get; set; }
        public string Userrole { get; set; }
        public string RecodedBy { get; set; }
        public bool Active { get; set; }
        public string UserPassword { get; set; }
        public string Designation { get; set; }
        public string Section { get; set; }
        public string SectionId { get; set; }
    }
}
