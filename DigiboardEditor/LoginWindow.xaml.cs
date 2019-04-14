using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void BtnSubmit_Click(object sender, RoutedEventArgs e)
        {

            //ObservableCollection<User> UsersCollection = UserRepository.Instance.GetAll();
            //foreach (User user in UsersCollection)
            //{

            //    if (txtEmail.Text == user.email)
            //    {
            //        if(txtPassword.Password == user.password)
            //        {
            //            MainWindow main = new MainWindow();
            //            main.Show();
            //            this.Close();
            //        }
            //        else
            //        {
            //            txtPassword.Password = null;
            //            txtPassword.WatermarkContent = "Wrong Password. Try Again";
            //            break;
            //        }

            //    }

            //}
            //txtEmail.Text = null;
            //txtEmail.WatermarkContent = "Wrong Email. Try Again.";
            MainWindow main = new MainWindow();
            main.Show();
            this.Close();
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
    }
}
