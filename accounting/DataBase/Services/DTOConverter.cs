using System;
using System.Globalization;
using accounting.DataBase.DTOs;
using accounting.Models;

namespace accounting.DataBase.Services
{
    public class DTOConverter
    {
        private readonly CultureInfo _faCulture = new("fa-IR");

        public AccountDTO AccountModelToDTO(AccountsModel accountsModel)
        {
            return new AccountDTO
            {
                Credit = accountsModel.Credit,
                AvailableCredit = accountsModel.AvailableCredit,
                CreateDate = accountsModel.CreateDate.ToString(_faCulture),
                OwnerNationalId = accountsModel.OwnerNationalId
            };
        }

        public AccountsModel AccountDTOToModel(AccountDTO accountDTO, DataBaseAccountsServices dataBaseAccountsServices)
        {
            return new AccountsModel(
                accountDTO.AccountId,
                accountDTO.Credit,
                accountDTO.AvailableCredit,
                DateTime.Parse(accountDTO.CreateDate, _faCulture),
                accountDTO.OwnerNationalId, dataBaseAccountsServices);
        }

        public PeoplesDTO PeopleModelToDTO(PeoplesModel peoplesModel)
        {
            return new PeoplesDTO
            {
                NationalId = peoplesModel.NationalId,
                Name = peoplesModel.Name,
                LastName = peoplesModel.LastName,
                FatherName = peoplesModel.FatherName,
                PersonalAccountNumber = peoplesModel.PersonalAccountNumber
            };
        }

        public PeoplesModel PeopleDTOToModel(PeoplesDTO peoplesDTO, DataBasePeopleServices dataBasePeopleServices)
        {
            return new PeoplesModel
            (peoplesDTO.NationalId,
                peoplesDTO.Name,
                peoplesDTO.LastName,
                peoplesDTO.FatherName,
                peoplesDTO.PersonalAccountNumber,
                dataBasePeopleServices
            );
        }

        public TransactionsDTO TransactionsToDTO(TransactionsModel transactionsModel)
        {
            return new TransactionsDTO
            {
                Amount = transactionsModel.Amount,
                Date = transactionsModel.Date.ToString(_faCulture),
                PersonalAccountNumber = "0",
                AccountId = transactionsModel.FundAccountId
            };
        }

        public TransactionsModel TransactionDTOToModel(TransactionsDTO transactionsDTO)
        {
            return new TransactionsModel(transactionsDTO.Id,
                transactionsDTO.Amount,
                transactionsDTO.AccountId,
                DateTime.Parse(transactionsDTO.Date),
                transactionsDTO.PersonalAccountNumber
            );
        }

        public LoanTransactinosDTO LoanTransactionModelToDTO(LoanTransactinosModel loanTransactinosModel)
        {
            return new LoanTransactinosDTO
            {
                Amount = loanTransactinosModel.Amount,
                Date = loanTransactinosModel.Date.ToString(CultureInfo.CurrentCulture),
                LoanId = loanTransactinosModel.LoanId,
                AccountId = loanTransactinosModel.AccountId,
                PersonalAccountNumber = loanTransactinosModel.PersonalAccountNumber
            };
        }

        public LoanTransactinosModel LoanTransactionsDTOToModel(LoanTransactinosDTO loanTransactinosDTO)
        {
            return new LoanTransactinosModel(loanTransactinosDTO.Id,
                loanTransactinosDTO.Amount,
                DateTime.Parse(loanTransactinosDTO.Date, _faCulture),
                loanTransactinosDTO.LoanId,
                loanTransactinosDTO.AccountId,
                loanTransactinosDTO.PersonalAccountNumber
            );
        }

        public LoansDTO LoanModelToDTO(LoanModel loanModel)
        {
            return new LoansDTO
            {
                AccountId = loanModel.AccountId,
                PersonalAccountNumber = loanModel.PersonalAccountNumber,
                LendDate = loanModel.LendDate.ToString(_faCulture),
                Amount = loanModel.Amount,
                InstallmentsCount = loanModel.InstallmentsCount
            };
        }

        public LoanModel LoanDTOToModel(LoansDTO loan)
        {
            return new LoanModel(loan.Id,
                loan.Amount,
                loan.InstallmentsCount,
                DateTime.Parse(loan.LendDate),
                loan.PersonalAccountNumber,
                loan.AccountId);
        }

        public LoanInstallmentsDTO LoanInstalmentModelToDTO(InstalmentLoanModel instalmentLoanModel)
        {
            return new LoanInstallmentsDTO
            {
                LoanId = instalmentLoanModel.LoanId,
                Amount = instalmentLoanModel.Amount,
                Date = instalmentLoanModel.Date.ToString(CultureInfo.CurrentCulture)
            };
        }
    }
}