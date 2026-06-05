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

            // Set new detailed fields
            if (!string.IsNullOrEmpty(CurrentEvent.DetailedDescription))
            {
                DetailedDescriptionPanel.Visible = true;
                DetailedDescriptionLiteral.Text = HttpUtility.HtmlEncode(CurrentEvent.DetailedDescription);
            }

            if (CurrentEvent.Highlights != null && CurrentEvent.Highlights.Length > 0)
            {
                HighlightsPanel.Visible = true;
                HighlightsLiteral.Text = BuildGuidelinesMarkup(CurrentEvent.Highlights);
            }

            if (!string.IsNullOrEmpty(CurrentEvent.Sponsor))
            {
                SponsorPanel.Visible = true;
                SponsorLiteral.Text = HttpUtility.HtmlEncode(CurrentEvent.Sponsor);
            }

            if (!string.IsNullOrEmpty(CurrentEvent.Winner))
            {
                WinnersPanel.Visible = true;
                WinnerLiteral.Text = HttpUtility.HtmlEncode(CurrentEvent.Winner);
                
                if (!string.IsNullOrEmpty(CurrentEvent.RunnerUpFirst))
                {
                    RunnerUpFirstLiteral.Text = HttpUtility.HtmlEncode(CurrentEvent.RunnerUpFirst);
                }

                if (!string.IsNullOrEmpty(CurrentEvent.RunnerUpSecond))
                {
                    RunnerUpSecondLiteral.Text = HttpUtility.HtmlEncode(CurrentEvent.RunnerUpSecond);
                }
            }

            if (CurrentEvent.ParticipantCount.HasValue)
            {
                ParticipantCountPanel.Visible = true;
                ParticipantCountLiteral.Text = CurrentEvent.ParticipantCount.Value.ToString("N0");
            }
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