using System.Windows.Controls;
using VKI.MapApp.ViewModels.Controls;

namespace VKI.MapApp.Views.Controls;

public partial class FloorBlock4 : UserControl
{
    public FloorBlock4()
    {
        InitializeComponent();
        DataContext = new FloorBlock4VM();
    }
}