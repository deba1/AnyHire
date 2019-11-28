using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AnyHire.Models
{   
    public class AdminViewModel
    {
       public  List<Customer> Customers {set;get;};
       public List<ServiceProvider> ServiceProviders{set;get;};
       public double DailyIncome { set; get; }
       public double CompanyDailyIncome{set;get;}



    }
}