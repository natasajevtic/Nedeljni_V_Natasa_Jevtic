using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zadatak_1.Models;
using Zadatak_1.Views;

namespace Zadatak_1.ViewModels
{
    class OtherLikesViewModel : BaseViewModel
    {
        OtherLikesView otherLikesView;
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

        public OtherLikesViewModel(OtherLikesView otherLikesView, vwPost post)
        {
            this.otherLikesView = otherLikesView;
            Post = post;
            UserList = posts.UsersWhoLikedPost(Post);
        }
    }
}
