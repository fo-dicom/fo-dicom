// Copyright (c) 2012-2021 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System;
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
        public bool Equals(DicomItem item1, DicomItem item2)
        {
            if (item1 is DicomElement xElement && item2 is DicomElement yElement)
            {
                var xValue = string.Join("\\", xElement.Get<string[]>());
                var yValue = string.Join("\\", yElement.Get<string[]>());

                return item1.Tag == item2.Tag && xValue == yValue;
            }

            if (item1 is DicomSequence xSequence && item2 is DicomSequence ySequence)
            {
                var itemsCount = xSequence.Items.Count;
                if (itemsCount != ySequence.Items.Count)
                {
                    return false;
                }

                var datasetComparer = new DicomDatasetComparer();
                for (var i = 0; i < itemsCount; i++)
                {
                    var dataset1 = xSequence.Items[i];
                    var dataset2 = ySequence.Items[i];
                    if (!datasetComparer.Equals(dataset1, dataset2))
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

            var valueComparer = new DicomValueComparer();
            foreach (var elements in dataset1.Zip(dataset2, Tuple.Create))
            {
                if (!valueComparer.Equals(elements.Item1, elements.Item2))
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