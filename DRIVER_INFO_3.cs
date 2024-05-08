// Decompiled with JetBrains decompiler
// Type: RawPrint.DRIVER_INFO_3
// Assembly: RawPrint, Version=0.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A470BAED-380A-4929-918B-3B2143B33FB1
// Assembly location: C:\Users\DELL\.nuget\packages\rawprint\0.5.0\lib\net40\RawPrint.dll

using System;
using System.Runtime.InteropServices;

#nullable disable
namespace RawPrint
{
  [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
  internal struct DRIVER_INFO_3
  {
    public uint cVersion;
    public string pName;
    public string pEnvironment;
    public string pDriverPath;
    public string pDataFile;
    public string pConfigFile;
    public string pHelpFile;
    public IntPtr pDependentFiles;
    public string pMonitorName;
    public string pDefaultDataType;
  }
}
