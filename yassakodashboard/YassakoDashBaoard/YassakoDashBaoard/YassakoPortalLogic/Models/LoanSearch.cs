using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YassakoPortalLogic.Models
{
    public class LoanSearch : GenericResponse
    {
        public List<Loan> loans { get; set; }
    }
}
