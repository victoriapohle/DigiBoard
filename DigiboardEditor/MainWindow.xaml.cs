
using DigiboardEditor.Pages;
using System;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Threading;
using Telerik.Windows;
using Telerik.Windows.Controls;

//google authentication: https://stackoverflow.com/questions/48321034/wpf-application-authentication-with-google

namespace DigiboardEditor
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //State = new OAuthState();
            DataContext = this;
            this.Height = (System.Windows.SystemParameters.PrimaryScreenHeight);
            this.Width = (System.Windows.SystemParameters.PrimaryScreenWidth);
         
        }
      
        private bool _isDropdownOpen;

        public bool IsDropdownOpen
        {
            get { return _isDropdownOpen; }
            set
            {
                _isDropdownOpen = value;
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


        private void BtnAnnouncements_Click(object sender, RoutedEventArgs e)
        {
            if (navigationView.IsPaneOpen == true)
            {
                btnAnnouncementsMax.IsOpen = true;

            }
            else
            {
                btnAnnouncementsMin.IsOpen = true;

            }
        }

        private void BtnAdminSettings_Click(object sender, RoutedEventArgs e)
        {
            if (navigationView.IsPaneOpen == true)
            {
                btnAdminSettingsMax.IsOpen = true;

            }
            else
            {
                btnAdminSettingsMin.IsOpen = true;
            }
        }

        private void BtnCalendar_Click(object sender, RoutedEventArgs e)
        {
            if (navigationView.IsPaneOpen == true)
            {
                btnCalendarMax.IsOpen = true;

            }
            else
            {
                btnCalendarMin.IsOpen = true;

            }

        }

        private void BtnTimer_Click(object sender, RoutedEventArgs e)
        {
            if (navigationView.IsPaneOpen == true)
            {
                btnTimerMax.IsOpen = true;

            }
            else
            {
                btnTimerMin.IsOpen = true;

            }
        }

        private void BtnUsername_Click(object sender, RoutedEventArgs e)
        {

        }
        private void FrmMain_Navigated(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {

        }

        private void RmiPDF_Click(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            NoteAnnouncementPage page = new NoteAnnouncementPage();
            frmMain.Navigate(page);

        }

        private void RmiNote_Click(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            PDFAnnouncementPage page = new PDFAnnouncementPage();
            frmMain.Navigate(page);

        }

        private void RmiView_Click(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            ManageAnnouncementsPage page = new ManageAnnouncementsPage();
            frmMain.Navigate(page);
        }

        private void RmiAddUser_Click(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            AddUserPage page = new AddUserPage();
            frmMain.Navigate(page);
        }

        private void RmiViewUsergroups_Click(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            ManageUsergroupsPage page = new ManageUsergroupsPage();
            frmMain.Navigate(page);
        }

        private void RmiAddEvent_Click(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            AddEventPage page = new AddEventPage();
            frmMain.Navigate(page);

        }

        private void RmiManageEvents_Click(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            ManageEventsPage page = new ManageEventsPage();
            frmMain.Navigate(page);
        }

        //TODO: Fix selection bug by unselecting everything else when button is clicked
        private void RadListBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if((sender as RadListBox).SelectedItem == null) return;
            String selection = (string)((sender as RadListBox).SelectedValue as RadListBoxItem).Content;
            

            switch (selection)
            {
                case "Add PDF Announcement":
                    PDFAnnouncementPage pDFAnnouncementPage = new PDFAnnouncementPage();
                    frmMain.Navigate(pDFAnnouncementPage);
                    btnAnnouncementsMax.IsOpen = false;
                    break;
                case "Add Note Announcement":
                    NoteAnnouncementPage noteAnnouncementPage = new NoteAnnouncementPage();
                    frmMain.Navigate(noteAnnouncementPage);
                    break;
                case "Manage Announcements":
                    ManageAnnouncementsPage manageAnnouncementsPage = new ManageAnnouncementsPage();
                    frmMain.Navigate(manageAnnouncementsPage);
                    break;
                case "Add Event":
                    AddEventPage addEventPage = new AddEventPage();
                    frmMain.Navigate(addEventPage);
                    break;
                case "Manage Events":
                    ManageEventsPage manageEventsPage = new ManageEventsPage();
                    frmMain.Navigate(manageEventsPage);
                    break;
                case "Start Timer":
                    break;
                case "Manage Timer":
                    break;
                case "Add User":
                    AddUserPage addUserPage = new AddUserPage();
                    frmMain.Navigate(addUserPage);
                    break;
                case "Manage Usergroups":
                    ManageUsergroupsPage manageUsergroupsPage = new ManageUsergroupsPage();
                    frmMain.Navigate(manageUsergroupsPage);
                    break;
                default:
                    Console.WriteLine("Default case");
                    break;
            }
            btnAnnouncementsMax.IsOpen = false;
            btnAnnouncementsMin.IsOpen = false;
            btnCalendarMax.IsOpen = false;
            btnCalendarMin.IsOpen = false;
            btnTimerMax.IsOpen = false;
            btnTimerMin.IsOpen = false;
            btnAdminSettingsMax.IsOpen = false;
            btnAdminSettingsMin.IsOpen = false;

            var box = sender as RadListBox;
            box.SelectedItem = null;
        }

        private void RadListBox_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
           
        }
    }
}