Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

function pageLoaded(sender, args)
{
    if (isViewingStep3())
    {
        Sys.WebForms.PageRequestManager.getInstance().remove_pageLoaded(pageLoaded);

        __doPostBack(loginViewUpdatePanelClientID, '');
    }
}

function isViewingStep3()
{
    if ($get(step3HeaderID))
    {
        return true;
    }

    return false;
}
