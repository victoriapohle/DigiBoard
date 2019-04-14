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
    /// Interaction logic for AddEventPage.xaml
    /// </summary>
    public partial class AddEventPage : Page, INotifyPropertyChanged
    {
        public AddEventPage()
        {
            DataContext = this;
            InitializeComponent();

            dpEventDate.SelectedDate = DateTime.Now.Date;

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
        private string _eventTitle;

        public string EventTitle
        {
            get { return _eventTitle; }
            set
            {
                _eventTitle = value;
                OnPropertyChanged();
            }
        }
        private string _eventDescription;

        public string EventDescription
        {
            get { return _eventDescription; }
            set
            {
                _eventDescription = value;
                OnPropertyChanged();
            }
        }
        private string _eventLocation;

        public string EventLocation
        {
            get { return _eventLocation; }
            set
            {
                _eventLocation = value;
                OnPropertyChanged();
            }
        }

        private DateTime _eventDateTime;

        public DateTime EventDateTime
        {
            get { return _eventDateTime; }
            set
            {
                _eventDateTime = value;
                OnPropertyChanged();
            }
        }



        private void BtnAddEvent_Click(object sender, RoutedEventArgs e)
        {
            Event newEvent = new Event();
            newEvent.eventTitle = EventTitle;
            newEvent.eventDescription = EventDescription;
            newEvent.eventLocation = EventLocation;
            newEvent.eventDateTime = EventDateTime;
            EventsRepository.Instance.Service.Add(newEvent);
            InitializePage();
        }
        public void InitializePage()
        {
            tbTitle.Text = null;
            tbDescription.Text = null;
            tbLocation.Text = null;
            dpEventDate.SelectedDate = DateTime.Now.Date;
        }
        private void BtnCancelEvent_Click(object sender, RoutedEventArgs e)
        {
            InitializePage();

        }
    }
}
