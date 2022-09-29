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

        public GenericResponse GetLandlords()
        {
            GenericResponse response = new GenericResponse();
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

        public GenericResponse AddLandLord(LandLord landLord)
        {
            GenericResponse response = new GenericResponse();
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
            GenericResponse response = new GenericResponse();
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
            GenericResponse response = new GenericResponse();
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
            GenericResponse response = new GenericResponse();
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
    }
}
