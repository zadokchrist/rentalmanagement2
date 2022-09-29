using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using YassakoPortalLogic.Models;
using System.Net.Mail;

namespace YassakoPortalLogic.Logic
{
    public class UserProcessor
    {
        DatabaseHandler dh = new DatabaseHandler();
        SystemUser user;
        DataTable table;
        EmailProcessor sendEmail = new EmailProcessor();
        public UserProcessor(SystemUser user)
        {
            this.user = user;
        }

        public UserProcessor()
        { }

        public GenericResponse ChangeUserPassword(string newpwd,string pwdrepeat)
        {
            GenericResponse response = new GenericResponse();
            try
            {
                DataTable dataTable = dh.GetLoginDetails(this.user.Username);
                if (dataTable.Rows.Count > 0)
                {
                    string userpassword = dataTable.Rows[0]["UserPassword"].ToString();
                    string encryptedpwd = MD5Hash(user.UserPassword);
                    if (userpassword.Equals(encryptedpwd))
                    {
                        bool IsActive = bool.Parse(dataTable.Rows[0]["Active"].ToString());
                        if (!IsActive)
                        {
                            response.IsSuccessfull = false;
                            response.Message = "USER WAS DISABLED PLEASE CONTACT SYSTEM ADMINISTRATOR";
                        }
                        else if (!newpwd.Equals(pwdrepeat))
                        {
                            response.IsSuccessfull = false;
                            response.Message = "YOUR PASSWORDS MISMATCH. PLEASE TRY AGAIN";
                        }
                        else
                        {
                            string encpwd = MD5Hash(newpwd);
                            dh.UpdateNewPwd(user.Username, encpwd);
                            response.IsSuccessfull = true;
                            response.Message = "YOUR PASSWORD HAS SUCCESSFULLY BEEN CHANGED";
                        }
                    }
                    else
                    {
                        response.IsSuccessfull = false;
                        response.Message = "WRONG OLD PASSWORD";
                    }
                }
                else
                {
                    response.IsSuccessfull = false;
                    response.Message = "THIS USER IS NOT FOUND";
                }
            }
            catch (Exception ex)
            {
                response.IsSuccessfull = false;
                response.Message = ex.Message;
            }
            return response;
        }

        public UserDepartments GetUserDepartments(string status)
        {
            UserDepartments departments = new UserDepartments();
            try
            {
                table = dh.GetUserDepartments(status);
                if (table.Rows.Count > 0)
                {
                    List<Department> departments1 = new List<Department>();
                    foreach (DataRow dr in table.Rows)
                    {
                        Department department = new Department();
                        department.RecordId = dr["RecordId"].ToString();
                        department.DepartmentName = dr["DepartmentName"].ToString();
                        department.Departmentemail = dr["DepartmentEmail"].ToString();
                        department.Status = dr["Status"].ToString();
                        department.RecordDate = dr["RecordDate"].ToString();
                        department.RecordedBy = dr["RecordedBy"].ToString();
                        departments1.Add(department);
                    }
                    departments.IsSuccessfull = true;
                    departments.Message = "SUCCESS";
                    departments.departments = departments1;
                }
                else
                {
                    departments.IsSuccessfull = false;
                    departments.Message = "NO USER DEPARTMENTS FOUND";
                }
            }
            catch (Exception ex)
            {
                departments.IsSuccessfull = false;
                departments.Message = ex.Message;
            }
            return departments;
        }

        public UserDepartments GetUserDepartmentsById(string departmentid)
        {
            UserDepartments departments = new UserDepartments();
            try
            {
                table = dh.GetUserDepartmentsById(departmentid);
                if (table.Rows.Count > 0)
                {
                    List<Department> departments1 = new List<Department>();
                    foreach (DataRow dr in table.Rows)
                    {
                        Department department = new Department();
                        department.RecordId = dr["RecordId"].ToString();
                        department.DepartmentName = dr["DepartmentName"].ToString();
                        department.Departmentemail = dr["DepartmentEmail"].ToString();
                        department.Status = dr["Status"].ToString();
                        department.RecordDate = dr["RecordDate"].ToString();
                        department.RecordedBy = dr["RecordedBy"].ToString();
                        departments1.Add(department);
                    }
                    departments.IsSuccessfull = true;
                    departments.Message = "SUCCESS";
                    departments.departments = departments1;
                }
                else
                {
                    departments.IsSuccessfull = false;
                    departments.Message = "NO USER DEPARTMENTS FOUND";
                }
            }
            catch (Exception ex)
            {
                departments.IsSuccessfull = false;
                departments.Message = ex.Message;
            }
            return departments;
        }
        /// <summary>
        /// Resets system user
        /// </summary>
        /// <returns></returns>
        public GenericResponse ResetUser()
        {
            GenericResponse response = new GenericResponse();
            try
            {
                string randompwd = CreatePassword(8);                
                if (user.Active)
                {
                    string encpwd = MD5Hash(randompwd);
                    dh.UpdateNewPwd(user.UserEmail, encpwd);
                    dh.LogAuditTrail(user.Username, "Reset User Password for : " + user.UserEmail);
                    string Name = GetUserDetailsByEmail(user.UserEmail).Rows[0]["FullName"].ToString();
                    string Message = "Dear " + Name + ",<br>Please find below are your yassako new user credentials. Please remember to change them on your initial login<br>";
                    Message += "User name : " + user.UserEmail + "<br>User Password : " + randompwd;
                    response = sendEmail.SendEmail(Name, user.UserEmail, "YASSAKO USER CREDENTIALS", Message);
                    if (response.IsSuccessfull)
                    {
                        response.IsSuccessfull = true;
                        response.Message = "SUCCESSFUL";
                    }
                    else
                    {
                        dh.LogError("SendEmail", "UserProcessor", response.Message, "YassakoPortal");
                    }
                    
                }
                else
                {
                    response.IsSuccessfull = false;
                    response.Message = "UNABLE TO RESET USER PASSWORD";
                }
            }
            catch (Exception ex)
            {
                response.IsSuccessfull = false;
                response.Message = ex.Message;
                dh.LogError("ResetUser", "UserProcessor", ex.Message, "YassakoPortal");
            }
            return response;
        }

        /// <summary>
        /// Deactivates Activates user
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public GenericResponse DeactivateActivateUser()
        {
            GenericResponse response = new GenericResponse();
            try
            {
                user.Active = bool.Parse(GetUserDetailsByEmail(user.UserEmail).Rows[0]["Active"].ToString());
                if (user.Active)
                {
                    user.Active = false;
                    dh.LogAuditTrail(user.Username, "Deactivated User : "+ user.UserEmail);
                }
                else
                {
                    user.Active = true;
                    dh.LogAuditTrail(user.Username, "Activated User : " + user.UserEmail);
                }
                
                dh.ActivateDeactivateUser(user.UserEmail, user.Active);
                response.IsSuccessfull = true;
                response.Message = "SUCCESSFUL";
            }
            catch (Exception ex)
            {
                response.IsSuccessfull = false;
                response.Message = ex.Message;
                dh.LogError("DeactivateActivateUser", "UserProcessor",ex.Message, "YassakoPortal");
            }
            return response;
        }

        /// <summary>
        /// Gets all system users in the system
        /// </summary>
        /// <returns></returns>
        public UserSearchResult GetSystemUsers()
        {
            UserSearchResult userSearchResult = new UserSearchResult();
            try
            {
                dh.LogAuditTrail(user.Username, "Quarried user details");
                DataTable dataTable = dh.GetSystemUsers();
                if (dataTable.Rows.Count > 0)
                {
                    List<SystemUser> users = new List<SystemUser>();
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        SystemUser systemUser = new SystemUser();
                        systemUser.FullName = dr["FullName"].ToString();
                        systemUser.PhoneNumber = dr["PhoneNumber"].ToString();
                        systemUser.UserCompany = dr["UserCompany"].ToString();
                        systemUser.UserEmail = dr["UserEmail"].ToString();
                        systemUser.Username = dr["Username"].ToString();
                        systemUser.Userrole = dr["Userrole"].ToString();
                        systemUser.Active = bool.Parse(dr["Active"].ToString());
                        users.Add(systemUser);
                    }
                    userSearchResult.IsSuccessfull = true;
                    userSearchResult.Message = "SUCCESS";
                    userSearchResult.results = users;
                    dh.LogAuditTrail(user.UserEmail, "Queried successfully user details");
                }
                else
                {
                    userSearchResult.IsSuccessfull = false;
                    userSearchResult.Message = "NO SYSTEM USERS FOUND";
                }
            }
            catch (Exception ex)
            {
                userSearchResult.IsSuccessfull = false;
                userSearchResult.Message = ex.Message;
                dh.LogError("GetSystemUsers", "UserProcessor", ex.Message, "YassakoPortal");
            }
            return userSearchResult;
        }
        /// <summary>
        /// Gets the Login Details of the person logging into the system
        /// </summary>
        /// <returns></returns>
        public UserSearchResult LoginDetails()
        {
            UserSearchResult searchResult = new UserSearchResult();
            try
            {
                dh.LogAuditTrail(user.UserEmail, "Attempted to log into the system");
                DataTable dataTable = dh.GetLoginDetails(this.user.Username);
                if (dataTable.Rows.Count > 0)
                {
                    string userpassword = dataTable.Rows[0]["UserPassword"].ToString();
                    string encryptedpwd = MD5Hash(user.UserPassword);
                    if (userpassword.Equals(encryptedpwd))
                    {
                        bool IsActive = bool.Parse(dataTable.Rows[0]["Active"].ToString());
                        if (IsActive)
                        {
                            List<SystemUser> users = new List<SystemUser>();
                            foreach (DataRow dr in dataTable.Rows)
                            {
                                SystemUser systemUser = new SystemUser();
                                systemUser.FullName = dr["FullName"].ToString();
                                systemUser.PhoneNumber = dr["PhoneNumber"].ToString();
                                systemUser.UserCompany = dr["UserDepartment"].ToString();
                                systemUser.UserEmail = dr["UserEmail"].ToString();
                                systemUser.Username = dr["Username"].ToString();
                                systemUser.Userrole = dr["Userrole"].ToString();
                                users.Add(systemUser);
                            }
                            searchResult.IsSuccessfull = true;
                            searchResult.Message = "SUCCESS";
                            searchResult.results = users;
                            dh.LogAuditTrail(user.UserEmail, "Logged into the system successfully");
                        }
                        else
                        {
                            searchResult.IsSuccessfull = false;
                            searchResult.Message = "USER WAS DISABLED PLEASE CONTACT SYSTEM ADMINISTRATOR";
                        }

                    }
                    else
                    {
                        searchResult.IsSuccessfull = false;
                        searchResult.Message = "INVALID LOGIN CREDENTIALS";
                    }

                }
                else
                {
                    searchResult.IsSuccessfull = false;
                    searchResult.Message = "NO USERS FOUND";
                }
            }
            catch (Exception ex)
            {
                searchResult.IsSuccessfull = false;
                searchResult.Message = "EXCEPTION OCCURED";
                dh.LogError("GetUsers", "UserProcessor", ex.Message, "YassakoPortal");
            }
            return searchResult;
        }
        /// <summary>
        /// Creates users in the system
        /// </summary>
        /// <returns></returns>
        /// <summary>
        /// Creates users in the system
        /// </summary>
        /// <returns></returns>
        public GenericResponse RegisterUser()
        {
            GenericResponse resp = new GenericResponse();
            try
            {
                if (IsUserEmailRegistered(user.UserEmail))
                {
                    resp.IsSuccessfull = false;
                    resp.Message = "EMAIL ALREADY REGISTERED WITH ANOTHER USER";
                }
                else if (string.IsNullOrEmpty(user.FullName))
                {
                    resp.IsSuccessfull = false;
                    resp.Message = "USER NAME REQUIRED";
                }
                else if (string.IsNullOrEmpty(user.UserEmail))
                {
                    resp.IsSuccessfull = false;
                    resp.Message = "EMAIL REQUIRED";
                }
                else if (string.IsNullOrEmpty(user.Userrole))
                {
                    resp.IsSuccessfull = false;
                    resp.Message = "USER ROLE REQUIRED";
                }
                else if (!IsValidEmail(user.UserEmail))
                {
                    resp.IsSuccessfull = false;
                    resp.Message = "INVALID EMAIL ADDRESS";
                }
                else if (string.IsNullOrEmpty(user.Section))
                {
                    resp.IsSuccessfull = false;
                    resp.Message = "PLEASE ENTER USER SECTION";
                }
                else
                {
                    user.Username = user.UserEmail;
                    string randomPassword = CreatePassword(8);
                    user.UserPassword = MD5Hash(randomPassword);
                    dh.RegisterUser(user.FullName, user.Username, user.UserEmail, user.UserCompany, user.Userrole, user.RecodedBy, user.UserPassword, user.PhoneNumber, user.Section);
                    resp.IsSuccessfull = true;
                    resp.Message = "SUCCESS";
                    dh.LogAuditTrail(user.Username, "registered user  with username : " + user.Username + " and email : " + user.UserEmail);
                    string Message = "Dear " + user.FullName + ",<br>Please find below are your yassako user credentials. Please remember to change them on your initial login<br>";
                    Message += "User name : " + user.Username + "<br>User Password : " + randomPassword;
                    resp = sendEmail.SendEmail(user.FullName, user.Username, "USER CREDENTIALS", Message);
                    if (resp.IsSuccessfull)
                    {
                        resp.Message = "User details have been captured successfully and email sent to user with details";
                    }
                    else
                    {
                        resp.Message = "User details sent successfully but email has not been sent. Error : " + resp.Message;
                    }

                }

            }
            catch (Exception ex)
            {
                resp.IsSuccessfull = false;
                resp.Message = ex.Message;
                dh.LogError("RegisterUser", "UserProcessor", ex.Message, "YassakoPortal");
            }
            return resp;
        }


        
        /// <summary>
        /// Generates a random password for the new user and the user who's password has been reset
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        private string CreatePassword(int length)
        {
            const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();
            while (0 < length--)
            {
                res.Append(valid[rnd.Next(valid.Length)]);
            }
            return res.ToString();
        }
        /// <summary>
        /// Checks whether this is a valid email address
        /// </summary>
        /// <param name="emailaddress"></param>
        /// <returns></returns>
        internal bool IsValidEmail(string emailaddress)
        {
            try
            {
                MailAddress m = new MailAddress(emailaddress);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }
        /// <summary>
        /// Checks whether User email is registered
        /// </summary>
        /// <param name="userEmail"></param>
        /// <returns></returns>
        private bool IsUserEmailRegistered(string userEmail)
        {
            bool IsTrue = false;
            try
            {
                DataTable userdetails = GetUserDetailsByEmail(userEmail);
                if (userdetails.Rows.Count > 0)
                {
                    IsTrue = true;
                }
            }
            catch (Exception ex)
            {
                dh.LogError("IsUserEmailRegistered", "UserProcessor", ex.Message, "YassakoPortal");
            }
            return IsTrue;
        }
        /// <summary>
        /// Returns user details basing on the email supplied
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public DataTable GetUserDetailsByEmail(string email)
        {
            try
            {
                table = dh.GetLoginDetails(email);
            }
            catch (Exception ex)
            {
                dh.LogError("GetUserDetailsByEmail", "UserProcessor", ex.Message, "YassakoPortal");
            }
            return table;
        }
        /// <summary>
        /// Encrypts the generated password
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        private string MD5Hash(string text)
        {
            MD5 md5 = new MD5CryptoServiceProvider();

            //compute hash from the bytes of text  
            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(text));

            //get hash result after compute it  
            byte[] result = md5.Hash;

            StringBuilder strBuilder = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                //change it into 2 hexadecimal digits  
                //for each byte  
                strBuilder.Append(result[i].ToString("x2"));
            }

            return strBuilder.ToString();
        }
    }
}
