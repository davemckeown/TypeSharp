// <copyright file="MethodDeclaration.cs" company="TypeSharp Project">
//     Apache 2.0 License
// </copyright>
// <author>Dave McKeown</author>

namespace TypeSharpController.Types
{
    using System.Collections.Generic;

    /// <summary>
    /// MethodDeclaration represents a class or interface method declaration
    /// </summary>
    public class MethodDeclaration
    {
        /// <summary>
        /// A list of MethodArgumentDeclaration instances that form the methods arguments
        /// </summary>
        private List<MethodArgumentDeclaration> arguments = new List<MethodArgumentDeclaration>();

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

            if (this.Identifier != compare.Identifier)
            {
                return false;
            }

            if (this.Arguments.Count != compare.Arguments.Count)
            {
                return false;
            }

            for (int x = 0; x < this.Arguments.Count; x++)
            {
                if (!this.Arguments[x].Equals(compare.Arguments[x]))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Calculates the objects hash code
        /// </summary>
        /// <returns>The object hash code</returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
