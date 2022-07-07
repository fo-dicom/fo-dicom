using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace FellowOakDicom
{
    public static class DicomTagsIndex
    {
        private static Lazy<Dictionary<ushort, Dictionary<ushort, DicomTag>>> Index = new Lazy<Dictionary<ushort, Dictionary<ushort, DicomTag>>>(BuildIndex);

        private static Dictionary<ushort, Dictionary<ushort, DicomTag>> BuildIndex()
        {
            var dicomTagsPerGroup = typeof(DicomTag)
                .GetFields(BindingFlags.Public | BindingFlags.Static)
                .Where(field => field.FieldType == typeof(DicomTag))
                .Select(field => field.GetValue(null) as DicomTag)
                .Where(tag => tag != null)
                .GroupBy(tag => tag.Group)
                .ToList();

            var index = new Dictionary<ushort, Dictionary<ushort, DicomTag>>(dicomTagsPerGroup.Count);
            foreach (var group in dicomTagsPerGroup)
            {
                var tagsInGroup = group.ToList();
                var groupIndex = new Dictionary<ushort, DicomTag>(tagsInGroup.Count);

                foreach (var tag in group)
                {
                    if (!groupIndex.ContainsKey(tag.Element))
                    {
                        groupIndex.Add(tag.Element, tag);
                    }
                }

                index[group.Key] = groupIndex;
            }

            return index;
        }

        internal static DicomTag Lookup(ushort group, ushort element) =>
            Index.Value.TryGetValue(group, out var tagsInGroup)
            && tagsInGroup.TryGetValue(element, out var tag)
                ? tag
                : null;
    }
}