using System;
using System.IO;
using System.Web.UI;

using Prerit.Com.Web;

public partial class controls_random_photo : UserControl
{
    #region Methods

    protected void Page_Load(object sender, EventArgs args)
    {
        string file;
        string folder;

        string[] files;

        Random random = new Random();

        switch (random.Next() % 4)
        {
            default:
            case 0:
                folder = "~/photos/pithi/";
                break;
            case 1:
                folder = "~/photos/wedding/";
                break;
            case 2:
                folder = "~/photos/honeymoon/";
                break;
            case 3:
                folder = "~/photos/christmas/";
                break;
        }

        files = Directory.GetFiles(MapPath(folder));

        do
        {
            file = Path.GetFileName(files[random.Next() % files.Length]);
        }
        while (file.EndsWith("_thumb.jpg"));

        photoLink.HRef = ResolveUrl(folder + file);

        photoLink.Attributes[HtmlMarkup.OnClick] = string.Format(photoLink.Attributes[HtmlMarkup.OnClick], ResolveUrl(folder + file));

        photoImage.Src = ResolveUrl(folder + Path.GetFileNameWithoutExtension(file) + "_thumb.jpg");
    }

    #endregion
}