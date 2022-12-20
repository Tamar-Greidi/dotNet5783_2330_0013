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
    /// Interaction logic for ProductsWindow.xaml
    /// </summary>
    public partial class ProductsWindow : Window
    {
        BlApi.IBl bl = new Bl();
        string status; 
        public ProductsWindow()
        {
            InitializeComponent();
            CategoriesSelector.ItemsSource = Enum.GetValues(typeof(BO.categories));
            status = "add";
        }

        public ProductsWindow(BO.Product selectedItem)
        {
            InitializeComponent();
            CategoriesSelector.ItemsSource = Enum.GetValues(typeof(BO.categories));
            status = "update";
            txtID.Text = selectedItem.ID.ToString();
            txtName.Text = selectedItem.Name;
            CategoriesSelector.Text = selectedItem.Category.ToString();
            txtPrice.Text = selectedItem.Price.ToString();
            txtInStock.Text = selectedItem.InStock.ToString();
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

        private void Update(BO.Product product) => bl.Product.Update(product);
    }
}
