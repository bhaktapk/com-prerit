using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;

namespace Com.Prerit.Services
{
    public class ImageEditorService : IImageEditorService
    {
        #region Methods

        private Image CreateScaledImage(Size size, Image image)
        {
            Image scaledImage = new Bitmap(size.Width, size.Height);

            using (Graphics graphics = Graphics.FromImage(scaledImage))
            {
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.FillRectangle(Brushes.White, 0, 0, size.Width, size.Height);
                graphics.DrawImage(image, 0, 0, size.Width, size.Height);
            }

            return scaledImage;
        }

        private void DisallowUsageOfEmbeddedThumbnail(Image image)
        {
            image.RotateFlip(RotateFlipType.Rotate180FlipNone);
            image.RotateFlip(RotateFlipType.Rotate180FlipNone);
        }

        private EncoderParameters GetEncoderParams()
        {
            var quality = new[]
                              {
                                  75L
                              };

            var encoderParams = new EncoderParameters();

            encoderParams.Param[0] = new EncoderParameter(Encoder.Quality, quality);

            return encoderParams;
        }

        private ImageCodecInfo GetJpegCodecInfo()
        {
            return (from codecInfo in ImageCodecInfo.GetImageEncoders()
                    where string.Compare(codecInfo.FormatDescription, "JPEG", StringComparison.OrdinalIgnoreCase) == 0
                    select codecInfo).First();
        }

        public void ResizeImage(int maxDimension, string sourceFilePath, string destinationFilePath)
        {
            if (maxDimension < 0)
            {
                throw new ArgumentOutOfRangeException("maxDimension", maxDimension, "Cannot be less than zero");
            }

            if (sourceFilePath == null)
            {
                throw new ArgumentNullException("sourceFilePath");
            }

            if (destinationFilePath == null)
            {
                throw new ArgumentNullException("destinationFilePath");
            }

            using (Image image = Image.FromFile(sourceFilePath))
            {
                Size scaledSize = ScaleToMaxDimension(maxDimension, image);

                DisallowUsageOfEmbeddedThumbnail(image);

                Image resizedImage = CreateScaledImage(scaledSize, image);

                resizedImage.Save(destinationFilePath, GetJpegCodecInfo(), GetEncoderParams());
            }
        }

        private Size ScaleToMaxDimension(int maxDimension, Image image)
        {
            bool isLandscapeImage = image.Height < image.Width;

            if (isLandscapeImage)
            {
                float scaleFactor = (float) maxDimension / image.Height;

                return new Size(Convert.ToInt32(scaleFactor * image.Width), maxDimension);
            }
            else
            {
                float scaleFactor = (float) maxDimension / image.Width;

                return new Size(maxDimension, Convert.ToInt32(scaleFactor * image.Height));
            }
        }

        #endregion
    }
}