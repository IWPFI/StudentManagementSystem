using System.Windows;
using System.Windows.Media;

namespace StudentManagementSystem.AttachProperties
{
    public static partial class UIProperties
    {
        public static readonly DependencyProperty DropShadowColorProperty = DependencyProperty.RegisterAttached(
            "DropShadowColor", typeof(Color), typeof(UIProperties), new FrameworkPropertyMetadata(Colors.Transparent, FrameworkPropertyMetadataOptions.AffectsRender));

        public static void SetDropShadowColor(DependencyObject element, Color value)
        {
            element.SetValue(DropShadowColorProperty, value);
        }

        public static Color GetDropShadowColor(DependencyObject element)
        {
            return (Color)element.GetValue(DropShadowColorProperty);
        }

        public static readonly DependencyProperty DropShadowDepthProperty = DependencyProperty.RegisterAttached(
            "DropShadowDepth", typeof(double), typeof(UIProperties), new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsRender));

        public static void SetDropShadowDepth(DependencyObject element, double value)
        {
            element.SetValue(DropShadowDepthProperty, value);
        }

        public static double GetDropShadowDepth(DependencyObject element)
        {
            return (double)element.GetValue(DropShadowDepthProperty);
        }

        public static readonly DependencyProperty DropShadowBlurRadiusProperty = DependencyProperty.RegisterAttached(
            "DropShadowBlurRadius", typeof(double), typeof(UIProperties), new FrameworkPropertyMetadata(10.0, FrameworkPropertyMetadataOptions.AffectsRender));

        public static void SetDropShadowBlurRadius(DependencyObject element, double value)
        {
            element.SetValue(DropShadowBlurRadiusProperty, value);
        }

        public static double GetDropShadowBlurRadius(DependencyObject element)
        {
            return (double)element.GetValue(DropShadowBlurRadiusProperty);
        }

        public static readonly DependencyProperty MouseOverDropShadowColorProperty = DependencyProperty.RegisterAttached(
            "MouseOverDropShadowColor", typeof(Color), typeof(UIProperties), new FrameworkPropertyMetadata(Colors.Transparent, FrameworkPropertyMetadataOptions.AffectsRender));

        public static void SetMouseOverDropShadowColor(DependencyObject element, Color value)
        {
            element.SetValue(MouseOverDropShadowColorProperty, value);
        }

        public static Color GetMouseOverDropShadowColor(DependencyObject element)
        {
            return (Color)element.GetValue(MouseOverDropShadowColorProperty);
        }

        public static readonly DependencyProperty MouseOverDropShadowDepthProperty = DependencyProperty.RegisterAttached(
            "MouseOverDropShadowDepth", typeof(double), typeof(UIProperties), new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsRender));

        public static void SetMouseOverDropShadowDepth(DependencyObject element, double value)
        {
            element.SetValue(MouseOverDropShadowDepthProperty, value);
        }

        public static double GetMouseOverDropShadowDepth(DependencyObject element)
        {
            return (double)element.GetValue(MouseOverDropShadowDepthProperty);
        }

        public static readonly DependencyProperty MouseOverDropShadowBlurRadiusProperty = DependencyProperty.RegisterAttached(
            "MouseOverDropShadowBlurRadius", typeof(double), typeof(UIProperties), new FrameworkPropertyMetadata(10.0, FrameworkPropertyMetadataOptions.AffectsRender));

        public static void SetMouseOverDropShadowBlurRadius(DependencyObject element, double value)
        {
            element.SetValue(MouseOverDropShadowBlurRadiusProperty, value);
        }

        public static double GetMouseOverDropShadowBlurRadius(DependencyObject element)
        {
            return (double)element.GetValue(MouseOverDropShadowBlurRadiusProperty);
        }

        public static readonly DependencyProperty MouseOverDropShadowOpcityProperty = DependencyProperty.RegisterAttached(
            "MouseOverDropShadowOpcity", typeof(double), typeof(UIProperties), new FrameworkPropertyMetadata(10.0, FrameworkPropertyMetadataOptions.AffectsRender));

        public static void SetMouseOverDropShadowOpcity(DependencyObject element, double value)
        {
            element.SetValue(MouseOverDropShadowOpcityProperty, value);
        }

        public static double GetMouseOverDropShadowOpcity(DependencyObject element)
        {
            return (double)element.GetValue(MouseOverDropShadowOpcityProperty);
        }

        public static readonly DependencyProperty MouseOverForegroundProperty = DependencyProperty.RegisterAttached(
            "MouseOverForeground", typeof(Brush), typeof(UIProperties), new FrameworkPropertyMetadata(Brushes.White, FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.Inherits));

        public static void SetMouseOverForeground(DependencyObject element, Brush value)
        {
            element.SetValue(MouseOverForegroundProperty, value);
        }

        public static Brush GetMouseOverForeground(DependencyObject element)
        {
            return (Brush)element.GetValue(MouseOverForegroundProperty);
        }

        public static readonly DependencyProperty CheckedDropShadowColorProperty = DependencyProperty.RegisterAttached(
            "CheckedDropShadowColor", typeof(Color), typeof(UIProperties), new FrameworkPropertyMetadata(Colors.Transparent, FrameworkPropertyMetadataOptions.AffectsRender));

        public static void SetCheckedDropShadowColor(DependencyObject element, Color value)
        {
            element.SetValue(CheckedDropShadowColorProperty, value);
        }

        public static Color GetCheckedDropShadowColor(DependencyObject element)
        {
            return (Color)element.GetValue(CheckedDropShadowColorProperty);
        }

        public static readonly DependencyProperty CheckedDropShadowDepthProperty = DependencyProperty.RegisterAttached(
            "CheckedDropShadowDepth", typeof(double), typeof(UIProperties), new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsRender));

        public static void SetCheckedDropShadowDepth(DependencyObject element, double value)
        {
            element.SetValue(CheckedDropShadowDepthProperty, value);
        }

        public static double GetCheckedDropShadowDepth(DependencyObject element)
        {
            return (double)element.GetValue(CheckedDropShadowDepthProperty);
        }

        public static readonly DependencyProperty CheckedDropShadowBlurRadiusProperty = DependencyProperty.RegisterAttached(
            "CheckedDropShadowBlurRadius", typeof(double), typeof(UIProperties), new FrameworkPropertyMetadata(10.0, FrameworkPropertyMetadataOptions.AffectsRender));

        public static void SetCheckedDropShadowBlurRadius(DependencyObject element, double value)
        {
            element.SetValue(CheckedDropShadowBlurRadiusProperty, value);
        }

        public static double GetCheckedDropShadowBlurRadius(DependencyObject element)
        {
            return (double)element.GetValue(CheckedDropShadowBlurRadiusProperty);
        }

        public static readonly DependencyProperty CheckedDropShadowOpcityProperty = DependencyProperty.RegisterAttached(
            "CheckedDropShadowOpcity", typeof(double), typeof(UIProperties), new FrameworkPropertyMetadata(10.0, FrameworkPropertyMetadataOptions.AffectsRender));

        public static void SetCheckedDropShadowOpcity(DependencyObject element, double value)
        {
            element.SetValue(CheckedDropShadowOpcityProperty, value);
        }

        public static double GetCheckedDropShadowOpcity(DependencyObject element)
        {
            return (double)element.GetValue(CheckedDropShadowOpcityProperty);
        }

        public static readonly DependencyProperty CheckedForegroundProperty = DependencyProperty.RegisterAttached(
            "CheckedForeground", typeof(Brush), typeof(UIProperties), new FrameworkPropertyMetadata(Brushes.White, FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.Inherits));

        public static void SetCheckedForeground(DependencyObject element, Brush value)
        {
            element.SetValue(CheckedForegroundProperty, value);
        }

        public static Brush GetCheckedForeground(DependencyObject element)
        {
            return (Brush)element.GetValue(CheckedForegroundProperty);
        }

        public static readonly DependencyProperty PressedForegroundProperty = DependencyProperty.RegisterAttached(
            "PressedForeground", typeof(Brush), typeof(UIProperties), new FrameworkPropertyMetadata(Brushes.White, FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.Inherits));

        public static void SetPressedForeground(DependencyObject element, Brush value)
        {
            element.SetValue(PressedForegroundProperty, value);
        }

        public static Brush GetPressedForeground(DependencyObject element)
        {
            return (Brush)element.GetValue(PressedForegroundProperty);
        }

        public static readonly DependencyProperty MouseOverBackgroundProperty = DependencyProperty.RegisterAttached(
            "MouseOverBackground", typeof(Brush), typeof(UIProperties), new FrameworkPropertyMetadata(Brushes.Transparent, FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.Inherits));

        public static void SetMouseOverBackground(DependencyObject element, Brush value)
        {
            element.SetValue(MouseOverBackgroundProperty, value);
        }

        public static Brush GetMouseOverBackground(DependencyObject element)
        {
            return (Brush)element.GetValue(MouseOverBackgroundProperty);
        }

        public static readonly DependencyProperty CheckedBackgroundProperty = DependencyProperty.RegisterAttached(
            "CheckedBackground", typeof(Brush), typeof(UIProperties), new FrameworkPropertyMetadata(Brushes.Transparent, FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.Inherits));

        public static void SetCheckedBackground(DependencyObject element, Brush value)
        {
            element.SetValue(CheckedBackgroundProperty, value);
        }

        public static Brush GetCheckedBackground(DependencyObject element)
        {
            return (Brush)element.GetValue(CheckedBackgroundProperty);
        }

        public static readonly DependencyProperty PressedBackgroundProperty = DependencyProperty.RegisterAttached(
            "PressedBackground", typeof(Brush), typeof(UIProperties), new FrameworkPropertyMetadata(Brushes.Transparent, FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.Inherits));

        public static void SetPressedBackground(DependencyObject element, Brush value)
        {
            element.SetValue(PressedBackgroundProperty, value);
        }

        public static Brush GetPressedBackground(DependencyObject element)
        {
            return (Brush)element.GetValue(PressedBackgroundProperty);
        }

        public static readonly DependencyProperty CornerRadiusProperty = DependencyProperty.RegisterAttached(
            "CornerRadius", typeof(CornerRadius), typeof(UIProperties), new FrameworkPropertyMetadata(new CornerRadius(0), FrameworkPropertyMetadataOptions.AffectsRender));

        public static void SetCornerRadius(DependencyObject element, CornerRadius value)
        {
            element.SetValue(CornerRadiusProperty, value);
        }

        public static CornerRadius GetCornerRadius(DependencyObject element)
        {
            return (CornerRadius)element.GetValue(CornerRadiusProperty);
        }

        public static readonly DependencyProperty IconProperty = DependencyProperty.RegisterAttached(
            "Icon", typeof(ImageSource), typeof(UIProperties), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender));

        public static void SetIcon(DependencyObject element, ImageSource value)
        {
            element.SetValue(IconProperty, value);
        }

        public static ImageSource GetIcon(DependencyObject element)
        {
            return (ImageSource)element.GetValue(IconProperty);
        }

        public static readonly DependencyProperty ImageBrushProperty = DependencyProperty.RegisterAttached(
            "ImageBrush", typeof(ImageSource), typeof(UIProperties), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender));

        public static void SetImageBrush(DependencyObject element, ImageSource value)
        {
            element.SetValue(ImageBrushProperty, value);
        }

        public static ImageSource GetImageBrush(DependencyObject element)
        {
            return (ImageSource)element.GetValue(ImageBrushProperty);
        }

        public static readonly DependencyProperty MouseOverIconProperty = DependencyProperty.RegisterAttached(
            "MouseOverIcon", typeof(ImageSource), typeof(UIProperties), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender));

        public static void SetMouseOverIcon(DependencyObject element, ImageSource value)
        {
            element.SetValue(MouseOverIconProperty, value);
        }

        public static ImageSource GetMouseOverIcon(DependencyObject element)
        {
            return (ImageSource)element.GetValue(MouseOverIconProperty);
        }

        public static readonly DependencyProperty CheckedIconProperty = DependencyProperty.RegisterAttached(
            "CheckedIcon", typeof(ImageSource), typeof(UIProperties), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender));

        public static void SetCheckedIcon(DependencyObject element, ImageSource value)
        {
            element.SetValue(CheckedIconProperty, value);
        }

        public static ImageSource GetCheckedIcon(DependencyObject element)
        {
            return (ImageSource)element.GetValue(CheckedIconProperty);
        }

        public static readonly DependencyProperty PressedIconProperty = DependencyProperty.RegisterAttached(
            "PressedIcon", typeof(ImageSource), typeof(UIProperties), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender));

        public static void SetPressedIcon(DependencyObject element, ImageSource value)
        {
            element.SetValue(PressedIconProperty, value);
        }

        public static ImageSource GetPressedIcon(DependencyObject element)
        {
            return (ImageSource)element.GetValue(PressedIconProperty);
        }


        public static readonly DependencyProperty IconWidthProperty = DependencyProperty.RegisterAttached(
            "IconWidth", typeof(double), typeof(UIProperties), new FrameworkPropertyMetadata(32.0, FrameworkPropertyMetadataOptions.AffectsRender));

        public static void SetIconWidth(DependencyObject element, double value)
        {
            element.SetValue(IconWidthProperty, value);
        }

        public static double GetIconWidth(DependencyObject element)
        {
            return (double)element.GetValue(IconWidthProperty);
        }


        public static readonly DependencyProperty IconHeightProperty = DependencyProperty.RegisterAttached(
            "IconHeight", typeof(double), typeof(UIProperties), new FrameworkPropertyMetadata(32.0, FrameworkPropertyMetadataOptions.AffectsRender));

        public static void SetIconHeight(DependencyObject element, double value)
        {
            element.SetValue(IconHeightProperty, value);
        }

        public static double GetIconHeight(DependencyObject element)
        {
            return (double)element.GetValue(IconHeightProperty);
        }


        public static readonly DependencyProperty IconMarginProperty = DependencyProperty.RegisterAttached(
            "IconMargin", typeof(Thickness), typeof(UIProperties), new FrameworkPropertyMetadata(new Thickness(0), FrameworkPropertyMetadataOptions.AffectsRender));

        public static void SetIconMargin(DependencyObject element, Thickness value)
        {
            element.SetValue(IconMarginProperty, value);
        }

        public static Thickness GetIconMargin(DependencyObject element)
        {
            return (Thickness)element.GetValue(IconMarginProperty);
        }

        public static readonly DependencyProperty MouseOverWidthProperty = DependencyProperty.RegisterAttached(
            "MouseOverWidth", typeof(double), typeof(UIProperties), new FrameworkPropertyMetadata(32.0, FrameworkPropertyMetadataOptions.AffectsRender));

        public static void SetMouseOverWidth(DependencyObject element, double value)
        {
            element.SetValue(MouseOverWidthProperty, value);
        }

        public static double GetMouseOverWidth(DependencyObject element)
        {
            return (double)element.GetValue(MouseOverWidthProperty);
        }

        public static readonly DependencyProperty MouseOverHeightProperty = DependencyProperty.RegisterAttached(
            "MouseOverHeight", typeof(double), typeof(UIProperties), new FrameworkPropertyMetadata(32.0, FrameworkPropertyMetadataOptions.AffectsRender));

        public static void SetMouseOverHeight(DependencyObject element, double value)
        {
            element.SetValue(MouseOverHeightProperty, value);
        }

        public static double GetMouseOverHeight(DependencyObject element)
        {
            return (double)element.GetValue(MouseOverHeightProperty);
        }

        public static readonly DependencyProperty MouseOverMarginProperty = DependencyProperty.RegisterAttached(
            "MouseOverMargin", typeof(Thickness), typeof(UIProperties), new FrameworkPropertyMetadata(new Thickness(0), FrameworkPropertyMetadataOptions.AffectsRender));

        public static void SetMouseOverMargin(DependencyObject element, Thickness value)
        {
            element.SetValue(MouseOverMarginProperty, value);
        }

        public static Thickness GetMouseOverMargin(DependencyObject element)
        {
            return (Thickness)element.GetValue(MouseOverMarginProperty);
        }

        public static readonly DependencyProperty CheckedWidthProperty = DependencyProperty.RegisterAttached(
            "CheckedWidth", typeof(double), typeof(UIProperties), new FrameworkPropertyMetadata(32.0, FrameworkPropertyMetadataOptions.AffectsRender));

        public static void SetCheckedWidth(DependencyObject element, double value)
        {
            element.SetValue(CheckedWidthProperty, value);
        }

        public static double GetCheckedWidth(DependencyObject element)
        {
            return (double)element.GetValue(CheckedWidthProperty);
        }

        public static readonly DependencyProperty CheckedHeightProperty = DependencyProperty.RegisterAttached(
            "CheckedHeight", typeof(double), typeof(UIProperties), new FrameworkPropertyMetadata(32.0, FrameworkPropertyMetadataOptions.AffectsRender));

        public static void SetCheckedHeight(DependencyObject element, double value)
        {
            element.SetValue(CheckedHeightProperty, value);
        }

        public static double GetCheckedHeight(DependencyObject element)
        {
            return (double)element.GetValue(CheckedHeightProperty);
        }

        public static readonly DependencyProperty CheckedMarginProperty = DependencyProperty.RegisterAttached(
            "CheckedMargin", typeof(Thickness), typeof(UIProperties), new FrameworkPropertyMetadata(new Thickness(0), FrameworkPropertyMetadataOptions.AffectsRender));

        public static void SetCheckedMargin(DependencyObject element, Thickness value)
        {
            element.SetValue(CheckedMarginProperty, value);
        }

        public static Thickness GetCheckedMargin(DependencyObject element)
        {
            return (Thickness)element.GetValue(CheckedMarginProperty);
        }

        public static readonly DependencyProperty BorderBrushProperty = DependencyProperty.RegisterAttached(
            "BorderBrush", typeof(Brush), typeof(UIProperties), new FrameworkPropertyMetadata(Brushes.White, FrameworkPropertyMetadataOptions.AffectsRender));

        public static void SetBorderBrush(DependencyObject element, Brush value)
        {
            element.SetValue(BorderBrushProperty, value);
        }

        public static Brush GetBorderBrush(DependencyObject element)
        {
            return (Brush)element.GetValue(BorderBrushProperty);
        }

        public static readonly DependencyProperty MouseOverBorderBrushProperty = DependencyProperty.RegisterAttached(
            "MouseOverBorderBrush", typeof(Brush), typeof(UIProperties), new FrameworkPropertyMetadata(Brushes.White, FrameworkPropertyMetadataOptions.AffectsRender));

        public static void SetMouseOverBorderBrush(DependencyObject element, Brush value)
        {
            element.SetValue(MouseOverBorderBrushProperty, value);
        }

        public static Brush GetMouseOverBorderBrush(DependencyObject element)
        {
            return (Brush)element.GetValue(MouseOverBorderBrushProperty);
        }


        public static readonly DependencyProperty CheckedBorderBrushProperty = DependencyProperty.RegisterAttached(
            "CheckedBorderBrush", typeof(Brush), typeof(UIProperties), new FrameworkPropertyMetadata(Brushes.White, FrameworkPropertyMetadataOptions.AffectsRender));

        public static void SetCheckedBorderBrush(DependencyObject element, Brush value)
        {
            element.SetValue(CheckedBorderBrushProperty, value);
        }

        public static Brush GetCheckedBorderBrush(DependencyObject element)
        {
            return (Brush)element.GetValue(CheckedBorderBrushProperty);
        }

        public static readonly DependencyProperty BorderThicknessProperty = DependencyProperty.RegisterAttached(
            "BorderThickness", typeof(Thickness), typeof(UIProperties), new FrameworkPropertyMetadata(new Thickness(0), FrameworkPropertyMetadataOptions.AffectsRender));

        public static void SetBorderThickness(DependencyObject element, Thickness value)
        {
            element.SetValue(BorderThicknessProperty, value);
        }

        public static Thickness GetBorderThickness(DependencyObject element)
        {
            return (Thickness)element.GetValue(BorderThicknessProperty);
        }
    }
}
