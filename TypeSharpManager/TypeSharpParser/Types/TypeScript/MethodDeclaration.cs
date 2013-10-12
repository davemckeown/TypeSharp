// <copyright file="MethodDeclaration.cs" company="TypeSharp Project">
//     Apache 2.0 License
// </copyright>
// <author>Dave McKeown</author>

namespace TypeSharpParser.Types.TypeScript
{
    using System.Collections.Generic;
    using System.Linq;

    using Antlr.Runtime;

    using TypeSharpController.Types;

    /// <summary>
    /// MethodDeclaration represents a class or interface method declaration
    /// </summary>
    public class MethodDeclaration
    {
        /// <summary>
        /// A list of MethodArgumentDeclaration instances that form the methods arguments
        /// </summary>
        private readonly List<MethodArgumentDeclaration> arguments = new List<MethodArgumentDeclaration>();

        /// <summary>
        /// Gets the method's arguments
        /// </summary>
        public List<MethodArgumentDeclaration> Arguments
        {
            get
            {
                return this.arguments;
            }
        }

        /// <summary>
        /// Gets or sets the name of the method
        /// </summary>
        public string Identifier { get; set; }

        /// <summary>
        /// Compares the method for equality to another method
        /// </summary>
        /// <param name="obj">Object to compare</param>
        /// <returns>True if equal</returns>
        public override bool Equals(object obj)
        {
            MethodDeclaration compare = obj as MethodDeclaration;

            if (obj == null)
            {
                return false;
            }

            if (compare != null && this.Identifier != compare.Identifier)
            {
                return false;
            }

            if (compare != null && this.Arguments.Count != compare.Arguments.Count)
            {
                return false;
            }

            return !this.Arguments.Where((t, x) => compare != null && !t.Equals(compare.Arguments[x])).Any();
        }

        /// <summary>
        /// Calculates the objects hash code
        /// </summary>
        /// <returns>The object hash code</returns>
        public override int GetHashCode()
        {
            int hash = 13;
            hash = (hash * 7) + this.Arguments.GetHashCode();
            hash = (hash * 7) + this.Identifier.GetHashCode();

            return hash;
        }
    }
}
