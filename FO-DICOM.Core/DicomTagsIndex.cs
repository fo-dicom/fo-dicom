using System;
using System.Linq;
using System.Reflection;

namespace FellowOakDicom
{
    /// <summary>
    /// Prebuilt index for high-performance lookup of known DICOM tags
    /// </summary>
    public static class DicomTagsIndex
    {
        private static readonly Lazy<DicomTag[][]> _index = new Lazy<DicomTag[][]>(BuildIndex);
        
        /// <summary>
        /// This builds a (rather sparse) 2D array of known DICOM tags
        /// Benchmarking showed that this consumes about 6MB of memory.
        /// The best alternative is using a dictionary based approach, which only consumes 0.4MB, but is 6x slower for lookups.
        /// </summary>
        private static DicomTag[][] BuildIndex()
        {
            var dicomTagsPerGroup = typeof(DicomTag)
                .GetFields(BindingFlags.Public | BindingFlags.Static)
                .Where(field => field.FieldType == typeof(DicomTag))
                .Select(field => field.GetValue(null) as DicomTag)
                .Where(tag => tag != null)
                .GroupBy(tag => tag.Group)
                .ToList();

            var highestGroup = dicomTagsPerGroup.Max(g => g.Key);
            var index = new DicomTag[highestGroup+1][];

            foreach (var group in dicomTagsPerGroup)
            {
                var highestElement = group.Max(g => g.Element);
                var groupIndex = new DicomTag[highestElement+1];

                foreach (var tag in group)
                {
                    if (groupIndex[tag.Element] == null)
                    {
                        groupIndex[tag.Element] = tag;
                    }
                }

                index[group.Key] = groupIndex;
            }
            return index;
        }
        
        public static DicomTag Lookup(ushort group, ushort element) => _index.Value[group]?[element];
    }
}