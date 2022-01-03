using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

using Microsoft.Xaml.Behaviors;

namespace WPFUtilities.Behaviors.Interactivity
{
    /// <summary>
    /// property changed command delegate behavior
    /// </summary>
    public class PropertyChangedDelegateCommandBehavior
        : Behavior<FrameworkElement>
    {
        #region Command

        /// <summary>
        /// command
        /// </summary>
        public ICommand Command
        {
            get => (ICommand)GetValue(CommandProperty);
            set { SetValue(CommandProperty, value); }
        }

        /// <summary>
        /// get command
        /// </summary>
        /// <param name="dependencyObject">dependency Object</param>
        /// <returns>command</returns>
        public static ICommand GetCommand(DependencyObject dependencyObject)
            => (ICommand)dependencyObject.GetValue(CommandProperty);

        /// <summary>
        /// set command
        /// </summary>
        /// <param name="dependencyObject">dependency Object</param>
        /// <param name="value">value</param>
        public static void SetCommand(DependencyObject dependencyObject, ICommand value)
            => dependencyObject.SetValue(CommandProperty, value);

        /// <summary>
        /// command property
        /// </summary>
        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.RegisterAttached("Command", typeof(ICommand), typeof(PropertyChangedDelegateCommandBehavior), new PropertyMetadata(null));

        #endregion

        #region CommandParameter

        /// <summary>
        /// command parameter
        /// </summary>
        public object CommandParameter
        {
            get => (object)GetValue(CommandParameterProperty);
            set { SetValue(CommandParameterProperty, value); }
        }

        /// <summary>
        /// get command
        /// </summary>
        /// <param name="dependencyObject">dependency Object</param>
        /// <returns>command</returns>
        public static object GetCommandParameter(DependencyObject dependencyObject)
            => (object)dependencyObject.GetValue(CommandParameterProperty);

        /// <summary>
        /// set command
        /// </summary>
        /// <param name="dependencyObject">dependency Object</param>
        /// <param name="value"></param>
        public static void SetCommandParameter(DependencyObject dependencyObject, object value)
            => dependencyObject.SetValue(CommandParameterProperty, value);

        /// <summary>
        /// command parameter
        /// </summary>
        public static readonly DependencyProperty CommandParameterProperty =
            DependencyProperty.RegisterAttached("CommandParameter", typeof(object), typeof(PropertyChangedDelegateCommandBehavior), new PropertyMetadata(null));

        #endregion

        #region Source

        /// <summary>
        /// source
        /// </summary>
        public INotifyPropertyChanged Source
        {
            get => (INotifyPropertyChanged)GetValue(SourceProperty);
            set { SetValue(SourceProperty, value); }
        }

        /// <summary>
        /// get source
        /// </summary>
        /// <param name="dependencyObject">dependency Object</param>
        /// <returns>source</returns>
        public static INotifyPropertyChanged GetSource(DependencyObject dependencyObject)
            => (INotifyPropertyChanged)dependencyObject.GetValue(SourceProperty);

        /// <summary>
        /// set source
        /// </summary>
        /// <param name="dependencyObject">dependency Object</param>
        /// <param name="value">value</param>
        public static void SetSource(DependencyObject dependencyObject, INotifyPropertyChanged value)
            => dependencyObject.SetValue(SourceProperty, value);

        /// <summary>
        /// source property
        /// </summary>
        public static readonly DependencyProperty SourceProperty =
            DependencyProperty.RegisterAttached("Source", typeof(INotifyPropertyChanged), typeof(PropertyChangedDelegateCommandBehavior), new PropertyMetadata(null));

        #endregion

        #region PropertyName

        /// <summary>
        /// property name
        /// </summary>
        public string PropertyName
        {
            get => (string)GetValue(PropertyNameProperty);
            set { SetValue(PropertyNameProperty, value); }
        }

        /// <summary>
        /// get property name
        /// </summary>
        /// <param name="dependencyObject">dependency Object</param>
        /// <returns>property name</returns>
        public static string GetPropertyName(DependencyObject dependencyObject)
            => (string)dependencyObject.GetValue(PropertyNameProperty);

        /// <summary>
        /// set property name
        /// </summary>
        /// <param name="dependencyObject">dependency Object</param>
        /// <param name="value">value</param>
        public static void SetPropertyName(DependencyObject dependencyObject, string value)
            => dependencyObject.SetValue(PropertyNameProperty, value);

        /// <summary>
        /// property name dependency property
        /// </summary>
        public static readonly DependencyProperty PropertyNameProperty =
            DependencyProperty.RegisterAttached("PropertyName", typeof(string), typeof(PropertyChangedDelegateCommandBehavior), new PropertyMetadata(null));

        #endregion

        /// <inheritdoc/>
        protected override void OnAttached()
        {
            var source = Source ?? throw new ArgumentNullException(nameof(Source));
            source.PropertyChanged += Source_PropertyChanged;
            AssociatedObject.Loaded += Source_Loaded;
        }

        private void Source_Loaded(object sender, RoutedEventArgs e)
        {
            AssociatedObject.Loaded -= Source_Loaded;
            Command.Execute(CommandParameter);
        }

        private void Source_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var propertyName = PropertyName ?? throw new ArgumentNullException(nameof(PropertyName));
            if (propertyName == e.PropertyName)
            {
                Command.Execute(CommandParameter);
            }
        }

        /// <inheritdoc/>
        protected override void OnDetaching()
        {
            Source.PropertyChanged -= Source_PropertyChanged;
        }
    }
}
