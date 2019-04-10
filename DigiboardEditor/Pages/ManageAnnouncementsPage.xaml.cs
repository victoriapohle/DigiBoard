using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

namespace DigiboardEditor.Pages
{
    /// <summary>
    /// Interaction logic for AnnouncementsViewPage.xaml
    /// </summary>
    public partial class ManageAnnouncementsPage : Page
    {
        public ManageAnnouncementsPage()
        {
            DataContext = this;

            InitializeComponent();
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
        #region INotifyPropertyChanged Members



        public event PropertyChangedEventHandler PropertyChanged;



        //Create OnPropertyChanged method to raise event

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {

            if (PropertyChanged != null)

                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        private void BtnDeleteAnnouncement_Click(object sender, RoutedEventArgs e)
        {
            DeleteNote(SelectedNote);
        }
        public void DeleteNote(AnnouncementsNote note)
        {
            var thingy = NoteAnnouncementsRepository.Instance.Service.Delete(note.noteID);
            NotesCollection.Remove(note);
        }
        public void DeletePDF(AnnouncementsPDF note)
        {
            var thingy = PDFAnnouncementsRepository.Instance.Service.Delete(note.pdfID);
            PDFCollection.Remove(note);
        }

        private void BtnDeletePDF_Click(object sender, RoutedEventArgs e)
        {
            DeletePDF(SelectedPDF);
        }
    }
}
