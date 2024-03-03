using System.Xml;

namespace CBR.UfebsStream
{
    public class ConditionalXmlReader : XmlReader
    {
        private XmlReader reader;
        private SkipConditionDelegate skipConditionDelegate;

        public override int AttributeCount => this.reader.AttributeCount;

        public override string BaseURI => this.reader.BaseURI;

        public override int Depth => this.reader.Depth;

        public override bool EOF => this.reader.EOF;

        public override bool HasValue => this.reader.HasValue;

        public override bool IsEmptyElement => this.reader.IsEmptyElement;

        public override string LocalName => this.reader.LocalName;

        public override XmlNameTable NameTable => this.reader.NameTable;

        public override string NamespaceURI => this.reader.NamespaceURI;

        public override XmlNodeType NodeType => this.reader.NodeType;

        public override string Prefix => this.reader.Prefix;

        public override ReadState ReadState => this.reader.ReadState;

        public override string Value => this.reader.Value;

        public ConditionalXmlReader(XmlReader reader, SkipConditionDelegate skipConditionDelegate)
        {
            this.reader = reader;
            this.skipConditionDelegate = skipConditionDelegate;
        }

        public override void Close() => this.reader.Close();

        public override string GetAttribute(int index) => this.reader.GetAttribute(index);

        public override string GetAttribute(string name, string namespaceURI)
        {
            return this.reader.GetAttribute(name, namespaceURI);
        }

        public override string GetAttribute(string name) => this.reader.GetAttribute(name);

        public override string LookupNamespace(string prefix) => this.reader.LookupNamespace(prefix);

        public override bool MoveToAttribute(string name, string namespaceURI)
        {
            return this.reader.MoveToAttribute(name, namespaceURI);
        }

        public override bool MoveToAttribute(string name) => this.reader.MoveToAttribute(name);

        public override bool MoveToElement() => this.reader.MoveToElement();

        public override bool MoveToFirstAttribute() => this.reader.MoveToFirstAttribute();

        public override bool MoveToNextAttribute() => this.reader.MoveToNextAttribute();

        public override bool Read()
        {
            bool flag = this.reader.Read();
            if (!flag)
                return flag;
            while (this.skipConditionDelegate(this.reader))
                this.reader.Skip();
            return this.reader.ReadState != ReadState.EndOfFile;
        }

        public override bool ReadAttributeValue() => this.reader.ReadAttributeValue();

        public override void ResolveEntity() => this.reader.ResolveEntity();
    }
}
