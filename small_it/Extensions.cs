using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.IO.Compression;

namespace small_it
{
    public static class Extensions
    {
        public static ImageCodecInfo GetEncoderInfo(String mimeType)
        {
            int j;
            var encoders = ImageCodecInfo.GetImageEncoders();
            for (j = 0; j < encoders.Length; ++j)
            {
                if (encoders[j].MimeType == mimeType)
                    return encoders[j];
            }
            return null;
        }

        public static void WriteToFile(byte[] array, string outputPath)
        {
            if (array == null)
                return;

            MemoryStream output = new MemoryStream();
            using (DeflateStream stream = new DeflateStream(output, CompressionLevel.Optimal))
                stream.Write(array, 0, array.Length);
            
            File.WriteAllBytes(outputPath, output.ToArray());
        }

        public static Bitmap LoadBitmap(string fileName)
        {
            Bitmap result;
            using (Bitmap bm = new Bitmap(fileName))
            {
                result = new Bitmap(bm.Width, bm.Height);
                using (Graphics gr = Graphics.FromImage(result))
                {
                    Rectangle rect = new Rectangle(0, 0, bm.Width, bm.Height);
                    gr.DrawImage(bm, rect, rect, GraphicsUnit.Pixel);
                }
            }
            return result;
        }

        public static void SaveJpg(this Image image, string outputPath, int level)
        {
            EncoderParameters encoderParams = new EncoderParameters(1)
            {
                Param =
                {
                    [0] = new EncoderParameter(Encoder.Quality, level)
                }
            };
            
            image.Save(outputPath, Extensions.GetEncoderInfo("image/jpeg"), encoderParams);
        }
    }
}
