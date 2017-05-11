using System;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Data;
using System.Text.RegularExpressions;

namespace LrsitodUI
{
    public class IntToStringSourceConverter : IValueConverter
    {
        public object Convert(object value,
                       Type targetType,
                       object parameter,
                       System.Globalization.CultureInfo culture)
        {
            if (value != null)
            {
                return ((int)value).ToString();
            }
            return null;
        }
        public object ConvertBack(object value,
                             Type targetType,
                             object parameter,
                             System.Globalization.CultureInfo culture)
        {
            if (value != null && value.ToString() != "")
            {
                int i = 5000;
                try
                {
                    i = System.Convert.ToInt32((string)value);
                }
                catch { }
                return i;
            }
            {
                return 0;
            }
        }
    }

    public class BoolToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            {
                bool Bool = (bool)value;
                return (bool)(Bool) ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
            {
                return false;
            }

            if ((Visibility)value == Visibility.Visible)
            {
                return true;
            }

            return false;
        }
    }

    public class BoolToNVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            {
                bool Bool = (bool)value;
                return (bool)(!Bool) ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
            {
                return true;
            }

            if ((Visibility)value == Visibility.Visible)
            {
                return false;
            }

            return true;
        }
    }

    public class BoolToNBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            {
                bool Bool = (bool)value;
                return !Bool;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw null;
        }
    }

    public class IntToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            {
                int idex = (int)value;
                return idex == 0 ? Visibility.Collapsed : Visibility.Visible;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw null;
        }
    }

    public class IntToNVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            {
                int idex = (int)value;
                return idex == 0 ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw null;
        }
    }

    public class StringToImageSourceConverter : IValueConverter
    {
        public object Convert(object value,
                       Type targetType,
                       object parameter,
                       System.Globalization.CultureInfo culture)
        {
            if (value != null)
            {
                string path = value.ToString();

                if (path.ToLower().Contains("pack:"))
                {
                    return BitmapFromUri(new Uri(path));
                }
                else if (File.Exists(path))
                {
                    return BitmapFromFileWithCache(path);
                }

                else
                {
                    return null;
                }
            }
            return null;


        }
        public object ConvertBack(object value,
                             Type targetType,
                             object parameter,
                             System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public ImageSource BitmapFromUri(Uri source)
        {
            var bitmap = new BitmapImage();
            try
            {
                var temp = new BitmapImage(source);
                temp = null;
                lock (lockObj)
                {
                    bitmap.BeginInit();
                    bitmap.UriSource = source;
                    bitmap.CacheOption = BitmapCacheOption.OnLoad;
                    bitmap.EndInit();
                }
            }
            catch { }
            return bitmap;
        }

        private static object lockObj = new object();
        public ImageSource BitmapFromFileWithCache(String source)
        {
            var bitmap = new BitmapImage();
            try
            {
                if (File.Exists(source))
                {
                    using (Stream stream = File.OpenRead(source))
                    {
                        if (stream != null)
                        {
                            lock (lockObj)
                            {
                                bitmap.BeginInit();
                                try
                                {
                                    bitmap.StreamSource = stream;
                                    bitmap.CacheOption = BitmapCacheOption.OnLoad;
                                }
                                catch { }
                                bitmap.EndInit();
                                stream.Dispose();//释放占用的资源
                            }
                        }
                    }
                }
            }
            catch
            {
            }
            return bitmap;
        }
    }

    public class GroupBoxHeaderPahConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            PathGeometry pg = new PathGeometry();
            if (JudgeValuesIsNull(values))
                return pg;
            int idex = 0;
            double w = double.Parse(values[idex++].ToString()) - 1;
            double h = double.Parse(values[idex++].ToString()) - 2;
            double aw = double.Parse(values[idex++].ToString());
            double bw = double.Parse(values[idex++].ToString());
            double cw = double.Parse(values[idex++].ToString());

            if (w == 0 || h == 0 || aw == 0 || cw == 0)
                return pg;

            double xtmpw = 20;
            double xtmph = 14;
            double xwl = aw - xtmpw;
            double xwr = w - cw + xtmpw;
            double xwt = h - xtmph;

            PathFigure pf = new PathFigure();
            pf.StartPoint = new Point(0, 0);

            LineSegment ls = null;
            ls = new LineSegment();
            ls.Point = new Point(xwl, 0);
            ls = new LineSegment();
            ls.Point = new Point(xwl, 0);
            pf.Segments.Add(ls);
            ls = new LineSegment();
            ls.Point = new Point(aw, xwt);
            pf.Segments.Add(ls);
            ls = new LineSegment();
            ls.Point = new Point(aw + bw, xwt);
            pf.Segments.Add(ls);
            ls = new LineSegment();
            ls.Point = new Point(xwr, 0);
            pf.Segments.Add(ls);
            ls = new LineSegment();
            ls.Point = new Point(w, 0);
            pf.Segments.Add(ls);
            ls = new LineSegment();
            ls.Point = new Point(w, h);
            pf.Segments.Add(ls);
            ls = new LineSegment();
            ls.Point = new Point(0, h);
            pf.Segments.Add(ls);
            ls = new LineSegment();
            ls.Point = new Point(0, 0);
            pf.Segments.Add(ls);

            pg.Figures.Add(pf);

            return pg;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public bool JudgeValuesIsNull(object[] values)
        {
            bool bk = true;
            if (values == null)
                return bk;
            int count = values.Length;
            int num = 0;
            foreach (object o in values)
            {
                if (o == null)
                    num++;
            }
            if (num == 0)
                bk = false;
            return bk;
        }
    }

    public class IntToIsCheckedConverter : IValueConverter
    {
        public object Convert(object value,
                       Type targetType,
                       object parameter,
                       System.Globalization.CultureInfo culture)
        {
            if (value != null)
            {
                return int.Parse(value.ToString()) == int.Parse(parameter.ToString()) ? true : false;
            }
            return null;
        }
        public object ConvertBack(object value,
                             Type targetType,
                             object parameter,
                             System.Globalization.CultureInfo culture)
        {
            if (value != null)
            {
                return (bool)value == true ? int.Parse(parameter.ToString()) : -1;
            }
            return null;
        }
    }

    public class RadioButtonConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return !(bool)value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return !(bool)value;
        }
    }


    public class CountToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return Visibility.Collapsed;
            if ((int)value > 0)
                return Visibility.Visible;
            else
                return Visibility.Collapsed;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class StringToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return new SolidColorBrush(Colors.Gray);
            else
            {
                Color cor = (Color)ColorConverter.ConvertFromString(value.ToString());
                return new SolidColorBrush(cor);
            }

        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}

