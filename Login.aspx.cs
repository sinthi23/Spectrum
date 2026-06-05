using System;
using System.Web;
using System.Web.UI;
using SpectrumWebForms.Data;
using SpectrumWebForms.Models;

namespace SpectrumWebForms
{
    public partial class LoginPage : Page
    {
        protected string ReturnUrl => Request.QueryString["returnUrl"] ?? "AdminPanel.aspx";

        protected void Page_Load(object sender, EventArgs e)
        {
            // Prevent caching of login page
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetNoStore();
            Response.AppendHeader("Pragma", "no-cache");
            Response.AppendHeader("Cache-Control", "no-cache, no-store, must-revalidate, max-age=0");

            if (!IsPostBack && AuthSession.IsAuthenticated)
            {
                Response.Redirect(ReturnUrl, true);
            }

            if (!IsPostBack && string.Equals(Request.QueryString["registered"], "1", StringComparison.OrdinalIgnoreCase))
            {
                SuccessPanel.Visible = true;
                SuccessLiteral.Text = "Registration completed. Please log in with your new admin account.";
            }
        }

        protected void LoginButton_Click(object sender, EventArgs e)
        {
            MessagePanel.Visible = false;
            SuccessPanel.Visible = false;

            var username = UsernameTextBox.Text.Trim();
            var password = PasswordTextBox.Text;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                MessagePanel.Visible = true;
                MessageLiteral.Text = "Username and password are required.";
                return;
            }

            var user = AdminAuthService.Authenticate(username, password);
            if (user == null)
            {
                MessagePanel.Visible = true;
                MessageLiteral.Text = "Invalid credentials or inactive admin account.";
                return;
            }

            AuthSession.Set(user);
            Response.Redirect(ReturnUrl, true);
        }
    }
}
