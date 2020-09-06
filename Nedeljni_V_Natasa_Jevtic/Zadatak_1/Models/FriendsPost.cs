using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Zadatak_1.Models
{
    class FriendsPost
    {
        /// <summary>
        /// This method creates a list of user friends posts.
        /// </summary>
        /// <param name="user">User.</param>
        /// <returns>List of friends posts.</returns>
        public List<vwFriendPost> GetFriendsPost(vwUser user)
        {
            try
            {
                using (BetweenUsEntities context = new BetweenUsEntities())
                {
                    return context.vwFriendPosts.Where(x => x.RecipientId == user.UserId || x.SenderId == user.UserId).Where(x => x.UserId != user.UserId).ToList();

                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }
        /// <summary>
        /// This method creates a list of posts of other users which are not friends with forwarded user.
        /// </summary>
        /// <param name="user">User.</param>
        /// <returns>List of posts.</returns>
        public List<vwPost> GetOtherUsersPost(vwUser user)
        {
            try
            {
                using (BetweenUsEntities context = new BetweenUsEntities())
                {
                    var friendPost = GetFriendsPost(user);
                    List<vwPost> postOfFriends = new List<vwPost>();
                    foreach (var item in friendPost)
                    {
                        var post = context.vwPosts.Where(x => x.PostId == item.PostId).FirstOrDefault();
                        if (post != null)
                        {
                            postOfFriends.Add(post);
                        }
                    }
                    List<vwPost> posts = context.vwPosts.ToList();
                    foreach (var item in postOfFriends)
                    {
                        if (posts.Contains(item))
                        {
                            posts.Remove(item);
                        }
                    }
                    return posts;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }
        /// <summary>
        /// This method increment number of likes of post.
        /// </summary>
        /// <param name="post">Post to be liked.</param>
        public void LikePost(vwFriendPost post)
        {
            try
            {
                using (BetweenUsEntities context = new BetweenUsEntities())
                {
                    var postToLike = context.tblPosts.Where(x => x.PostId == post.PostId).FirstOrDefault();
                    if (postToLike != null)
                    {
                        postToLike.NumberOfLikes++;
                        context.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
            }
        }
    }
}
