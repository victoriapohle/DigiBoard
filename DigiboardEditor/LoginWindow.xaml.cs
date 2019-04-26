using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using System.Windows.Input;
using Telerik.Windows.Controls;

namespace DigiboardEditor
{
    /// <summary>
    /// 4/25/2019 - Victoria Pohle
    /// This window takes email and password input from the user for login. If the user is new, they click the "New User" button at the bottom of the page.
    /// The email and passwords get verified with the database information and then the user is either granted access or denied.
    /// If the user is new, an admin has previously set up their password so they are just adding their personal password to their account. Once they have
    /// set that up, they are granted access and their new password is Salted with their username from their email and Hashed.
    /// </summary>
    /// 

    public partial class LoginWindow : Window, INotifyPropertyChanged
    {
        public LoginWindow()
        {
            InitializeComponent();
            DataContext = this;
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




        public byte[] GetSaltedPasswordHash(string username, string password)
        {

            byte[] pwdBytes = Encoding.UTF8.GetBytes(password);
            byte[] salt = Encoding.UTF8.GetBytes(username);
            byte[] saltedPassword = new byte[pwdBytes.Length + salt.Length];

            Buffer.BlockCopy(pwdBytes, 0, saltedPassword, 0, pwdBytes.Length);
            Buffer.BlockCopy(salt, 0, saltedPassword, pwdBytes.Length, salt.Length);

            SHA1 sha = SHA1.Create();

            return sha.ComputeHash(saltedPassword);
        }

        private void VerifyUser(User user)
        {
            MainWindow main = new MainWindow();

            if (user != null)
            {

                if (user.isNewUser == false || user.isNewUser == null)
                {
                    //Data Validation - Empty PasswordBox
                    if (LoginPassword == null)
                    {
                        txtPassword.Clear();
                        txtPassword.WatermarkContent = "Wrong Password. Try Again.";
                    };

                    var passwordEntered = GetSaltedPasswordHash(user.userName, LoginPassword);

                    if (passwordEntered.SequenceEqual(TrimZerosOnEnd(user.password)))
                    {
                        globals.CurrentUser = user;
                        UserLogHistory newLog = new UserLogHistory();
                        newLog.loginTime = DateTime.Now;
                        newLog.userID = globals.CurrentUser.UserID;
                        UserLogRepository.Instance.Service.Add(newLog);

                        main.Show();
                        this.Close();

                    }
                    else
                    {
                        //Data Validation - Incorrect PasswordBox
                        txtPassword.Clear();
                        txtPassword.WatermarkContent = "Wrong Password. Try Again.";
                    }
                }
                else
                {
                    if (ConfirmPassword == SetupPassword)
                    {
                        user.isNewUser = false;
                        user.password = GetSaltedPasswordHash(user.userName, SetupPassword);
                        UserRespository.Instance.Service.SaveChanges();
                        globals.CurrentUser = user;
                        UserLogHistory newLog = new UserLogHistory();
                        newLog.loginTime = DateTime.Now;
                        newLog.userID = globals.CurrentUser.UserID;
                        UserLogRepository.Instance.Service.Add(newLog);
                        main.Show();
                        this.Close();
                    }
                    else
                    {
                        //Data Validation - Incorrect Password Setup
                    }

                }

            }
            else
            {
                //Data Validation - Incorrect Email 
                txtEmail.Text = null;
                txtEmail.WatermarkContent = "Wrong Email. Try Again.";


            }

        }

        public static byte[] TrimZerosOnEnd(byte[] array)
        {
            int lastIndex = Array.FindLastIndex(array, b => b != 0);

            Array.Resize(ref array, lastIndex + 1);

            return array;
        }





        private void BtnSubmit_Click(object sender, RoutedEventArgs e)
        {
            User user = UserRespository.Instance.Service.GetAll().Where(x => x.email == UserEmail).FirstOrDefault();
            VerifyUser(user);
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)

            {
                User user = UserRespository.Instance.Service.GetAll().Where(x => x.email == UserEmail).FirstOrDefault();
                VerifyUser(user);

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

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();

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
