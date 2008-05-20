using System.Drawing;

namespace Com.Prerit.Web.Services
{
    public interface IImageEditorService
    {
        #region Methods

        void DisallowUsageOfEmbeddedThumbnail(Image image);

        void GetScaledHeightAndWidth(int maxDimension, Image image, out int height, out int width);

        bool IsPortrait(Image image);

        Image ScaleAndSaveImage(int maxDimension, string scaledImagePhysicalPath, string originalImagePhysicalPath);

        Image ScaleAndSaveImage(int maxDimension, string scaledImagePhysicalPath, Image originalImage);

        #endregion
    }
}