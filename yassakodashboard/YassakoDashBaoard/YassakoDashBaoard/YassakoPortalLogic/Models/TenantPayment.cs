using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YassakoPortalLogic.Models
{
    public class TenantPayment
    {
        public string  PropertyRef { get; set; }
        public string Amount { get; set; }
        public string DatePaid { get; set; }
        public string PaymentMode { get; set; }
        public string RecordedBy { get; set; }
        public string ReceiptNumber { get; set; }
        public string RecordDate { get; set; }
        public string KhpComm { get; set; }
        public string LandLordCommission { get; set; }
        public string LandLordid { get; set; }
    }
}
