using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ShoesMarket.Domain.Abstractions;
using ShoesMarket.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SchoesMarket.ViewModels
{
    public partial class LoginViewModel : ObservableObject
    {
        private readonly IUserRepository _userRepository;

        public event Action? LoginSuccessful;

        [ObservableProperty]
        private string _username = "94d5ous@gmail.com";
        [ObservableProperty]
        private string _password = "uzWC67";
        [ObservableProperty]
        private string _errorMessage;

        public LoginViewModel(IUserRepository userRepository)
        {
            _userRepository = userRepository;   
        }
        [RelayCommand]
        private void ExecuteLogin()
        {
            if (AuthenticateUser(Username, Password))
            {
                LoginSuccessful?.Invoke();
            }
            else
            {
                ErrorMessage = "Неверный логин или пароль";
            }
        }

        private bool AuthenticateUser(string username, string password)
        {
            var user = _userRepository.GetOne(username, password);
            return user != null;
        }
    }
}
