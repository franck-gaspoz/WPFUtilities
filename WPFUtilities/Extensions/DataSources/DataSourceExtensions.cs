using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows.Controls;

namespace WPFUtilities.Extensions.DataSources
{
    /// <summary>
    /// data source extensions
    /// </summary>
    public static class DataSourceExtensions
    {
        /// <summary>
        /// trigger an action when a DataGrid ItemsSource, is a bindable collection (INotifyCollectionChanged) or list (IBindingList) that is reset
        /// </summary>
        /// <param name="datagrid">source</param>
        /// <param name="action">action</param>
        /// <param name="repeat">if true, repeat action on next event</param>
        public static void OnItemSourceReset(
            this DataGrid datagrid,
            Action<object> action,
            bool repeat = true)
            => OnItemSourceReset(
                datagrid.ItemsSource,
                action,
                repeat);

        /// <summary>
        /// trigger an action when a bindable collection (INotifyCollectionChanged) or list (IBindingList) is reset
        /// </summary>
        /// <param name="source">source</param>
        /// <param name="action">action</param>
        /// <param name="repeat">if true, repeat action on next event</param>
        public static void OnItemSourceReset(
            this object source,
            Action<object> action,
            bool repeat = true
            )
        {
            if (source is INotifyCollectionChanged coll)
                OnItemSourceReset(coll, action, repeat);

            if (source is IBindingList list)
                OnItemSourceReset(list, action, repeat);
        }

        /// <summary>
        /// trigger an action when a bindable collection (INotifyCollectionChanged)
        /// </summary>
        /// <param name="coll">source</param>
        /// <param name="action">action</param>
        /// <param name="repeat">if true, repeat action on next event</param>
        public static void OnItemSourceReset(
            this INotifyCollectionChanged coll,
            Action<object> action,
            bool repeat = true)
        {
            void CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
            {
                if (e.Action == NotifyCollectionChangedAction.Reset)
                {
                    if (!repeat) coll.CollectionChanged -= CollectionChanged;
                    action(sender);
                }
            }
            coll.CollectionChanged += CollectionChanged;
        }

        /// <summary>
        /// trigger an action when a list (IBindingList) is reset
        /// </summary>
        /// <param name="list">source</param>
        /// <param name="action">action</param>
        /// <param name="repeat">if true, repeat action on next event</param>
        public static void OnItemSourceReset(
            this IBindingList list,
            Action<object> action,
            bool repeat = true)
        {
            void List_ListChanged(object sender, ListChangedEventArgs e)
            {
                if (e.ListChangedType == ListChangedType.Reset)
                {
                    if (!repeat) list.ListChanged -= List_ListChanged;
                    action(sender);
                }
            }
            list.ListChanged += List_ListChanged;
        }
    }
}
