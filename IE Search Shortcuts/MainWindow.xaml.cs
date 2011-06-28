using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace IE_Search_Shortcuts
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool nameChanged, keywordChanged, urlChanged;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void nameTxtBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (nameChanged == false)
            {
                nameTxtBox.Text = "";
            }
        }

        private void keywordTxtBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (keywordChanged == false)
            {
                keywordTxtBox.Text = "";
            }
        }

        private void urlTxtBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (urlChanged == false)
            {
                urlTxtBox.Text = "";
            }
        }

        private void nameTxtBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (nameTxtBox.Text != "")
            {
                nameChanged = true;
            }
        }

        private void keywordTxtBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (keywordTxtBox.Text != "")
            {
                keywordChanged = true;
            }
        }

        private void urlTxtBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (urlTxtBox.Text != "")
            {
                urlChanged = true;
            }
        }

        private void urlTxtBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab)
            {
                AddShortcut();
                shortcutListBox.Focus();
            }
        }

        private void AddShortcut()
        {
            
        }

        private void addBtn_Click(object sender, RoutedEventArgs e)
        {
            AddShortcut();
            shortcutListBox.Focus();
        }
    }
}
