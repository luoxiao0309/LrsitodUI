
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Collections;
using System.Windows.Media;
using System.Collections.ObjectModel;
using System.Windows.Controls.Primitives;
using System.Windows.Interop;
using System.Runtime.InteropServices;

namespace LrsitodUI.Controls
{
    public class PopupEx : Popup
    {
        public static readonly DependencyProperty PopupPlacementTargetProperty = DependencyProperty.Register(
               "PopupPlacementTarget",
               typeof(DependencyObject),
               typeof(PopupEx),
               new PropertyMetadata(
                      null,
                      new PropertyChangedCallback(PopupPlacementTargetChanged)));
        /// <summary>
        /// 父级对象
        /// </summary>
        public DependencyObject PopupPlacementTarget
        {
            get { return (DependencyObject)GetValue(PopupPlacementTargetProperty); }
            set { SetValue(PopupPlacementTargetProperty, value); }
        }

        private static void PopupPlacementTargetChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != null)
            {
                DependencyObject popupPopupPlacementTarget = e.NewValue as DependencyObject;
                Popup pop = d as Popup;

                Window w = Window.GetWindow(popupPopupPlacementTarget);
                if (null != w)
                {
                    w.LocationChanged += delegate
                    {
                        var offset = pop.HorizontalOffset;
                        pop.HorizontalOffset = offset + 1;
                        pop.HorizontalOffset = offset;
                    };
                    w.SizeChanged += delegate
                    {
                        var offset = pop.HorizontalOffset;
                        pop.HorizontalOffset = offset + 1;
                        pop.HorizontalOffset = offset;
                    };
                }
            }
        }

        //是否最前默认为非最前（false）  解锁输入法
        public static DependencyProperty TopmostProperty = Window.TopmostProperty.AddOwner(typeof(PopupEx), new FrameworkPropertyMetadata(false, OnTopmostChanged));
        public bool Topmost
        {
            get { return (bool)GetValue(TopmostProperty); }
            set { SetValue(TopmostProperty, value); }
        }
        private static void OnTopmostChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            (obj as PopupEx).UpdateWindow();
        }

        /// <summary>  
        /// 重写拉开方法，置于非最前  
        /// </summary>  
        /// <param name="e"></param>  
        protected override void OnOpened(EventArgs e)
        {
            UpdateWindow();
        }

        /// <summary>  
        /// 刷新Popup层级  
        /// </summary>  
        private void UpdateWindow()
        {
            var hwnd = ((HwndSource)PresentationSource.FromVisual(this.Child)).Handle;
            RECT rect;
            if (NativeMethods.GetWindowRect(hwnd, out rect))
            {
                NativeMethods.SetWindowPos(hwnd, Topmost ? -1 : -2, rect.Left, rect.Top, (int)this.Width, (int)this.Height, 0);
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }
        #region P/Invoke imports & definitions
        public static class NativeMethods
        {


            [DllImport("user32.dll")]
            [return: MarshalAs(UnmanagedType.Bool)]
            internal static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);
            [DllImport("user32", EntryPoint = "SetWindowPos")]
            internal static extern int SetWindowPos(IntPtr hWnd, int hwndInsertAfter, int x, int y, int cx, int cy, int wFlags);
        }
        #endregion
    }
}




