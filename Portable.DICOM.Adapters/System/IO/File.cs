// ReSharper disable CheckNamespace
namespace System.IO
// ReSharper restore CheckNamespace
{
    public class File
    {
        #region METHODS

        public static bool Exists(string path)
        {
            return true;
        }

        public static void Delete(string path)
        {
        }

        public static Stream OpenRead(string path)
        {
            return Stream.Null;
        }

        public static byte[] ReadAllBytes(string path)
        {
            return new byte[0];
        }

        public static void WriteAllBytes(string path, byte[] bytes)
        {
        }

        #endregion
    }
}