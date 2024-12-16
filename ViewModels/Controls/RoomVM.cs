using System.ComponentModel;
using System.Runtime.CompilerServices;
using VKI.MapApp.Models;
using VKI.ScheduleLib.Services;

namespace VKI.MapApp.ViewModels.Controls;

public class RoomVM : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;
    public string RoomNumber { get; set; } = string.Empty;
    public char Floor => RoomNumber[0];

    private Searcher _searcher;


    public RoomVM()
    {
        ThemeManager.PropertyChanged += ThemeManagerPropertyChanged;
        _searcher = new(SettingsApp.College);
    }


    public string Background => Check() ? ThemeManager.ThemeSecondary : ThemeManager.ThemeButton;
    public string Secondary => ThemeManager.ThemeSecondary;
    public string ThemeText => ThemeManager.ThemeText;


    protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    private bool Check()
    {
        return RoomNumber == _searcher.GetCurrentRoomNumberForGroup(SettingsApp.SelectedGroup);
    }
    private void ChangingTheme()
    {
        OnPropertyChanged(nameof(ThemeText));
        OnPropertyChanged(nameof(Secondary));
        OnPropertyChanged(nameof(Background));
    }
    private void ThemeManagerPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(ThemeManager.ThemeId))
        {
            ChangingTheme();
        }
    }
}