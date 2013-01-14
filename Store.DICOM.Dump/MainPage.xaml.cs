using System;
using Dicom;
using Windows.Storage.Pickers;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Store.DICOM.Dump
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage
    {
        #region FIELDS

        // Using a DependencyProperty as the backing store for Dataset.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DatasetProperty =
            DependencyProperty.Register("Dataset", typeof(DicomDataset), typeof(MainPage), new PropertyMetadata(null));

        #endregion

        public MainPage()
        {
            InitializeComponent();
        }

        #region PROPERTIES

        public DicomDataset Dataset
        {
            get { return (DicomDataset)GetValue(DatasetProperty); }
            set { SetValue(DatasetProperty, value); }
        }

        #endregion

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.  The Parameter
        /// property is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        private async void OpenFile_OnClick(object sender, RoutedEventArgs e)
        {
            var filePicker = new FileOpenPicker { SuggestedStartLocation = PickerLocationId.DocumentsLibrary };
            filePicker.FileTypeFilter.Add(".dcm");
            filePicker.FileTypeFilter.Add(".dic");

            var file = await filePicker.PickSingleFileAsync();
            if (file == null) return;

            var dicomFile = DicomFile.Open(file.Name);
            Dataset = dicomFile.Dataset;
        }
    }
}
