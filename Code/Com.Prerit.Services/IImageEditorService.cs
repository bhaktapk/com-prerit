using System.Drawing;
using System.Drawing.Imaging;

namespace Com.Prerit.Services
{
    public interface IImageEditorService
    {
        #region Methods

        Image CreateScaledImage(int width, int height, Image originalImage);

        void DisallowUsageOfEmbeddedThumbnail(Image image);

        EncoderParameters GetEncoderParams();

        void GetImageMetadata(string imagePhysicalPath, out int height, out int width);

        ImageCodecInfo GetJpegCodecInfo();

        void GetScaledHeightAndWidth(int maxDimension, Image image, out int height, out int width);

        bool IsPortrait(Image image);

        void SaveImageMetadata(string imagePhysicalPath, Image image);

        Image ScaleAndSaveImage(int maxDimension, string scaledImagePhysicalPath, Image originalImage);

        #endregion
    }
}