// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Xml;

namespace FellowOakDicom.Tests.Helpers
{

    public static class SerializationExtensions
    {
        public static T GetDataContractSerializerDeserializedObject<T>(this T tag)
        {
            var serializer = new DataContractSerializer(typeof(T));

            T deserializedObject;
            var builder = new StringBuilder();
            using var writer = XmlWriter.Create(builder);
            serializer.WriteObject(writer, tag);
            writer.Close();

            using var stream = new MemoryStream(Encoding.Unicode.GetBytes(builder.ToString()));
            deserializedObject = (T)serializer.ReadObject(stream);
            return deserializedObject;
        }
    }
}
