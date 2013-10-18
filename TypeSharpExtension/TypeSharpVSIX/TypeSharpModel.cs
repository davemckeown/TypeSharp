// <copyright file="TypeSharpModel.cs" company="TypeSharp Project">
//     Apache 2.0 License
// </copyright>
// <author>Dave McKeown</author>

namespace TypeSharp.VisualStudioExtension
{
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using EnvDTE;

    /// <summary>
    /// TypeSharpModel manages data for the TypeSharp extension
    /// </summary>
    public class TypeSharpModel
    {
        /// <summary>
        /// Initializes a new instance of the TypeSharpModel class
        /// </summary>
        public TypeSharpModel()
        {
            this.BuildResults = new List<bool>();
        }

        /// <summary>
        /// Gets or sets the build result success list
        /// </summary>
        public List<bool> BuildResults { get; set; }

        /// <summary>
        /// Gets or sets the source files stack
        /// </summary>
        public ConcurrentStack<string> SourceFiles { get; set; }

        /// <summary>
        /// Gets or sets the user projects in the solution
        /// </summary>
        public List<Project> UserProjects { get; set; }

        /// <summary>
        /// Enumerates the project items in a project
        /// </summary>
        /// <param name="project">The target project</param>
        /// <returns>ProjectItem Enumerable</returns>
        public IEnumerable<ProjectItem> AllProjectItems(Project project)
        {
            if (project != null && project.ProjectItems != null)
            {
                foreach (ProjectItem item in project.ProjectItems)
                {
                    yield return item;

                    foreach (ProjectItem subitem in this.SubProjectItems(item))
                    {
                        yield return subitem;
                    }
                }
            }
        }

        /// <summary>
        /// Enumerates a project item for sub project items
        /// </summary>
        /// <param name="item">The project item</param>
        /// <returns>ProjectItem Enumerable</returns>
        public IEnumerable<ProjectItem> SubProjectItems(ProjectItem item)
        {
            if (item != null && item.ProjectItems != null)
            {
                foreach (ProjectItem record in item.ProjectItems)
                {
                    yield return record;

                    foreach (ProjectItem child in this.SubProjectItems(record))
                    {
                        yield return child;
                    }
                }
            }
        }
    }
}
