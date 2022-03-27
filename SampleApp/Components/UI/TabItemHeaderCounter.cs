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
    public static class TabItemHeaderCounter
    {
        #region counter

        /// <summary>
        /// get counter
        /// </summary>
        /// <param name="dependencyObject">dependency object</param>
        /// <returns>value</returns>
        public static object GetCounter(DependencyObject dependencyObject) => dependencyObject.GetValue(CounterProperty);

        /// <summary>
        /// set adjust column size modes
        /// </summary>
        /// <param name="dependencyObject">dependency Object</param>
        /// <param name="Counter">Counter</param>
        public static void SetCounter(DependencyObject dependencyObject, object Counter)
        {
            if (CounterProperty == null) return;
            dependencyObject.SetValue(CounterProperty, Counter);
        }

        /// <summary>
        /// margin property
        /// </summary>
        public static readonly DependencyProperty CounterProperty =
            DependencyObjectExtensions.RegisterAttached(
                "Counter",
                typeof(object),
                typeof(TabItemHeaderCounter),
                new UIPropertyMetadata(null, CounterChanged));

        #endregion

        static string Decorates(object text, object value)
            => text + " (" + value + ")";

        static void CounterChanged(
            DependencyObject dependencyObject,
            DependencyPropertyChangedEventArgs eventArgs)
        {
            if (DesignerProperties.GetIsInDesignMode(dependencyObject)) return;
            if (!(dependencyObject is TabItem tabItem)) return;

            tabItem.OnLoaded((routed) =>
            {
                var counter = tabItem.GetValue(CounterProperty);
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
