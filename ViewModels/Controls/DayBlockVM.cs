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
    public string Background => ThemeManager.ThemeBackground;
    public string Secondary => Check() ? ThemeManager.ThemeSecondary : ThemeManager.ThemeBackground;
    public string ThemeText => ThemeManager.ThemeText;


    public DayBlockVM(List<Subject>? subjects)
    {
        Subjects = subjects;
        ThemeManager.PropertyChanged += ThemeManagerPropertyChanged;
        
        if (subjects != null)
        {
            SubjectBlockVMs = new();

            foreach (var subject in subjects)
                SubjectBlockVMs.Add(new(subject));
        }
    }


    protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    private bool Check()
    {
        return Subjects?[0].IsDayMatch() ?? false;
    }
    private void ChangingTheme()
    {
        OnPropertyChanged(nameof(ThemeText));
        OnPropertyChanged(nameof(Secondary));
        OnPropertyChanged(nameof(Background));
    }
    private void ThemeManagerPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(ThemeManager.ThemeId)) ChangingTheme();
    }
}