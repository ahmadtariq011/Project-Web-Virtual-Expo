using System;
using System.IO;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace VirtualExpo.Bll.Helpers
{
    /// <summary>
    /// Summary description for ImageHelper
    /// </summary>
    public class ImageHelper
    {
        public ImageHelper()
        {
            //
            // TODO: Add constructor logic here
            //
        }


        public static string ImageToBase64(string filePath)
        {
            using (Image image = Image.FromFile(filePath))
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    // Convert Image to byte[]
                    image.Save(ms, image.RawFormat);
                    byte[] imageBytes = ms.ToArray();

                    // Convert byte[] to Base64 String
                    string base64String = Convert.ToBase64String(imageBytes);
                    return base64String;
                }
            }
        }


        public static Image Base64ToImage(string base64String)
        {
            // Convert Base64 String to byte[]
            byte[] imageBytes = Convert.FromBase64String(base64String);
            MemoryStream ms = new MemoryStream(imageBytes, 0,
              imageBytes.Length);

            // Convert byte[] to Image
            ms.Write(imageBytes, 0, imageBytes.Length);
            Image image = Image.FromStream(ms, true);
            return image;
        }


        public static bool IsAnImage(string FileName)
        {
            string fileExtension = Path.GetExtension(FileName).ToLower();

            if (!(fileExtension == ".jpg" || fileExtension == ".gif" || fileExtension == ".png"))
                return false;

            return true;
        }


        public static void CreateScaledThumbnail(string imagePath, string ThumbnailPath, int maxWidth, int maxHeight, bool aspectRatio)
        {
            // Validation successful
            // Load the image into Bitmap Object
            Bitmap uploadedImage = new Bitmap(imagePath);

            // Set the maximum width and height here.
            // You can make this versatile by getting these values from
            // QueryString or textboxes

            // Resize the image
            Bitmap resizedImage;
            if (aspectRatio)
                resizedImage = GetScaledPicture(uploadedImage, maxWidth, maxHeight);
            else
                resizedImage = GetResizedImage(uploadedImage, maxWidth, maxHeight);
            //Save the image        
            resizedImage.Save(ThumbnailPath, uploadedImage.RawFormat);
        }


        public static void CreateScaledThumbnail(Stream fileContent, string ThumbnailPath, int maxWidth, int maxHeight, bool aspectRatio)
        {
            // Validation successful
            // Load the image into Bitmap Object
            Bitmap uploadedImage = new Bitmap(fileContent);

            // Set the maximum width and height here.
            // You can make this versatile by getting these values from
            // QueryString or textboxes

            // Resize the image
            Bitmap resizedImage;
            if (aspectRatio)
                resizedImage = GetScaledPicture(uploadedImage, maxWidth, maxHeight);
            else
                resizedImage = GetResizedImage(uploadedImage, maxWidth, maxHeight);
            //Save the image        
            resizedImage.Save(ThumbnailPath, uploadedImage.RawFormat);
        }


        public static Bitmap GetScaledPicture(Bitmap source, int maxWidth, int maxHeight)
        {
            int width, height;
            float aspectRatio = (float)source.Width / (float)source.Height;

            if ((maxHeight > 0) && (maxWidth > 0))
            {
                if ((source.Width < maxWidth) && (source.Height < maxHeight))
                {
                    //Return unchanged image
                    return source;
                }
                else if (aspectRatio > 1)
                {
                    // Calculated width and height,
                    // and recalcuate if the height exceeds maxHeight
                    width = maxWidth;
                    height = (int)(width / aspectRatio);
                    if (height > maxHeight)
                    {
                        height = maxHeight;
                        width = (int)(height * aspectRatio);
                    }
                }
                else
                {
                    // Calculated width and height,
                    // and recalcuate if the width exceeds maxWidth
                    height = maxHeight;
                    width = (int)(height * aspectRatio);
                    if (width > maxWidth)
                    {
                        width = maxWidth;
                        height = (int)(width / aspectRatio);
                    }
                }
            }
            else if ((maxHeight == 0) && (source.Width > maxWidth))
            {
                // If MaxHeight is not provided (unlimited), and
                // the source width exceeds maxWidth,
                // then recalculate height
                width = maxWidth;
                height = (int)(width / aspectRatio);
            }
            else if ((maxWidth == 0) && (source.Height > maxHeight))
            {
                // If MaxWidth is not provided (unlimited), and the
                // source height exceeds maxHeight, then
                // recalculate width
                height = maxHeight;
                width = (int)(height * aspectRatio);
            }
            else
            {
                //Return unchanged image
                return source;
            }

            Bitmap newImage = GetResizedImage(source, width, height);
            return newImage;
        }
        protected static Bitmap GetResizedImage(Bitmap source, int width, int height)
        {
            //This function creates the thumbnail image.
            //The logic is to create a blank image and to
            // draw the source image onto it

            Bitmap thumb = new Bitmap(width, height);
            Graphics gr = Graphics.FromImage(thumb);

            gr.InterpolationMode = InterpolationMode.HighQualityBicubic;
            gr.SmoothingMode = SmoothingMode.HighQuality;
            gr.PixelOffsetMode = PixelOffsetMode.HighQuality;
            gr.CompositingQuality = CompositingQuality.HighQuality;

            gr.DrawImage(source, 0, 0, width, height);
            return thumb;
        }
    }
}