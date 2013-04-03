using ExampleProject.Model.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TypeSharp;

namespace ExampleProject.Model
{
    [TypeSharpClass]
    public class CompositeTypeTest
    {
        public IUser User { get; set; }
        public DataRepository Repository { get; set; }

        public IDataItem ComplexMethod(string item)
        {
            return null;
        }
    }
}