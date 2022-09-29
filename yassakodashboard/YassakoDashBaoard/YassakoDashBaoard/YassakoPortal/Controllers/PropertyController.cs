using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace YassakoPortal.Controllers
{
    public class PropertyController : Controller
    {
        YassakoPortalLogic.Models.GenericResponse response = new YassakoPortalLogic.Models.GenericResponse();
        YassakoPortalLogic.Models.Property property = new YassakoPortalLogic.Models.Property();
        YassakoPortalLogic.Logic.RentalProcessor processor = new YassakoPortalLogic.Logic.RentalProcessor();
        // GET: Property
        public ActionResult ViewProperties(string LandloadId)
        {
            if (Session["FullName"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                try
                {
                    ViewBag.Landlordid = LandloadId;
                    property.LandLordId = LandloadId;
                    response = processor.GetProperties(property);
                    if (response.IsSuccessfull)
                    {
                        ViewBag.Properties = response.list;

                    }
                    else
                    {
                        ViewBag.Error = response.Message;
                    }
                }
                catch (Exception EX)
                {
                    ViewBag.Error = EX.Message;
                }
            }

            return View();
        }

        public ActionResult AddProperty(string LandloardId)
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddProperty(FormCollection collection, string LandloadId)
        {
            try
            {
                property.LandLordId = LandloadId;
                property.PRN = Request["prn"];
                property.PropertyLoc = Request["proploc"];
                property.Longtitude = Request["longtitude"];
                property.Latitude = Request["latitude"];
                property.TotalRooms = Request["numrooms"];
                property.RentValue = Request["rentvalue"];
                response = processor.AddProperty(property);
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

        public ActionResult AddTenant(string propertyId)
        {
            if (Session["FullName"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                try
                {
                }
                catch (Exception EX)
                {
                    ViewBag.Error = EX.Message;
                }
            }

            return View();
        }

        [HttpPost]
        public ActionResult AddTenant(FormCollection collection,string propertyId)
        {
            if (Session["FullName"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                try
                {
                    YassakoPortalLogic.Models.Tenant tenant = new YassakoPortalLogic.Models.Tenant();
                    tenant.PropertyId = propertyId;
                    tenant.TenantName = Request["name"];
                    tenant.Telnumber = Request["telnumber"];
                    tenant.Deposit = Request["deposit"];
                    tenant.Paymentdate = Request["paymentdate"];
                    tenant.Paymentmode = Request["paymentmode"];
                    response = processor.AddTenant(tenant);
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

            return View();
        }
    }
}