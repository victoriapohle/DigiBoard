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
    /// Interaction logic for NoteAnnouncementPage.xaml
    /// </summary>
    public partial class NoteAnnouncementPage : Page
    {
        public NoteAnnouncementPage()
        {
            DataContext = this;

            InitializeComponent();
            dpStartDate.SelectedDate = DateTime.Now.Date;
            dpEndDate.SelectedDate = DateTime.Now.Date;

        }

        private void BtnAddAnnouncement_Click(object sender, RoutedEventArgs e)
        {
            AnnouncementsNote newNote = new AnnouncementsNote();
            newNote.announcementHeader = Header;
            newNote.announcementBody = Body;
            newNote.isDeleted = false;
            newNote.displayStartDate = StartDate;
            newNote.displayEndDate = EndDate;
            NoteAnnouncementsRepository.Instance.Add(newNote);
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
