using System;
using System.Drawing;
using System.IO;

namespace small_it
{
    internal class SimpleSmall : IEncoder
    {
        private readonly Bitmap _image;
        private readonly int _quality;

        /// <summary>
        /// Compresses equal to the quality of the image
        /// </summary>
        /// <param name="filePath">The location of the image file</param>
        /// <param name="compressionQuality">The quality of the image</param>
        public SimpleSmall(string filePath, int compressionQuality)
        {
            _image = Extensions.LoadBitmap(filePath);
            _quality = compressionQuality;
            Destination = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"simple\\{Path.GetFileName(filePath)}");
        }

        public string Destination { get; }

        public void Compress()
        {
            _image.SaveJpg(Destination, _quality);
        }

        public byte[] AsByteArray()
        {
            ImageConverter converter = new ImageConverter();
            return (byte[])converter.ConvertTo(_image, typeof(byte[]));
        }
    }
}
