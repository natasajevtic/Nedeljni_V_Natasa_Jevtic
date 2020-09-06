using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using Zadatak_1.Commands;
using Zadatak_1.Models;
using Zadatak_1.Views;

namespace Zadatak_1.ViewModels
{
    class FeedViewModel : BaseViewModel
    {
        FeedView feedView;
        Users users = new Users();
        Posts posts = new Posts();
        FriendsPost friendsPost = new FriendsPost();

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

        private vwFriendPost post;

        public vwFriendPost Post
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

        private List<vwFriendPost> friendsPostList;

        public List<vwFriendPost> FriendsPostList
        {
            get
            {
                return friendsPostList;
            }
            set
            {
                friendsPostList = value;
                OnPropertyChanged("FriendsPostList");
            }
        }

        private vwPost postOfNotFriend;

        public vwPost PostOfNotFriend
        {
            get
            {
                return postOfNotFriend;
            }
            set
            {
                postOfNotFriend = value;
                OnPropertyChanged("PostOfNotFriend");
            }
        }

        private List<vwPost> otherPostList;

        public List<vwPost> OtherPostList
        {
            get
            {
                return otherPostList;
            }
            set
            {
                otherPostList = value;
                OnPropertyChanged("OtherPostList");
            }
        }

        private ICommand likePost;

        public ICommand LikePost
        {
            get
            {
                if (likePost == null)
                {
                    likePost = new RelayCommand(param => LikeExecute(), param => CanLikeExecute());
                }
                return likePost;
            }
        }
        public FeedViewModel(FeedView feedView, vwUser user)
        {
            this.feedView = feedView;
            User = user;
            FriendsPostList = friendsPost.GetFriendsPost(User);
            OtherPostList = friendsPost.GetOtherUsersPost(User);
        }

        public bool CanLikeExecute()
        {
            if (Post != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void LikeExecute()
        {
            try
            {
                friendsPost.LikePost(Post);
                FriendsPostList = friendsPost.GetFriendsPost(User);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}