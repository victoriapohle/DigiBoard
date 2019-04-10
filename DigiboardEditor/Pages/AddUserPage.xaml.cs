using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
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
    /// Interaction logic for AddUserPage.xaml
    /// </summary>
    public partial class AddUserPage : Page
    {
        public AddUserPage()
        {
            InitializeComponent();
            DataContext = this;
            PopulateUserRolesCollection();
            

        }
        private void PopulateUserRolesCollection()
        {
            UserRoleCollection = UserRoleRepository.Instance.Service.GetAll();

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
        private string _userName;

        public string UserName
        {
            get { return _userName; }
            set
            {
                _userName = value;
                OnPropertyChanged();
            }
        }
        private string _userEmail;

        public string UserEmail
        {
            get { return _userEmail; }
            set
            {
                _userEmail = value;
                OnPropertyChanged();
            }
        }
        private string _userPassword;

        public string UserPassword
        {
            get { return _userPassword; }
            set
            {
                _userPassword = value;
                OnPropertyChanged();
            }
        }
        private UserRole _selectedUserRole;

        public UserRole SelectedUserRole
        {
            get { return _selectedUserRole; }
            set
            {
                _selectedUserRole = value;
                OnPropertyChanged();

            }
        }
        private ObservableCollection<UserRole> _userRoleCOllection;

        public ObservableCollection<UserRole> UserRoleCollection
        {
            get { return _userRoleCOllection; }
            set
            {
                _userRoleCOllection = value;
                OnPropertyChanged();
            }
        }

        private void BtnCancelUser_Click(object sender, RoutedEventArgs e)
        {
            InitializePage();
        }

        private void BtnAddUser_Click(object sender, RoutedEventArgs e)
        {
            AddNewUser(UserName, UserEmail, false, SelectedUserRole.roleID, GetSaltedPasswordHash(UserName, UserPassword));
            InitializePage();
            ManageUsergroupsPage ManageUsergroups = new ManageUsergroupsPage();


        }

        public byte[] GetSaltedPasswordHash(string username, string password)
        {
            byte[] pwdBytes = Encoding.UTF8.GetBytes(password);
            // byte[] salt = BitConverter.GetBytes(userId);
            byte[] salt = Encoding.UTF8.GetBytes(username);
            byte[] saltedPassword = new byte[pwdBytes.Length + salt.Length];

            Buffer.BlockCopy(pwdBytes, 0, saltedPassword, 0, pwdBytes.Length);
            Buffer.BlockCopy(salt, 0, saltedPassword, pwdBytes.Length, salt.Length);

            SHA1 sha = SHA1.Create();

            return sha.ComputeHash(saltedPassword);
        }


        public void AddNewUser(string userName , string userEmail, bool isDeleted, int roleID, byte[] userPassword)
        {
            User newUser = new User();
            newUser.userName = userName;
            newUser.email = userEmail;
            newUser.isDeleted = isDeleted;
            newUser.roleID = roleID;
            newUser.password = userPassword;
            UserRespository.Instance.Service.Add(newUser);
           
        }

        public void InitializePage()
        {
            tbName.Text = null;
            tbEmail.Text = null;
            tbPassword.Clear();
            tbPasswordConfirm.Clear();
            cbUserRole.SelectedItem = null;
        }

        private void TbPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            var pb = sender as RadPasswordBox;
            UserPassword = pb.Password;
        }
    }
}
