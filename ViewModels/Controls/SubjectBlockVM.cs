using System.ComponentModel;
using System.Runtime.CompilerServices;
using VKI.ScheduleLib.Models;
using VKI.MapApp.Models;

namespace VKI.MapApp.ViewModels.Controls;

public  class SubjectBlockVM : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;
    public Subject Subject { get; set; }
    public string Background { get; private set; }
    public string Secondary { get; private set; }
    public string ThemeText { get; private set; }


    public SubjectBlockVM(Subject subject)
    {
        ThemeManager.PropertyChanged += ThemeManagerPropertyChanged;
        Background = "#FFFFFF";
        ThemeText = "#FFFFFF";
        Secondary = "#FFFFFF";
        Subject = subject;
        ChangingTheme();
    }


    protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    private void ChangingTheme()
    {
        Secondary = Subject.IsTimeInLesson()
            ? ThemeManager.ThemeSecondary
            : ThemeManager.ThemeBackground;

        ThemeText = ThemeManager.ThemeText;
        Background = ThemeManager.ThemeBackground;
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