﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
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
using Newtonsoft.Json;
using Telerik.Windows.Documents.Fixed;
using Telerik.Windows.Controls.FixedDocumentViewersUI;
using Telerik.Windows.Documents.Fixed.UI;
using System.Windows.Threading;
using DigiboardEditor;

namespace DigiboardDisplay
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            tbDate.Text = DateTime.Now.ToString("dddd, dd MMMM yyyy h:mm tt");

            TodaysDate = DateTime.Now;
            PopulateNoteCollection();
            PopulateEventsCollection();
            PopulatePDFCollection();

            CurrPDFIndex = 0;

            MemoryStream ms = new MemoryStream(PDFCollection.Where(x => x.pdfID == 1).First().pdfBody);
            pdfViewer.DocumentSource = new PdfDocumentSource(ms);

            lbNotes.ItemsSource = NotesCollection;
            lbEvents.ItemsSource = FilteredEventCollection;
            var URL = @"http://api.openweathermap.org/data/2.5/weather?q=37363,us&APPID=a30aeb6b37aa19af8299bde337377743&units=imperial";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL);
            try
            {
                WebResponse response = request.GetResponse();
                using (Stream responseStream = response.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(responseStream, System.Text.Encoding.UTF8);
                    var array = reader.ReadToEnd();

                    var result = JsonConvert.DeserializeObject<RootObject>(array);
                    //var id = result.weather[0].id;
                    var id = 801;


                    if (id >= 200 && id < 300) /*img = "thunderstorm.png";*/
                    {

                    }
                    else if (id >= 300 && id < 500) /*img = "drizzle.png";*/
                    {
                    }
                    else if (id >= 500 && id < 600) /*img = "rain.png";*/
                    {
                    }
                    else if (id >= 600 && id < 700) /*img = "snow.png";*/
                    {
                    }
                    else if (id >= 700 && id < 800) /*img = "atmosphere.png";*/
                    {
                    }
                    else if (id == 800)/* img = (timePeriod == 'd') ? "clear_day.png" : "clear_night.png";*/
                    {
                        ClearNight.Visibility = Visibility.Visible;

                    }
                    else if (id == 801)/* img = (timePeriod == 'd') ? "few_clouds_day.png" : "few_clouds_night.png";*/
                    {
                        Cloudy.Visibility = Visibility.Visible;

                    }
                    else if (id == 802 || id == 803)
                    {
                    } /*img = (timePeriod == 'd') ? "broken_clouds_day.png" : "broken_clouds_night.png";*/
                    else if (id == 804)/* img = "overcast_clouds.png";*/
                    {
                    }
                    else if (id >= 900 && id < 903) /*img = "extreme.png";*/
                    {
                    }
                    else if (id == 903) /*img = "cold.png";*/
                    {
                    }
                    else if (id == 904) /*img = "hot.png";*/
                    {

                    }
                    else if (id == 905 || id >= 951) /*img = "windy.png";*/
                    { }
                    else if (id == 906) /*img = "hail.png";*/
                    { }
                    tempLabel.Content = result.main.temp + "ºF";
                }

            }
            catch (WebException ex)
            {
                WebResponse errorResponse = ex.Response;
                using (Stream responseStream = errorResponse.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(responseStream, System.Text.Encoding.GetEncoding("utf-8"));
                    String errorText = reader.ReadToEnd();
                    // log errorText
                }
                throw;
            }
            System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            System.Windows.Threading.DispatcherTimer PDFTimer = new System.Windows.Threading.DispatcherTimer();
            PDFTimer.Tick += PDFTimer_Tick;
            PDFTimer.Interval = new TimeSpan(0, 0, 30);
            dispatcherTimer.Tick += dispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            PDFTimer.Start();
            dispatcherTimer.Start();



            Timer activeTimer = TimerRepository.Instance.Service.GetAll().Where(x => x.isDeleted == false && x.StopTime > DateTime.Now).FirstOrDefault();
            if (activeTimer != null)
            {
                Layer.Visibility = Visibility.Visible;

            }
        }
        public int CurrPDFIndex { get; set; }

        private void PDFTimer_Tick(object sender, EventArgs e)
        {
            if (CurrPDFIndex >= PDFCollection.Count-1)
            {
                CurrPDFIndex = 0;
            }
            else
            {
                CurrPDFIndex++;
            }
            MemoryStream ms = new MemoryStream(PDFCollection[CurrPDFIndex].pdfBody);
            pdfViewer.DocumentSource = new PdfDocumentSource(ms);


        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            //PopulatePDFCollection();
            PopulateNoteCollection();
            PopulateEventsCollection();
            tbDate.Text = DateTime.Now.ToString("dddd, dd MMMM yyyy h:mm:ss tt");
            lbNotes.ItemsSource = NotesCollection;
            lbEvents.ItemsSource = FilteredEventCollection;

            Timer activeTimer = TimerRepository.Instance.Service.GetAll().Where(x => x.isDeleted == false && x.StopTime > DateTime.Now).FirstOrDefault();
            if (activeTimer != null)
            {
                Layer.Visibility = Visibility.Visible;
                tbTimer.Visibility = Visibility.Visible;


                TimeSpan duration = activeTimer.StopTime - DateTime.Now;
                if(duration.Hours == 0)
                {
                    tbTimer.Text =  duration.Minutes.ToString() + ":" + duration.Seconds.ToString();

                }
                else
                {
                    tbTimer.Text = duration.Hours.ToString() + ":" + duration.Minutes.ToString() + ":" + duration.Seconds.ToString();

                }


            }
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
        private ObservableCollection<Event> _filteredEventCollection;

        public ObservableCollection<Event> FilteredEventCollection
        {
            get { return _filteredEventCollection; }
            set
            {
                _filteredEventCollection = value;
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
        private void PopulateEventsCollection()
        {
            FilteredEventCollection = EventsRepository.Instance.GetAll();

        }
        private void PopulateNoteCollection()
        {
            NotesCollection = NoteAnnouncementRepository.Instance.GetAll();

        }

        private void PopulatePDFCollection()
        {
            PDFCollection = PDFAnnouncementsRepository.Instance.Service.GetAll();

        }
        private DateTime _todaysDate;

        public DateTime TodaysDate
        {
            get { return _todaysDate; }
            set
            {
                _todaysDate = value;
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

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            pdfViewer.ScaleMode = ScaleMode.FitToPage;

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            mainWindow.Refresh();
            pdfViewer.ScaleMode = ScaleMode.FitToPage;



        }

        private void PdfViewer_Loaded(object sender, RoutedEventArgs e)
        {
            pdfViewer.ScaleMode = ScaleMode.FitToPage;

        }
        #region Weather
        public class Coord
        {
            public double lon { get; set; }
            public double lat { get; set; }
        }

        public class Weather
        {
            public int id { get; set; }
            public string main { get; set; }
            public string description { get; set; }
            public string icon { get; set; }
        }

        public class Main
        {
            public double temp { get; set; }
            public int pressure { get; set; }
            public int humidity { get; set; }
            public double temp_min { get; set; }
            public double temp_max { get; set; }
        }

        public class Wind
        {
            public double speed { get; set; }
        }

        public class Clouds
        {
            public int all { get; set; }
        }

        public class Sys
        {
            public int type { get; set; }
            public int id { get; set; }
            public double message { get; set; }
            public string country { get; set; }
            public int sunrise { get; set; }
            public int sunset { get; set; }
        }

        public class RootObject
        {
            public Coord coord { get; set; }
            public List<Weather> weather { get; set; }
            public string @base { get; set; }
            public Main main { get; set; }
            public int visibility { get; set; }
            public Wind wind { get; set; }
            public Clouds clouds { get; set; }
            public int dt { get; set; }
            public Sys sys { get; set; }
            public int id { get; set; }
            public string name { get; set; }
            public int cod { get; set; }
        }
        #endregion

        private void PdfViewer_DocumentChanged(object sender, DocumentChangedEventArgs e)
        {
            mainWindow.Refresh();
            pdfViewer.ScaleMode = ScaleMode.FitToPage;

        }
    }



}
public static class ExtensionMethods
{
    private static readonly Action EmptyDelegate = delegate { };
    public static void Refresh(this UIElement uiElement)
    {
        uiElement.Dispatcher.Invoke(DispatcherPriority.Render, EmptyDelegate);
    }
}
