using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zadatak_1.Models;
using Zadatak_1.Views;

namespace Zadatak_1.ViewModels
{
    class FriendsLikesViewModel : BaseViewModel
    {
        FriendsLikesView friendsLikesView;
        FriendsPost posts = new FriendsPost();

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

        private List<vwUser> userList;

        public List<vwUser> UserList
        {
            get
            {
                return userList;
            }
            set
            {
                userList = value;
                OnPropertyChanged("UserList");
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

        public FriendsLikesViewModel(FriendsLikesView friendsLikesView, vwFriendPost post)
        {
            this.friendsLikesView = friendsLikesView;
            Post = post;
            UserList = posts.UsersWhoLikedPost(Post);
        }
    }
}
