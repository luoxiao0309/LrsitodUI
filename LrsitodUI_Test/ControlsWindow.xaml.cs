using System;
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
using System.Windows.Shapes;
using LrsitodUI;
using LrsitodUI.Controls;

namespace LrsitodUI_Test
{
    /// <summary>
    /// ControlsWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ControlsWindow : Window
    {
        public ControlsWindow()
        {
            InitializeComponent();

            compe.ItemsSource = Init();
        }

        public List<SourceProperty> Init()
        {
            List<SourceProperty> result = new List<SourceProperty>();
            for (int i = 0; i < 10; i++)
            {
                SourceProperty sp = new SourceProperty();
                sp.DisplayPath = i.ToString() + (i + 1).ToString();
                result.Add(sp);
            }
            return result;
        }
    }
}
