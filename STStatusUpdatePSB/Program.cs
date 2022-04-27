using System;
using STStatusUpdatePSB.Abstract;
using STStatusUpdatePSB.Concrete;
using STStatusUpdatePSB.Entities;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using STStatusUpdatePSB.Helpers;

namespace STStatusUpdatePSB
{
    class Program
    {
        static void Main(string[] args)
        {
          //  PsbStatusUpdate();
            PibStatusUpdate();
        }

        public static void PsbStatusUpdate()
        {

            IShippingInvoiceRepository shippingRepository = new ShippingInvoiceRepositorySql();
            // get shipping invoice with PSBIssueActive = 1
            List<PsbPibIssueDetails> shippingInvoiceList = shippingRepository.GetAllPsbIssueActive(6738812, SiteConfigurationWc.ProcessCount);

            if (shippingInvoiceList != null)
            {
                string shipList = string.Join(",", shippingInvoiceList.Select(x => x.ShippingInvoiceFk));

                int logId = shippingRepository.AddLogStStatusUpdatePsb(shipList.ToString(),
                             "PSBStatusUpdate",
                              0, // shippingInvoice id
                             "STStatusUpdatePSB",
                             "Task Started",
                             DateTime.Now);

                foreach (PsbPibIssueDetails shipDetails in shippingInvoiceList)
                {
                   logId = shippingRepository.AddLogStStatusUpdatePsb(
                                  shipList.ToString(),
                                  "Task In Process For : " + shipDetails.ShippingInvoiceFk,
                                  shipDetails.ShippingInvoiceFk,
                                  "STStatusUpdatePSB",
                                  "PSBStatusUpdate checking",
                                  DateTime.Now);

                    // check if all PSB issues resolved
                    bool isResolved = shippingRepository.CheckIfAllPsbIssuesResolved(shipDetails.ShippingInvoiceFk);
                    if (isResolved)
                    {
                        // if all fixed update shipping invoice
                        shippingRepository.UpdateShippingInvoicePsbIssuesResolved(shipDetails.ShippingInvoiceFk, false, DateTime.Now);

                        //log
                        shippingRepository.AddLogTrackPsbPibIssueLifeCycle(shipDetails.ShippingInvoiceFk, 0
                                   , "STStatusUpdatePSB", false, false, null, true, false, null, false,
                                   "PSB issues mark resolved from ST", DateTime.Now);


                        logId = shippingRepository.AddLogStStatusUpdatePsb(
                                                         shipList.ToString(),
                                                         "Task In Process For : " + shipDetails.ShippingInvoiceFk,
                                                         shipDetails.ShippingInvoiceFk,
                                                         "STStatusUpdatePSB",
                                                         "PSB issues resolved",
                                                         DateTime.Now);

                        //Updating customer note will remove entry from PTM
                        //CustomerNote_PSBAllResolved = 1 
                        if (shipDetails.PsbCustomerNoteFk > 0) {
                            shippingRepository.UpdateCustomerNoteIssueResolved(shipDetails.PsbCustomerNoteFk, true, DateTime.Now, true);

                        }
                    }
                    //if all not resolved add log
                    else
                    {
                        Issues issueList = shippingRepository.GetPrescreenIssueList(shipDetails.ShippingInvoiceFk);

                        shippingRepository.AddLogTrackPsbPibIssueLifeCycle(shipDetails.ShippingInvoiceFk, 0
                                                      , "STStatusUpdatePSB", true, null, issueList.PrescreenIssueList, false, null, null, null,
                                                      "PSB issues are not all resolved", DateTime.Now);

                        logId = shippingRepository.AddLogStStatusUpdatePsb(
                                                        shipList.ToString(),
                                                        "Task In Process For : " + shipDetails.ShippingInvoiceFk,
                                                        shipDetails.ShippingInvoiceFk,
                                                        "STStatusUpdatePSB",
                                                        "PSB issues are not all resolved",
                                                        DateTime.Now);

                    }
                    int logIdComplete = shippingRepository.AddLogStStatusUpdatePsb(
                                     shipList.ToString(),
                                    "PSBStatusUpdate",
                                     shipDetails.ShippingInvoiceFk,
                                    "STStatusUpdatePSB",
                                    "PSBStatusUpdate Completed for " + shipDetails.ShippingInvoiceFk,
                                    DateTime.Now);
                }
              
            }
            else
            {
                int logId = shippingRepository.AddLogStStatusUpdatePsb("",
                             "PSBStatusUpdate Skipped",
                             0,
                             "STStatusUpdatePSB",
                             "No Records found",
                             DateTime.Now);
            }
        }

        public static void PibStatusUpdate()
        {

            IShippingInvoiceRepository shippingRepository = new ShippingInvoiceRepositorySql();
            // get shipping invoice with PSBIssueActive = 1
            List<PsbPibIssueDetails> shippinginvoicePIBList = shippingRepository.GetAllPibIssueActive(6737652, SiteConfigurationWc.ProcessCount);

            if (shippinginvoicePIBList != null)
            {
                string shipList = string.Join(",", shippinginvoicePIBList.Select(x => x.ShippingInvoiceFk));
                int logId = shippingRepository.AddLogStStatusUpdatePsb(shipList.ToString(),
                          "PIBStatusUpdate function Start",
                           0, // shippingInvoice id
                          "STStatusUpdatePSB",
                          "PIBStatusUpdate Started",
                          DateTime.Now);

                foreach (PsbPibIssueDetails shipDetails in shippinginvoicePIBList)
                {
                    // check if all PIB issues resolved
                    bool isResolved = shippingRepository.CheckIfAllPibIssuesResolved(shipDetails.ShippingInvoiceFk);
                    if (isResolved)
                    {
                        // if all issues fixed update shipping invoice
                        shippingRepository.UpdateShippingInvoicePibIssuesResolved(shipDetails.ShippingInvoiceFk, false, DateTime.Now);

                        //log
                        shippingRepository.AddLogTrackPsbPibIssueLifeCycle(shipDetails.ShippingInvoiceFk, 0
                                   , "STStatusUpdatePSB", null, null, null, null, true, null, true,
                                   "PIB issues mark resolved from ST", DateTime.Now);

                        shippingRepository.AddLogStStatusUpdatePsb(shipList.ToString(),
                         "PIBStatusUpdate In Process For : "+ shipDetails.ShippingInvoiceFk,
                          shipDetails.ShippingInvoiceFk, 
                         "STStatusUpdatePSB",
                         "PIB issues resolved",
                         DateTime.Now);

                        //Updating customer note will remove entry from PTM
                        //CustomerNote_PIBAllResolved = 1 
                        if (shipDetails.PibCustomerNoteFk > 0)
                        {
                            shippingRepository.UpdateCustomerNoteIssueResolved(shipDetails.PibCustomerNoteFk, true, DateTime.Now, false);

                        }
                    }
                    //if all not resolved add log
                    else
                    {
                        Issues issueList = shippingRepository.GetPibIssueList(shipDetails.ShippingInvoiceFk);

                        shippingRepository.AddLogTrackPsbPibIssueLifeCycle(shipDetails.ShippingInvoiceFk, 0
                                                      , "STStatusUpdatePSB", null, null,null, null, true , issueList.PibIssueList, false,
                                                      "PIB issues are not all resolved", DateTime.Now);

                        logId = shippingRepository.AddLogStStatusUpdatePsb(
                                                      shipList.ToString(),
                                                      "PIBStatusUpdate In Process For : " + shipDetails.ShippingInvoiceFk,
                                                      shipDetails.ShippingInvoiceFk,
                                                      "STStatusUpdatePSB",
                                                      "PIB issues are not all resolved",
                                                      DateTime.Now);

                    }
                    int logIdComplete = shippingRepository.AddLogStStatusUpdatePsb(
                                   shipList.ToString(),
                                  "PIBStatusUpdate",
                                   shipDetails.ShippingInvoiceFk,
                                  "STStatusUpdatePSB",
                                  "PIBStatusUpdate Completed for "+ shipDetails.ShippingInvoiceFk,
                                  DateTime.Now);
                }
               
            }
            else
            {
                int logId = shippingRepository.AddLogStStatusUpdatePsb("",
                             "PIBStatusUpdate Skipped",
                             0,
                             "STStatusUpdatePSB",
                             "No Records found",
                             DateTime.Now);
            }
        }
    }
}
