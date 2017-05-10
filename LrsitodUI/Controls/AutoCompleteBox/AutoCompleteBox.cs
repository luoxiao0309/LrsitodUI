using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;

namespace LrsitodUI.Controls
{
    public class AutoCompleteBox : Control
    {
        static AutoCompleteBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(AutoCompleteBox), new FrameworkPropertyMetadata(typeof(AutoCompleteBox)));
        }

        public TextBox InText = null;
        public Popup SrPopup = null;
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            Grid gr = VisualTreeHelper.GetChild(this, 0) as Grid;
            InText = gr.FindName("intext") as TextBox;
            SrPopup = gr.FindName("pop") as Popup;
            InText.TextChanged += InText_TextChanged;
        }

        void InText_TextChanged(object sender, TextChangedEventArgs e)
        {
            InText = sender as TextBox;
            SrPopup.IsOpen = true;
            var temp = ItemsSource.ToList<SourceList>().FindAll(ex => ex.DisplayrPath == InText.Text);
        }

        #region 基础属性
        public static readonly DependencyProperty IsPopOpenProperty = DependencyProperty.Register(
                "IsPopOpen",
                typeof(bool),
                typeof(AutoCompleteBox),
                new PropertyMetadata(false));

        public bool IsPopOpen
        {
                get { return (bool) GetValue(IsPopOpenProperty); }
                set { SetValue(IsPopOpenProperty, value); }
        }

        public static readonly DependencyProperty ItemsSourceProperty = DependencyProperty.Register(
              "ItemsSource",
              typeof(ObservableCollection<SourceList>),
              typeof(AutoCompleteBox),
              new PropertyMetadata(new ObservableCollection<SourceList>()));

        public ObservableCollection<SourceList> ItemsSource
        {
            get { return (ObservableCollection<SourceList>)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }
        #endregion
    }
}
