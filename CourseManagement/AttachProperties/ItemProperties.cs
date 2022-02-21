using System.Windows;
using System.Windows.Media;

namespace StudentManagementSystem.AttachProperties
{
    public static partial class UIProperties
    {
        public static readonly DependencyProperty ItemMouseOverBackgroundProperty = DependencyProperty.RegisterAttached(
            "ItemMouseOverBackground", typeof(Brush), typeof(UIProperties), new FrameworkPropertyMetadata(Brushes.Transparent, FrameworkPropertyMetadataOptions.AffectsRender));

        public static void SetItemMouseOverBackground(DependencyObject element, Brush value)
        {
            element.SetValue(ItemMouseOverBackgroundProperty, value);
        }

        public static Brush GetItemMouseOverBackground(DependencyObject element)
        {
            return (Brush)element.GetValue(ItemMouseOverBackgroundProperty);
        }

        public static readonly DependencyProperty ItemMouseOverForegroundProperty = DependencyProperty.RegisterAttached(
            "ItemMouseOverForeground", typeof(Brush), typeof(UIProperties), new FrameworkPropertyMetadata(Brushes.Transparent, FrameworkPropertyMetadataOptions.AffectsRender));

        public static void SetItemMouseOverForeground(DependencyObject element, Brush value)
        {
            element.SetValue(ItemMouseOverForegroundProperty, value);
        }

        public static Brush GetItemMouseOverForeground(DependencyObject element)
        {
            return (Brush)element.GetValue(ItemMouseOverForegroundProperty);
        }

        public static readonly DependencyProperty ItemSelectedBackgroundProperty = DependencyProperty.RegisterAttached(
            "ItemSelectedBackground", typeof(Brush), typeof(UIProperties), new FrameworkPropertyMetadata(Brushes.Transparent, FrameworkPropertyMetadataOptions.AffectsRender));

        public static void SetItemSelectedBackground(DependencyObject element, Brush value)
        {
            element.SetValue(ItemSelectedBackgroundProperty, value);
        }

        public static Brush GetItemSelectedBackground(DependencyObject element)
        {
            return (Brush)element.GetValue(ItemSelectedBackgroundProperty);
        }

        public static readonly DependencyProperty ItemSelectedForegroundProperty = DependencyProperty.RegisterAttached(
            "ItemSelectedForeground", typeof(Brush), typeof(UIProperties), new FrameworkPropertyMetadata(Brushes.Transparent, FrameworkPropertyMetadataOptions.AffectsRender));

        public static void SetItemSelectedForeground(DependencyObject element, Brush value)
        {
            element.SetValue(ItemSelectedForegroundProperty, value);
        }

        public static Brush GetItemSelectedForeground(DependencyObject element)
        {
            return (Brush)element.GetValue(ItemSelectedForegroundProperty);
        }


    }
}
