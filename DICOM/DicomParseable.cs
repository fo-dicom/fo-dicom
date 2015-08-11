// Copyright (c) 2012-2015 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System;
using System.Reflection;

namespace Dicom
{
    public abstract class DicomParseable
    {
        public static T Parse<T>(string value)
        {
            if (!typeof(T).IsSubclassOf(typeof(DicomParseable))) throw new DicomDataException("DicomParseable.Parse expects a class derived from DicomParseable");

            Type t = typeof(T);
            MethodInfo m = typeof(T).GetMethod("Parse", BindingFlags.Public | BindingFlags.Static);
            return (T)m.Invoke(null, new object[] { value });
        }
    }
}
