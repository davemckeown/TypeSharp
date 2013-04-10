// <copyright file="TypeSharpProject.cs" company="TypeSharp Project">
//     Apache 2.0 License
// </copyright>
// <author>Dave McKeown</author>

namespace TypeSharp.Core
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Xml.Linq;

    /// <summary>
    /// TypeSharpProject manages the settings for a TypeSharpProject
    /// </summary>
    public class TypeSharpProject
    {
        /// <summary>
        /// The settings XML file
        /// </summary>
        private XDocument settings;

        /// <summary>
        /// Initializes a new instance of the TypeSharpProject class
        /// </summary>
        /// <param name="solution">The path to the solution file</param>
        public TypeSharpProject(string solution)
        {
            this.SettingsPath = string.Format("{0}{1}{2}", Path.GetDirectoryName(solution), Path.DirectorySeparatorChar, string.Format("{0}.TypeSharp.xml", Path.GetFileNameWithoutExtension(solution)));

            if (File.Exists(this.SettingsPath))
            {
                try
                {
                    this.settings = XDocument.Load(this.SettingsPath);
                }
                catch (Exception)
                {
                    this.settings = this.CreateNewSettingsFile();
                }
            }
            else
            {
                this.settings = this.CreateNewSettingsFile();
            }
        }

        /// <summary>
        /// Gets or sets the OutputProject
        /// </summary>
        public string OutputProject
        {
            get
            {
                return this.settings.Descendants("OutputProject").Single().Value;
            }

            set
            {
                this.settings.Descendants("OutputProject").Single().Value = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the ToolPane window should be shown
        /// </summary>
        public bool ShowToolPaneWindow
        {
            get
            {
                string value = this.settings.Descendants("ShowToolPaneWindow").Single().Value;
                return !string.IsNullOrEmpty(value) ? Convert.ToBoolean(value) : false;
            }

            set
            {
                this.settings.Descendants("ShowToolPaneWindow").Single().Value = value.ToString();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether test classes should be generated
        /// </summary>
        public bool CreateTestClasses
        {
            get
            {
                string value = this.settings.Descendants("CreateTestClasses").Single().Value;
                return !string.IsNullOrEmpty(value) ? Convert.ToBoolean(value) : false;
            }

            set
            {
                this.settings.Descendants("CreateTestClasses").Single().Value = value.ToString();
            }
        }

        /// <summary>
        /// Gets or sets the setting path
        /// </summary>
        private string SettingsPath { get; set; }

        /// <summary>
        /// Saves the TypeSharp settings
        /// </summary>
        public void Save()
        {
            this.settings.Save(this.SettingsPath);
        }

        /// <summary>
        /// Create a new setting file in memory
        /// </summary>
        /// <returns>Settings XDocument</returns>
        private XDocument CreateNewSettingsFile()
        {
            return new XDocument(
                new XElement(
                    "TypeSharp", 
                    new XElement("OutputProject", string.Empty), 
                    new XElement("ShowToolPaneWindow", string.Empty),
                    new XElement("TestingServer", string.Empty),
                    new XElement("CreateTestClasses", string.Empty)));
        }

        public string TestingServer
        {
            get
            {
                return this.settings.Descendants("TestingServer").Single().Value;
            }

            set
            {
                this.settings.Descendants("TestingServer").Single().Value = value;
            }
        }
    }
}
