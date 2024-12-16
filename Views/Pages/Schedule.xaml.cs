using System.Windows.Controls;
using VKI.MapApp.ViewModels.Pages;

namespace VKI.MapApp.Views.Pages;

public partial class Schedule : Page
{
    public Schedule()
    {
        InitializeComponent();
        DataContext = new ScheduleVM();
    }
}