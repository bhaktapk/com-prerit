using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace Com.Prerit.Web.Services
{
    public class ImageEditorService : IImageEditorService
    {
        #region Methods

        public void DisallowUsageOfEmbeddedThumbnail(Image image)
        {
            if (image == null)
            {
                throw new ArgumentNullException("image");
            }

            image.RotateFlip(RotateFlipType.Rotate180FlipNone);
            image.RotateFlip(RotateFlipType.Rotate180FlipNone);
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

            result = originalImage.GetThumbnailImage(width, height, () => false, IntPtr.Zero);

            result.Save(scaledImagePhysicalPath, ImageFormat.Jpeg);

            return result;
        }

        #endregion
    }
}