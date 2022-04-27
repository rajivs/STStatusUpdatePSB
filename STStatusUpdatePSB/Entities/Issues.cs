using System;
using System.Collections.Generic;
using System.Text;

namespace STStatusUpdatePSB.Entities
{
   public class Issues
    {
        public int ShippinginvoiceId { get; set; }
        public bool IsPrescreenIssueActive { get; set; }
        public string PrescreenIssueList { get; set; }
        public string PibIssueList { get; set; }
    }
}
