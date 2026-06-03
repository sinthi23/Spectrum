using System;
using System.Web.UI;

namespace SpectrumWebForms
{
    public class AdminBasePage : Page
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            // Kill cache on every admin page
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetNoStore();
            Response.Cache.SetExpires(DateTime.UtcNow.AddDays(-1));

            if (!AuthSession.IsAuthenticated)
            {
                Response.Redirect("~/Login.aspx?returnUrl=" + 
                    Server.UrlEncode(Request.RawUrl), true);
            }
        }
    }
}