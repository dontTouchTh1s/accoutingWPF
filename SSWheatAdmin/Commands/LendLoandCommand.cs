﻿using System;
using System.Threading.Tasks;
using System.Windows.Media;
using MaterialDesignThemes.Wpf;
using SSWheatAdmin.Exceptions;
using SSWheatAdmin.Models;
using SSWheatAdmin.ViewModels.Dialogs;
using SSWheatAdmin.ViewModels.ManageLoans;

namespace SSWheatAdmin.Commands
{
    public class LendLoanCommand : BaseAsyncCommand
    {
        private readonly InvestmentFundModel _investmentFundModel;
        private readonly LendLoanViewModel _lendLoanViewModel;

        public LendLoanCommand(LendLoanViewModel lendLoanViewModel, InvestmentFundModel investmentFundModel)
        {
            _lendLoanViewModel = lendLoanViewModel;
            _investmentFundModel = investmentFundModel;
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            try
            {
                await _investmentFundModel.LendLoan(_lendLoanViewModel);
                var dialogViewModel = new MessageDialogViewModel("وام با موفقیت داده شد.",
                    PackIconKind.Check, new SolidColorBrush(Colors.Green));
                await DialogHost.Show(dialogViewModel, "rootDialog");
            }
            catch (NotEnoughCreditException e)
            {
                var dialogViewModel =
                    new MessageDialogViewModel(
                        string.Format("حداکثر مبلغ وام، دو برابر اعتبار حساب است. حداکثر {0}", e.MaximumLoan),
                        PackIconKind.WarningCircle, new SolidColorBrush(Colors.Red));
                await DialogHost.Show(dialogViewModel, "rootDialog");
            }
            catch (NotEnoughFundAvailableBalanceException e)
            {
                var dialogViewModel =
                    new MessageDialogViewModel(
                        string.Format("صندوق موجودی دردسترس کافی برای این وام ندارد. موجودی فعلی : {0}",
                            e.AvailableBalance),
                        PackIconKind.WarningCircle, new SolidColorBrush(Colors.Red));
                await DialogHost.Show(dialogViewModel, "rootDialog");
            }
            catch (Exception e)
            {
                var dialogViewModel =
                    new MessageDialogViewModel("خطایی در ثبت اطالاعات رخ داده است.",
                        PackIconKind.WarningCircle, new SolidColorBrush(Colors.Red));
                await DialogHost.Show(dialogViewModel, "rootDialog");
            }
        }
    }
}