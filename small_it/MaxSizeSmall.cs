using System;
using System.Drawing;
using System.IO;

namespace small_it
{
    internal class MaxSizeSmall : IEncoder
    {
        private readonly Bitmap _image;
        private readonly string _filePath;
        private readonly long _maxFileSize;

        /// <summary>
        /// Compresses closest to to the maximum file size
        /// </summary>
        /// <param name="filePath">The location of the image file</param>
        /// <param name="maxFileSize">The maximum file size</param>
        public MaxSizeSmall(string filePath, long maxFileSize)
        {
            _image = Extensions.LoadBitmap(filePath);
            _filePath = filePath;
            _maxFileSize = maxFileSize;
            Destination = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"maxsize\\{Path.GetFileName(filePath)}");
        }

        public string Destination { get; }

        public void Compress()
        {
            const int delta = 5;

            int lowestLevel = delta;
            int maxLevel = 100 / delta * delta + delta;

            for (;;)
            {
                int midLevel = (lowestLevel + maxLevel) / 2;
                midLevel = midLevel / delta * delta;
                if (midLevel == lowestLevel)
                    break;

                _image.SaveJpg(Destination, midLevel);

                if (GetFileSize(_filePath) > _maxFileSize)
                    maxLevel = midLevel;
                else
                    lowestLevel = midLevel;
            }

            _image.SaveJpg(Destination, lowestLevel);
        }

        public byte[] AsByteArray()
        {
            ImageConverter converter = new ImageConverter();
            return (byte[])converter.ConvertTo(_image, typeof(byte[]));
        }

        private long GetFileSize(string fileName)
        {
            return new FileInfo(fileName).Length;
        }
    }
}
