// Decompiled with JetBrains decompiler
// Type: RawPrint.PRINTER_ACCESS_MASK
// Assembly: RawPrint, Version=0.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A470BAED-380A-4929-918B-3B2143B33FB1
// Assembly location: C:\Users\DELL\.nuget\packages\rawprint\0.5.0\lib\net40\RawPrint.dll

using System;

#nullable disable
namespace RawPrint
{
  [Flags]
  internal enum PRINTER_ACCESS_MASK : uint
  {
    PRINTER_ACCESS_ADMINISTER = 4,
    PRINTER_ACCESS_USE = 8,
    PRINTER_ACCESS_MANAGE_LIMITED = 64, // 0x00000040
    PRINTER_ALL_ACCESS = 983052, // 0x000F000C
  }
}
