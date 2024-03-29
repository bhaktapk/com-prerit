using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web.Hosting;
using System.Xml.Serialization;

namespace Com.Prerit.Services
{
    public class DiskInputOutputService : IDiskInputOutputService
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

        private void EnsureDirectoryExists(string filePath)
        {
            string directoryPath = Path.GetDirectoryName(filePath);

            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
        }

        public bool FileExists(string filePath)
        {
            return File.Exists(filePath);
        }

        public IEnumerable<string> GetDirectories(string path)
        {
            return Directory.GetDirectories(path);
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

        public IEnumerable<string> GetFiles(string path)
        {
            return Directory.GetFiles(path);
        }

        public IEnumerable<string> GetFiles(string path, string searchPattern)
        {
            return Directory.GetFiles(path, searchPattern);
        }

        public IEnumerable<string> GetFiles(string path, string searchPattern, SearchOption searchOption)
        {
            return Directory.GetFiles(path, searchPattern, searchOption);
        }

        private ImageCodecInfo GetJpegCodecInfo()
        {
            return (from codecInfo in ImageCodecInfo.GetImageEncoders()
                    where string.Compare(codecInfo.FormatDescription, "JPEG", StringComparison.OrdinalIgnoreCase) == 0
                    select codecInfo).First();
        }

        public T LoadXmlFile<T>(string filePath)
        {
            var serializer = new XmlSerializer(typeof(T));

            using (var reader = new StreamReader(filePath))
            {
                return (T) serializer.Deserialize(reader);
            }
        }

        public string MapPath(string virtualPath)
        {
            return HostingEnvironment.MapPath(virtualPath);
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

            EnsureDirectoryExists(destinationFilePath);

            using (Image image = Image.FromFile(sourceFilePath))
            {
                Size scaledSize = ScaleToMaxDimension(maxDimension, image);

                DisallowUsageOfEmbeddedThumbnail(image);

                Image resizedImage = CreateScaledImage(scaledSize, image);

                resizedImage.Save(destinationFilePath, GetJpegCodecInfo(), GetEncoderParams());
            }
        }

        public void SaveXmlFile<T>(string filePath, T obj)
        {
            var serializer = new XmlSerializer(typeof(T));

            EnsureDirectoryExists(filePath);

            using (var writer = new StreamWriter(filePath))
            {
                serializer.Serialize(writer, obj);
            }
        }

        private Size ScaleToMaxDimension(int maxDimension, Image image)
        {
            bool isLandscapeImage = image.Height < image.Width;

            if (isLandscapeImage)
            {
                float scaleFactor = (float) maxDimension / image.Width;

                return new Size(maxDimension, Convert.ToInt32(scaleFactor * image.Height));
            }
            else
            {
                float scaleFactor = (float) maxDimension / image.Height;

                return new Size(Convert.ToInt32(scaleFactor * image.Width), maxDimension);
            }
        }

        #endregion
    }
}