using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Controls.Shapes;
using Avalonia.Interactivity;
using Avalonia.Media;
using WpfLikeAvaloniaNavigation;

namespace ПрактическаяРабота4_Зевакин_Шпилько.Pages;

public partial class ThirdFunction : Page
{
    public static double CalculateF(double x, double b)
    {
        return Math.Pow(x, 4) + Math.Cos(2 + Math.Pow(x, 3) - b);
    }
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
        Vich(BoxX0, BoxXk, BoxDx, BoxB);
    }

    public bool Vich(TextBox boxX0, TextBox boxXk, TextBox boxDx, TextBox boxB)
    {
        if (string.IsNullOrWhiteSpace(boxX0.Text) ||
            string.IsNullOrWhiteSpace(boxXk.Text) ||
            string.IsNullOrWhiteSpace(boxDx.Text) ||
            string.IsNullOrWhiteSpace(boxB.Text))
        {
            var mainWindow = (Application.Current.ApplicationLifetime
                as IClassicDesktopStyleApplicationLifetime)?.MainWindow;
            var dialog = new Messagebox("Заполните все поля");
            dialog.ShowDialog(mainWindow);
            return false;
        }

        if (!double.TryParse(boxX0.Text.Replace(".", ","), out double x0) ||
            !double.TryParse(boxXk.Text.Replace(".", ","), out double xk) ||
            !double.TryParse(boxDx.Text.Replace(".", ","), out double dx) ||
            !double.TryParse(boxB.Text.Replace(".", ","), out double b))
        {
            var mainWindow = (Application.Current.ApplicationLifetime
                as IClassicDesktopStyleApplicationLifetime)?.MainWindow;
            var dialog = new Messagebox("Введите корректные числа");
            dialog.ShowDialog(mainWindow);
            return false;
        }

        if (dx <= 0)
        {
            var mainWindow = (Application.Current.ApplicationLifetime
                as IClassicDesktopStyleApplicationLifetime)?.MainWindow;
            var dialog = new Messagebox("dX должен быть больше 0!");
            dialog.ShowDialog(mainWindow);
            return false;
        }

        if (x0 >= xk)
        {
            var mainWindow = (Application.Current.ApplicationLifetime
                as IClassicDesktopStyleApplicationLifetime)?.MainWindow;
            var dialog = new Messagebox("X₀ должен быть меньше Xₖ!");
            dialog.ShowDialog(mainWindow);
            return false;
        }

        if ((xk - x0) / dx > 10000)
        {
            var mainWindow = (Application.Current.ApplicationLifetime
                as IClassicDesktopStyleApplicationLifetime)?.MainWindow;
            var dialog = new Messagebox("Слишком много точек, увеличьте dX!");
            dialog.ShowDialog(mainWindow);
            return false;
        }

        try
        {
            _points.Clear();
            ResultList.Items.Clear();

            for (double x = x0; x <= xk + 1e-9; x += dx)
            {
                double y = F(x, b);
                _points.Add((x, y));
                ResultList.Items.Add($"{Math.Round(y, 4)}");
            }

            DrawChart(x0, xk, dx, b);
            return true;
        }
        catch (Exception ex)
        {
            ResultList.Items.Clear();
            ResultList.Items.Add($"Ошибка: {ex.Message}");
            return false;
        }
    }

    private void DrawChart(double x0, double xk, double dx, double b)
    {
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
        BoxB.Text = "";
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