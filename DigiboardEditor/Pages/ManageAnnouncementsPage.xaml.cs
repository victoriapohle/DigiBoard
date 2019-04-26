using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Telerik.Windows.Controls;
using Telerik.Windows.Documents.Fixed;
using Telerik.Windows.Documents.Fixed.UI;

namespace DigiboardEditor.Pages
{
    /// <summary>
    /// Interaction logic for AnnouncementsViewPage.xaml
    /// </summary>
    public partial class ManageAnnouncementsPage : System.Windows.Controls.Page, INotifyPropertyChanged
    {
        public ManageAnnouncementsPage()
        {

            InitializeComponent();
            DataContext = this;

            PopulateNotesCollection();
            PopulatePDFCollection();
            lbPDF.ItemsSource = PDFCollection;
            lbNotes.ItemsSource = NotesCollection;
        }
        private ObservableCollection<AnnouncementsPDF> _pdfCollection;

        public ObservableCollection<AnnouncementsPDF> PDFCollection
        {
            get { return _pdfCollection; }
            set
            {
                _pdfCollection = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<AnnouncementsNote> _notesCollection;

        public ObservableCollection<AnnouncementsNote> NotesCollection
        {
            get { return _notesCollection; }
            set
            {
                _notesCollection = value;
                OnPropertyChanged();
            }
        }
        private AnnouncementsNote _selectedNote;

        public AnnouncementsNote SelectedNote
        {
            get { return _selectedNote; }
            set
            {
                _selectedNote = value;
                OnPropertyChanged();
            }
        }
        private AnnouncementsPDF _selectedPDF;

        public AnnouncementsPDF SelectedPDF
        {
            get { return _selectedPDF; }
            set
            {
                _selectedPDF = value;
                OnPropertyChanged();
            }
        }


        private void PopulatePDFCollection()
        {
            PDFCollection = PDFAnnouncementsRepository.Instance.Service.GetAll();

        }
        private void PopulateNotesCollection()
        {
            NotesCollection = NoteAnnouncementsRepository.Instance.Service.GetAll();

        }
   

        private void BtnDeleteAnnouncement_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedNote == null) return;
            RadWindow.Confirm("Are you sure you want to delete " + SelectedNote.announcementHeader + "?", this.OnClosedNote);

            
        }
        private void OnClosedNote(object sender, WindowClosedEventArgs e)
        {
            var result = e.DialogResult;
            if (result == true)
            {
                DeleteNote(SelectedNote);
                PopulateNotesCollection();


            }
            lbNotes.SelectedItem = null;

        }
        public void DeleteNote(AnnouncementsNote note)
        {
            NoteAnnouncementsRepository.Instance.Service.Delete(note.noteID);
            NotesCollection.Remove(note);
        }
        public void DeletePDF(AnnouncementsPDF note)
        {

            PDFAnnouncementsRepository.Instance.Service.Delete(note.pdfID);
            PDFCollection.Remove(note);
        }


        private void BtnDeletePDF_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedPDF == null) return;
            RadWindow.Confirm("Are you sure you want to delete "+ SelectedPDF.pdfHeader + "?", this.OnClosedPDF);
            
           
        }
        private void OnClosedPDF(object sender, WindowClosedEventArgs e)
        {
            var result = e.DialogResult;
            if (result == true)
            {
                DeletePDF(SelectedPDF);
                PopulatePDFCollection();

      
            }
            lbPDF.SelectedItem = null;
            pdfViewer.Visibility = Visibility.Collapsed;
        }
        private void LbPDF_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SelectedPDF == null) return;
            MemoryStream ms = new MemoryStream(SelectedPDF.pdfBody);
            pdfViewer.Visibility = Visibility.Visible;
            pdfViewer.DocumentSource = new PdfDocumentSource(ms);
            pdfViewer.Width = 350;

        }

        private void PdfViewer_DocumentChanged(object sender, DocumentChangedEventArgs e)
        {

            if (pdfViewer.Document != null)
            {
                this.pdfViewer.ScaleFactor = 0.35;

            }
        }

        private void LbNotes_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)

            {
                if (SelectedNote == null) return;
                RadWindow.Confirm("Are you sure you want to delete " + SelectedNote.announcementHeader + "?", this.OnClosedNote);

            }
        }

        private void LbPDF_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)

            {
                if (SelectedPDF == null) return;
                RadWindow.Confirm("Are you sure you want to delete " + SelectedPDF.pdfHeader + "?", this.OnClosedPDF);

            }
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
    }
}
