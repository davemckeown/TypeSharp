using ExampleProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypeSharp;

namespace ExampleProject2.Repository
{

    [TypeSharpClass]
    public class UserRepository
    {
        public IUser UserData { get; set; }
    }
}
