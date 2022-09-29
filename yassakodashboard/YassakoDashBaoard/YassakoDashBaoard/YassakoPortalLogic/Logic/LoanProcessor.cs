using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YassakoPortalLogic.Models;

namespace YassakoPortalLogic.Logic
{
    public class LoanProcessor
    {
        DatabaseHandler dh = new DatabaseHandler();
        Loan loan;
        public LoanProcessor(Loan loan) 
        {
            this.loan = loan;
        }
        public RepaymentSearch GetRepayments() 
        {
            RepaymentSearch repaymentSearch = new RepaymentSearch();
            try
            {
                DataTable table = dh.GetLoanRepayments();
                if (table.Rows.Count>0)
                {
                    List<RepaymentHistory> repayments = new List<RepaymentHistory>();
                    foreach (DataRow dr in table.Rows)
                    {
                        RepaymentHistory repayment = new RepaymentHistory();
                        repayment.AmountRepaid = dr["AmountRepaid"].ToString();
                        repayment.Balance = dr["Balance"].ToString();
                        repayment.CustomerRef = dr["CustomerRef"].ToString();
                        repayment.CustomerTel = dr["CustomerTel"].ToString();
                        repayment.LoanVendorId = dr["LoanVendorId"].ToString();
                        repayment.LoanYassakoRef = dr["LoanYassakoRef"].ToString();
                        repayment.RepaymentDate = dr["RepaymentDate"].ToString();
                        repayment.RepaymentId = dr["RepaymentId"].ToString();
                        repayment.TotalLoanAmount = dr["TotalLoanAmount"].ToString();
                        repayment.TransactionDate = dr["TransactionDate"].ToString();
                        repayments.Add(repayment);
                    }

                    repaymentSearch.repayments = repayments;
                    repaymentSearch.IsSuccessfull = true;
                    repaymentSearch.Message = "SUCCESS";
                }
                else
                {
                    repaymentSearch.IsSuccessfull = false;
                    repaymentSearch.Message = "NO LOAN REPAYMENT FOUND";
                }
            }
            catch (Exception ex)
            {
                repaymentSearch.IsSuccessfull = false;
                repaymentSearch.Message = ex.Message;
            }
            return repaymentSearch;
        }
        public LoanSearch GetLoans()
        {
            LoanSearch loanSearch = new LoanSearch();
            try
            {
                DataTable getloans = dh.GetLoans(loan.Repaid, loan.PenaltyApplied);
                if (getloans.Rows.Count>0)
                {
                    List<Loan> loans = new List<Loan>();
                    foreach (DataRow dr in getloans.Rows)
                    {
                        Loan loan = new Loan();
                        loan.Amount = dr["Amount"].ToString();
                        loan.CustomerRef = dr["CustomerRef"].ToString();
                        loan.Repaymentid = dr["Repaymentid"].ToString();
                        loan.RepaymentDate = dr["RepaymentDate"].ToString();
                        loan.Repaid = bool.Parse(dr["Repaid"].ToString());
                        loan.RecordDate = dr["RecordDate"].ToString();
                        loan.PenaltyApplied = bool.Parse(dr["PenaltyApplied"].ToString());
                        loan.LoanId = dr["LoanId"].ToString();
                        loan.LoanDate = dr["LoanDate"].ToString();
                        loan.DateRepaid = dr["DateRepaid"].ToString();
                        loan.CustomerTel = dr["CustomerTel"].ToString();
                        loans.Add(loan);
                    }
                    loanSearch.Message = "SUCCESSFUL";
                    loanSearch.IsSuccessfull = true;
                    loanSearch.loans = loans;
                }
                else
                {
                    loanSearch.Message = "NO LOAN FOUND";
                    loanSearch.IsSuccessfull = false;
                }
            }
            catch (Exception ex)
            {
                loanSearch.Message = ex.Message;
                loanSearch.IsSuccessfull = false;
            }
            return loanSearch;
        }

        public LoanSearch SearchLoansByRepaidOrOverdue()
        {
            LoanSearch loanSearch = new LoanSearch();
            try
            {
                DataTable getloans = dh.SearchLoansByRepaidOrOverdue(loan.Repaid, loan.PenaltyApplied);
                if (getloans.Rows.Count > 0)
                {
                    List<Loan> loans = new List<Loan>();
                    foreach (DataRow dr in getloans.Rows)
                    {
                        Loan loan = new Loan();
                        loan.Amount = dr["Amount"].ToString();
                        loan.CustomerRef = dr["CustomerRef"].ToString();
                        loan.Repaymentid = dr["Repaymentid"].ToString();
                        loan.RepaymentDate = dr["RepaymentDate"].ToString();
                        loan.Repaid = bool.Parse(dr["Repaid"].ToString());
                        loan.RecordDate = dr["RecordDate"].ToString();
                        loan.PenaltyApplied = bool.Parse(dr["PenaltyApplied"].ToString());
                        loan.LoanId = dr["LoanId"].ToString();
                        loan.LoanDate = dr["LoanDate"].ToString();
                        loan.DateRepaid = dr["DateRepaid"].ToString();
                        loan.CustomerTel = dr["CustomerTel"].ToString();
                        loans.Add(loan);
                    }
                    loanSearch.Message = "SUCCESSFUL";
                    loanSearch.IsSuccessfull = true;
                    loanSearch.loans = loans;
                }
                else
                {
                    loanSearch.Message = "NO LOAN FOUND";
                    loanSearch.IsSuccessfull = false;
                }
            }
            catch (Exception ex)
            {
                loanSearch.Message = ex.Message;
                loanSearch.IsSuccessfull = false;
            }
            return loanSearch;
        }
    }
}
