using BlApi;
using DalApi;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PL;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>

public partial class MainWindow : Window
{
    IBl bl = BlApi.Factory.Get();
    int debily = 0;

    public MainWindow()
    {
        InitializeComponent();
        Products.Visibility = Visibility.Hidden;
        Orders.Visibility = Visibility.Hidden;
    }

    private void AdminButton_Click(object sender, RoutedEventArgs e)
    {
        Admin.Visibility = Visibility.Hidden;
        NewOrder.Visibility = Visibility.Hidden;
        Track.Visibility = Visibility.Hidden;
        Products.Visibility = Visibility.Visible;
        Orders.Visibility = Visibility.Visible;
    }

    private void NewOrderButton_Click(object sender, RoutedEventArgs e) => new ProductsListWindow("user").Show();

    private void Track_Click(object sender, RoutedEventArgs e) => new OrdersListWindow("tracking").Show();

    private void ShowProductsButton_Click(object sender, RoutedEventArgs e) => new ProductsListWindow("admin").Show();

    private void ShowOrdersButton_Click(object sender, RoutedEventArgs e) => new OrdersListWindow().Show();
}