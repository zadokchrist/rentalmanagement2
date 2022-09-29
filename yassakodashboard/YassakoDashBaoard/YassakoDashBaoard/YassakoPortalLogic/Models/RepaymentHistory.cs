using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YassakoPortalLogic.Models
{
    public class RepaymentHistory
    {
        public string CustomerRef { get; set; }
        public string CustomerTel { get; set; }
        public string TotalLoanAmount { get; set; }
        public string AmountRepaid { get; set; }
        public string Balance { get; set; }
        public string LoanVendorId { get; set; }
        public string LoanYassakoRef { get; set; }
        public string RepaymentId { get; set; }
        public string TransactionDate { get; set; }
        public string RepaymentDate { get; set; }
    }
}
