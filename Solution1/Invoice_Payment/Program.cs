using System;
using System.Collections.Generic;
using OdooXmlRpcLibrary;
using CookComputing.XmlRpc;
using static OdooXmlRpcLibrary.OdooXmlRpcClient;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            string url = "http://localhost:8010";
            string db = "your_database";
            string username = "your_username";
            string password = "your_password";

            try
            {
                OdooXmlRpcClient client = new OdooXmlRpcClient(url, db, username, password);
                Console.WriteLine("Authentication successful!");

                int invoiceNumber = 3090; // Example invoice number

                bool exists = client.InvoiceExists(invoiceNumber);
                if (exists)
                {
                    Console.WriteLine($"Invoice {invoiceNumber} exists.");
                }
                else
                {
                    Console.WriteLine($"Invoice {invoiceNumber} does not exist.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}
