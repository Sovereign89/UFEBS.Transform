using System;

namespace Org.Xml.Sax
{
    public interface IErrorHandler
    {
        void Warning(Exception ex);

        void Error(Exception ex);

        void FatalError(Exception ex);
    }
}
