using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace YassakoPortal.Controllers
{
    public class TransactionController : Controller
    {
        YassakoPortalLogic.Models.GenericResponse response = new YassakoPortalLogic.Models.GenericResponse();
        YassakoPortalLogic.Models.Property property = new YassakoPortalLogic.Models.Property();
        YassakoPortalLogic.Logic.RentalProcessor processor = new YassakoPortalLogic.Logic.RentalProcessor();
        // GET: Transaction
        public ActionResult Index(string tenantid)
        {
            try
            {
                response = processor.GetTenantsById(tenantid);
                if (response.IsSuccessfull)
                {
                    ViewBag.Tenant = response.list;
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

        [HttpPost]
        public ActionResult Index(FormCollection collection,string tenantid)
        {
            try
            {
                YassakoPortalLogic.Models.TenantPayment payment = new YassakoPortalLogic.Models.TenantPayment();
                payment.PropertyRef = tenantid;
                payment.Amount = Request["deposit"];
                payment.DatePaid = Request["paymentdate"];
                payment.PaymentMode = Request["paymentmode"];
                payment.ReceiptNumber = Request["receiptnumber"];
                payment.RecordedBy = Session["FullName"].ToString();
                response = processor.AddTenantPayment(payment);
                if (response.IsSuccessfull)
                {
                    response = processor.GetTenantsById(tenantid);
                    if (response.IsSuccessfull)
                    {
                        ViewBag.Tenant = response.list;
                    }
                    else
                    {
                        ViewBag.Error = response.Message;
                    }
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

        public ActionResult UtilityTransactions()
        {
            try
            {
                response = processor.GetTenantPayments();
                if (response.IsSuccessfull)
                {
                    ViewBag.PaymentTransactions = response.list;
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