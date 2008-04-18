using System;
using System.Web.UI;

public partial class controls_photo_search : UserControl
{
    #region Methods

    protected void Page_Load(object sender, EventArgs args)
    {
        if (string.IsNullOrEmpty(Page.Form.DefaultButton))
        {
            Page.Form.DefaultButton = photoSearchButton.UniqueID;
        }

        if (string.IsNullOrEmpty(Page.Form.DefaultFocus))
        {
            Page.Form.DefaultFocus = photoSearchInputText.ClientID;
        }
    }

    protected void PhotoSearchButton_Click(object sender, EventArgs args)
    {
    }

    #endregion
}