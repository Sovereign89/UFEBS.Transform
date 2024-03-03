using CBR.UfebsStream;
using Normalizer.TransformStream;
using Normalizer.TransformStream.Parsers;
using Org.Xml.Sax;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Xml;

namespace UFEBS.Canonicalizer
{
    public static class Canonicalization
    {
        public class SAX
        {
            private static List<string> strSigValue = (List<string>)null;
            private static Mutex mtxTransform = new Mutex();

            private static bool OnSkipSigValue(XmlReader delegateReader)
            {
                return delegateReader.NodeType == XmlNodeType.Element && delegateReader.LocalName == "SigValue" && delegateReader.NamespaceURI == "urn:cbr-ru:dsig:v1.1" && delegateReader.Depth == 1 || delegateReader.NodeType == XmlNodeType.Whitespace || delegateReader.NodeType == XmlNodeType.SignificantWhitespace;
            }

            private static bool OnExtractSigValue(XmlReader delegateReader)
            {
                bool sigValue = false;
                if (delegateReader.NodeType == XmlNodeType.Element && delegateReader.LocalName == "SigValue" && delegateReader.NamespaceURI == "urn:cbr-ru:dsig:v1.1" && delegateReader.Depth == 1)
                {
                    while (delegateReader.Read())
                    {
                        if (delegateReader.NodeType == XmlNodeType.Text)
                        {
                            if (strSigValue == null)
                                strSigValue = new List<string>();
                            strSigValue.Add(delegateReader.Value);
                        }
                        else if (delegateReader.NodeType == XmlNodeType.EndElement && delegateReader.LocalName == "SigValue" && delegateReader.NamespaceURI == "urn:cbr-ru:dsig:v1.1")
                            break;
                    }
                    sigValue = true;
                }
                if (delegateReader.NodeType == XmlNodeType.Whitespace || delegateReader.NodeType == XmlNodeType.SignificantWhitespace)
                    sigValue = true;
                return sigValue;
            }

            public static List<byte[]> Transform(Stream inputStream, Stream outputStream)
            {
                try
                {
                    mtxTransform.WaitOne();
                    strSigValue = new List<string>();
                    XmlTextReader reader = new XmlTextReader(inputStream);
                    reader.Normalization = true;
                    reader.WhitespaceHandling = WhitespaceHandling.All;
                    reader.EntityHandling = EntityHandling.ExpandEntities;
                    SkipConditionDelegate skipConditionDelegate = new SkipConditionDelegate(OnExtractSigValue);
                    XmlReader xmlReader = (XmlReader) new ConditionalXmlReader((XmlReader)reader, skipConditionDelegate);
                    IXmlReader saxParser = Normalizer.TransformStream.Utils.AbstractSingleton<ParsersFactory>.GetInstance().CreateSaxParser(xmlReader);
                    Normalizer.TransformStream.Utils.AbstractSingleton<TransformFactory>.GetInstance().CreateStreamTransformator(saxParser, outputStream).Parse();
                    List<byte[]> numArrayList = new List<byte[]>();
                    foreach (string s in strSigValue)
                        numArrayList.Add(Convert.FromBase64String(s));
                    return numArrayList;
                }
                finally
                {
                   mtxTransform.ReleaseMutex();
                }
            }

            public static List<byte[]> Transform(string inFileName, string outFileName)
            {
                using (FileStream inputStream = File.OpenRead(inFileName))
                {
                    using (FileStream outputStream = new FileStream(outFileName, FileMode.Create, FileAccess.Write, FileShare.None))
                    {
                        List<byte[]> numArrayList = Transform((Stream)inputStream, (Stream)outputStream);
                        inputStream.Close();
                        outputStream.Close();
                        return numArrayList;
                    }
                }
            }
        }
    }
}
