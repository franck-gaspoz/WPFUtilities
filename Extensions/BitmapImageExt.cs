using System.IO;
using System.Windows.Media.Imaging;

namespace WPFUtilities.Extensions
{
    public static class BitmapImageExt
    {
        public static byte[] ToByteArray(this BitmapSource bitmapSource)
        {
            byte[] data;
            var encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(bitmapSource));
            using (MemoryStream ms = new MemoryStream())
            {
                encoder.Save(ms);
                data = ms.ToArray();
            }
            return data;
        }
    }
}
