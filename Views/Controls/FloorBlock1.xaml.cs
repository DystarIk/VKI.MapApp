using System.Windows.Controls;
using VKI.MapApp.ViewModels.Controls;

namespace VKI.MapApp.Views.Controls;

public partial class FloorBlock1 : UserControl
{
    public FloorBlock1()
    {
        InitializeComponent();
        DataContext = new FloorBlock1VM();
    }
}