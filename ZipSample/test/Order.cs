using System;
using System.Security.Cryptography;

namespace ZipSample.test
{
    public class Order
    {

        public int OrderId { get; set; }
        public int Amount { get; set; }
        public DateTime CreateDate { get; set; }
        public int Key { get; set; }
        public string Name { get; set; }

    }

}