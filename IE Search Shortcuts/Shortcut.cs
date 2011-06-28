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

        private bool isNew;

        Shortcut(string name, string keyword, string url)
        {
            this.isNew = false;
            this.Name = name;
            this.Keyword = keyword;
            this.URL = url;
        }

        public Shortcut()
        {
            this.isNew = true;
        }

        public bool Save()
        {
            RegistryKey regKey = Registry.CurrentUser.CreateSubKey(SEARCH_URL_KEY + "\\" + this.Keyword);
            regKey.SetValue("", this.URL);
            regKey.SetValue("ShortcutName", this.Name);

            return true;
        }

        public bool Delete()
        {
            Registry.CurrentUser.OpenSubKey(SEARCH_URL_KEY, true).DeleteSubKey(this.Keyword);

            return true;
        }

        public static ObservableCollection<Shortcut> LoadShortcuts() {
            ObservableCollection<Shortcut> shortcuts = new ObservableCollection<Shortcut>();

            RegistryKey searchUrls = Registry.CurrentUser.OpenSubKey(SEARCH_URL_KEY);

            string[] subKeys = searchUrls.GetSubKeyNames();

            foreach (string key in subKeys)
            {
                RegistryKey regKey = Registry.CurrentUser.OpenSubKey(SEARCH_URL_KEY + "\\" + key);

                Shortcut shortcut = new Shortcut() {
                    Name = regKey.GetValue("ShortcutName") == null ? "" : regKey.GetValue("ShortcutName").ToString(),
                    Keyword = key,
                    URL = regKey.GetValue("").ToString()
                };
                
                shortcuts.Add(shortcut);
            }

            return shortcuts;
        }
    }
}
