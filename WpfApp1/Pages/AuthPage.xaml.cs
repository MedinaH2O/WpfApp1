using System;
using System.Collections.Generic;
using System.Data.Entity;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfApp1.Components;

namespace WpfApp1.Pages
{
    /// <summary>
    /// Логика взаимодействия для AuthPage1.xaml
    /// </summary>
    public partial class AuthPage : Page
    {
        public AuthPage()
        {
            InitializeComponent();
            if(Properties.Settings.Default.Login != null)
            LoginTb.Text = Properties.Settings.Default.Login;
            if(Properties.Settings.Default.Password != null)
                PasswordTb.Text = Properties.Settings.Default.Password;
        }

        private void RegistrationBtn_Click(object sender, RoutedEventArgs e)
        {
            Navigation.NextPage(new Nav("Регистрация", new RegistPage()));
        }

        private void AuthBtn_Click(object sender, RoutedEventArgs e)
        {
            string login = LoginTb.Text.Trim();
            string passwword = PasswordTb.Text.Trim();
            if (login.Length == 0 && passwword.Length == 0)
                MessageBox.Show("Заполните поля");

            else
            {
                Navigation.AuthUser = DBConnect.db.User.ToList().Find(x => x.Login == login && x.Password == passwword);
                
                if (Navigation.AuthUser == null)
                    MessageBox.Show("Такого пользователя не существует ");
                else
                {
                    if (SaveCb.IsChecked == true)
                    {
                        Properties.Settings.Default.Login = LoginTb.Text;
                        Properties.Settings.Default.Password = PasswordTb.Text;
                        Properties.Settings.Default.Save();


                    }
                    Navigation.isAuth = true;
                    Navigation.NextPage(new Nav(" Список услуг ", new ServicesListPage()));
                }
                   
                
            }
              

        }
    }
}
