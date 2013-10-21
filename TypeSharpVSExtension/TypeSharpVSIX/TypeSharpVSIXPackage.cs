// <copyright file="TypeSharpVSIXPackage.cs" company="TypeSharp Project">
//     Apache 2.0 License
// </copyright>
// <author>Dave McKeown</author>

namespace TypeSharp.VisualStudioExtension
{
    using System;
    using System.ComponentModel.Design;
    using System.IO;
    using System.Runtime.InteropServices;
    using EnvDTE80;
    using Microsoft.VisualStudio.Shell;
    using Microsoft.VisualStudio.Shell.Interop;
    using TypeSharp.VisualStudioExtension.Controls;

    /// <summary>
    /// This is the class that implements the package exposed by this assembly.
    /// The minimum requirement for a class to be considered a valid package for Visual Studio
    /// is to implement the IVsPackage interface and register itself with the shell.
    /// This package uses the helper classes defined inside the Managed Package Framework (MPF)
    /// to do it: it derives from the Package class that provides the implementation of the 
    /// IVsPackage interface and uses the registration attributes defined in the framework to 
    /// register itself and its components with the shell.
    /// </summary>
    //// This attribute tells the PkgDef creation utility (CreatePkgDef.exe) that this class is a package
    [PackageRegistration(UseManagedResourcesOnly = true)]
    //// This attribute is used to register the information needed to show this package
    //// in the Help/About dialog of Visual Studio.
    [InstalledProductRegistration("#110", "#112", "1.0", IconResourceID = 400)]
    //// This attribute is needed to let the shell know that this package exposes some menus.
    [ProvideMenuResource("Menus.ctmenu", 1)]
    //// This attribute registers a tool window exposed by this package.
    [ProvideToolWindow(typeof(TypeSharpDockWindow))]
    [ProvideAutoLoad(Microsoft.VisualStudio.Shell.Interop.UIContextGuids80.SolutionExists)]
    [Guid(GuidList.GuidTypeSharpVSIXPkgString)]
    public sealed class TypeSharpVSIXPackage : Package
    {
        /// <summary>
        /// Initializes a new instance of the TypeSharpVSIXPackage class
        /// </summary>
        public TypeSharpVSIXPackage()
        {
            this.IDEInstance = Package.GetGlobalService(typeof(SDTE)) as DTE2;
            this.Controller = new TypeSharpController(this.IDEInstance, this);

            this.IDEInstance.Events.BuildEvents.OnBuildBegin += this.Controller.BuildEvents_OnBuildBegin;
            this.IDEInstance.Events.BuildEvents.OnBuildDone += this.Controller.BuildEvents_OnBuildDone;
            this.IDEInstance.Events.BuildEvents.OnBuildProjConfigDone += this.Controller.BuildEvents_OnBuildProjConfigDone;
            this.IDEInstance.Events.SolutionEvents.Opened += this.Controller.SolutionEvents_Opened;
            this.IDEInstance.Events.SolutionEvents.AfterClosing += this.Controller.SolutionEvents_AfterClosing;
            this.IDEInstance.Events.SolutionEvents.BeforeClosing += this.Controller.SolutionEvents_BeforeClosing;
        }

        /// <summary>
        /// Gets or sets a reference to the Visual Studio instance
        /// </summary>
        public DTE2 IDEInstance { get; set; }

        /// <summary>
        /// Gets or sets a reference to the Controller
        /// </summary>
        public TypeSharpController Controller { get; set; }

        /// <summary>
        /// Initialization of the package; this method is called right after the package is sited, so this is the place
        /// where you can put all the initialization code that rely on services provided by VisualStudio.
        /// </summary>
        protected override void Initialize()
        {
            base.Initialize();

            // Add our command handlers for menu (commands must exist in the .vsct file)
            OleMenuCommandService mcs = GetService(typeof(IMenuCommandService)) as OleMenuCommandService;
            if (null != mcs)
            {
                // Create the command for the menu item.
                CommandID menuCommandID = new CommandID(GuidList.GuidTypeSharpVSIXCmdSet, (int)PkgCmdIDList.CmdidTypeSharp);
                MenuCommand menuItem = new MenuCommand(this.MenuItemCallback, menuCommandID);
                mcs.AddCommand(menuItem);

                // Create the command for the tool window
                CommandID toolwndCommandID = new CommandID(GuidList.GuidTypeSharpVSIXCmdSet, (int)PkgCmdIDList.CmdidTypeSharpTool);
                MenuCommand menuToolWin = new MenuCommand(this.ShowToolWindow, toolwndCommandID);
                mcs.AddCommand(menuToolWin);

                this.Controller.ToolMenuItem = menuItem;
                this.Controller.ToolViewItem = menuToolWin;
            }

            string tempPath = string.Format("{0}{1}{2}", Path.GetTempPath(), Path.DirectorySeparatorChar, "TypeSharp");

            if (Directory.Exists(tempPath))
            {
                Directory.CreateDirectory(tempPath);
            }
        }

        /// <summary>
        /// This function is called when the user clicks the menu item that shows the 
        /// tool window. See the Initialize method to see how the menu item is associated to 
        /// this function using the OleMenuCommandService service and the MenuCommand class.
        /// </summary>
        /// <param name="sender">Sending object</param>
        /// <param name="e">Event arguments</param>
        private void ShowToolWindow(object sender, EventArgs e)
        {
            // Get the instance number 0 of this tool window. This window is single instance so this instance
            // is actually the only one.
            // The last flag is set to true so that if the tool window does not exists it will be created.
            this.Controller.ToolPaneWindow = this.FindToolWindow(typeof(TypeSharpDockWindow), 0, true);
            if ((null == this.Controller.ToolPaneWindow) || (null == this.Controller.ToolPaneWindow.Frame))
            {
                throw new NotSupportedException(Resources.CanNotCreateWindow);
            }

            IVsWindowFrame windowFrame = (IVsWindowFrame)this.Controller.ToolPaneWindow.Frame;
            Microsoft.VisualStudio.ErrorHandler.ThrowOnFailure(windowFrame.Show());
        }

        /// <summary>
        /// This function is the callback used to execute a command when the a menu item is clicked.
        /// See the Initialize method to see how the menu item is associated to this function using
        /// the OleMenuCommandService service and the MenuCommand class.
        /// </summary>
        /// <param name="sender">Sending object</param>
        /// <param name="e">Event arguments</param>
        private void MenuItemCallback(object sender, EventArgs e)
        {
            ProjectConfiguration options = new ProjectConfiguration(this.IDEInstance);

            options.ShowDialog();
        }
    }
}
