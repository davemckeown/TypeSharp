// Guids.cs
// MUST match guids.h
using System;

namespace HewlettPackardCompany.VSPackage2
{
    static class GuidList
    {
        public const string guidVSPackage2PkgString = "dbd86329-3a64-463a-b216-dc6b17a725d1";
        public const string guidVSPackage2CmdSetString = "471b4de1-0077-4dd1-9566-c37a0d136056";

        public static readonly Guid guidVSPackage2CmdSet = new Guid(guidVSPackage2CmdSetString);
    };
}