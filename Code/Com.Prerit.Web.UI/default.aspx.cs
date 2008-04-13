using System;
using System.Web.UI;

public partial class _default : Page
{
	protected void Page_Load(object sender, EventArgs args)
	{
		Response.Redirect("~/about/");
	}
}