using System.Windows.Controls;
using VKI.MapApp.ViewModels.Controls;

namespace VKI.MapApp.Views.Controls;

public partial class FloorBlock3 : UserControl
{
    public FloorBlock3()
    {
        InitializeComponent();
        DataContext = new FloorBlock3VM();
    }
}