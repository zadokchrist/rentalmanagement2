using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YassakoPortalLogic.Models;

namespace YassakoPortalLogic.Logic
{
    public class RentalProcessor
    {
        DatabaseHandler dh = new DatabaseHandler();
        LandLord landlord;
        DataTable table;
        EmailProcessor sendEmail = new EmailProcessor();
        GenericResponse response;
        List<Object> objects = new List<Object>();
        public RentalProcessor() 
        {
            response = new GenericResponse();
        }

        public GenericResponse GetLandlords()
        {
            try
            {
                table = dh.GetLandLords();
                if (table.Rows.Count > 0)
                {
                    List<object> landLords = new List<object>();
                    foreach (DataRow dr in table.Rows)
                    {
                        LandLord landLord = new LandLord();
                        landLord.id = dr["RecordId"].ToString();
                        landLord.name = dr["Name"].ToString();
                        landLord.email = dr["Email"].ToString();
                        landLord.tel = dr["telcontact"].ToString();
                        landLord.creationdate = dr["CreationDate"].ToString();
                        landLord.active = bool.Parse(dr["Active"].ToString());
                        landLords.Add(landLord);
                    }
                    response.IsSuccessfull = true;
                    response.Message = "Successfull";
                    response.list = landLords;
                }
                else
                {
                    response.IsSuccessfull = false;
                    response.Message = "NO LANDLORDS FOUND";
                }
            }
            catch (Exception ex)
            {
                response.IsSuccessfull = false;
                response.Message = ex.Message;
            }
            return response;
        }

        public GenericResponse GetTenantsById(string tenantid)
        {
            try
            {
                table = dh.GetTenantsById(tenantid);
                if (table.Rows.Count > 0)
                {
                    foreach (DataRow dr in table.Rows)
                    {
                        Tenant tenant = new Tenant();
                        tenant.TenantId = dr["TenantId"].ToString();
                        tenant.TenantName = dr["TenantName"].ToString();
                        tenant.Telnumber = dr["Telnumber"].ToString();
                        tenant.CreationDate = dr["CreationDate"].ToString();
                        tenant.PropertyId = dr["PropertyId"].ToString();
                        objects.Add(tenant);
                    }
                    response.IsSuccessfull = true;
                    response.list = objects;
                }
                else
                {
                    response.IsSuccessfull = false;
                    response.Message = "NO TENANTS AVAILABLE";
                }
            }
            catch (Exception ex)
            {
                response.IsSuccessfull = false;
                response.Message = ex.Message;
            }
            return response;
        }

        public GenericResponse AddLandLord(LandLord landLord)
        {
            try
            {
                dh.AddLandlord(landLord.name, landLord.email, landLord.tel);
                response.IsSuccessfull = true;
                response.Message = "LandLord Added Successfully";
            }
            catch (Exception ex)
            {
                response.IsSuccessfull = false;
                response.Message = ex.Message;
            }
            return response;
        }

        public GenericResponse AddProperty(Property property)
        {
            try
            {
                dh.AddProperty(property.LandLordId, property.PRN, property.PropertyLoc, property.Longtitude, property.Latitude, property.TotalRooms,property.RentValue);
                response.IsSuccessfull = true;
                response.Message = "PROPERTY ADDED SUCCESSFULLY";
            }
            catch (Exception ex)
            {
                response.IsSuccessfull = false;
                response.Message = ex.Message;
            }
            return response;
        }

        public GenericResponse GetProperties(Property property)
        {
            try
            {
                table = dh.GetProperties(property.LandLordId);
                if (table.Rows.Count > 0)
                {
                    List<object> properties = new List<object>();
                    foreach (DataRow dr in table.Rows)
                    {
                        Property property1 = new Property();
                        property1.PropertyId = dr["PropertyId"].ToString();
                        property1.LandLordId = dr["LandLordId"].ToString();
                        property1.PRN = dr["PRN"].ToString();
                        property1.PropertyLoc = dr["PropertyLoc"].ToString();
                        property1.Longtitude = dr["Longtitude"].ToString();
                        property1.Latitude = dr["Latitude"].ToString();
                        property1.CreationDate = dr["CreationDate"].ToString();
                        property1.Status = dr["Status"].ToString();
                        property1.RentValue = dr["RentValue"].ToString();
                        properties.Add(property1);
                    }
                    response.list = properties;
                    response.IsSuccessfull = true;
                    response.Message = "SUCCESS";
                }
                else
                {
                    response.IsSuccessfull = false;
                    response.Message = "NO PROPERTY FOUND FOR THE LANDLORD";
                }
            }
            catch (Exception ex)
            {
                response.IsSuccessfull = false;
                response.Message = ex.Message;
            }
            return response;
        }

        public GenericResponse AddTenant(Tenant tenant) 
        {
            try
            {
                dh.AddTenant(tenant.TenantName,tenant.Telnumber,tenant.Deposit,tenant.Paymentdate,tenant.Paymentmode,tenant.PropertyId);
                response.IsSuccessfull = true;
                response.Message = "TENANT ADDED SUCCESSFULLY";
            }
            catch (Exception ex)
            {
                response.IsSuccessfull = false;
                response.Message = ex.Message;
            }
            return response;
        }

        public GenericResponse GetTenants() 
        {
            try
            {
                table = dh.GetTenants();
                if (table.Rows.Count>0)
                {
                    foreach (DataRow dr in table.Rows)
                    {
                        Tenant tenant = new Tenant();
                        tenant.TenantId = dr["TenantId"].ToString();
                        tenant.TenantName = dr["TenantName"].ToString();
                        tenant.Telnumber = dr["Telnumber"].ToString();
                        tenant.CreationDate = dr["CreationDate"].ToString();
                        tenant.PropertyId = dr["PropertyId"].ToString();
                        objects.Add(tenant);
                    }
                    response.IsSuccessfull = true;
                    response.list = objects;
                }
                else
                {
                    response.IsSuccessfull = false;
                    response.Message = "NO TENANTS AVAILABLE";
                }
            }
            catch (Exception ex)
            {
                response.IsSuccessfull = false;
                response.Message = ex.Message;
            }
            return response;
        }

        public GenericResponse GetTenantsByLandLordId(string landlordId) 
        {
            try
            {
                table = dh.GetTenantsByLandLordId(landlordId);
                if (table.Rows.Count > 0)
                {
                    foreach (DataRow dr in table.Rows)
                    {
                        Tenant tenant = new Tenant();
                        tenant.TenantId = dr["TenantId"].ToString();
                        tenant.TenantName = dr["TenantName"].ToString();
                        tenant.Telnumber = dr["Telnumber"].ToString();
                        tenant.CreationDate = dr["CreationDate"].ToString();
                        tenant.PropertyId = dr["PropertyId"].ToString();
                        objects.Add(tenant);
                    }
                    response.IsSuccessfull = true;
                    response.list = objects;
                }
                else
                {
                    response.IsSuccessfull = false;
                    response.Message = "NO TENANTS AVAILABLE";
                }
            }
            catch (Exception ex)
            {
                response.IsSuccessfull = false;
                response.Message = ex.Message;
            }
            return response;
        }

        public GenericResponse AddTenantPayment(TenantPayment payment) 
        {
            try
            {
                dh.AddTenantPayment(payment.PropertyRef, payment.Amount, payment.DatePaid, payment.PaymentMode, payment.RecordedBy, payment.ReceiptNumber);
                response.IsSuccessfull = true;
                response.Message = "PAYMENT RECORDED SUCCESSFULLY";
            }
            catch (Exception ex)
            {
                response.IsSuccessfull = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public GenericResponse GetTenantPayments() 
        {
            try
            {
                table = dh.GetTenantPayments();
                if (table.Rows.Count>0)
                {
                    foreach (DataRow dr in table.Rows)
                    {
                        TenantPayment payment = new TenantPayment();
                        payment.Amount = dr["Amount"].ToString();
                        payment.DatePaid = dr["DatePaid"].ToString();
                        payment.PaymentMode = dr["PaymentMode"].ToString();
                        payment.PropertyRef = dr["PropertyRef"].ToString();
                        payment.ReceiptNumber = dr["ReceiptNumber"].ToString();
                        payment.RecordDate = dr["RecordDate"].ToString();
                        payment.RecordedBy = dr["RecordedBy"].ToString();
                        payment.KhpComm = (double.Parse(payment.Amount) * 0.1).ToString();
                        payment.LandLordCommission = (double.Parse(payment.Amount) - double.Parse(payment.KhpComm)).ToString();
                        objects.Add(payment);
                    }
                    response.list = objects;
                    response.IsSuccessfull = true;
                    response.Message = "SUCCESSFUL";
                }
            }
            catch (Exception ex)
            {
                response.IsSuccessfull = false;
                response.Message = ex.Message;
            }
            return response;
        }

        public GenericResponse SearchTenantPayments(TenantPayment pymt)
        {
            try
            {
                table = dh.SearchTenantPayments(pymt.LandLordid, pymt.DatePaid, pymt.RecordDate);
                if (table.Rows.Count > 0)
                {
                    foreach (DataRow dr in table.Rows)
                    {
                        TenantPayment payment = new TenantPayment();
                        payment.Amount = dr["Amount"].ToString();
                        payment.DatePaid = dr["DatePaid"].ToString();
                        payment.PaymentMode = dr["PaymentMode"].ToString();
                        payment.PropertyRef = dr["PropertyRef"].ToString();
                        payment.ReceiptNumber = dr["ReceiptNumber"].ToString();
                        payment.RecordDate = dr["RecordDate"].ToString();
                        payment.RecordedBy = dr["RecordedBy"].ToString();
                        payment.KhpComm = (double.Parse(payment.Amount) * 0.1).ToString();
                        payment.LandLordCommission = (double.Parse(payment.Amount) - double.Parse(payment.KhpComm)).ToString();
                        objects.Add(payment);
                    }

                    
                    response.list = objects;
                    response.IsSuccessfull = true;
                    response.Message = "SUCCESSFUL";
                }
            }
            catch (Exception ex)
            {
                response.IsSuccessfull = false;
                response.Message = ex.Message;
            }
            return response;
        }

        public GenericResponse GetTenantsByPropertyId(string propertyid) 
        {
            try
            {
                table = dh.GetTenantsByPropertyId(propertyid);
                if (table.Rows.Count > 0)
                {
                    foreach (DataRow dr in table.Rows)
                    {
                        Tenant tenant = new Tenant();
                        tenant.TenantId = dr["TenantId"].ToString();
                        tenant.TenantName = dr["TenantName"].ToString();
                        tenant.Telnumber = dr["Telnumber"].ToString();
                        tenant.CreationDate = dr["CreationDate"].ToString();
                        tenant.PropertyId = dr["PropertyId"].ToString();
                        objects.Add(tenant);
                    }
                    response.IsSuccessfull = true;
                    response.list = objects;
                }
                else
                {
                    response.IsSuccessfull = false;
                    response.Message = "NO TENANTS AVAILABLE";
                }
            }
            catch (Exception ex)
            {
                response.IsSuccessfull = false;
                response.Message = ex.Message;
            }
            return response;
        }

        public GenericResponse RemoveTenantFromProperty(string propertyid, string reason) 
        {
            try
            {
                dh.RemoveTenantFromProperty(propertyid, reason);
                response.IsSuccessfull = true;
                response.Message = "TENANT REMOVED SUCCESSFUL";
            }
            catch (Exception ex)
            {
                response.IsSuccessfull = false;
                response.Message = ex.Message;
            }

            return response;
        }
    }
}
