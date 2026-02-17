using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using WpfLikeAvaloniaNavigation;

namespace ПрактическаяРабота4_Зевакин_Шпилько.Pages;

public partial class SecondFunction : Page
{
    public SecondFunction()
    {
        InitializeComponent();
    }

    private void Button_vicheslit_Click(object? sender, RoutedEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(BoxX.Text) ||
            string.IsNullOrWhiteSpace(BoxM.Text))
        {
            
            var mainWindow = (Application.Current.ApplicationLifetime
                as IClassicDesktopStyleApplicationLifetime)?.MainWindow;
            var dialogg= new Messagebox("не все поля заполнены");
            dialogg.ShowDialog(mainWindow);
            return;
        }

        if (!double.TryParse(BoxX.Text.Replace(".", ","), out double x) ||
            !int.TryParse(BoxM.Text, out int m))
        {
            
            var mainWindow = (Application.Current.ApplicationLifetime
                as IClassicDesktopStyleApplicationLifetime)?.MainWindow;
            var dialogg= new Messagebox("введите корректные числа");
            dialogg.ShowDialog(mainWindow);
            return;
        }

        if (RadioButton_one.IsChecked != true &&
            RadioButton_two.IsChecked != true &&
            RadioButton_three.IsChecked != true)
        {
            
            var mainWindow = (Application.Current.ApplicationLifetime
                as IClassicDesktopStyleApplicationLifetime)?.MainWindow;
            var dialogg= new Messagebox("Выберете функцию!");
            dialogg.ShowDialog(mainWindow);
            return;
        }
        
        try
        {
             x = double.Parse(BoxX.Text.Replace(".", ","));
             m = int.Parse(BoxM.Text);
             
            double fx;
            if (RadioButton_one.IsChecked == true)
                fx = Math.Sinh(x);        
            else if (RadioButton_two.IsChecked == true)
                fx = x * x;                
            else if (RadioButton_three.IsChecked == true)
                fx = Math.Exp(x);            
            else
            {
                
                var mainWindow = (Application.Current.ApplicationLifetime
                    as IClassicDesktopStyleApplicationLifetime)?.MainWindow;
                var dialogg= new Messagebox("Функция не выбрана");
                dialogg.ShowDialog(mainWindow);
                return;
              
            }

            double result;

            if (m % 2 != 0 && x > 0)
            {
                result = m * Math.Sqrt(fx);
            }
            else if (m % 2 == 0 && x < 0)
            {
                result = (m / 2.0) * Math.Sqrt(Math.Abs(fx));
            }
            else
            {
                result = Math.Sqrt(Math.Abs(fx));
            }

            Box_Rez.Text = $"e = {result:F4}";
        }
        catch (Exception ex)
        {
            Box_Rez.Text = $"Ошибка: {ex.Message}";
        }
    }

    private void Button_clear_Click(object? sender, RoutedEventArgs e)
    {
        BoxX.Text = "";
        BoxM.Text = "";
        RadioButton_one.IsChecked = false;
        RadioButton_two.IsChecked = false;
        RadioButton_three.IsChecked = false;
        Box_Rez.Text = "—";
    }
    private void Button_back_OnClick_OnClick(object? sender, RoutedEventArgs e)
    {
        if (NavigationService.CanGoBack)
        {
            NavigationService.GoBack();
        }
    }
}