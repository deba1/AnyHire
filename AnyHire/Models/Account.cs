//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AnyHire.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Account
    {
        public Account()
        {
            this.Notices = new HashSet<Notice>();
            this.Notices1 = new HashSet<Notice>();
        }
    
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int Mobile { get; set; }
        public string Gender { get; set; }
        public System.DateTime DateOfBirth { get; set; }
        public string Address { get; set; }
        public string ProfilePicture { get; set; }
        public int UserType { get; set; }
        public Nullable<int> CustomerId { get; set; }
        public Nullable<int> ServiceProviderId { get; set; }
    
        public virtual Customer Customer { get; set; }
        public virtual ServiceProvider ServiceProvider { get; set; }
        public virtual UserType UserType1 { get; set; }
        public virtual ICollection<Notice> Notices { get; set; }
        public virtual ICollection<Notice> Notices1 { get; set; }
    }
}