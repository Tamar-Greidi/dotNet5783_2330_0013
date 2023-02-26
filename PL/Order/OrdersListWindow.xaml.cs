using BO;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PL;

/// <summary>
/// Interaction logic for OrdersListWindow.xaml
/// </summary>
public partial class OrdersListWindow : Window
{
    BlApi.IBl bl = BlApi.Factory.Get();
    string use = "";

    public OrdersListWindow()
    {
        InitializeComponent();
        try
        {
            List<OrderForList?> ordersList = bl.Order.Get().ToList();
            DataContext = ordersList;
        }
        catch (DalException ex)
        {
            MessageBox.Show("Exception: " + ex.Message + " " + ex.InnerException?.Message);
        }
    }

    public OrdersListWindow(string str)
    {
        InitializeComponent();
        use = str;
        try
        {
            List<OrderForList> orders = bl.Order.Get().ToList();
            List<OrderTracking> ordersTracking = new List<OrderTracking>();
            orders.ForEach(order => ordersTracking.Add(bl.Order.OrderTracking(order.ID)));
            DataContext = ordersTracking;
        }
        catch (DalException ex)
        {
            MessageBox.Show("Exception: " + ex.Message + " " + ex.InnerException?.Message);
        }
    }

    private void OrdersListview_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        if (use == "tracking")
        {
            OrderTracking order = (BO.OrderTracking)OrdersListview.SelectedItem;
            BO.Order selectedItem = bl.Order.GetDetails(order.ID);
            new OrdersWindow(selectedItem, use).Show();
        }
        else
        {
            OrderForList order = (BO.OrderForList)OrdersListview.SelectedItem;
            BO.Order selectedItem = bl.Order.GetDetails(order.ID);
            new OrdersWindow(selectedItem).ShowDialog();
        }

    }

    private void OrdersListview_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {

    }
}
