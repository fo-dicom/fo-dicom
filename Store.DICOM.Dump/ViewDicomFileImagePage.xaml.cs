using Windows.UI.Xaml;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Store.DICOM.Dump
{
	/// <summary>
	/// An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class ViewDicomFileImagePage
	{
		public ViewDicomFileImagePage()
		{
			InitializeComponent();
		}

		#region PROPERTIES

		public MainPage.DicomFileImage FileImage { get; private set; }

		#endregion

		/// <summary>
		/// Invoked when this page is about to be displayed in a Frame.
		/// </summary>
		/// <param name="e">Event data that describes how this page was reached.  The Parameter
		/// property is typically used to configure the page.</param>
		protected override void OnNavigatedTo(NavigationEventArgs e)
		{
			FileImage = e.Parameter as MainPage.DicomFileImage;
		}

		private void OnBackButtonClick(object sender, RoutedEventArgs e)
		{
			Frame.GoBack();
		}
	}
}
