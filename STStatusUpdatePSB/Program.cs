﻿using System;
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
            PsbStatusUpdate();
            PibStatusUpdate();
        }

        public static void PsbStatusUpdate()
        {

            IShippinginvoiceRepository shippingRepository = new ShippinginvoiceRepositorySql();
            // get shipping invoice with PSBIssueActive = 1
            List<PSBPIBIssueDetails> shippingInvoiceList = shippingRepository.GetAllPsbIssueActive();

            if (shippingInvoiceList != null)
            {
                string shipList = string.Join(",", shippingInvoiceList.Select(x => x.ShippingInvoiceFk));

                int logId = shippingRepository.AddLogStStatusUpdatePsb(shipList.ToString(),
                             "PSBStatusUpdate",
                              0, // shippingInvoice id
                             "STStatusUpdatePSB",
                             "Task Started",
                             DateTime.Now);

                foreach (PSBPIBIssueDetails shipDetails in shippingInvoiceList)
                {
                   logId = shippingRepository.AddLogStStatusUpdatePsb(
                                  shipList.ToString(),
                                  "Task In Process For : " + shipDetails.ShippingInvoiceFk,
                                  shipDetails.ShippingInvoiceFk,
                                  "STStatusUpdatePSB",
                                  "PSBStatusUpdate  in progress",
                                  DateTime.Now);

                    // check if all PSB issues resolved
                    bool isResolved = shippingRepository.CheckIfAllPsbIssuesResolved(shipDetails.ShippingInvoiceFk);
                    if (isResolved)
                    {
                        // if all fixed update shipping invoice
                        shippingRepository.UpdateShippingInvoicePsbIssuesResolved(shipDetails.ShippingInvoiceFk, false, DateTime.Now);

                        //log
                        shippingRepository.AddLogTrackPsbpibIssueLifeCycle(shipDetails.ShippingInvoiceFk, 0
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
                        shippingRepository.AddLogTrackPsbpibIssueLifeCycle(shipDetails.ShippingInvoiceFk, 0
                                                      , "STStatusUpdatePSB", true, null, null, false, null, null, null,
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
                                    "PSBStatusUpdate",
                                    "PSBStatusUpdate Completed for" + shipDetails.ShippingInvoiceFk,
                                    DateTime.Now);
                }
              
            }
            else
            {
                int logId = shippingRepository.AddLogStStatusUpdatePsb("",
                             "PSBStatusUpdate Skipped",
                             0,
                             "PSBStatusUpdate",
                             "No Records found",
                             DateTime.Now);
            }
        }

        public static void PibStatusUpdate()
        {

            IShippinginvoiceRepository shippingRepository = new ShippinginvoiceRepositorySql();
            // get shipping invoice with PSBIssueActive = 1
            List<PSBPIBIssueDetails> shippinginvoicePIBList = shippingRepository.GetAllPibIssueActive();

            if (shippinginvoicePIBList != null)
            {
                string shipList = string.Join(",", shippinginvoicePIBList.Select(x => x.ShippingInvoiceFk));
                int logId = shippingRepository.AddLogStStatusUpdatePsb(shipList.ToString(),
                          "PIBStatusUpdate function Start",
                           0, // shippingInvoice id
                          "STStatusUpdatePSB",
                          "PIBStatusUpdate Started",
                          DateTime.Now);

                foreach (PSBPIBIssueDetails shipDetails in shippinginvoicePIBList)
                {
                    // check if all PIB issues resolved
                    bool isResolved = shippingRepository.CheckIfAllPibIssuesResolved(shipDetails.ShippingInvoiceFk);
                    if (isResolved)
                    {
                        // if all issues fixed update shipping invoice
                        shippingRepository.UpdateShippingInvoicePibIssuesResolved(shipDetails.ShippingInvoiceFk, false, DateTime.Now);

                        //log
                        shippingRepository.AddLogTrackPsbpibIssueLifeCycle(shipDetails.ShippingInvoiceFk, 0
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
                        if (shipDetails.PsbCustomerNoteFk > 0)
                        {
                            shippingRepository.UpdateCustomerNoteIssueResolved(shipDetails.PibCustomerNoteFk, true, DateTime.Now, false);

                        }
                    }
                    //if all not resolved add log
                    else
                    {
                        shippingRepository.AddLogTrackPsbpibIssueLifeCycle(shipDetails.ShippingInvoiceFk, 0
                                                      , "STStatusUpdatePSB", null, null, null, null, true, null, false,
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
                                  "PSBStatusUpdate",
                                  "PIBStatusUpdate Completed for"+ shipDetails.ShippingInvoiceFk,
                                  DateTime.Now);
                }
               
            }
            else
            {
                int logId = shippingRepository.AddLogStStatusUpdatePsb("",
                             "PIBStatusUpdate Skipped",
                             0,
                             "PIBStatusUpdate",
                             "No Records found",
                             DateTime.Now);
            }
        }
    }
}
