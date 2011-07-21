using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Win32;
using System.Collections.ObjectModel;

namespace IESearchShortcuts
{
    class Shortcut
    {
        private const string SEARCH_URL_KEY = "Software\\Microsoft\\Internet Explorer\\SearchUrl";

        public string Name { get; set; }
        public string Keyword { get; set; }
        public string URL { get; set; }
        public string Icon
        {
            get
            {
                try
                {
                    Uri searchUri = new Uri(this.URL);
                    return "http://" + searchUri.Host + "/favicon.ico";
                }
                catch (UriFormatException e)
                {
                    return "pack://application:,,,/IE%20Search%20Shortcuts;component/images/globe.png";

                }
            }
        }
        private string CurrentKeyword; // If the shortcut is not new, this is the existing key

        Shortcut(string name, string keyword, string url)
        {
            this.Name = name;
            this.Keyword = keyword;
            this.URL = url;
            this.CurrentKeyword = keyword;
        }

        public Shortcut()
        {
        }

        public bool Save()
        {
            RegistryKey regKey = Registry.CurrentUser.CreateSubKey(SEARCH_URL_KEY + "\\" + this.Keyword);

            if (regKey.GetValue("") != null)
            {
                return false;
            }

            regKey.SetValue("", this.URL);
            regKey.SetValue("ShortcutName", this.Name);
            this.CurrentKeyword = this.Keyword;

            return true;
        }

        public bool Delete()
        {
            if (this.CurrentKeyword != null)
            {
                Registry.CurrentUser.OpenSubKey(SEARCH_URL_KEY, true).DeleteSubKey(this.CurrentKeyword);
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Update()
        {
            if (this.CurrentKeyword != null)
            {
                this.Delete(); // Delete the old record
                this.CurrentKeyword = this.Keyword;

                this.Save();

                return true;
            }
            else
            {
                return false;
            }
        }

        public static ObservableCollection<Shortcut> LoadShortcuts() {
            ObservableCollection<Shortcut> shortcuts = new ObservableCollection<Shortcut>();

            RegistryKey searchUrls = Registry.CurrentUser.OpenSubKey(SEARCH_URL_KEY);

            string[] subKeys = searchUrls.GetSubKeyNames();

            foreach (string key in subKeys)
            {
                RegistryKey regKey = Registry.CurrentUser.OpenSubKey(SEARCH_URL_KEY + "\\" + key);

                Shortcut shortcut = new Shortcut(
                    regKey.GetValue("ShortcutName") == null ? "" : regKey.GetValue("ShortcutName").ToString(),
                    key,
                    regKey.GetValue("").ToString());
                
                shortcuts.Add(shortcut);
            }

            return shortcuts;
        }
    }
}
