using System;
using System.IO;

namespace Org.Xml.Sax
{
  public class XmlFilterSkeleton : 
    IXmlFilter,
    IXmlReader,
    IEntityResolver,
    IContentHandler,
    IErrorHandler,
    IDtdHandler
  {
    private IXmlReader parent;
    private ILocator locator;
    private IEntityResolver entityResolver;
    private IDtdHandler dtdHandler;
    private IContentHandler contentHandler;
    private IErrorHandler errorHandler;

    public IXmlReader Parent
    {
      get => this.parent;
      set
      {
        this.parent = value != null ? value : throw new ArgumentNullException(nameof (value), "Null parent");
      }
    }

    public virtual IContentHandler ContentHandler
    {
      get => this.contentHandler;
      set
      {
        this.contentHandler = value != null ? value : throw new ArgumentNullException(nameof (value), "Null content handler");
      }
    }

    public virtual IErrorHandler ErrorHandler
    {
      get => this.errorHandler;
      set
      {
        this.errorHandler = value != null ? value : throw new ArgumentNullException(nameof (value), "Null error handler");
      }
    }

    public virtual IDtdHandler DtdHandler
    {
      get => this.dtdHandler;
      set
      {
        this.dtdHandler = value != null ? value : throw new ArgumentNullException(nameof (value), "Null DTD handler");
      }
    }

    public virtual IEntityResolver EntityResolver
    {
      get => this.entityResolver;
      set
      {
        this.entityResolver = value != null ? value : throw new ArgumentNullException(nameof (value), "Null entity resolver");
      }
    }

    public XmlFilterSkeleton()
    {
    }

    public XmlFilterSkeleton(IXmlReader parent) => this.parent = parent;

    protected void SetupParse()
    {
      if (this.parent == null)
        throw new ArgumentNullException("parent", "No parent to filter");
      this.parent.EntityResolver = (IEntityResolver) this;
      this.parent.DtdHandler = (IDtdHandler) this;
      this.parent.ContentHandler = (IContentHandler) this;
      this.parent.ErrorHandler = (IErrorHandler) this;
    }

    public virtual void Parse(string data)
    {
      this.SetupParse();
      this.parent.Parse(data);
    }

    public virtual void Parse(Stream data)
    {
      this.SetupParse();
      this.parent.Parse(data);
    }

    public void Parse()
    {
      this.SetupParse();
      this.parent.Parse();
    }

    public virtual bool GetFeature(string featureName)
    {
      return this.parent != null ? this.parent.GetFeature(featureName) : throw new ArgumentException("Unrecognized feature name", nameof (featureName));
    }

    public virtual void SetFeature(string featureName, bool value)
    {
      if (this.parent == null)
        throw new ArgumentException("Unrecognized feature name", nameof (featureName));
      this.parent.SetFeature(featureName, value);
    }

    public virtual object GetProperty(string propertyName)
    {
      return this.parent != null ? this.parent.GetProperty(propertyName) : throw new ArgumentException("Unrecognized property name", nameof (propertyName));
    }

    public virtual void SetProperty(string propertyName, object value)
    {
      if (this.parent == null)
        throw new ArgumentException("Unrecognized property name", nameof (propertyName));
      this.parent.SetProperty(propertyName, value);
    }

    public virtual Stream ResolveEntity(string publicId, string systemId)
    {
      return this.entityResolver != null ? this.entityResolver.ResolveEntity(publicId, systemId) : (Stream) null;
    }

    public virtual void StartDocument()
    {
      if (this.contentHandler == null)
        return;
      this.contentHandler.StartDocument();
    }

    public virtual void EndDocument()
    {
      if (this.contentHandler == null)
        return;
      this.contentHandler.EndDocument();
    }

    public virtual void StartElement(
      string namespaceURI,
      string localName,
      string qName,
      IAttributeList attributes)
    {
      if (this.contentHandler == null)
        return;
      this.contentHandler.StartElement(namespaceURI, localName, qName, attributes);
    }

    public virtual void EndElement(string namespaceURI, string localName, string qName)
    {
      if (this.contentHandler == null)
        return;
      this.contentHandler.EndElement(namespaceURI, localName, qName);
    }

    public virtual void ProcessingInstruction(string target, string data)
    {
      if (this.contentHandler == null)
        return;
      this.contentHandler.ProcessingInstruction(target, data);
    }

    public virtual void StartPrefixMapping(string prefix, string uri)
    {
      if (this.contentHandler == null)
        return;
      this.contentHandler.StartPrefixMapping(prefix, uri);
    }

    public virtual void EndPrefixMapping(string prefix)
    {
      if (this.contentHandler == null)
        return;
      this.contentHandler.EndPrefixMapping(prefix);
    }

    public virtual void SkippedEntity(string name)
    {
      if (this.contentHandler == null)
        return;
      this.contentHandler.SkippedEntity(name);
    }

    public virtual void Characters(char[] chars, int start, int length)
    {
      if (this.contentHandler == null)
        return;
      this.contentHandler.Characters(chars, start, length);
    }

    public virtual void IgnorableWhitespace(char[] chars, int start, int length)
    {
      if (this.contentHandler == null)
        return;
      this.contentHandler.IgnorableWhitespace(chars, start, length);
    }

    public virtual void SetDocumentLocator(ILocator locator)
    {
      this.locator = locator;
      if (this.contentHandler == null)
        return;
      this.contentHandler.SetDocumentLocator(locator);
    }

    public virtual void Warning(Exception ex)
    {
      if (this.errorHandler == null)
        return;
      this.errorHandler.Warning(ex);
    }

    public virtual void Error(Exception ex)
    {
      if (this.errorHandler == null)
        return;
      this.errorHandler.Error(ex);
    }

    public virtual void FatalError(Exception ex)
    {
      if (this.errorHandler == null)
        return;
      this.errorHandler.FatalError(ex);
    }

    public virtual void NotationDecl(string name, string publicId, string systemId)
    {
      if (this.dtdHandler == null)
        return;
      this.dtdHandler.NotationDecl(name, publicId, systemId);
    }

    public virtual void UnparsedEntityDecl(
      string name,
      string publicId,
      string systemId,
      string notationName)
    {
      if (this.dtdHandler == null)
        return;
      this.dtdHandler.UnparsedEntityDecl(name, publicId, systemId, notationName);
    }
  }
}
