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
                    return posts.Where(x => x.UserId != user.UserId).ToList();
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
        public void LikePost(vwFriendPost post, vwUser userWhoLikePost)
        {
            try
            {
                using (BetweenUsEntities context = new BetweenUsEntities())
                {
                   int id = context.vwFriendPosts.Where(x => x.PostId == post.PostId).Select(x => x.PostId).FirstOrDefault();
                    tblPost postToLike = context.tblPosts.Where(x => x.PostId == id).FirstOrDefault();
                    if (postToLike != null)
                    {
                        postToLike.NumberOfLikes++;
                        context.SaveChanges();
                    }
                    tblLikedPost likedPost = new tblLikedPost
                    {
                        UserId = userWhoLikePost.UserId,
                        PostId = postToLike.PostId
                    };
                    context.tblLikedPosts.Add(likedPost);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
            }
        }

        public bool IsUserLikedPost(vwFriendPost post, vwUser user)
        {
            try
            {
                using (BetweenUsEntities context = new BetweenUsEntities())
                {
                    tblLikedPost likedPost = context.tblLikedPosts.Where(x => x.PostId == post.PostId && x.UserId == user.UserId).FirstOrDefault();
                    if (likedPost != null)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
                return false;
            }
        }

        public List<vwUser> UsersWhoLikedPost(vwFriendPost post)
        {
            try
            {
                using (BetweenUsEntities context = new BetweenUsEntities())
                {
                    List<int> usersId = context.tblLikedPosts.Where(x => x.PostId == post.PostId).Select(x => x.UserId).ToList();
                    List<vwUser> users = new List<vwUser>();
                    foreach (var item in usersId)
                    {
                        vwUser user = context.vwUsers.Where(x => x.UserId == item).FirstOrDefault();
                        if (user != null)
                        {
                            users.Add(user);
                        }
                    }
                    return users;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }
    }
}
