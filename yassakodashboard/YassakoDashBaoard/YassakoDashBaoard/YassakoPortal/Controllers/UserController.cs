using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace YassakoPortal.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            if (Session["FullName"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                try
                {
                    YassakoPortalLogic.Models.UserDepartments userDepartments = new YassakoPortalLogic.Models.UserDepartments();
                    YassakoPortalLogic.Logic.UserProcessor userProcessor = new YassakoPortalLogic.Logic.UserProcessor();
                    userDepartments = userProcessor.GetUserDepartments("1");
                    if (userDepartments.IsSuccessfull)
                    {
                        ViewBag.UserDepartments = userDepartments.departments;
                    }
                    else
                    {
                        ViewBag.Error = userDepartments.Message;
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.Error = ex.Message;
                }
                return View();
            }
        }

        public ActionResult ChangeUserPwd() 
        {
            try
            {
                ViewBag.Username = Session["Uname"].ToString();
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }
            return View();
        }
        [HttpPost]
        public ActionResult ChangeUserPwd(FormCollection form)
        {
            try
            {
                ViewBag.Username = Session["Uname"].ToString();
                YassakoPortalLogic.Models.SystemUser systemUser = new YassakoPortalLogic.Models.SystemUser();
                YassakoPortalLogic.Models.GenericResponse response = new YassakoPortalLogic.Models.GenericResponse();
                systemUser.Username = Session["Uname"].ToString();
                systemUser.UserPassword = Request["oldpwd"];
                string newpwd = Request["newpwd"];
                string pwdrept = Request["repeatpwd"];
                YassakoPortalLogic.Logic.UserProcessor userProcessor = new YassakoPortalLogic.Logic.UserProcessor(systemUser);
                response = userProcessor.ChangeUserPassword(newpwd, pwdrept);
                if (response.IsSuccessfull)
                {
                    ViewBag.Message = response.Message;
                }
                else
                {
                    ViewBag.Error = response.Message;
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }
            return View();
        }

        public ActionResult ResetUserPwd(string useremail, string implementer)
        {
            try
            {
                if (!string.IsNullOrEmpty(useremail) || !string.IsNullOrEmpty(implementer))
                {
                    YassakoPortalLogic.Models.SystemUser systemUser = new YassakoPortalLogic.Models.SystemUser();
                    systemUser.Active = true;
                    systemUser.UserEmail = useremail;
                    systemUser.Username = implementer;
                    YassakoPortalLogic.Logic.UserProcessor userProcessor = new YassakoPortalLogic.Logic.UserProcessor(systemUser);
                    userProcessor.ResetUser();
                }
                return RedirectToAction("ViewUsers", "User");
            }
            catch (Exception ex)
            {
                return RedirectToAction("ViewUsers", "User");
            }
        }

        public ActionResult ActivateDeactivateCustomer(string useremail, string implementer)
        {
            try
            {
                if (!string.IsNullOrEmpty(useremail)|| !string.IsNullOrEmpty(implementer))
                {
                    YassakoPortalLogic.Models.SystemUser systemUser = new YassakoPortalLogic.Models.SystemUser();
                    systemUser.UserEmail = useremail;
                    systemUser.Username = implementer;
                    YassakoPortalLogic.Logic.UserProcessor userProcessor = new YassakoPortalLogic.Logic.UserProcessor(systemUser);
                    userProcessor.DeactivateActivateUser();
                }
                return RedirectToAction("ViewUsers", "User");
            }
            catch (Exception ex)
            {
                return RedirectToAction("ViewUsers", "User");
            }
        }

        public ActionResult ViewUsers()
        {
            try
            {
                YassakoPortalLogic.Models.SystemUser user = new YassakoPortalLogic.Models.SystemUser();
                user.Username = Session["Uname"].ToString();
                YassakoPortalLogic.Logic.UserProcessor userProcessor = new YassakoPortalLogic.Logic.UserProcessor(user);
                YassakoPortalLogic.Models.UserSearchResult searchResult = new YassakoPortalLogic.Models.UserSearchResult();
                searchResult = userProcessor.GetSystemUsers();
                if (searchResult.IsSuccessfull)
                {
                    ViewBag.SystemUsers = searchResult.results;
                }
                else
                {
                    ViewBag.Message = searchResult.Message;
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }
            return View();
        }

        [HttpPost]
        public ActionResult Index(FormCollection form)
        {
            try
            {
                YassakoPortalLogic.Models.SystemUser user = new YassakoPortalLogic.Models.SystemUser();
                string firstname = Request["firstname"].ToString();
                string lastname = Request["lastname"].ToString();
                user.FullName = firstname + " " + lastname;
                user.PhoneNumber = Request["phone"].ToString();
                user.RecodedBy = Session["Uname"].ToString();
                //user.UserCompany = Request["usercompany"].ToString();
                user.UserEmail = Request["username"].ToString();
                user.Username = Request["username"].ToString();
                user.Userrole = Request["userrole"].ToString();
                YassakoPortalLogic.Logic.UserProcessor userProcessor = new YassakoPortalLogic.Logic.UserProcessor(user);
                YassakoPortalLogic.Models.GenericResponse response = userProcessor.RegisterUser();
                if (response.IsSuccessfull)
                {
                    ViewBag.Message = response.Message;
                }
                else
                {
                    ViewBag.Error = response.Message;
                }
                
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }
            return View();
        }
    }
}