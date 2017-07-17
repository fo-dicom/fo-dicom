// Copyright (c) 2012-2017 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System.Collections.Generic;

namespace System.Collections.Concurrent
{
    /// <summary>
    /// Compatibility replacement for <code>ConcurrentBag</code> in .NET 3.5.
    /// </summary>
    /// <typeparam name="T">Inner type of list.</typeparam>
    public class ConcurrentBag<T> : List<T>
    {
    }
}
