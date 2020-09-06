using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Zadatak_1.Commands;
using Zadatak_1.Models;
using Zadatak_1.Views;

namespace Zadatak_1.ViewModels
{
    class RegistrationViewModel : BaseViewModel
    {
        RegistrationView registrationView;
        Users users = new Users();
        Genders genders = new Genders();
        RelationshipStatus marriageStatus = new RelationshipStatus();

        private vwUser user;

        public vwUser User
        {
            get
            {
                return user;
            }
            set
            {
                user = value;
                OnPropertyChanged("User");
            }
        }

        private List<string> genderList;

        public List<string> GenderList
        {
            get
            {
                return genderList;
            }
            set
            {
                genderList = value;
                OnPropertyChanged("GenderList");
            }
        }

        private List<string> marriageStatusList;

        public List<string> MarriageStatusList
        {
            get
            {
                return marriageStatusList;
            }
            set
            {
                marriageStatusList = value;
                OnPropertyChanged("MarriageStatusList");
            }
        }

        private ICommand save;
        public ICommand Save
        {
            get
            {
                if (save == null)
                {
                    save = new RelayCommand(SaveExecute, CanSaveExecute);
                }
                return save;
            }
        }

        private ICommand cancel;
        public ICommand Cancel
        {
            get
            {
                if (cancel == null)
                {
                    cancel = new RelayCommand(param => CancelExecute(), param => CanCancelExecute());
                }
                return cancel;
            }
        }

        public RegistrationViewModel(RegistrationView registrationView)
        {
            this.registrationView = registrationView;
            User = new vwUser();
            GenderList = genders.GetGenders();
            MarriageStatusList = marriageStatus.GetRelationshipStatus();
        }

        public void SaveExecute(object password)
        {
            User.Password = (password as PasswordBox).Password;
            if (String.IsNullOrEmpty(User.FirstName) || String.IsNullOrEmpty(User.LastName) || String.IsNullOrEmpty(User.Username)
               || String.IsNullOrEmpty(User.Password) || String.IsNullOrEmpty(User.Gender) || String.IsNullOrEmpty(User.DateOfBirth.ToString()))

            {
                MessageBox.Show("Please fill all required fields.", "Notification");
            }
            else
            {
                try
                {
                    MessageBoxResult result = MessageBox.Show("Are you sure you want to register?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (result == MessageBoxResult.Yes)
                    {
                        bool isCreated = users.CreateUser(User, out int id);
                        User.UserId = id;
                        if (isCreated == true)
                        {
                            MessageBox.Show("Successful registration!", "Notification", MessageBoxButton.OK);
                            LoginView login = new LoginView();
                            registrationView.Close();                            
                            login.ShowDialog();
                        }
                        else
                        {
                            MessageBox.Show("Registration cannot be performed.", "Notification", MessageBoxButton.OK);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }
        public bool CanSaveExecute(object password)
        {
            User.Password = (password as PasswordBox).Password;
            return true;
        }

        public void CancelExecute()
        {
            try
            {
                MessageBoxResult result = MessageBox.Show("Are you sure you want to cancel registration?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    LoginView login = new LoginView();
                    registrationView.Close();                    
                    login.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public bool CanCancelExecute()
        {
            return true;
        }
    }
}