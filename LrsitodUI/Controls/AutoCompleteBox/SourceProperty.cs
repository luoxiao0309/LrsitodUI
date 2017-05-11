using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using System.Windows.Media;

namespace LrsitodUI.Controls
{
    public class SourceProperty : PropertyChangedBase
    {
        private string displayPath;
        public string DisplayPath
        {
            get { return displayPath; }
            set
            {
                displayPath = value;
                NotifyOfPropertyChange(() => DisplayPath);
            }
        }

        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                NotifyOfPropertyChange(() => Name);
            }
        }

        private bool isShow = true;
        public bool IsShow
        {
            get { return isShow; }
            set
            {
                isShow = value;
                NotifyOfPropertyChange(() => IsShow);
            }
        }

    }
}
