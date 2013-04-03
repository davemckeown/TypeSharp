// <copyright file="ReferenceAggregator.cs" company="TypeSharp Project">
//     Apache 2.0 License
// </copyright>
// <author>Dave McKeown</author>

namespace TypeSharpParser.Generator
{
    using System.Collections.Generic;

    /// <summary>
    /// ReferenceAggregator tracks reference between modules
    /// </summary>
    public class ReferenceAggregator
    {
        /// <summary>
        /// The module references 
        /// </summary>
        private Dictionary<string, List<string>> moduleReferences = new Dictionary<string, List<string>>();

        /// <summary>
        /// Gets the list of references for a module
        /// </summary>
        /// <param name="module">The module</param>
        /// <returns>The list of referenced modules</returns>
        public List<string> this[string module]
        {
            get
            {
                if (this.moduleReferences.ContainsKey(module))
                {
                    return this.moduleReferences[module];
                }

                return null;
            }
        }

        /// <summary>
        /// Adds a reference between modules
        /// </summary>
        /// <param name="from">From module</param>
        /// <param name="to">To module</param>
        public void AddReference(string from, string to)
        {
            if (!this.moduleReferences.ContainsKey(from))
            {
                this.moduleReferences.Add(from, new List<string>());
            }

            this.moduleReferences[from].Add(to);
        }
    }
}
