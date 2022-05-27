using STStatusUpdatePSB.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STStatusUpdatePSB.Abstract
{
    public interface IShippingInvoiceRepository
    {
        List<PsbPibIssueDetails> GetAllPsbIssueActive(int shipId , int numofRecords);
        bool CheckIfAllPsbIssuesResolved(int shippingInvoiceId);
        int UpdateShippingInvoicePsbIssuesResolved(int shippingInvoiceFk, bool psbIssueActive, DateTime dateResolved);

        int AddLogTrackPsbPibIssueLifeCycle(int shippingInvoiceFk, int userAdminFk, string issueCreatedBy,
           bool? isPsbIssueActive, bool? isPibIssueActive, string psbIssueList, bool? isPsbResolved,
           bool? pibIssueActive, string pibIssueList, bool? isPibResolved, string comment, DateTime dateCreated);
      
        List<PsbPibIssueDetails> GetAllPibIssueActive(int shipId, int numofRecords);
        bool CheckIfAllPibIssuesResolved(int shippingInvoiceId);
        int UpdateShippingInvoicePibIssuesResolved(int shippingInvoiceFk, bool psbIssueActive, DateTime dateResolved);
        int AddLogStStatusUpdatePsb(string shippingInvoiceList, string actionName, int shippingInvoiceFk, string runFrom, string message, DateTime dateCreated);
        int UpdateCustomerNoteIssueResolved(int customerNoteFk, bool isAllResolved, DateTime dateResolved, bool isPsbCustomerNote );
        Issues GetPreScreenIssueList(int shippingInvoiceId);
        Issues GetPibIssueList(int shippingInvoiceId);
        int UpdatePsbAttemptCount(int shippingInvoiceFk, DateTime dateUpdated);
        int UpdatePibAttemptCount(int shippingInvoiceFk, DateTime dateUpdated);
        int UpdateClearPreScreenFollowupNotes(int orderInvoiceId, int reasonFk);
    }
}
