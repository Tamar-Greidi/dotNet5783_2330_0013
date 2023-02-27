using BO;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

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
            if (selectedItem.ShipDate == DateTime.MinValue)
                txtShipDate.Text = "No date";
            if (selectedItem.DeliveryDate == DateTime.MinValue)
                txtDeliveryDate.Text = "No date";
            DataContext = new { isEnabled = false, selectedItem = selectedItem };
        }

        public OrdersWindow(BO.Order selectedItem, string use)
        {
            InitializeComponent();
            txtID.IsEnabled = false;
            txtCustomerName.IsEnabled = false;
            txtCustomerEmail.IsEnabled = false;
            txtCustomerAddress.IsEnabled = false;
            txtOrderDate.IsEnabled = false;
            txtStatus.IsEnabled = false;
            txtShipDate.IsEnabled = false;
            txtDeliveryDate.IsEnabled = false;
            txtTotalPrice.IsEnabled = false;
            UpdateShipDate.Visibility = Visibility.Collapsed;
            UpdateDeliveryDate.Visibility = Visibility.Collapsed;
            DataContext = selectedItem;
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
    public class IsEnabledConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool isEnabled = (bool)value;
            if (isEnabled)
                return true;
            else
                return false;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
    }
}

