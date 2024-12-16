using System.ComponentModel;
using System.Runtime.CompilerServices;
using VKI.MapApp.Models;
using VKI.ScheduleLib.Models;

namespace VKI.MapApp.ViewModels.Pages.SettingsVM
{
    public class SettingsVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private Dictionary<int, string> _lstTheme = new()
        {
            {0,"Тема 1"},
            {1,"Тема 2"},
            {2,"Тема 3"},
            {3,"Тема 4"},
            {4,"Тема 5"},
            {5,"Тема 6"},
            {6,"Тема 7"}
        };
        private int _selectedThemeId;


        public SettingsVM()
        {
            ThemeManager.PropertyChanged += ThemeManagerProperty;
            SettingsApp.PropertyChanged += SettingsAppProperty;
            _selectedThemeId = ThemeManager.ThemeId;

            ChangingTheme();
        }


        public string Background => ThemeManager.ThemeBackground;
        public string Secondary => ThemeManager.ThemeSecondary;
        public string ThemeText => ThemeManager.ThemeText;

        public Dictionary<int, string> ListTheme => _lstTheme;
        public List<Group> Groups => SettingsApp.Groups;
        public List<Course> Courses => SettingsApp.College.Courses;

        public Course? SelectedCourse
        {
            get => SettingsApp.SelectedCourse;
            set
            {
                SettingsApp.SelectedCourse = value;
                OnPropertyChanged(nameof(SelectedCourse));
                OnPropertyChanged(nameof(Groups));
            }
        }
        public Group? SelectedGroup
        {
            get => SettingsApp.SelectedGroup;
            set
            {
                SettingsApp.SelectedGroup = value;
                OnPropertyChanged(nameof(SelectedGroup));
            }
        }
        public int SelectedId
        {
            get => _selectedThemeId;
            set
            {
                _selectedThemeId = value;
                ThemeManager.ThemeId = value;
                OnPropertyChanged(nameof(SelectedId));
            }
        }


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
        private void ThemeManagerProperty(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ThemeManager.ThemeId))
            {
                ChangingTheme();
            }
        }
        private void SettingsAppProperty(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(SettingsApp.SelectedCourse))
            {
                OnPropertyChanged(nameof(SelectedCourse));
                OnPropertyChanged(nameof(Groups));
            }
            if (e.PropertyName == nameof(SettingsApp.SelectedGroup))
            {
                OnPropertyChanged(nameof(SelectedGroup));
            }
        }
    }
}