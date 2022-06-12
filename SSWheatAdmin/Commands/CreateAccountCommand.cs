using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Media;
using MaterialDesignThemes.Wpf;
using SSWheatAdmin.Exceptions;
using SSWheatAdmin.Models;
using SSWheatAdmin.ViewModels.Dialogs;
using SSWheatAdmin.ViewModels.ManageAccounts;

namespace SSWheatAdmin.Commands
{
    public class CreateAccountCommand : BaseAsyncCommand
    {
        private readonly CreateAccountViewModel _createAccountViewModel;
        private readonly InvestmentFundModel _investmentFundModel;

        public CreateAccountCommand(CreateAccountViewModel account, InvestmentFundModel investmentFundModel)
        {
            _createAccountViewModel = account;
            _investmentFundModel = investmentFundModel;
            account.ErrorsChanged += CreateAccountViewModelPropertyChanged;
        }

        private void CreateAccountViewModelPropertyChanged(object? sender, DataErrorsChangedEventArgs e)
        {
            OnCanExecuteChanged();
        }

        public override bool CanExecute(object? parameter)
        {
            if (_createAccountViewModel.NationalId == null ||
                _createAccountViewModel.Name == null ||
                _createAccountViewModel.LastName == null ||
                _createAccountViewModel.FatherName == null) return false;

            return !_createAccountViewModel.HasErrors;
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            try
            {
                await _investmentFundModel.AddPeople(_createAccountViewModel);

                var dialogViewModel = new MessageDialogViewModel("حساب با موفقیت ایجاد شد.",
                    PackIconKind.Check, new SolidColorBrush(Colors.Green));
                await DialogHost.Show(dialogViewModel, "rootDialog");
            }
            catch (NationalIdExistException e)
            {
                var dialogViewModel =
                    new MessageDialogViewModel($"حسابی با کد ملی {e.NationalId} قبلا ساخته شده است.",
                        PackIconKind.WarningCircle, new SolidColorBrush(Colors.Red));
                await DialogHost.Show(dialogViewModel, "rootDialog");
            }
            catch (Exception)
            {
                var dialogViewModel =
                    new MessageDialogViewModel("در ایجاد حساب مشکلی پیش آمده است.",
                        PackIconKind.WarningCircle, new SolidColorBrush(Colors.Red));
                await DialogHost.Show(dialogViewModel, "rootDialog");
            }
        }
    }
}