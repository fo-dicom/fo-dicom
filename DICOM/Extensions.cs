// Copyright (c) 2012-2015 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System;
using System.Threading;

namespace Dicom
{
    internal static class Extensions
    {
        public static void InvokeAsync(this Delegate delegate_, params object[] args)
        {
            ThreadPool.QueueUserWorkItem(delegate(object state) { delegate_.DynamicInvoke(args); });
        }
    }
}
