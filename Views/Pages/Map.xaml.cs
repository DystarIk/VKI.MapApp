using System.Windows.Controls;
using VKI.MapApp.ViewModels.Pages;

namespace VKI.MapApp.Views.Pages;

public partial class Map : Page
{
    public Map()
    {
        InitializeComponent();
        DataContext = new MapVM();
    }
}