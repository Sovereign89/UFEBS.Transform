namespace Org.Xml.Sax
{
    public interface IDtdHandler
    {
        void NotationDecl(string name, string publicId, string systemId);

        void UnparsedEntityDecl(string name, string publicId, string systemId, string notationName);
    }
}
