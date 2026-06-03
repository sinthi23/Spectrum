using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Web.UI;
using SpectrumWebForms.Data;
using SpectrumWebForms.Models;

namespace SpectrumWebForms
{
    public partial class AdminRegisterPage : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void RegisterButton_Click(object sender, EventArgs e)
        {
            MessagePanel.Visible = false;
            SuccessPanel.Visible = false;

            var fullName = FullNameTextBox.Text.Trim();
            var email = EmailTextBox.Text.Trim();
            var username = UsernameTextBox.Text.Trim();
            var password = PasswordTextBox.Text;
            var confirmPassword = ConfirmPasswordTextBox.Text;
            var dateOfBirthText = DateOfBirthTextBox.Text.Trim();
            var phone = PhoneTextBox.Text.Trim();
            var inviteCode = InviteCodeTextBox.Text.Trim();

            if (fullName.Length < 2 || email.Length < 5 || username.Length < 4 || password.Length < 8)
            {
                ShowError("Please fill in all required fields with valid values.");
                return;
            }

            if (!string.Equals(password, confirmPassword, StringComparison.Ordinal))
            {
                ShowError("Passwords do not match.");
                return;
            }

            if (!Regex.IsMatch(email, "^[^\\s@]+@[^\\s@]+\\.[^\\s@]+$"))
            {
                ShowError("Please enter a valid email address.");
                return;
            }

            DateTime dateOfBirth;
            if (!DateTime.TryParse(dateOfBirthText, CultureInfo.InvariantCulture, DateTimeStyles.None, out dateOfBirth))
            {
                ShowError("Please enter a valid date of birth.");
                return;
            }

            var request = new AdminRegistrationRequest
            {
                FullName = fullName,
                Email = email,
                Username = username,
                Password = password,
                DateOfBirth = dateOfBirth,
                Phone = phone
            };

            var result = AdminAuthService.RegisterAdmin(request, inviteCode);
            if (!result.IsSuccess)
            {
                ShowError(result.Message);
                return;
            }

            SuccessPanel.Visible = true;
            SuccessLiteral.Text = result.Message + " You can now log in.";
            Response.Redirect("Login.aspx?registered=1", true);
        }

        private void ShowError(string message)
        {
            MessagePanel.Visible = true;
            MessageLiteral.Text = message;
        }
    }
}
