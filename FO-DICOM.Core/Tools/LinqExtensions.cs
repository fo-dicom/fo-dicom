// Copyright (c) 2012-2021 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System;
using System.Collections.Generic;

namespace FellowOakDicom.Tools
{
    public static class LinqExtensions
    {

        public static IEnumerable<T> Diff<S, T>(this IEnumerable<S> input, Func<S, S, T> difference)
        {
            var enumerator = input.GetEnumerator();
            if (!enumerator.MoveNext())
            {
                yield break;
            }
            var first = enumerator.Current;
            while (enumerator.MoveNext())
            {
                var second = enumerator.Current;
                yield return difference(first, second);
                first = second;
            }
        }

        public static IEnumerable<T> FilterByType<T>(this IEnumerable<object> input)
        {
            foreach(var obj in input)
            {
                if (obj is T typedvalue)
                {
                    yield return typedvalue;
                }
            }
        }

    }
}
