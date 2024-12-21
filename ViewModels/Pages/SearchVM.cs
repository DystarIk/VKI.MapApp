using System.ComponentModel;
using System.Runtime.CompilerServices;
using VKI.MapApp.Models;
using VKI.MapApp.ViewModels.Controls;
using VKI.ScheduleLib.Models;
using VKI.ScheduleLib.Services;

namespace VKI.MapApp.ViewModels.Pages;

public partial class SearchVM : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;
    public DayBlockVM? Monday { get; set; }
    public DayBlockVM? Tuesday { get; set; }
    public DayBlockVM? Wednesday { get; set; }
    public DayBlockVM? Thursday { get; set; }
    public DayBlockVM? Friday { get; set; }
    public DayBlockVM? Saturday { get; set; }

    public string Background => ThemeManager.ThemeBackground;
    public string Secondary => ThemeManager.ThemeSecondary;
    public string ThemeText => ThemeManager.ThemeText;
    public string? Text { get; set; }
    public List<string> Tags { get; } = new()
    {
       "Группа",
       "Преподаватель",
       "Кабинет"
    };

    private string? _selectedTag;
    private RelayCommand? _btSearch;


    public SearchVM()
    {
        ThemeManager.PropertyChanged += ThemeManagerPropertyChanged;
        ChangingTheme();
    }


    public string? SelectedTag
    {
        get => _selectedTag;
        set
        {
            _selectedTag = value;
            OnPropertyChanged();
        }
    }
    public RelayCommand BtSearch
    {
        get => _btSearch ?? (_btSearch = new RelayCommand(obj => { Find(); }));
    }


    public void PopulateSchedule(List<Subject> subjects)
    {
        Monday = null;
        Tuesday = null;
        Wednesday = null;
        Thursday = null;
        Friday = null;
        Saturday = null;

        Dictionary<DayOfWeek, Action<DayBlockVM>> dayProperties = new()
        {
            { DayOfWeek.Monday, vm => Monday = vm },
            { DayOfWeek.Tuesday, vm => Tuesday = vm },
            { DayOfWeek.Wednesday, vm => Wednesday = vm },
            { DayOfWeek.Thursday, vm => Thursday = vm },
            { DayOfWeek.Friday, vm => Friday = vm },
            { DayOfWeek.Saturday, vm => Saturday = vm }
        };
        var groupedByDay = subjects.GroupBy(s => s.DayOfWeek);

        foreach (var group in groupedByDay)
        {
            var dayOfWeek = group.Key;

            if (dayProperties.ContainsKey(dayOfWeek))
            {
                var dayBlock = new DayBlockVM(group.ToList());

                dayProperties[dayOfWeek](dayBlock);
            }
        }
        HackyFix(Monday);
        HackyFix(Tuesday);
        HackyFix(Wednesday);
        HackyFix(Thursday);
        HackyFix(Friday);
        HackyFix(Saturday);

        OnPropertyChanged(nameof(Monday));
        OnPropertyChanged(nameof(Tuesday));
        OnPropertyChanged(nameof(Wednesday));
        OnPropertyChanged(nameof(Thursday));
        OnPropertyChanged(nameof(Friday));
        OnPropertyChanged(nameof(Saturday));
    }

    protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    private void Find()
    {
        Searcher searcher = new(SettingsApp.College);
        switch (SelectedTag)
        {
            case "Группа":
                PopulateSchedule(searcher.FindSubjectsByGroupName(Text));
                break;

            case "Преподаватель":
                PopulateSchedule(searcher.FindSubjectsByTeacherName(Text));
                break;

            case "Кабинет":
                PopulateSchedule(searcher.FindSubjectsByRoomNumber(Text));
                break;
        }
    }
    private void ChangingTheme()
    {
        OnPropertyChanged(nameof(ThemeText));
        OnPropertyChanged(nameof(Secondary));
        OnPropertyChanged(nameof(Background));
    }
    private void HackyFix(DayBlockVM? daily)
    {
        if (daily == null) return;
        if (daily.SubjectBlockVMs!.Count == 5) return;

        List<SubjectBlockVM> subjectBlock = new();

        List<Subject> subjects =
       [
            new() { Name = "", LessonNumber = "1", DayOfWeek = daily.Subjects![0].DayOfWeek },
            new() { Name = "", LessonNumber = "2", DayOfWeek = daily.Subjects![0].DayOfWeek },
            new() { Name = "", LessonNumber = "3", DayOfWeek = daily.Subjects![0].DayOfWeek },
            new() { Name = "", LessonNumber = "4", DayOfWeek = daily.Subjects![0].DayOfWeek },
            new() { Name = "", LessonNumber = "5", DayOfWeek = daily.Subjects![0].DayOfWeek },
        ];

        foreach (var subject in daily.Subjects!)
        {
            int id = Convert.ToInt32(subject.LessonNumber);
            subjects[id - 1] = subject;
        }

        foreach (var subject in subjects)
        {
            subjectBlock.Add(new(subject));
        }

        daily.SubjectBlockVMs = subjectBlock!;
    }
    private void ThemeManagerPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(ThemeManager.ThemeId))
        {
            ChangingTheme();
        }
    }
}