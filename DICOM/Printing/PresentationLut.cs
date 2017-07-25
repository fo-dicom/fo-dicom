// Copyright (c) 2012-2017 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System;

namespace Dicom.Printing
{
    public class PresentationLut : DicomDataset
    {

        public static readonly DicomUID SopClassUid = DicomUID.PresentationLUTSOPClass;

        public DicomUID SopInstanceUid { get; }

        public DicomDataset LutSequence
        {
            get
            {
                var lutSequence = Get<DicomSequence>(DicomTag.PresentationLUTSequence, null);
                if (lutSequence != null)
                {
                    return lutSequence.Items[0];
                }

                return null;
            }
        }

        public ushort[] LutDescriptor
        {
            get
            {
                if (LutSequence != null)
                {
                    return LutSequence.Get<ushort[]>(DicomTag.LUTDescriptor);
                }

                return new ushort[0];
            }
            set
            {
                if (LutSequence != null)
                {
                    LutSequence.AddOrUpdate(DicomTag.LUTDescriptor, value);
                }
                else
                {
                    throw new InvalidOperationException("No LUT sequence found, call CreateLutSequence first");
                }
            }
        }

        public string LutExplanation
        {
            get
            {
                if (LutSequence != null)
                {
                    return LutSequence.Get(DicomTag.LUTDescriptor, string.Empty);
                }

                return string.Empty;
            }
            set
            {
                if (LutSequence != null)
                {
                    LutSequence.AddOrUpdate(DicomTag.LUTDescriptor, value);
                }
                else
                {
                    throw new InvalidOperationException("No LUT sequence found, call CreateLutSequence first");
                }
            }
        }

        public ushort[] LutData
        {
            get
            {
                if (LutSequence != null)
                {
                    return LutSequence.Get<ushort[]>(DicomTag.LUTData);
                }

                return new ushort[0];
            }
            set
            {
                if (LutSequence != null)
                {
                    LutSequence.AddOrUpdate(DicomTag.LUTData, value);
                }
                else
                {
                    throw new InvalidOperationException("No LUT sequence found, call CreateLutSequence first");
                }
            }
        }

        public string PresentationLutShape
        {
            get
            {
                return Get(DicomTag.PresentationLUTShape, string.Empty);
            }
            set
            {
                AddOrUpdate(DicomTag.PresentationLUTShape, value);
            }
        }

        #region Constrctuors

        public PresentationLut(DicomUID sopInstance = null)
        {
            InternalTransferSyntax = DicomTransferSyntax.ExplicitVRLittleEndian;
            SopInstanceUid = sopInstance == null || sopInstance.UID == string.Empty ? DicomUID.Generate() : sopInstance;

            CreateLutSequence();
        }

        public PresentationLut(DicomUID sopInstance, DicomDataset dataset)
        {
            if (dataset == null)
            {
                throw new ArgumentNullException(nameof(dataset));
            }
            dataset.CopyTo(this);

            SopInstanceUid = sopInstance == null || sopInstance.UID == string.Empty ? DicomUID.Generate() : sopInstance;
            AddOrUpdate(DicomTag.SOPInstanceUID, SopInstanceUid);
        }

        public void CreateLutSequence()
        {
            var lutSequence = new DicomSequence(DicomTag.PresentationLUTSequence);
            lutSequence.Items.Add(new DicomDataset());
            Add(lutSequence);
        }

        #endregion
    }
}
