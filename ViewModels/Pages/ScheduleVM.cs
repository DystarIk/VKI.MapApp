using System.ComponentModel;
using System.Runtime.CompilerServices;
using VKI.MapApp.Models;
using VKI.MapApp.ViewModels.Controls;
using VKI.ScheduleLib.Models;

namespace VKI.MapApp.ViewModels.Pages;

public class ScheduleVM : INotifyPropertyChanged
{
    public string Background => ThemeManager.ThemeBackground;
    public string Secondary => ThemeManager.ThemeSecondary;
    public string ThemeText => ThemeManager.ThemeText;

    public DayBlockVM? Monday { get; set; }
    public DayBlockVM? Tuesday { get; set; }
    public DayBlockVM? Wednesday { get; set; }
    public DayBlockVM? Thursday { get; set; }
    public DayBlockVM? Friday { get; set; }
    public DayBlockVM? Saturday { get; set; }


    private Schedule? _schedule = SettingsApp.SelectedGroup?.Schedule ?? null;


    public ScheduleVM()
    {
        SettingsApp.PropertyChanged += SettingsAppProperty;
        ThemeManager.PropertyChanged += ThemeManagerProperty;

        ChangingTheme();
        UpdateSchedule();
    }


    public Group? SelectedGroup => SettingsApp.SelectedGroup;



    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    private void ChangingTheme()
    {
        OnPropertyChanged(nameof(ThemeText));
        OnPropertyChanged(nameof(Secondary));
        OnPropertyChanged(nameof(Background));
    }
    private void UpdateSchedule()
    {
        _schedule = SettingsApp.SelectedGroup?.Schedule ?? null;


        Monday = new(_schedule?.Subjects.GetRange(0, 5) ?? null);
        Tuesday = new(_schedule?.Subjects.GetRange(5, 5) ?? null);
        Wednesday = new(_schedule?.Subjects.GetRange(10, 5) ?? null);
        Thursday = new(_schedule?.Subjects.GetRange(15, 5) ?? null);
        Friday = new(_schedule?.Subjects.GetRange(20, 5) ?? null);
        Saturday = new(_schedule?.Subjects.GetRange(25, 5) ?? null);

        OnPropertyChanged(nameof(Monday));
        OnPropertyChanged(nameof(Tuesday));
        OnPropertyChanged(nameof(Wednesday));
        OnPropertyChanged(nameof(Thursday));
        OnPropertyChanged(nameof(Friday));
        OnPropertyChanged(nameof(Saturday));
    }
    private void SettingsAppProperty(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(SettingsApp.SelectedGroup))
        {
            UpdateSchedule();
        }
    }
    private void ThemeManagerProperty(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(ThemeManager.ThemeId))
        {
            ChangingTheme();
        }
    }
}