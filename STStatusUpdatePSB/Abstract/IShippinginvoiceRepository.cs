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
        List<PSBPIBIssueDetails> GetAllPsbIssueActive();
        bool CheckIfAllPsbIssuesResolved(int shippingInvoiceId);
        int UpdateShippingInvoicePsbIssuesResolved(int shippingInvoiceFk, bool psbIssueActive, DateTime dateResolved);

        int AddLogTrackPsbpibIssueLifeCycle(int shippingInvoiceFk, int userAdminfk, string issueCreatedBy,
           bool? isPsbIssueActive, bool? isPibIssueActive, string psbIssueList, bool? isPsbResolved,
           bool? pibIssueActive, string pibIssueList, bool? isPibResolved, string comment, DateTime dateCreated);
      
        List<PSBPIBIssueDetails> GetAllPibIssueActive();
        bool CheckIfAllPibIssuesResolved(int shippingInvoiceId);
        int UpdateShippingInvoicePibIssuesResolved(int shippingInvoiceFk, bool psbIssueActive, DateTime dateResolved);
        int AddLogStStatusUpdatePsb(string shippinginvoiceList, string actionName, int shippingInvoiceFk, string runFrom, string message, DateTime dateCreated);
        int UpdateCustomerNoteIssueResolved(int customerNoteFk, bool isAllResolved, DateTime dateResolved, bool isPsbCustomerNote );
    }
}
