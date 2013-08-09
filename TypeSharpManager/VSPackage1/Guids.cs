// Guids.cs
// MUST match guids.h
using System;

namespace Company.VSPackage1
{
    static class GuidList
    {
        public const string guidVSPackage1PkgString = "b43ee811-617e-4b08-a254-84648833586d";
        public const string guidVSPackage1CmdSetString = "5b2d0e92-768c-4dfb-bd79-38722596d264";

        public static readonly Guid guidVSPackage1CmdSet = new Guid(guidVSPackage1CmdSetString);
    };
}