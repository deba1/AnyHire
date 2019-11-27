﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AnyHire.Models
{
    public class HomeViewModel
    {
        public IEnumerable<Service> Services { get; set; }
        public IEnumerable<Package> Packages { get; set; }
        public IEnumerable<ServiceProvider> ServicesProviders { get; set; }
    }

    public class AdminViewModel
    {
        public double IncomeDaily { get; set; }
        public double IncomeDailyOrg { get; set; }
        public IEnumerable<Account> Customers { get; set; }
        public IEnumerable<Account> ServiceProvides { get; set; }
    }

    public class ServiceProviderViewModel
    {
        public double IncomeDaily { get; set; }
        public double IncomeMonth { get; set; }

        public int TotalAppointments { get; set; }

        public IEnumerable<Package> Packages { get; set; }
        public IEnumerable<Appointment> Appointments { get; set; }
        public IEnumerable<Transaction> Transactions { get; set; }
    }
}