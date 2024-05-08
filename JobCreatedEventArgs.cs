// Decompiled with JetBrains decompiler
// Type: RawPrint.JobCreatedEventArgs
// Assembly: RawPrint, Version=0.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A470BAED-380A-4929-918B-3B2143B33FB1
// Assembly location: C:\Users\DELL\.nuget\packages\rawprint\0.5.0\lib\net40\RawPrint.dll

using System;

#nullable disable
namespace RawPrint
{
  public class JobCreatedEventArgs : EventArgs
  {
    public uint Id { get; set; }

    public string PrinterName { get; set; }
  }
}
