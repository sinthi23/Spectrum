using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using SpectrumWebForms.Models;

namespace SpectrumWebForms
{
    public partial class RegistrationPage : Page
    {
        protected string CurrentEventId;
        protected EventInfo CurrentEvent;

        protected void Page_Load(object sender, EventArgs e)
        {
            CurrentEventId = Request.QueryString["event"] ?? "ignite";
            CurrentEvent = EventCatalog.GetEvent(CurrentEventId);

            PageTitleLiteral.Text = HttpUtility.HtmlEncode(CurrentEvent.Title);
            EventTitleLiteral.Text = HttpUtility.HtmlEncode(CurrentEvent.Title);
            EventFormatLabelLiteral.Text = HttpUtility.HtmlEncode(CurrentEvent.Format);
            EligibilityLabelLiteral.Text = HttpUtility.HtmlEncode(CurrentEvent.Eligibility);
            InstitutionWrap.Visible = CurrentEvent.IsInterUniversity;

            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(CurrentEventId))
                {
                    ViewState["CurrentEventId"] = CurrentEventId;
                }
            }
        }

        protected void SubmitRegistration_Click(object sender, EventArgs e)
        {
            var errors = new StringBuilder();

            var fullName = FullNameTextBox.Text.Trim();
            var email = EmailTextBox.Text.Trim();
            var department = DepartmentTextBox.Text.Trim();
            var academicYear = AcademicYearDropDown.SelectedValue;
            var motivation = MotivationTextBox.Text.Trim();
            var institutionName = InstitutionNameTextBox.Text.Trim();
            var paymentMethod = PaymentMethodDropDown.SelectedValue;
            var paymentReference = PaymentReferenceTextBox.Text.Trim();

            if (fullName.Length < 2)
            {
                AddError(errors, "Full Name is required.");
            }

            if (email.Length == 0)
            {
                AddError(errors, "Email is required.");
            }
            else if (!Regex.IsMatch(email, "^[^\\s@]+@[^\\s@]+\\.[^\\s@]+$"))
            {
                AddError(errors, "Email address is not valid.");
            }

            if (department.Length < 2)
            {
                AddError(errors, "Department is required.");
            }

            if (academicYear.Length == 0)
            {
                AddError(errors, "Academic Year is required.");
            }

            if (motivation.Length < 10)
            {
                AddError(errors, "Motivation is required.");
            }

            if (CurrentEvent.IsInterUniversity && institutionName.Length < 2)
            {
                AddError(errors, "Institution Name is required for inter-university events.");
            }

            if (paymentMethod.Length == 0)
            {
                AddError(errors, "Payment Method is required.");
            }

            if (paymentMethod != "Cash (On Campus)" && paymentReference.Length < 6)
            {
                AddError(errors, "A valid Payment Transaction ID is required for digital payment methods.");
            }

            if (errors.Length > 0)
            {
                SuccessPanel.Visible = false;
                ErrorPanel.Visible = true;
                ErrorMessageLiteral.Text = BuildErrorList(errors.ToString().Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries));
                return;
            }

            var registrationId = "SPC-" + Guid.NewGuid().ToString("N").Substring(0, 6).ToUpperInvariant();
            var submittedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            RegistrationIdLiteral.Text = HttpUtility.HtmlEncode(registrationId);
            SubmittedAtLiteral.Text = HttpUtility.HtmlEncode(submittedAt);
            EventLiteral.Text = HttpUtility.HtmlEncode(CurrentEvent.Title);
            EventFormatLiteral.Text = HttpUtility.HtmlEncode(CurrentEvent.Format);
            FullNameLiteral.Text = HttpUtility.HtmlEncode(fullName);
            EmailLiteral.Text = HttpUtility.HtmlEncode(email);
            InstitutionLiteral.Text = HttpUtility.HtmlEncode(string.IsNullOrEmpty(institutionName) ? "N/A" : institutionName);
            DepartmentLiteral.Text = HttpUtility.HtmlEncode(department);
            AcademicYearLiteral.Text = HttpUtility.HtmlEncode(academicYear);
            PaymentMethodLiteral.Text = HttpUtility.HtmlEncode(paymentMethod);
            PaymentReferenceLiteral.Text = HttpUtility.HtmlEncode(string.IsNullOrEmpty(paymentReference) ? "Cash payment selected" : paymentReference);
            MotivationLiteral.Text = HttpUtility.HtmlEncode(motivation).Replace("\r\n", "<br />").Replace("\n", "<br />");

            ErrorPanel.Visible = false;
            SuccessPanel.Visible = true;
        }

        private static void AddError(StringBuilder errors, string message)
        {
            if (errors.Length > 0)
            {
                errors.Append('\n');
            }

            errors.Append(message);
        }

        private static string BuildErrorList(string[] errors)
        {
            var builder = new StringBuilder();
            builder.Append("<h2>Registration Failed</h2><ul>");

            foreach (var error in errors)
            {
                if (string.IsNullOrWhiteSpace(error))
                {
                    continue;
                }

                builder.Append("<li>")
                    .Append(HttpUtility.HtmlEncode(error))
                    .Append("</li>");
            }

            builder.Append("</ul>");
            return builder.ToString();
        }
    }
}