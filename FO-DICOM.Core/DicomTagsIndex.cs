// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace FellowOakDicom
{
    /// <summary>
    /// Prebuilt index for high-performance lookup of known DICOM tags
    /// </summary>
    internal static class DicomTagsIndex
    {
        private static readonly Lazy<Dictionary<uint, DicomTag>> _index = new Lazy<Dictionary<uint, DicomTag>>(BuildIndex);
        
        /// <summary>
        /// This builds a dictionary of known DICOM tags
        /// Benchmarking showed that this consumes about 0.8MB of memory.
        /// See https://github.com/fo-dicom/fo-dicom/pull/1417#discussion_r916766088 for benchmarks
        /// </summary>
        private static Dictionary<uint, DicomTag> BuildIndex()
        {
            var allDicomTags = typeof(DicomTag)
                .GetFields(BindingFlags.Public | BindingFlags.Static)
                .Where(field => field.FieldType == typeof(DicomTag))
                .Select(field => field.GetValue(null) as DicomTag)
                .Where(tag => tag != null)
                .ToList();

            var index = new Dictionary<uint, DicomTag>(allDicomTags.Count);
            foreach (var tag in allDicomTags)
            {
                var key = ((uint)tag.Group << 16) | tag.Element;
                if (!index.ContainsKey(key))
                {
                    index[key] = tag;
                }
            }


            return index;
        }
        
        /// <summary>
        /// Looks up or creates a DICOM tag based on its group and element
        /// </summary>
        /// <param name="group">The group of the DICOM tag</param>
        /// <param name="element">The element of the DICOM tag</param>
        /// <returns>A tag from the known DICOM tag index or a newly created instance of <see cref="DicomTag"/> otherwise</returns>
        public static DicomTag LookupOrCreate(ushort group, ushort element) => 
            _index.Value.TryGetValue(((uint)group << 16) | element, out var tag)
                ? tag
                : new DicomTag(group, element);
    }
}