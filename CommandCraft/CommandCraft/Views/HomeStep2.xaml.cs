﻿using System;
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
using CommandCraft.ViewModels;

namespace CommandCraft.Views
{
    /// <summary>
    /// Interaction logic for HomeStep2.xaml
    /// </summary>
    public partial class HomeStep2 : UserControl
    {
        public HomeStep2(MainWindowViewModel mainWindowViewModel)
        {
            InitializeComponent();
            DataContext = new HomeStep2ViewModel(mainWindowViewModel);
        }
    }
}
