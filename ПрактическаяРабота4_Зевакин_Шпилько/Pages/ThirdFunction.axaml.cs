using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Controls.Shapes;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using WpfLikeAvaloniaNavigation;

namespace ПрактическаяРабота4_Зевакин_Шпилько.Pages;

public partial class ThirdFunction : Page
{
    public ThirdFunction()
    {
        InitializeComponent();
    }

    private List<(double x, double y)> _points = new();

    private double F(double x, double b)
    {
        return Math.Pow(x, 4) + Math.Cos(2 + Math.Pow(x, 3) - b);
    }

    private void Button_Vicheslit_OnClick(object? sender, RoutedEventArgs e)
    {
        try
        {
            double x0 = double.Parse(BoxX0.Text.Replace(".", ","));
            double xk = double.Parse(BoxXk.Text.Replace(".", ","));
            double dx = double.Parse(BoxDx.Text.Replace(".", ","));
            double b  = double.Parse(BoxB.Text.Replace(".", ","));

            _points.Clear();
            ResultList.Items.Clear();

            for (double x = x0; x <= xk + 1e-9; x += dx)
            {
                double y = F(x, b);
                _points.Add((x, y));
                ResultList.Items.Add($"{Math.Round(y, 4)}");
            }

            DrawChart();
        }
        catch (Exception ex)
        {
            ResultList.Items.Clear();
            ResultList.Items.Add($"Ошибка: {ex.Message}");
        }
    }

    private void DrawChart()
    {
        if (string.IsNullOrWhiteSpace(BoxX0.Text) ||
            string.IsNullOrWhiteSpace(BoxXk.Text) ||
            string.IsNullOrWhiteSpace(BoxDx.Text) ||
            string.IsNullOrWhiteSpace(BoxB.Text))
        {
            ResultList.Items.Clear();
            
            var mainWindow = (Application.Current.ApplicationLifetime
                as IClassicDesktopStyleApplicationLifetime)?.MainWindow;
            var dialogg= new Messagebox("Заполните все поля");
            dialogg.ShowDialog(mainWindow);
            return;
        }

        if (!double.TryParse(BoxX0.Text.Replace(".", ","), out double x0) ||
            !double.TryParse(BoxXk.Text.Replace(".", ","), out double xk) ||
            !double.TryParse(BoxDx.Text.Replace(".", ","), out double dx) ||
            !double.TryParse(BoxB.Text.Replace(".", ","), out double b))
        {
            ResultList.Items.Clear();
            
            var mainWindow = (Application.Current.ApplicationLifetime
                as IClassicDesktopStyleApplicationLifetime)?.MainWindow;
            var dialogg= new Messagebox("Введите корректные числа");
            dialogg.ShowDialog(mainWindow); 
            return;
        }

        if (dx <= 0)
        {
            ResultList.Items.Clear();
            
            var mainWindow = (Application.Current.ApplicationLifetime
                as IClassicDesktopStyleApplicationLifetime)?.MainWindow;
            var dialogg= new Messagebox("dX должен быть больше 0!");
            dialogg.ShowDialog(mainWindow);
            return;
        }

        if (x0 >= xk)
        {
            ResultList.Items.Clear();
            var mainWindow = (Application.Current.ApplicationLifetime
                as IClassicDesktopStyleApplicationLifetime)?.MainWindow;
            var dialogg= new Messagebox("X₀ должен быть меньше Xₖ!");
            dialogg.ShowDialog(mainWindow);
            return;
        }

        if ((xk - x0) / dx > 10000)
        {
            ResultList.Items.Clear();
            var mainWindow = (Application.Current.ApplicationLifetime
                as IClassicDesktopStyleApplicationLifetime)?.MainWindow;
            var dialogg= new Messagebox("Слишком много точек, увеличьте dX!");
            dialogg.ShowDialog(mainWindow);
            return;
        }

        ChartCanvas.Children.Clear();

        if (_points.Count < 2) return;

        double canvasW = ChartCanvas.Bounds.Width;
        double canvasH = ChartCanvas.Bounds.Height;

        
        if (canvasW < 1) canvasW = 500;
        if (canvasH < 1) canvasH = 320;

        double padding = 30;
        double minX = _points.Min(p => p.x);
        double maxX = _points.Max(p => p.x);
        double minY = _points.Min(p => p.y);
        double maxY = _points.Max(p => p.y);

        double rangeX = maxX - minX == 0 ? 1 : maxX - minX;
        double rangeY = maxY - minY == 0 ? 1 : maxY - minY;

        
        double ToCanvasX(double x) =>
            padding + (x - minX) / rangeX * (canvasW - padding * 2);
        double ToCanvasY(double y) =>
            (canvasH - padding) - (y - minY) / rangeY * (canvasH - padding * 2);
        
        for (int i = 0; i <= 5; i++)
        {
            double gy = padding + i * (canvasH - padding * 2) / 5;
            var gridLine = new Line
            {
                StartPoint = new Point(padding, gy),
                EndPoint = new Point(canvasW - padding, gy),
                Stroke = new SolidColorBrush(Color.Parse("#E2E8F0")),
                StrokeThickness = 1
            };
            ChartCanvas.Children.Add(gridLine);
        }
        
        var polyline = new Polyline
        {
            Stroke = new SolidColorBrush(Color.Parse("#4299E1")),
            StrokeThickness = 2
        };

        var pointsList = new Points();
        foreach (var (x, y) in _points)
            pointsList.Add(new Point(ToCanvasX(x), ToCanvasY(y)));

        polyline.Points = pointsList;
        ChartCanvas.Children.Add(polyline);
        
        foreach (var (x, _) in _points)
        {
            var label = new TextBlock
            {
                Text = $"{Math.Round(x, 2)}",
                FontSize = 9,
                Foreground = new SolidColorBrush(Color.Parse("#718096"))
            };
            Canvas.SetLeft(label, ToCanvasX(x) - 10);
            Canvas.SetTop(label, canvasH - padding + 4);
            ChartCanvas.Children.Add(label);
        }
    }

    private void Button_clear_OnClick(object? sender, RoutedEventArgs e)
    {
        BoxX0.Text = "";
        BoxXk.Text = "";
        BoxDx.Text = "";
        BoxB.Text  = "";
        ResultList.Items.Clear();
        ChartCanvas.Children.Clear();
        _points.Clear();
    }

    private void Button_back_OnClick_OnClick(object? sender, RoutedEventArgs e)
    {
        if (NavigationService.CanGoBack)
        {
            NavigationService.GoBack();
        } 
    }
}