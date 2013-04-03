// <copyright file="TypeSharpController.cs" company="TypeSharp Project">
//     Apache 2.0 License
// </copyright>
// <author>Dave McKeown</author>

namespace TypeSharp.VisualStudioExtension
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.ComponentModel.Design;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Threading.Tasks;
    using EnvDTE;
    using EnvDTE80;
    using Microsoft.VisualStudio.Shell;
    using Microsoft.VisualStudio.Shell.Interop;
    using TypeSharp.Core;
    using TypeSharpParser;
    using TypeSharpParser.Types;

    /// <summary>
    /// TypeSharpController manages interaction with the visual studio extension
    /// </summary>
    public class TypeSharpController
    {
        /// <summary>
        /// Initializes a new instance of the TypeSharpController class
        /// </summary>
        /// <param name="instance">The visual studio instance</param>
        /// <param name="extension">The visual studio extension package</param>
        public TypeSharpController(DTE2 instance, TypeSharpVSIXPackage extension)
        {
            this.IDEInstance = instance;
            this.Extension = extension;
            this.Model = new TypeSharpModel();
        }

        /// <summary>
        /// Gets or sets the reference to the Visual Studio IDE
        /// </summary>
        public DTE2 IDEInstance { get; set; }

        /// <summary>
        /// Gets or sets the reference to the Visual Studio extension package object
        /// </summary>
        public TypeSharpVSIXPackage Extension { get; set; }

        /// <summary>
        /// Gets or sets a reference to the tool pane window
        /// </summary>
        public ToolWindowPane ToolPaneWindow { get; set; }

        /// <summary>
        /// Gets or sets a reference to the tool menu item command
        /// </summary>
        public MenuCommand ToolMenuItem { get; set; }

        /// <summary>
        /// Gets or sets a reference to the tool menu view item
        /// </summary>
        public MenuCommand ToolViewItem { get; set; }

        /// <summary>
        /// Gets or sets the reference to the TypeSharpModel
        /// </summary>
        public TypeSharpModel Model { get; set; }

        /// <summary>
        /// Persists the expansion state of the project items in a project
        /// </summary>
        /// <param name="outputTo">The target project</param>
        /// <returns>A dictionary mapping project item path to expansion state</returns>
        public Dictionary<string, bool> PersistSolutionExplorerState(string outputTo)
        {
            UIHierarchy explorer = this.IDEInstance.ToolWindows.SolutionExplorer;
            UIHierarchyItem project = explorer.UIHierarchyItems.OfType<UIHierarchyItem>().First().UIHierarchyItems.OfType<UIHierarchyItem>().FirstOrDefault(x => x.Name == outputTo);

            Dictionary<string, bool> expandState = new Dictionary<string, bool>();

            if (project != null)
            {
                foreach (var uiitem in project.UIHierarchyItems.OfType<UIHierarchyItem>())
                {
                    var test = uiitem.Name;

                    expandState.Add(uiitem.Name, uiitem.UIHierarchyItems.Expanded);

                    foreach (var childItem in uiitem.UIHierarchyItems.OfType<UIHierarchyItem>())
                    {
                        this.PersistChildUIItemState(uiitem.Name + '/', childItem, expandState);
                    }
                }
            }

            return expandState;
        }

        /// <summary>
        /// Saves the expand state of a child project item
        /// </summary>
        /// <param name="path">The item path</param>
        /// <param name="item">The item</param>
        /// <param name="expandState">The expand state cache</param>
        public void PersistChildUIItemState(string path, UIHierarchyItem item, Dictionary<string, bool> expandState)
        {
            if (!expandState.ContainsKey(path + item.Name))
            {
                expandState.Add(path + item.Name, item.UIHierarchyItems.Expanded);
            }
            else
            {
                var name = item.Name;
            }

            foreach (var childItem in item.UIHierarchyItems.OfType<UIHierarchyItem>())
            {
                this.PersistChildUIItemState(path + '/' + item.Name + '/', childItem, expandState);
            }
        }

        /// <summary>
        /// On build begin event handler
        /// </summary>
        /// <param name="scope">Build scope</param>
        /// <param name="action">Build action</param>
        public void BuildEvents_OnBuildBegin(EnvDTE.vsBuildScope scope, EnvDTE.vsBuildAction action)
        {
            TypeSharpProject project = new TypeSharpProject(this.IDEInstance.Solution.FullName);

            this.Model.BuildResults.Clear();

            if (!string.IsNullOrEmpty(project.OutputProject))
            {
                this.ScanForTypeSharpFiles();
            }
        }

        /// <summary>
        /// On build done event handler
        /// </summary>
        /// <param name="scope">Build scope</param>
        /// <param name="action">Build action</param>
        public void BuildEvents_OnBuildDone(EnvDTE.vsBuildScope scope, EnvDTE.vsBuildAction action)
        {
            try
            {
                TypeSharpProject project = new TypeSharpProject(this.IDEInstance.Solution.FullName);

                if (this.Model.BuildResults.All(x => x == true) && !string.IsNullOrEmpty(project.OutputProject))
                {
                    TypeSharpProject settings = new TypeSharpProject(this.IDEInstance.Solution.FullName);
                    string outputProject = settings.OutputProject;
                    bool createTests = settings.CreateTestClasses;

                    TypeScriptGenerator sources = new TypeScriptGenerator(this.Model.SourceFiles.ToList());
                    List<TypeScriptOutput> output = sources.GenerateOutputFiles();
                    Dictionary<string, TypeScriptReferenceOutput> references = sources.GenerateReferenceOutputFiles(output);

                    IEnumerable<string> modules = output.Select(x => x.Module).Distinct().Select(x => x.Contains('.') ? x.Substring(0, x.IndexOf('.')) : x).Distinct();

                    Dictionary<string, bool> expandState = this.PersistSolutionExplorerState(outputProject);
                    this.UnlinkExistingProjectFiles(modules, outputProject, createTests);
                    this.SyncProjectFiles(output, references, outputProject);

                    if (createTests)
                    {
                        this.SyncProjectTestFiles(output.Where(x => x.IsClass).ToList(), references, outputProject);
                    }

                    this.RestoreSolutionExplorerState(expandState, outputProject);
                }
            }
            catch (Exception ex)
            {
                Debug.Assert(false, ex.ToString());
            }
        }

        /// <summary>
        /// On project built event handler
        /// </summary>
        /// <param name="project">The project</param>
        /// <param name="projectConfig">The configuration</param>
        /// <param name="platform">The platform</param>
        /// <param name="solutionConfig">The solution configuration</param>
        /// <param name="success">Was successful flag</param>
        public void BuildEvents_OnBuildProjConfigDone(string project, string projectConfig, string platform, string solutionConfig, bool success)
        {
            this.Model.BuildResults.Add(success);
        }

        /// <summary>
        /// Before solution closing event handler
        /// </summary>
        public void SolutionEvents_BeforeClosing()
        {
            TypeSharpProject project = new TypeSharpProject(this.IDEInstance.Solution.FullName);

            if (this.ToolPaneWindow != null)
            {
                project.ShowToolPaneWindow = true;
                ((IVsWindowFrame)this.ToolPaneWindow.Frame).CloseFrame((uint)__FRAMECLOSE.FRAMECLOSE_SaveIfDirty);
            }
            else
            {
                project.ShowToolPaneWindow = false;
            }

            project.Save();
        }

        /// <summary>
        /// After solution closed event handler
        /// </summary>
        public void SolutionEvents_AfterClosing()
        {
            if (this.ToolMenuItem != null)
            {
                this.ToolMenuItem.Visible = false;
            }

            if (this.ToolViewItem != null)
            {
                this.ToolViewItem.Visible = false;
            }
        }

        /// <summary>
        /// Solution Opened event handler
        /// </summary>
        public void SolutionEvents_Opened()
        {
            TypeSharpProject project = new TypeSharpProject(this.IDEInstance.Solution.FullName);

            if (this.ToolMenuItem != null)
            {
                this.ToolMenuItem.Visible = true;
            }

            if (this.ToolViewItem != null)
            {
                this.ToolViewItem.Visible = true;
            }

            if (project.ShowToolPaneWindow)
            {
                this.ToolPaneWindow = this.Extension.FindToolWindow(typeof(TypeSharpDockWindow), 0, true);
                ((IVsWindowFrame)this.ToolPaneWindow.Frame).Show();
            }
        }

        /// <summary>
        /// Scans a solution for TypeSharp source files
        /// </summary>
        private void ScanForTypeSharpFiles()
        {
            List<string> paths = new List<string>();
            this.Model.UserProjects = new List<Project>();

            try
            {
                foreach (Project proj in this.IDEInstance.Solution.Projects)
                {
                    this.Model.UserProjects.Add(proj);

                    foreach (ProjectItem item in this.Model.AllProjectItems(proj))
                    {
                        if (item.Properties != null)
                        {
                            Property prop = item.Properties.Item("FullPath");
                            object value = null;

                            if (prop != null)
                            {
                                value = prop.Value;
                            }

                            if (value != null)
                            {
                                string path = value.ToString();

                                if (path.EndsWith("cs", StringComparison.InvariantCultureIgnoreCase))
                                {
                                    paths.Add(path);
                                }
                            }
                        }
                    }
                }

                ConcurrentStack<string> files = new ConcurrentStack<string>();

                Parallel.ForEach(
                    paths,
                    path =>
                    {
                        if (File.ReadAllText(path).IndexOf("[TypeSharp", StringComparison.InvariantCultureIgnoreCase) != -1)
                        {
                            files.Push(path);
                        }
                    });

                this.Model.SourceFiles = files;
            }
            catch (Exception ex)
            {
                Debug.Assert(false, ex.ToString());
            }
        }

        /// <summary>
        /// Determines the sub modules of a module
        /// </summary>
        /// <param name="module">The module</param>
        /// <returns>Stack of sub modules</returns>
        private Stack<string> SubModules(string module)
        {
            Stack<string> submodules = new Stack<string>();
            string submodule;

            if (module.Contains('.'))
            {
                while (module.Contains('.'))
                {
                    int index = module.LastIndexOf('.');
                    submodule = module.Substring(index + 1);
                    submodules.Push(submodule);
                    module = module.Substring(0, index);
                }

                submodules.Push(module);
            }
            else
            {
                submodules.Push(module);
            }

            return submodules;
        }

        /// <summary>
        /// Removes the existing files from the project so that they can be regenerated without "reload file" warnings in visual studio
        /// </summary>
        /// <param name="modules">The modules to generate</param>
        /// <param name="outputTo">The output project</param>
        /// <param name="createTests">Should test classes be created</param>
        private void UnlinkExistingProjectFiles(IEnumerable<string> modules, string outputTo, bool createTests)
        {
            Project project = this.IDEInstance.Solution.Projects.OfType<Project>().FirstOrDefault(x => x.Name == outputTo);
            string location;

            if (project != null)
            {
                location = Path.GetDirectoryName(project.FullName) + Path.DirectorySeparatorChar;

                foreach (string module in modules)
                {
                    ProjectItem projectModule = project.ProjectItems.OfType<ProjectItem>().FirstOrDefault(x => x.Name == module);

                    if (projectModule != null)
                    {
                        projectModule.Delete();
                    }
                    else
                    {
                        if (Directory.Exists(location + module))
                        {
                            Directory.Delete(location + module, true);
                        }
                    }

                    if (createTests)
                    {
                        string tests = string.Format("{0}.Tests", module);
                        ProjectItem testModule = project.ProjectItems.OfType<ProjectItem>().FirstOrDefault(x => x.Name == tests);

                        if (testModule != null)
                        {
                            testModule.Delete();
                        }
                        else
                        {
                            if (Directory.Exists(location + tests))
                            {
                                Directory.Delete(location + tests, true);
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Syncs the project items with the output of the TypeSharp compiler
        /// </summary>
        /// <param name="output">The TypeScriptOutput files</param>
        /// <param name="references">The TypeScript references</param>
        /// <param name="outputTo">The target project</param>
        private void SyncProjectFiles(List<TypeScriptOutput> output, Dictionary<string, TypeScriptReferenceOutput> references, string outputTo)
        {
            Project project = this.IDEInstance.Solution.Projects.OfType<Project>().FirstOrDefault(x => x.Name == outputTo);

            if (project != null)
            {
                foreach (TypeScriptOutput file in output)
                {
                    Stack<string> module = this.SubModules(file.Module);
                    ProjectItems items = project.ProjectItems;

                    StringBuilder fullPath = new StringBuilder(Path.GetDirectoryName(project.FullName));

                    while (module.Count > 0)
                    {
                        string submodule = module.Pop();
                        fullPath.Append(Path.DirectorySeparatorChar).Append(submodule);

                        if (!items.OfType<ProjectItem>().Any(x => x.Name == submodule))
                        {
                            ProjectItem folder = items.AddFolder(submodule);
                            items = folder.ProjectItems;
                        }
                        else
                        {
                            items = items.OfType<ProjectItem>().First(x => x.Name == submodule).ProjectItems;
                        }
                    }

                    if (references.ContainsKey(file.Module) && !File.Exists(fullPath.ToString() + Path.DirectorySeparatorChar + file.Module + ".d.ts"))
                    {
                        string filePath = fullPath.ToString() + Path.DirectorySeparatorChar + file.Module + ".d.ts";
                        File.WriteAllText(filePath, references[file.Module].Content);
                        items.AddFromTemplate(filePath, file.Module + ".d.ts");
                    }

                    string typescriptFileName = file.FileName + ".ts";
                    string javascriptFileName = file.FileName + ".js";

                    if (!items.OfType<ProjectItem>().Any(x => x.Name == typescriptFileName))
                    {
                        File.WriteAllText(fullPath.ToString() + Path.DirectorySeparatorChar + typescriptFileName, file.Syntax);
                        File.WriteAllText(fullPath.ToString() + Path.DirectorySeparatorChar + javascriptFileName, string.Empty);

                        ProjectItem item = items.AddFromTemplate(fullPath.ToString() + Path.DirectorySeparatorChar + typescriptFileName, typescriptFileName);

                        item.Properties.Item("ItemType").Value = "TypeScriptCompile";
                        item.ProjectItems.AddFromTemplate(fullPath.ToString() + Path.DirectorySeparatorChar + javascriptFileName, javascriptFileName);
                    }
                }
            }
        }

        /// <summary>
        /// Creates the test classes in the proper module namespace
        /// </summary>
        /// <param name="output">The TypeScriptOutput files</param>
        /// <param name="references">The TypeScript references</param>
        /// <param name="outputTo">The target project</param>
        private void SyncProjectTestFiles(List<TypeScriptOutput> output, Dictionary<string, TypeScriptReferenceOutput> references, string outputTo)
        {
            Project project = this.IDEInstance.Solution.Projects.OfType<Project>().FirstOrDefault(x => x.Name == outputTo);

            if (project != null)
            {
                ProjectItem typescriptUnit = project.ProjectItems.OfType<ProjectItem>().FirstOrDefault(x => x.Name == "tsUnit.ts");

                if (typescriptUnit == null)
                {
                    File.Copy(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + Path.DirectorySeparatorChar + "typescriptlibs" + Path.DirectorySeparatorChar + "tsUnit.ts", Path.GetDirectoryName(project.FullName) + Path.DirectorySeparatorChar + "tsUnit.ts");
                    ProjectItem typescriptUnitItem = project.ProjectItems.AddFromTemplate(Path.GetDirectoryName(project.FullName) + Path.DirectorySeparatorChar + "tsUnit.ts", "tsUnit.ts");
                    typescriptUnitItem.ProjectItems.AddFromTemplate(Path.GetDirectoryName(project.FullName) + Path.DirectorySeparatorChar + "tsUnit.ts", "tsUnit.js");
                }

                foreach (TypeScriptOutput file in output)
                {
                    Stack<string> module = this.SubModules(file.Module);
                    ProjectItems items = project.ProjectItems;

                    StringBuilder fullPath = new StringBuilder(Path.GetDirectoryName(project.FullName));

                    while (module.Count > 0)
                    {
                        string submodule = string.Format("{0}.Tests", module.Pop());
                        fullPath.Append(Path.DirectorySeparatorChar).Append(submodule);

                        if (!items.OfType<ProjectItem>().Any(x => x.Name == submodule))
                        {
                            ProjectItem folder = items.AddFolder(submodule);
                            items = folder.ProjectItems;
                        }
                        else
                        {
                            items = items.OfType<ProjectItem>().First(x => x.Name == submodule).ProjectItems;
                        }
                    }

                    string typescriptFileName = file.FileName + "Tests.ts";
                    string javascriptFileName = file.FileName + "Tests.js";

                    if (!items.OfType<ProjectItem>().Any(x => x.Name == typescriptFileName))
                    {
                        File.WriteAllText(fullPath.ToString() + Path.DirectorySeparatorChar + typescriptFileName, file.TestSyntax);
                        File.WriteAllText(fullPath.ToString() + Path.DirectorySeparatorChar + javascriptFileName, string.Empty);

                        ProjectItem item = items.AddFromTemplate(fullPath.ToString() + Path.DirectorySeparatorChar + typescriptFileName, typescriptFileName);

                        item.Properties.Item("ItemType").Value = "TypeScriptCompile";
                        item.ProjectItems.AddFromTemplate(fullPath.ToString() + Path.DirectorySeparatorChar + javascriptFileName, javascriptFileName);
                    }
                }
            }
        }

        /// <summary>
        /// Restores the expansion state of project items in a project
        /// </summary>
        /// <param name="expandState">The expansion state mappings</param>
        /// <param name="outputTo">The target project</param>
        private void RestoreSolutionExplorerState(Dictionary<string, bool> expandState, string outputTo)
        {
            UIHierarchy explorer = this.IDEInstance.ToolWindows.SolutionExplorer;
            UIHierarchyItem project = explorer.UIHierarchyItems.OfType<UIHierarchyItem>().First().UIHierarchyItems.OfType<UIHierarchyItem>().FirstOrDefault(x => x.Name == outputTo);

            if (project != null)
            {
                foreach (var uiitem in project.UIHierarchyItems.OfType<UIHierarchyItem>())
                {
                    if (expandState.ContainsKey(uiitem.Name))
                    {
                        uiitem.UIHierarchyItems.Expanded = expandState[uiitem.Name];
                    }

                    foreach (var childItem in uiitem.UIHierarchyItems.OfType<UIHierarchyItem>())
                    {
                        this.RestoreChildUIItemState(uiitem.Name + '/', childItem, expandState);
                    }
                }
            }
        }

        /// <summary>
        /// Restore the expand state of child project items
        /// </summary>
        /// <param name="path">The path</param>
        /// <param name="item">The item</param>
        /// <param name="expandState">Expansion state</param>
        private void RestoreChildUIItemState(string path, UIHierarchyItem item, Dictionary<string, bool> expandState)
        {
            if (expandState.ContainsKey(path + item.Name))
            {
                item.UIHierarchyItems.Expanded = expandState[path + item.Name];
            }

            foreach (var childItem in item.UIHierarchyItems.OfType<UIHierarchyItem>())
            {
                this.RestoreChildUIItemState(path + '/' + item.Name + '/', childItem, expandState);
            }
        }
    }
}
