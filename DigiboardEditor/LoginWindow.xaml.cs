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
using System.Windows.Shapes;
using Telerik.Windows.Controls;

namespace DigiboardEditor
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window, INotifyPropertyChanged
    {
        public LoginWindow()
        {
            InitializeComponent();
            DataContext = this;
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
        //public bool ConfirmPasswordsEqual(string password, string username)
        //{
        //    byte[] passwordHash = Hash(password, username);

        //    return _passwordHash.SequenceEqual(passwordHash);
        //}
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

        private void BtnSubmit_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            this.Close();
            //User user = UserRespository.Instance.Service.GetAll().Where(x => x.email == UserEmail).FirstOrDefault();

            //if(user != null)
            //{

            //    if (user.isNewUser == false || user.isNewUser == null)
            //    {
            //        if (LoginPassword == null) return; //Add Data Validation - Try Again Empty Password
            //        var passwordEntered = GetSaltedPasswordHash(user.userName, LoginPassword);
            //        if (passwordEntered.SequenceEqual(user.password))
            //        {
            //            MainWindow main = new MainWindow();
            //            main.Show();
            //            this.Close();
            //        }
            //        else
            //        { 

            //        //Add Data Validation - Try Again INcorrect Password
            //        }
            //    }
            //    else
            //    {
            //        BtnNewUser_Click(null, null);
            //        if(ConfirmPassword == SetupPassword)
            //        {
            //            user.isNewUser = false;
            //            user.password = GetSaltedPasswordHash(user.userName, SetupPassword);
            //            UserRespository.Instance.Service.SaveChanges();
            //            MainWindow main = new MainWindow();
            //            main.Show();
            //            this.Close();
            //        }
            //        else
            //        {
            //            //Data Validation
            //        }

            //    }

            //    if (ConfirmPassword == null && SetupPassword == null)
            //    {
            //        if (LoginPassword == null) return; // Add Data Validation
            //        var passwordEntered = GetSaltedPasswordHash(user.userName, LoginPassword);
            //        var thingy = passwordEntered.SequenceEqual(user.password);

            //    }
            //    else
            //    {

            //    }

            //}
            //else
            //{
            //    //Data Validation - Incorrect Email Try Again
            //}

            //txtEmail.Text = null;
            //txtEmail.WatermarkContent = "Wrong Email. Try Again.";
            //MainWindow main = new MainWindow();
            //main.Show();
            //this.Close();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)

            {
                MainWindow main = new MainWindow();
                main.Show();
                this.Close();

            }
        }
        private string _loginPassword;

        public string LoginPassword
        {
            get { return _loginPassword; }
            set
            {
                _loginPassword = value;
                OnPropertyChanged();
            }
        }
        private string _setupPassword;

        public string SetupPassword
        {
            get { return _setupPassword; }
            set
            {
                _setupPassword = value;
                OnPropertyChanged();
            }
        }
        private string _confirmPassword;

        public string ConfirmPassword
        {
            get { return _confirmPassword; }
            set
            {
                _confirmPassword = value;
                OnPropertyChanged();
            }
        }

        private void BtnNewUser_Click(object sender, RoutedEventArgs e)
        {
            pnlPasswords.Visibility = Visibility.Visible;
            pnlLogonPassword.Visibility = Visibility.Collapsed;
            btnNewUser.Visibility = Visibility.Collapsed;


        }
        private void TbPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            var pb = sender as RadPasswordBox;
            SetupPassword = pb.Password;
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

        private void TbPasswordConfirm_PasswordChanged(object sender, RoutedEventArgs e)
        {
            var pb = sender as RadPasswordBox;
            ConfirmPassword = pb.Password;
        }

        private void TxtPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            var pb = sender as RadPasswordBox;
            LoginPassword = pb.Password;
        }
    }
}
