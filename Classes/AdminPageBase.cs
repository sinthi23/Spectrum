using System;
using System.Web;
using System.Web.UI;
using SpectrumWebForms.Models;

namespace SpectrumWebForms
{
    public abstract class AdminPageBase : Page
    {
        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);

            // Check session validity on EVERY page load
            if (!AuthSession.IsAuthenticated || !AuthSession.ValidateSessionToken())
            {
                // Clear any remaining session data
                AuthSession.Clear();
                
                var returnUrl = Server.UrlEncode(Request.RawUrl ?? "AdminPanel.aspx");
                Response.Redirect("Login.aspx?returnUrl=" + returnUrl, true);
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            // Prevent browser caching of authenticated pages - CRITICAL
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetNoStore();
            Response.AppendHeader("Pragma", "no-cache");
            Response.AppendHeader("Cache-Control", "no-cache, no-store, must-revalidate, max-age=0");
            Response.AppendHeader("Expires", "0");
        }
    }

    public static class AuthSession
    {
        private const string UserIdKey = "AdminUserId";
        private const string UserNameKey = "AdminUserName";
        private const string FullNameKey = "AdminFullName";
        private const string IsAdminKey = "AdminIsAdmin";
        private const string SessionTokenKey = "AdminSessionToken";
        private const string SessionCreatedKey = "AdminSessionCreated";

        public static bool IsAuthenticated
        {
            get { return HttpContext.Current != null && HttpContext.Current.Session[UserIdKey] != null; }
        }

        public static string UserName
        {
            get { return HttpContext.Current == null ? null : HttpContext.Current.Session[UserNameKey] as string; }
        }

        public static string FullName
        {
            get { return HttpContext.Current == null ? null : HttpContext.Current.Session[FullNameKey] as string; }
        }

        public static bool IsAdmin
        {
            get { return HttpContext.Current != null && HttpContext.Current.Session[IsAdminKey] != null && (bool)HttpContext.Current.Session[IsAdminKey]; }
        }

        /// <summary>
        /// Validates that the session token is still valid (prevents back-button access after logout)
        /// </summary>
        public static bool ValidateSessionToken()
        {
            if (HttpContext.Current == null || HttpContext.Current.Session == null)
            {
                return false;
            }

            var token = HttpContext.Current.Session[SessionTokenKey] as string;
            var createdTime = HttpContext.Current.Session[SessionCreatedKey] as DateTime?;

            // If token is missing or session is stale, invalidate
            if (string.IsNullOrEmpty(token) || !createdTime.HasValue)
            {
                return false;
            }

            // Check if session has expired (optional: add timeout validation)
            // For now, token existence is the main validator
            return true;
        }

        public static void Set(AdminUser user)
        {
            var session = HttpContext.Current.Session;
            
            // Generate a unique session token
            var sessionToken = Guid.NewGuid().ToString("N");
            
            session[UserIdKey] = user.UserId;
            session[UserNameKey] = user.Username;
            session[FullNameKey] = user.FullName;
            session[IsAdminKey] = user.IsAdmin;
            session[SessionTokenKey] = sessionToken;
            session[SessionCreatedKey] = DateTime.Now;
        }

        public static void Clear()
        {
            if (HttpContext.Current == null || HttpContext.Current.Session == null)
            {
                return;
            }

            // Clear all session keys
            HttpContext.Current.Session.RemoveAll();
            HttpContext.Current.Session.Clear();
            HttpContext.Current.Session.Abandon();

            // Explicitly clear the session token to prevent any validation
            HttpContext.Current.Session[SessionTokenKey] = null;

            // Clear authentication cookies
            ClearCookie("ASP.NET_SessionId");
            ClearCookie(".ASPXAUTH");
        }

        private static void ClearCookie(string cookieName)
        {
            if (HttpContext.Current == null)
                return;

            if (HttpContext.Current.Request.Cookies[cookieName] != null)
            {
                var cookie = new HttpCookie(cookieName)
                {
                    Expires = DateTime.Now.AddDays(-1),
                    HttpOnly = true,
                    Secure = HttpContext.Current.Request.IsSecureConnection
                };
                HttpContext.Current.Response.Cookies.Add(cookie);
            }
        }
    }
}
