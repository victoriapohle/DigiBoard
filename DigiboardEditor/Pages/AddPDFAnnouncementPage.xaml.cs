using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.Animation;
using Telerik.Windows.Documents.Fixed;
using Telerik.Windows.Documents.Fixed.UI;

namespace DigiboardEditor.Pages
{
    /// <summary>
    /// Interaction logic for AddPDFAnnouncementPage.xaml
    /// </summary>
    public partial class AddPDFAnnouncementPage : System.Windows.Controls.Page
    {
        public AddPDFAnnouncementPage()
        {
            StyleManager.ApplicationTheme = new MaterialTheme();
            DataContext = this;

            InitializeComponent();
            dpStartDate.SelectedDate = DateTime.Now.Date;
            dpEndDate.SelectedDate = DateTime.Now.Date;

        }
        #region INotifyPropertyChanged Members



        public event PropertyChangedEventHandler PropertyChanged;



        //Create OnPropertyChanged method to raise event

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {

            if (PropertyChanged != null)

                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
        private string _header;

        public string Header
        {
            get { return _header; }
            set
            {
                _header = value;
                OnPropertyChanged();
            }
        }
        private PdfDocumentSource _pdfBody;

        public PdfDocumentSource PDFBody
        {
            get { return _pdfBody; }
            set
            {
                _pdfBody = value;
                OnPropertyChanged();
            }
        }
        private byte[] _pdfBodyBytes;

        public byte[] PDFBodyBytes
        {
            get { return _pdfBodyBytes; }
            set
            {
                _pdfBodyBytes = value;
                OnPropertyChanged();
            }
        }
        private DateTime _startDate;

        public DateTime StartDate
        {
            get { return _startDate; }
            set
            {
                _startDate = value;
                OnPropertyChanged();
            }
        }
        private DateTime _endDate;

        public DateTime EndDate
        {
            get { return _endDate; }
            set
            {
                _endDate = value;
                OnPropertyChanged();
            }
        }


        public void SavePDFNote(string header, byte[] pdf, DateTime startDate, DateTime endTime)
        {


        }
        public void ConvertPDF(Stream fileStream)
        {
            byte[] file;
            using (var filestream = new StreamReader(fileStream))
            {
                using (var reader = new BinaryReader(fileStream))
                {
                    file = reader.ReadBytes((int)fileStream.Length);
                }
            }
            PDFBodyBytes = file;
        }


        private void RadButton_Click(object sender, RoutedEventArgs e)
        {
            var fileContent = string.Empty;
            RadOpenFileDialog openFileDialog = new RadOpenFileDialog()
            {
                Filter = "|PDF Files (*.pdf)|*.pdf",
                FilterIndex = 1,
            };

            openFileDialog.ShowDialog();

            if (openFileDialog.DialogResult == true)
            {
                try
                {

                    Stream fileStream = openFileDialog.OpenFile();
                    ConvertPDF(fileStream);
                    MemoryStream ms = new MemoryStream(PDFBodyBytes);
                    EmptyContent.Visibility = Visibility.Collapsed;
                    PDFBody = new PdfDocumentSource(ms);
                    pdfViewer.DocumentSource = new PdfDocumentSource(ms);

                }
                catch
                {

                }
            }
        }


        private void DpEndDate_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void BtnAddAnnouncement_Click(object sender, RoutedEventArgs e)
        {
            AddPDFAnnouncement(Header, PDFBodyBytes, false, StartDate, EndDate);


        }
        private void AddPDFAnnouncement(string header, byte[] body, bool isDeleted, DateTime startDate, DateTime endDate)
        {
            AnnouncementsPDF newpdf = new AnnouncementsPDF();
            newpdf.pdfHeader = header;
            newpdf.pdfBody = body;
            newpdf.isDeleted = false;
            newpdf.displayStartDate = startDate;
            newpdf.displayEndDate = endDate;
            PDFAnnouncementsRepository.Instance.Service.Add(newpdf);
            InitializePage();
        }
        public void InitializePage()
        {
            tbHeader.Text = null;
            dpStartDate.SelectedValue = null;
            dpEndDate.SelectedValue = null;
            dpStartDate.SelectedDate = DateTime.Now.Date;
            dpEndDate.SelectedDate = DateTime.Now.Date;
            pdfViewer.ClearDocument();
        }
        private void BtnCancelAnnouncement_Click(object sender, RoutedEventArgs e)
        {
            InitializePage();
        }
    }
}

