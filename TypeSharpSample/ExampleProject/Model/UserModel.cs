using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TypeSharp;

namespace ExampleProject.Model
{
    [TypeSharpClass]
    public class UserModel : IUser
    {
        public string Name { get; set; }

        public DateTime LastLogin { get; set; }

        public IUser User { get; set; }


        public string Organization
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }


        public decimal CombineArguments(string argument1, int argument2)
        {
            throw new NotImplementedException();
        }
    }
}