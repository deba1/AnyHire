using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AnyHireAPI.Models
{
    public partial class Package
    {
        public List<Links> Links = new List<Links>();

        public Package Clone()
        {
            return new Package() {
                Id = this.Id,
                Description = this.Description,
                ImagePath = this.ImagePath,
                Price = this.Price,
                ServiceId = this.ServiceId,
                ServiceProviderId = this.ServiceProviderId,
                Title = this.Title
            };
        }
        public List<Links> CreateLinks(string baseurl, string current)
        {
            var links = new List<Links>();
            links.Add(new Links()
            {
                Href = baseurl + "/api/providers/" + this.ServiceId + "/packages/",
                Method = "GET",
                Rel = (current == "GetAll") ? "Self" : "All Resources"
            });
            links.Add(new Links()
            {
                Href = baseurl + "/api/providers/" + this.ServiceId + "/packages/" + this.Id,
                Method = "GET",
                Rel = (current == "GetOne") ? "Self" : "Specific Resource"
            });
            links.Add(new Links()
            {
                Href = baseurl + "/api/providers/" + this.ServiceId + "/packages/",
                Method = "POST",
                Rel = (current == "Add") ? "Self" : "Create Resource"
            });
            links.Add(new Links()
            {
                Href = baseurl + "/api/providers/" + this.ServiceId + "/packages/" + this.Id,
                Method = "PUT",
                Rel = (current == "Edit") ? "Self" : "Edit Resource"
            });
            links.Add(new Links()
            {
                Href = baseurl + "/api/providers/" + this.ServiceId + "/packages/" + this.Id,
                Method = "DELETE",
                Rel = "Delete Resource"
            });
            return links;
        }
    }

    public partial class Service
    {
        public List<Links> Links = new List<Links>();

        public Service Clone()
        {
            return new Service()
            {
                Id = this.Id,
                ImagePath = this.ImagePath,
                Links = this.Links,
                Name = this.Name
            };
        }
        
        public List<Links> CreateLinks(string baseurl, string current)
        {
            var links = new List<Links>();
            links.Add(new Links()
            {
                Href = baseurl + "/api/services/",
                Method = "GET",
                Rel = (current == "GetAll") ? "Self" : "All Resources"
            });
            links.Add(new Links()
            {
                Href = baseurl + "/api/services/" + this.Id,
                Method = "GET",
                Rel = (current == "GetOne") ? "Self" : "Specific Resource"
            });
            links.Add(new Links()
            {
                Href = baseurl + "/api/services/",
                Method = "POST",
                Rel = (current == "Add") ? "Self" : "Create Resource"
            });
            links.Add(new Links()
            {
                Href = baseurl + "/api/services/" + this.Id,
                Method = "PUT",
                Rel = (current == "Edit") ? "Self" : "Edit Resource"
            });
            links.Add(new Links()
            {
                Href = baseurl + "/api/services/" + this.Id,
                Method = "DELETE",
                Rel = "Delete Resource"
            });
            return links;
        }
    }

    public partial class Transaction
    {
        public List<Links> Links = new List<Links>();

        public Transaction Clone()
        {
            return new Transaction() {
                Id = this.Id,
                AppointmentId = this.AppointmentId,
                CompanyRevenue = this.CompanyRevenue,
                Links = this.Links,
                ServiceProviderId = this.ServiceProviderId,
                ServiceProviderRevenue = this.ServiceProviderRevenue,
                TotalAmount = this.TotalAmount
            };
        }
        public List<Links> CreateLinks(string baseurl, string current)
        {
            var links = new List<Links>();
            links.Add(new Links()
            {
                Href = baseurl + "/api/transactions/",
                Method = "GET",
                Rel = (current == "GetAll") ? "Self" : "All Resources"
            });
            links.Add(new Links()
            {
                Href = baseurl + "/api/transactions/" + this.Id,
                Method = "GET",
                Rel = (current == "GetOne") ? "Self" : "Specific Resource"
            });
            links.Add(new Links()
            {
                Href = baseurl + "/api/transactions/",
                Method = "POST",
                Rel = (current == "Add") ? "Self" : "Create Resource"
            });
            links.Add(new Links()
            {
                Href = baseurl + "/api/transactions/" + this.Id,
                Method = "PUT",
                Rel = (current == "Edit") ? "Self" : "Edit Resource"
            });
            links.Add(new Links()
            {
                Href = baseurl + "/api/transactions/" + this.Id,
                Method = "DELETE",
                Rel = "Delete Resource"
            });
            return links;
        }
    }

    public partial class Appointment
    {
        public List<Links> Links = new List<Links>();

        public Appointment Clone()
        {
            return new Appointment()
            {
                Id = this.Id,
                Completed = this.Completed,
                CustomerId = this.CustomerId,
                Links = this.Links,
                Location = this.Location,
                PackageId = this.PackageId,
                Time = this.Time
            };
        }
        public List<Links> CreateLinks(string baseurl, string current)
        {
            var links = new List<Links>();
            links.Add(new Links()
            {
                Href = baseurl + "/api/services/" + this.Package.ServiceId + "/packages/" + this.PackageId + "/appointments",
                Method = "GET",
                Rel = (current == "GetAll") ? "Self" : "All Resources"
            });
            links.Add(new Links()
            {
                Href = baseurl + "/api/services/" + this.Package.ServiceId + "/packages/" + this.PackageId + "/appointments/" + this.Id,
                Method = "GET",
                Rel = (current == "GetOne") ? "Self" : "Specific Resource"
            });
            links.Add(new Links()
            {
                Href = baseurl + "/api/services/" + this.Package.ServiceId + "/packages/" + this.PackageId + "/appointments",
                Method = "POST",
                Rel = (current == "Add") ? "Self" : "Create Resource"
            });
            links.Add(new Links()
            {
                Href = baseurl + "/api/services/" + this.Package.ServiceId + "/packages/" + this.PackageId + "/appointments/" + this.Id,
                Method = "PUT",
                Rel = (current == "Edit") ? "Self" : "Edit Resource"
            });
            links.Add(new Links()
            {
                Href = baseurl + "/api/services/" + this.Package.ServiceId + "/packages/" + this.PackageId + "/appointments/" + this.Id,
                Method = "DELETE",
                Rel = "Delete Resource"
            });
            return links;
        }
    }
}