using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypeSharp;

namespace BusinessObjects
{
    /// <summary>
    /// Testing Comment
    /// </summary>
    [TypeSharpClass]
    public class Order
    {

        public Order()
        {
            Products = new List<Product>();
        }

        /// <summary>
        /// Add products to the order
        /// </summary>
        /// <param name="products"></param>
        /// <param name="group"></param>
        public void AddProducts(List<Product> products, string group) 
        {
            Products.AddRange(products);
        }

        public List<Product> Products { get; set; }
    }
}
