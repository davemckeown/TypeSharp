using System;

namespace TypeSharp
{
    [System.AttributeUsage(AttributeTargets.Interface, AllowMultiple = false)]
    public class TypeSharpInterface : System.Attribute
    {
        public string Module;
        public string InterfaceName;
    }
}
