using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YassakoPortalLogic.Models
{
    public class RepaymentSearch : GenericResponse
    {
        public List<RepaymentHistory> repayments { get; set; }
    }
}
