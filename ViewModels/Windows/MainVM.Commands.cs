using System.Windows.Media.Imaging;
using VKI.MapApp.Models;

namespace VKI.MapApp.ViewModels.Windows;

public partial class MainVM
{
    public RelayCommand GoToMap
    {
        get => _goToMap ?? (_goToMap = new(obj => { BtClickMap(); }));
    }
    public RelayCommand GoToSchedule
    {
        get => _goToSchedule ?? (_goToSchedule = new(obj => { BtClickSchedule(); }));
    }
    public RelayCommand GoToSearch
    {
        get => _goToSearch ?? (_goToSearch = new(obj => { BtClickSearch(); }));
    }
    public RelayCommand GoToSettings
    {
        get => _goToSettings ?? (_goToSettings = new(obj => { BtClickSettings(); }));
    }
}