using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TypeSharp;

namespace ExampleProject.Model.Data
{
    [TypeSharpClass]
    public class DataRepository
    {
        public string DataContext { get; set; }
        public string DataRepeat(int numb)
        {
            return "test";
        }
    }
}