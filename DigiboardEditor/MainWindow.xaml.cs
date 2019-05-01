
using DigiboardEditor.Pages;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using Telerik.Windows.Controls;

/// <summary>
/// 4/25/2019 - Victoria Pohle
/// This window functions as the primary application window with a navigation pane on the left for the user to navigate through the pages of the application.
/// The max and min versions of the dropdown buttons in the navigation are for when the naviation pane is collapsed or opened. The navigation does not have functionality
/// for dropdowns in the selected buttons but I used them as the content so that I could extend it's functionality to a dropdown under each navigation item.
/// </summary>
/// 
//Icon made by https://www.flaticon.com/authors/popcorns-arts from www.flaticon.com


namespace DigiboardEditor
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            StyleManager.ApplicationTheme = new MaterialTheme();
            InitializeComponent();
            //State = new OAuthState();
            DataContext = this;
            StartupPage page = new StartupPage();
 
            frmMain.Navigate(page);
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
            if (navigationView.IsPaneOpen == true)
            {
                btnUserMax.IsOpen = true;

            }
            else
            {
                btnUserMin.IsOpen = true;

            }

        }

        private void RadListBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if((sender as RadListBox).SelectedItem == null) return;
            String selection = (string)((sender as RadListBox).SelectedValue as RadListBoxItem).Content;
            

            switch (selection)
            {
                case "Add PDF Announcement":
                    AddPDFAnnouncementPage pDFAnnouncementPage = new AddPDFAnnouncementPage();
                    frmMain.Navigate(pDFAnnouncementPage);
                    btnAnnouncementsMax.IsOpen = false;
                    break;
                case "Add Note Announcement":
                    AddNoteAnnouncementPage noteAnnouncementPage = new AddNoteAnnouncementPage();
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
                    AddTimerPage addTimerPage = new AddTimerPage();
                    frmMain.Navigate(addTimerPage);
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
                    
                case "User Log":
                    UserLogPage userLogPage = new UserLogPage();
                    frmMain.Navigate(userLogPage);
                    break;
                case "Sign out":
                    System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
                    System.Windows.Application.Current.Shutdown();
                  
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (globals.CurrentUser != null)
            {
                tbUserName.Text = globals.CurrentUser.fullName;

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