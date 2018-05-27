using System;
using System.IO;

namespace small_it
{
    class Program
    {
        //CHALLENGE WITH COMPRESSION
        //https://hackaday.io/project/80627-badge-for-hackaday-conference-2018-in-belgrade/log/146227-image-compression-challenge
        static void Main(string[] args)
        {
            //step #1: load original image that is too big for PIC32MX370F512H memory
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "original\\Voja.jpg");
            IEncoder small = new MagickSmall(filePath);

            var outputFolder = Path.GetDirectoryName(small.Destination);
            if (string.IsNullOrWhiteSpace(outputFolder))
                throw new InvalidOperationException();
            if (!Directory.Exists(outputFolder))
                Directory.CreateDirectory(outputFolder);

            //step #2: Compress to lowest possible for 320x240 display
            small.Compress();

            //step #3: Prepare the bytes for read by PIC32MX370F512H
            var array = small.AsByteArray();
            var outputFile = $"{Path.GetFileNameWithoutExtension(small.Destination)}.txt";
            Extensions.WriteToFile(array, Path.Combine(outputFolder, outputFile));
        }
    }
}
