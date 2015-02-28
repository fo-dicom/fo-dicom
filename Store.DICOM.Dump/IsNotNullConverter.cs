using System;
using Windows.UI.Xaml.Data;

namespace Store.DICOM.Dump
{
	public class IsNotNullConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, string language)
		{
			return value != null;
		}

		public object ConvertBack(object value, Type targetType, object parameter, string language)
		{
			throw new NotSupportedException();
		}
	}
}