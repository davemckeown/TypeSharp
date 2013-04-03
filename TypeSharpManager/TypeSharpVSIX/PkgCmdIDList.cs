// <copyright file="PkgCmdIDList.cs" company="TypeSharp Project">
//     Apache 2.0 License
// </copyright>
// <author>Dave McKeown</author>

namespace TypeSharp.VisualStudioExtension
{
    // PkgCmdID.cs
    // MUST match PkgCmdID.h
    using System;

    /// <summary>
    /// Package Command ID List
    /// </summary>
    public static class PkgCmdIDList
    {
        /// <summary>
        /// TypeSharp CMD Id
        /// </summary>
        public const uint CmdidTypeSharp = 0x100;

        /// <summary>
        /// TypeSharpTool CMD Id
        /// </summary>
        public const uint CmdidTypeSharpTool = 0x101;
    }
}