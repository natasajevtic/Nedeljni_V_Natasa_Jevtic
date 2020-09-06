using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Zadatak_1.Helper;

namespace Zadatak_1.Models
{
    class Users
    {
        /// <summary>
        /// This methods finds user based on forwarded username and password.
        /// </summary>
        /// <param name="username">User username.</param>
        /// <param name="password">User password.</param>
        /// <returns>User if finded, null if not.</returns>
        public vwUser FindUser(string username, string password)
        {
            string encryptedPassword = Encryption.EncryptPassword(password);
            try
            {
                using (BetweenUsEntities context = new BetweenUsEntities())
                {
                    return context.vwUsers.Where(x => x.Username == username && x.Password == encryptedPassword).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }
        /// <summary>
        /// This method creates a list of data from view of all users.
        /// </summary>
        /// <returns>List of users.</returns>
        public List<vwUser> GetAllUsers()
        {
            try
            {
                using (BetweenUsEntities context = new BetweenUsEntities())
                {
                    return context.vwUsers.ToList();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }
        /// <summary>
        /// This method creates a list of data from table of all users.
        /// </summary>
        /// <returns>List of users.</returns>
        public List<tblUser> GetUsers()
        {
            try
            {
                using (BetweenUsEntities context = new BetweenUsEntities())
                {
                    return context.tblUsers.ToList();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }
        /// <summary>
        /// This method adds user to DbSet and saves changes to database.
        /// </summary>
        /// <param name="userToAdd">User to add.</param>
        /// <param name="id">User id.</param>
        /// <returns>True if added, false if not.</returns>
        public bool CreateUser(vwUser userToAdd, out int id)
        {
            try
            {
                using (BetweenUsEntities context = new BetweenUsEntities())
                {
                    tblUser newUser = new tblUser
                    {
                        DateOfBirth = userToAdd.DateOfBirth,
                        FirstName = userToAdd.FirstName,
                        LastName = userToAdd.LastName,
                        Gender = userToAdd.Gender,
                        Location = userToAdd.Location,
                        MarriageStatus = userToAdd.MarriageStatus,
                        Password = Encryption.EncryptPassword(userToAdd.Password),
                        PhoneNumber = userToAdd.PhoneNumber,
                        Profession = userToAdd.Profession,
                        Username = userToAdd.Username
                    };
                    context.tblUsers.Add(newUser);
                    context.SaveChanges();
                    id = newUser.UserId;
                    return true;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
                id = 0;
                return false;
            }
        }
        /// <summary>
        /// This method creates a list of users which are not friends.
        /// </summary>
        /// <param name="user">User to check.</param>
        /// <returns>List of users.</returns>
        public List<vwUser> UsersNotInFriends(vwUser user)
        {
            try
            {
                using (BetweenUsEntities context = new BetweenUsEntities())
                {
                    //find all users in relationship with this user
                    List<int> recipient = context.vwRequests.Where(x => x.SenderId == user.UserId).Select(x => x.RecipientId).ToList();
                    List<int> sender = context.vwRequests.Where(x => x.RecipientId == user.UserId).Select(x => x.SenderId).ToList();
                    List<vwUser> users = new List<vwUser>();
                    foreach (var item in recipient)
                    {
                        var userToAdd = context.vwUsers.Where(x => x.UserId == item).FirstOrDefault();
                        if (userToAdd != null)
                        {
                            users.Add(userToAdd);
                        }
                    }
                    foreach (var item in sender)
                    {
                        var userToAdd = context.vwUsers.Where(x => x.UserId == item).FirstOrDefault();
                        if (userToAdd != null)
                        {
                            users.Add(userToAdd);
                        }
                    }
                    List<vwUser> distinct = users.Distinct().ToList();
                    //removing friends from list of all users
                    List<vwUser> listOfAllUser = context.vwUsers.ToList();
                    foreach (var item in distinct)
                    {
                        if (listOfAllUser.Contains(item))
                        {
                            listOfAllUser.Remove(item);
                        }
                    }
                    return listOfAllUser.Where(x => x.UserId != user.UserId).ToList();
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

