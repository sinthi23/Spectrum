using System;
using System.Web;
using System.Web.UI;

namespace SpectrumWebForms
{
    public partial class SiteMaster : MasterPage
    {
        public bool IsAdminAuthenticated { get; private set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            IsAdminAuthenticated = AuthSession.IsAuthenticated;
            
            // Prevent caching on master page as well
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetNoStore();
            Response.AppendHeader("Pragma", "no-cache");
            Response.AppendHeader("Cache-Control", "no-cache, no-store, must-revalidate, max-age=0");
            Response.AppendHeader("Expires", "0");
        }
    }
}