using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypeSharp;

namespace BusinessObjects
{
    [TypeSharpClass]
    public class Order
    {

        public Order()
        {
            Products = new List<Product>();
        }

        public void AddProducts(List<Product> products, string group) 
        {
            Products.AddRange(products);
        }

        public List<Product> Products { get; set; }
    }
}
