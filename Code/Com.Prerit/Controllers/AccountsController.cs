using System.Web.Mvc;

using Com.Prerit.Models.Accounts;

namespace Com.Prerit.Controllers
{
    public partial class AccountsController : Controller
    {
        #region Methods

        public virtual ActionResult LoginStatus()
        {
            if (User.Identity.IsAuthenticated)
            {
                var model = new LoggedInModel();

                return View(MVC.Accounts.Views.LoggedIn, model);
            }
            else
            {
                var model = new NotLoggedInModel();

                return View(MVC.Accounts.Views.NotLoggedIn, model);
            }
        }

        #endregion
    }
}