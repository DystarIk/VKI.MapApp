using VKI.MapApp.Data;

namespace VKI.MapApp.Models;

public class UserSelection
{
    public int SelectedCourseId { get; set; }
    public int SelectedGroupId { get; set; }
    public int SelectedThemeId { get; set; }


    public static void SaveSelection()
    {
        UserSelection selection = new();

        selection.SelectedCourseId = SettingsApp.SelectedCourse?.Id - 1 ?? 0;
        selection.SelectedGroupId = SettingsApp.SelectedGroup?.Id - 1 ?? 0;
        selection.SelectedThemeId = ThemeManager.ThemeId;

        JsonDataManager.Save(selection, "Assets/Config/UserSelection.json");
    }
}