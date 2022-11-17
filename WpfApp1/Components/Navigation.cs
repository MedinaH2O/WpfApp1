using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations.Model;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using WpfApp1.Pages;

namespace WpfApp1.Components
{
    
    internal class Navigation
    {
        public static bool isAuth = false; //авторизовался ли пользователь
        public static MainWindow main; //доступ к элементам окна MainWindow
        public static List<Nav> navs = new List<Nav>(); // история страниц 
        public static User AuthUser = null;
        


      public static void NextPage(Nav nav)
        {
            navs.Add(nav);
            Update(nav);
        } 
        public static void BackPage()
        {
            if(navs.Count > 1)
                navs.RemoveAt(navs.Count-1);
            Update(navs[navs.Count-1]); 

        }
        private static void Update(Nav nav)
        {
            main.TitlePage.Text = nav.Title;
            main.BackBtn.Visibility = navs.Count > 2? System.Windows.Visibility.Visible: System.Windows.Visibility.Collapsed;
            main.ExitBtn.Visibility = isAuth == true ? System.Windows.Visibility.Visible : System.Windows.Visibility.Collapsed;
        main.MyFrame.Navigate(nav.Page);
        }
    }
    class Nav
    {
    
        public string Title { get; set; }
        public Page Page { get; set; }
        public Nav(string Title, Page Page)
        {
            this.Title = Title;
            this.Page = Page;
        }

      
    }
}
