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
using Telerik.Windows.Controls;

namespace DigiboardEditor.Pages
{
    /// <summary>
    /// Interaction logic for UsergroupsViewPage.xaml
    /// </summary>
    public partial class ManageUsergroupsPage : Page, INotifyPropertyChanged
    {
        public ManageUsergroupsPage()
        {
            InitializeComponent();
            DataContext = this;
            PopulateUsersCollection();
            lbUsers.ItemsSource = UsersCollection;

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
        private ObservableCollection<User> _usersCollection;

        public ObservableCollection<User> UsersCollection
        {
            get { return _usersCollection; }
            set
            {
                _usersCollection = value;
                OnPropertyChanged();
            }
        }
        private void PopulateUsersCollection()
        {
            UsersCollection = UserRespository.Instance.Service.GetAll();

        }

        private void LbUsers_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {

        }
        private Visibility _spOpacity;

        public Visibility spOpacity
        {
            get { return _spOpacity; }
            set {
                _spOpacity = value;
                OnPropertyChanged();
            }
        }
        public object lb_item = null;



        private void BtnDeleteUser_Click(object sender, RoutedEventArgs e)
        {
            User user = (User)lbUsers.SelectedItem;
            DeleteUser(user);
            PopulateUsersCollection();

        }

        private void DeleteUser(User user)
        {
            if (user == null) return;
            UserRespository.Instance.Service.DeleteUser(user.UserID);
            UsersCollection.Remove(user);
        }

        private void LbUsers_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)

            {
                User user = (User)lbUsers.SelectedItem;
                DeleteUser(user);
                PopulateUsersCollection();

            }
        }
    }
}
