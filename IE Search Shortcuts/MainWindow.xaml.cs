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

namespace IESearchShortcuts
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
                nameTxtBox.Foreground = SystemColors.ActiveCaptionTextBrush;
            }
        }

        private void keywordTxtBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (keywordChanged == false)
            {
                keywordTxtBox.Text = "";
                keywordTxtBox.Foreground = SystemColors.ActiveCaptionTextBrush;
            }
        }

        private void urlTxtBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (urlChanged == false)
            {
                urlTxtBox.Text = "";
                urlTxtBox.Foreground = SystemColors.ActiveCaptionTextBrush;
            }
        }

        private void nameTxtBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (nameTxtBox.Text != "")
            {
                nameChanged = true;
            }
            else
            {
                nameTxtBox.Foreground = SystemColors.InactiveCaptionBrush;
                nameTxtBox.Text = "Add New Shortcut";
            }
        }

        private void keywordTxtBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (keywordTxtBox.Text != "")
            {
                keywordChanged = true;
            }
            else
            {
                keywordTxtBox.Foreground = SystemColors.InactiveCaptionBrush;
                keywordTxtBox.Text = "Keyword";
            }
        }

        private void urlTxtBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (urlTxtBox.Text != "")
            {
                urlChanged = true;
            }
            else
            {
                urlTxtBox.Foreground = SystemColors.InactiveCaptionBrush;
                urlTxtBox.Text = "URL with %s in place of query";
            }
        }

        private Shortcut AddShortcut()
        {
            // Create new shortcut
            Shortcut shortcut = new Shortcut()
            {
                Name = nameTxtBox.Text,
                Keyword = keywordTxtBox.Text,
                URL = urlTxtBox.Text
            };

            // TODO: Add shortcut to registry

            shortcuts.Add(shortcut);
            ResetFields();

            shortcut.Save();

            return shortcut;
        }

        private void addBtn_Click(object sender, RoutedEventArgs e)
        {
            AddShortcut();
            shortcutListBox.Focus();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            shortcuts = Shortcut.LoadShortcuts();

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

            // Change brushes
            nameTxtBox.Foreground = SystemColors.InactiveCaptionBrush;
            keywordTxtBox.Foreground = SystemColors.InactiveCaptionBrush;
            urlTxtBox.Foreground = SystemColors.InactiveCaptionBrush; 

            shortcutListBox.Focus();
        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            Button b = (Button)sender;

            Shortcut s = (Shortcut)b.DataContext;

            shortcuts.Remove(s);
            s.Delete();
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox box = (TextBox)sender;

            Shortcut s = (Shortcut)box.DataContext;

            s.Update();
        }

        private void ListItem_MouseEnter(object sender, MouseEventArgs e)
        {
            ToggleDeleteBtn(sender as StackPanel, Visibility.Visible);
        }

        private bool ToggleDeleteBtn(StackPanel panel, Visibility visibility)
        {
            foreach (UIElement ele in panel.Children)
            {
                if (ele.GetType() == typeof(Button))
                {
                    ele.Visibility = visibility;
                    return true;
                }
            }

            return false;
        }

        private void Icon_Failed(object sender, ExceptionRoutedEventArgs e)
        {
            ((Image)sender).Source = new BitmapImage(new Uri("pack://application:,,,/IE%20Search%20Shortcuts;component/images/globe.png"));
        }

        private void ListItem_MouseLeave(object sender, MouseEventArgs e)
        {
            ToggleDeleteBtn(sender as StackPanel, System.Windows.Visibility.Hidden);
        }

        private void urlTxtBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab || e.Key == Key.Enter)
            {
                AddShortcut();

                // Stop tab
                e.Handled = true;

                nameTxtBox.Focus();
                urlChanged = false;
            }
        }
    }
}
