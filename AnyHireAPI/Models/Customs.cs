using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AnyHireAPI.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Name { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }
        public string Mobile { get; set; }

        public string Gender { get; set; }

        public string Address { get; set; }

        public string DateOfBirth { get; set; }

        public string ProfilePicture { get; set; }

        public int UserType { get; set; }

        public List<Links> Links { get; set; }
    }

    public class Consumer : User
    {
        public Consumer()
        {

        }
        public Consumer(Account account)
        {
            Id = account.Id;
            Username = account.Username;
            Password = account.Password;
            Name = account.Name;
            Email = account.Email;
            Mobile = "+880" + account.Mobile;
            Gender = account.Gender;
            Address = account.Address;
            DateOfBirth = account.DateOfBirth.ToString("dd/MM/yyyy");
            NID = account.Customer.NID;
            UserType = account.UserTypeId;
        }
        public int NID { get; set; }

        public List<Links> CreateLinks(string baseurl, string current)
        {
            var links = new List<Links>();
            links.Add(new Links()
            {
                Href = baseurl + "/api/customers/",
                Method = "GET",
                Rel = (current == "GetAll") ? "Self" : "All Resources"
            });
            links.Add(new Links() { 
                Href = baseurl+"/api/customers/"+this.Id,
                Method = "GET",
                Rel = (current == "GetOne") ? "Self" : "Specific Resource"
            });
            links.Add(new Links()
            {
                Href = baseurl + "/api/customers/",
                Method = "POST",
                Rel = (current == "Add") ? "Self" : "Create Resource"
            });
            links.Add(new Links()
            {
                Href = baseurl + "/api/customers/" + this.Id,
                Method = "PUT",
                Rel = (current == "Edit") ? "Self" : "Edit Resource"
            });
            links.Add(new Links()
            {
                Href = baseurl + "/api/customers/" + this.Id,
                Method = "DELETE",
                Rel = "Delete Resource"
            });
            return links;
        }
    }

    public class Provider : User
    {
        public int NID { get; set; }

        public string Coverage { get; set; }

        public DateTime JoinDate { get; set; }

        public string Skill { get; set; }

        public double Revenue { get; set; }

        public double Commission { get; set; }

    }


}