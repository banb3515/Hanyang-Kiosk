using HanyangKiosk.Utils;

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace HanyangKiosk.Custom
{
    /// <summary>
    /// CircleProgressBar.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class CircleProgressBar : UserControl
    {
        public CircleProgressBar()
        {
            try
            {
                InitializeComponent();
            }
            catch (Exception ex)
            {
                FileManager.WriteLog($"[Exception] {ex.Message}\n - {ex.StackTrace}");
            }
        }

        public int Diameter
        {
            get { return (int)GetValue(DiameterProperty); }
            set
            {
                if (value < 10)
                    value = 10;
                SetValue(DiameterProperty, value);
            }
        }

        public static readonly DependencyProperty DiameterProperty =
            DependencyProperty.Register("Diameter", typeof(int), typeof(CircleProgressBar), new PropertyMetadata(20, OnDiameterPropertyChanged));
        
        private static void OnDiameterPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            try
            {
                var vm = (CircleProgressBar)d;
                d.CoerceValue(CenterProperty);
                d.CoerceValue(RadiusProperty);
                d.CoerceValue(InnerRadiusProperty);
            }
            catch (Exception ex)
            {
                FileManager.WriteLog($"[Exception] {ex.Message}\n - {ex.StackTrace}");
            }
        }

        public int Radius
        {
            get { return (int)GetValue(RadiusProperty); }
            set { SetValue(RadiusProperty, value); }
        }

        public static readonly DependencyProperty RadiusProperty =
            DependencyProperty.Register("Radius", typeof(int), typeof(CircleProgressBar), new PropertyMetadata(15, null, OnCoerceRadius));

        private static object OnCoerceRadius(DependencyObject d, object baseValue)
        {
            try
            {
                var control = (CircleProgressBar)d;
                int newRadius = (int)(control.GetValue(DiameterProperty)) / 2;
                return newRadius;
            }
            catch (Exception ex)
            {
                FileManager.WriteLog($"[Exception] {ex.Message}\n - {ex.StackTrace}");
                return null;
            }
        }

        public int InnerRadius
        {
            get { return (int)GetValue(InnerRadiusProperty); }
            set { SetValue(InnerRadiusProperty, value); }
        }

        public static readonly DependencyProperty InnerRadiusProperty =
            DependencyProperty.Register("InnerRadius", typeof(int), typeof(CircleProgressBar), new PropertyMetadata(2, null, OnCoerceInnerRadius));

        private static object OnCoerceInnerRadius(DependencyObject d, object baseValue)
        {
            try
            {
                var control = (CircleProgressBar)d;
                int newInnerRadius = (int)(control.GetValue(DiameterProperty)) / 4;
                return newInnerRadius;
            }
            catch (Exception ex)
            {
                FileManager.WriteLog($"[Exception] {ex.Message}\n - {ex.StackTrace}");
                return null;
            }
        }

        public Point Center
        {
            get { return (Point)GetValue(CenterProperty); }
            set { SetValue(CenterProperty, value); }
        }

        public static readonly DependencyProperty CenterProperty =
            DependencyProperty.Register("Center", typeof(Point), typeof(CircleProgressBar), new PropertyMetadata(new Point(15, 15), null, OnCoerceCenter));

        private static object OnCoerceCenter(DependencyObject d, object baseValue)
        {
            try
            {
                var control = (CircleProgressBar)d;
                int newCenter = (int)(control.GetValue(DiameterProperty)) / 2;
                return new Point(newCenter, newCenter);
            }
            catch (Exception ex)
            {
                FileManager.WriteLog($"[Exception] {ex.Message}\n - {ex.StackTrace}");
                return null;
            }
        }

        public Color Color1
        {
            get { return (Color)GetValue(Color1Property); }
            set { SetValue(Color1Property, value); }
        }

        public static readonly DependencyProperty Color1Property =
            DependencyProperty.Register("Color1", typeof(Color), typeof(CircleProgressBar), new PropertyMetadata(Colors.Green));

        public Color Color2
        {
            get { return (Color)GetValue(Color2Property); }
            set { SetValue(Color2Property, value); }
        }

        public static readonly DependencyProperty Color2Property =
            DependencyProperty.Register("Color2", typeof(Color), typeof(CircleProgressBar), new PropertyMetadata(Colors.Transparent));
    }
}
