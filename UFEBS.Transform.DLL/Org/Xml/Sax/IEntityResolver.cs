using System.IO;

namespace Org.Xml.Sax
{
    public interface IEntityResolver
    {
        Stream ResolveEntity(string publicId, string systemId);
    }
}
