using BlImplementation;
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
    /// Interaction logic for OrderListWindow.xaml
    /// </summary>
    public partial class OrderListWindow : Window
    {
        BlApi.IBl bl = new Bl();

        public OrderListWindow()
        {
            InitializeComponent();
            OrderListview.ItemsSource= bl.Order.Get();
        }

        private void OrderListview_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
