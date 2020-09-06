using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using Zadatak_1.Commands;
using Zadatak_1.Models;
using Zadatak_1.Views;

namespace Zadatak_1.ViewModels
{
    class ProfileViewModel : BaseViewModel
    {
        ProfileView profileView;
        Users users = new Users();
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

        private List<vwPost> listPost;

        public List<vwPost> ListPost
        {
            get
            {
                return listPost;
            }
            set
            {
                listPost = value;
                OnPropertyChanged("ListPost");
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

        private ICommand viewLikes;

        public ICommand ViewLikes
        {
            get
            {
                if (viewLikes == null)
                {
                    viewLikes = new RelayCommand(param => ViewLikesExecute(), param => CanViewLikesExecute());
                }
                return viewLikes;
            }
        }

        public ProfileViewModel(ProfileView profileView, vwUser user)
        {
            this.profileView = profileView;
            User = user;
            ListPost = posts.GetUserPosts(User);
        }

        public bool CanAddPostExecute()
        {
            return true;
        }

        public void AddPostExecute()
        {
            AddPostView addPostView = new AddPostView(User);
            addPostView.ShowDialog();
            ListPost = posts.GetUserPosts(User);
        }

        public bool CanViewLikesExecute()
        {
            if (Post != null)
            {
                if (Post.NumberOfLikes > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public void ViewLikesExecute()
        {
            try
            {
                OtherLikesView otherLikes = new OtherLikesView(Post);
                otherLikes.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}