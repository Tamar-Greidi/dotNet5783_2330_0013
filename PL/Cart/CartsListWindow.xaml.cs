using BlImplementation;
using BO;
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
    /// Interaction logic for CartsListWindow.xaml
    /// </summary>
    public partial class CartsListWindow : Window
    {
        BlApi.IBl bl = BlApi.Factory.Get();
        public CartsListWindow()
        {
            InitializeComponent();
            CartsListview.ItemsSource = ProductsListWindow.cart.Items;
            ProductsListWindow.cart.Items = ProductsListWindow.cart.Items == null ? new List<OrderItem>() : ProductsListWindow.cart.Items;
        }

        private void CartsSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void CartsListview_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void CartsListview_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            OrderItem cartItem = (OrderItem)(sender as ListView).SelectedItem;
            new CartsWindow(cartItem).ShowDialog();
            CartsListview.Items.Refresh();
        }

        private void SaveCart_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                new UserDetails().ShowDialog();
                bl.Cart.ConfirmationCart(ProductsListWindow.cart);
                MessageBox.Show("The order has been confirmed, Thank you");
                Close();
                ProductsListWindow.cart = new BO.Cart { Items = new List<BO.OrderItem?>() };
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
    }
}
