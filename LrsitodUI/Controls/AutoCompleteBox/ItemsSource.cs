using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using System.Windows.Media;

namespace LrsitodUI.Controls
{
    public class SourceList : PropertyChangedBase
    {
        private string displayPath;
        public string DisplayrPath
        {
            get { return displayPath; }
            set
            {
                displayPath = value;
                NotifyOfPropertyChange(() => DisplayrPath);
            }
        }
    }
}
