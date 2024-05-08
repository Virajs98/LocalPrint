// Decompiled with JetBrains decompiler
// Type: RawPrint.NativeMethods
// Assembly: RawPrint, Version=0.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A470BAED-380A-4929-918B-3B2143B33FB1
// Assembly location: C:\Users\DELL\.nuget\packages\rawprint\0.5.0\lib\net40\RawPrint.dll

using System;
using System.Runtime.InteropServices;

#nullable disable
namespace RawPrint
{
  internal class NativeMethods
  {
    [DllImport("winspool.drv", SetLastError = true)]
    public static extern int ClosePrinter(IntPtr hPrinter);

    [DllImport("winspool.drv", CharSet = CharSet.Unicode, SetLastError = true)]
    public static extern int GetPrinterDriver(
      IntPtr hPrinter,
      string pEnvironment,
      int Level,
      IntPtr pDriverInfo,
      int cbBuf,
      ref int pcbNeeded);

    [DllImport("winspool.drv", CharSet = CharSet.Unicode, SetLastError = true)]
    public static extern uint StartDocPrinterW(IntPtr hPrinter, uint level, [MarshalAs(UnmanagedType.Struct)] ref DOC_INFO_1 di1);

    [DllImport("winspool.drv", CharSet = CharSet.Unicode, SetLastError = true)]
    public static extern int EndDocPrinter(IntPtr hPrinter);

    [DllImport("winspool.drv", CharSet = CharSet.Unicode, SetLastError = true)]
    public static extern int StartPagePrinter(IntPtr hPrinter);

    [DllImport("winspool.drv", CharSet = CharSet.Unicode, SetLastError = true)]
    public static extern int EndPagePrinter(IntPtr hPrinter);

    [DllImport("winspool.drv", CharSet = CharSet.Unicode, SetLastError = true)]
    public static extern int WritePrinter(
      IntPtr hPrinter,
      [In, Out] byte[] pBuf,
      int cbBuf,
      ref int pcWritten);

    [DllImport("winspool.drv", CharSet = CharSet.Unicode, SetLastError = true)]
    public static extern int OpenPrinterW(
      string pPrinterName,
      out IntPtr phPrinter,
      ref PRINTER_DEFAULTS pDefault);

    [DllImport("winspool.drv", EntryPoint = "SetJobA", SetLastError = true)]
    public static extern int SetJob(
      IntPtr hPrinter,
      uint JobId,
      uint Level,
      IntPtr pJob,
      uint Command_Renamed);
  }
}
