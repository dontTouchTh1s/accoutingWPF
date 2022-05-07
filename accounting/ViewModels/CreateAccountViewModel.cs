﻿#nullable enable

namespace accounting.ViewModels
{
    public class CreateAccountViewModel : BaseViewModel
    {
        private string? _name;
        private string? _name2;

        public string? Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        public string? Name2
        {
            get => _name2;
            set => SetProperty(ref _name2, value);
        }
    }
}