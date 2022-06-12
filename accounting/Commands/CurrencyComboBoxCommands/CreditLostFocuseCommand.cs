﻿using accounting.Models;
using accounting.ViewModels.ManageAccounts;
using accounting.ViewModels.ManageLoans;

namespace accounting.Commands.CurrencyComboBoxCommands
{
    public class CreditLostFocusCommand : BaseCommand
    {
        private readonly CreateAccountViewModel _createAccountViewModel;
        private readonly InstalmentLoanViewModel? _instalmentLoanViewModel;

        public CreditLostFocusCommand(InstalmentLoanViewModel instalmentLoanViewModel)
        {
            _instalmentLoanViewModel = instalmentLoanViewModel;
        }

        public CreditLostFocusCommand(CreateAccountViewModel createAccountViewModel)
        {
            _createAccountViewModel = createAccountViewModel;
        }

        public override void Execute(object? parameter)
        {
            if (_instalmentLoanViewModel != null)
            {
                if (_instalmentLoanViewModel.Amount <
                    ulong.Parse(_instalmentLoanViewModel.CurrentSelectedLoan.MinimumInstalmentAmount))
                    _instalmentLoanViewModel.AmountView =
                        _instalmentLoanViewModel.CurrentSelectedLoan.MinimumInstalmentAmount;
                return;
            }

            if (_createAccountViewModel.Credit < InvestmentFundModel.MinimumCredit)
                _createAccountViewModel.CreditView = "500000";
        }
    }
}