// Copyright (c) 2012-2017 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace System.Collections.Concurrent
{
    using System.Collections.Generic;

    /// <summary>
    /// Compatibility replacement for <code>ConcurrentDictionary</code> in .NET 3.5.
    /// </summary>
    /// <typeparam name="TKey">Key type.</typeparam>
    /// <typeparam name="TValue">Value type.</typeparam>
    internal class ConcurrentDictionary<TKey, TValue> : Dictionary<TKey, TValue>
    {
        internal TValue GetOrAdd(TKey key, Func<TKey, TValue> valueFactory)
        {
            if (this.ContainsKey(key))
            {
                return this[key];
            }

            var value = valueFactory(key);
            this.Add(key, value);
            return value;
        }
    }
}