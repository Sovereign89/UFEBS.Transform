using Normalizer.TransformStream.Utils;
using Org.Xml.Sax;
using System;
using System.IO;

namespace Normalizer.TransformStream
{
    public sealed class TransformFactory : AbstractSingleton<TransformFactory>
    {
        private TransformFactory()
        {
        }

        public IXmlReader CreateStreamPrinter(IXmlReader xmlReader, TextWriter output)
        {
            if (xmlReader == null)
                throw new ArgumentNullException(nameof(xmlReader));
            return output != null ? (IXmlReader)new StreamPrinter(xmlReader, output) : throw new ArgumentNullException(nameof(output));
        }

        public IXmlReader CreateStreamNormalizator(IXmlReader xmlReader)
        {
            return xmlReader != null ? (IXmlReader)new StreamNormalizator(xmlReader) : throw new ArgumentNullException(nameof(xmlReader));
        }

        public IXmlReader CreateStreamCanonizator(IXmlReader xmlReader, Stream output)
        {
            if (xmlReader == null)
                throw new ArgumentNullException(nameof(xmlReader));
            return output != null ? (IXmlReader)new StreamCanonizator(xmlReader, output) : throw new ArgumentNullException(nameof(output));
        }

        public IXmlReader CreateStreamTransformator(IXmlReader xmlReader, Stream output)
        {
            if (xmlReader == null)
                throw new ArgumentNullException(nameof(xmlReader));
            return output != null ? (IXmlReader)new StreamCanonizator((IXmlReader)new StreamNormalizator(xmlReader), output) : throw new ArgumentNullException(nameof(output));
        }
    }
}
