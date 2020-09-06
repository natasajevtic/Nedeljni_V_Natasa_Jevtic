using System;
using System.Windows;
using System.Windows.Input;
using Zadatak_1.Commands;
using Zadatak_1.Models;
using Zadatak_1.Views;

namespace Zadatak_1.ViewModels
{
    class AddPostViewModel : BaseViewModel
    {
        AddPostView addPostView;
        Posts posts = new Posts();

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

        private vwPost post;

        public vwPost Post
        {
            get
            {
                return post;
            }
            set
            {
                post = value;
                OnPropertyChanged("Post");
            }
        }

        private ICommand addPost;

        public ICommand AddPost
        {
            get
            {
                if (addPost == null)
                {
                    addPost = new RelayCommand(param => AddPostExecute(), param => CanAddPostExecute());
                }
                return addPost;
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

        public AddPostViewModel(AddPostView addPostView, vwUser user)
        {
            this.addPostView = addPostView;
            User = user;
            Post = new vwPost();
            Post.UserId = User.UserId;
        }

        public void AddPostExecute()
        {
            if (String.IsNullOrEmpty(Post.PostContent))
            {
                MessageBox.Show("Please fill a field.", "Notification");
            }
            else
            {
                try
                {
                    MessageBoxResult result = MessageBox.Show("Are you sure you want to post?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (result == MessageBoxResult.Yes)
                    {
                        bool isPosted = posts.CreatePost(Post);
                        if (isPosted == true)
                        {
                            MessageBox.Show("Successful posted!", "Notification", MessageBoxButton.OK);
                            addPostView.Close();
                        }
                        else
                        {
                            MessageBox.Show("Posting cannot be performed.", "Notification", MessageBoxButton.OK);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        public bool CanAddPostExecute()
        {
            return true;
        }

        public void CancelExecute()
        {
            try
            {
                MessageBoxResult result = MessageBox.Show("Are you sure you want to cancel?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    addPostView.Close();
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