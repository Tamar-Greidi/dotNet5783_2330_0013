using BlApi;
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

namespace PL
{
    /// <summary>
    /// Interaction logic for ProductsWindow.xaml
    /// </summary>
    public partial class ProductsWindow : Window
    {
        BlApi.IBl bl = BlApi.Factory.Get();
        string status = "";
        public ProductsWindow()
        {
            InitializeComponent();
            CategoriesSelector.ItemsSource = Enum.GetValues(typeof(BO.categories));
            status = "add";
            txtID.Visibility = Visibility.Hidden;
            lblID.Visibility = Visibility.Hidden;
            txtCategory.Visibility = Visibility.Hidden;
            txtAmount.Visibility = Visibility.Hidden;
            lblAmount.Visibility = Visibility.Hidden;
            btnAddToCart.Visibility = Visibility.Hidden;
            btnSave.Content = "Add";
        }

        public ProductsWindow(BO.ProductItem selectedItem)
        {
            InitializeComponent();
            CategoriesSelector.ItemsSource = Enum.GetValues(typeof(BO.categories));
            txtID.IsEnabled = false;
            txtName.IsEnabled = false;
            CategoriesSelector.Visibility = Visibility.Hidden;
            txtCategory.IsEnabled = false;
            txtPrice.IsEnabled = false;
            txtAmount.IsEnabled = false;
            txtInStock.IsEnabled = false;
            btnSave.Visibility = Visibility.Hidden;
            DataContext = selectedItem;
        }
        public ProductsWindow(BO.Product selectedItem)
        {
            InitializeComponent();
            CategoriesSelector.ItemsSource = Enum.GetValues(typeof(BO.categories));
            status = "update";
            txtID.IsEnabled = false;
            txtCategory.Visibility = Visibility.Hidden;
            txtAmount.Visibility = Visibility.Hidden;
            lblAmount.Visibility = Visibility.Hidden;
            btnAddToCart.Visibility = Visibility.Hidden;
            btnSave.Content = "Update";
            DataContext = selectedItem;
        }



        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            string? name = Convert.ToString(txtName.Text);
            double price = Convert.ToDouble(txtPrice.Text);
            BO.categories category = (BO.categories)CategoriesSelector.SelectedItem;
            int inStock = Convert.ToInt32(txtInStock.Text);
            if (status == "add")
            {
                BO.Product product = new BO.Product
                {
                    Name = name,
                    Price = price,
                    Category = category,
                    InStock = inStock
                };
                //Add(product);
                try
                {
                    bl.Product.Add(product);
                }
                catch (DalException ex)
                {
                    MessageBox.Show(ex.Message + " " + ex.InnerException?.Message);
                }
            }
            else
            {
                int ID = Convert.ToInt32(txtID.Text);
                BO.Product product = new BO.Product
                {
                    ID = ID,
                    Name = name,
                    Price = price,
                    Category = category,
                    InStock = inStock
                };
                Update(product);
            }
            Close();
        }

        //private void Add(BO.Product product) => bl.Product.Add(product);

        private void Update(BO.Product product) => bl.Product.Update(product);

        private void AddToCartButton_Click(object sender, RoutedEventArgs e)
        {
            int ID = Convert.ToInt32(txtID.Text);
            if (ProductsListWindow.cart.Items == null)
                ProductsListWindow.cart.Items = new List<BO.OrderItem>();
            bl.Cart.Add(ProductsListWindow.cart, ID);
            Close();
        }

        private void txtCategory_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
