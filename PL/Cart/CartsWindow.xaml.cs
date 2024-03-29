﻿using BO;
using System;
using System.Windows;
using System.Windows.Input;

namespace PL;

/// <summary>
/// Interaction logic for CartsWindow.xaml
/// </summary>
public partial class CartsWindow : Window
{
    BlApi.IBl bl = BlApi.Factory.Get();
    BO.OrderItem cartItem;
    bool AmountChange = false;
    public CartsWindow(OrderItem cartItem)
    {
        InitializeComponent();
        this.cartItem = cartItem;
        txtID.IsEnabled = false;
        txtName.IsEnabled = false;
        txtProductID.IsEnabled = false;
        txtPrice.IsEnabled = false;
        txtTotalPrice.IsEnabled = false;
        DataContext = cartItem;
    }

    private void DeleteButton_Click(object sender, RoutedEventArgs e)
    {
        cartItem.Amount = 0;
        bl.Cart.UpdateProductAmount(ProductsListWindow.cart, cartItem.ProductID, cartItem.Amount);
        Close();
    }

    private void Amount_LostMouseCapture(object sender, MouseEventArgs e)
    {
        if (!AmountChange)
            AmountChange = true;
        else
        {
            try
            {
                int amount = Convert.ToInt32(txtAmount.Text);
                bl.Cart.UpdateProductAmount(ProductsListWindow.cart, cartItem.ProductID, amount);
                Close();
            }
            catch (OutOfStock ex)
            {
                MessageBox.Show("Exception: " + ex.Message);
            }
        }
    }
}

