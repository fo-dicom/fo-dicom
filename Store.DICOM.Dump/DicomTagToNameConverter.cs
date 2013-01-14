using System;
using Dicom;
using Windows.UI.Xaml.Data;

namespace Store.DICOM.Dump
{
    public class DicomTagToNameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return ((DicomTag)value).DictionaryEntry.Name;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}