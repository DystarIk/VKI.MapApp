using System.ComponentModel;
using VKI.MapApp.Data;

namespace VKI.MapApp.Models;

public static class ThemeManager
{
    private static int _themeId = 0;
    private static readonly Dictionary<int, string> _themeButton = new()
    {
        {0, "#f582ae"},
        {1, "#ffc0ad"},
        {2, "#ff8ba7"},
        {3, "#ffd803"},
        {4, "#ff8906"},
        {5, "#eebbc3"},
        {6, "#8c7851"}
    };
    private static readonly Dictionary<int, string> _themeBackground = new()
    {
        {0, "#fef6e4"},
        {1, "#55423d"},
        {2, "#faeee7"},
        {3, "#fffffe"},
        {4, "#0f0e17"},
        {5, "#232946"},
        {6, "#f9f4ef"}
    };
    private static readonly Dictionary<int, string> _themeSecondary = new()
    {
        {0, "#8bd3dd"},
        {1, "#ffc0ad"},
        {2, "#ffc6c7"},
        {3, "#e3f6f5"},
        {4, "#ff8906"},
        {5, "#fffffe"},
        {6, "#eaddcf"}
    };
    private static readonly Dictionary<int, string> _themeText = new()
    {
        {0, "#172c66"},
        {1, "#271c19"},
        {2, "#33272a"},
        {3, "#172c66"},
        {4, "#e53170"},
        {5, "#b8c1ec"},
        {6, "#f25042"}
    };

    public static int ThemeId
    {
        get => _themeId;
        set
        {
            _themeId = value;
            UserSelection.SaveSelection();
            OnPropertyChanged(nameof(ThemeId));
            OnPropertyChanged(nameof(ThemeButton));
            OnPropertyChanged(nameof(ThemeBackground));
            OnPropertyChanged(nameof(ThemeSecondary));
            OnPropertyChanged(nameof(ThemeText));
        }
    }


    public static string ThemeButton => _themeButton[_themeId];
    public static string ThemeBackground => _themeBackground[_themeId];
    public static string ThemeSecondary => _themeSecondary[_themeId];
    public static string ThemeText => _themeText[_themeId];


   
    private static void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(null, new PropertyChangedEventArgs(propertyName));
    }
    public static event EventHandler<PropertyChangedEventArgs>? PropertyChanged;
}