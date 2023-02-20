using System.Windows;

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
