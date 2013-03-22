// Copyright (c) 2010-2013 Anders Gustafsson, Cureos AB.
// This source is subject to the Microsoft Public License.
// See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL.
// All other rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, 
// EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED 
// WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.

using Windows.Foundation;

namespace System.Threading
{
    public delegate void WaitCallback(object state);

    public static class ThreadPool
    {
         public static bool QueueUserWorkItem(WaitCallback callBack, object state = null)
         {
	         var workItem = global::Windows.System.Threading.ThreadPool.RunAsync(source => callBack(state));
             return workItem.Status != AsyncStatus.Error;
         }
    }
}