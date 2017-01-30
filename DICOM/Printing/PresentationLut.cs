// Copyright (c) 2012-2017 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System;

namespace Dicom.Printing
{
    public class PresentationLut : DicomDataset
    {

        public static readonly DicomUID SopClassUid = DicomUID.PresentationLUTSOPClass;

        public DicomUID SopInstanceUid { get; private set; }

        public DicomDataset LutSequence
        {
            get
            {
                var lutSequence = this.Get<DicomSequence>(DicomTag.PresentationLUTSequence);
                if (lutSequence != null)
                {
                    return lutSequence.Items[0];
                }
                else
                {
                    return null;
                }
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
                else
                {
                    return new ushort[0];
                }
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
                else
                {
                    return string.Empty;
                }
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
                else
                {
                    return new ushort[0];
                }
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
                return this.Get(DicomTag.PresentationLUTShape, string.Empty);
            }
            set
            {
                this.AddOrUpdate(DicomTag.PresentationLUTShape, value);
            }
        }

        #region Constrctuors

        public PresentationLut(DicomUID sopInstance = null)
            : base()
        {
            this.InternalTransferSyntax = DicomTransferSyntax.ExplicitVRLittleEndian;
            if (sopInstance == null || sopInstance.UID == string.Empty)
            {
                SopInstanceUid = DicomUID.Generate();
            }
            else
            {
                SopInstanceUid = sopInstance;
            }
            CreateLutSequence();
        }

        public PresentationLut(DicomUID sopInstance, DicomDataset dataset)
        {
            if (dataset == null)
            {
                throw new ArgumentNullException("dataset");
            }
            dataset.CopyTo(this);

        }

        public void CreateLutSequence()
        {
            var lutSequence = new DicomSequence(DicomTag.PresentationLUTSequence);
            lutSequence.Items.Add(new DicomDataset());
            this.Add(lutSequence);
        }

        #endregion
    }
}
