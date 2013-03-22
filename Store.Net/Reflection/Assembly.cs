// Copyright (c) 2010-2013 Anders Gustafsson, Cureos AB.
// This source is subject to the Microsoft Public License.
// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL.
// All other rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, 
// EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED 
// WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.

using System.Reflection;

// ReSharper disable CheckNamespace
namespace Override.System.Reflection
// ReSharper restore CheckNamespace
{
    public static class Assembly
    {
        public static global::System.Reflection.Assembly GetExecutingAssembly()
        {
            return typeof(Assembly).GetTypeInfo().Assembly;
        }
    }
}
