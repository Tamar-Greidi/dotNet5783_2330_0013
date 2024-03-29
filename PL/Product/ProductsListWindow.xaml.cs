﻿using BO;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace PL;

/// <summary>
/// Interaction logic for ProductsListWindow.xaml
/// </summary>
public partial class ProductsListWindow : Window
{
    BlApi.IBl bl = BlApi.Factory.Get();
    string user;
    public static BO.Cart cart = new Cart();

    public ProductsListWindow(string user)
    {
        InitializeComponent();
        this.user = user;
        CategoriesSelector.ItemsSource = Enum.GetValues(typeof(BO.categories));
        DataContext = new { user = user };
        if (user == "user")
        {
            ProductsListview.ItemsSource = bl.Product.GetAll();
            AddNewProduct.Visibility = Visibility.Hidden;
            Grouping();
        }
        else
        {
            ProductsListview.ItemsSource = bl.Product.GetCatalog();
            GoToCart.Visibility = Visibility.Hidden;
            Grouping();
        }
    }

    private void Grouping()
    {
        CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(ProductsListview.ItemsSource);
        PropertyGroupDescription groupDescription = new PropertyGroupDescription("Category");
        view.GroupDescriptions.Add(groupDescription);
    }

    private void AddNewProduct_Click(object sender, RoutedEventArgs e)
    {
        new ProductsWindow().ShowDialog();
        ProductsListview.ItemsSource = bl.Product.GetCatalog();
        Grouping();
    }

    private void CategoriesSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        BO.categories category = (BO.categories)CategoriesSelector.SelectedItem;
        if (user == "admin")
        {
            IEnumerable<BO.ProductForList> list = bl.Product.GetListProductForListByCategory(category);
            ProductsListview.ItemsSource = list;
            Grouping();
        }
        else
        {
            IEnumerable<BO.ProductItem> list = bl.Product.GetListProductItemByCategory(category);
            ProductsListview.ItemsSource = list;
            Grouping();
        }
    }

    private void ProductsListview_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        try
        {
            if (user == "admin")
            {
                ProductForList product = (ProductForList)ProductsListview.SelectedItem;
                BO.Product selectedItem = bl.Product.GetProductDetails(product.ID);
                new ProductsWindow(selectedItem).ShowDialog();
                ProductsListview.ItemsSource = bl.Product.GetCatalog();
                Grouping();
            }
            else
            {
                ProductItem product = (ProductItem)ProductsListview.SelectedItem;
                BO.ProductItem selectedItem = bl.Product.GetProductDetails(product.ID, cart);
                new ProductsWindow(selectedItem).ShowDialog();
                ProductsListview.ItemsSource = bl.Product.GetAll();
                Grouping();
            }
        }
        catch (DalException ex)
        {
            MessageBox.Show(ex.Message + " " + ex.InnerException?.Message);
        }
        catch (InvalidData ex)
        {
            MessageBox.Show("Exception: " + ex.Message);
        }
    }

    private void ProductsListview_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {

    }

    private void GoToCart_Click(object sender, RoutedEventArgs e)
    {
        new CartsListWindow().Show();
    }
}
public class AddNewProductConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        string stringValue = (string)value;
        if (stringValue == "user")
            return Visibility.Hidden;
        else
            return Visibility.Visible;
    }
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}