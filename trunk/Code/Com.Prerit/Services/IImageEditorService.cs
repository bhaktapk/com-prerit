namespace Com.Prerit.Services
{
    public interface IImageEditorService
    {
        #region Methods

        void ResizeImage(int maxDimension, string sourceFilePath, string destinationFilePath);

        #endregion
    }
}