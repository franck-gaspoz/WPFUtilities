using System.IO;
using System.Windows.Media.Imaging;

namespace WPFUtilities.Extensions.Image
{
    /// <summary>
    /// bitmap image extensions
    /// </summary>
    public static class BitmapImageExtensions
    {
        /// <summary>
        /// get bitmap source bytes
        /// </summary>
        /// <param name="bitmapSource">bitmap source</param>
        /// <returns>bytes array</returns>
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
