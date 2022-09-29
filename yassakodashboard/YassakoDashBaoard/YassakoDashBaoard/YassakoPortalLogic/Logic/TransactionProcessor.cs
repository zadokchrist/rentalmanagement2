using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YassakoPortalLogic.Models;

namespace YassakoPortalLogic.Logic
{
    public class TransactionProcessor
    {
        DatabaseHandler dh = new DatabaseHandler();
        Transaction trans;
        public TransactionProcessor(Transaction transaction)
        {
            this.trans = transaction;
        }
        /// <summary>
        /// Searches a transaction using given parameters
        /// </summary>
        /// <param name="transearchrequest"></param>
        /// <returns></returns>
        public TransactionSearchResponse SearchTransaction()
        {
            TransactionSearchResponse resp = new TransactionSearchResponse();
            try
            {
                if (string.IsNullOrEmpty(trans.FromDate))
                {
                    resp.IsSuccessfull = false;
                    resp.Message = "PLEASE SPECIFY THE FROM DATE";
                    dh.LogAuditTrail(trans.RequestedBy, "Queried transaction report and got error : " + resp.Message);
                }
                else if (string.IsNullOrEmpty(trans.ToDate))
                {
                    resp.IsSuccessfull = false;
                    resp.Message = "PLEASE SPECIFY THE TO DATE";
                    dh.LogAuditTrail(trans.RequestedBy, "Queried transaction report and got error : " + resp.Message);
                }
                else if (string.IsNullOrEmpty(this.trans.Vendor))
                {
                    resp.IsSuccessfull = false;
                    resp.Message = "PLEASE SPECIFY THE VENDOR";
                    dh.LogAuditTrail(trans.RequestedBy, "Queried transaction report and got error : " + resp.Message);
                }
                else
                {
                    DataTable dataTable = dh.GetRequestedTransactions(trans.CustPhone,trans.FromDate,trans.MeterNumber,trans.ToDate,trans.TranStatus,trans.Vendor,trans.VendorId,trans.Yassakoid);
                    if (dataTable.Rows.Count>0)
                    {
                        List<Transaction> tranlist = new List<Transaction>();
                        foreach (DataRow dr in dataTable.Rows)
                        {
                            Transaction tran = new Transaction();
                            tran.CustPhone = dr["CustomerTel"].ToString();
                            tran.MeterNumber = dr["CustRef"].ToString();
                            tran.PaymentDate = dr["PaymentDate"].ToString();
                            tran.TranAmount = dr["TranAmount"].ToString();
                            tran.TranStatus = dr["Status"].ToString();
                            tran.Vendor = dr["Vendor"].ToString();
                            tran.VendorId = dr["VendorTranId"].ToString();
                            tran.TranType = dr["TranType"].ToString();
                            tran.UtilityToken = dr["UtilityRef"].ToString();
                            tran.Yassakoid = dr["YakakoRef"].ToString();
                            tranlist.Add(tran);
                        }
                        resp.IsSuccessfull = true;
                        resp.Message = "SUCCESS";
                        resp.transactions = tranlist;
                        dh.LogAuditTrail(trans.RequestedBy, "Queried transaction report and got error : " + resp.Message);
                    }
                    else
                    {
                        resp.IsSuccessfull = false;
                        resp.Message = "NO TRANSACTION FOUND";
                        dh.LogAuditTrail(trans.RequestedBy, "Queried transaction report and got error : " + resp.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                resp.IsSuccessfull = false;
                resp.Message = "UNABLE TO SEARCH TRANSACTION AT THE MOMENT";
                dh.LogAuditTrail(trans.RequestedBy, "Queried transaction report and got error : " + resp.Message);
                dh.LogError("SearchTransaction", "TransactionProcessor", ex.Message,"YassakoPortal");
            }
            return resp;
        }

        public TransactionSearchResponse UtilityTransaction()
        {
            TransactionSearchResponse resp = new TransactionSearchResponse();
            try
            {
                if (string.IsNullOrEmpty(trans.FromDate))
                {
                    resp.IsSuccessfull = false;
                    resp.Message = "PLEASE SPECIFY THE FROM DATE";
                    dh.LogAuditTrail(trans.RequestedBy, "Queried transaction report and got error : " + resp.Message);
                }
                else if (string.IsNullOrEmpty(trans.ToDate))
                {
                    resp.IsSuccessfull = false;
                    resp.Message = "PLEASE SPECIFY THE TO DATE";
                    dh.LogAuditTrail(trans.RequestedBy, "Queried transaction report and got error : " + resp.Message);
                }
                else if (string.IsNullOrEmpty(this.trans.Vendor))
                {
                    resp.IsSuccessfull = false;
                    resp.Message = "PLEASE SPECIFY THE VENDOR";
                    dh.LogAuditTrail(trans.RequestedBy, "Queried transaction report and got error : " + resp.Message);
                }
                else
                {
                    DataTable dataTable = dh.GetUtilityTransacltions(trans.CustPhone, trans.FromDate, trans.MeterNumber, trans.ToDate, trans.TranStatus, trans.Vendor, trans.VendorId, trans.Yassakoid);
                    if (dataTable.Rows.Count > 0)
                    {
                        List<Transaction> tranlist = new List<Transaction>();
                        foreach (DataRow dr in dataTable.Rows)
                        {
                            Transaction tran = new Transaction();
                            tran.CustPhone = dr["CustomerTel"].ToString();
                            tran.MeterNumber = dr["CustomerRef"].ToString();
                            tran.CustomerName = dr["CustomerName"].ToString();
                            tran.PaymentDate = dr["PaymentDate"].ToString();
                            tran.TranAmount = dr["TranAmount"].ToString();
                            tran.TranStatus = dr["Status"].ToString();
                            tran.Vendor = dr["Vendor"].ToString();
                            tran.VendorId = dr["VendorTranId"].ToString();
                            tran.Utility = dr["Utility"].ToString();
                            tran.UtilityToken = dr["UtilityRef"].ToString();
                            tran.Reason = dr["Reason"].ToString();
                            tran.Yassakoid = dr["YassakoId"].ToString();
                            tranlist.Add(tran);
                        }
                        resp.IsSuccessfull = true;
                        resp.Message = "SUCCESS";
                        resp.transactions = tranlist;
                        dh.LogAuditTrail(trans.RequestedBy, "Queried transaction report and got error : " + resp.Message);
                    }
                    else
                    {
                        resp.IsSuccessfull = false;
                        resp.Message = "NO TRANSACTION FOUND";
                        dh.LogAuditTrail(trans.RequestedBy, "Queried transaction report and got error : " + resp.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                resp.IsSuccessfull = false;
                resp.Message = "UNABLE TO SEARCH TRANSACTION AT THE MOMENT";
                dh.LogAuditTrail(trans.RequestedBy, "Queried transaction report and got error : " + resp.Message);
                dh.LogError("UtilityTransaction", "TransactionProcessor", ex.Message, "YassakoPortal");
            }
            return resp;
        }
    }
}
