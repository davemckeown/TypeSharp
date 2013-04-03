// <copyright file="ProjectConfiguration.xaml.cs" company="TypeSharp Project">
//     Apache 2.0 License
// </copyright>
// <author>Dave McKeown</author>

namespace TypeSharp.VisualStudioExtension.Controls
{
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows;
    using EnvDTE;
    using EnvDTE80;
    using Microsoft.VisualStudio.PlatformUI;
    using TypeSharp.Core;

    /// <summary>
    /// Interaction logic for ProjectConfiguration
    /// </summary>
    public partial class ProjectConfiguration : DialogWindow
    {
        /// <summary>
        /// The TypeSharp project
        /// </summary>
        private TypeSharpProject project;

        /// <summary>
        /// The projects in the solution
        /// </summary>
        private ObservableCollection<string> projects;

        /// <summary>
        /// Initializes a new instance of the ProjectConfiguration class
        /// </summary>
        /// <param name="package">The VisualStudio IDE Instance</param>
        public ProjectConfiguration(DTE2 package)
        {
            this.VisualStudio = package;
            this.project = new TypeSharpProject(this.VisualStudio.Solution.FullName);
            this.InitializeComponent();
        }

        /// <summary>
        /// Gets the Projects in the Solution
        /// </summary>
        public ObservableCollection<string> Projects
        {
            get
            {
                if (this.projects == null)
                {
                    this.projects = new ObservableCollection<string>();

                    foreach (string project in this.VisualStudio.Solution.Projects.OfType<Project>().Select(x => x.Name))
                    {
                        this.projects.Add(project);
                    }
                }

                return this.projects;
            }
        }

        /// <summary>
        /// Gets or sets a reference to the VisualStudio IDE
        /// </summary>
        public DTE2 VisualStudio { get; set; }

        /// <summary>
        /// Gets or sets the selected project
        /// </summary>
        public string Project
        {
            get
            {
                return this.project.OutputProject;
            }

            set
            {
                this.project.OutputProject = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether tests should be generated
        /// </summary>
        public bool CreateTests
        {
            get
            {
                return this.project.CreateTestClasses;
            }

            set 
            {
                this.project.CreateTestClasses = value;
            }
        }

        /// <summary>
        /// The save button event handler
        /// </summary>
        /// <param name="sender">Save button</param>
        /// <param name="e">event argument</param>
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            this.project.Save();
            this.DialogResult = true;
        }
    }
}
