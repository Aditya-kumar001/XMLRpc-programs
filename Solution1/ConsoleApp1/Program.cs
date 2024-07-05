//////using System;
//////using System.Collections.Generic;
//////using OdooXmlRpcLibrary;
//////using CookComputing.XmlRpc;
//////using static OdooXmlRpcLibrary.OdooXmlRpcClient;

//////namespace ConsoleApp1
//////{
//////    class Program
//////    {
//////        static void Main(string[] args)
//////        {
//////            string url = "http://localhost:8010";
//////            string db = "your_database";
//////            string username = "your_username";
//////            string password = "your_password";

//////            try
//////            {
//////                OdooXmlRpcClient client = new OdooXmlRpcClient(url, db, username, password);
//////                Console.WriteLine("Authentication successful!");

//////                int invoiceNumber = 3090; // Example invoice number

//////                bool exists = client.InvoiceExists(invoiceNumber);
//////                if (exists)
//////                {
//////                    Console.WriteLine($"Invoice {invoiceNumber} exists.");
//////                }
//////                else
//////                {
//////                    Console.WriteLine($"Invoice {invoiceNumber} does not exist.");
//////                }
//////            }
//////            catch (Exception ex)
//////            {
//////                Console.WriteLine($"Error: {ex.Message}");
//////            }
//////        }
//////    }
//////}
////using System;
////using System.Collections.Generic;
////using CookComputing.XmlRpc;// If using CookComputing.XmlRpc
////using OdooXmlRpcLibrary;
////using static OdooXmlRpcLibrary.OdooXmlRpcClient;


////namespace ConsoleApp1
////{
////    class Program
////    {
////        static void Main(string[] args)
////        {
////            string url = "http://localhost:8010";
////            string db = "invoice_payment1";
////            string username = "admin";
////            string password = "admin";



////            //===============invoice data =============================//

////            //try
////            //{
////            //    OdooXmlRpcClient client = new OdooXmlRpcClient(url, db, username, password);
////            //    Console.WriteLine("Authentication successful!");

////            //    var invoiceData = new Dictionary<string, object>()
////            //                {
////            //                    { "name", "INV/2024/00001" },
////            //                    // Add other invoice data fields here
////            //                };
////            //}
////            //catch (Exception ex)
////            //{
////            //    Console.WriteLine($"Error: {ex.Message}");
////            //}
////            //=====================Create Invoice using library and XmlRpc=================================//
////            //Dictionary<string, object> invoiceData = new Dictionary<string, object>
////            //{
////            //    { "name", "Invoice001" }, // Replace with actual invoice name
////            //    { "partner_id", 123 },    // Replace with actual partner ID
////            //    { "amount_total", 1000.0 } // Replace with actual amount
////            //    // Add other invoice data as needed
////            //};

////            //try
////            //{
////            //    int invoiceId = CreateInvoice(invoiceData);
////            //    Console.WriteLine($"Invoice created successfully with ID: {invoiceId}");
////            //}
////            //catch (InvalidOperationException ex)
////            //{
////            //    Console.WriteLine($"Error creating invoice: {ex.Message}");
////            //}
////            //catch (Exception ex)
////            //{
////            //    Console.WriteLine($"Unexpected error: {ex.Message}");
////            //}

////            //Console.WriteLine("Press any key to exit...");
////            //Console.ReadKey();

////            //====================Post invoice=======================//

////            var client = new OdooXmlRpcClient(url, db, username, password);
////            string invoiceId = "2";
////            client.PostInvoice(invoiceId);
////            //=============================================================================//

////            //var client = new OdooXmlRpcClient(url, db, username, password);
////            //var invoiceData = new Dictionary<string, object>()
////            //{
////            //    { "name", "INV/2024/00005" },
////            //    { "date_invoice", DateTime.Now },
////            //};

////            //int invoiceId = 2; // Replace with your actual invoice ID

////            //client.PostInvoice(invoiceId, invoiceData);

////            //==================================================================================//


////            //var client = new OdooXmlRpcClient(url, db, username, password);

////            //// Example usage: Reconcile invoice with ID 123
////            //int invoiceId = 2;
////            //client.ReconcileInvoice(invoiceId);

////        }
////    }
////}

//using System;
//using System.Collections.Generic;
//using OdooXmlRpcLibrary;

//namespace ConsoleApp
//{
//    class Program
//    {
//        static void Main(string[] args)
//        {
//            // Replace these values with your actual Odoo server details
//            string url = "http://localhost:8010";
//            string db = "invoice_payment1";
//            string username = "admin";
//            string password = "admin";

//            //var client = new OdooXmlRpcClient(url, db, username, password);

//            //// Example invoice number to check
//            //string invoiceNumber = "INV/2024/001";  // Replace with your invoice number

//            //// Check if the invoice exists
//            //bool invoiceExists = client.InvoiceExists(invoiceNumber);

//            //if (invoiceExists)
//            //{
//            //    Console.WriteLine($"Invoice with number '{invoiceNumber}' exists in Odoo.");
//            //}
//            //else
//            //{
//            //    Console.WriteLine($"Invoice with number '{invoiceNumber}' does not exist in Odoo.");
//            //}

////====================================================================================================================//

//            var client = new OdooXmlRpcClient(url, db, username, password);

//            // Example invoice ID to reconcile
//            int invoiceId = 1;  // Replace with your invoice ID

//            // Reconcile the invoice
//            client.ReconcileInvoice(invoiceId);

//            Console.WriteLine($"Invoice with ID '{invoiceId}' reconciled successfully.");
//        }
//    }
//}



using OdooXmlRpcLibrary;
using System;
using System.Collections.Generic;

namespace OdooInvoiceIntegration
{
    class Program
    {
        static void Main(string[] args)
        {
            // Replace these with your Odoo instance details
            string odooUrl = "http://localhost:8010";
            string database = "invoice_payment1";
            string username = "admin";
            string password = "admin";

            // Initialize Odoo XML-RPC client
            var client = new OdooXmlRpcClient(odooUrl, database, username, password);

            // Example data for creating an invoice
            var invoiceData = new Dictionary<string, object>
            {
                { "partner_id", 1 },            // Example partner_id
                { "invoice_number", "INV/2024/00007" }, // Example invoice number
                { "date_invoice", DateTime.Now.ToString("2024-12-12") },
                { "amount_total", 1000.00 }     // Example amount_total
                // Add other required fields as needed
            };

            string invoiceNumber = "INV/2024/00007";

            try
            {

                Console.WriteLine($"0001");
                // Check if invoice with the same number exists
                if (client.CheckInvoiceExists(invoiceNumber))
                {
                    Console.WriteLine($"0002");
                    Console.WriteLine($"Invoice with number {invoiceNumber} already exists in Odoo.");
                }
                else
                {
                    Console.WriteLine($"0003");
                    // Create the invoice
                    int invoiceId = client.CreateInvoice(invoiceData);
                    Console.WriteLine($"Invoice created successfully with ID: {invoiceId}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"0004");
                Console.WriteLine($"An error occurred: {ex.Message}");
            }

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
