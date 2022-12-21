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
using System.Xml.Linq;

namespace PL
{
    /// <summary>
    /// Interaction logic for OrdersWindow.xaml
    /// </summary>
    public partial class OrdersWindow : Window
    {
        public OrdersWindow()
        {
            InitializeComponent();
        }

        public OrdersWindow(BO.Order selectedItem)
        {
            InitializeComponent();
            txtID.Text = selectedItem.ID.ToString();
            txtID.IsEnabled = false;
            txtCustomerName.Text = selectedItem.CustomerName;
            txtCustomerName.IsEnabled = false;
            txtCustomerEmail.Text = selectedItem.CustomerEmail;
            txtCustomerEmail.IsEnabled = false;
            txtCustomerAddress.Text = selectedItem.CustomerAddress;
            txtCustomerAddress.IsEnabled = false;
            txtOrderDate.Text = selectedItem.OrderDate.ToShortDateString();
            txtOrderDate.IsEnabled = false;
            txtStatus.Text = selectedItem.Status.ToString();
            txtStatus.IsEnabled = false;
            txtPaymentDate.Text = selectedItem.PaymentDate.ToShortDateString();
            txtPaymentDate.IsEnabled = false;
            txtShipDate.Text = selectedItem.ShipDate.ToShortDateString();
            txtShipDate.IsEnabled = false;
            txtDeliveryDate.Text = selectedItem.DeliveryDate.ToShortDateString();
            txtDeliveryDate.IsEnabled = false;
            ItemsLitsView.ItemsSource = selectedItem.Items;
            txtTotalPrice.Text = selectedItem.TotalPrice.ToString();
            txtTotalPrice.IsEnabled = false;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
