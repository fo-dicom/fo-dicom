// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using FellowOakDicom.IO.Buffer;
using System.Collections.Generic;
using System.Linq;

namespace FellowOakDicom
{
    public class DicomTagComparer : IEqualityComparer<DicomItem>
    {
        public bool Equals(DicomItem x, DicomItem y) => x?.Tag == y?.Tag;

        public int GetHashCode(DicomItem obj) => obj.Tag.GetHashCode();
    }


    public class DicomValueComparer : IEqualityComparer<DicomItem>
    {
        public static DicomValueComparer DefaultInstance { get; set; } = new DicomValueComparer();

        public bool Equals(DicomItem item1, DicomItem item2)
        {
            if (ReferenceEquals(item1, item2))
            {
                // short circuit comparing the same item
                return true;
            }
            if (item1 is DicomElement xElement && item2 is DicomElement yElement)
            {
                if (xElement.Buffer is BulkDataUriByteBuffer xBulkbuffer && !xBulkbuffer.IsMemory || 
                    yElement.Buffer is BulkDataUriByteBuffer yBulkbuffer && !yBulkbuffer.IsMemory)
                {
                    // skip validation in case of BulkDataUriByteBuffer, where the content has not been downloaded
                    return item1.Tag == item2.Tag;
                }

                return xElement.Tag == yElement.Tag && xElement.Equals(yElement);
            }

            if (item1 is DicomSequence xSequence && item2 is DicomSequence ySequence)
            {
                var itemsCount = xSequence.Items.Count;
                if (itemsCount != ySequence.Items.Count)
                {
                    return false;
                }

                for (var i = 0; i < itemsCount; i++)
                {
                    var dataset1 = xSequence.Items[i];
                    var dataset2 = ySequence.Items[i];
                    if (!DicomDatasetComparer.DefaultInstance.Equals(dataset1, dataset2))
                    {
                        return false;
                    }
                }
            }
            else
            {
                return item1?.Tag == item2?.Tag;
            }

            return true;
        }

        public int GetHashCode(DicomItem obj)
        {
            if (obj is DicomElement elem)
            {
                var value = string.Join("\\", elem.Get<string[]>());
                return (value?.GetHashCode() ?? 0) ^ obj.Tag.GetHashCode();
            }

            return obj.Tag.GetHashCode();
        }
    }

    public class DicomDatasetComparer : IEqualityComparer<DicomDataset>
    {

        public static DicomDatasetComparer DefaultInstance { get; set; } = new DicomDatasetComparer();

        public bool Equals(DicomDataset dataset1, DicomDataset dataset2)
        {
            if ((dataset1 == null) != (dataset2 == null))
            {
                return false;
            }

            if (dataset1 == null)
            {
                return true;
            }

            var count = dataset1.Count();
            if (count != dataset2.Count())
            {
                return false;
            }

            foreach (var element in dataset1)
            {
                var element2 = dataset2.GetDicomItem<DicomItem>(element.Tag);
                if (!DicomValueComparer.DefaultInstance.Equals(element, element2))
                {
                    return false;
                }
            }

            return true;
        }

        public int GetHashCode(DicomDataset dataset)
        {
            var hash = 17;
            foreach (var element in dataset)
            {
                hash = hash * 23 + element.GetHashCode();
            }

            return hash;
        }
    }
}