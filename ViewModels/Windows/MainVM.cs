using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using SkiaSharp;
using VKI.MapApp.Models;
using VKI.MapApp.Views.Pages;
using VKI.MapApp.Views.Windows;
using VKI.ScheduleLib.Data;
using VKI.MapApp.Data;

namespace VKI.MapApp.ViewModels.Windows;

public partial class MainVM : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    private bool _isBtMapActive;
    private bool _isBtScheduleActive;
    private bool _isBtSearchActive;
    private bool _isBtUserActive;

    private RelayCommand? _goToMap;
    private RelayCommand? _goToSchedule;
    private RelayCommand? _goToSearch;
    private RelayCommand? _goToSettings;

    private BitmapSource _BtMapActive = null!;
    private BitmapSource _BtMapInactive = null!;

    private BitmapSource _BtScheduleActive = null!;
    private BitmapSource _BtScheduleInactive = null!;

    private BitmapSource _BtSearchActive = null!;
    private BitmapSource _BtSearchInactive = null!;

    private BitmapSource _BtUserActive = null!;
    private BitmapSource _BtUserInactive = null!;


    public MainVM()
    {
        ThemeManager.PropertyChanged += ThemeManagerPropertyChanged;
        AppDbContext appDb = new();
        SettingsApp.College = appDb.LoadCollege();

        var userSelection = JsonDataManager.Load<UserSelection>("Assets/Config/UserSelection.json");
        if (userSelection != null)
        {
            SettingsApp.SelectedCourse = SettingsApp.College.Courses[userSelection.SelectedCourseId];
            SettingsApp.SelectedGroup = SettingsApp.SelectedCourse.Groups[userSelection.SelectedGroupId];
            ThemeManager.ThemeId = userSelection.SelectedThemeId;
        }


        ChangingTheme();
        BtClickMap();
    }
    

    public bool IsBtMapActive
    {
        get => _isBtMapActive;
        set
        {
            _isBtMapActive = value;
            OnPropertyChanged(nameof(BtMap));
        }
    }
    public bool IsBtScheduleActive
    {
        get => _isBtScheduleActive;
        set
        {
            _isBtScheduleActive = value;
            OnPropertyChanged(nameof(BtSchedule));
        }
    }
    public bool IsBtSearchActive
    {
        get => _isBtSearchActive;
        set
        {
            _isBtSearchActive = value;
            OnPropertyChanged(nameof(BtSearch));
        }
    }
    public bool IsBtUserActive
    {
        get => _isBtUserActive;
        set
        {
            _isBtUserActive = value;
            OnPropertyChanged(nameof(BtSettings));
        }
    }

    public string Secondary => ThemeManager.ThemeSecondary;
    public string Background => ThemeManager.ThemeBackground;

    public BitmapSource BtMap => _isBtMapActive ? _BtMapActive : _BtMapInactive;
    public BitmapSource BtSchedule => _isBtScheduleActive ? _BtScheduleActive : _BtScheduleInactive;
    public BitmapSource BtSearch => _isBtSearchActive ? _BtSearchActive : _BtSearchInactive;
    public BitmapSource BtSettings => _isBtUserActive ? _BtUserActive : _BtUserInactive;


    protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    private void BtInactive()
    {
        IsBtMapActive = false;
        IsBtScheduleActive = false;
        IsBtSearchActive = false;
        IsBtUserActive = false;
    }
    private void BtClickMap()
    {
        BtInactive();
        IsBtMapActive = true;
        NavigateToPage(new Map());
    }
    private void BtClickSchedule()
    {
        BtInactive();
        IsBtScheduleActive = true;
        NavigateToPage(new Schedule());
    }
    private void BtClickSearch()
    {
        BtInactive();
        IsBtSearchActive = true;
        NavigateToPage(new Search());
    }
    private void BtClickSettings()
    {
        BtInactive();
        IsBtUserActive = true;
        NavigateToPage(new Settings());
    }

    private void ChangingTheme()
    {
        _BtMapActive = new SvgRenderer(SKColor.Parse(ThemeManager.ThemeButton), "Assets/Icons/Active/Map.svg").SvgToBitMap();
        _BtMapInactive = new SvgRenderer(SKColor.Parse(ThemeManager.ThemeButton), "Assets/Icons/Inactive/Map.svg").SvgToBitMap();

        _BtScheduleActive = new SvgRenderer(SKColor.Parse(ThemeManager.ThemeButton), "Assets/Icons/Active/Schedule.svg").SvgToBitMap();
        _BtScheduleInactive = new SvgRenderer(SKColor.Parse(ThemeManager.ThemeButton), "Assets/Icons/Inactive/Schedule.svg").SvgToBitMap();

        _BtSearchActive = new SvgRenderer(SKColor.Parse(ThemeManager.ThemeButton), "Assets/Icons/Active/Search.svg").SvgToBitMap();
        _BtSearchInactive = new SvgRenderer(SKColor.Parse(ThemeManager.ThemeButton), "Assets/Icons/Inactive/Search.svg").SvgToBitMap();

        _BtUserActive = new SvgRenderer(SKColor.Parse(ThemeManager.ThemeButton), "Assets/Icons/Active/User.svg").SvgToBitMap();
        _BtUserInactive = new SvgRenderer(SKColor.Parse(ThemeManager.ThemeButton), "Assets/Icons/Inactive/User.svg").SvgToBitMap();

        OnPropertyChanged(nameof(Secondary));
        OnPropertyChanged(nameof(Background));
        OnPropertyChanged(nameof(BtMap));
        OnPropertyChanged(nameof(BtSchedule));
        OnPropertyChanged(nameof(BtSearch));
        OnPropertyChanged(nameof(BtSettings));
    }
    private void NavigateToPage(Page page)
    {
        if (Application.Current.MainWindow is Main mainWindow)
            mainWindow.MainWindows.Navigate(page);
    }
    private void ThemeManagerPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(ThemeManager.ThemeId)) ChangingTheme();
    }
}