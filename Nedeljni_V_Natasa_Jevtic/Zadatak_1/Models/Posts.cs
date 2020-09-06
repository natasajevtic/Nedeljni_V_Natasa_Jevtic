using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Zadatak_1.Models
{
    class Posts
    {
        /// <summary>
        /// This method adds post to DbSet and saves changes to database.
        /// </summary>
        /// <param name="postToAdd">Post to be added.</param>
        /// <returns>True if post is added, false if not.</returns>
        public bool CreatePost(vwPost postToAdd)
        {
            try
            {
                using (BetweenUsEntities context = new BetweenUsEntities())
                {
                    tblPost newPost = new tblPost
                    {
                        DateOfPost = DateTime.Now.Date,
                        PostContent = postToAdd.PostContent,
                        UserId = postToAdd.UserId,
                        NumberOfLikes = 0
                    };
                    context.tblPosts.Add(newPost);
                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
                return false;
            }
        }
        /// <summary>
        /// This method creates a list of user posts.
        /// </summary>
        /// <param name="user">User.</param>
        /// <returns>List of posts.</returns>
        public List<vwPost> GetUserPosts(vwUser user)
        {
            try
            {
                using (BetweenUsEntities context = new BetweenUsEntities())
                {
                    return context.vwPosts.Where(x => x.UserId == user.UserId).ToList();
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
