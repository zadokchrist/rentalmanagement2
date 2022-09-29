using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YassakoPortalLogic.Models;

namespace YassakoPortalLogic.Logic
{
    public class BalanceProcessor
    {
        public BalanceResponse GetUtilityAccountBalance() 
        {
			BalanceResponse response = new BalanceResponse();
			try
			{
				liveutilityapi.YakakoUtilityApi utilityApi = new liveutilityapi.YakakoUtilityApi();
				liveutilityapi.CheckBalanceRequest balanceRequest = new liveutilityapi.CheckBalanceRequest();
				liveutilityapi.CheckBalanceResponse balanceResponse = new liveutilityapi.CheckBalanceResponse();
				balanceRequest.Password = "Y@ssako@2019";
				balanceRequest.UtilityCode = "UMEME";
				balanceRequest.VendorCode = "AIRTEL";
				balanceResponse = utilityApi.GetUtilityBalance(balanceRequest);
				if (balanceResponse.errorCode.Equals("0"))
				{
					response.IsSuccessfull = true;
					response.Message = "SUCCESSFUL";
					response.Balance = balanceResponse.UtilityBalance;
					response.Commission = balanceResponse.UtilityCommBalance;
				}
				else
				{
					response.IsSuccessfull = false;
					response.Message = balanceResponse.errorMsg;
				}
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
