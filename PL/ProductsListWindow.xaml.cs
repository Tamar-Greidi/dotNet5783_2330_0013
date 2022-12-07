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

        public ProductListWindow()
        {
            InitializeComponent();
            try
            {
                ProductListview.ItemsSource = bl.Product.GetProducts();
            }
            catch (Exception)
            {

            }
            ProductSelector.ItemsSource = Enum.GetValues(typeof(BO.Product));
        }

        private void CartListview_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ProductSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        ///private void AddNewProduct_Click(object sender, RoutedEventArgs e) => 
    }
}
