//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Zadatak_1.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblPost
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblPost()
        {
            this.tblLikedPosts = new HashSet<tblLikedPost>();
        }
    
        public int PostId { get; set; }
        public int UserId { get; set; }
        public string PostContent { get; set; }
        public System.DateTime DateOfPost { get; set; }
        public int NumberOfLikes { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblLikedPost> tblLikedPosts { get; set; }
        public virtual tblUser tblUser { get; set; }
    }
}
