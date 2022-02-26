using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace WPFUtilities.Helpers
{
    /// <summary>
    /// helpers pour l'accès aux arbres d'éléments WPF
    /// </summary>
    public static class WPFHelper
    {
        /// <summary>
        /// find visual ancestor of type T
        /// </summary>
        /// <typeparam name="T">lookup type</typeparam>
        /// <param name="dependencyObject">from this object</param>
        /// <returns>object of type T or null</returns>
        public static T FindVisualAncestor<T>(DependencyObject dependencyObject)
            where T : DependencyObject
        {
            var parent = VisualTreeHelper.GetParent(dependencyObject);

            if (parent == null) return null;

            var parentT = parent as T;
            return parentT ?? FindAncestor<T>(parent);
        }

        /// <summary>
        /// find logicial ancestor of type T
        /// </summary>
        /// <typeparam name="T">lookup type</typeparam>
        /// <param name="dependencyObject">from this object</param>
        /// <returns>object of type T or null</returns>
        public static T FindAncestor<T>(DependencyObject dependencyObject)
            where T : DependencyObject
        {
            var parent = LogicalTreeHelper.GetParent(dependencyObject);

            if (parent == null) return null;

            var parentT = parent as T;
            return parentT ?? FindAncestor<T>(parent);
        }

        /// <summary>
        /// find ancestor having type T in visual tree
        /// </summary>
        /// <typeparam name="T">lookup type</typeparam>
        /// <param name="dependencyObject">from object</param>
        /// <returns>object or null</returns>
        public static T FindAncestor<T>(FrameworkElement dependencyObject)
            where T : FrameworkElement
        {
            var parent = VisualTreeHelper.GetParent(dependencyObject);

            if (parent == null) return null;

            var parentT = parent as T;
            return parentT ?? FindAncestor<T>(parent);
        }

        /// <summary>
        /// find ancestor having type T in visual tree
        /// </summary>
        /// <typeparam name="T">lookup type</typeparam>
        /// <param name="dependencyObject">from object</param>
        /// <returns>object or null</returns>
        public static T FindAncestor<T>(
            object dependencyObject)
            //where T : IInputElement
            //where T : IDataController
            where T : class
        {
            var parent = VisualTreeHelper.GetParent(
                dependencyObject as DependencyObject)
                as object;

            if (parent == null) return null;

            if (parent is T)
                return (T)parent;

            return FindAncestor<T>(parent) as T;
        }

        /// <summary>
        /// string view of KeyBinding
        /// </summary>
        /// <param name="keyBinding">key binding</param>
        /// <returns>string</returns>
        public static string ToString(KeyBinding keyBinding)
        {
            string r = "[" + keyBinding.Key + " " + keyBinding.Modifiers + "]";
            if (keyBinding.CommandParameter != null)
                r += ",p=" + keyBinding.CommandParameter;
            return r;
        }

        /// <summary>
        /// find a visual child having type T
        /// </summary>
        /// <typeparam name="T">type lookup</typeparam>
        /// <param name="depencencyObject">from object</param>
        /// <returns>object or null</returns>
        public static T FindVisualChild<T>(DependencyObject depencencyObject) where T : DependencyObject
        {
            if (depencencyObject != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depencencyObject); ++i)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(depencencyObject, i);
                    T result = (child as T) ?? FindVisualChild<T>(child);
                    if (result != null)
                        return result;
                }
            }
            return null;
        }

        /// <summary>
        /// find a visual child having name
        /// </summary>
        /// <param name="depencencyObject">from object</param>
        /// <param name="name">lookup name</param>
        /// <returns>object or null</returns>
        public static FrameworkElement FindByNameInVisualTree(DependencyObject depencencyObject, string name)
        {
            if (depencencyObject != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depencencyObject); ++i)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(depencencyObject, i);
                    FrameworkElement result = (child as FrameworkElement);
                    if (result != null)
                    {
                        if (result.Name == name)
                            return result;
                        var r = FindByNameInVisualTree(result, name);
                        if (r != null)
                            return r;
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// find elements in logicical tree
        /// </summary>
        /// <param name="obj">from object</param>
        /// <param name="SearchType">lookup type</param>
        /// <returns>elements list that match criteria</returns>
        public static List<FrameworkElement> FindByTypeInLogicalTree(FrameworkElement obj, Type SearchType)
        {
            List<FrameworkElement> r = new List<FrameworkElement>();

            if (obj is FrameworkElement)
            {
                FrameworkElement fe = (FrameworkElement)obj;
                if (SearchType.IsInstanceOfType(fe))
                    r.Add(fe);

                //System.Diagnostics.Debug.WriteLine("Logical Type: " + fe.GetType() + ", Name: " + fe.Name);

                // recurse through the children
                IEnumerable children = LogicalTreeHelper.GetChildren(fe);
                foreach (object child in children)
                {
                    if (child is FrameworkElement)
                    {
                        List<FrameworkElement> r2 = FindByTypeInLogicalTree((FrameworkElement)child, SearchType);
                        if (r2 != null)
                            r.AddRange(r2);
                    }
                }
            }
            else
            {
                // stop recursing as we certainly can't have any more FrameworkElement children
                //print("Logical Type: " + obj.GetType());
            }
            return r;
        }

        /// <summary>
        /// find elements in logicical tree having type and name
        /// </summary>
        /// <param name="obj">from object</param>
        /// <param name="SearchName">lookup name</param>
        /// <returns>elements list</returns>
        public static List<FrameworkElement> FindByNameInLogicalTree(FrameworkElement obj, string SearchName)
        {
            List<FrameworkElement> r = new List<FrameworkElement>();

            if (obj is FrameworkElement)
            {
                FrameworkElement fe = (FrameworkElement)obj;
                if (fe.Name == SearchName)
                    r.Add(fe);

                // recurse through the children
                IEnumerable children = LogicalTreeHelper.GetChildren(fe);
                foreach (object child in children)
                {
                    if (child is FrameworkElement)
                    {
                        List<FrameworkElement> r2 = FindByNameInLogicalTree((FrameworkElement)child, SearchName);
                        if (r2 != null)
                            r.AddRange(r2);
                    }
                }
            }
            else
            {
                // stop recursing as we certainly can't have any more FrameworkElement children
                //print("Logical Type: " + obj.GetType());
            }
            return r;
        }

        /// <summary>
        /// scroll to end of a list view
        /// </summary>
        /// <param name="_ListView"></param>
        public static void ScrollToEnd(ListView _ListView)
        {
            ScrollViewer _ScrollViewer = GetDescendantByType(_ListView, typeof(ScrollViewer)) as ScrollViewer;
            _ScrollViewer.ScrollToEnd();
        }

        /// <summary>
        /// scroll to end of a list box
        /// </summary>
        /// <param name="_ListBox"></param>
        public static void ScrollToEnd(ListBox _ListBox)
        {
            ScrollViewer _ScrollViewer = GetDescendantByType(_ListBox, typeof(ScrollViewer)) as ScrollViewer;
            _ScrollViewer.ScrollToEnd();
        }

        /// <summary>
        /// get descendant by type in visual tree
        /// </summary>
        /// <param name="element">from element</param>
        /// <param name="type">lookup type</param>
        /// <returns>object or null</returns>
        public static Visual GetDescendantByType(Visual element, Type type)
        {
            if (element == null) return null;
            if (element.GetType() == type) return element;
            Visual foundElement = null;
            if (element is FrameworkElement)
            {
                (element as FrameworkElement).ApplyTemplate();
            }
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(element); i++)
            {
                Visual visual = VisualTreeHelper.GetChild(element, i) as Visual;
                foundElement = GetDescendantByType(visual, type);
                if (foundElement != null)
                    break;
            }
            return foundElement;
        }


    }
}
