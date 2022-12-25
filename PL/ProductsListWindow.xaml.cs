using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace PL
{
    /// <summary>
    /// Interaction logic for ProductsListWindow.xaml
    /// </summary>
    public partial class ProductsListWindow : Window
    {
        BlApi.IBl bl = BlApi.Factory.Get();
        int debily = 0;
        string user;
        public ProductsListWindow(string userType)
        {
            InitializeComponent();
            CategoriesSelector.ItemsSource = Enum.GetValues(typeof(BO.categories));
            if (userType != "admin")
            {
                ProductsListview.ItemsSource = bl.Product.GetAll();
                AddNewProduct.Visibility = Visibility.Hidden;
            }
            else
                ProductsListview.ItemsSource = bl.Product.GetCatalog();
            user = userType;
        }

        private void AddNewProduct_Click(object sender, RoutedEventArgs e)
        {
            new ProductsWindow().ShowDialog();
            ProductsListview.ItemsSource = bl.Product.GetCatalog();
        }

        private void CategoriesSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BO.categories category = (BO.categories)CategoriesSelector.SelectedItem;
            IEnumerable<BO.ProductForList> list = bl.Product.GetListByCategory(category);
            ProductsListview.ItemsSource = list;
        }

        private void ProductsListview_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                var product = (sender as ListView).SelectedItem;
                Cart cart = new Cart();
                //if (user == "admin")
                //{
                //    BO.Product selectedItem = bl.Product.GetProductDetails(product.ID);
                //    new ProductsWindow(selectedItem).ShowDialog();
                //}
                //else
                //{
                //    BO.ProductItem selectedItem = bl.Product.GetProductDetails(product.ID, cart);
                //    new ProductsWindow(selectedItem).ShowDialog();
                //}
                ProductsListview.ItemsSource = bl.Product.GetCatalog();
            }

            catch (DalException ex)
            {
                MessageBox.Show(ex.Message + " " + ex.InnerException.Message);
            }
            catch (InvalidData ex)
            {
                MessageBox.Show("Exception: " + ex.Message);
            }
        }

        private void ProductsListview_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}