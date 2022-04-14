using System;
using STStatusUpdatePSB.Abstract;
using STStatusUpdatePSB.Concrete;
using STStatusUpdatePSB.Entities;
using System.Text;
using System.Collections.Generic;
using System.Linq;
namespace STStatusUpdatePSB
{
    class Program
    {
        static void Main(string[] args)
        {
            PSBStatusUpdate();
            PIBStatusUpdate();
        }

        public static void PSBStatusUpdate()
        {

            IShippinginvoiceRepository shippingRepository = new ShippinginvoiceRepositorySql();
            // get shipping invoice with PSBIssueActive = 1
            List<PSBPIBIssueDetails> shippinginvoiceList = shippingRepository.GetAllPSBIssueActive();

            if (shippinginvoiceList != null)
            {
                string shipList = string.Join(",", shippinginvoiceList.Select(x => x.ShippingInvoiceFk));

                int logId = shippingRepository.AddLogSTStatusUpdatePSB(shipList.ToString(),
                             "PSBStatusUpdate",
                              0, // shippingInvoice id
                             "STStatusUpdatePSB",
                             "Task Started",
                             DateTime.Now);

                foreach (PSBPIBIssueDetails shipDetails in shippinginvoiceList)
                {
                   logId = shippingRepository.AddLogSTStatusUpdatePSB(
                                  shipList.ToString(),
                                  "Task In Process For : " + shipDetails.ShippingInvoiceFk,
                                  shipDetails.ShippingInvoiceFk,
                                  "STStatusUpdatePSB",
                                  "PSBStatusUpdate  in progress",
                                  DateTime.Now);

                    // check if all PSB issues resolved
                    bool isResolved = shippingRepository.CheckIfAllPSBIssuesResolved(shipDetails.ShippingInvoiceFk);
                    if (isResolved)
                    {
                        // if all fixed update shippinginvoice
                        shippingRepository.UpdateShippingInvoicePSBIssuesResolved(shipDetails.ShippingInvoiceFk, false, DateTime.Now);

                        //log
                        shippingRepository.AddLogTrackPSBPIBIssueLifeCycle(shipDetails.ShippingInvoiceFk, 0
                                   , "STStatusUpdatePSB", false, false, null, true, false, null, false,
                                   "PSB issues mark resolved from ST", DateTime.Now);


                        logId = shippingRepository.AddLogSTStatusUpdatePSB(
                                                         shipList.ToString(),
                                                         "Task In Process For : " + shipDetails.ShippingInvoiceFk,
                                                         shipDetails.ShippingInvoiceFk,
                                                         "STStatusUpdatePSB",
                                                         "PSB issues resolved",
                                                         DateTime.Now);

                        //Updating customer note will remove entry from PTM
                        //CustomerNote_PSBAllResolved = 1 
                        if (shipDetails.PSBCustomerNoteFk > 0) {
                            shippingRepository.UpdateCustomerNoteIssueResolved(shipDetails.PSBCustomerNoteFk, true, DateTime.Now, true);

                        }
                    }
                    //if all not resolved add log
                    else
                    {
                        shippingRepository.AddLogTrackPSBPIBIssueLifeCycle(shipDetails.ShippingInvoiceFk, 0
                                                      , "STStatusUpdatePSB", true, null, null, false, null, null, null,
                                                      "PSB issues are not all resolved", DateTime.Now);

                        logId = shippingRepository.AddLogSTStatusUpdatePSB(
                                                        shipList.ToString(),
                                                        "Task In Process For : " + shipDetails.ShippingInvoiceFk,
                                                        shipDetails.ShippingInvoiceFk,
                                                        "STStatusUpdatePSB",
                                                        "PSB issues are not all resolved",
                                                        DateTime.Now);

                    }
                    int logIdComplete = shippingRepository.AddLogSTStatusUpdatePSB(
                                     shipList.ToString(),
                                    "PSBStatusUpdate",
                                     shipDetails.ShippingInvoiceFk,
                                    "PSBStatusUpdate",
                                    "PSBStatusUpdate Completed for" + shipDetails.ShippingInvoiceFk,
                                    DateTime.Now);
                }
              
            }
            else
            {
                int logId = shippingRepository.AddLogSTStatusUpdatePSB("",
                             "PSBStatusUpdate Skipped",
                             0,
                             "PSBStatusUpdate",
                             "No Records found",
                             DateTime.Now);
            }
        }

        public static void PIBStatusUpdate()
        {

            IShippinginvoiceRepository shippingRepository = new ShippinginvoiceRepositorySql();
            // get shipping invoice with PSBIssueActive = 1
            List<PSBPIBIssueDetails> shippinginvoicePIBList = shippingRepository.GetAllPIBIssueActive();

            if (shippinginvoicePIBList != null)
            {
                string shipList = string.Join(",", shippinginvoicePIBList.Select(x => x.ShippingInvoiceFk));
                int logId = shippingRepository.AddLogSTStatusUpdatePSB(shipList.ToString(),
                          "PIBStatusUpdate function Start",
                           0, // shippingInvoice id
                          "STStatusUpdatePSB",
                          "PIBStatusUpdate Started",
                          DateTime.Now);

                foreach (PSBPIBIssueDetails shipDetails in shippinginvoicePIBList)
                {
                    // check if all PIB issues resolved
                    bool isResolved = shippingRepository.CheckIfAllPIBIssuesResolved(shipDetails.ShippingInvoiceFk);
                    if (isResolved)
                    {
                        // if all issues fixed update shippinginvoice
                        shippingRepository.UpdateShippingInvoicePIBIssuesResolved(shipDetails.ShippingInvoiceFk, false, DateTime.Now);

                        //log
                        shippingRepository.AddLogTrackPSBPIBIssueLifeCycle(shipDetails.ShippingInvoiceFk, 0
                                   , "STStatusUpdatePSB", null, null, null, null, true, null, true,
                                   "PIB issues mark resolved from ST", DateTime.Now);

                        shippingRepository.AddLogSTStatusUpdatePSB(shipList.ToString(),
                         "PIBStatusUpdate In Process For : "+ shipDetails.ShippingInvoiceFk,
                          shipDetails.ShippingInvoiceFk, 
                         "STStatusUpdatePSB",
                         "PIB issues resolved",
                         DateTime.Now);

                        //Updating customer note will remove entry from PTM
                        //CustomerNote_PIBAllResolved = 1 
                        if (shipDetails.PSBCustomerNoteFk > 0)
                        {
                            shippingRepository.UpdateCustomerNoteIssueResolved(shipDetails.PIBCustomerNoteFk, true, DateTime.Now, false);

                        }
                    }
                    //if all not resolved add log
                    else
                    {
                        shippingRepository.AddLogTrackPSBPIBIssueLifeCycle(shipDetails.ShippingInvoiceFk, 0
                                                      , "STStatusUpdatePSB", null, null, null, null, true, null, false,
                                                      "PIB issues are not all resolved", DateTime.Now);

                        logId = shippingRepository.AddLogSTStatusUpdatePSB(
                                                      shipList.ToString(),
                                                      "PIBStatusUpdate In Process For : " + shipDetails.ShippingInvoiceFk,
                                                      shipDetails.ShippingInvoiceFk,
                                                      "STStatusUpdatePSB",
                                                      "PIB issues are not all resolved",
                                                      DateTime.Now);

                    }
                    int logIdComplete = shippingRepository.AddLogSTStatusUpdatePSB(
                                   shipList.ToString(),
                                  "PIBStatusUpdate",
                                   shipDetails.ShippingInvoiceFk,
                                  "PSBStatusUpdate",
                                  "PIBStatusUpdate Completed for"+ shipDetails.ShippingInvoiceFk,
                                  DateTime.Now);
                }
               
            }
            else
            {
                int logId = shippingRepository.AddLogSTStatusUpdatePSB("",
                             "PIBStatusUpdate Skipped",
                             0,
                             "PIBStatusUpdate",
                             "No Records found",
                             DateTime.Now);
            }
        }
    }
}
