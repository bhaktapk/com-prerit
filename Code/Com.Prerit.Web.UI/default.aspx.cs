using System;
using System.Web.UI;

public partial class _default : Page
{
    #region Methods

    protected void Page_Load(object sender, EventArgs args)
    {
        Response.Redirect("~/about/");
    }

    #endregion
}