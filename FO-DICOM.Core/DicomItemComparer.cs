// Copyright (c) 2012-2021 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System.Collections.Generic;

namespace FellowOakDicom
{

    public class DicomTagComparer : IEqualityComparer<DicomItem>
    {
        public bool Equals(DicomItem x, DicomItem y) => x.Tag == y.Tag;
        public int GetHashCode(DicomItem obj) => obj.Tag.GetHashCode();
    }


    public class DicomValueComparer : IEqualityComparer<DicomItem>
    {
        public bool Equals(DicomItem x, DicomItem y)
        {
            if (x is DicomElement xElement && y is DicomElement yElement)
            {
                var xValue = string.Join("\\", xElement.Get<string[]>());
                var yValue = string.Join("\\", yElement.Get<string[]>());

                return x.Tag == y.Tag && xValue == yValue;
            }
            else
            {
                return x.Tag == y.Tag;
            }
        }

        public int GetHashCode(DicomItem obj)
        {
            if (obj is DicomElement elem)
            {
                var value = string.Join("\\", elem.Get<string[]>());
                return (value?.GetHashCode() ?? 0) ^ obj.Tag.GetHashCode();
            }
            else
            {
                return obj.Tag.GetHashCode();
            }
        }
    }
}
