using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using Zadatak_1.Commands;
using Zadatak_1.Models;
using Zadatak_1.Views;

namespace Zadatak_1.ViewModels
{
    class RequestsViewModel : BaseViewModel
    {
        RequestsView requestView;
        Users users = new Users();
        Requests requests = new Requests();

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

        private vwUser recipient;

        public vwUser Recipient
        {
            get
            {
                return recipient;
            }
            set
            {
                recipient = value;
                OnPropertyChanged("Recipient");
            }
        }

        private List<vwUser> notFriendsList;

        public List<vwUser> NotFriendsList
        {
            get
            {
                return notFriendsList;
            }
            set
            {
                notFriendsList = value;
                OnPropertyChanged("NotFriendsList");
            }
        }

        private vwRequest request;

        public vwRequest Request
        {
            get
            {
                return request;
            }
            set
            {
                request = value;
                OnPropertyChanged("Request");
            }
        }

        private List<vwRequest> requestList;

        public List<vwRequest> RequestList
        {
            get
            {
                return requestList;
            }
            set
            {
                requestList = value;
                OnPropertyChanged("RequestList");
            }
        }

        private ICommand sendRequest;

        public ICommand SendRequest
        {
            get
            {
                if (sendRequest == null)
                {
                    sendRequest = new RelayCommand(param => SendRequestExecute(), param => CanSendRequestExecute());
                }
                return sendRequest;
            }
        }

        private ICommand acceptRequest;

        public ICommand AcceptRequest
        {
            get
            {
                if (acceptRequest == null)
                {
                    acceptRequest = new RelayCommand(param => AcceptRequestExecute(), param => CanAcceptRequestExecute());
                }
                return acceptRequest;
            }
        }

        private ICommand declineRequest;

        public ICommand DeclineRequest
        {
            get
            {
                if (declineRequest == null)
                {
                    declineRequest = new RelayCommand(param => DeclineRequestExecute(), param => CanDeclineRequestExecute());
                }
                return declineRequest;
            }
        }

        public RequestsViewModel(RequestsView requestView, vwUser user)
        {
            this.requestView = requestView;
            User = user;
            NotFriendsList = users.UsersNotInFriends(User);
            RequestList = requests.ViewRecipientRequest(User);
        }

        public bool CanSendRequestExecute()
        {
            if (Recipient != null)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public void SendRequestExecute()
        {
            try
            {
                MessageBoxResult result = MessageBox.Show("Are you sure you want to send request?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    bool isSent = requests.SendRequest(User, Recipient);
                    if (isSent == true)
                    {
                        MessageBox.Show("Request sent!", "Notification", MessageBoxButton.OK);
                        NotFriendsList = users.UsersNotInFriends(User);
                    }
                    else
                    {
                        MessageBox.Show("Request cannot be sent.", "Notification", MessageBoxButton.OK);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public bool CanAcceptRequestExecute()
        {
            if (Request != null)
            {
                if (Request.Status == "Accepted")
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return false;
            }

        }

        public void AcceptRequestExecute()
        {
            try
            {
                MessageBoxResult result = MessageBox.Show("Are you sure you want to accept request?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    bool isAccepted = requests.AcceptRequest(Request);
                    if (isAccepted == true)
                    {
                        Thread.Sleep(1000);
                        MessageBox.Show("Request accepted!", "Notification", MessageBoxButton.OK);
                        NotFriendsList = users.UsersNotInFriends(User);
                        RequestList = requests.ViewRecipientRequest(User);
                    }
                    else
                    {
                        MessageBox.Show("Request cannot be accepted.", "Notification", MessageBoxButton.OK);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public bool CanDeclineRequestExecute()
        {
            if (Request != null)
            {
                if (Request.Status=="Declined")
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return false;
            }

        }

        public void DeclineRequestExecute()
        {
            try
            {
                MessageBoxResult result = MessageBox.Show("Are you sure you want to decline request?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    bool isDeclined = requests.DeclineRequest(Request);
                    if (isDeclined == true)
                    {
                        Thread.Sleep(1000);
                        MessageBox.Show("Request declined!", "Notification", MessageBoxButton.OK);
                        NotFriendsList = users.UsersNotInFriends(User);
                        RequestList = requests.ViewRecipientRequest(User);
                    }
                    else
                    {
                        MessageBox.Show("Request cannot be declined.", "Notification", MessageBoxButton.OK);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}