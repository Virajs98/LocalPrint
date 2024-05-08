// Decompiled with JetBrains decompiler
// Type: RawPrint.Printer
// Assembly: RawPrint, Version=0.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A470BAED-380A-4929-918B-3B2143B33FB1
// Assembly location: C:\Users\DELL\.nuget\packages\rawprint\0.5.0\lib\net40\RawPrint.dll

using System;
using System.IO;
using System.Linq;

#nullable disable
namespace RawPrint
{
  public class Printer : IPrinter
  {
    public event JobCreatedHandler OnJobCreated;

    public void PrintRawFile(string printer, string path, bool paused)
    {
      this.PrintRawFile(printer, path, path, paused);
    }

    public void PrintRawFile(string printer, string path, string documentName, bool paused)
    {
      using (FileStream fileStream = File.OpenRead(path))
        this.PrintRawStream(printer, (Stream) fileStream, documentName, paused);
    }

    public void PrintRawStream(string printer, Stream stream, string documentName, bool paused)
    {
      this.PrintRawStream(printer, stream, documentName, paused, 1);
    }

    public void PrintRawStream(
      string printer,
      Stream stream,
      string documentName,
      bool paused,
      int pagecount)
    {
      PRINTER_DEFAULTS defaults = new PRINTER_DEFAULTS()
      {
        DesiredPrinterAccess = PRINTER_ACCESS_MASK.PRINTER_ACCESS_USE
      };
      using (SafePrinter printer1 = SafePrinter.OpenPrinter(printer, ref defaults))
        this.DocPrinter(printer1, documentName, Printer.IsXPSDriver(printer1) ? "XPS_PASS" : "RAW", stream, paused, pagecount, printer);
    }

    private static bool IsXPSDriver(SafePrinter printer)
    {
      return printer.GetPrinterDriverDependentFiles().Any<string>((Func<string, bool>) (f => f.EndsWith("pipelineconfig.xml", StringComparison.InvariantCultureIgnoreCase)));
    }

    private void DocPrinter(
      SafePrinter printer,
      string documentName,
      string dataType,
      Stream stream,
      bool paused,
      int pagecount,
      string printerName)
    {
      DOC_INFO_1 di1 = new DOC_INFO_1()
      {
        pDataType = dataType,
        pDocName = documentName
      };
      uint JobId = printer.StartDocPrinter(di1);
      if (paused)
        NativeMethods.SetJob(printer.DangerousGetHandle(), JobId, 0U, IntPtr.Zero, 1U);
      JobCreatedHandler onJobCreated = this.OnJobCreated;
      if (onJobCreated != null)
        onJobCreated((object) this, new JobCreatedEventArgs()
        {
          Id = JobId,
          PrinterName = printerName
        });
      try
      {
        Printer.PagePrinter(printer, stream, pagecount);
      }
      finally
      {
        printer.EndDocPrinter();
      }
    }

    private static void PagePrinter(SafePrinter printer, Stream stream, int pagecount)
    {
      printer.StartPagePrinter();
      try
      {
        Printer.WritePrinter(printer, stream);
      }
      finally
      {
        printer.EndPagePrinter();
      }
      for (int index = 1; index < pagecount; ++index)
      {
        printer.StartPagePrinter();
        printer.EndPagePrinter();
      }
    }

    private static void WritePrinter(SafePrinter printer, Stream stream)
    {
      stream.Seek(0L, SeekOrigin.Begin);
      byte[] buffer = new byte[1048576];
      int size;
      while ((size = stream.Read(buffer, 0, 1048576)) != 0)
        printer.WritePrinter(buffer, size);
    }

    [Obsolete]
    public static void PrintFile(string printer, string path, string documentName)
    {
      using (FileStream fileStream = File.OpenRead(path))
        Printer.PrintStream(printer, (Stream) fileStream, documentName);
    }

    [Obsolete]
    public static void PrintFile(string printer, string path)
    {
      Printer.PrintFile(printer, path, path);
    }

    [Obsolete]
    public static void PrintStream(string printer, Stream stream, string documentName)
    {
      PRINTER_DEFAULTS defaults = new PRINTER_DEFAULTS()
      {
        DesiredPrinterAccess = PRINTER_ACCESS_MASK.PRINTER_ACCESS_USE
      };
      using (SafePrinter printer1 = SafePrinter.OpenPrinter(printer, ref defaults))
        new Printer().DocPrinter(printer1, documentName, Printer.IsXPSDriver(printer1) ? "XPS_PASS" : "RAW", stream, false, 1, printer);
    }
  }
}
