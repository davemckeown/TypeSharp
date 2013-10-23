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
    using TypeSharp.Parser.Types;

    using TypeSharpParser;

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
            this.VisualStudioInstance = instance;
            this.Extension = extension;
            this.Model = new TypeSharpModel();
        }

        /// <summary>
        /// Gets or sets the reference to Visual Studio
        /// </summary>
        public DTE2 VisualStudioInstance { get; set; }

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
            UIHierarchy explorer = this.VisualStudioInstance.ToolWindows.SolutionExplorer;
            UIHierarchyItem project = explorer.UIHierarchyItems.OfType<UIHierarchyItem>().First().UIHierarchyItems.OfType<UIHierarchyItem>().FirstOrDefault(x => x.Name == outputTo);

            Dictionary<string, bool> expandState = new Dictionary<string, bool>();

            if (project == null)
            {
                return expandState;
            }

            foreach (var uiHierarchyItem in project.UIHierarchyItems.OfType<UIHierarchyItem>())
            {
                expandState.Add(uiHierarchyItem.Name, uiHierarchyItem.UIHierarchyItems.Expanded);

                foreach (var childItem in uiHierarchyItem.UIHierarchyItems.OfType<UIHierarchyItem>())
                {
                    this.PersistChildUIItemState(uiHierarchyItem.Name + '/', childItem, expandState);
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
        public void BuildEvents_OnBuildBegin(vsBuildScope scope, vsBuildAction action)
        {
            TypeSharpProject project = new TypeSharpProject(this.VisualStudioInstance.Solution.FullName);

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
        public void BuildEvents_OnBuildDone(vsBuildScope scope, vsBuildAction action)
        {
                System.Threading.Tasks.Task.Factory.StartNew(() =>
                {
                    object icon = null;

                    try
                    {
                        TypeSharpProject project = new TypeSharpProject(this.VisualStudioInstance.Solution.FullName);

                        if (this.Model.BuildResults.Any(x => x != true) || string.IsNullOrEmpty(project.OutputProject))
                        {
                            return;
                        }

                        icon = (short)Microsoft.VisualStudio.Shell.Interop.Constants.SBAI_Build;

                        this.VisualStudioInstance.StatusBar.Text = "Building TypeSharp Sources...";
                        this.VisualStudioInstance.StatusBar.Animate(true, icon);

                        TypeSharpProject settings = new TypeSharpProject(this.VisualStudioInstance.Solution.FullName);
                        string outputProject = settings.OutputProject;

                        TypeScriptGenerator sources = new TypeScriptGenerator(this.Model.SourceFiles.ToList());
                        List<TypeScriptOutput> output = sources.GenerateOutputFiles();
                        Dictionary<string, TypeScriptReferenceOutput> references = sources.GenerateReferenceOutputFiles(output);

                        IEnumerable<string> modules = output.Select(x => x.Module).Distinct().Select(x => x.Contains('.') ? x.Substring(0, x.IndexOf('.')) : x).Distinct();

                        Dictionary<string, bool> expandState = this.PersistSolutionExplorerState(outputProject);
                        this.UnlinkExistingProjectFiles(modules, outputProject);
                        this.SyncProjectFiles(output, references, outputProject);
                        this.SyncNamespaces(output, references, outputProject);

                        if (settings.CreateTestClasses)
                        {
                            this.SyncProjectTestFiles(output.Where(x => x.IsClass).ToList(), outputProject);
                        }

                        this.RestoreSolutionExplorerState(expandState, outputProject);

                        this.VisualStudioInstance.StatusBar.Text = "Build Succeeded";
                    }
                    catch (Exception ex)
                    {
                        Debug.Assert(false, ex.ToString());
                        VisualStudioInstance.StatusBar.Text = "TypeSharp Build Failed";
                    }
                    finally
                    {
                        if (icon != null)
                        {
                            VisualStudioInstance.StatusBar.Animate(false, icon);
                        }
                    }
                });
        }

        /// <summary>
        /// On project built event handler
        /// </summary>
        /// <param name="project">The project</param>
        /// <param name="projectConfig">The configuration</param>
        /// <param name="platform">The platform</param>
        /// <param name="solutionConfig">The solution configuration</param>
        /// <param name="success">Was successful flag</param>
        public void BuildEvents_OnBuildProjectConfigDone(string project, string projectConfig, string platform, string solutionConfig, bool success)
        {
            this.Model.BuildResults.Add(success);
        }

        /// <summary>
        /// Before solution closing event handler
        /// </summary>
        public void SolutionEvents_BeforeClosing()
        {
            TypeSharpProject project = new TypeSharpProject(this.VisualStudioInstance.Solution.FullName);

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
            TypeSharpProject project = new TypeSharpProject(this.VisualStudioInstance.Solution.FullName);

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
        /// Determines the sub modules of a module
        /// </summary>
        /// <param name="module">The module</param>
        /// <returns>Stack of sub modules</returns>
        private static Stack<string> SubModules(string module)
        {
            Stack<string> subModules = new Stack<string>();

            if (module.Contains('.'))
            {
                while (module.Contains('.'))
                {
                    int index = module.LastIndexOf('.');
                    string subModule = module.Substring(index + 1);
                    subModules.Push(subModule);
                    module = module.Substring(0, index);
                }

                subModules.Push(module);
            }
            else
            {
                subModules.Push(module);
            }

            return subModules;
        }

        /// <summary>
        /// Synchronizes references between namespaces
        /// </summary>
        /// <param name="output">The TypeScript output</param>
        /// <param name="references">A lookup of TypeScriptReferenceOutputs</param>
        /// <param name="outputTo">The location to output to</param>
        private void SyncNamespaces(List<TypeScriptOutput> output, Dictionary<string, TypeScriptReferenceOutput> references, string outputTo)
        {
            Project project = this.VisualStudioInstance.Solution.Projects.OfType<Project>().FirstOrDefault(x => x.Name == outputTo);

            if (project != null)
            {
                string fullNamespacePath = Path.GetDirectoryName(project.FullName) + Path.DirectorySeparatorChar + "Namespaces";

                if (Directory.Exists(fullNamespacePath))
                {
                    Directory.Delete(fullNamespacePath, true);
                }

                foreach (string assembly in output.Select(x => x.Module).Distinct())
                {
                    if (!File.Exists(Path.GetDirectoryName(project.FullName) + Path.DirectorySeparatorChar + assembly + ".ts"))
                    {
                        File.Create(Path.GetDirectoryName(project.FullName) + Path.DirectorySeparatorChar + assembly + ".ts");
                    }

                    if (!File.Exists(Path.GetDirectoryName(project.FullName) + Path.DirectorySeparatorChar + assembly + ".js"))
                    {
                        File.Create(Path.GetDirectoryName(project.FullName) + Path.DirectorySeparatorChar + assembly + ".js");
                    }

                    ProjectItems items = project.ProjectItems;

                    if (items.OfType<ProjectItem>().All(x => x.Name != "Namespaces"))
                    {
                        ProjectItem folder = items.AddFolder("Namespaces");
                        items = folder.ProjectItems;
                    }
                    else
                    {
                        items = items.OfType<ProjectItem>().First(x => x.Name == "Namespaces").ProjectItems;
                    }

                    string typescriptFileName = assembly + ".ts";
                    string javascriptFileName = assembly + ".js";

                    if (items.OfType<ProjectItem>().Any(x => x.Name == typescriptFileName))
                    {
                        continue;
                    }

                    File.WriteAllText(fullNamespacePath + Path.DirectorySeparatorChar + typescriptFileName, string.Empty);
                    File.WriteAllText(fullNamespacePath + Path.DirectorySeparatorChar + javascriptFileName, string.Empty);

                    ProjectItem item = items.AddFromTemplate(fullNamespacePath + Path.DirectorySeparatorChar + typescriptFileName, typescriptFileName);

                    item.Properties.Item("ItemType").Value = "TypeScriptCompile";
                    item.ProjectItems.AddFromTemplate(fullNamespacePath + Path.DirectorySeparatorChar + javascriptFileName, javascriptFileName);
                }

                foreach (TypeScriptOutput file in output)
                {
                    Stack<string> module = SubModules(file.Module);
                    ProjectItems items = project.ProjectItems;

                    StringBuilder fullPath = new StringBuilder(Path.GetDirectoryName(project.FullName));

                    while (module.Count > 0)
                    {
                        string subModule = module.Pop();
                        fullPath.Append(Path.DirectorySeparatorChar).Append(subModule);

                        if (items.OfType<ProjectItem>().All(x => x.Name != subModule))
                        {
                            ProjectItem folder = items.AddFolder(subModule);
                            items = folder.ProjectItems;
                        }
                        else
                        {
                            items = items.OfType<ProjectItem>().First(x => x.Name == subModule).ProjectItems;
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

                    if (items.OfType<ProjectItem>().All(x => x.Name != typescriptFileName))
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
        /// Scans a solution for TypeSharp source files
        /// </summary>
        private void ScanForTypeSharpFiles()
        {
            List<string> paths = new List<string>();
            this.Model.UserProjects = new List<Project>();

            try
            {
                foreach (Project project in this.VisualStudioInstance.Solution.Projects)
                {
                    this.Model.UserProjects.Add(project);

                    foreach (ProjectItem item in this.Model.AllProjectItems(project))
                    {
                        if (item.Properties == null)
                        {
                            continue;
                        }

                        Property prop = item.Properties.Item("FullPath");
                        object value = null;

                        if (prop != null)
                        {
                            value = prop.Value;
                        }

                        if (value == null)
                        {
                            continue;
                        }

                        string path = value.ToString();

                        if (path.EndsWith("cs", StringComparison.InvariantCultureIgnoreCase))
                        {
                            paths.Add(path);
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
        /// Removes the existing files from the project so that they can be regenerated without "reload file" warnings in visual studio
        /// </summary>
        /// <param name="modules">The modules to generate</param>
        /// <param name="outputTo">The output project</param>
        private void UnlinkExistingProjectFiles(IEnumerable<string> modules, string outputTo)
        {
            Project project = this.VisualStudioInstance.Solution.Projects.OfType<Project>().FirstOrDefault(x => x.Name == outputTo);

            if (project == null)
            {
                return;
            }

            string location = Path.GetDirectoryName(project.FullName) + Path.DirectorySeparatorChar;

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

        /// <summary>
        /// Syncs the project items with the output of the TypeSharp compiler
        /// </summary>
        /// <param name="output">The TypeScriptOutput files</param>
        /// <param name="references">The TypeScript references</param>
        /// <param name="outputTo">The target project</param>
        private void SyncProjectFiles(IEnumerable<TypeScriptOutput> output, Dictionary<string, TypeScriptReferenceOutput> references, string outputTo)
        {
            Project project = this.VisualStudioInstance.Solution.Projects.OfType<Project>().FirstOrDefault(x => x.Name == outputTo);

            if (project == null)
            {
                return;
            }

            foreach (TypeScriptOutput file in output)
            {
                Stack<string> module = SubModules(file.Module);
                ProjectItems items = project.ProjectItems;

                StringBuilder fullPath = new StringBuilder(Path.GetDirectoryName(project.FullName));

                while (module.Count > 0)
                {
                    string subModule = module.Pop();
                    fullPath.Append(Path.DirectorySeparatorChar).Append(subModule);

                    if (items.OfType<ProjectItem>().All(x => x.Name != subModule))
                    {
                        ProjectItem folder = items.AddFolder(subModule);
                        items = folder.ProjectItems;
                    }
                    else
                    {
                        items = items.OfType<ProjectItem>().First(x => x.Name == subModule).ProjectItems;
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

                if (items.OfType<ProjectItem>().Any(x => x.Name == typescriptFileName))
                {
                    continue;
                }

                File.WriteAllText(fullPath.ToString() + Path.DirectorySeparatorChar + typescriptFileName, file.Syntax);
                File.WriteAllText(fullPath.ToString() + Path.DirectorySeparatorChar + javascriptFileName, string.Empty);

                ProjectItem item = items.AddFromTemplate(fullPath.ToString() + Path.DirectorySeparatorChar + typescriptFileName, typescriptFileName);

                item.Properties.Item("ItemType").Value = "TypeScriptCompile";
                item.ProjectItems.AddFromTemplate(fullPath.ToString() + Path.DirectorySeparatorChar + javascriptFileName, javascriptFileName);
            }
        }

        /// <summary>
        /// Creates the test classes in the proper module namespace
        /// </summary>
        /// <param name="output">The TypeScriptOutput files</param>
        /// <param name="outputTo">The target project</param>
        private void SyncProjectTestFiles(IEnumerable<TypeScriptOutput> output, string outputTo)
        {
            Project project = this.VisualStudioInstance.Solution.Projects.OfType<Project>().FirstOrDefault(x => x.Name == outputTo);

            if (project == null)
            {
                return;
            }

            ProjectItem typescriptUnit = project.ProjectItems.OfType<ProjectItem>().FirstOrDefault(x => x.Name == "tsUnit.ts");

            if (typescriptUnit == null)
            {
                File.Copy(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + Path.DirectorySeparatorChar + "typescriptlibs" + Path.DirectorySeparatorChar + "tsUnit.ts", Path.GetDirectoryName(project.FullName) + Path.DirectorySeparatorChar + "tsUnit.ts");
                ProjectItem typescriptUnitItem = project.ProjectItems.AddFromTemplate(Path.GetDirectoryName(project.FullName) + Path.DirectorySeparatorChar + "tsUnit.ts", "tsUnit.ts");
                typescriptUnitItem.ProjectItems.AddFromTemplate(Path.GetDirectoryName(project.FullName) + Path.DirectorySeparatorChar + "tsUnit.ts", "tsUnit.js");
            }

            foreach (TypeScriptOutput file in output)
            {
                Stack<string> module = SubModules(file.Module);
                ProjectItems items = project.ProjectItems;

                StringBuilder fullPath = new StringBuilder(Path.GetDirectoryName(project.FullName));

                while (module.Count > 0)
                {
                    string subModule = string.Format("{0}.Tests", module.Pop());
                    fullPath.Append(Path.DirectorySeparatorChar).Append(subModule);

                    if (items.OfType<ProjectItem>().All(x => x.Name != subModule))
                    {
                        ProjectItem folder = items.AddFolder(subModule);
                        items = folder.ProjectItems;
                    }
                    else
                    {
                        items = items.OfType<ProjectItem>().First(x => x.Name == subModule).ProjectItems;
                    }
                }

                string typescriptFileName = file.FileName + "Tests.ts";
                string javascriptFileName = file.FileName + "Tests.js";

                if (items.OfType<ProjectItem>().Any(x => x.Name == typescriptFileName))
                {
                    continue;
                }

                File.WriteAllText(fullPath.ToString() + Path.DirectorySeparatorChar + typescriptFileName, file.TestSyntax);
                File.WriteAllText(fullPath.ToString() + Path.DirectorySeparatorChar + javascriptFileName, string.Empty);

                ProjectItem item = items.AddFromTemplate(fullPath.ToString() + Path.DirectorySeparatorChar + typescriptFileName, typescriptFileName);

                item.Properties.Item("ItemType").Value = "TypeScriptCompile";
                item.ProjectItems.AddFromTemplate(fullPath.ToString() + Path.DirectorySeparatorChar + javascriptFileName, javascriptFileName);
            }
        }

        /// <summary>
        /// Restores the expansion state of project items in a project
        /// </summary>
        /// <param name="expandState">The expansion state mappings</param>
        /// <param name="outputTo">The target project</param>
        private void RestoreSolutionExplorerState(Dictionary<string, bool> expandState, string outputTo)
        {
            UIHierarchy explorer = this.VisualStudioInstance.ToolWindows.SolutionExplorer;
            UIHierarchyItem project = explorer.UIHierarchyItems.OfType<UIHierarchyItem>().First().UIHierarchyItems.OfType<UIHierarchyItem>().FirstOrDefault(x => x.Name == outputTo);

            if (project != null)
            {
                foreach (var uiHierarchyItem in project.UIHierarchyItems.OfType<UIHierarchyItem>())
                {
                    if (expandState.ContainsKey(uiHierarchyItem.Name))
                    {
                        uiHierarchyItem.UIHierarchyItems.Expanded = expandState[uiHierarchyItem.Name];
                    }

                    foreach (var childItem in uiHierarchyItem.UIHierarchyItems.OfType<UIHierarchyItem>())
                    {
                        this.RestoreChildUIItemState(uiHierarchyItem.Name + '/', childItem, expandState);
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
