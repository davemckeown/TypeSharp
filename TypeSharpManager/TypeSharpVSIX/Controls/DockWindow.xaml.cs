// <copyright file="DockWindow.xaml.cs" company="TypeSharp Project">
//     Apache 2.0 License
// </copyright>
// <author>Dave McKeown</author>

namespace TypeSharp.VisualStudioExtension.Controls
{
    using System;
    using System.Diagnostics;
    using System.Reflection;
    using System.Windows;
    using System.Windows.Controls;
    using TypeSharp.Core;

    /// <summary>
    /// DockWindow Implementation
    /// </summary>
    public partial class DockWindow : UserControl
    {
        //TestRunner nodeRunner = new TestRunner(System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));

        /// <summary>
        /// Initializes a new instance of the DockWindow class
        /// </summary>
        public DockWindow()
        {
            this.InitializeComponent();

            //nodeRunner.OnTestResult += nodeRunner_OnTestResult;
        }

        void nodeRunner_OnTestResult(string result)
        {

            Dispatcher.BeginInvoke((Action)(() =>
            {
                MessageBox.Show(Window.GetWindow(this), result);
            }));
        }

        /// <summary>
        /// Node button click handler
        /// </summary>
        /// <param name="sender">Node button</param>
        /// <param name="e">Event arguments</param>
        private void Node_Click(object sender, RoutedEventArgs e)
        {
            //ProcessStartInfo node = new ProcessStartInfo(System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + System.IO.Path.DirectorySeparatorChar + "runtimes" + System.IO.Path.DirectorySeparatorChar + "node-v0.10.2" + System.IO.Path.DirectorySeparatorChar + "node.exe");
            //node.UseShellExecute = false;

            //System.Diagnostics.Process.Start(node);

            //nodeRunner.SendCommand();
        }

        /// <summary>
        /// Phantom button click handler
        /// </summary>
        /// <param name="sender">Phantom button</param>
        /// <param name="e">Event arguments</param>
        private void Phantom_Click(object sender, RoutedEventArgs e)
        {
            ProcessStartInfo phantom = new ProcessStartInfo(System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + System.IO.Path.DirectorySeparatorChar + "runtimes" + System.IO.Path.DirectorySeparatorChar + "phantomjs-1.9.0" + System.IO.Path.DirectorySeparatorChar + "phantomjs.exe");
            phantom.UseShellExecute = false;

            System.Diagnostics.Process.Start(phantom);
        }
    }
}