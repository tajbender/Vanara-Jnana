using Microsoft.UI.Xaml.Media.Imaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jnana.Helpers;

public class ImageExtensions
{
    public static BitmapImage? ToBitmapImage(byte[]? bytes)
    {
        if (bytes == null || bytes.Length == 0)
            return null;

        var bitmap = new BitmapImage();

        using var ms = new MemoryStream(bytes);
        var ras = ms.AsRandomAccessStream();

        bitmap.SetSource(ras);
        return bitmap;
    }
}
