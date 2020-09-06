using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Zadatak_1.Commands;
using Zadatak_1.Models;
using Zadatak_1.Views;

namespace Zadatak_1.ViewModels
{
    class LoginViewModel : BaseViewModel
    {
        LoginView login;
        Users users = new Users();
        public vwUser User { get; set; }

        private string username;

        public string Username
        {
            get
            {
                return username;
            }
            set
            {
                username = value;
                OnPropertyChanged("Username");
            }
        }

        private string password;

        public string Password
        {
            get
            {
                return password;
            }
            set
            {
                password = value;
                OnPropertyChanged("Password");
            }
        }

        private ICommand logIn;

        public ICommand LogIn
        {
            get
            {
                if (logIn == null)
                {
                    logIn = new RelayCommand(LogInExecute, CanLogInExecute);
                }
                return logIn;
            }
        }

        private ICommand registration;

        public ICommand Registration
        {
            get
            {
                if (registration == null)
                {
                    registration = new RelayCommand(param => RegistrationExecute(), param => CanRegistrationExecute());
                }
                return registration;
            }
        }

        public LoginViewModel(LoginView login)
        {
            this.login = login;
        }
        /// <summary>
        /// This method checks if username and password valid.
        /// </summary>
        /// <param name="password">User input for password.</param>
        public void LogInExecute(object password)
        {
            Password = (password as PasswordBox).Password;
            if (users.FindUser(Username, Password) != null)
            {
                User = users.FindUser(Username, Password);
                UserView userView = new UserView();
                userView.ShowDialog();

            }
            else
            {
                MessageBox.Show("Wrong username or password. Please, try again.", "Notification");
            }
        }
        /// <summary>
        /// This method ensures that the login can only be executed when the login fields are not empty.
        /// </summary>
        /// <param name="password">User input for password.</param>
        /// <returns>True if login can execute, false if not.</returns>
        public bool CanLogInExecute(object password)
        {
            Password = (password as PasswordBox).Password;
            if (!String.IsNullOrEmpty(Username) && !String.IsNullOrEmpty(Password))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool CanRegistrationExecute()
        {
            return true;
        }

        public void RegistrationExecute()
        {
            RegistrationView registrationView = new RegistrationView();
            registrationView.ShowDialog();            
        }
    }
}