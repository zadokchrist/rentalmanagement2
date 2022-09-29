using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YassakoPortalLogic.Models
{
    public class BalanceResponse:GenericResponse
    {
        public string Balance {get;set;}
        public string Commission { get; set; }
    }
}
