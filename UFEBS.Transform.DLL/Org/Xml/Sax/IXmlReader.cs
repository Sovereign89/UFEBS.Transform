using System.IO;

namespace Org.Xml.Sax
{
    public interface IXmlReader
    {
        IContentHandler ContentHandler { get; set; }

        IErrorHandler ErrorHandler { get; set; }

        IDtdHandler DtdHandler { get; set; }

        IEntityResolver EntityResolver { get; set; }

        void Parse(string data);

        void Parse(Stream data);

        void Parse();

        bool GetFeature(string featureName);

        void SetFeature(string featureName, bool value);

        object GetProperty(string propertyName);

        void SetProperty(string propertyName, object value);
    }
}
