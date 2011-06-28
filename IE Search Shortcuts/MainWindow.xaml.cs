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
using System.Collections.ObjectModel;

namespace IE_Search_Shortcuts
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool nameChanged, keywordChanged, urlChanged;
        private ObservableCollection<Shortcut> shortcuts;

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
            // Create new shortcut
            Shortcut shortcut = new Shortcut();
            shortcut.Name = nameTxtBox.Text;
            shortcut.Keyword = keywordTxtBox.Text;
            shortcut.URL = urlTxtBox.Text;

            // TODO: Add shortcut to registry

            shortcuts.Add(shortcut);
            ResetFields();
        }

        private void addBtn_Click(object sender, RoutedEventArgs e)
        {
            AddShortcut();
            shortcutListBox.Focus();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            shortcuts = new ObservableCollection<Shortcut>();
            Shortcut s = new Shortcut()
            {
                Name = "Tom"
            };
            shortcuts.Add(s);

            shortcutListBox.DataContext = shortcuts;
        }

        private void ResetFields()
        {
            nameTxtBox.Text = "Add New Shortcut";
            nameChanged = false;
            keywordTxtBox.Text = "Keyword";
            keywordChanged = false;
            urlTxtBox.Text = "URL with %s in place of query";
            urlChanged = false;

            shortcutListBox.Focus();
        }
    }
}
