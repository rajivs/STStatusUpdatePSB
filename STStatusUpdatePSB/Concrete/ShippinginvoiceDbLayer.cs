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
    public class ShippingInvoiceDbLayer
    {
        readonly string _sCon = ConfigurationManager.ConnectionStrings["Connection"].ToString();
        DataSet _ds;

        public DataSet GetAllPsbIssueActive()
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
        public bool CheckIfAllPsbIssuesResolved(int shippingInvoiceId)
        {
            try
            {
                SqlParameter[] paramCollection = SqlHelperParameterCache.GetSpParameterSet(_sCon, "usp_ST_CheckIfAllPSBIssuesResolved]", true);

                paramCollection[1].Value = shippingInvoiceId;
                paramCollection[2].Value = null; // true/false

                SqlHelper.ExecuteNonQuery(_sCon, CommandType.StoredProcedure, "usp_ST_CheckIfAllPSBIssuesResolved]", paramCollection);

                var value = paramCollection[2].Value;
                return value != null && Convert.ToBoolean(value.ToString());

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int UpdateShippingInvoicePsbIssuesResolved(int shippingInvoiceFk, bool psbIssueActive, DateTime dateResolved)
        {
            try
            {
                SqlParameter[] paramCollection = SqlHelperParameterCache.GetSpParameterSet(_sCon, "usp_ST_UpdateShippingInvoicePSBIssuesResolved", true);

                paramCollection[1].Value = shippingInvoiceFk;
                paramCollection[2].Value = psbIssueActive;
                paramCollection[3].Value = dateResolved;
                paramCollection[4].Value = null; // UpdateStatus
                paramCollection[5].Value = null; // Message

                SqlHelper.ExecuteNonQuery(_sCon, CommandType.StoredProcedure, "usp_ST_UpdateShippingInvoicePSBIssuesResolved", paramCollection);

                var updateStatus = Convert.ToInt32(paramCollection[4].Value.ToString());
                var message = paramCollection[5].Value.ToString();

                return updateStatus;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int AddLogTrackPsbpibIssueLifeCycle(int shippingInvoiceFk, int userAdminfk, string issueCreatedby,
           bool? isPsbIssueActive, bool? isPibIssueActive, string psbIssueList, bool? isPsbResolved,
           bool? pibIssueActive, string pibIssueList, bool? isPibResolved, string comment, DateTime dateCreated)
        {
            try
            {
                SqlParameter[] paramCollection = SqlHelperParameterCache.GetSpParameterSet(_sCon, "usp_ST_LogTrackPSBPIBIssueLifeCycle", true);

                paramCollection[1].Value = null;// UpdateStatus
                paramCollection[2].Value = null;// Message
                paramCollection[3].Value = shippingInvoiceFk;
                paramCollection[4].Value = userAdminfk;
                paramCollection[5].Value = issueCreatedby;
                paramCollection[6].Value = isPsbIssueActive;
                paramCollection[7].Value = isPibIssueActive;
                paramCollection[8].Value = psbIssueList;
                paramCollection[9].Value = isPsbResolved;
                paramCollection[10].Value = pibIssueActive;
                paramCollection[11].Value = isPibResolved;
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
        public DataSet GetAllPibIssueActive()
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
        public bool CheckIfAllPibIssuesResolved(int shippinginvoiceId)
        {
            try
            {
                SqlParameter[] paramCollection = SqlHelperParameterCache.GetSpParameterSet(_sCon, "usp_ST_CheckIfAllPIBIssuesResolved", true);

                paramCollection[1].Value = shippinginvoiceId;
                paramCollection[2].Value = null; // true/false

                SqlHelper.ExecuteNonQuery(_sCon, CommandType.StoredProcedure, "usp_ST_CheckIfAllPIBIssuesResolved", paramCollection);

                var value = paramCollection[2].Value;
                return value != null && Convert.ToBoolean(value.ToString());

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int UpdateShippingInvoicePibIssuesResolved(int shippingInvoiceFk, bool psbIssueActive, DateTime dateResolved)
        {
            try
            {
                SqlParameter[] paramCollection = SqlHelperParameterCache.GetSpParameterSet(_sCon, "usp_ST_UpdateShippingInvoicePIBIssuesResolved", true);

                paramCollection[1].Value = shippingInvoiceFk;
                paramCollection[2].Value = psbIssueActive;
                paramCollection[3].Value = dateResolved;
                paramCollection[4].Value = null; // UpdateStatus
                paramCollection[5].Value = null; // Message

                SqlHelper.ExecuteNonQuery(_sCon, CommandType.StoredProcedure, "usp_ST_UpdateShippingInvoicePIBIssuesResolved", paramCollection);

                var updateStatus = Convert.ToInt32(paramCollection[4].Value.ToString());
                var message = paramCollection[5].Value.ToString();

                return updateStatus;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int AddLogStStatusUpdatePsb(string shippinginvoiceList, string actionName, int shippingInvoiceFk, string runFrom, string message, DateTime dateCreated)
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
        public int UpdateCustomerNoteIssueResolved(int customerNoteFk, bool isallResolved, DateTime dateResolved, bool isPsbCustomerNote)
        {
            try
            {
                SqlParameter[] paramCollection = SqlHelperParameterCache.GetSpParameterSet(_sCon, "usp_ST_UpdateCustomerNoteIssueResolved", true);

                paramCollection[1].Value = customerNoteFk;
                paramCollection[2].Value = isallResolved;
                paramCollection[3].Value = dateResolved;
                paramCollection[4].Value = isPsbCustomerNote;
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
