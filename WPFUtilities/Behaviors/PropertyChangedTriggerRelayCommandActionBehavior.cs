using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

using Microsoft.Xaml.Behaviors;

namespace WPFUtilities.Behaviors
{
    public class PropertyChangedTriggerRelayCommandActionBehavior
        : Behavior<FrameworkElement>
    {
        #region Command

        public ICommand Command
        {
            get => (ICommand)GetValue(CommandProperty);
            set { SetValue(CommandProperty, value); }
        }

        public static ICommand GetCommand(DependencyObject obj)
            => (ICommand)obj.GetValue(CommandProperty);

        public static void SetCommand(DependencyObject obj, ICommand value)
        {
            obj.SetValue(CommandProperty, value);
        }

        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.RegisterAttached("Command", typeof(ICommand), typeof(PropertyChangedTriggerRelayCommandActionBehavior), new PropertyMetadata(null));

        #endregion

        #region CommandParameter

        public object CommandParameter
        {
            get => (object)GetValue(CommandParameterProperty);
            set { SetValue(CommandParameterProperty, value); }
        }

        public static object GetCommandParameter(DependencyObject obj)
            => (object)obj.GetValue(CommandParameterProperty);

        public static void SetCommandParameter(DependencyObject obj, object value)
        {
            obj.SetValue(CommandParameterProperty, value);
        }

        public static readonly DependencyProperty CommandParameterProperty =
            DependencyProperty.RegisterAttached("CommandParameter", typeof(object), typeof(PropertyChangedTriggerRelayCommandActionBehavior), new PropertyMetadata(null));

        #endregion

        #region Source

        public INotifyPropertyChanged Source
        {
            get => (INotifyPropertyChanged)GetValue(SourceProperty);
            set { SetValue(SourceProperty, value); }
        }

        public static INotifyPropertyChanged GetSource(DependencyObject obj)
            => (INotifyPropertyChanged)obj.GetValue(SourceProperty);

        public static void SetSource(DependencyObject obj, INotifyPropertyChanged value)
        {
            obj.SetValue(SourceProperty, value);
        }

        public static readonly DependencyProperty SourceProperty =
            DependencyProperty.RegisterAttached("Source", typeof(INotifyPropertyChanged), typeof(PropertyChangedTriggerRelayCommandActionBehavior), new PropertyMetadata(null));

        #endregion

        #region PropertyName

        public string PropertyName
        {
            get => (string)GetValue(PropertyNameProperty);
            set { SetValue(PropertyNameProperty, value); }
        }

        public static string GetPropertyName(DependencyObject obj)
            => (string)obj.GetValue(PropertyNameProperty);

        public static void SetPropertyName(DependencyObject obj, string value)
        {
            obj.SetValue(PropertyNameProperty, value);
        }

        public static readonly DependencyProperty PropertyNameProperty =
            DependencyProperty.RegisterAttached("PropertyName", typeof(string), typeof(PropertyChangedTriggerRelayCommandActionBehavior), new PropertyMetadata(null));

        #endregion

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

        protected override void OnDetaching()
        {
            Source.PropertyChanged -= Source_PropertyChanged;
        }
    }
}
