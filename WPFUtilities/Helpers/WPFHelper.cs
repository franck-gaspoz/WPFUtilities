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
        public static T FindAncestor<T>(DependencyObject dependencyObject)
            where T : DependencyObject
        {
            var parent = VisualTreeHelper.GetParent(dependencyObject);

            if (parent == null) return null;

            var parentT = parent as T;
            return parentT ?? FindAncestor<T>(parent);
        }

        public static T FindAncestor<T>(FrameworkElement dependencyObject)
            where T : FrameworkElement
        {
            var parent = VisualTreeHelper.GetParent(dependencyObject);

            if (parent == null) return null;

            var parentT = parent as T;
            return parentT ?? FindAncestor<T>(parent);
        }

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
        /// <param name="kb">key binding</param>
        /// <returns>string</returns>
        public static string ToString(KeyBinding kb)
        {
            string r = "[" + kb.Key + " " + kb.Modifiers + "]";
            if (kb.CommandParameter != null)
                r += ",p=" + kb.CommandParameter;
            return r;
        }

        /// <summary>
        /// retrouve un enfant visuel
        /// </summary>
        /// <typeparam name="T">type de l'objet demandé</typeparam>
        /// <param name="depencencyObject">racine</param>
        /// <returns>objet du type demandé ou null</returns>
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
        /// retrouve un enfant visuel
        /// </summary>
        /// <typeparam name="T">type de l'objet demandé</typeparam>
        /// <param name="depencencyObject">racine</param>
        /// <returns>objet du type demandé ou null</returns>
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
        /// trouve les éléments dans l'arbre logique de l'objet WPF
        /// </summary>
        /// <param name="obj">objet racine de la recherche</param>
        /// <param name="SearchType">type des objets recherchés</param>
        /// <returns>liste des éléments qui possède le type IBindable</returns>
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
        /// trouve les éléments dans l'arbre logique de l'objet WPF
        /// </summary>
        /// <param name="obj">objet racine de la recherche</param>
        /// <param name="SearchName">nom de l'objet recherché</param>
        /// <returns>liste des éléments qui possède le type IBindable</returns>
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

        public static void ScrollToEnd(ListView _ListView)
        {
            ScrollViewer _ScrollViewer = GetDescendantByType(_ListView, typeof(ScrollViewer)) as ScrollViewer;
            _ScrollViewer.ScrollToEnd();
        }

        public static void ScrollToEnd(ListBox _ListBox)
        {
            ScrollViewer _ScrollViewer = GetDescendantByType(_ListBox, typeof(ScrollViewer)) as ScrollViewer;
            _ScrollViewer.ScrollToEnd();
        }

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

        public static Visual GetDescendants(Visual element, List<Visual> Descendants)
        {
            if (element == null) return null;

            //if (element.GetType() == type) return element;
            Descendants.Add(element);

            Visual foundElement = null;
            if (element is FrameworkElement)
            {
                (element as FrameworkElement).ApplyTemplate();
            }
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(element); i++)
            {
                Visual visual = VisualTreeHelper.GetChild(element, i) as Visual;
                foundElement = GetDescendants(visual, Descendants);
                if (foundElement != null)
                    break;
            }
            return foundElement;
        }

        /// <summary>
        /// trouve les éléments dans l'arbre logique de l'objet WPF
        /// </summary>
        /// <param name="obj">objet racine de la recherche</param>
        /// <param name="Descendants">liste des descendants</param>
        /// <returns>liste des éléments descendants de obj dans l'arbre logique</returns>
        public static void GetLogicalDecendants(FrameworkElement obj, List<FrameworkElement> Descendants)
        {
            if (obj is FrameworkElement)
            {
                FrameworkElement fe = (FrameworkElement)obj;

                //System.Diagnostics.Debug.WriteLine("Logical Type: " + fe.GetType() + ", Name: " + fe.Name);

                // recurse through the children
                IEnumerable children = LogicalTreeHelper.GetChildren(fe);
                foreach (object child in children)
                {
                    var chi = child as FrameworkElement;
                    if (chi != null)
                    {
                        Descendants.Add(chi);
                        GetLogicalDecendants(chi, Descendants);
                    }
                }
            }
            else
            {
                // stop recursing as we certainly can't have any more FrameworkElement children
                //print("Logical Type: " + obj.GetType());
            }
        }
    }
}
