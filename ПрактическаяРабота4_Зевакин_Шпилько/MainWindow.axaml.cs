using Avalonia.Controls;
using ПрактическаяРабота4_Зевакин_Шпилько.Pages;

namespace ПрактическаяРабота4_Зевакин_Шпилько;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        MainContent.Navigate(new FirstPage());
    }
}