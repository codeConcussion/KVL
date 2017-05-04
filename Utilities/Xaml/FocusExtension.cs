using System;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Threading;

namespace CodeConcussion.KVL.Utilities.Xaml
{
    internal static class FocusExtension
    {
        #region IsFocused

        public static readonly DependencyProperty IsFocusedProperty = DependencyProperty.RegisterAttached(
             "IsFocused",
             typeof(bool),  
             typeof(FocusExtension),
             new UIPropertyMetadata(false, OnIsFocusedPropertyChanged));
        
        private static void OnIsFocusedPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var uiElement = (UIElement)d;
            if (!(bool)e.NewValue) return;
            
            uiElement.Dispatcher.BeginInvoke(new Action(() =>
            {
                var list = uiElement as ListBox;
                if (list != null)
                {
                    if (list.HasItems && list.SelectedItem == null) list.SelectedIndex = 0;
                    if (list.SelectedItem != null) ((ListBoxItem)list.ItemContainerGenerator.ContainerFromIndex(list.SelectedIndex)).Focus();
                    if (!list.HasItems) list.Focus();
                }
                else
                {
                    uiElement.Focus();
                }

                SetIsFocused(d, false);
            }), DispatcherPriority.Background, null);
        }

        public static bool GetIsFocused(DependencyObject obj)
        {
            return (bool) obj.GetValue(IsFocusedProperty);
        }

        public static void SetIsFocused(DependencyObject obj, bool value)
        {
            obj.SetValue(IsFocusedProperty, value);
        }

        #endregion

        public static void SetBindingOnFocusAttachedBehavior(string path, FrameworkElement control)
        {
            var binding = new Binding(path) { Mode = BindingMode.TwoWay };
            BindingOperations.SetBinding(control, IsFocusedProperty, binding);
        }

        public static void CheckForFocusProperty(string focusPropertyName, PropertyInfo[] properties, FrameworkElement control)
        {
            if (properties.Any(x => x.Name.Equals(focusPropertyName, StringComparison.InvariantCultureIgnoreCase)))
            {
                SetBindingOnFocusAttachedBehavior(focusPropertyName, control);
            }
        }
    }
}
