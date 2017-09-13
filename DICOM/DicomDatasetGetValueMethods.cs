// Copyright (c) 2012-2017 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

using Dicom.IO.Buffer;
using Dicom.StructuredReport;

namespace Dicom
{
    /// <summary>
    /// A collection of <see cref="DicomItem">DICOM items</see>.
    /// </summary>
    public partial class DicomDataset : IEnumerable<DicomItem>
    {
        public int GetValueCount(DicomTag tag )
        {
            DicomItem item;


            ValidateDicomTag (tag, out item);

            if (item.GetType().GetTypeInfo().IsSubclassOf(typeof(DicomElement)))
            {
                return ((DicomElement)item).Count;
            }
            else
            {
                //TODO: check if this is the correct. 
                //Are there any other cases where this method can be called for non DicomElement types?
                throw new DicomDataException ( "DicomTag doesn't support values.");
            }
        }

        public T GetValue<T>(DicomTag tag, int index)
        {
            if (index < 0)                       { throw new ArgumentOutOfRangeException ("n", "index must be a non-negative value"); }
            if (index >= GetValueCount(tag))     { throw new ArgumentOutOfRangeException ("index", "index must be less than value count"); }
            if (typeof(T).GetTypeInfo().IsArray) { throw new DicomDataException          ("T can't be an Array type. Use GetValues instead") ; }
            

            return Get(tag, index, false, default(T));
        }
        
        public bool TryGetValue<T> (DicomTag tag, int index, out T elementValue)
        {
            if (index < 0)                       { throw new ArgumentOutOfRangeException ("n", "index must be a non-negative value"); }
            if (typeof(T).GetTypeInfo().IsArray) { throw new DicomDataException          ("T can't be an Array type. Use GetValues instead") ; }
            
            if ( !Contains (tag))
            {
                elementValue = default(T);

                return false;
            }

            try
            {
                elementValue = Get (tag, index, false, default(T));

                return true;
            }
            catch (DicomDataException)
            {
                elementValue = default(T) ;

                return false;
            }
        }

        public T[] GetValues<T>(DicomTag tag )
        {
            //TODO: are there cases when this could be valid? byte[][]??
            if (typeof(T).GetTypeInfo().IsArray) { throw new DicomDataException ("T can't be an Array type.") ; }

            object[] values = new object[0];

            values = Get (tag, -1, false, values);

            return values.Cast<T> ().ToArray ();
        }

        public bool TryGetValues<T>(DicomTag tag, out T[] values )
        {
            try
            {
                values = GetValues<T> (tag);

                return true;
            }
            catch (DicomDataException)
            {
               values = null;

               return false;
            }
        }

        public T GetSingleValue<T> (DicomTag tag)
        {
            int count = GetValueCount (tag) ;

           if ( count != 1 ) { throw new DicomDataException( "DICOM element must contains a single value") ; }

            return Get<T> (tag, 0, false, default(T));
        }

        public bool TryGetSingleValue<T> (DicomTag tag, out T value)
        {
            value = default (T) ;

            //This check must precede "GetValueCount" as the later will throw if tag doesn't exists
            if (!Contains (tag))
            {
                return false ;
            }

            int count = GetValueCount (tag) ;

            if ( count > 1 ) { throw new DicomDataException ( "DICOM element must contains a single value") ; }
            
            if ( count == 0 ) 
            {
                return false ;
            }

            value = GetSingleValue<T> (tag) ;

            return true ;
        }

        public string GetString (DicomTag tag )
        {
            DicomItem item ;


            ValidateDicomTag (tag, out item) ;

            if (item is DicomStringElement || item is DicomMultiStringElement )
            {
                if ( ((DicomElement)item).Count == 0 )
                {
                    return string.Empty ;
                }
                else
                {
                    string[] values ;

                    values = Get<string[]> (tag, -1, false, null) ;

                    return string.Join ("\\", values) ;
                }
            }
            else
            {
                throw new DicomDataException ("DicomTag must be a string based element");
            }
        }

        public bool TryGetString (DicomTag tag, out string stringValue )
        {
            stringValue = null ;

            if (!Contains (tag))
            {
                return false ;
            }

            stringValue = GetString (tag);

            return true ;
        }

        private void ValidateDicomTag(DicomTag tag, out DicomItem item)
        {
            if (!_items.TryGetValue(tag, out item))
            {
                throw new DicomDataException("Tag: {0} not found in dataset", tag);
            }
        }
        
    }
}
