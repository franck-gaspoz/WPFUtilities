using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

using WPFUtilities.Extensions.DependencyObjects;
using WPFUtilities.Extensions.FrameworkElements;

namespace SampleApp.Components.UI
{
    /// <summary>
    /// tab item header counter: decorates the tab item header with a value: TabItemHeader (x)
    /// </summary>
    public static class TabItemHeaderCount
    {
        #region counter

        /// <summary>
        /// get counter
        /// </summary>
        /// <param name="dependencyObject">dependency object</param>
        /// <returns>value</returns>
        public static object GetCount(DependencyObject dependencyObject) => dependencyObject.GetValue(CountProperty);

        /// <summary>
        /// set adjust column size modes
        /// </summary>
        /// <param name="dependencyObject">dependency Object</param>
        /// <param name="Count">Count</param>
        public static void SetCount(DependencyObject dependencyObject, object Count)
        {
            if (CountProperty == null) return;
            dependencyObject.SetValue(CountProperty, Count);
        }

        /// <summary>
        /// margin property
        /// </summary>
        public static readonly DependencyProperty CountProperty =
            DependencyObjectExtensions.RegisterAttached(
                "Count",
                typeof(object),
                typeof(TabItemHeaderCount),
                new UIPropertyMetadata(null, CountChanged));

        #endregion

        static string Decorates(object text, object value)
            => text + " (" + value + ")";

        static void CountChanged(
            DependencyObject dependencyObject,
            DependencyPropertyChangedEventArgs eventArgs)
        {
            if (DesignerProperties.GetIsInDesignMode(dependencyObject)) return;
            if (!(dependencyObject is TabItem tabItem)) return;

            tabItem.OnLoaded((routed) =>
            {
                var counter = tabItem.GetValue(CountProperty);
                if (tabItem.Header == null)
                    tabItem.Header = Decorates(tabItem.Header, counter);
                else
                {
                    var header = tabItem.Header.ToString();
                    int i = header.IndexOf('(');
                    if (i > -1)
                        header = header.Substring(0, i - 1);
                    tabItem.Header = Decorates(header, counter);
                }
            });
        }
    }
}
