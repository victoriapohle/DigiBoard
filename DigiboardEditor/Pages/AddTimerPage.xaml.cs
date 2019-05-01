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
using Telerik.Windows.Controls;

namespace DigiboardEditor
{
    /// <summary>
    /// Interaction logic for TimerPage.xaml
    /// </summary>
    public partial class AddTimerPage : Page
    {
        public AddTimerPage()
        {
            InitializeComponent();
            DataContext = this;
            TimerAmount = 0;
        }
        private double _timerAmount;

        public double TimerAmount
        {
            get { return _timerAmount; }
            set
            {
                _timerAmount = value;
                OnPropertyChanged();
            }
        }


        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            tbTime.Clear();
        }

        private void BtnStartTimer_Click(object sender, RoutedEventArgs e)
        {
            var manager = new RadDesktopAlertManager();
            manager = new RadDesktopAlertManager(AlertScreenPosition.BottomCenter);
            manager = new RadDesktopAlertManager(AlertScreenPosition.TopCenter, 5);
            manager = new RadDesktopAlertManager(AlertScreenPosition.TopRight, new Point(0, 0));
            manager = new RadDesktopAlertManager(AlertScreenPosition.BottomCenter, new Point(0, 0), 10);
            if (TimerAmount != null && TimerAmount != 0)
            {
                Timer timer = new Timer();
                timer.StartTime = DateTime.Now;
                timer.StopTime = DateTime.Now.AddMinutes(TimerAmount);
                timer.isDeleted = false;
                TimerRepository.Instance.Service.Add(timer);
                var alert = new RadDesktopAlert
                {
                    Header = "Timer Started",
                    Content = "Timer has been started on the display monitor.",
                    ShowDuration = 5000,
                };
                manager.ShowAlert(alert);

            }
            else
            {
                var alert1 = new RadDesktopAlert
                {
                    Header = "Timer Not Started Sucessfully",
                    Content = "Timer has not been started on the display monitor.",
                    ShowDuration = 5000,
                };
                manager.ShowAlert(alert1);

            }
            tbTime.Clear();

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
