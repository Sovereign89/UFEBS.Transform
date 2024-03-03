using System.IO;
using System.Xml;
using Normalizer.TransformStream.Utils;
using Org.Xml.Sax;

namespace Normalizer.TransformStream.Parsers
{
    public sealed class ParsersFactory : AbstractSingleton<ParsersFactory>
    {
        private ParsersFactory()
        {
        }

        private IXmlReader CreateDomParserFromXmlReader(XmlReader xmlReader)
        {
            XmlDocument node = new XmlDocument();
            node.PreserveWhitespace = true;
            node.Load(xmlReader);
            return (IXmlReader)new Dom2SaxAdapter((XmlNode)node);
        }

        public IXmlReader CreateDomParser(string fileName)
        {
            return File.Exists(fileName) ? this.CreateDomParserFromXmlReader((XmlReader)new XmlTextReader(fileName)
            {
                Normalization = true
            }) : throw new FileNotFoundException("Файл не существует", fileName);
        }

        public IXmlReader CreateDomParser(Stream content)
        {
            return this.CreateDomParserFromXmlReader((XmlReader)new XmlTextReader(content)
            {
                Normalization = true
            });
        }

        public IXmlReader CreateDomParser(TextReader textReader)
        {
            return this.CreateDomParserFromXmlReader((XmlReader)new XmlTextReader(textReader)
            {
                Normalization = true
            });
        }

        public IXmlReader CreateDomParser(XmlReader xmlReader)
        {
            return this.CreateDomParserFromXmlReader(xmlReader);
        }

        public IXmlReader CreateDomParser(XmlNode node) => (IXmlReader)new Dom2SaxAdapter(node);

        public IXmlReader CreateSaxParser(string fileName)
        {
            return (IXmlReader)new Sax2SaxAdapter((XmlReader)new XmlTextReader(fileName)
            {
                Normalization = true
            });
        }

        public IXmlReader CreateSaxParser(Stream content)
        {
            return (IXmlReader)new Sax2SaxAdapter((XmlReader)new XmlTextReader(content)
            {
                Normalization = true
            });
        }

        public IXmlReader CreateSaxParser(TextReader textReader)
        {
            return (IXmlReader)new Sax2SaxAdapter((XmlReader)new XmlTextReader(textReader)
            {
                Normalization = true
            });
        }

        public IXmlReader CreateSaxParser(XmlReader xmlReader)
        {
            return (IXmlReader)new Sax2SaxAdapter(xmlReader);
        }
    }
}
