using System.Drawing;
/// <summary>
/// Alex Plagman
/// </summary>
namespace PacSharpApp.Utils
{
    static class BitmapExtensions
    {
        public static Bitmap AfterRotateFlip(this Bitmap self, RotateFlipType rfType)
        {
            Bitmap result = self.Clone() as Bitmap;
            result.RotateFlip(rfType);
            return result;
        }
    }
}
