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

    public class ShippinginvoiceRepositorySql : IShippinginvoiceRepository
    {

        private readonly ShippingInvoiceDbLayer _ShippinginvoiceDbl = new ShippingInvoiceDbLayer();

        public List<PSBPIBIssueDetails> GetAllPsbIssueActive()
        {
            DataSet dsPSBIssues = _ShippinginvoiceDbl.GetAllPsbIssueActive();
            
            List<PSBPIBIssueDetails> psbIssuesList = null;
            if (dsPSBIssues != null)
            {
                if (dsPSBIssues.Tables[0].Rows.Count > 0)
                {
                    psbIssuesList =
                        dsPSBIssues.Tables[0].AsEnumerable()
                            .Select(row => new PSBPIBIssueDetails
                            {
                                ShippingInvoiceFk = Convert.IsDBNull(row.Field<object>("shippinginvoice_Id")) ? 0 : Convert.ToInt32(row.Field<object>("shippinginvoice_Id")),
                                PibCustomerNoteFk = Convert.IsDBNull(row.Field<object>("shippinginvoice_PIBCustomerNote_Fk")) ? 0 : Convert.ToInt32(row.Field<object>("shippinginvoice_PIBCustomerNote_Fk")),
                                PsbCustomerNoteFk = Convert.IsDBNull(row.Field<object>("shippinginvoice_PSBCustomerNote_Fk")) ? 0 : Convert.ToInt32(row.Field<object>("shippinginvoice_PSBCustomerNote_Fk")),

                            }).ToList();

                }
            }
            return psbIssuesList;
        }

        public bool CheckIfAllPsbIssuesResolved(int shippinginvoiceId)
        {
            return _ShippinginvoiceDbl.CheckIfAllPsbIssuesResolved(shippinginvoiceId);
        }
        public int UpdateShippingInvoicePsbIssuesResolved(int shippingInvoiceFk, bool psbIssueActive, DateTime dateResolved)
        {
            int updateStatus = _ShippinginvoiceDbl.UpdateShippingInvoicePsbIssuesResolved(shippingInvoiceFk, psbIssueActive, dateResolved);

            return updateStatus;
        }
        public int AddLogTrackPsbpibIssueLifeCycle(int shippingInvoiceFk, int userAdminfk, string issueCreatedby,
           bool? isPSBIssueActive, bool? isPIBIssueActive, string psbIssueList, bool? isPSBResolved,
           bool? pibIssueActive, string pibIssueList, bool? isPIBResolved, string comment, DateTime dateCreated)
        {
            int newLogId = _ShippinginvoiceDbl.AddLogTrackPsbpibIssueLifeCycle(shippingInvoiceFk, userAdminfk, issueCreatedby,
            isPSBIssueActive, isPIBIssueActive, psbIssueList, isPSBResolved,
            pibIssueActive, pibIssueList, isPIBResolved, comment, dateCreated);
            return newLogId;

        }
        public List<PSBPIBIssueDetails> GetAllPibIssueActive()
        {
            DataSet dsPSBIssues = _ShippinginvoiceDbl.GetAllPibIssueActive();

            List<PSBPIBIssueDetails> psbIssuesList = null;
            if (dsPSBIssues != null)
            {
                if (dsPSBIssues.Tables[0].Rows.Count > 0)
                {
                    psbIssuesList =
                        dsPSBIssues.Tables[0].AsEnumerable()
                            .Select(row => new PSBPIBIssueDetails
                            {
                                ShippingInvoiceFk = Convert.IsDBNull(row.Field<object>("shippinginvoice_Id")) ? 0 : Convert.ToInt32(row.Field<object>("shippinginvoice_Id")),

                            }).ToList();

                }
            }
            return psbIssuesList;
        }
        public bool CheckIfAllPibIssuesResolved(int shippinginvoiceId)
        {
            return _ShippinginvoiceDbl.CheckIfAllPibIssuesResolved(shippinginvoiceId);
        }
        public int UpdateShippingInvoicePibIssuesResolved(int shippingInvoiceFk, bool psbIssueActive, DateTime dateResolved)
        {
            int updateStatus = _ShippinginvoiceDbl.UpdateShippingInvoicePibIssuesResolved(shippingInvoiceFk, psbIssueActive, dateResolved);

            return updateStatus;
        }
        public int AddLogStStatusUpdatePsb(string shippinginvoiceList, string actionName, int shippingInvoiceFk, string runFrom, string message, DateTime dateCreated)
        {
            int newLogId = _ShippinginvoiceDbl.AddLogStStatusUpdatePsb(shippinginvoiceList, actionName, shippingInvoiceFk, runFrom, message, dateCreated);

            return newLogId;

        }
        public int UpdateCustomerNoteIssueResolved(int customerNoteFk, bool isallResolved, DateTime dateResolved, bool IsPSBCustomerNote)
        {
            int updateStatus = _ShippinginvoiceDbl.UpdateCustomerNoteIssueResolved( customerNoteFk, isallResolved, dateResolved, IsPSBCustomerNote);
            return updateStatus;
        }
    }
}
