using System.Collections.Generic;

namespace Zadatak_1.Models
{
    class RelationshipStatus
    {
        /// <summary>
        /// This method created list of relationship status.
        /// </summary>
        /// <returns>List of relationship status.</returns>
        public List<string> GetRealtionshipStatus()
        {
            return new List<string> { "Single", "In relationship", "Married", "Divorced" };
        }
    }
}
