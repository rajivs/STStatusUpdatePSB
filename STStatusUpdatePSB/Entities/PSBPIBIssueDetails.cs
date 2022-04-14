using System;
using System.Collections.Generic;
using System.Text;

namespace STStatusUpdatePSB.Entities
{
    public class PSBPIBIssueDetails
    {
        public int ShippingInvoiceFk{ get; set; }
        public int PsbCustomerNoteFk { get; set; }
        public int PibCustomerNoteFk { get; set; }
    }
}
