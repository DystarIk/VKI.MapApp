using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Media.Imaging;
using SkiaSharp;
using VKI.MapApp.Models;
using VKI.MapApp.ViewModels.Controls;

namespace VKI.MapApp.ViewModels.Pages;

public class MapVM : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    private bool _isBtFloor1Active;
    private bool _isBtFloor2Active;
    private bool _isBtFloor3Active;
    private bool _isBtFloor4Active;

    private RelayCommand? _goToFloor1;
    private RelayCommand? _goToFloor2;
    private RelayCommand? _goToFloor3;
    private RelayCommand? _goToFloor4;

    private FloorBlock1VM _floor1 = new();
    private FloorBlock2VM _floor2 = new ();
    private FloorBlock3VM _floor3 = new ();
    private FloorBlock4VM _floor4 = new();

    private BitmapSource _BtFloor1Active = null!;
    private BitmapSource _BtFloor2Active = null!;
    private BitmapSource _BtFloor3Active = null!;
    private BitmapSource _BtFloor4Active = null!;

    private BitmapSource _BtFloor1Inactive = null!;
    private BitmapSource _BtFloor2Inactive = null!;
    private BitmapSource _BtFloor3Inactive = null!;
    private BitmapSource _BtFloor4Inactive = null!;


    public MapVM()
    {
        ThemeManager.PropertyChanged += ThemeManagerPropertyChanged;
        ChangingTheme();
        _isBtFloor1Active = true;
    }


    public BitmapSource BtFloor1 => _isBtFloor1Active ? _BtFloor1Active : _BtFloor1Inactive;
    public BitmapSource BtFloor2 => _isBtFloor2Active ? _BtFloor2Active : _BtFloor2Inactive;
    public BitmapSource BtFloor3 => _isBtFloor3Active ? _BtFloor3Active : _BtFloor3Inactive;
    public BitmapSource BtFloor4 => _isBtFloor4Active ? _BtFloor4Active : _BtFloor4Inactive;

    public FloorBlock1VM? Floor1 => _isBtFloor1Active ? _floor1 : null;
    public FloorBlock2VM? Floor2 => _isBtFloor2Active ? _floor2 : null;
    public FloorBlock3VM? Floor3 => _isBtFloor3Active ? _floor3 : null;
    public FloorBlock4VM? Floor4 => _isBtFloor4Active ? _floor4 : null;

    public bool IsBtFloor1Active
    {
        get => _isBtFloor1Active;
        set
        {
            _isBtFloor1Active = value;
            OnPropertyChanged(nameof(Floor1));
            OnPropertyChanged(nameof(BtFloor1));
        }
    }
    public bool IsBtFloor2Active
    {
        get => _isBtFloor2Active;
        set
        {
            _isBtFloor2Active = value;
            OnPropertyChanged(nameof(Floor2));
            OnPropertyChanged(nameof(BtFloor2));
        }
    }
    public bool IsBtFloor3Active
    {
        get => _isBtFloor3Active;
        set
        {
            _isBtFloor3Active = value;
            OnPropertyChanged(nameof(Floor3));
            OnPropertyChanged(nameof(BtFloor3));
        }
    }
    public bool IsBtFloor4Active
    {
        get => _isBtFloor3Active;
        set
        {
            _isBtFloor4Active = value;
            OnPropertyChanged(nameof(Floor4));
            OnPropertyChanged(nameof(BtFloor4));
        }
    }

    public RelayCommand GoToFloor1
    {
        get => _goToFloor1 ?? (_goToFloor1 = new(obj => { BtClickFloor1(); }));
    }
    public RelayCommand GoToFloor2
    {
        get => _goToFloor2 ?? (_goToFloor2 = new(obj => { BtClickFloor2(); }));
    }
    public RelayCommand GoToFloor3
    {
        get => _goToFloor3 ?? (_goToFloor3 = new(obj => { BtClickFloor3(); }));
    }
    public RelayCommand GoToFloor4
    {
        get => _goToFloor4 ?? (_goToFloor4 = new(obj => { BtClickFloor4(); }));
    }


    public void BtClickFloor1()
    {
        BtInactive();
        IsBtFloor1Active = true;

    }
    public void BtClickFloor2()
    {
        BtInactive();
        IsBtFloor2Active = true;

    }
    public void BtClickFloor3()
    {
        BtInactive();
        IsBtFloor3Active = true;
    }
    public void BtClickFloor4()
    {
        BtInactive();
        IsBtFloor4Active = true;
    }

    protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    private void BtInactive()
    {
        IsBtFloor1Active = false;
        IsBtFloor2Active = false;
        IsBtFloor3Active = false;
        IsBtFloor4Active = false;
    }
    private void ChangingTheme()
    {
        _BtFloor1Active = new SvgRenderer(SKColor.Parse(ThemeManager.ThemeButton), "Assets/Icons/Active/Floor1.svg").SvgToBitMap();
        _BtFloor2Active = new SvgRenderer(SKColor.Parse(ThemeManager.ThemeButton), "Assets/Icons/Active/Floor2.svg").SvgToBitMap();
        _BtFloor3Active = new SvgRenderer(SKColor.Parse(ThemeManager.ThemeButton), "Assets/Icons/Active/Floor3.svg").SvgToBitMap();
        _BtFloor4Active = new SvgRenderer(SKColor.Parse(ThemeManager.ThemeButton), "Assets/Icons/Active/Floor4.svg").SvgToBitMap();

        _BtFloor1Inactive = new SvgRenderer(SKColor.Parse(ThemeManager.ThemeButton), "Assets/Icons/Inactive/Floor1.svg").SvgToBitMap();
        _BtFloor2Inactive = new SvgRenderer(SKColor.Parse(ThemeManager.ThemeButton), "Assets/Icons/Inactive/Floor2.svg").SvgToBitMap();
        _BtFloor3Inactive = new SvgRenderer(SKColor.Parse(ThemeManager.ThemeButton), "Assets/Icons/Inactive/Floor3.svg").SvgToBitMap();
        _BtFloor4Inactive = new SvgRenderer(SKColor.Parse(ThemeManager.ThemeButton), "Assets/Icons/Inactive/Floor4.svg").SvgToBitMap();




        OnPropertyChanged(nameof(BtFloor1));
        OnPropertyChanged(nameof(BtFloor2));
        OnPropertyChanged(nameof(BtFloor3));
        OnPropertyChanged(nameof(BtFloor4));
    }
    private void ThemeManagerPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(ThemeManager.ThemeId)) ChangingTheme();
    }
}