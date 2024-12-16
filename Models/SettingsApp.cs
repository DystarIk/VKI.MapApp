using System.ComponentModel;
using VKI.ScheduleLib.Models;

namespace VKI.MapApp.Models;

public static class SettingsApp
{
    private static College _college = new();
    private static Course? _selectedCourse;
    private static Group? _selectedGroup;

    public static College College
    {
        get => _college;
        set
        {
            _college = value;
            OnPropertyChanged(nameof(College));
        }
    }

    public static Course? SelectedCourse
    {
        get => _selectedCourse;
        set
        {
            _selectedCourse = value;
            UserSelection.SaveSelection();
            OnPropertyChanged(nameof(SelectedCourse));
            OnPropertyChanged(nameof(Groups));
        }
    }

    public static List<Group> Groups => _selectedCourse?.Groups ?? new();

    public static Group? SelectedGroup
    {
        get => _selectedGroup;
        set
        {
            _selectedGroup = value;
            UserSelection.SaveSelection();
            OnPropertyChanged(nameof(SelectedGroup));
        }
    }


    public static event EventHandler<PropertyChangedEventArgs>? PropertyChanged;

    private static void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(null, new PropertyChangedEventArgs(propertyName));
    }
}