// Copyright (c) 2012 Anders Gustafsson, Cureos AB.
// Licensed under the Microsoft Public License (MS-PL).

using System.IO;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;

namespace Dicom.Helpers
{
    public static class Serialization
    {
        public static T GetSerializedDeserializedObject<T>(this T tag)
        {
            var _serializer = new DataContractSerializer(typeof(T));

            T deserializedObject;
            var builder = new StringBuilder();
            using (var writer = XmlWriter.Create(builder))
            {
                _serializer.WriteObject(writer, tag);
                writer.Close();

                using (var stream = new MemoryStream(Encoding.Unicode.GetBytes(builder.ToString())))
                {
                    deserializedObject = (T)_serializer.ReadObject(stream);
                }
            }
            return deserializedObject;
        }
    }
}