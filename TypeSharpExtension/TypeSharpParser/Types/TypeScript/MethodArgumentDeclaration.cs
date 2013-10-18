// <copyright file="MethodArgumentDeclaration.cs" company="TypeSharp Project">
//     Apache 2.0 License
// </copyright>
// <author>Dave McKeown</author>

namespace TypeSharpController.Types
{
    /// <summary>
    /// MethodArgumentDeclaration represents a argument declaration in a method
    /// </summary>
    public class MethodArgumentDeclaration
    {
        /// <summary>
        /// Gets or sets the name of the argument
        /// </summary>
        public string Identifier { get; set; }

        /// <summary>
        /// Gets or sets the type of the argument
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Compares the argument to another for equality
        /// </summary>
        /// <param name="obj">The object to compare</param>
        /// <returns>True if equal</returns>
        public override bool Equals(object obj)
        {
            MethodArgumentDeclaration compare = obj as MethodArgumentDeclaration;

            if (compare == null)
            {
                return false;
            }

            return this.Identifier == compare.Identifier && this.Type == compare.Type;
        }

        /// <summary>
        /// Calculates the object hash code
        /// </summary>
        /// <returns>The object hash code</returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
