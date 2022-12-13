using BlImplementation;
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
using System.Windows.Shapes;

namespace PL
{
    /// <summary>
    /// Interaction logic for ProductsListWindow.xaml
    /// </summary>
    public partial class ProductsListWindow : Window
    {
        BlApi.IBl bl = new Bl();

        public ProductsListWindow()
        {
            InitializeComponent();
            ProductsListview.ItemsSource = bl.Product.GetProducts();
        }

        private void CartListview_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ProductSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        private void ProductsListview_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void AddNewProduct_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CategoriesSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CategoriesSelector.ItemsSource = Enum.GetValues(typeof(BO.categories));
        }

        ///private void AddNewProduct_Click(object sender, RoutedEventArgs e) => 
    }
}
