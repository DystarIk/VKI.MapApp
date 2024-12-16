using System.IO;
using System.Windows.Media.Imaging;
using SkiaSharp;
using Svg.Skia;

namespace VKI.MapApp.Models;

public class SvgRenderer
{
    private float _scaleFactor;
    private string _pathSvg;
    private SKColor _color;


    public SvgRenderer(float scaleFactor, SKColor color, string pathSvg)
    {
        _scaleFactor = scaleFactor;
        _color = color;
        _pathSvg = pathSvg;
    }
    public SvgRenderer(SKColor color, string pathSvg) : this(1, color, pathSvg) { }


    public BitmapSource SvgToBitMap()
    {
        // Загружаем SVG
        SKSvg svg = new SKSvg();
        using (var stream = new FileStream(_pathSvg, FileMode.Open, FileAccess.Read))
        {
            svg.Load(stream);
        }

        // Получаем размеры SVG
        int width = (int)(svg.Picture?.CullRect.Width ?? 25 * _scaleFactor);
        int height = (int)(svg.Picture?.CullRect.Height ?? 25 * _scaleFactor);

        // Создаем холст для рендеринга SVG
        using (var bitmap = new SKBitmap(width, height))
        using (var canvas = new SKCanvas(bitmap))
        {
            // Рендерим SVG на холст
            canvas.Clear(SKColors.Transparent);
            using (var paint = new SKPaint())
            {
                paint.ColorFilter = SKColorFilter.CreateBlendMode(_color, SKBlendMode.SrcIn);
                canvas.DrawPicture(svg.Picture, paint);
            }
            canvas.Flush();

            // Преобразуем SKBitmap в BitmapSource
            using (var image = SKImage.FromBitmap(bitmap))
            using (var data = image.Encode(SKEncodedImageFormat.Png, 100))
            using (var stream = data.AsStream())
            {
                var bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.StreamSource = stream;
                bitmapImage.EndInit();
                bitmapImage.Freeze(); // Замораживаем изображение для безопасного использования в других потоках

                return bitmapImage;
            }
        }
    }
}