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
		/// <summary>
		/// Gets the assembly that contains the code that is currently executing.
		/// </summary>
		/// <returns>The assembly that contains the code that is currently executing.</returns>
		/// <remarks>
		/// By definition, the executing assembly is the assembly from which this method is invoked.
		/// The return value should thus be the assembly from which this method is called.
		/// The "classic" method used to obtain this assembly, <code>Assembly.GetCallingAssembly</code> is not
		/// publicly exposed in the .NET for Windows Store assembly. Therefore the method is
		/// here invoked through reflection, based on a StackOverflow tip at http://stackoverflow.com/a/14754653/650012 .
		/// </remarks>
        public static global::System.Reflection.Assembly GetExecutingAssembly()
        {
			return (global::System.Reflection.Assembly)typeof(global::System.Reflection.Assembly).GetTypeInfo()
				.GetDeclaredMethod("GetCallingAssembly").Invoke(null, new object[0]);
        }
    }
}
