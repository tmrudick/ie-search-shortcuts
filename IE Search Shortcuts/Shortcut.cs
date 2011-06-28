using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IESearchShortcuts
{
    class Shortcut
    {
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
    }
}
