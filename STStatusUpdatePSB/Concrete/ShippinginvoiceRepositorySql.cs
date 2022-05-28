using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using STStatusUpdatePSB.Abstract;
using STStatusUpdatePSB.Entities;

namespace STStatusUpdatePSB.Concrete
{

    public class ShippingInvoiceRepositorySql : IShippingInvoiceRepository
    {

        private readonly ShippingInvoiceDbLayer _shippingInvoiceDbl = new ShippingInvoiceDbLayer();

        public List<PsbPibIssueDetails> GetAllPsbIssueActive(int shipId, int numofRecords)
        {
            DataSet dsPsbIssues = _shippingInvoiceDbl.GetAllPsbIssueActive(shipId, numofRecords);
            
            List<PsbPibIssueDetails> psbIssuesList = null;
            if (dsPsbIssues != null)
            {
                if (dsPsbIssues.Tables[0].Rows.Count > 0)
                {
                    psbIssuesList =
                        dsPsbIssues.Tables[0].AsEnumerable()
                            .Select(row => new PsbPibIssueDetails
                            {
                                ShippingInvoiceFk = Convert.IsDBNull(row.Field<object>("shippinginvoice_Id")) ? 0 : Convert.ToInt32(row.Field<object>("shippinginvoice_Id")),
                                OrderInvoiceId = Convert.IsDBNull(row.Field<object>("Shippinginvoice_Orderinvoice_Fk")) ? 0 : Convert.ToInt32(row.Field<object>("Shippinginvoice_Orderinvoice_Fk")),
                                PibCustomerNoteFk = Convert.IsDBNull(row.Field<object>("shippinginvoice_PIBCustomerNote_Fk")) ? 0 : Convert.ToInt32(row.Field<object>("shippinginvoice_PIBCustomerNote_Fk")),
                                PsbCustomerNoteFk = Convert.IsDBNull(row.Field<object>("shippinginvoice_PSBCustomerNote_Fk")) ? 0 : Convert.ToInt32(row.Field<object>("shippinginvoice_PSBCustomerNote_Fk")),

                            }).ToList();

                }
            }
            return psbIssuesList;
        }

        public bool CheckIfAllPsbIssuesResolved(int shippingInvoiceId)
        {
            return _shippingInvoiceDbl.CheckIfAllPsbIssuesResolved(shippingInvoiceId);
        }
        public int UpdateShippingInvoicePsbIssuesResolved(int shippingInvoiceFk, bool psbIssueActive, DateTime dateResolved)
        {
            int updateStatus = _shippingInvoiceDbl.UpdateShippingInvoicePsbIssuesResolved(shippingInvoiceFk, psbIssueActive, dateResolved);

            return updateStatus;
        }
        public int AddLogTrackPsbPibIssueLifeCycle(int shippingInvoiceFk, int userAdminfk, string issueCreatedby,
           bool? isPsbIssueActive, bool? isPibIssueActive, string psbIssueList, bool? isPsbResolved,
           bool? pibIssueActive, string pibIssueList, bool? isPibResolved, string comment, DateTime dateCreated)
        {
            int newLogId = _shippingInvoiceDbl.AddLogTrackPsbpibIssueLifeCycle(shippingInvoiceFk, userAdminfk, issueCreatedby,
            isPsbIssueActive, isPibIssueActive, psbIssueList, isPsbResolved,
            pibIssueActive, pibIssueList, isPibResolved, comment, dateCreated);
            return newLogId;

        }
        public List<PsbPibIssueDetails> GetAllPibIssueActive(int shipId, int numofRecords)
        {
            DataSet dsPsbIssues = _shippingInvoiceDbl.GetAllPibIssueActive(shipId, numofRecords);

            List<PsbPibIssueDetails> psbIssuesList = null;
            if (dsPsbIssues != null)
            {
                if (dsPsbIssues.Tables[0].Rows.Count > 0)
                {
                    psbIssuesList =
                        dsPsbIssues.Tables[0].AsEnumerable()
                            .Select(row => new PsbPibIssueDetails
                            {
                                ShippingInvoiceFk = Convert.IsDBNull(row.Field<object>("shippinginvoice_Id")) ? 0 : Convert.ToInt32(row.Field<object>("shippinginvoice_Id")),

                            }).ToList();

                }
            }
            return psbIssuesList;
        }
        public bool CheckIfAllPibIssuesResolved(int shippingInvoiceId)
        {
            return _shippingInvoiceDbl.CheckIfAllPibIssuesResolved(shippingInvoiceId);
        }
        public int UpdateShippingInvoicePibIssuesResolved(int shippingInvoiceFk, bool psbIssueActive, DateTime dateResolved)
        {
            int updateStatus = _shippingInvoiceDbl.UpdateShippingInvoicePibIssuesResolved(shippingInvoiceFk, psbIssueActive, dateResolved);

            return updateStatus;
        }
        public int AddLogStStatusUpdatePsb(string shippingInvoiceList, string actionName, int shippingInvoiceFk, string runFrom, string message, DateTime dateCreated)
        {
            int newLogId = _shippingInvoiceDbl.AddLogStStatusUpdatePsb(shippingInvoiceList, actionName, shippingInvoiceFk, runFrom, message, dateCreated);

            return newLogId;

        }
        public int UpdateCustomerNoteIssueResolved(int customerNoteFk, bool isAllResolved, DateTime dateResolved, bool IsPSBCustomerNote)
        {
            int updateStatus = _shippingInvoiceDbl.UpdateCustomerNoteIssueResolved( customerNoteFk, isAllResolved, dateResolved, IsPSBCustomerNote);
            return updateStatus;
        }
        public Issues GetPreScreenIssueList(int shippingInvoiceId)
        {
            DataSet dsIssueList = _shippingInvoiceDbl.GetPrescreenIssueList(shippingInvoiceId);

            Issues issueList = null;

            if (dsIssueList.Tables.Count > 0 && dsIssueList.Tables[0].Rows.Count > 0)
            {
                issueList =
                    dsIssueList.Tables[0].AsEnumerable()
                        .Select(row => new Issues
                        {
                            PrescreenIssueList = row.Field<string>("issueList"),
                        }).ToList().FirstOrDefault();

            }
            return issueList;
        }
        public Issues GetPibIssueList(int shippingInvoiceId)
        {
            DataSet dsIssueList = _shippingInvoiceDbl.GetPibIssueList(shippingInvoiceId);

            Issues issueList = null;

            if (dsIssueList.Tables.Count > 0 && dsIssueList.Tables[0].Rows.Count > 0)
            {
                issueList =
                    dsIssueList.Tables[0].AsEnumerable()
                        .Select(row => new Issues
                        {
                            PibIssueList = row.Field<string>("issueList"),
                        }).ToList().FirstOrDefault();

            }
            return issueList;
        }
        // increment attempt count for psb check
        public int UpdatePsbAttemptCount(int shippingInvoiceFk, DateTime dateUpdated)
        {
            var updateStatus = _shippingInvoiceDbl.UpdatePsbAttemptCount(shippingInvoiceFk, dateUpdated);
            return updateStatus;
        }

        // increment attempt count for pib check
        public int UpdatePibAttemptCount(int shippingInvoiceFk, DateTime dateUpdated)
        {
            var updateStatus = _shippingInvoiceDbl.UpdatePibAttemptCount(shippingInvoiceFk, dateUpdated);
            return updateStatus;
        }

        public int UpdateClearPreScreenFollowupNotes(int orderInvoiceId, int reasonFk, DateTime dateUpdated)
        {
            var updateStatus = _shippingInvoiceDbl.UpdateClearPreScreenFollowupNotes(orderInvoiceId, reasonFk, dateUpdated);
            return updateStatus;
        }
    }
}
