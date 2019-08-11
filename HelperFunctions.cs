using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;
using System.Linq;

namespace AutoShot {
    class HelperFunctions {
        public static string GetDateString(string format) {
            DateTime dateTime = DateTime.Now;
            return (dateTime.ToString(format));
        }

        public static void log(string text, bool print = true) {
            string outString = String.Format("[{0}] {1}", GetDateString("HH:mm:ss"), text);

            if (print) Console.WriteLine(outString);
            using (var sw = new StreamWriter("log.txt", true)) {
                sw.WriteLine(outString);
            }
        }

        public static void TakeScreenshot(string path, long screenshotquality) {
            Rectangle screenBounds = SystemInformation.VirtualScreen;
            Bitmap bitmap = new Bitmap(screenBounds.Width,
                screenBounds.Height);

            Graphics image = Graphics.FromImage(bitmap);
            image.CopyFromScreen(screenBounds.X, screenBounds.Y, 
                0, 0, 
                bitmap.Size);

            EncoderParameters encoder = new EncoderParameters();
            encoder.Param[0] = new EncoderParameter(Encoder.Quality, screenshotquality);

            ImageCodecInfo jpegCodec = GetImageFormat(ImageFormat.Jpeg);
            if(jpegCodec == null) {
                log("jpeg codec not found");
                return;
            }
            bitmap.Save(path, jpegCodec, encoder);
        }

        public static ImageCodecInfo GetImageFormat(ImageFormat imageFormat) {
            ImageCodecInfo[] encoders = ImageCodecInfo.GetImageEncoders();
            foreach (ImageCodecInfo encoder in encoders) {
                if (encoder.FormatID == imageFormat.Guid) {
                    return encoder;
                }
            }
            return null;
        }
    }
}
