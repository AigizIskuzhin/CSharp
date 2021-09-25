using System;
using System.Windows;
using System.Windows.Controls;

namespace MyBus.Resources.Controls
{
    public class MaskeddTextBox : Control
    {
        public static readonly DependencyProperty MaskTextProperty =
            DependencyProperty.Register("MaskText", typeof(string), typeof(MaskeddTextBox), new PropertyMetadata(string.Empty));
        public string MaskText
        {
            get => (string)GetValue(MaskTextProperty);
            set => SetValue(MaskTextProperty, value);
        }

        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(MaskeddTextBox), new PropertyMetadata(string.Empty));

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }


        #region Events
        private void UIElement_OnGotFocus_(object sender, RoutedEventArgs e)
        {
            throw new System.NotImplementedException();
        }

        private void TextBoxBase_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            throw new System.NotImplementedException();
        }

        private void UIElement_OnLostFocus(object sender, RoutedEventArgs e)
        {
            throw new System.NotImplementedException();
        }

        private void FrameworkElement_OnInitialized(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        } 
        #endregion
    }
}
