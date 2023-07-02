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

namespace WpfApp3
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static List<Products> FullList = App.DB.Products.ToList();
        public MainWindow()
        {
            InitializeComponent();
            table.ItemsSource = FullList;
            quantity.Content = $"Товаров {FullList.Count()}";
        }
        
        private void sort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int SortSelectedIndex = sort.SelectedIndex;
            int SaleSelectedIndex = selectSale.SelectedIndex;

            List<Products> listAll = FullList;
            #region Фильтрация
            if (SaleSelectedIndex == -1 || SaleSelectedIndex == 0)
            {
                listAll = FullList;
            }
            else if(SaleSelectedIndex == 1)
            {
                listAll = FullList.Where(p => p.Sale >= 0 && p.Sale < 10.0).ToList();
            }
            else if (SaleSelectedIndex == 2)
            {
                listAll = FullList.Where(p => p.Sale >= 10 && p.Sale < 15.0).ToList();
            }
            else if (SaleSelectedIndex == 3)
            {
                listAll = FullList.Where(p => p.Sale > 15.0).ToList();
            }
            #endregion
            #region Сортировка
            if (SortSelectedIndex == -1 || SortSelectedIndex == 0)
            {
                table.ItemsSource = listAll;
                    
            }
            else if (SortSelectedIndex == 1)
            {
                table.ItemsSource = listAll.OrderBy(p => p.Price);
            }
            else if (SortSelectedIndex == 2)
            {
                table.ItemsSource = listAll.OrderByDescending(p => p.Price);
            }
            #endregion
            if (listAll.Count() != FullList.Count())
            {
                quantity.Content = $"Товаров {listAll.Count()} из {FullList.Count()}";
                return;
            }
            else
            {
                quantity.Content = $"Товаров {listAll.Count()}";
            }
            
        }
    }
}
