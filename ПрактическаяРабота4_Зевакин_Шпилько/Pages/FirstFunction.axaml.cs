using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using WpfLikeAvaloniaNavigation;


namespace ПрактическаяРабота4_Зевакин_Шпилько.Pages;

public partial class FirstFunction : Page
{
    public FirstFunction()
    {
        InitializeComponent();
    }

    private void Button_Vicheslit_OnClick(object? sender, RoutedEventArgs e)
    {
        if (String.IsNullOrWhiteSpace(BoxX.Text) || String.IsNullOrWhiteSpace(BoxY.Text) ||
            String.IsNullOrWhiteSpace(BoxZ.Text))
        {
            var mainWindow = (Application.Current.ApplicationLifetime
                as IClassicDesktopStyleApplicationLifetime)?.MainWindow;
            var dialogg= new Messagebox("Не все поля заполнены");
            dialogg.ShowDialog(mainWindow);
            return;
        }
        try
        {
            double x = double.Parse(BoxX.Text.Replace(".", ","));
            double y = double.Parse(BoxY.Text.Replace(".", ","));
            
            double z = double.Parse(BoxZ.Text.Replace(".", ","));
        
            double part1 = Math.Log(Math.Pow(y, -Math.Sqrt(Math.Abs(x))));
            double part2 = x - y / 2;
            double part3 = Math.Pow(Math.Sin(Math.Atan(z)), 2);

            double a = part1 * part2 + part3;
            if (y <= 0)
            {
                var mainWindow = (Application.Current.ApplicationLifetime
                    as IClassicDesktopStyleApplicationLifetime)?.MainWindow;
                var dialogg= new Messagebox("y нельзя меньше нуля");
                dialogg.ShowDialog(mainWindow);
                return;
            }
            Box_rez.Text = $"a = {a}";
        }
        catch (Exception ex)
        {
            Box_rez.Text = $"Ошибка: {ex.Message}";
        }
    }

    private void Button_clear_OnClick(object? sender, RoutedEventArgs e)
    {
        BoxX.Text = "";
        BoxY.Text = "";
        BoxZ.Text = "";
        Box_rez.Text = "Результат";
    }

    private void Button_back_OnClick_OnClick(object? sender, RoutedEventArgs e)
    {
        if (NavigationService.CanGoBack)
        {
            NavigationService.GoBack();
        }
    }
}