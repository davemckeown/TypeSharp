// Guids.cs
// MUST match guids.h
using System;

namespace HewlettPackardCompany.VSPackage1
{
    static class GuidList
    {
        public const string guidVSPackage1PkgString = "b5aea30c-8211-41fc-9e8d-4b40403ba8c2";
        public const string guidVSPackage1CmdSetString = "360b1ccd-07f7-4859-afbe-2d10ebee23a7";

        public static readonly Guid guidVSPackage1CmdSet = new Guid(guidVSPackage1CmdSetString);
    };
}