using ExampleProject.Model.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TypeSharp;

namespace ExampleProject.Controller.Data
{
    [TypeSharpClass]
    public class ItemController
    {
        public IDataItem Item { get; set; }
    }
}