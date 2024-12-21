using System.ComponentModel;
using System.Runtime.CompilerServices;
using VKI.MapApp.Models;
using VKI.ScheduleLib.Models;

namespace VKI.MapApp.ViewModels.Controls;

public class SubjectBlockVM : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;
    public Subject Subject { get; set; }
    public string Background => ThemeManager.ThemeBackground;
    public string Secondary => Check() ? ThemeManager.ThemeSecondary : ThemeManager.ThemeBackground;
    public string ThemeText => ThemeManager.ThemeText;


    public SubjectBlockVM(Subject subject)
    {
        ThemeManager.PropertyChanged += ThemeManagerPropertyChanged;
        Subject = subject;
    }


    protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    private bool Check()
    {
        return Subject.IsTimeInLesson();
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