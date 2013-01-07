// ReSharper disable CheckNamespace
namespace NLog
// ReSharper restore CheckNamespace
{
    public class LogManager
    {
        public static Logger GetLogger(string name)
        {
            return new Logger(MetroLog.LogManagerFactory.DefaultLogManager.GetLogger(name));
        }
    }
}