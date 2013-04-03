using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace TypeSharp.Bridge.Node
{
    public class NodeBroker
    {

        private string context;

        private Process node;

        public event EventHandler<DataReceivedEventArgs> OnNodeResult = delegate { };

        public Process Node
        {
            get
            {
                if (node == null)
                {
                    string script = string.Empty;
                    ProcessStartInfo nodeStart = new ProcessStartInfo(this.context + Path.DirectorySeparatorChar + "runtimes" + Path.DirectorySeparatorChar + "node-v0.10.2" + Path.DirectorySeparatorChar + "node.exe");
                    nodeStart.UseShellExecute = false;
                    nodeStart.RedirectStandardInput = true;
                    nodeStart.RedirectStandardOutput = true;
                    nodeStart.RedirectStandardError = true;
                    nodeStart.CreateNoWindow = true;
                    nodeStart.Arguments = string.Format("{0}", this.context + Path.DirectorySeparatorChar + "runtimes" + Path.DirectorySeparatorChar + "interop" + Path.DirectorySeparatorChar + "NodeRuntimeBridge.js");

                    //script = File.ReadAllText(this.context + Path.DirectorySeparatorChar + "runtimes" + Path.DirectorySeparatorChar + "interop" + Path.DirectorySeparatorChar + "NodeBroker.js");

                   
                    node = Process.Start(nodeStart);
                    node.BeginOutputReadLine();
                    node.StandardInput.AutoFlush = true;

                    node.OutputDataReceived += node_OutputDataReceived;
                    node.ErrorDataReceived += node_ErrorDataReceived;


                }

                return node;
            }
        }

        void node_ErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            OnNodeResult(sender, e);
        }

        void node_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            OnNodeResult(sender, e);
        }

        public NodeBroker(string context)
        {
            this.context = context;
        }


    }
}