using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using WpfLikeAvaloniaNavigation;

namespace ПрактическаяРабота4_Зевакин_Шпилько.Pages;

public partial class FirstPage : Page
{
    public FirstPage()
    {
        InitializeComponent();
    }

    private void Button_OnClick(object? sender, RoutedEventArgs e)
    {
        NavigationService?.Navigate(new FirstFunction());
    }

    private void Button_OnClick1(object? sender, RoutedEventArgs e)
    {
        NavigationService?.Navigate(new SecondFunction());;
    }

    private void Button_OnClick2(object? sender, RoutedEventArgs e)
    {
        NavigationService.Navigate(new ThirdFunction());;
    }
}