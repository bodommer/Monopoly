using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.Main
{
    /**
     * A deprecated static class used for adjust the wanted comapny logos to match the dimensions
     * needed in the gameWindow.
     */
    public static class ImageManipulator
    {
        public static void CreateThumbnails()
        {
            int[] order = { 1, 3, 6, 8, 9, 11, 13, 14, 16, 18, 19, 21, 23,
                24, 26, 27, 29, 31, 32, 34, 37, 39, 12, 28, 5, 15, 25, 35 };
            foreach (int i in order) {
                var image = System.Drawing.Image.FromFile("Resources/logos/" + i + ".png");
                var ratioX = (double)280 / image.Width;
                var ratioY = (double)130 / image.Height;
                var ratio = Math.Min(ratioX, ratioY);
                var newWidth = (int)(image.Width * ratio);
                var newHeight = (int)(image.Height * ratio);
                var newImage = new Bitmap(newWidth, newHeight);
                Graphics thumbGraph = Graphics.FromImage(newImage);

                thumbGraph.CompositingQuality = CompositingQuality.HighQuality;
                thumbGraph.SmoothingMode = SmoothingMode.HighQuality;

                thumbGraph.DrawImage(image, 0, 0, newWidth, newHeight);
                image.Dispose();

                string fileRelativePath = "Resources/resized/" + i;
                newImage.Save(fileRelativePath);
            }
        }
    }
}
