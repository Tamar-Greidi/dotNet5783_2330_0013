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
using System.Xml.Linq;

namespace PL
{
    /// <summary>
    /// Interaction logic for OrdersWindow.xaml
    /// </summary>
    public partial class OrdersWindow : Window
    {
        BlApi.IBl bl = BlApi.Factory.Get();

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
            txtShipDate.Text = selectedItem.ShipDate.ToShortDateString();
            txtShipDate.IsEnabled = false;
            txtDeliveryDate.Text = selectedItem.DeliveryDate.ToShortDateString();
            txtDeliveryDate.IsEnabled = false;
            ItemsLitsView.ItemsSource = selectedItem.Items;
            txtTotalPrice.Text = selectedItem.TotalPrice.ToString();
            txtTotalPrice.IsEnabled = false;
            if (selectedItem.ShipDate == DateTime.MinValue)
                txtShipDate.Text = "No date";
            if (selectedItem.DeliveryDate == DateTime.MinValue)
                txtDeliveryDate.Text = "No date";
        }

        public OrdersWindow(BO.Order selectedItem, string use)
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
            txtShipDate.Text = selectedItem.ShipDate.ToShortDateString();
            txtShipDate.IsEnabled = false;
            txtDeliveryDate.Text = selectedItem.DeliveryDate.ToShortDateString();
            txtDeliveryDate.IsEnabled = false;
            ItemsLitsView.ItemsSource = selectedItem.Items;
            txtTotalPrice.Text = selectedItem.TotalPrice.ToString();
            txtTotalPrice.IsEnabled = false;
            //if (selectedItem.ShipDate == DateTime.MinValue)
            //    txtShipDate.Text = "No date";
            //if (selectedItem.DeliveryDate == DateTime.MinValue)
            //    txtDeliveryDate.Text = "No date";
            UpdateShipDate.Visibility = Visibility.Collapsed;
            UpdateDeliveryDate.Visibility = Visibility.Collapsed;
        }


        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void UpdateShipDate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                BO.Order updateOrder = bl.Order.UpdateShipping(Convert.ToInt32(txtID.Text));
                txtShipDate.Text = updateOrder.ShipDate.ToShortDateString();

            }
            catch (OrderAlreadyShipped ex)
            {
                MessageBox.Show("Exception: " + ex.Message);
            }
            catch (DalException ex)
            {
                MessageBox.Show(ex.Message + " " + ex.InnerException?.Message);
            }
        }

        private void UpdateDeliveryDate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                BO.Order updateOrder = bl.Order.UpdateDelivery(Convert.ToInt32(txtID.Text));
                txtDeliveryDate.Text = updateOrder.DeliveryDate.ToShortDateString();

            }
            catch (OrderAlreadyShipped ex)
            {
                MessageBox.Show("Exception: " + ex.Message);
            }
            catch (DalException ex)
            {
                MessageBox.Show(ex.Message + " " + ex.InnerException?.Message);
            }
        }
    }
}
