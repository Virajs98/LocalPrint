// Decompiled with JetBrains decompiler
// Type: RawPrint.IPrinter
// Assembly: RawPrint, Version=0.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A470BAED-380A-4929-918B-3B2143B33FB1
// Assembly location: C:\Users\DELL\.nuget\packages\rawprint\0.5.0\lib\net40\RawPrint.dll

using System.IO;

#nullable disable
namespace RawPrint
{
  public interface IPrinter
  {
    void PrintRawFile(string printer, string path, string documentName, bool paused = false);

    void PrintRawFile(string printer, string path, bool paused = false);

    void PrintRawStream(string printer, Stream stream, string documentName, bool paused = false);

    void PrintRawStream(
      string printer,
      Stream stream,
      string documentName,
      bool paused,
      int pagecount);

    event JobCreatedHandler OnJobCreated;
  }
}
