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
    public class Product
    {
        public string Name { get; set; }
        public decimal UnitPrice { get; set; }
        public string Description { get; set; }
    }
}
