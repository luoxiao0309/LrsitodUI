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
using System.Collections;
using System.Reflection;

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
        public ListBox List = null;
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            Grid gr = VisualTreeHelper.GetChild(this, 0) as Grid;
            InText = gr.FindName("intext") as TextBox;
            SrPopup = gr.FindName("pop") as Popup;
            List = gr.FindName("list") as ListBox;
            InText.TextChanged += InText_TextChanged;
            List.SelectionChanged += List_SelectionChanged1;
            InitItemsSource();
        }

        private void List_SelectionChanged1(object sender, SelectionChangedEventArgs e)
        {
            List = sender as ListBox;

            this.SelectText = List.SelectedItem.ToString();
            SrPopup.IsOpen = false;
        }

        void InText_TextChanged(object sender, TextChangedEventArgs e)
        {
            InText = sender as TextBox;
            SrPopup.IsOpen = true;
            var temp = DisplayList.ToList<string>().FindAll(ex => ex.Contains(InText.Text));
            if (temp.Count == 0)
            {
                IsFound = false;
            }
            else
            {
                IsFound = true;
                foreach (var item in ItemsSource)
                {
                    
                }
                for (int i = 0; i < temp.Count; i++)
                {
                    //SourceProperty sp = ItemsSource.ToList<SourceProperty>().Find(ex => ex.DisplayPath == temp[i].DisplayPath);
                    //sp.IsShow = true;
                }
            }
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

        public static readonly DependencyProperty DisplayListProperty = DependencyProperty.Register(
                 "DisplayList",
                 typeof(ObservableCollection<string>),
                 typeof(AutoCompleteBox),
                 new PropertyMetadata());

        public ObservableCollection<string> DisplayList
        {
            get { return (ObservableCollection<string>)GetValue(DisplayListProperty); }
            set { SetValue(DisplayListProperty, value); }
        }


        public static readonly DependencyProperty ItemsSourceProperty = DependencyProperty.Register(
              "ItemsSource",
              typeof(IEnumerable),
              typeof(AutoCompleteBox),
              new PropertyMetadata());

        public IEnumerable ItemsSource
        {
            get { return (IEnumerable)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        //public static readonly DependencyProperty SelectItemProperty = DependencyProperty.Register(
        //      "SelectItem",
        //      typeof(en),
        //      typeof(AutoCompleteBox),
        //      new PropertyMetadata());

        //public Enumerable SelectItem
        //{
        //    get { return (Enumerable)GetValue(SelectItemProperty); }
        //    set { SetValue(SelectItemProperty, value); }
        //}

        public static readonly DependencyProperty DisplayMemberPathProperty = DependencyProperty.Register(
                "DisplayMemberPath",
                typeof(string),
                typeof(AutoCompleteBox),
                new PropertyMetadata());

        public string DisplayMemberPath
        {
            get { return (string)GetValue(DisplayMemberPathProperty); }
            set { SetValue(DisplayMemberPathProperty, value); }
        }


        public static readonly DependencyProperty IsFoundProperty = DependencyProperty.Register(
              "IsFound",
              typeof(bool),
              typeof(AutoCompleteBox),
              new PropertyMetadata(true));

        public bool IsFound
        {
            get { return (bool)GetValue(IsFoundProperty); }
            set { SetValue(IsFoundProperty, value); }
        }

        public static readonly DependencyProperty SelectTextProperty = DependencyProperty.Register(
              "SelectText",
              typeof(string),
              typeof(AutoCompleteBox),
              new PropertyMetadata());

        public string SelectText
        {
            get { return (string)GetValue(SelectTextProperty); }
            set { SetValue(SelectTextProperty, value); }
        }


        #endregion

        #region 方法
        public void InitItemsSource()
        {
            DisplayList = new ObservableCollection<string>();
            foreach (var temp in ItemsSource)
            {
                Type type = temp.GetType();
                PropertyInfo field = type.GetProperty(DisplayMemberPath);  //这里s就是你想要num
                string value = (string)field.GetValue(temp, null);
                DisplayList.Add(value);
            }
        }
        #endregion
    }
}
