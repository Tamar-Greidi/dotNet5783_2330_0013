using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
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
    /// Interaction logic for CartsWindow.xaml
    /// </summary>
    public partial class CartsWindow : Window
    {
        BlApi.IBl bl = BlApi.Factory.Get();
        BO.OrderItem cartItem;
        bool AmountChange = false;
        public CartsWindow(OrderItem cartItem)
        {
            InitializeComponent();
            this.cartItem = cartItem;
            txtID.Text = Convert.ToString(cartItem.ID);
            txtID.IsEnabled = false;
            txtName.Text = cartItem.Name;
            txtName.IsEnabled = false;
            txtProductID.Text = Convert.ToString(cartItem.ProductID);
            txtProductID.IsEnabled = false;
            txtPrice.Text = Convert.ToString(cartItem.Price);
            txtPrice.IsEnabled = false;
            txtAmount.Text = Convert.ToString(cartItem.Amount);
            txtTotalPrice.Text = Convert.ToString(cartItem.TotalPrice);
            txtTotalPrice.IsEnabled = false;
        }

        private void Amount_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!AmountChange)
                AmountChange = true;
            else
            {
                cartItem.Amount = Convert.ToInt32(txtAmount.Text);
                bl.Cart.UpdateProductAmount(ProductsListWindow.cart, cartItem.ProductID, cartItem.Amount);
                Close();
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            cartItem.Amount = 0;
            bl.Cart.UpdateProductAmount(ProductsListWindow.cart, cartItem.ProductID, cartItem.Amount);
            Close();
        }
    }
}
