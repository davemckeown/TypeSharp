using System;

namespace TypeSharp
{
    [System.AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class TypeSharpClass : System.Attribute
    {
        public string Module;
        public string ClassName;
    }
}
