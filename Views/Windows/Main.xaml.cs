using System.Windows;
using VKI.MapApp.ViewModels.Windows;

namespace VKI.MapApp.Views.Windows;

public partial class Main : Window
{
    public Main()
    {
        InitializeComponent();

        DataContext = new MainVM();
    }
}