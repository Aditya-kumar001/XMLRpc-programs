//using CookComputing.XmlRpc;
//using System;
//using System.Collections.Generic;

//namespace OdooXmlRpcLibrary
//{
//    public class OdooXmlRpcClient
//    {
//        private readonly string _url;
//        private readonly string _db;
//        private readonly string _username;
//        private readonly string _password;
//        private readonly int _uid;

//        public OdooXmlRpcClient(string url, string db, string username, string password)
//        {
//            _url = url;
//            _db = db;
//            _username = username;
//            _password = password;

//            _uid = Authenticate();

//        }

//        //=================Authenticates the user with Odoo and retrieves the ser ID===============//
//        private int Authenticate()
//        {
//            var client = XmlRpcProxyGen.Create<ICommonProxy>();
//            //updated a line of code here client.url...
//            client.Url = $"{_url}/xmlrpc/2/common";
//            var uid = client.Authenticate(_db, _username, _password, new object[] { });
//            return uid;
//        }

//        //================Attribute that maps interface methods to Odoo XML-RPC methods.============//
//        [XmlRpcUrl("http://localhost:8010/xmlrpc/2/common")]
//        public interface ICommonProxy : IXmlRpcProxy
//        {
//            [XmlRpcMethod("authenticate")]
//            int Authenticate(string db, string username, string password, object[] args);
//        }

//        [XmlRpcUrl("http://localhost:8010/xmlrpc/2/object")]
//        public interface IObjectProxy : IXmlRpcProxy
//        {
//            [XmlRpcMethod("execute_kw")]
//            int[] Search(string db, int uid, string password, string model, object[] domain, object options);

//            [XmlRpcMethod("execute_kw")]
//            object[] ExecuteKw(string db, int uid, string password, string model, string method, object[] args, IDictionary<string, object> kwargs);
//        }

//        //==============Creates and returns an IObjectProxy instance for interacting with Odoo models.========//
//        private IObjectProxy GetObjectProxy()
//        {
//            //return XmlRpcProxyGen.Create<IObjectProxy>();
//            var proxy = XmlRpcProxyGen.Create<IObjectProxy>();
//            proxy.Url = $"{_url}/xmlrpc/2/object";
//            return proxy;
//        }

//        //===============1.Checks if an invoice with the given number exists in Odoo===============//
//        public bool InvoiceExists(string invoiceNumber)
//        {
//            var proxy = GetObjectProxy();
//            var result = proxy.ExecuteKw(_db, _uid, _password, "account.move", "search",
//                new object[] { new List<object> { new List<object> { "name", "ilike", invoiceNumber } } },
//                new Dictionary<string, object>());
//            return result.Length > 0;
//        }

//        //        //===============2.Creates a new invoice in Odoo if it doesn't already exist==============//
//        //        //public int CreateInvoice(Dictionary<string, object> invoiceData)
//        //        //{
//        //        //    if (InvoiceExists(Convert.ToInt32(invoiceData["name"])))
//        //        //    {
//        //        //        throw new InvalidOperationException("Invoice already exists.");
//        //        //    }

//        //        //    var proxy = GetObjectProxy();
//        //        //    var result = proxy.ExecuteKw(_db, _uid, _password, "account.move", "create",
//        //        //        new object[] { invoiceData },
//        //        //        new Dictionary<string, object>());
//        //        //    return Convert.ToInt32(result[0]);
//        //        //}
//        public int CreateInvoice(Dictionary<string, object> invoiceData)
//        {
//        // Check if an invoice with the given name already exists
//            if (InvoiceExists(invoiceData["name"].ToString()))
//            {
//                throw new InvalidOperationException("Invoice already exists.");
//            }

//            var proxy = GetObjectProxy();
//            var result = proxy.ExecuteKw(_db, _uid, _password, "account.move", "create",
//            new object[] { new object[] { invoiceData } },  // Pass invoiceData correctly
//            new Dictionary<string, object>());

//            // Assuming result[0] contains the ID of the newly created invoice
//            return Convert.ToInt32(result[0]);
//        }


//        //        //==============3.Posts (validates) the invoice in Odoo====================//
//        //        public void PostInvoice(string invoiceId)
//        //        {
//        //            var proxy = GetObjectProxy();
//        //            proxy.ExecuteKw(_db, _uid, _password, "account.move", "action_post",
//        //                new object[] { new object[] { invoiceId } },
//        //                new Dictionary<string, object>());
//        //            Console.WriteLine($"Invoice {invoiceId} posted successfully.");
//        //        }

//        ////================4.Creates a new payment in Odoo =========================//
//        //        public int CreatePayment(Dictionary<string, object> paymentData)
//        //        {
//        //            var proxy = GetObjectProxy();
//        //            var result = proxy.ExecuteKw(_db, _uid, _password, "account.payment", "create",
//        //                new object[] { paymentData },
//        //                new Dictionary<string, object>());
//        //            return Convert.ToInt32(result[0]);
//        //        }

//        ////===================5. Posta (validates) the payment in Odoo =====================//

//        //        public void PostInvoice(int invoiceId, Dictionary<string, object> invoiceData)
//        //        {
//        //            var proxy = GetObjectProxy();


//        //            proxy.ExecuteKw(_db, _uid, _password, "account.move", "action_post",
//        //                new object[] { new object[] { invoiceId },  },    // remove rpcData from this line of code 
//        //                new Dictionary<string, object>());
//        //            Console.WriteLine($"Invoice {invoiceId} posted successfully.");
//        //        }

//        //        //==================6. Checks if the total payments cover the invoice amount================//
//        //        public void ReconcileInvoice(int invoiceId)
//        //        {
//        //            var proxy = GetObjectProxy();
//        //            var invoice = proxy.ExecuteKw(_db, _uid, _password, "account.move", "read",
//        //                new object[] { new object[] { invoiceId } },
//        //                new Dictionary<string, object>());

//        //            var payments = proxy.ExecuteKw(_db, _uid, _password, "account.payment", "search",
//        //                new object[] { new List<object> { new List<object> { "invoice_ids", "in", invoiceId } } },
//        //                new Dictionary<string, object>());

//        //            decimal invoiceAmount = Convert.ToDecimal(((object[])invoice[0])[0]);
//        //            decimal paymentAmount = 0;
//        //            foreach (var payment in payments)
//        //            {
//        //                var paymentDetails = proxy.ExecuteKw(_db, _uid, _password, "account.payment", "read",
//        //                    new object[] { new object[] { payment } },
//        //                    new Dictionary<string, object>());
//        //                paymentAmount += Convert.ToDecimal(((object[])paymentDetails[0])[0]);
//        //            }

//        //            if (invoiceAmount <= paymentAmount)
//        //            {
//        //                proxy.ExecuteKw(_db, _uid, _password, "account.move", "write",
//        //                    new object[] { new object[] { invoiceId }, new Dictionary<string, object> { { "payment_state", "paid" } } },
//        //                    new Dictionary<string, object>());
//        //            }
//    }


//}

using CookComputing.XmlRpc;
using System;
using System.Collections.Generic;

namespace OdooXmlRpcLibrary
{
    public class OdooXmlRpcClient
    {
        private readonly string _url;
        private readonly string _db;
        private readonly string _username;
        private readonly string _password;
        private readonly int _uid;

        public OdooXmlRpcClient(string url, string db, string username, string password)
        {
            _url = url;
            _db = db;
            _username = username;
            _password = password;

            _uid = Authenticate();

        }

        //=================Authenticates the user with Odoo and retrieves the ser ID===============//
        private int Authenticate()
        {
            var client = XmlRpcProxyGen.Create<ICommonProxy>();
            //updated a line of code here client.url...
            client.Url = $"{_url}/xmlrpc/2/common";
            var uid = client.Authenticate(_db, _username, _password, new object[] { });
            return uid;
        }

        public int CreateInvoice(Dictionary<string, object> invoiceData)
        {
            int uid = Authenticate();
            int invoiceId = 0;

            try
            {
                var proxy = XmlRpcProxyGen.Create<IObjectProxy>();
                proxy.Url = $"{_url}/object";

                // Create an XmlRpcStruct and populate it with invoiceData
                var xmlRpcStruct = new XmlRpcStruct();
                foreach (var item in invoiceData)
                {
                    xmlRpcStruct.Add(item.Key, item.Value);
                }

                invoiceId = Convert.ToInt32(proxy.ExecuteKw(_db, uid, _password,
                    "account.move", "create", new object[] { new object[] { xmlRpcStruct } }, null));
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to create invoice in Odoo.", ex);
            }

            return invoiceId;
        }

        public bool CheckInvoiceExists(string invoiceNumber)
        {
            int uid = Authenticate();
            bool exists = false;

            try
            {
                var proxy = XmlRpcProxyGen.Create<IObjectProxy>();
                proxy.Url = $"{_url}/object";
                var domain = new object[] {
                        new object[] {
                            new object[] { "name", "ilike", invoiceNumber }
                        }
                    };

                object[] invoiceIds = proxy.Search(_db, uid, _password,
                    "account.move", domain, new object{ });

                exists = invoiceIds.Length>0;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to check if invoice exists in Odoo.", ex);
            }

            return exists;
        }

        [XmlRpcUrl("http://localhost:8010/xmlrpc/2/common")]
        public interface ICommonProxy : IXmlRpcProxy
        {
            [XmlRpcMethod("authenticate")]
            int Authenticate(string db, string username, string password, object[] args);
        }

        [XmlRpcUrl("http://localhost:8010/xmlrpc/2/object")]
        public interface IObjectProxy : IXmlRpcProxy
        {
            [XmlRpcMethod("execute_kw")]
            object ExecuteKw(string db, int uid, string password, string model, string method, object[] args, IDictionary<string, object> kwargs);

            [XmlRpcMethod("execute_kw")]
            object[] Search(string db, int uid, string password, string model, object[] domain, object options);
        }
    }
}

