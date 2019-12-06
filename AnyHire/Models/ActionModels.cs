using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AnyHire.Models
{
    public partial class Account
    {
        public bool IsAdmin()
        {
            return this.UserType == 1;
        }

        public bool IsCustomer()
        {
            return this.UserType == 2;
        }

        public bool IsServiceProvider()
        {
            return this.UserType == 3;
        }
    }

}