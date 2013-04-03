using ExampleProject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypeSharp;

namespace ExampleProject2.Repository
{
    [TypeSharpClass]
    public class DataRepository
    {
        public string DataField { get; set; }
        public ItemsProxy DataContext { get; set; }
    }
}
