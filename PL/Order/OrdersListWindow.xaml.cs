using BO;
using System;
using System.Collections;
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
    /// Interaction logic for OrdersListWindow.xaml
    /// </summary>
    public partial class OrdersListWindow : Window
    {
        BlApi.IBl bl = BlApi.Factory.Get();
        string use;

        public OrdersListWindow()
        {
            InitializeComponent();
            try
            {
                OrdersListview.ItemsSource = bl.Order.Get();
            }
            catch (DalException ex)
            {
                MessageBox.Show("Exception: " + ex.Message + " " + ex.InnerException.Message);
            }
        }

        public OrdersListWindow(string str)
        {
            InitializeComponent();
            use = str;
            try
            {
                IEnumerable<OrderForList> orders = bl.Order.Get();
                List<OrderTracking> ordersTracking = new List<OrderTracking>();
                foreach (var order in orders)
                {
                    OrderTracking orderTracking = new()
                    {
                        ID = order.ID,
                        Status = order.Status
                    };
                    ordersTracking.Add(orderTracking);
                }
                OrdersListview.ItemsSource = ordersTracking;
            }
            catch (DalException ex)
            {
                MessageBox.Show("Exception: " + ex.Message + " " + ex.InnerException.Message);
            }
        }

        private void OrdersListview_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

            if (use == "tracking")
            { 
                OrderTracking order = (BO.OrderTracking)(sender as ListView).SelectedItem;
                BO.Order selectedItem = bl.Order.GetDetails(order.ID);
                new OrdersWindow(selectedItem, use).Show();
            }
            else
            { 
                OrderForList order = (BO.OrderForList)(sender as ListView).SelectedItem;
                BO.Order selectedItem = bl.Order.GetDetails(order.ID);
                new OrdersWindow(selectedItem).Show();
            }

        }

        private void OrdersListview_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }
    }
}
