using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypeSharp;

namespace BusinessObjects
{
    /// <summary>
    /// Product
    /// </summary>
    [TypeSharpClass]
    public class Product
    {
        /// <summary>
        /// Gets or sets the product name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the unit price
        /// </summary>
        public decimal UnitPrice { get; set; }

        /// <summary>
        /// Gets or sets the Description
        /// </summary>
        public string Description { get; set; }
    }
}
