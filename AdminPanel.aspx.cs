using System;
using System.Globalization;
using System.Web.UI.WebControls;
using SpectrumWebForms.Data;
using SpectrumWebForms.Models;

namespace SpectrumWebForms
{
    public partial class AdminPanelPage : AdminPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                AdminNameLiteral.Text = AuthSession.FullName ?? AuthSession.UserName ?? "Admin";
                BindDashboard();
                ClearMemberForm();
                ClearEventForm();
            }
        }

        protected void MemberSaveButton_Click(object sender, EventArgs e)
        {
            var member = new ClubMember
            {
                FullName = MemberFullNameTextBox.Text.Trim(),
                Position = MemberPositionTextBox.Text.Trim(),
                Department = MemberDepartmentTextBox.Text.Trim(),
                Email = MemberEmailTextBox.Text.Trim(),
                Phone = MemberPhoneTextBox.Text.Trim(),
                Bio = MemberBioTextBox.Text.Trim(),
                PhotoUrl = MemberPhotoUrlTextBox.Text.Trim(),
                IsActive = MemberActiveCheckBox.Checked
            };

            if (string.IsNullOrWhiteSpace(member.FullName) || string.IsNullOrWhiteSpace(member.Position))
            {
                ShowMessage("Member full name and position are required.");
                return;
            }

            var memberId = ToInt(MemberIdHiddenField.Value);
            if (memberId > 0)
            {
                member.MemberId = memberId;
                ClubMemberRepository.Update(member);
                ShowMessage("Member updated successfully.");
            }
            else
            {
                ClubMemberRepository.Insert(member);
                ShowMessage("Member added successfully.");
            }

            ClearMemberForm();
            BindDashboard();
        }

        protected void MemberClearButton_Click(object sender, EventArgs e)
        {
            ClearMemberForm();
        }

        protected void MembersGridView_SelectedIndexChanged(object sender, EventArgs e)
        {
            var memberId = ToInt(MembersGridView.SelectedDataKey.Value);
            var member = ClubMemberRepository.GetById(memberId);
            if (member == null)
            {
                return;
            }

            MemberIdHiddenField.Value = member.MemberId.ToString(CultureInfo.InvariantCulture);
            MemberFullNameTextBox.Text = member.FullName;
            MemberPositionTextBox.Text = member.Position;
            MemberDepartmentTextBox.Text = member.Department;
            MemberEmailTextBox.Text = member.Email;
            MemberPhoneTextBox.Text = member.Phone;
            MemberBioTextBox.Text = member.Bio;
            MemberPhotoUrlTextBox.Text = member.PhotoUrl;
            MemberActiveCheckBox.Checked = member.IsActive;
        }

        protected void MembersGridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            var memberId = ToInt(MembersGridView.DataKeys[e.RowIndex].Value);
            ClubMemberRepository.Delete(memberId);
            ShowMessage("Member deleted successfully.");
            ClearMemberForm();
            BindDashboard();
        }

        protected void EventSaveButton_Click(object sender, EventArgs e)
        {
            DateTime eventDate;
            if (!DateTime.TryParse(EventDateTextBox.Text, out eventDate))
            {
                ShowMessage("A valid event date is required.");
                return;
            }

            var eventInfo = new EventInfo
            {
                Slug = EventSlugTextBox.Text.Trim(),
                Title = EventTitleTextBox.Text.Trim(),
                Date = eventDate.ToString("MMMM dd, yyyy", CultureInfo.InvariantCulture),
                EventDate = eventDate,
                Venue = EventVenueTextBox.Text.Trim(),
                Format = EventFormatTextBox.Text.Trim(),
                Fee = EventFeeTextBox.Text.Trim(),
                Tagline = EventTaglineTextBox.Text.Trim(),
                Summary = EventSummaryTextBox.Text.Trim(),
                Eligibility = EventEligibilityTextBox.Text.Trim(),
                PaymentNote = EventPaymentNoteTextBox.Text.Trim(),
                Guidelines = SplitGuidelines(EventGuidelinesTextBox.Text),
                BackgroundUrl = EventBackgroundUrlTextBox.Text.Trim(),
                IsUpcoming = EventUpcomingCheckBox.Checked,
                IsActive = EventActiveCheckBox.Checked
            };

            if (string.IsNullOrWhiteSpace(eventInfo.Slug) || string.IsNullOrWhiteSpace(eventInfo.Title) || string.IsNullOrWhiteSpace(eventInfo.Format))
            {
                ShowMessage("Event slug, title, and format are required.");
                return;
            }

            var eventId = ToInt(EventIdHiddenField.Value);
            if (eventId > 0)
            {
                eventInfo.EventId = eventId;
                EventRepository.Update(eventInfo);
                ShowMessage("Event updated successfully.");
            }
            else
            {
                EventRepository.Insert(eventInfo);
                ShowMessage("Event added successfully.");
            }

            ClearEventForm();
            BindDashboard();
        }

        protected void EventClearButton_Click(object sender, EventArgs e)
        {
            ClearEventForm();
        }

        protected void EventsGridView_SelectedIndexChanged(object sender, EventArgs e)
        {
            var eventId = ToInt(EventsGridView.SelectedDataKey.Value);
            var eventInfo = EventRepository.GetById(eventId);
            if (eventInfo == null)
            {
                return;
            }

            EventIdHiddenField.Value = eventInfo.EventId.ToString(CultureInfo.InvariantCulture);
            EventSlugTextBox.Text = eventInfo.Slug;
            EventTitleTextBox.Text = eventInfo.Title;
            EventDateTextBox.Text = eventInfo.EventDate.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
            EventVenueTextBox.Text = eventInfo.Venue;
            EventFormatTextBox.Text = eventInfo.Format;
            EventFeeTextBox.Text = eventInfo.Fee;
            EventTaglineTextBox.Text = eventInfo.Tagline;
            EventSummaryTextBox.Text = eventInfo.Summary;
            EventEligibilityTextBox.Text = eventInfo.Eligibility;
            EventPaymentNoteTextBox.Text = eventInfo.PaymentNote;
            EventGuidelinesTextBox.Text = string.Join(Environment.NewLine, eventInfo.Guidelines ?? new string[0]);
            EventBackgroundUrlTextBox.Text = eventInfo.BackgroundUrl;
            EventUpcomingCheckBox.Checked = eventInfo.IsUpcoming;
            EventActiveCheckBox.Checked = eventInfo.IsActive;
        }

        protected void EventsGridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            var eventId = ToInt(EventsGridView.DataKeys[e.RowIndex].Value);
            EventRepository.Delete(eventId);
            ShowMessage("Event deleted successfully.");
            ClearEventForm();
            BindDashboard();
        }

        private void BindDashboard()
        {
            var members = ClubMemberRepository.GetAll();
            var events = EventRepository.GetAll();
            var upcoming = EventCatalog.GetUpcomingEvents();

            MemberCountLiteral.Text = members.Count.ToString(CultureInfo.InvariantCulture);
            EventCountLiteral.Text = events.Count.ToString(CultureInfo.InvariantCulture);
            UpcomingCountLiteral.Text = upcoming.Count.ToString(CultureInfo.InvariantCulture);

            MembersGridView.DataSource = members;
            MembersGridView.DataBind();

            EventsGridView.DataSource = events;
            EventsGridView.DataBind();
        }

        private void ClearMemberForm()
        {
            MemberIdHiddenField.Value = "0";
            MemberFullNameTextBox.Text = string.Empty;
            MemberPositionTextBox.Text = string.Empty;
            MemberDepartmentTextBox.Text = string.Empty;
            MemberEmailTextBox.Text = string.Empty;
            MemberPhoneTextBox.Text = string.Empty;
            MemberBioTextBox.Text = string.Empty;
            MemberPhotoUrlTextBox.Text = string.Empty;
            MemberActiveCheckBox.Checked = true;
        }

        private void ClearEventForm()
        {
            EventIdHiddenField.Value = "0";
            EventSlugTextBox.Text = string.Empty;
            EventTitleTextBox.Text = string.Empty;
            EventDateTextBox.Text = string.Empty;
            EventVenueTextBox.Text = string.Empty;
            EventFormatTextBox.Text = string.Empty;
            EventFeeTextBox.Text = string.Empty;
            EventTaglineTextBox.Text = string.Empty;
            EventSummaryTextBox.Text = string.Empty;
            EventEligibilityTextBox.Text = string.Empty;
            EventPaymentNoteTextBox.Text = string.Empty;
            EventGuidelinesTextBox.Text = string.Empty;
            EventBackgroundUrlTextBox.Text = string.Empty;
            EventUpcomingCheckBox.Checked = true;
            EventActiveCheckBox.Checked = true;
        }

        private void ShowMessage(string message)
        {
            MessagePanel.Visible = true;
            MessageLiteral.Text = message;
        }

        private static int ToInt(object value)
        {
            if (value == null)
            {
                return 0;
            }

            return Convert.ToInt32(value);
        }

        private static string[] SplitGuidelines(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                return new string[0];
            }

            return text.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}
