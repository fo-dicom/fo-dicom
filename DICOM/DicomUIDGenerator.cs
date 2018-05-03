// Copyright (c) 2012-2018 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;

using Dicom.Network;

namespace Dicom
{
    /// <summary>
    /// Class for generating DICOM UIDs.
    /// </summary>
    public class DicomUIDGenerator
    {
        #region FIELDS

        private static volatile DicomUID _instanceRootUid;

        private static long _lastTicks;

        private static readonly object _lock = new object();

        private static readonly DateTime Y2K = new DateTime(2000, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        private readonly ConcurrentDictionary<string, DicomUID> _sourceUidMap =
            new ConcurrentDictionary<string, DicomUID>();

        #endregion

        #region PROPERTIES

        private static DicomUID InstanceRootUID
        {
            get
            {
                if (_instanceRootUid != null) return _instanceRootUid;

                lock (_lock)
                {
                    if (_instanceRootUid != null) return _instanceRootUid;

                    DicomUID dicomUid;
                    _instanceRootUid = NetworkManager.TryGetNetworkIdentifier(out dicomUid)
                        ? dicomUid
                        : DicomUID.Append(DicomImplementation.ClassUID, Environment.TickCount);
                }

                return _instanceRootUid;
            }
        }

        #endregion

        #region METHODS

        [Obsolete("Will be deprecated. Use static method GenerateNew or GenerateFromDerivedUUID instead.")]
        public DicomUID Generate()
        {
            return GenerateNew();
        }

        /// <summary>
        /// If <paramref name="sourceUid"/> is known, return associated destination UID, otherwise generate and return 
        /// a new destination UID for the specified <paramref name="sourceUid"/>.
        /// </summary>
        /// <param name="sourceUid">Source UID.</param>
        /// <returns>Known or generated UID.</returns>
        public DicomUID Generate(DicomUID sourceUid)
        {
            if (sourceUid == null) throw new ArgumentNullException(nameof(sourceUid));
            return _sourceUidMap.GetOrAdd(sourceUid.UID, uid => GenerateNew());
        }

        /// <summary>
        /// Regenerate all UIDs in a DICOM dataset.
        /// </summary>
        /// <param name="dataset">Dataset in which UIDs should be regenerated.</param>
        public void RegenerateAll(DicomDataset dataset)
        {
            foreach (var ui in dataset.Where(x => x.ValueRepresentation == DicomVR.UI).ToArray())
            {
                var uid = dataset.Get<DicomUID>(ui.Tag);
                if (uid.Type == DicomUidType.SOPInstance || uid.Type == DicomUidType.Unknown) dataset.AddOrUpdate(ui.Tag, Generate(uid));
            }

            foreach (var sq in dataset.Where(x => x.ValueRepresentation == DicomVR.SQ).Cast<DicomSequence>().ToArray())
            {
                foreach (var item in sq)
                {
                    RegenerateAll(item);
                }
            }
        }

        /// <summary>
        /// Generate a new DICOM UID.
        /// </summary>
        /// <returns>Generated UID.</returns>
        public static DicomUID GenerateNew()
        {
            lock (_lock)
            {
                var ticks = DateTime.UtcNow.Subtract(Y2K).Ticks;
                if (ticks <= _lastTicks) ticks = _lastTicks + 1;
                _lastTicks = ticks;

                var str = ticks.ToString();
                if (str.EndsWith("0000")) str = str.Substring(0, str.Length - 4);

                var uid = new StringBuilder();
                uid.Append(InstanceRootUID.UID).Append('.').Append(str);

                return new DicomUID(uid.ToString(), "SOP Instance UID", DicomUidType.SOPInstance);
            }
        }

        /// <summary>
        /// Generate a UUID-derived UID, according to http://medical.nema.org/medical/dicom/current/output/html/part05.html#sect_B.2
        /// </summary>
        /// <returns>A new UID with 2.25 prefix.</returns>
        public static DicomUID GenerateDerivedFromUUID()
        {            
            var guid = Guid.NewGuid().ToByteArray();
            var bigint = new System.Numerics.BigInteger(guid);
            if (bigint < 0) bigint = -bigint;
            var uid = "2.25." + bigint;

            return new DicomUID(uid, "Local UID", DicomUidType.Unknown);
        }

        #endregion
    }
}
