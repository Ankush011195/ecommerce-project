using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace Ecommerce
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            // This is where you'd initialize things when the application starts.
        }

        protected void Session_Start(object sender, EventArgs e)
        {
            // This is where you'd start session logic if needed.
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            // This is where you'd handle request initialization if needed.
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {
            // This is where you can authenticate requests.
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            // This is where you'd handle global errors (e.g., logging errors).
        }

        protected void Session_End(object sender, EventArgs e)
        {
            // This is where you'd handle session end logic (if needed).
        }

        protected void Application_End(object sender, EventArgs e)
        {
            // This is where you'd handle cleanup when the application ends.
        }
    }
}
