using System.IO;

using WPFUtilities.ComponentModels;

namespace SampleApp.Components.Settings
{
    public class SettingsFileViewModel : ModelBase, ISettingsFileViewModel
    {
        bool _isVisible = false;
        /// <inheritdoc/>
        public bool IsVisible
        {
            get
            {
                return _isVisible;
            }
            set
            {
                _isVisible = value;
                NotifyPropertyChanged();
            }
        }

        string _fileText = null;
        /// <inheritdoc/>
        public string FileText
        {
            get
            {
                return _fileText;
            }
            set
            {
                _fileText = value;
                NotifyPropertyChanged();
                NotifyPropertyChanged(nameof(IsFileAvailable));
            }
        }

        string _filePath = null;
        /// <inheritdoc/>
        public string FilePath
        {
            get
            {
                return _filePath;
            }
            set
            {
                _filePath = value;
                NotifyPropertyChanged();
            }
        }

        string _fileName = null;
        /// <inheritdoc/>
        public string FileName
        {
            get
            {
                return _fileName;
            }
            set
            {
                _fileName = value;
                NotifyPropertyChanged();
            }
        }

        /// <inheritdoc/>
        public string FullPath => Path.Combine(FilePath, FileName);

        string _errorText = null;
        /// <inheritdoc/>
        public string Error
        {
            get
            {
                return _errorText;
            }
            set
            {
                _errorText = value;
                NotifyPropertyChanged();
            }
        }

        /// <inheritdoc/>
        public bool IsFileAvailable => FileText != null;

    }
}
