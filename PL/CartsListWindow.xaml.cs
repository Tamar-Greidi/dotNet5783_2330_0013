﻿using BlImplementation;
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
    /// Interaction logic for CartsListWindow.xaml
    /// </summary>
    public partial class CartsListWindow : Window
    {
        BlApi.IBl bl = new Bl();
        public CartsListWindow(BO.Cart cart)
        {
            InitializeComponent();
            CartsListview.ItemsSource = cart.Items;

        }

        private void CartsSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void CartsListview_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
