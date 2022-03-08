using System;
using System.Windows;
using System.Windows.Markup;

namespace SampleApp.Components.Data.Tree
{
    /// <summary>
    /// declares a column binding from the given path
    /// <para>binding source is auto throughts VisualTree</para>
    /// </summary>
    public class TreeDataGridColumnBinding : MarkupExtension
    {
        /// <summary>
        /// column path binding
        /// </summary>
        string _path;

        /*public string Path
        {
            get => Path; set => _path = value;
        }*/

        public TreeDataGridColumnBinding(string path)
        {
            _path = path;
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            if (serviceProvider != null)
            {
                IProvideValueTarget provideValueTarget = serviceProvider.GetService(typeof(IProvideValueTarget)) as IProvideValueTarget;
                if (provideValueTarget != null)
                {
                    var target = provideValueTarget.TargetObject;
                    if (provideValueTarget.TargetObject is FrameworkElement e)
                    {

                    }
                }
            }
            return null;
        }
    }
}
