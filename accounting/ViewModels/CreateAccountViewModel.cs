using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Windows.Input;
using accounting.Commands;
using accounting.Commands.CurrencyComboBoxCommands;
using accounting.Models;
using accounting.ViewModels.ValidationRules;

namespace accounting.ViewModels
{
    public class CreateAccountViewModel : BaseViewModel, INotifyDataErrorInfo
    {
        private readonly Dictionary<string, List<string>> _errors;
        private string _creditView = "0";
        private string? _fatherName;
        private string? _lastName;
        private string? _name;
        private string? _nationalId;
        private string? _personalAccountNumber;

        public CreateAccountViewModel()
        {
        }

        public CreateAccountViewModel(InvestmentFundModel investmentFundModel)
        {
            CreateAccountCommand = new CreateAccountCommand(this, investmentFundModel);
            CreditPreviewKeyUpCommand = new CreditPreviewKeyUpCommand();
            CreditPreviewKeyDownCommand = new CreditPreviewKeyDownCommand();
            CreditLostFocusCommand = new CreditLostFocusCommand(this);
            _errors = new Dictionary<string, List<string>>();
            CreditView = InvestmentFundModel.MinimumCredit.ToString();
        }

        public string? Name
        {
            get => _name;
            set
            {
                SetProperty(ref _name, value);
                var errors = new List<string>();
                _errors.Remove(nameof(Name));

                if (!CheckValidation.Required(_name))
                    errors.Add("فیلد را پر کنید.");
                if (!CheckValidation.Length(_name, 15, 3))
                    errors.Add("حداقل 3 و حداثکر 15 کاراکتر وارد کنید.");
                if (!CheckValidation.Alphabetical(_name))
                    errors.Add("فقط حروف وارد کنید.");

                if (errors.Count != 0)
                    _errors.Add(nameof(Name), errors);
                ErrorChanged(nameof(Name));
            }
        }

        public string? LastName
        {
            get => _lastName;
            set
            {
                SetProperty(ref _lastName, value);
                var errors = new List<string>();
                _errors.Remove(nameof(LastName));

                if (!CheckValidation.Required(_lastName))
                    errors.Add("فیلد را پر کنید.");
                if (!CheckValidation.Length(_lastName, 20, 3))
                    errors.Add("حداقل 3 و حداثکر 20 کاراکتر وارد کنید.");
                if (!CheckValidation.Alphabetical(_lastName))
                    errors.Add("فقط حروف وارد کنید.");

                if (errors.Count != 0)
                    _errors.Add(nameof(LastName), errors);
                ErrorChanged(nameof(LastName));
            }
        }

        public string? FatherName
        {
            get => _fatherName;
            set
            {
                SetProperty(ref _fatherName, value);
                var errors = new List<string>();
                _errors.Remove(nameof(FatherName));

                if (!CheckValidation.Required(_fatherName))
                    errors.Add("فیلد را پر کنید.");
                if (!CheckValidation.Length(_fatherName, 15, 3))
                    errors.Add("حداقل 3 و حداثکر 15 کاراکتر وارد کنید.");
                if (!CheckValidation.Alphabetical(_fatherName))
                    errors.Add("فقط حروف وارد کنید.");

                if (errors.Count != 0)
                    _errors?.Add(nameof(FatherName), errors);
                ErrorChanged(nameof(FatherName));
            }
        }

        public string? NationalId
        {
            get => _nationalId;
            set
            {
                SetProperty(ref _nationalId, value);
                var errors = new List<string>();
                _errors.Remove(nameof(NationalId));

                if (!CheckValidation.Required(_nationalId))
                    errors.Add("فیلد را پر کنید.");
                if (!CheckValidation.Length(_nationalId, 10))
                    errors.Add("کد ملی را 10 رقم وارد کنید.");
                if (!CheckValidation.Numerical(_nationalId))
                    errors.Add("فقط عدد وارد کنید.");

                if (errors.Count != 0)
                    _errors?.Add(nameof(NationalId), errors);
                ErrorChanged(nameof(NationalId));
            }
        }

        public string? PersonalAccountNumber
        {
            get => _personalAccountNumber;
            set
            {
                SetProperty(ref _personalAccountNumber, value);
                var errors = new List<string>();
                _errors.Remove(nameof(PersonalAccountNumber));

                if (!CheckValidation.Length(_personalAccountNumber, 16, true))
                    errors.Add("شماره حساب 16 رقمی وارد کنید.");
                if (!CheckValidation.Numerical(_personalAccountNumber, true))
                    errors.Add("فقط عدد وارد کنید.");

                if (errors.Count != 0)
                    _errors?.Add(nameof(PersonalAccountNumber), errors);
                ErrorChanged(nameof(PersonalAccountNumber));
            }
        }

        public ICommand? CreateAccountCommand { get; }

        public string CreditView
        {
            get => _creditView;
            set
            {
                ulong.TryParse(value, NumberStyles.Number, CultureInfo.CurrentCulture, out var provider);
                Credit = provider;
                value = provider.ToString("N0", CultureInfo.CurrentCulture);
                SetProperty(ref _creditView, value);
            }
        }

        public ulong Credit { get; set; }

        public ICommand CreditLostFocusCommand { get; }

        public ICommand CreditPreviewKeyDownCommand { get; }

        public ICommand CreditPreviewKeyUpCommand { get; }

        public IEnumerable GetErrors(string? propertyName)
        {
            return _errors.GetValueOrDefault(propertyName, new List<string>());
        }

        public bool HasErrors => _errors.Any();

        public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;

        private void ErrorChanged(string propertyName)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }
    }
}