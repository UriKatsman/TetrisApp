using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace TetrisApp
{
    public partial class LoadingSign : UserControl
    {
        public LoadingSign()
        {
            InitializeComponent();
            Loaded += LoadingSign_Loaded;
            SizeChanged += LoadingSign_SizeChanged;

            // Set default values
            SpinnerColor = Brushes.Blue;
            BackgroundColor = Brushes.Transparent;
            StrokeThickness = 5;
        }

        private void LoadingSign_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateSpinnerPath();
        }

        private void LoadingSign_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            UpdateSpinnerPath();
        }

        private void UpdateSpinnerPath()
        {
            if (ActualWidth <= 0 || ActualHeight <= 0)
                return;

            double margin = 10 + StrokeThickness / 2;
            double radius = Math.Min(ActualWidth, ActualHeight) / 2 - margin;
            double centerX = ActualWidth / 2;
            double centerY = ActualHeight / 2;

            // Create Arc Path
            PathGeometry pathGeometry = new PathGeometry();
            PathFigure pathFigure = new PathFigure();

            // Set the start point at the top of the circle
            pathFigure.StartPoint = new Point(centerX, centerY - radius);

            // Add an arc segment that covers 1/4 of the circle (90 degrees)
            ArcSegment arcSegment = new ArcSegment();
            arcSegment.Point = new Point(centerX + radius, centerY);
            arcSegment.Size = new Size(radius, radius);
            arcSegment.SweepDirection = SweepDirection.Clockwise;
            arcSegment.IsLargeArc = false;

            pathFigure.Segments.Add(arcSegment);
            pathGeometry.Figures.Add(pathFigure);

            // Set the path data
            SpinnerPath.Data = pathGeometry;
        }

        #region Dependency Properties

        public static readonly DependencyProperty SpinnerColorProperty =
            DependencyProperty.Register("SpinnerColor", typeof(Brush), typeof(LoadingSign),
                new PropertyMetadata(Brushes.Blue, OnSpinnerPropertyChanged));

        public static readonly DependencyProperty BackgroundColorProperty =
            DependencyProperty.Register("BackgroundColor", typeof(Brush), typeof(LoadingSign),
                new PropertyMetadata(Brushes.Transparent));

        public static readonly DependencyProperty StrokeThicknessProperty =
            DependencyProperty.Register("StrokeThickness", typeof(double), typeof(LoadingSign),
                new PropertyMetadata(5.0, OnSpinnerPropertyChanged));

        private static void OnSpinnerPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is LoadingSign control)
            {
                control.UpdateSpinnerPath();
            }
        }

        public Brush SpinnerColor
        {
            get { return (Brush)GetValue(SpinnerColorProperty); }
            set { SetValue(SpinnerColorProperty, value); }
        }

        public Brush BackgroundColor
        {
            get { return (Brush)GetValue(BackgroundColorProperty); }
            set { SetValue(BackgroundColorProperty, value); }
        }

        public double StrokeThickness
        {
            get { return (double)GetValue(StrokeThicknessProperty); }
            set { SetValue(StrokeThicknessProperty, value); }
        }

        #endregion
    }

    // Helper converter to divide a value by 2
    public class DivideByTwoConverter : System.Windows.Data.IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is double doubleValue)
                return doubleValue / 2;
            return 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}