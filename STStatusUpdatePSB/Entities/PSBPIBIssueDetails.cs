using System;
using System.Collections.Generic;
using System.Text;

namespace STStatusUpdatePSB.Entities
{
    public class PSBPIBIssueDetails
    {
        public int ShippingInvoiceFk{ get; set; }
        public int PSBCustomerNoteFk { get; set; }
        public int PIBCustomerNoteFk { get; set; }
    }
}
