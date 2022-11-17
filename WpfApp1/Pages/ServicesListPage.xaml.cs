using System;
using System.Collections.Generic;
using System.ComponentModel;
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
//using ServiceShool.Componets;

namespace WpfApp1.Pages
{
    /// <summary>
    /// Логика взаимодействия для ServicesListPage.xaml
    /// </summary>
    public partial class ServicesListPage : Page
    {
        int actualPage = 0;
        public ServicesListPage()
        {
            InitializeComponent();
            if (Navigation.AuthUser.RoleId == 2)
                AddServBtn.Visibility = Visibility.Collapsed;
            
            ServiceList.ItemsSource = DBConnect.db.Service.Where(x=> x.IsDelete == true || x.IsDelete == null).ToList();
            GeneralCount.Text = DBConnect.db.Service.Where(x=>x.IsDelete != true).Count().ToString();
        }

        public static List<Service> ItemSource { get; private set; }
        private void Refresh()
        {
            IEnumerable<Service> filterService = DBConnect.db.Service.Where(x => x.IsDelete == true);
            if (SortCostCb.SelectedIndex > 0)
            {
                if (SortCostCb.SelectedIndex == 1)
                    filterService = filterService.OrderBy(x => x.CostDiscount);
                else
                    filterService = filterService.OrderByDescending(x => x.CostDiscount);
            }




            var DiscountCb = DiscountSortCb.SelectedItem as ComboBoxItem;
            if (DiscountCb != null)
            {
                if (DiscountCb.Tag.ToString() == "1")
                    filterService = DBConnect.db.Service;
                else if (DiscountCb.Tag.ToString() == "2")
                    filterService = filterService.Where(x => (x.Discount >= 0 && x.Discount < 5) || x.Discount == null);
                else if (DiscountCb.Tag.ToString() == "3")
                    filterService = filterService.Where(x => x.Discount >= 5 && x.Discount < 15);
                else if (DiscountCb.Tag.ToString() == "4")
                    filterService = filterService.Where(x => x.Discount >= 15 && x.Discount < 30);

                else if (DiscountCb.Tag.ToString() == "5")
                    filterService = filterService.Where(x => x.Discount >= 30 && x.Discount < 70);
            }



            if (NameDisSearchTb.Text.Length > 0)
                filterService = filterService.Where(x => x.Title.ToLower().StartsWith(NameDisSearchTb.Text.ToLower()) || x.Description.ToLower().StartsWith(NameDisSearchTb.Text));


            if (CountCb.SelectedIndex > -1 && filterService.Count() > 0)
            {
                int selCount = Convert.ToInt32((CountCb.SelectedItem as ComboBoxItem).Content);
                filterService = filterService.Skip(selCount * actualPage).Take(selCount);
                if (filterService.Count() == 0)
                {
                    actualPage--;
                    Refresh();
                }
            }

            ServiceList.ItemsSource = filterService.ToList();
            FoundCount.Text = filterService.Count().ToString() + " из ";

        }
            //ServiceList.ItemsSource = filterService.ToList();



            private void DiscountSortCb_SelectionChanged(object sender, SelectionChangedEventArgs e)
            {
                
                Refresh();
            }

            private void AddServiceBtn_Click(object sender, RoutedEventArgs e)
            {

            }

            private void CreateBtn_Click(object sender, RoutedEventArgs e)
            {
                var selService = (sender as Button).DataContext as Service;
                Navigation.NextPage(new Nav("Редактирование услуги", new AddServicePage(selService)));
            }

            private void DeleteBtn_Click(object sender, RoutedEventArgs e)
            {
                var selService = (sender as Button).DataContext as Service;
                if (MessageBox.Show("Вы точно хотите удалить эту запись!", "Редактирование", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    //Удаление продукта из БД
                    // DBConnect.db.Service.Remove(selService);

                    //Помечает, что запись удалена
                    selService.IsDelete = true;
                    MessageBox.Show("Запись удалена");
                    DBConnect.db.SaveChanges();
                    ServiceList.ItemsSource = DBConnect.db.Service.Where(x => x.IsDelete == false || x.IsDelete == null).ToList();

                }

            }



            private void LeftBtn_Click(object sender, RoutedEventArgs e)
            {
                actualPage--;
                if (actualPage < 0)
                    actualPage = 0;
                Refresh();


            }

            private void RightBtn_Click(object sender, RoutedEventArgs e)
            {
                actualPage++;
                Refresh();
            }

            private void CountCb_SelectionChanged(object sender, SelectionChangedEventArgs e)
            {
                actualPage = 0;
                Refresh();
            }



            private void AddServBtn_Click(object sender, RoutedEventArgs e)
            {
                Navigation.NextPage(new Nav("Добавление услуги", new AddServicePage(new Service())));

            }

            private void SortCostCb_SelectionChanged(object sender, SelectionChangedEventArgs e)
            {

            }

        private void ServiceList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
