// Copyright (c) 2012-2021 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using Dicom.IO;

namespace Dicom
{

    /// <summary>
    /// Class for managing DICOM dictionaries.
    /// </summary>
    public class DicomDictionary : IEnumerable<DicomDictionaryEntry>
    {
        #region Private Members

        public static readonly DicomDictionaryEntry UnknownTag =
            new DicomDictionaryEntry(
                DicomMaskedTag.Parse("xxxx", "xxxx"),
                "Unknown",
                "Unknown",
                DicomVM.VM_1_n,
                false,
                DicomVR.UN,
                DicomVR.AE,
                DicomVR.AS,
                DicomVR.AT,
                DicomVR.CS,
                DicomVR.DA,
                DicomVR.DS,
                DicomVR.DT,
                DicomVR.FD,
                DicomVR.FL,
                DicomVR.IS,
                DicomVR.LO,
                DicomVR.LT,
                DicomVR.OB,
                DicomVR.OD,
                DicomVR.OF,
                DicomVR.OL,
                DicomVR.OV,
                DicomVR.OW,
                DicomVR.PN,
                DicomVR.SH,
                DicomVR.SL,
                DicomVR.SQ,
                DicomVR.SS,
                DicomVR.ST,
                DicomVR.SV,
                DicomVR.TM,
                DicomVR.UC,
                DicomVR.UI,
                DicomVR.UL,
                DicomVR.UR,
                DicomVR.US,
                DicomVR.UT,
                DicomVR.UV);

        public static readonly DicomDictionaryEntry PrivateCreatorTag =
            new DicomDictionaryEntry(
                DicomMaskedTag.Parse("xxxx", "00xx"),
                "Private Creator",
                "PrivateCreator",
                DicomVM.VM_1,
                false,
                DicomVR.LO);

        private readonly ConcurrentDictionary<string, DicomPrivateCreator> _creators;

        private readonly ConcurrentDictionary<DicomPrivateCreator, DicomDictionary> _private;

        private readonly ConcurrentDictionary<DicomTag, DicomDictionaryEntry> _entries;

        private readonly ConcurrentDictionary<string, DicomTag> _keywords;

        private readonly ConcurrentBag<DicomDictionaryEntry> _masked;

        #endregion

        #region Constructors

        public DicomDictionary()
        {
            _creators = new ConcurrentDictionary<string, DicomPrivateCreator>();
            _private = new ConcurrentDictionary<DicomPrivateCreator, DicomDictionary>();
            _entries = new ConcurrentDictionary<DicomTag, DicomDictionaryEntry>();
            _keywords = new ConcurrentDictionary<string, DicomTag>();
            _masked = new ConcurrentBag<DicomDictionaryEntry>();
        }

        private DicomDictionary(DicomPrivateCreator creator)
        {
            PrivateCreator = creator;
            _entries = new ConcurrentDictionary<DicomTag, DicomDictionaryEntry>();
            _keywords = new ConcurrentDictionary<string, DicomTag>();
            _masked = new ConcurrentBag<DicomDictionaryEntry>();
        }

        #endregion

        #region Properties

        private static readonly object _lock = new object();

        private static DicomDictionary _default;
        private static bool _defaultIncludesPrivate;

        /// <summary>
        /// Ensures the default DICOM dictionaries are loaded
        /// Safe to call multiple times but will throw an exception if inconsistent values for loadPrivateDictionary are provided over multiple calls
        /// </summary>
        /// <param name="loadPrivateDictionary">Leave null (default value) if unconcerned.  Set true to search for resource streams named "Dicom.Dictionaries.Private Dictionary.xml.gz" in referenced assemblies</param>
        /// <returns></returns>
        public static DicomDictionary EnsureDefaultDictionariesLoaded(bool? loadPrivateDictionary = null)
        {
            // short-circuit if already initialised (#151).
            if (_default != null)
            {
                if (loadPrivateDictionary.HasValue && _defaultIncludesPrivate != loadPrivateDictionary.Value)
                {
                    throw new DicomDataException("Default DICOM dictionary already loaded " +
                                                 (_defaultIncludesPrivate ? "with" : "without") +
                                                 "private dictionary and the current request to ensure the default dictionary is loaded requests that private dictionary " +
                                                 (loadPrivateDictionary.Value ? "is" : "is not") + " loaded");
                }
                return _default;
            }

            lock (_lock)
            {
                if (_default == null)
                {
                    var dict = new DicomDictionary
                    {
                        new DicomDictionaryEntry(
                            DicomMaskedTag.Parse("xxxx", "0000"),
                            "Group Length",
                            "GroupLength",
                            DicomVM.VM_1,
                            false,
                            DicomVR.UL)
                    };
                    try
                    {
#if NET35 || HOLOLENS
                        using (
                            var stream =
                                new MemoryStream(
                                    UnityEngine.Resources.Load<UnityEngine.TextAsset>("DICOM Dictionary").bytes))
                        {
                            var reader = new DicomDictionaryReader(dict, DicomDictionaryFormat.XML, stream);
                            reader.Process();
                        }
#else
                        var assembly = typeof(DicomDictionary).GetTypeInfo().Assembly;
                        using (
                            var stream = assembly.GetManifestResourceStream(
                                "Dicom.Dictionaries.DICOMDictionary.xml.gz"))
                        {
                            var gzip = new GZipStream(stream, CompressionMode.Decompress);
                            var reader = new DicomDictionaryReader(dict, DicomDictionaryFormat.XML, gzip);
                            reader.Process();
                        }
#endif
                    }
                    catch (Exception e)
                    {
                        throw new DicomDataException(
                            "Unable to load DICOM dictionary from resources.\n\n" + e.Message,
                            e);
                    }
                    if (loadPrivateDictionary.GetValueOrDefault(true))
                    {
                        try
                        {
#if NET35 || HOLOLENS
                            using (
                                var stream =
                                    new MemoryStream(
                                        UnityEngine.Resources.Load<UnityEngine.TextAsset>("Private Dictionary").bytes))
                            {
                                var reader = new DicomDictionaryReader(dict, DicomDictionaryFormat.XML, stream);
                                reader.Process();
                            }
#else
                            var assembly = typeof(DicomDictionary).GetTypeInfo().Assembly;
                            using (
                                var stream =
                                    assembly.GetManifestResourceStream("Dicom.Dictionaries.PrivateDictionary.xml.gz"))
                            {
                                var gzip = new GZipStream(stream, CompressionMode.Decompress);
                                var reader = new DicomDictionaryReader(dict, DicomDictionaryFormat.XML, gzip);
                                reader.Process();
                            }
#endif
                        }
                        catch (Exception e)
                        {
                            throw new DicomDataException(
                                "Unable to load private dictionary from resources.\n\n" + e.Message,
                                e);
                        }
                    }

                    _defaultIncludesPrivate = loadPrivateDictionary.GetValueOrDefault(true);
                    _default = dict;
                }
                else
                {
                    //ensure the race wasn't for two different "load private dictionary" states
                    if (loadPrivateDictionary.HasValue && _defaultIncludesPrivate != loadPrivateDictionary)
                    {
                        throw new DicomDataException("Default DICOM dictionary already loaded " +
                                                     (_defaultIncludesPrivate ? "with" : "without") +
                                                     "private dictionary and the current request to ensure the default dictionary is loaded requests that private dictionary " +
                                                     (loadPrivateDictionary.Value ? "is" : "is not") + " loaded");
                    }
                    return _default;
                }

                //race is complete
                return _default;
            }
        }

        public static DicomDictionary Default
        {
            get => EnsureDefaultDictionariesLoaded(loadPrivateDictionary: null);
            set
            {
                lock (_lock)
                {
                    if (_default != null)
                    {
                        throw new DicomDataException(
                            "Cannot set Default DicomDictionary as it has already been initialised");
                    }
                    _default = value;
                }
            }
        }

        public DicomPrivateCreator PrivateCreator { get; internal set; }

        public DicomDictionaryEntry this[DicomTag tag]
        {
            get
            {
                if (_private != null && tag.PrivateCreator != null
                    && _private.TryGetValue(tag.PrivateCreator, out DicomDictionary pvt))
                {
                    return pvt[tag];
                }

                // special case for private creator tag
                // according to
                // http://dicom.nema.org/medical/dicom/current/output/chtml/part05/sect_7.8.html
                // The requirements of this section do not allow any use of elements in the ranges 
                // (gggg,0001-000F) and (gggg,0100-0FFF) where gggg is odd.
                if (tag.IsPrivate && tag.Element >= 0x0010 && tag.Element <= 0x00ff)
                {
                    return PrivateCreatorTag;
                }

                if (_entries.TryGetValue(tag, out DicomDictionaryEntry entry))
                {
                    return entry;
                }

                // this is faster than LINQ query
                foreach (var x in _masked)
                {
                    if (x.MaskTag.IsMatch(tag))
                    {
                        return x;
                    }
                }

                return UnknownTag;
            }
        }

        public DicomDictionary this[DicomPrivateCreator creator] => _private.GetOrAdd(creator, _ => new DicomDictionary(creator));

        /// <summary>
        /// Gets the DIcomTag for a given keyword.
        /// </summary>
        /// <param name="keyword">The attribute keyword that we look for.</param>
        /// <returns>A matching DicomTag or null if none is found.</returns>
        public DicomTag this[string keyword]
        {
            get
            {
                if (_keywords.TryGetValue(keyword, out DicomTag result))
                {
                    return result;
                }

                if (_private != null)
                {
                    foreach (var privDict in _private.Values)
                    {
                        var r = privDict[keyword];
                        if (r != null)
                        {
                            return r;
                        }
                    }
                }

                return null;
            }
        }



        #endregion

        #region Public Methods

        public void Add(DicomDictionaryEntry entry)
        {
            if (PrivateCreator != null)
            {
                entry.Tag = new DicomTag(entry.Tag.Group, entry.Tag.Element, PrivateCreator);
                if (entry.MaskTag != null)
                {
                    entry.MaskTag.Tag = entry.Tag;
                }
            }

            if (entry.MaskTag == null)
            {
                // allow overwriting of existing entries
                _entries[entry.Tag] = entry;
                _keywords[entry.Keyword] = entry.Tag;
            }
            else
            {
                _masked.Add(entry);
                _keywords[entry.Keyword] = entry.Tag;
            }
        }

        public DicomPrivateCreator GetPrivateCreator(string creator)
        {
            return _creators.GetOrAdd(creator, _ => new DicomPrivateCreator(creator));
        }

        /// <summary>
        /// Load DICOM dictionary data from file.
        /// </summary>
        /// <param name="file">File name.</param>
        /// <param name="format">File format.</param>
        public void Load(string file, DicomDictionaryFormat format)
        {
            using (var fs = IOManager.CreateFileReference(file).OpenRead())
            {
                var s = fs;
#if !NET35
                if (file.EndsWith(".gz"))
                {
                    s = new GZipStream(s, CompressionMode.Decompress);
                }
#endif
                var reader = new DicomDictionaryReader(this, format, s);
                reader.Process();
            }
        }
        #endregion

        #region IEnumerable Members

        public IEnumerator<DicomDictionaryEntry> GetEnumerator() => _entries.Values.Concat(_masked).GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        #endregion
    }
}
