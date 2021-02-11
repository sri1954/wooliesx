using System;
using System.Collections.Generic;
using System.Text;

namespace WXFunctionApp.Models
{
    public class user
    {
        public string name { get; set; }
        public string token { get; set; }
    }

    public class customer
    {
        public int customerId { get; set; }
        public string name { get; set; }
    }

    public class product
    {
        public string name { get; set; }
        public float price { get; set; }
        public int quantity { get; set; }
    }
}
