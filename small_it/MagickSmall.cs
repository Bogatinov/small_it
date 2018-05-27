using System;
using System.IO;
using ImageMagick;

namespace small_it
{
    internal class MagickSmall : IEncoder
    {
        private readonly string _filePath;

        public MagickSmall(string filePath)
        {
            _filePath = filePath;
            Destination = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"magick\\{Path.GetFileName(_filePath)}");
        }

        public string Destination { get; }

        public void Compress()
        {
            using (MagickImage image = new MagickImage(_filePath))
            {
                image.Quality = 3;
                image.Contrast(true);
                image.Grayscale(PixelIntensityMethod.Lightness);
                image.AdaptiveThreshold(320, 240);
                image.Write(Destination);
            }
        }

        public byte[] AsByteArray()
        {
            using (MagickImage image = new MagickImage(Destination))
                return image.ToByteArray();
        }
    }
}
