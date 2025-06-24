using MovieDesktopApp.data.dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;

namespace MovieDesktopApp.helpers
{
    class MovieItemTemplateFactory
    {
        public static DataTemplate CreateTemplate(RoutedEventHandler addFavoriteHandler)
        {
            var stackPanelFactory = new FrameworkElementFactory(typeof(StackPanel));

            // Poster
            var posterImage = new FrameworkElementFactory(typeof(Image));
            posterImage.SetBinding(Image.SourceProperty, new Binding("Poster"));
            posterImage.SetValue(Image.HeightProperty, 200.0);
            posterImage.SetValue(Image.StretchProperty, Stretch.UniformToFill);
            posterImage.SetValue(FrameworkElement.MarginProperty, new Thickness(0, 0, 0, 10));
            stackPanelFactory.AppendChild(posterImage);

            // Title
            var titleText = new FrameworkElementFactory(typeof(TextBlock));
            titleText.SetBinding(TextBlock.TextProperty, new Binding("Title"));
            titleText.SetValue(TextBlock.FontWeightProperty, FontWeights.Bold);
            titleText.SetValue(TextBlock.FontSizeProperty, 14.0);
            titleText.SetValue(TextBlock.TextWrappingProperty, TextWrapping.Wrap);
            stackPanelFactory.AppendChild(titleText);

            // Year
            var yearText = new FrameworkElementFactory(typeof(TextBlock));
            yearText.SetBinding(TextBlock.TextProperty, new Binding("Year"));
            yearText.SetValue(TextBlock.ForegroundProperty, Brushes.Gray);
            yearText.SetValue(TextBlock.FontSizeProperty, 12.0);
            stackPanelFactory.AppendChild(yearText);

            // Botón solo si hay usuario
            if (SessionManager.UserId > 0)
            {
                var favButton = new FrameworkElementFactory(typeof(Button));
                favButton.SetBinding(Button.ContentProperty, new Binding("ActionIcon"));
                favButton.SetValue(Button.BackgroundProperty, Brushes.Transparent);
                favButton.SetValue(Button.ForegroundProperty, Brushes.Red);
                favButton.SetValue(Button.BorderThicknessProperty, new Thickness(0));
                favButton.SetValue(Button.CursorProperty, Cursors.Hand);
                favButton.SetValue(FrameworkElement.MarginProperty, new Thickness(0, 10, 0, 0));
                favButton.SetBinding(Button.TagProperty, new Binding("."));
                favButton.AddHandler(Button.ClickEvent, addFavoriteHandler);
                stackPanelFactory.AppendChild(favButton);
            }

            var borderFactory = new FrameworkElementFactory(typeof(Border));
            borderFactory.SetValue(Border.BorderBrushProperty, Brushes.Gray);
            borderFactory.SetValue(Border.BorderThicknessProperty, new Thickness(1));
            borderFactory.SetValue(Border.PaddingProperty, new Thickness(10));
            borderFactory.SetValue(Border.MarginProperty, new Thickness(10));
            borderFactory.SetValue(Border.WidthProperty, 200.0);
            borderFactory.SetValue(Border.BackgroundProperty, Brushes.White);
            borderFactory.SetValue(Border.CornerRadiusProperty, new CornerRadius(8));
            borderFactory.AppendChild(stackPanelFactory);

            var dataTemplate = new DataTemplate(typeof(MovieDTO))
            {
                VisualTree = borderFactory
            };

            return dataTemplate;
        }



    }
}
