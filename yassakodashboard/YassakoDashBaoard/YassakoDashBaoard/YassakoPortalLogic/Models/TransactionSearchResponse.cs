using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YassakoPortalLogic.Models
{
    public class TransactionSearchResponse : GenericResponse
    {
        public List<Transaction> transactions { get; set; }
    }

    public class Transaction
    {
        public string Yassakoid { get; set; }
        public string VendorId { get; set; }
        public string CustomerName { get; set; }
        public string PaymentDate { get; set; }
        public string TranAmount { get; set; }
        public string CustPhone { get; set; }
        public string MeterNumber { get; set; }
        public string TranStatus { get; set; }
        public string Vendor { get; set; }
        public string TranType { get; set; }
        public string Utility { get; set; }
        public string UtilityToken { get; set; }
        public string Reason { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string RequestedBy { get; set; }
    }
}
