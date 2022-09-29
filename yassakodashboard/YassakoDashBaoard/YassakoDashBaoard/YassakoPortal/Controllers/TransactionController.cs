using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace YassakoPortal.Controllers
{
    public class TransactionController : Controller
    {
        // GET: Transaction
        public ActionResult Index()
        {
            try
            {
                YassakoPortalLogic.Models.Transaction transaction = new YassakoPortalLogic.Models.Transaction();
                transaction.RequestedBy = Session["Uname"].ToString();
                transaction.FromDate = "2020-01-01";
                transaction.ToDate = DateTime.Now.ToString("yyyy-MM-dd");
                if (Session["Utype"].ToString().ToUpper().Equals("YASSAKO"))
                {
                    transaction.Vendor = "ALL";
                }
                else
                {
                    transaction.Vendor = Session["Utype"].ToString().ToUpper();
                }
                YassakoPortalLogic.Logic.TransactionProcessor tranprocessor = new YassakoPortalLogic.Logic.TransactionProcessor(transaction);
                YassakoPortalLogic.Models.TransactionSearchResponse searchResult = new YassakoPortalLogic.Models.TransactionSearchResponse();
                searchResult = tranprocessor.SearchTransaction();
                if (searchResult.IsSuccessfull)
                {
                    ViewBag.Transactions = searchResult.transactions;
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

        public ActionResult UtilityTransactions()
        {
            try
            {
                YassakoPortalLogic.Models.Transaction transaction = new YassakoPortalLogic.Models.Transaction();
                transaction.RequestedBy = Session["Uname"].ToString();
                transaction.FromDate = "2020-01-01";
                transaction.ToDate = DateTime.Now.ToString("yyyy-MM-dd");
                if (Session["Utype"].ToString().ToUpper().Equals("YASSAKO"))
                {
                    transaction.Vendor = "ALL";
                }
                else
                {
                    transaction.Vendor = Session["Utype"].ToString().ToUpper();
                }
                YassakoPortalLogic.Logic.TransactionProcessor tranprocessor = new YassakoPortalLogic.Logic.TransactionProcessor(transaction);
                YassakoPortalLogic.Models.TransactionSearchResponse searchResult = new YassakoPortalLogic.Models.TransactionSearchResponse();
                searchResult = tranprocessor.UtilityTransaction();
                if (searchResult.IsSuccessfull)
                {
                    ViewBag.Transactions = searchResult.transactions;
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
    }
}