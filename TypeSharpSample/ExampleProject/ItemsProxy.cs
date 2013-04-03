using ExampleProject.Controller.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TypeSharp;

namespace ExampleProject
{
    [TypeSharpClass]
    public class ItemsProxy
    {
        public ItemController ItemController { get; set; }
    }
}