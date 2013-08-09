using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypeSharp;

namespace BusinessObjects.DataStore
{
    /// <summary>
    /// DataServices Class
    /// </summary>
    [TypeSharpClass]
    public class DataService
    {
        /// <summary>
        /// Gets or sets the connection string
        /// </summary>
        public string ConnectionString { get; set; }

        /// <summary>
        /// Load the Products
        /// </summary>
        /// <returns>All Products</returns>
        public List<Product> LoadProducts()
        {
            return null;
        }

        /// <summary>
        /// Load the Customers
        /// </summary>
        /// <returns>Loads all Customers</returns>
        public List<Customer> LoadCustomers()
        {
            return null;
        }

        /// <summary>
        /// Load all Orders
        /// </summary>
        /// <returns>Loads all Orders</returns>
        public List<Order> LoadOrders()
        {
            return null;
        }
    }
}
