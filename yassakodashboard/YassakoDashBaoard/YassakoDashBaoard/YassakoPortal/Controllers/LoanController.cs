using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace YassakoPortal.Controllers
{
    public class LoanController : Controller
    {
        // GET: Loan
        public ActionResult LoanStatus()
        {
            try
            {
                YassakoPortalLogic.Models.Loan loan = new YassakoPortalLogic.Models.Loan();
                YassakoPortalLogic.Logic.LoanProcessor loanProcessor = new YassakoPortalLogic.Logic.LoanProcessor(loan);
                YassakoPortalLogic.Models.LoanSearch searchResult = new YassakoPortalLogic.Models.LoanSearch();
                searchResult = loanProcessor.GetLoans();
                if (searchResult.IsSuccessfull)
                {
                    ViewBag.Loans = searchResult.loans;
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

        public ActionResult RepaymentHistory() 
        {
            try
            {
                YassakoPortalLogic.Models.Loan loan = new YassakoPortalLogic.Models.Loan();
                YassakoPortalLogic.Logic.LoanProcessor loanProcessor = new YassakoPortalLogic.Logic.LoanProcessor(loan);
                YassakoPortalLogic.Models.RepaymentSearch searchResult = new YassakoPortalLogic.Models.RepaymentSearch();
                searchResult = loanProcessor.GetRepayments();
                if (searchResult.IsSuccessfull)
                {
                    ViewBag.RepaymentHistory = searchResult.repayments;
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