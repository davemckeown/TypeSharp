using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypeSharp;

namespace BusinessObjects
{
    /// <summary>
    /// Testing comment
    /// </summary>
    [TypeSharpClass]
    public class Customer : BusinessObjects.IOrderAssignable
    {
        /// <summary>
        /// Assign the Order to the Customer
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public bool AssignOrder(Order order) 
        {
            return true;
        }
    }
}
