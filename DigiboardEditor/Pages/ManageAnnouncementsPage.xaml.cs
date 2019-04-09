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
            InitializeComponent();
            DataContext = this;
            PopulateUsersCollection();
            lbNotes.ItemsSource = NotesCollection;
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
        private void PopulateUsersCollection()
        {
            NotesCollection = NoteAnnouncementsRepository.Instance.GetAll();

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

        }
    }
}
