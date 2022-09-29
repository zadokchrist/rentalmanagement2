using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YassakoPortalLogic.Models
{
    public class UserSearchResult : GenericResponse
    {
        public List<SystemUser> results { get; set; }
    }
}
