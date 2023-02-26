using BlApi;
using System.Windows;

namespace PL;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>

public partial class MainWindow : Window
{
    IBl bl = BlApi.Factory.Get();

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

    private void Simulator_Click(object sender, RoutedEventArgs e) => new SimulatorWindow().Show();
}
