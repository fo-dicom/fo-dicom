// Copyright (c) 2012-2023 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).
#nullable disable

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace FellowOakDicom.Network
{
    /// <summary>
    /// Base class that encapsulates the Service Class Application Information field for the
    /// SOP Class Extended Negotiation Sub-item. Inherited classes can implement the application
    /// information specific to the Service Class specification identified by the SOP Class UID.
    /// See: http://dicom.nema.org/medical/dicom/current/output/chtml/part07/sect_D.3.3.5.html for
    /// details on the Service Class Application Information field.
    /// </summary>
    public class DicomServiceApplicationInfo : IEnumerable<KeyValuePair<byte, byte>>
    {
        private readonly SortedDictionary<byte, byte> _fields;

        /// <summary>
        /// Initializes an instance of the <see cref="DicomServiceApplicationInfo"/> class.
        /// </summary>
        public DicomServiceApplicationInfo()
        {
            _fields = new SortedDictionary<byte, byte>();
        }

        /// <summary>
        /// Initializes an instance of the <see cref="DicomServiceApplicationInfo"/> class.
        /// </summary>
        /// <param name="rawApplicationInfo">Raw byte array with the application info</param>
        /// <filterpriority>2</filterpriority>
        public DicomServiceApplicationInfo(byte[] rawApplicationInfo)
        {
            _fields = new SortedDictionary<byte, byte>();

            for (byte i = 1; i <= rawApplicationInfo.Length; i++)
            {
                _fields.Add(i, rawApplicationInfo[i - 1]);
            }
        }

        /// <summary>
        /// Gets the number of fields contained in the <see cref="DicomServiceApplicationInfo"/>.
        /// </summary>
        public int Count => _fields.Count;

        /// <summary>
        /// Gets the byte value for given <paramref name="index"/>.
        /// </summary>
        /// <param name="index">Application info field index.</param>
        /// <returns>Byte value</returns>
        /// <exception cref="System.ArgumentException">Invalid field index.</exception>
        public byte this[byte index]
        {
            get => _fields[index];
            protected set
            {
                if (index == 0)
                {
                    throw new ArgumentException("0 is not a valid field index");
                }

                _fields[index] = value;
                FillInTheGaps();
            }
        }

        /// <summary>
        /// Represents a call-back method to be executed when application info is created.
        /// </summary>
        /// <param name="sopClass">SOP Class UID.</param>
        /// <param name="rawApplicationInfo">The raw application info byte data.</param>
        /// <returns>The application information class.</returns>
        public delegate DicomServiceApplicationInfo CreateApplicationInfoDelegate(DicomUID sopClass, byte[] rawApplicationInfo);

        /// <summary>
        /// A delegate to be executed when the application info is created.
        /// </summary>
        public static CreateApplicationInfoDelegate OnCreateApplicationInfo { get; set; }

        /// <summary>
        /// A factory method to initialize a new application information class based on the provided sop class.
        /// </summary>
        /// <param name="sopClass">SOP Class UID.</param>
        /// <param name="rawApplicationInfo">The raw application info byte data.</param>
        /// <returns>The application information class.</returns>
        public static DicomServiceApplicationInfo Create(DicomUID sopClass, byte[] rawApplicationInfo)
        {
            if (OnCreateApplicationInfo != null)
            {
                return OnCreateApplicationInfo.Invoke(sopClass, rawApplicationInfo) ??
                       new DicomServiceApplicationInfo(rawApplicationInfo);
            }

            if (sopClass.Name.Contains("Storage") &&
                sopClass.Type == DicomUidType.SOPClass)
            {
                return new DicomCStoreApplicationInfo(rawApplicationInfo);
            }

            if (sopClass.Name.Contains("- FIND") ||
                sopClass == DicomUID.UnifiedProcedureStepPull ||
                sopClass == DicomUID.UnifiedProcedureStepWatch)
            {
                return new DicomCFindApplicationInfo(rawApplicationInfo);
            }

            if (sopClass.Name.Contains("- MOVE"))
            {
                return new DicomCMoveApplicationInfo(rawApplicationInfo);
            }

            if (sopClass.Name.Contains("- GET"))
            {
                return new DicomCGetApplicationInfo(rawApplicationInfo);
            }

            return new DicomServiceApplicationInfo(rawApplicationInfo);
        }

        /// <summary>
        /// Add a single application info field value given by index and value.
        /// </summary>
        /// <param name="index">Application info field index.</param>
        /// <param name="value">Application info field value.</param>
        /// <exception cref="System.ArgumentException">Invalid field index or a field with same index already exists.</exception>
        public void Add(byte index, byte value)
        {
            if (index == 0)
            {
                throw new ArgumentException("0 is not a valid field index");
            }

            _fields.Add(index, value);
            FillInTheGaps();
        }

        /// <summary>
        /// Add or update a single application info field value given by index and value.
        /// </summary>
        /// <param name="index">Application info field index.</param>
        /// <param name="value">Application info field value.</param>
        /// <exception cref="System.ArgumentException">Invalid field index.</exception>
        public void AddOrUpdate(byte index, byte value)
        {
            this[index] = value;
        }

        /// <summary>
        /// Add or update a single application info field value given by index and value.
        /// </summary>
        /// <param name="index">Application info field index.</param>
        /// <param name="value">Application info field value.</param>
        /// <exception cref="System.ArgumentException">Invalid field index.</exception>
        public void AddOrUpdate(byte index, bool value)
        {
            this[index] = value ? (byte)1 : (byte)0;
        }

        /// <summary>
        /// Determines whether the Service Class Application Info
        /// contains a field with the specified index.
        /// </summary>
        /// <param name="index">Application info field index.</param>
        /// <returns><c>True</c> if exist, otherwise <c>False</c></returns>
        public bool Contains(byte index) => _fields.ContainsKey(index);

        /// <summary>
        /// Get the application info field value.
        /// </summary>
        /// <param name="index">Application info field index.</param>
        /// <returns>Application info field value.</returns>
        public byte GetValue(byte index)
        {
            return this[index];
        }

        /// <summary>
        /// Get the application info field value as <code>bool</code>.
        /// </summary>
        /// <param name="index">Application info field index.</param>
        /// <param name="defaultValue">Default value if Application info field index does not exist.</param>
        /// <returns><c>True</c> if 1 or <c>False</c> if 0, or <paramref name="defaultValue"/> if index does not exist.</returns>
        public bool GetValueAsBoolean(byte index, bool defaultValue)
        {
            return Contains(index) ? this[index] == 1 : defaultValue;
        }

        /// <summary>
        /// Get the application info field value for <c>enum</c> of type T.
        /// </summary>
        /// <typeparam name="T">Enum type to verify values against.</typeparam>
        /// <param name="index">Application info field index.</param>
        /// <param name="defaultValue">Default value if Application info field index does not exist.</param>
        /// <returns>Application info field value or <paramref name="defaultValue"/> if not exists.</returns>
        /// <exception cref="System.ArgumentException"><paramref name="T"/> is not an Enum.</exception>
        public byte GetValueForEnum<T>(byte index, byte defaultValue)
        {
            if (Contains(index) && Enum.IsDefined(typeof(T), (int)this[index]))
            {
                return this[index];
            }

            return defaultValue;
        }

        /// <summary>
        /// Get the raw Service Class Application Information field.
        /// </summary>
        /// <returns>Service Class Application Information field.</returns>
        public byte[] GetValues()
        {
            return _fields.Values.ToArray();
        }

        /// <summary>
        /// Removes the field with the specified index from the Service Class Application Info.
        /// </summary>
        /// <param name="index">Application info field index.</param>
        /// <returns><c>True</c> if successful removed, <c>False</c> otherwise.</returns>
        public bool Remove(byte index)
        {
            var result = _fields.Remove(index);
            FillInTheGaps();
            return result;
        }

        /// <summary>
        /// Returns an enumerator that iterates through the Application Info fields.
        /// </summary>
        /// <returns>Enumerator for the Application Info fields.</returns>
        /// <filterpriority>2</filterpriority>
        public IEnumerator<KeyValuePair<byte, byte>> GetEnumerator()
        {
            return _fields.GetEnumerator();
        }

        /// <summary>
        /// Returns an enumerator that iterates through the Application Info fields.
        /// </summary>
        /// <returns>Enumerator for the Application Info fields.</returns>
        /// <filterpriority>2</filterpriority>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>
        /// A string that represents the current object.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return string.Join(", ", _fields.Values);
        }

        /// <summary>
        /// Make sure to keep a sequential list from 1 to the highest index value.
        /// </summary>
        /// <param name="defaultValue">The value to fill in the empty gaps</param>
        private void FillInTheGaps(byte defaultValue = 0)
        {
            if (!_fields.Any())
            {
                return;
            }

            for (byte i = 1; i < _fields.Keys.Max(); i++)
            {
                if (!_fields.ContainsKey(i))
                {
                    _fields[i] = defaultValue;
                }
            }
        }
    }
}
