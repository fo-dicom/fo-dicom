using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Dicom;
using Dicom.Imaging;
using Microsoft.Phone.Shell;
using Phone.DICOM.Viewer.Resources;
using Store.DICOM.Dump;
using Windows.Storage;

namespace Phone.DICOM.Viewer
{
	public partial class MainPage
	{
		private readonly ObservableCollection<DicomTextItem> _textItems = new ObservableCollection<DicomTextItem>();

		// Using a DependencyProperty as the backing store for DicomImageSource.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty DicomImageSourceProperty =
			DependencyProperty.Register("DicomImageSource", typeof(ImageSource), typeof(MainPage), new PropertyMetadata(null));
	
		// Constructor
		public MainPage()
		{
			InitializeComponent();

			// Sample code to localize the ApplicationBar
			//BuildLocalizedApplicationBar();
		}

		public ImageSource DicomImageSource
		{
			get { return (ImageSource)GetValue(DicomImageSourceProperty); }
			set { SetValue(DicomImageSourceProperty, value); }
		}

		public ObservableCollection<DicomTextItem> TextItems { get { return _textItems; } }

		// Sample code for building a localized ApplicationBar
		private void BuildLocalizedApplicationBar()
		{
		//    // Set the page's ApplicationBar to a new instance of ApplicationBar.
		    ApplicationBar = new ApplicationBar();

		//    // Create a new button and set the text value to the localized string from AppResources.
		    var appBarButton = new ApplicationBarIconButton(new Uri("/Assets/ApplicationIcon.png", UriKind.Relative))
			                       {
				                       Text = AppResources.AppBarButtonText
			                       };
			ApplicationBar.Buttons.Add(appBarButton);

		//    // Create a new menu item with the localized string from AppResources.
		    var appBarMenuItem = new ApplicationBarMenuItem(AppResources.AppBarMenuItemText);
		    ApplicationBar.MenuItems.Add(appBarMenuItem);
		}

		private async void MainPage_OnLoaded(object sender, RoutedEventArgs e)
		{
			var uri = new Uri("ms-appx:///Images/CT-MONO2-16-ankle.dcm");
			var file = await StorageFile.GetFileFromApplicationUriAsync(uri);
			var stream = (await file.OpenAsync(FileAccessMode.Read)).AsStream();

			var dicomObj = DicomFile.Open(stream);

			_textItems.Clear();
			new DicomDatasetWalker(dicomObj.FileMetaInfo).Walk(new DicomDumpWalker(_textItems));
			new DicomDatasetWalker(dicomObj.Dataset).Walk(new DicomDumpWalker(_textItems));

			var dicomImage = new DicomImage(dicomObj.Dataset);
			DicomImageSource = dicomImage.RenderImageSource();
		}
	}
}