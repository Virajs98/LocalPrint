// Decompiled with JetBrains decompiler
// Type: RawPrint.DOC_INFO_1
// Assembly: RawPrint, Version=0.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A470BAED-380A-4929-918B-3B2143B33FB1
// Assembly location: C:\Users\DELL\.nuget\packages\rawprint\0.5.0\lib\net40\RawPrint.dll

using System.Runtime.InteropServices;

#nullable disable
namespace RawPrint
{
  [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
  internal struct DOC_INFO_1
  {
    public string pDocName;
    public string pOutputFile;
    public string pDataType;
  }
}
