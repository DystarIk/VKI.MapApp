using System.Windows.Controls;
using VKI.MapApp.ViewModels.Controls;

namespace VKI.MapApp.Views.Controls;

public partial class FloorBlock2 : UserControl
{
    public FloorBlock2()
    {
        InitializeComponent();
        DataContext = new FloorBlock2VM();
    }
}