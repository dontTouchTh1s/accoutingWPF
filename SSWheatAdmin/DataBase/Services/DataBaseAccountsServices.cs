using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SSWheatAdmin.DataBase.DbContexts;
using SSWheatAdmin.DataBase.DTOs;
using SSWheatAdmin.Exceptions;
using SSWheatAdmin.Models;

namespace SSWheatAdmin.DataBase.Services
{
    public class DataBaseAccountsServices
    {
        private readonly DataBaseTransactionsServices _dataBaseTransactionsServices;
        private readonly DTOConverter _dtoConverterService;
        private readonly InvestmentFundDbContextFactory _investmentFundDbContextFactory;

        public DataBaseAccountsServices(InvestmentFundDbContextFactory investmentFundDbContextFactory,
            DTOConverter dtoConverterService)
        {
            _investmentFundDbContextFactory = investmentFundDbContextFactory;
            _dtoConverterService = dtoConverterService;
            _dataBaseTransactionsServices =
                new DataBaseTransactionsServices(_investmentFundDbContextFactory, _dtoConverterService);
        }

        public async Task CreateAccount(AccountsModel account)
        {
            await using var context = _investmentFundDbContextFactory.CreateDbContext();
            var accountDTO = _dtoConverterService.AccountModelToDTO(account);
            context.Accounts.Add(accountDTO);
            await context.SaveChangesAsync();
        }

        public async Task MakeTransaction(TransactionsModel transactionsModel)
        {
            await using var context = _investmentFundDbContextFactory.CreateDbContext();
            var accountDTO = await context.Accounts.FindAsync(transactionsModel.FundAccountId);
            var transactionsDTO = _dtoConverterService.TransactionsToDTO(transactionsModel);

            switch (transactionsModel.Amount)
            {
                case < 0:
                {
                    // Check if available credit is not enough on withdraw, throw exception
                    if (accountDTO.AvailableCredit <
                        (ulong)Math.Abs(transactionsModel.Amount) + InvestmentFundModel.MinimumCredit)
                    {
                        ulong withdrawableCredit;
                        if (InvestmentFundModel.MinimumCredit > accountDTO.AvailableCredit)
                            withdrawableCredit = 0;
                        else withdrawableCredit = accountDTO.AvailableCredit - InvestmentFundModel.MinimumCredit;
                        throw new NotEnoughAvailableCreditException(withdrawableCredit);
                    }

                    var value = (ulong)Math.Abs(transactionsDTO.Amount);
                    accountDTO.Credit -= value;
                    accountDTO.AvailableCredit -= value;
                    break;
                }
                case > 0:
                    accountDTO.Credit += (ulong)transactionsDTO.Amount;
                    accountDTO.AvailableCredit += (ulong)transactionsDTO.Amount;
                    break;
            }

            context.Transactions.Add(transactionsDTO);
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<LoanModel>> GetAccountLoans(ushort? fundAccountId)
        {
            await using var context = _investmentFundDbContextFactory.CreateDbContext();
            var loans = await context.Loans.Where(lone => lone.AccountId == fundAccountId).ToListAsync();
            return loans.Select(loan => _dtoConverterService.LoanDTOToModel(loan)).ToList();
        }

        public async Task<Dictionary<LoanModel, ulong>> GetAccountUnpaidLoans(ushort? fundAccountId)
        {
            await using var context = _investmentFundDbContextFactory.CreateDbContext();
            var loansModel = new Dictionary<LoanModel, ulong>();
            var loans = await context.Loans.Where(lone => lone.AccountId == fundAccountId).ToListAsync();
            foreach (var loan in loans)
            {
                var loanPayedAmount = Enumerable.Aggregate<LoanInstallmentsDTO, ulong>(
                    context.LoanInstallments.Where(loanInstallment => loanInstallment.LoanId == loan.Id), 0,
                    (current, loanInstallment) => current + loanInstallment.Amount);
                if (loanPayedAmount != loan.Amount)
                    loansModel.Add(_dtoConverterService.LoanDTOToModel(loan), loan.Amount - loanPayedAmount);
            }

            return loansModel;
        }
    }
}