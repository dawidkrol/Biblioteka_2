﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Biblioteka_2
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        Module _module = null;
        public LoginWindow(Module module)
        {
            InitializeComponent();
            _module = module;
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            _module.login(LoginName.Text, passw.Password, this);
            if (_module.user != null)
            {
                main.Text = "Zalogowano";
                this.Close();
            }
            else
            {
                main.Text = "!!!BŁĄD!!!\nPodaj dane do połączenia z serwerem sql";
            }
        }
    }
}
