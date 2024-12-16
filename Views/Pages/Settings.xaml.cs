using System.Windows.Controls;
using VKI.MapApp.ViewModels.Pages.SettingsVM;

namespace VKI.MapApp.Views.Pages;

public partial class Settings : Page
{
    public Settings()
    {
        InitializeComponent();
        DataContext = new SettingsVM();
    }
}