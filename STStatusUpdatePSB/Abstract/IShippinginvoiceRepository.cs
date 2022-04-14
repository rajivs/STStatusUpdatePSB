using STStatusUpdatePSB.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STStatusUpdatePSB.Abstract
{
    public interface IShippinginvoiceRepository
    {
        List<PSBPIBIssueDetails> GetAllPSBIssueActive();
        bool CheckIfAllPSBIssuesResolved(int shippinginvoiceId);
        int UpdateShippingInvoicePSBIssuesResolved(int shippingInvoiceFk, bool psbIssueActive, DateTime dateResolved);

        int AddLogTrackPSBPIBIssueLifeCycle(int shippingInvoiceFk, int userAdminfk, string issueCreatedby,
           bool? isPSBIssueActive, bool? isPIBIssueActive, string psbIssueList, bool? isPSBResolved,
           bool? pibIssueActive, string pibIssueList, bool? isPIBResolved, string comment, DateTime dateCreated);
      
        List<PSBPIBIssueDetails> GetAllPIBIssueActive();
        bool CheckIfAllPIBIssuesResolved(int shippinginvoiceId);
        int UpdateShippingInvoicePIBIssuesResolved(int shippingInvoiceFk, bool psbIssueActive, DateTime dateResolved);
        int AddLogSTStatusUpdatePSB(string shippinginvoiceList, string actionName, int shippingInvoiceFk, string runFrom, string message, DateTime dateCreated);
        int UpdateCustomerNoteIssueResolved(int customerNoteFk, bool isallResolved, DateTime dateResolved, bool IsPSBCustomerNote );
    }
}
