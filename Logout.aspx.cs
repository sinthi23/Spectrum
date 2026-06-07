using System;
using System.Web;
using System.Web.UI;

namespace SpectrumWebForms
{
    public partial class LogoutPage : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Prevent caching before clearing session - CRITICAL
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetNoStore();
            Response.AppendHeader("Pragma", "no-cache");
            Response.AppendHeader("Cache-Control", "no-cache, no-store, must-revalidate, max-age=0");
            Response.AppendHeader("Expires", "0");

            // Clear the session completely
            AuthSession.Clear();
            
            // Add a marker to prevent back button access
            Session["LoggedOut"] = DateTime.Now;
            
            // Ensure the client cannot go back using browser cache with unique token
            Response.Redirect("Default.aspx?_logout=" + DateTime.Now.Ticks, true);
        }
    }
}
