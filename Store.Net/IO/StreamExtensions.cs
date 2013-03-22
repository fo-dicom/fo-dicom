// Copyright (c) 2010-2013 Anders Gustafsson, Cureos AB.
// This source is subject to the Microsoft Public License.
// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL.
// All other rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, 
// EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED 
// WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.

using System.Threading.Tasks;

namespace System.IO
{
    public static class StreamExtensions
    {
        public static void Close(this Stream stream)
        {
            stream.Dispose();
        }

        public static IAsyncResult BeginWrite(this Stream stream, byte[] buffer, int offset, int count,
                                              AsyncCallback callback, object state)
        {
	        return
		        new TaskFactory().StartNew(asyncState => stream.Write(buffer, offset, count), state)
		                         .ContinueWith(task => callback(task));
        }

        public static void EndWrite(this Stream stream, IAsyncResult asyncResult)
        {
        }

		public static IAsyncResult BeginRead(this Stream stream, byte[] buffer, int offset, int count,
											  AsyncCallback callback, object state)
		{
			return new TaskFactory<int>().StartNew(asyncState => stream.Read(buffer, offset, count), state)
			                             .ContinueWith(task =>
				                                           {
					                                           callback(task);
					                                           return task.Result;
				                                           });
		}

		public static int EndRead(this Stream stream, IAsyncResult asyncResult)
		{
			var task = asyncResult as Task<int>;
			return task != null ? task.Result : 0;
		}
	}
}