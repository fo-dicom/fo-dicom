// Copyright (c) 2012 Anders Gustafsson, Cureos AB.
// Licensed under the Microsoft Public License (MS-PL).

using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Xml;

namespace Dicom.Helpers
{
    public static class SerializationExtensions
    {
        public static T GetDataContractSerializerDeserializedObject<T>(this T tag)
        {
            var serializer = new DataContractSerializer(typeof(T));

            T deserializedObject;
            var builder = new StringBuilder();
            using (var writer = XmlWriter.Create(builder))
            {
                serializer.WriteObject(writer, tag);
                writer.Close();

                using (var stream = new MemoryStream(Encoding.Unicode.GetBytes(builder.ToString())))
                {
                    deserializedObject = (T)serializer.ReadObject(stream);
                }
            }
            return deserializedObject;
        }

        public static T GetBinaryFormatterDeserializedObject<T>(this T input)
        {
            T deserialized;
            var formatter = new BinaryFormatter();
            var tempFileName = Path.GetTempFileName();
            try
            {
                using (var write = File.OpenWrite(tempFileName))
                {
                    formatter.Serialize(write, input);
                    write.Close();
                    using (var read = File.OpenRead(tempFileName))
                    {
                        deserialized = (T)formatter.Deserialize(read);
                    }
                }
            }
            finally
            {
                try
                {
                    File.Delete(tempFileName);
                }
                catch
                {
                }
            }
            return deserialized;
        }
    }
}