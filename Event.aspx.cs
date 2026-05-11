using System;
using System.Text;
using System.Web;
using System.Web.UI;
using SpectrumWebForms.Models;

namespace SpectrumWebForms
{
    public partial class EventDetailsPage : Page
    {
        protected string CurrentEventId;
        protected EventInfo CurrentEvent;

        protected void Page_Load(object sender, EventArgs e)
        {
            CurrentEventId = Request.QueryString["event"] ?? "ignite";
            CurrentEvent = EventCatalog.GetEvent(CurrentEventId);

            PageTitleLiteral.Text = HttpUtility.HtmlEncode(CurrentEvent.Title);
            DateLiteral.Text = HttpUtility.HtmlEncode(CurrentEvent.Date);
            TitleLiteral.Text = HttpUtility.HtmlEncode(CurrentEvent.Title);
            TaglineLiteral.Text = HttpUtility.HtmlEncode(CurrentEvent.Tagline);
            SummaryLiteral.Text = HttpUtility.HtmlEncode(CurrentEvent.Summary);
            FormatLiteral.Text = HttpUtility.HtmlEncode(CurrentEvent.Format);
            EligibilityLiteral.Text = HttpUtility.HtmlEncode(CurrentEvent.Eligibility);
            FeeLiteral.Text = HttpUtility.HtmlEncode(CurrentEvent.Fee);
            PaymentNoteLiteral.Text = HttpUtility.HtmlEncode(CurrentEvent.PaymentNote);
            GuidelinesLiteral.Text = BuildGuidelinesMarkup(CurrentEvent.Guidelines);
        }

        private static string BuildGuidelinesMarkup(string[] guidelines)
        {
            var builder = new StringBuilder();

            foreach (var guideline in guidelines)
            {
                builder.Append("<li>")
                    .Append(HttpUtility.HtmlEncode(guideline))
                    .Append("</li>");
            }

            return builder.ToString();
        }
    }
}