using System;
using System.Web.UI;

public partial class controls_login_view : UserControl
{
    #region Properties

    public string UpdatePanelClientID
    {
        get { return updatePanel.ClientID; }
    }

    #endregion

    #region Methods

    protected void Page_Load(object sender, EventArgs args)
    {
    }

    public void Update()
    {
        updatePanel.Update();
    }

    #endregion
}