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
    /// Interaction logic for OrdersListWindow.xaml
    /// </summary>
    public partial class OrdersListWindow : Window
    {
        BlApi.IBl bl = BlApi.Factory.Get();
        int debily = 0;

        public OrdersListWindow()
        {
            InitializeComponent();
            OrdersListview.ItemsSource = bl.Order.Get();
        }

        private void OrdersListview_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var order = (BO.OrderForList)(sender as ListView).SelectedItem;
            BO.Order selectedItem = bl.Order.GetDetails(order.ID);
            new OrdersWindow(selectedItem).ShowDialog();
        }

        private void OrdersListview_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }
    }
}
