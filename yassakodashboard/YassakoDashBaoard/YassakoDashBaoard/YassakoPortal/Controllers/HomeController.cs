using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace YassakoPortal.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(FormCollection form)
        {
            try
            {
                YassakoPortalLogic.Models.SystemUser systemUser = new YassakoPortalLogic.Models.SystemUser();
                YassakoPortalLogic.Models.UserSearchResult userSearchResult = new YassakoPortalLogic.Models.UserSearchResult();
                systemUser.Username = Request["username"];
                systemUser.UserPassword = Request["password"];
                YassakoPortalLogic.Logic.UserProcessor userProcessor = new YassakoPortalLogic.Logic.UserProcessor(systemUser);
                userSearchResult = userProcessor.LoginDetails();
                if (userSearchResult.IsSuccessfull)
                {
                    Session["Uid"] = userSearchResult.results[0].Username;
                    Session["Uname"] = userSearchResult.results[0].Username;
                    Session["FullName"] = userSearchResult.results[0].FullName;
                    Session["UserRole"] = userSearchResult.results[0].Userrole;
                    Session["UserEmail"] = userSearchResult.results[0].UserEmail;
                    Session["UserDepartment"] = userSearchResult.results[0].UserCompany;
                    Session["Section"] = userSearchResult.results[0].UserCompany;
                    return RedirectToAction("Index", "Admin");
                }
                else
                {
                    ViewBag.Error = userSearchResult.Message;
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }
            return View();
        }

        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }
    }
}