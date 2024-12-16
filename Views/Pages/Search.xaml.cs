using System.Windows.Controls;
using VKI.MapApp.ViewModels.Pages;

namespace VKI.MapApp.Views.Pages;

public partial class Search : Page
{
    public Search()
    {
        InitializeComponent();
        DataContext = new SearchVM();
    }
}