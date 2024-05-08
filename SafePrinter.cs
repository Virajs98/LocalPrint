// Decompiled with JetBrains decompiler
// Type: RawPrint.SafePrinter
// Assembly: RawPrint, Version=0.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A470BAED-380A-4929-918B-3B2143B33FB1
// Assembly location: C:\Users\DELL\.nuget\packages\rawprint\0.5.0\lib\net40\RawPrint.dll

using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

#nullable disable
namespace RawPrint
{
  internal class SafePrinter : SafeHandleZeroOrMinusOneIsInvalid
  {
    private SafePrinter(IntPtr hPrinter)
      : base(true)
    {
      this.handle = hPrinter;
    }

    protected override bool ReleaseHandle()
    {
      if (this.IsInvalid)
        return false;
      int num = NativeMethods.ClosePrinter(this.handle) != 0 ? 1 : 0;
      this.handle = IntPtr.Zero;
      return num != 0;
    }

    public uint StartDocPrinter(DOC_INFO_1 di1)
    {
      int num = (int) NativeMethods.StartDocPrinterW(this.handle, 1U, ref di1);
      if (num != 0)
        return (uint) num;
      if (Marshal.GetLastWin32Error() == 1804)
        throw new Exception("The specified datatype is invalid, try setting 'Enable advanced printing features' in printer properties.", (Exception) new Win32Exception());
      throw new Win32Exception();
    }

    public void EndDocPrinter()
    {
      if (NativeMethods.EndDocPrinter(this.handle) == 0)
        throw new Win32Exception();
    }

    public void StartPagePrinter()
    {
      if (NativeMethods.StartPagePrinter(this.handle) == 0)
        throw new Win32Exception();
    }

    public void EndPagePrinter()
    {
      if (NativeMethods.EndPagePrinter(this.handle) == 0)
        throw new Win32Exception();
    }

    public void WritePrinter(byte[] buffer, int size)
    {
      int pcWritten = 0;
      if (NativeMethods.WritePrinter(this.handle, buffer, size, ref pcWritten) == 0)
        throw new Win32Exception(Marshal.GetLastWin32Error());
    }

    public IEnumerable<string> GetPrinterDriverDependentFiles()
    {
      int pcbNeeded = 0;
      if (NativeMethods.GetPrinterDriver(this.handle, (string) null, 3, IntPtr.Zero, 0, ref pcbNeeded) != 0 || Marshal.GetLastWin32Error() != 122)
        throw new Win32Exception();
      IntPtr num = Marshal.AllocHGlobal(pcbNeeded);
      try
      {
        if (NativeMethods.GetPrinterDriver(this.handle, (string) null, 3, num, pcbNeeded, ref pcbNeeded) == 0)
          throw new Win32Exception();
        return (IEnumerable<string>) SafePrinter.ReadMultiSz(((DRIVER_INFO_3) Marshal.PtrToStructure(num, typeof (DRIVER_INFO_3))).pDependentFiles).ToList<string>();
      }
      finally
      {
        Marshal.FreeHGlobal(num);
      }
    }

    private static IEnumerable<string> ReadMultiSz(IntPtr ptr)
    {
      if (!(ptr == IntPtr.Zero))
      {
        StringBuilder stringBuilder = new StringBuilder();
        IntPtr pos = ptr;
        while (true)
        {
          char ch = (char) Marshal.ReadInt16(pos);
          if (ch == char.MinValue)
          {
            if (stringBuilder.Length != 0)
            {
              yield return stringBuilder.ToString();
              stringBuilder = new StringBuilder();
            }
            else
              break;
          }
          else
            stringBuilder.Append(ch);
          pos += 2;
        }
      }
    }

    public static SafePrinter OpenPrinter(string printerName, ref PRINTER_DEFAULTS defaults)
    {
      IntPtr phPrinter;
      if (NativeMethods.OpenPrinterW(printerName, out phPrinter, ref defaults) == 0)
        throw new Win32Exception();
      return new SafePrinter(phPrinter);
    }
  }
}
