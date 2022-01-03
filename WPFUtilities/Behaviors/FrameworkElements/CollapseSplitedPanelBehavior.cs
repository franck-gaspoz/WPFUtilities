using System.Windows;
using System.Windows.Controls;

using Microsoft.Xaml.Behaviors;

namespace WPFUtilities.Behaviors.FrameworkElements
{
    /// <summary>
    /// parameters: IsCollapsed , target
    /// </summary>
    public class CollapseSplitedPanelBehavior
        : Behavior<FrameworkElement>
    {
        #region props
        public double DefaultLength
        {
            get => (double)GetValue(DefaultLengthProperty);
            set { SetValue(DefaultLengthProperty, value); }
        }

        public static double GetDefaultLength(DependencyObject obj)
        {
            return (double)obj.GetValue(DefaultLengthProperty);
        }

        public static void SetDefaultLength(DependencyObject obj, double value)
        {
            obj.SetValue(DefaultLengthProperty, value);
        }

        public static readonly DependencyProperty DefaultLengthProperty =
            DependencyProperty.RegisterAttached(
                "DefaultLength",
                typeof(double),
                typeof(CollapseSplitedPanelBehavior),
                new PropertyMetadata(80d));

        public double Length
        {
            get => (double)GetValue(LengthProperty);
            set { SetValue(LengthProperty, value); }
        }

        public static double GetLength(DependencyObject obj)
        {
            return (double)obj.GetValue(LengthProperty);
        }

        public static void SetLength(DependencyObject obj, double value)
        {
            obj.SetValue(LengthProperty, value);
        }

        public static readonly DependencyProperty LengthProperty =
            DependencyProperty.RegisterAttached(
                "Length",
                typeof(double),
                typeof(CollapseSplitedPanelBehavior),
                new PropertyMetadata(80d));

        public int Row
        {
            get => (int)GetValue(RowProperty);
            set { SetValue(RowProperty, value); }
        }

        public static int GetRow(DependencyObject obj)
        {
            return (int)obj.GetValue(RowProperty);
        }

        public static void SetRow(DependencyObject obj, int value)
        {
            obj.SetValue(RowProperty, value);
        }

        public static readonly DependencyProperty RowProperty =
            DependencyProperty.RegisterAttached(
                "Row",
                typeof(int),
                typeof(CollapseSplitedPanelBehavior),
                new PropertyMetadata(-1));

        public int Col
        {
            get => (int)GetValue(ColProperty);
            set { SetValue(ColProperty, value); }
        }

        public static int GetCol(DependencyObject obj)
        {
            return (int)obj.GetValue(ColProperty);
        }

        public static void SetCol(DependencyObject obj, int value)
        {
            obj.SetValue(ColProperty, value);
        }

        public static readonly DependencyProperty ColProperty =
            DependencyProperty.RegisterAttached(
                "Col",
                typeof(int),
                typeof(CollapseSplitedPanelBehavior),
                new PropertyMetadata(-1));

        public bool IsCollapsed
        {
            get => (bool)GetValue(IsCollapsedProperty);
            set { SetValue(IsCollapsedProperty, value); }
        }

        public static bool GetIsCollapsed(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsCollapsedProperty);
        }

        public static void SetIsCollapsed(DependencyObject obj, bool value)
        {
            obj.SetValue(IsCollapsedProperty, value);
        }

        public static readonly DependencyProperty IsCollapsedProperty =
            DependencyProperty.RegisterAttached(
                "IsCollapsed",
                typeof(bool),
                typeof(CollapseSplitedPanelBehavior),
                new PropertyMetadata(false, IsCollapsedChanged));

        #endregion

        public bool IsAssociated { get; protected set; }

        public bool CacheInitialized { get; protected set; }

        GridLength _length;

        Grid _grid;

        int _row;
        bool _applyOnRow => _row > -1;

        int _col;
        bool _applyOnCol => _col > -1;

        protected override void OnAttached()
            => InitTracking(AssociatedObject);

        protected override void OnDetaching()
            => EndTracking(AssociatedObject);

        void InitTracking(DependencyObject d)
        {
            CacheInit(d);
            CollapsedChanged(IsCollapsed);
            AssociatedObject.SizeChanged += AssociatedObject_SizeChanged;
        }

        void EndTracking(DependencyObject d)
        {
            AssociatedObject.SizeChanged -= AssociatedObject_SizeChanged;
            _grid = null;
        }

        void CacheInit(DependencyObject d)
        {
            if (IsAssociated || CacheInitialized)
                return;
            _length = new GridLength(Length);
            _grid = WPFHelper.FindAncestor<Grid>(d);
            _row = Row;
            _col = Col;
            CacheInitialized = true;
            IsAssociated = true;
        }

        void AssociatedObject_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (!IsAssociated)
                return;
            if (_applyOnRow)
            {
                var l = e.NewSize.Height;
                _length = new GridLength(l);
                Length = l;
            }
            else
            {
                if (_applyOnCol)
                {
                    var l = e.NewSize.Width;
                    _length = new GridLength(l);
                    Length = l;
                }
            }
        }

        static GridLength _length0 = new GridLength(0d);

        static void IsCollapsedChanged(
            DependencyObject d,
            DependencyPropertyChangedEventArgs e)
        {
            if (
                d != null && e.NewValue is bool isCollapsed
                && d is CollapseSplitedPanelBehavior obj
                && obj.IsAssociated)
                obj.CollapsedChanged(isCollapsed);
        }
        public void CollapsedChanged(bool isCollapsed)
        {
            if (_applyOnRow)
            {
                var rd = _grid.RowDefinitions[_row];
                if (isCollapsed)
                {
                    _length = rd.Height;
                    rd.Height = _length0;
                    rd.MaxHeight = 0;
                }
                else
                {
                    if (_length.Value <= 1)
                        _length = new GridLength(DefaultLength);
                    rd.Height = _length;
                    rd.MaxHeight = double.PositiveInfinity;
                }
            }
            else
            {
                if (_applyOnCol)
                {
                    var cd = _grid.ColumnDefinitions[_col];
                    if (isCollapsed)
                    {
                        _length = cd.Width;
                        cd.Width = _length0;
                        cd.MaxWidth = 0;
                    }
                    else
                    {
                        if (_length.Value <= 1)
                            _length = new GridLength(DefaultLength);
                        cd.Width = _length;
                        cd.MaxWidth = double.PositiveInfinity;
                    }
                }
            }
        }
    }
}
