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
    /// Interaction logic for ManageEventsPage.xaml
    /// </summary>
    public partial class ManageEventsPage : Page
    {
        public ManageEventsPage()
        {

            InitializeComponent();
            DataContext = this;



        }
        private DateTime _selectedDate;

        public DateTime SelectedDate
        {
            get { return _selectedDate; }
            set
            {
                _selectedDate = value;
                OnPropertyChanged();
            }
        }
        private ObservableCollection<Event> _filteredEventsCollection;

        public ObservableCollection<Event> FilteredEventsCollection
        {
            get { return _filteredEventsCollection; }
            set
            {
                _filteredEventsCollection = value;
                OnPropertyChanged();
            }
        }


        private void PopulateFilteredEvents()
        {
            if(SelectedDate != null)
            {
                FilteredEventsCollection = new ObservableCollection<Event>(EventsRepository.Instance.Service.GetAll().Where(x => x.eventDateTime.Value.Date == SelectedDate.Date));

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

        private void Calendar_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            PopulateFilteredEvents();
            if(FilteredEventsCollection.Count == 0)
            {
                EmptyContent.Visibility = Visibility.Visible;
                EventsHeader.Header = "Events";



            }
            else
            {
                EmptyContent.Visibility = Visibility.Collapsed;
                EventsHeader.Header = "Events for " + SelectedDate.Date.ToString("dddd, dd MMMM yyyy");


            }
            lbEvents.ItemsSource = FilteredEventsCollection;
            

        }

        private void LbEvents_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
