using System.IO;

namespace CBR.UfebsStream
{
  public sealed class ContentStorageFactory : AbstractSingleton<ContentStorageFactory>
  {
    private ContentStorageFactory()
    {
    }

    public ContentStorage CreateMemoryContentStorage()
    {
      return new ContentStorage((Stream) new MemoryStream());
    }

    public ContentStorage CreateFileContentStorage(string fileName)
    {
      return new ContentStorage((Stream) new FileStream(fileName, FileMode.Create, FileAccess.ReadWrite));
    }

    public ContentStorage CreateTempFileContentStorage()
    {
      return this.CreateFileContentStorage(Path.GetTempFileName());
    }

    public ContentStorage CreateStreamContentStorage(Stream stream) => new ContentStorage(stream);
  }
}
