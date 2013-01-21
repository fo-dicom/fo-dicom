using System;

// ReSharper disable CheckNamespace
namespace NLog
// ReSharper restore CheckNamespace
{
    internal static class LogManager
    {
        internal static Logger GetLogger(string name)
        {
            return new Logger(MetroLog.LogManagerFactory.DefaultLogManager.GetLogger(name));
        }

		internal static Logger GetLogger(Type type)
		{
			return GetLogger(type.FullName);
		}

		internal static Logger GetLogger<T>()
		{
			return GetLogger(typeof(T));
		}
    }
}