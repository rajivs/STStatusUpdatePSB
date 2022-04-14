using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace STStatusUpdatePSB.Concrete
{
    public class ShippinginvoiceDbLayer
    {
        readonly string _sCon = ConfigurationManager.ConnectionStrings["Connection"].ToString();
        DataSet _ds;

        public DataSet GetAllPSBIssueActive()
        {
            try
            {
                _ds = SqlHelper.ExecuteDataset(_sCon, "usp_ST_GetAllPSBIssueActiveShippingInvoice");
            }
            catch (Exception ex)
            {
                _ds = null;

            }
            return _ds;
        }
        public bool CheckIfAllPSBIssuesResolved(int shippinginvoiceId)
        {
            try
            {
                SqlParameter[] paramCollection = SqlHelperParameterCache.GetSpParameterSet(_sCon, "usp_be_CheckIfAllPSBIssuesResolved", true);

                paramCollection[1].Value = shippinginvoiceId;
                paramCollection[2].Value = null; // true/false

                SqlHelper.ExecuteNonQuery(_sCon, CommandType.StoredProcedure, "usp_be_CheckIfAllPSBIssuesResolved", paramCollection);

                var value = paramCollection[2].Value;
                return value != null && Convert.ToBoolean(value.ToString());

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int UpdateShippingInvoicePSBIssuesResolved(int shippingInvoiceFk, bool psbIssueActive, DateTime dateResolved)
        {
            try
            {
                SqlParameter[] paramCollection = SqlHelperParameterCache.GetSpParameterSet(_sCon, "usp_be_UpdateShippingInvoicePSBIssuesResolved", true);

                paramCollection[1].Value = shippingInvoiceFk;
                paramCollection[2].Value = psbIssueActive;
                paramCollection[3].Value = dateResolved;
                paramCollection[4].Value = null; // UpdateStatus
                paramCollection[5].Value = null; // Message

                SqlHelper.ExecuteNonQuery(_sCon, CommandType.StoredProcedure, "usp_be_UpdateShippingInvoicePSBIssuesResolved", paramCollection);

                var updateStatus = Convert.ToInt32(paramCollection[4].Value.ToString());
                var message = paramCollection[5].Value.ToString();

                return updateStatus;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int AddLogTrackPSBPIBIssueLifeCycle(int shippingInvoiceFk, int userAdminfk, string issueCreatedby,
           bool? isPSBIssueActive, bool? isPIBIssueActive, string psbIssueList, bool? isPSBResolved,
           bool? pibIssueActive, string pibIssueList, bool? isPIBResolved, string comment, DateTime dateCreated)
        {
            try
            {
                SqlParameter[] paramCollection = SqlHelperParameterCache.GetSpParameterSet(_sCon, "usp_ST_LogTrackPSBPIBIssueLifeCycle", true);

                paramCollection[1].Value = null;// UpdateStatus
                paramCollection[2].Value = null;// Message
                paramCollection[3].Value = shippingInvoiceFk;
                paramCollection[4].Value = userAdminfk;
                paramCollection[5].Value = issueCreatedby;
                paramCollection[6].Value = isPSBIssueActive;
                paramCollection[7].Value = isPIBIssueActive;
                paramCollection[8].Value = psbIssueList;
                paramCollection[9].Value = isPSBResolved;
                paramCollection[10].Value = pibIssueActive;
                paramCollection[11].Value = isPIBResolved;
                paramCollection[12].Value = pibIssueList;
                paramCollection[13].Value = comment;
                paramCollection[14].Value = dateCreated;

                SqlHelper.ExecuteNonQuery(_sCon, CommandType.StoredProcedure, "usp_ST_LogTrackPSBPIBIssueLifeCycle", paramCollection);

                var msg = paramCollection[2].Value;
                var value = paramCollection[1].Value;

                if (value != null) return Convert.ToInt32(value.ToString());

                return 0;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet GetAllPIBIssueActive()
        {
            try
            {
                _ds = SqlHelper.ExecuteDataset(_sCon, "usp_ST_GetAllPIBIssueActiveShippingInvoice");
            }
            catch (Exception ex)
            {
                _ds = null;

            }
            return _ds;
        }
        public bool CheckIfAllPIBIssuesResolved(int shippinginvoiceId)
        {
            try
            {
                SqlParameter[] paramCollection = SqlHelperParameterCache.GetSpParameterSet(_sCon, "usp_be_CheckIfAllPIBIssuesResolved", true);

                paramCollection[1].Value = shippinginvoiceId;
                paramCollection[2].Value = null; // true/false

                SqlHelper.ExecuteNonQuery(_sCon, CommandType.StoredProcedure, "usp_be_CheckIfAllPIBIssuesResolved", paramCollection);

                var value = paramCollection[2].Value;
                return value != null && Convert.ToBoolean(value.ToString());

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int UpdateShippingInvoicePIBIssuesResolved(int shippingInvoiceFk, bool psbIssueActive, DateTime dateResolved)
        {
            try
            {
                SqlParameter[] paramCollection = SqlHelperParameterCache.GetSpParameterSet(_sCon, "usp_be_UpdateShippingInvoicePIBIssuesResolved", true);

                paramCollection[1].Value = shippingInvoiceFk;
                paramCollection[2].Value = psbIssueActive;
                paramCollection[3].Value = dateResolved;
                paramCollection[4].Value = null; // UpdateStatus
                paramCollection[5].Value = null; // Message

                SqlHelper.ExecuteNonQuery(_sCon, CommandType.StoredProcedure, "usp_be_UpdateShippingInvoicePIBIssuesResolved", paramCollection);

                var updateStatus = Convert.ToInt32(paramCollection[4].Value.ToString());
                var message = paramCollection[5].Value.ToString();

                return updateStatus;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int AddLogSTStatusUpdatePSB(string shippinginvoiceList, string actionName, int shippingInvoiceFk, string runFrom, string message, DateTime dateCreated)
        {
            try
            {
                SqlParameter[] paramCollection = SqlHelperParameterCache.GetSpParameterSet(_sCon, "usp_ST_LogSTStatusUpdatePSB", true);

                paramCollection[1].Value = shippinginvoiceList;
                paramCollection[2].Value = actionName;
                paramCollection[3].Value = shippingInvoiceFk;
                paramCollection[4].Value = runFrom;
                paramCollection[5].Value = message;
                paramCollection[6].Value = dateCreated;
                paramCollection[7].Value = null; // new logId
                paramCollection[8].Value = null;  // message

                SqlHelper.ExecuteNonQuery(_sCon, CommandType.StoredProcedure, "usp_ST_LogSTStatusUpdatePSB", paramCollection);

                var msg = paramCollection[8].Value;
                var value = paramCollection[7].Value;

                if (value != null) return Convert.ToInt32(value.ToString());

                return 0;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public int UpdateCustomerNoteIssueResolved(int customerNoteFk, bool isallResolved, DateTime dateResolved, bool IsPSBCustomerNote)
        {
            try
            {
                SqlParameter[] paramCollection = SqlHelperParameterCache.GetSpParameterSet(_sCon, "usp_ST_UpdateCustomerNoteIssueResolved", true);

                paramCollection[1].Value = customerNoteFk;
                paramCollection[2].Value = isallResolved;
                paramCollection[3].Value = dateResolved;
                paramCollection[4].Value = IsPSBCustomerNote;
                paramCollection[5].Value = null;  // UpdateStatus
                paramCollection[6].Value = null; // Message

                SqlHelper.ExecuteNonQuery(_sCon, CommandType.StoredProcedure, "usp_ST_UpdateCustomerNoteIssueResolved", paramCollection);

                var updateStatus = Convert.ToInt32(paramCollection[5].Value.ToString());
                var message = paramCollection[5].Value.ToString();

                return updateStatus;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
