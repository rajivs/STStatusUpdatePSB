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

        private readonly ShippinginvoiceDbLayer _ShippinginvoiceDbl = new ShippinginvoiceDbLayer();

        public List<PSBPIBIssueDetails> GetAllPSBIssueActive()
        {
            DataSet dsPSBIssues = _ShippinginvoiceDbl.GetAllPSBIssueActive();
            
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
                                PIBCustomerNoteFk = Convert.IsDBNull(row.Field<object>("shippinginvoice_PIBCustomerNote_Fk")) ? 0 : Convert.ToInt32(row.Field<object>("shippinginvoice_PIBCustomerNote_Fk")),
                                PSBCustomerNoteFk = Convert.IsDBNull(row.Field<object>("shippinginvoice_PSBCustomerNote_Fk")) ? 0 : Convert.ToInt32(row.Field<object>("shippinginvoice_PSBCustomerNote_Fk")),

                            }).ToList();

                }
            }
            return psbIssuesList;
        }

        public bool CheckIfAllPSBIssuesResolved(int shippinginvoiceId)
        {
            return _ShippinginvoiceDbl.CheckIfAllPSBIssuesResolved(shippinginvoiceId);
        }
        public int UpdateShippingInvoicePSBIssuesResolved(int shippingInvoiceFk, bool psbIssueActive, DateTime dateResolved)
        {
            int updateStatus = _ShippinginvoiceDbl.UpdateShippingInvoicePSBIssuesResolved(shippingInvoiceFk, psbIssueActive, dateResolved);

            return updateStatus;
        }
        public int AddLogTrackPSBPIBIssueLifeCycle(int shippingInvoiceFk, int userAdminfk, string issueCreatedby,
           bool? isPSBIssueActive, bool? isPIBIssueActive, string psbIssueList, bool? isPSBResolved,
           bool? pibIssueActive, string pibIssueList, bool? isPIBResolved, string comment, DateTime dateCreated)
        {
            int newLogId = _ShippinginvoiceDbl.AddLogTrackPSBPIBIssueLifeCycle(shippingInvoiceFk, userAdminfk, issueCreatedby,
            isPSBIssueActive, isPIBIssueActive, psbIssueList, isPSBResolved,
            pibIssueActive, pibIssueList, isPIBResolved, comment, dateCreated);
            return newLogId;

        }
        public List<PSBPIBIssueDetails> GetAllPIBIssueActive()
        {
            DataSet dsPSBIssues = _ShippinginvoiceDbl.GetAllPIBIssueActive();

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
        public bool CheckIfAllPIBIssuesResolved(int shippinginvoiceId)
        {
            return _ShippinginvoiceDbl.CheckIfAllPIBIssuesResolved(shippinginvoiceId);
        }
        public int UpdateShippingInvoicePIBIssuesResolved(int shippingInvoiceFk, bool psbIssueActive, DateTime dateResolved)
        {
            int updateStatus = _ShippinginvoiceDbl.UpdateShippingInvoicePIBIssuesResolved(shippingInvoiceFk, psbIssueActive, dateResolved);

            return updateStatus;
        }
        public int AddLogSTStatusUpdatePSB(string shippinginvoiceList, string actionName, int shippingInvoiceFk, string runFrom, string message, DateTime dateCreated)
        {
            int newLogId = _ShippinginvoiceDbl.AddLogSTStatusUpdatePSB(shippinginvoiceList, actionName, shippingInvoiceFk, runFrom, message, dateCreated);

            return newLogId;

        }
        public int UpdateCustomerNoteIssueResolved(int customerNoteFk, bool isallResolved, DateTime dateResolved, bool IsPSBCustomerNote)
        {
            int updateStatus = _ShippinginvoiceDbl.UpdateCustomerNoteIssueResolved( customerNoteFk, isallResolved, dateResolved, IsPSBCustomerNote);
            return updateStatus;
        }
    }
}
