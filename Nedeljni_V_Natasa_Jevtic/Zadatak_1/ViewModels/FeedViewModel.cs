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

        private ICommand viewFriendsLikes;

        public ICommand ViewFriendsLikes
        {
            get
            {
                if (viewFriendsLikes == null)
                {
                    viewFriendsLikes = new RelayCommand(param => ViewFriendsLikesExecute(), param => CanViewFriendsLikesExecute());
                }
                return viewFriendsLikes;
            }
        }

        private ICommand viewOtherUsersLikes;

        public ICommand ViewOtherUsersLikes
        {
            get
            {
                if (viewOtherUsersLikes == null)
                {
                    viewOtherUsersLikes = new RelayCommand(param => ViewOtherUsersLikesExecute(), param => CanViewOtherUsersLikesExecute());
                }
                return viewOtherUsersLikes;
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
                if (friendsPost.IsUserLikedPost(Post,User))
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

        public void LikeExecute()
        {
            try
            {
                friendsPost.LikePost(Post, User);
                FriendsPostList = friendsPost.GetFriendsPost(User);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public bool CanViewFriendsLikesExecute()
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

        public void ViewFriendsLikesExecute()
        {
            try
            {
                FriendsLikesView friendsLikes = new FriendsLikesView(Post);
                friendsLikes.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public bool CanViewOtherUsersLikesExecute()
        {
            if (PostOfNotFriend != null)
            {
                if (PostOfNotFriend.NumberOfLikes > 0)
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

        public void ViewOtherUsersLikesExecute()
        {
            try
            {
                OtherLikesView otherLikes = new OtherLikesView(PostOfNotFriend);
                otherLikes.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}