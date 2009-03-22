using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace Com.Prerit.Services
{
    public class ImageEditorService : IImageEditorService
    {
        #region Methods

        public Image CreateScaledImage(int width, int height, Image image)
        {
            if (width < 0)
            {
                throw new ArgumentOutOfRangeException("width", width, "Cannot be less than zero");
            }

            if (height < 0)
            {
                throw new ArgumentOutOfRangeException("height", height, "Cannot be less than zero");
            }

            if (image == null)
            {
                throw new ArgumentNullException("image");
            }

            Image result = new Bitmap(width, height);

            using (Graphics graphics = Graphics.FromImage(result))
            {
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.FillRectangle(Brushes.White, 0, 0, width, height);
                graphics.DrawImage(image, 0, 0, width, height);
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

        public void GetImageMetadata(string imagePhysicalPath, out int height, out int width)
        {
            if (imagePhysicalPath == null)
            {
                throw new ArgumentNullException("imagePhysicalPath");
            }

            string imageMetadataXmlPhysicalPath = GetAssociatedImageMetadataPhysicalPath(imagePhysicalPath);

            if (!File.Exists(imageMetadataXmlPhysicalPath))
            {
                using (Image image = Image.FromFile(imagePhysicalPath))
                {
                    SaveImageMetadata(imagePhysicalPath, image);
                }
            }

            XDocument doc = XDocument.Load(imageMetadataXmlPhysicalPath);

            IEnumerable<XElement> heightResult = from imageMetadata in doc.Descendants("imageMetadata")
                                                 select imageMetadata.Element("height");

            IEnumerable<XElement> widthResult = from imageMetadata in doc.Descendants("imageMetadata")
                                                select imageMetadata.Element("width");

            height = int.Parse(heightResult.First().Value);
            width = int.Parse(widthResult.First().Value);
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

        public void SaveImageMetadata(string imagePhysicalPath, Image image)
        {
            if (imagePhysicalPath == null)
            {
                throw new ArgumentNullException("imagePhysicalPath");
            }

            string imageMetadataXmlPhysicalPath = GetAssociatedImageMetadataPhysicalPath(imagePhysicalPath);

            XDocument doc = new XDocument(new XDeclaration("1.0", "utf-8", "yes"),
                                    new XElement("imageMetadata", new XElement("height", image.Height), new XElement("width", image.Width)));

            doc.Save(imageMetadataXmlPhysicalPath);
        }

        private string GetAssociatedImageMetadataPhysicalPath(string imagePhysicalPath)
        {
            return Path.ChangeExtension(imagePhysicalPath, ".xml");
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

            SaveImageMetadata(scaledImagePhysicalPath, result);

            return result;
        }

        #endregion
    }
}