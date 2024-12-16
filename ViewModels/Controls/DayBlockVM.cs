using System.ComponentModel;
using System.Runtime.CompilerServices;
using VKI.MapApp.Models;
using VKI.ScheduleLib.Models;

namespace VKI.MapApp.ViewModels.Controls;

public class DayBlockVM : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;
    public List<SubjectBlockVM?>? SubjectBlockVMs { get; set; }
    public List<Subject>? Subjects { get; set; }
    public string Background { get; private set; }
    public string Secondary { get; private set; }
    public string ThemeText { get; private set; }


    public DayBlockVM(List<Subject>? subjects)
    {
        Subjects = subjects;
        ThemeManager.PropertyChanged += ThemeManagerPropertyChanged;
        Background = "#FFFFFF";
        Secondary = "#FFFFFF";
        ThemeText = "#FFFFFF";

        if (subjects != null)
        {
            SubjectBlockVMs = new();

            foreach (var subject in subjects)
                SubjectBlockVMs.Add(new(subject));
        }
        ChangingTheme();
    }


    protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    private void ChangingTheme()
    {
        Secondary = Subjects?[0].IsDayMatch() ?? false ? ThemeManager.ThemeSecondary : ThemeManager.ThemeBackground;

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