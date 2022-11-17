using System;
using System.Collections.Generic;
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
    /// Логика взаимодействия для RegistPage.xaml
    /// </summary>
    public partial class RegistPage : Page
    {
        public RegistPage()
        {
            InitializeComponent();
        }

        private void RegistBtn_Click(object sender, RoutedEventArgs e)
        {
            string login = LoginTb.Text.Trim();
            string passwword = PasswordTb.Text.Trim();
            if (login.Length > 0 && passwword.Length > 0)
            {
                DBConnect.db.User.Add(new User
                {
                    Login = login,
                    Password = passwword,
                    RoleId = 2

                });
                            DBConnect.db.SaveChanges();
                MessageBox.Show("Регистрация прошла успешно");
                Navigation.BackPage();

            }

            else
                MessageBox.Show("Заполните поля");
            


        }

        private void ClearBtn_Click(object sender, RoutedEventArgs e)
        {
            LoginTb.Text = "";
            PasswordTb.Text = "";
        }
    }
}
