using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Controls.Platform;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using WpfLikeAvaloniaNavigation;


namespace ПрактическаяРабота4_Зевакин_Шпилько.Pages;

public partial class FirstFunction : Page
{
    public static double Calculate(double x, double y, double z)
    {
        double part1 = Math.Log(Math.Pow(y, -Math.Sqrt(Math.Abs(x))));
        double part2 = x - y / 2;
        double part3 = Math.Pow(Math.Sin(Math.Atan(z)), 2);
        return part1 * part2 + part3;
    }
    public FirstFunction()
    {
        InitializeComponent();
    }

    private void Button_Vicheslit_OnClick(object? sender, RoutedEventArgs e)
    {
        Vich(BoxX, BoxY, BoxZ);
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
    public bool Vich(TextBox BoxX, TextBox BoxY, TextBox BoxZ)
    {
        if (String.IsNullOrWhiteSpace(BoxX.Text) || String.IsNullOrWhiteSpace(BoxY.Text) ||
            String.IsNullOrWhiteSpace(BoxZ.Text))
        {
            var mainWindow = (Application.Current.ApplicationLifetime
                as IClassicDesktopStyleApplicationLifetime)?.MainWindow;
            var dialogg = new Messagebox("Не все поля заполнены");
            dialogg.ShowDialog(mainWindow);
            return false;
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
                var dialogg = new Messagebox("y нельзя меньше нуля");
                dialogg.ShowDialog(mainWindow);
                return false;
            }
            Box_rez.Text = $"a = {a}";
            return true;
        }
        catch (Exception ex)
        {
            Box_rez.Text = $"Ошибка: {ex.Message}";
            return false;
        }
    }
}