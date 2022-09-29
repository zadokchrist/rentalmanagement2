using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace YassakoPortal.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            //GetOverDueLoans();
            //GetUnPaidLoans();
            //GetAggregatorBalance();
            return View();
        }

        private void GetAggregatorBalance() 
        {
            try
            {
                //YassakoPortalLogic.Logic.BalanceProcessor balanceProcessor = new YassakoPortalLogic.Logic.BalanceProcessor();
                //YassakoPortalLogic.Models.BalanceResponse response = new YassakoPortalLogic.Models.BalanceResponse();
                //response = balanceProcessor.GetUtilityAccountBalance();
                //if (response.IsSuccessfull)
                //{
                //    ViewBag.UtilityBalance = (double.Parse(response.Balance)+15000000).ToString();
                //    ViewBag.Commission = response.Commission;
                //}
                //else
                //{
                //    ViewBag.Error = response.Message;
                //}
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }
        }
        private void GetLoans() 
        {
            try
            {
                YassakoPortalLogic.Models.Loan loan = new YassakoPortalLogic.Models.Loan();
                YassakoPortalLogic.Logic.LoanProcessor loanProcessor = new YassakoPortalLogic.Logic.LoanProcessor(loan);
                YassakoPortalLogic.Models.LoanSearch response = new YassakoPortalLogic.Models.LoanSearch();
                response = loanProcessor.GetLoans();
                if (response.IsSuccessfull)
                {
                    ViewBag.TotalLoans = response.loans.Count;
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
        }
        private void GetUnPaidLoans()
        {
            try
            {
                YassakoPortalLogic.Models.Loan loan = new YassakoPortalLogic.Models.Loan();
                YassakoPortalLogic.Logic.LoanProcessor loanProcessor = new YassakoPortalLogic.Logic.LoanProcessor(loan);
                YassakoPortalLogic.Models.LoanSearch response = new YassakoPortalLogic.Models.LoanSearch();
                response = loanProcessor.GetLoans();
                if (response.IsSuccessfull)
                {
                    ViewBag.UnPaidLoans = response.loans.Count;
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
        }
        private void GetOverDueLoans()
        {
            try
            {
                YassakoPortalLogic.Models.Loan loan = new YassakoPortalLogic.Models.Loan();
                loan.PenaltyApplied = true;
                YassakoPortalLogic.Logic.LoanProcessor loanProcessor = new YassakoPortalLogic.Logic.LoanProcessor(loan);
                YassakoPortalLogic.Models.LoanSearch response = new YassakoPortalLogic.Models.LoanSearch();
                response = loanProcessor.GetLoans();
                if (response.IsSuccessfull)
                {
                    ViewBag.OverDueLoans = response.loans.Count;
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
        }
    }
}