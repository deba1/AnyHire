using System;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Filters;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading;
using System.Security.Principal;
using System.Web.Http.Controllers;
using AnyHireAPI.Models;

namespace AnyHireAPI.Attributes
{
    public class BasicAuthenticationAttribute : AuthorizationFilterAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            base.OnAuthorization(actionContext);
            if (actionContext.Request.Headers.Authorization == null)
            {
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
            }
            else
            {
                var ctx = new AnyHireDbContext();
                string encodedStr = actionContext.Request.Headers.Authorization.Parameter;
                string decodedStr = Encoding.UTF8.GetString(Convert.FromBase64String(encodedStr));
                string[] splitedData = decodedStr.Split(new Char[] { ':' });
                string username = splitedData[0];
                string password = splitedData[1];
                var user = ctx.Accounts.Where(a=>a.Username == username && a.Password == password).FirstOrDefault();
                if (user != null)
                {
                    Thread.CurrentPrincipal = new GenericPrincipal(new GenericIdentity(username), null);
                }
                else
                {
                    actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
                }

            }

        }
    }
}