//// <copyright file="TestRunner.cs" company="TypeSharp Project">
////     Apache 2.0 License
//// </copyright>
//// <author>Dave McKeown</author>

//using System.Diagnostics;
//using System.IO;
//using TypeSharp.Bridge.Node;

//namespace TypeSharp.Core
//{
//    /// <summary>
//    /// TestRunner manages TypeScript automation tests
//    /// </summary>
//    public class TestRunner
//    {

//        public NodeBroker NodeBroker { get; private set; }

//        public delegate void TestResult(string result);

//        public event TestResult OnTestResult = delegate { };

//        /// <summary>
//        /// Initializes a new instance of the TestRunner class
//        /// </summary>
//        /// <param name="context">The base path of the TypeSharp extension</param>
//        public TestRunner(string context)
//        {
//            this.ContextPath = context;
//            NodeBroker = new NodeBroker(context);

//            NodeBroker.OnNodeResult += NodeBroker_OnNodeResult;
//        }

//        void NodeBroker_OnNodeResult(object sender, DataReceivedEventArgs e)
//        {
//            OnTestResult(e.Data);
//        }

//        /// <summary>
//        /// Gets or sets the base path of the TypeSharp extension
//        /// </summary>
//        public string ContextPath { get; set; }

//        public void SendCommand()
//        {
//            NodeBroker.Node.StandardInput.Write("HelloWorld();");
//        }
//    }
//}
