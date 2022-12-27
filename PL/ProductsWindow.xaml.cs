using BlApi;
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
        int debily = 0;
        string status, user;
        BO.Cart cart;
        public ProductsWindow()
        {
            InitializeComponent();
            CategoriesSelector.ItemsSource = Enum.GetValues(typeof(BO.categories));
            status = "add";
        }

        public ProductsWindow(BO.Product selectedItem)
        {
            InitializeComponent();
            user = "admin";
            CategoriesSelector.ItemsSource = Enum.GetValues(typeof(BO.categories));
            status = "update";
            txtID.Text = selectedItem.ID.ToString();
            txtName.Text = selectedItem.Name;
            CategoriesSelector.Text = selectedItem.Category.ToString();
            txtPrice.Text = selectedItem.Price.ToString();
            txtInStock.Text = selectedItem.InStock.ToString();
            txtAmount.Visibility = Visibility.Hidden;
            lblAmount.Visibility = Visibility.Hidden;
            btnAddToCart.Visibility = Visibility.Hidden;
        }

        public ProductsWindow(BO.ProductItem selectedItem, BO.Cart cart)
        {
            InitializeComponent();
            user = "customer";
            this.cart = cart;
            CategoriesSelector.ItemsSource = Enum.GetValues(typeof(BO.categories));
            txtID.Text = selectedItem.ID.ToString();
            txtID.IsEnabled = false;
            txtName.Text = selectedItem.Name;
            txtName.IsEnabled = false;
            CategoriesSelector.Visibility = Visibility.Hidden;
            txtCategory.Text = selectedItem.Category.ToString();
            txtCategory.IsEnabled = false;
            txtPrice.Text = selectedItem.Price.ToString();
            txtPrice.IsEnabled = false;
            txtAmount.Text = selectedItem.Amount.ToString();
            txtAmount.IsEnabled = false;
            txtInStock.Text = selectedItem.InStock.ToString();
            txtInStock.IsEnabled = false;
            btnSave.Visibility = Visibility.Hidden;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            int ID = Convert.ToInt32(txtID.Text);
            string? name = Convert.ToString(txtName.Text);
            double price = Convert.ToDouble(txtPrice.Text);
            BO.categories category = (BO.categories)CategoriesSelector.SelectedItem;
            int inStock = Convert.ToInt32(txtInStock.Text);
            BO.Product product = new BO.Product
            {
                ID = ID,
                Name = name,
                Price = price,
                Category = category,
                InStock = inStock
            };
            if (status == "add")
                Add(product);
            else
                Update(product);
            Close();
        }

        private void Add(BO.Product product) => bl.Product.Add(product);

        private void AddToCartButton_Click(object sender, RoutedEventArgs e)
        {
            int ID = Convert.ToInt32(txtID.Text);
            if (cart.Items == null)
                cart.Items = new List<BO.OrderItem>();
            bl.Cart.Add(cart, ID);
            Close();
        }

        private void Update(BO.Product product) => bl.Product.Update(product);
    }
}
