using System;
using System.Collections.Generic;
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
    /// Interaction logic for AddNoteAnnouncementPage.xaml
    /// </summary>
    public partial class AddNoteAnnouncementPage : Page
    {
        public AddNoteAnnouncementPage()
        {
            DataContext = this;

            InitializeComponent();
  
            dpStartDate.SelectedDate = DateTime.Now.Date;
            dpEndDate.SelectedDate = DateTime.Now.Date;

        }

        private void BtnAddAnnouncement_Click(object sender, RoutedEventArgs e)
        {
            AddNoteAnnouncement(Header, Body, false, StartDate, EndDate);
        }

        private void AddNoteAnnouncement(string header, string body, bool isDeleted, DateTime startDate, DateTime endDate)
        {
            AnnouncementsNote newNote = new AnnouncementsNote();
            newNote.announcementHeader = header;
            newNote.announcementBody = body;
            newNote.isDeleted = false;
            newNote.displayStartDate = startDate;
            newNote.displayEndDate = endDate;
            NoteAnnouncementsRepository.Instance.Service.Add(newNote);
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


        private void BtnCancelAnnouncement_Click(object sender, RoutedEventArgs e)
        {

        }
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
        private string _body;

        public string Body
        {
            get { return _body; }
            set
            {
                _body = value;
                OnPropertyChanged();
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
