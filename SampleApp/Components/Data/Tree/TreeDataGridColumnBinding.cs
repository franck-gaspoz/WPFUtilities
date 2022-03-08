using System.Windows.Data;

namespace SampleApp.Components.Data.Tree
{
    /// <summary>
    /// declares a column binding from the given path
    /// <para>binding source is auto throughts VisualTree</para>
    /// </summary>
    public class TreeDataGridColumnBinding : Binding
    {
        /// <summary>
        /// column path binding
        /// </summary>
        string _path;

        /*public string Path
        {
            get => Path; set => _path = value;
        }*/

        public TreeDataGridColumnBinding()
        {
            Initialize();
        }

        public TreeDataGridColumnBinding(string path)
        {
            _path = path;
            Initialize();
        }

        void Initialize()
        {

        }
    }
}
