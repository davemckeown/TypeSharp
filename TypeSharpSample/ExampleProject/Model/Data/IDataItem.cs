using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TypeSharp;

namespace ExampleProject.Model.Data
{
    [TypeSharpInterface]
    public interface IDataItem
    {
        string DataField { get; set; }
        object DataValue { get; set; }
    }
}
