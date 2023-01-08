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
    /// Interaction logic for UserDetails.xaml
    /// </summary>
    public partial class UserDetails : Window
    {
        BO.Cart cart = ProductsListWindow.cart;
        public UserDetails()
        {
            InitializeComponent();
        }

        private void OK_Click(object sender, RoutedEventArgs e)
        {
            cart.CustomerName = txtCustomerName.Text;
            cart.CustomerEmail = txtCustomerEmail.Text;
            cart.CustomerAddress = txtCustomerAddress.Text;
            Close();
        }
    }
}
