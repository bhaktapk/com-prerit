using System.Drawing;
using System.Drawing.Imaging;

namespace Com.Prerit.Web.Services
{
    public interface IImageEditorService
    {
        #region Methods

        Image CreateScaledImage(int width, int height, Image originalImage);

        void DisallowUsageOfEmbeddedThumbnail(Image image);

        EncoderParameters GetEncoderParams();

        ImageCodecInfo GetJpegCodecInfo();

        void GetScaledHeightAndWidth(int maxDimension, Image image, out int height, out int width);

        bool IsPortrait(Image image);

        Image ScaleAndSaveImage(int maxDimension, string scaledImagePhysicalPath, string originalImagePhysicalPath);

        Image ScaleAndSaveImage(int maxDimension, string scaledImagePhysicalPath, Image originalImage);

        #endregion
    }
}