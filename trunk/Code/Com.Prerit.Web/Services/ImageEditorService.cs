using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace Com.Prerit.Web.Services
{
    public class ImageEditorService : IImageEditorService
    {
        #region Methods

        public Image CreateScaledImage(int width, int height, Image originalImage)
        {
            Image result = new Bitmap(width, height);

            using (Graphics graphics = Graphics.FromImage(result))
            {
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.FillRectangle(Brushes.White, 0, 0, width, height);
                graphics.DrawImage(originalImage, 0, 0, width, height);
            }

            return result;
        }

        public void DisallowUsageOfEmbeddedThumbnail(Image image)
        {
            if (image == null)
            {
                throw new ArgumentNullException("image");
            }

            image.RotateFlip(RotateFlipType.Rotate180FlipNone);
            image.RotateFlip(RotateFlipType.Rotate180FlipNone);
        }

        public EncoderParameters GetEncoderParams()
        {
            long[] quality = new long[] { 75 };
            EncoderParameters encoderParams = new EncoderParameters();

            encoderParams.Param[0] = new EncoderParameter(Encoder.Quality, quality);

            return encoderParams;
        }

        public ImageCodecInfo GetJpegCodecInfo()
        {
            ImageCodecInfo result;

            result = Array.Find(ImageCodecInfo.GetImageEncoders(), codecInfo => string.Compare(codecInfo.FormatDescription, "JPEG", true) == 0);

            return result;
        }

        public void GetScaledHeightAndWidth(int maxDimension, Image image, out int height, out int width)
        {
            if (maxDimension < 0)
            {
                throw new ArgumentOutOfRangeException("maxDimension", maxDimension, "Cannot be less than zero");
            }

            if (image == null)
            {
                throw new ArgumentNullException("image");
            }

            if (IsPortrait(image))
            {
                float scaleFactor = (float) maxDimension / (float) image.Height;

                height = maxDimension;
                width = Convert.ToInt32(scaleFactor * (float) image.Width);
            }
            else
            {
                float scaleFactor = (float) maxDimension / (float) image.Width;

                height = Convert.ToInt32(scaleFactor * (float) image.Height);
                width = maxDimension;
            }
        }

        public bool IsPortrait(Image image)
        {
            if (image == null)
            {
                throw new ArgumentNullException("image");
            }

            return image.Height > image.Width;
        }

        public Image ScaleAndSaveImage(int maxDimension, string scaledImagePhysicalPath, string originalImagePhysicalPath)
        {
            Image result;

            using (Image originalImage = Image.FromFile(originalImagePhysicalPath))
            {
                result = ScaleAndSaveImage(maxDimension, scaledImagePhysicalPath, originalImage);
            }

            return result;
        }

        public Image ScaleAndSaveImage(int maxDimension, string scaledImagePhysicalPath, Image originalImage)
        {
            if (maxDimension < 0)
            {
                throw new ArgumentOutOfRangeException("maxDimension", maxDimension, "Cannot be less than zero");
            }

            if (scaledImagePhysicalPath == null)
            {
                throw new ArgumentNullException("scaledImagePhysicalPath");
            }

            if (originalImage == null)
            {
                throw new ArgumentNullException("originalImage");
            }

            Image result;

            int height;
            int width;

            DisallowUsageOfEmbeddedThumbnail(originalImage);

            GetScaledHeightAndWidth(maxDimension, originalImage, out height, out width);

            result = CreateScaledImage(width, height, originalImage);

            result.Save(scaledImagePhysicalPath, GetJpegCodecInfo(), GetEncoderParams());

            return result;
        }

        #endregion
    }
}