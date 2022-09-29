using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YassakoPortalLogic.Models
{
    public class Loan
    {
        public string CustomerRef { get; set; }
        public string CustomerTel { get; set; }
        public string Amount { get; set; }
        public string LoanId { get; set; }
        public string LoanDate { get; set; }
        public string RepaymentDate { get; set; }
        public string DateRepaid { get; set; }
        public string RecordDate { get; set; }
        public string Repaymentid { get; set; }
        public bool Repaid { get; set; }
        public bool PenaltyApplied { get; set; }
    }
}
