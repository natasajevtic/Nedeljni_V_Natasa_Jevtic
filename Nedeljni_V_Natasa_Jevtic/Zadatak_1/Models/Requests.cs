using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Zadatak_1.Models
{
    class Requests
    {
        /// <summary>
        /// This method adds request to DbSet and saves changes to database.
        /// </summary>
        /// <param name="sender">Sender of request.</param>
        /// <param name="recipient">Recipient of request.</param>
        /// <returns>True if added, false if not.</returns>
        public bool SendRequest(vwUser sender, vwUser recipient)
        {
            try
            {
                using (BetweenUsEntities context = new BetweenUsEntities())
                {
                    tblRequest request = new tblRequest
                    {
                        RecipientId = recipient.UserId,
                        SenderId = sender.UserId,
                        Status = "Pending"
                    };
                    context.tblRequests.Add(request);
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
        /// This method creates a list of user requests.
        /// </summary>
        /// <param name="user">User.</param>
        /// <returns>List of user request.</returns>
        public List<vwRequest> ViewRecipientRequest(vwUser user)
        {
            try
            {
                using (BetweenUsEntities context = new BetweenUsEntities())
                {
                    return context.vwRequests.Where(x => x.RecipientId == user.UserId).ToList();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }
        /// <summary>
        /// This method changes status of request to accepted.
        /// </summary>
        /// <param name="requestToAccept">Request to be accepted.</param>
        /// <returns>True if accepted, false if not.</returns>
        public bool AcceptRequest(vwRequest requestToAccept)
        {
            try
            {
                using (BetweenUsEntities context = new BetweenUsEntities())
                {
                    var request = context.tblRequests.Where(x => x.RequestId == requestToAccept.RequestId).FirstOrDefault();
                    if (request != null)
                    {
                        request.Status = "Accepted";
                        context.SaveChanges();
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
        /// <summary>
        /// This method changes status of request to declined.
        /// </summary>
        /// <param name="requestToDecline">Request to decline.</param>
        /// <returns>True if declined, false if not.</returns>
        public bool DeclineRequest(vwRequest requestToDecline)
        {
            try
            {
                using (BetweenUsEntities context = new BetweenUsEntities())
                {
                    var request = context.tblRequests.Where(x => x.RequestId == requestToDecline.RequestId).FirstOrDefault();
                    if (request != null)
                    {
                        request.Status = "Declined";
                        context.SaveChanges();
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
    }
}
