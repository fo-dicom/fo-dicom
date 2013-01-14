using System;
using Dicom;
using Windows.UI.Xaml.Data;

namespace Store.DICOM.Dump
{
    public class DicomItemToValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var item = value as DicomItem;
            if (item == null) return null;

            if (item is DicomElement) return ((DicomElement)item).Get<string>();
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}