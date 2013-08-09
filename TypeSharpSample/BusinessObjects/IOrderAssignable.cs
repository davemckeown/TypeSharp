using System;
using TypeSharp;
namespace BusinessObjects
{
    [TypeSharpInterface]
    public interface IOrderAssignable
    {
        /// <summary>
        /// Assigns the Order to assignable
        /// </summary>
        /// <param name="order">The order to assign</param>
        /// <returns>true on success</returns>
        bool AssignOrder(Order order);
    }
}
