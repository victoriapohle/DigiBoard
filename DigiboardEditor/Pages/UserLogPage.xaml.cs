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
    /// Interaction logic for UserLogPage.xaml
    /// </summary>
    public partial class UserLogPage : Page
    {
        public UserLogPage()
        {
            InitializeComponent();
            DataContext = this;
            PopulateLogCollection();
        }
        private void PopulateLogCollection()
        {
            LogCollection = UserLogRepository.Instance.Service.GetAll();
        }
        private ObservableCollection<UserLogHistory> _logCollection;

        public ObservableCollection<UserLogHistory> LogCollection
        {
            get { return _logCollection; }
            set { _logCollection = value; }
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
