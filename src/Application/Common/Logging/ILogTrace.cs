using System;

namespace PLS.Application.Common.Logging
{
    public interface ILogTrace
    {
        void Flush();
        void Debug(string messageTemplate, params object[] propertyValues);
        void Info(string messageTemplate, params object[] propertyValues);
        void Warning(string messageTemplate, params object[] propertyValues);
        void Error(Exception exception);
    }
}
