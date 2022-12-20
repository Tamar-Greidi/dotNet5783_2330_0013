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
    /// Interaction logic for NewProductWindow.xaml
    /// </summary>
    public partial class NewProductWindow : Window
    {
        BlApi.IBl bl = new Bl();

        public NewProductWindow()
        {
            InitializeComponent();
            CategoriesSelector.ItemsSource = Enum.GetValues(typeof(BO.categories));
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            TextBox.
        }
    }
}
