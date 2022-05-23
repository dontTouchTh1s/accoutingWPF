﻿using accounting.Models;

namespace accounting.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public MainViewModel(InvestmentFundModel investmentFundModel)
        {
            TransactionsViewModel = new TransactionsViewModel(investmentFundModel);
            CreateAccountViewModel = new CreateAccountViewModel(investmentFundModel);
            SummeryViewModel = new SummeryViewModel(investmentFundModel);
            LendLoanViewModel = new LendLoanViewModel(investmentFundModel);
        }

        public CreateAccountViewModel CreateAccountViewModel { get; }
        public SummeryViewModel SummeryViewModel { get; }
        public TransactionsViewModel TransactionsViewModel { get; }

        public LendLoanViewModel LendLoanViewModel { get; }
    }
}