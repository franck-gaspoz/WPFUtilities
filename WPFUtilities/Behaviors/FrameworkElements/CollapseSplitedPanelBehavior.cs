using System.Windows;
using System.Windows.Controls;

using Microsoft.Xaml.Behaviors;

using WPFUtilities.Helpers;

namespace WPFUtilities.Behaviors.FrameworkElements
{
    /// <summary>
    /// collapse a split panel behavior
    /// </summary>
    public class CollapseSplitedPanelBehavior
        : Behavior<FrameworkElement>
    {
        #region properties

        /// <summary>
        /// panel default length
        /// </summary>
        public double DefaultLength
        {
            get => (double)GetValue(DefaultLengthProperty);
            set { SetValue(DefaultLengthProperty, value); }
        }

        /// <summary>
        /// get panel default length
        /// </summary>
        /// <param name="dependencyObject">dependency Object</param>
        /// <returns>default length</returns>
        public static double GetDefaultLength(DependencyObject dependencyObject)
            => (double)dependencyObject.GetValue(DefaultLengthProperty);

        /// <summary>
        /// set panel default length
        /// </summary>
        /// <param name="dependencyObject">dependency Object</param>
        /// <param name="value">vakue</param>
        public static void SetDefaultLength(DependencyObject dependencyObject, double value)
            => dependencyObject.SetValue(DefaultLengthProperty, value);

        /// <summary>
        /// default length property
        /// </summary>
        public static readonly DependencyProperty DefaultLengthProperty =
            DependencyProperty.RegisterAttached(
                "DefaultLength",
                typeof(double),
                typeof(CollapseSplitedPanelBehavior),
                new PropertyMetadata(80d));

        /// <summary>
        /// panel length
        /// </summary>
        public double Length
        {
            get => (double)GetValue(LengthProperty);
            set { SetValue(LengthProperty, value); }
        }

        /// <summary>
        /// get panel length
        /// </summary>
        /// <param name="dependencyObject">dependency Object</param>
        /// <returns>length</returns>
        public static double GetLength(DependencyObject dependencyObject)
            => (double)dependencyObject.GetValue(LengthProperty);

        /// <summary>
        /// set length
        /// </summary>
        /// <param name="dependencyObject">dependency Object</param>
        /// <param name="value">value</param>
        public static void SetLength(DependencyObject dependencyObject, double value)
            => dependencyObject.SetValue(LengthProperty, value);

        /// <summary>
        /// length property
        /// </summary>
        public static readonly DependencyProperty LengthProperty =
            DependencyProperty.RegisterAttached(
                "Length",
                typeof(double),
                typeof(CollapseSplitedPanelBehavior),
                new PropertyMetadata(80d));

        /// <summary>
        /// row
        /// </summary>
        public int Row
        {
            get => (int)GetValue(RowProperty);
            set { SetValue(RowProperty, value); }
        }

        /// <summary>
        /// get row
        /// </summary>
        /// <param name="dependencyObject">dependency Object</param>
        /// <returns>row</returns>
        public static int GetRow(DependencyObject dependencyObject)
            => (int)dependencyObject.GetValue(RowProperty);

        /// <summary>
        /// set row
        /// </summary>
        /// <param name="dependencyObject">dependency Object</param>
        /// <param name="value">value</param>
        public static void SetRow(DependencyObject dependencyObject, int value)
            => dependencyObject.SetValue(RowProperty, value);

        /// <summary>
        /// row property
        /// </summary>
        public static readonly DependencyProperty RowProperty =
            DependencyProperty.RegisterAttached(
                "Row",
                typeof(int),
                typeof(CollapseSplitedPanelBehavior),
                new PropertyMetadata(-1));

        /// <summary>
        /// column
        /// </summary>
        public int Col
        {
            get => (int)GetValue(ColProperty);
            set { SetValue(ColProperty, value); }
        }

        /// <summary>
        /// get column
        /// </summary>
        /// <param name="dependencyObject">dependency Object</param>
        /// <returns>column</returns>
        public static int GetCol(DependencyObject dependencyObject)
            => (int)dependencyObject.GetValue(ColProperty);

        /// <summary>
        /// set column
        /// </summary>
        /// <param name="dependencyObject">dependencyObject</param>
        /// <param name="value">value</param>
        public static void SetCol(DependencyObject dependencyObject, int value)
            => dependencyObject.SetValue(ColProperty, value);

        /// <summary>
        /// column property
        /// </summary>
        public static readonly DependencyProperty ColProperty =
            DependencyProperty.RegisterAttached(
                "Col",
                typeof(int),
                typeof(CollapseSplitedPanelBehavior),
                new PropertyMetadata(-1));

        /// <summary>
        /// is collapsed
        /// </summary>
        public bool IsCollapsed
        {
            get => (bool)GetValue(IsCollapsedProperty);
            set { SetValue(IsCollapsedProperty, value); }
        }

        /// <summary>
        /// get is collapsed
        /// </summary>
        /// <param name="dependencyObject">dependency Object</param>
        /// <returns>is collapsed</returns>

        public static bool GetIsCollapsed(DependencyObject dependencyObject)
            => (bool)dependencyObject.GetValue(IsCollapsedProperty);

        /// <summary>
        /// set is collapsed
        /// </summary>
        /// <param name="dependencyObject">dependency Object</param>
        /// <param name="value">value</param>
        public static void SetIsCollapsed(DependencyObject dependencyObject, bool value)
            => dependencyObject.SetValue(IsCollapsedProperty, value);

        /// <summary>
        /// is collapsed
        /// </summary>
        public static readonly DependencyProperty IsCollapsedProperty =
            DependencyProperty.RegisterAttached(
                "IsCollapsed",
                typeof(bool),
                typeof(CollapseSplitedPanelBehavior),
                new PropertyMetadata(false, IsCollapsedChanged));

        #endregion

        /// <summary>
        /// is associated
        /// </summary>
        public bool IsAssociated { get; protected set; }

        /// <summary>
        /// cache is initialized
        /// </summary>
        public bool CacheInitialized { get; protected set; }

        GridLength _length;

        Grid _grid;

        int _row;
        bool _applyOnRow => _row > -1;

        int _col;
        bool _applyOnCol => _col > -1;

        /// <inheritdoc/>
        protected override void OnAttached()
            => InitTracking(AssociatedObject);

        /// <inheritdoc/>
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
            if (d != null && e.NewValue is bool isCollapsed
                && d is CollapseSplitedPanelBehavior obj
                && obj.IsAssociated)
                obj.CollapsedChanged(isCollapsed);
        }

        void CollapsedChanged(bool isCollapsed)
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
