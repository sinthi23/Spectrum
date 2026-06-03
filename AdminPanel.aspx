<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminPanel.aspx.cs"
Inherits="SpectrumWebForms.AdminPanelPage" MasterPageFile="~/Site.Master" %>
<asp:Content
  ID="TitleContent"
  ContentPlaceHolderID="TitleContent"
  runat="server"
>
  Admin Panel | Spectrum
</asp:Content>
<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
  <main class="admin-shell">
    <div class="admin-layout">
      <aside class="admin-sidebar">
        <section class="admin-hero">
          <span class="admin-badge">
            <i class="fa-solid fa-shield-halved"></i>
            Protected Workspace
          </span>
          <h2>Admin Control Center</h2>
          <p class="admin-hero-summary">
            Manage members, events, and upcoming visibility from a refined,
            organized workspace.
          </p>
          <div class="admin-hero-meta">
            <span>
              Signed in as
              <strong
                ><asp:Literal ID="AdminNameLiteral" runat="server"
              /></strong>
            </span>
          </div>
        </section>

        <section class="admin-stats">
          <article class="admin-stat">
            <span>Club Members</span>
            <strong
              ><asp:Literal ID="MemberCountLiteral" runat="server"
            /></strong>
          </article>
          <article class="admin-stat">
            <span>All Events</span>
            <strong
              ><asp:Literal ID="EventCountLiteral" runat="server"
            /></strong>
          </article>
          <article class="admin-stat">
            <span>Upcoming Events</span>
            <strong
              ><asp:Literal ID="UpcomingCountLiteral" runat="server"
            /></strong>
          </article>
        </section>

        <asp:Panel
          ID="MessagePanel"
          runat="server"
          Visible="false"
          CssClass="admin-notice"
        >
          <asp:Literal ID="MessageLiteral" runat="server" />
        </asp:Panel>

        <section class="admin-quicklinks">
          <h3>Quick Access</h3>
          <a href="#membersSection">Members</a>
          <a href="#eventsSection">Events</a>
          <a href="Default.aspx">View Site</a>
        </section>
      </aside>

      <div class="admin-content">
        <section class="admin-card" id="membersSection">
          <div class="admin-card-head">
            <div class="admin-card-title">
              <h3><i class="fa-solid fa-users"></i> Club Member Management</h3>
              <p>
                Add new members, edit their public details, or deactivate them.
              </p>
            </div>
          </div>

          <div class="admin-form-grid">
            <asp:HiddenField
              ID="MemberIdHiddenField"
              runat="server"
              Value="0"
            />
            <label class="full-width">
              Full Name
              <asp:TextBox ID="MemberFullNameTextBox" runat="server" />
            </label>
            <label>
              Position
              <asp:TextBox ID="MemberPositionTextBox" runat="server" />
            </label>
            <label>
              Department
              <asp:TextBox ID="MemberDepartmentTextBox" runat="server" />
            </label>
            <label>
              Email
              <asp:TextBox
                ID="MemberEmailTextBox"
                runat="server"
                TextMode="Email"
              />
            </label>
            <label>
              Phone
              <asp:TextBox
                ID="MemberPhoneTextBox"
                runat="server"
                TextMode="Phone"
              />
            </label>
            <label class="full-width">
              Photo URL
              <asp:TextBox ID="MemberPhotoUrlTextBox" runat="server" />
            </label>
            <label class="full-width">
              Bio
              <asp:TextBox
                ID="MemberBioTextBox"
                runat="server"
                TextMode="MultiLine"
                Rows="4"
              />
            </label>
            <label>
              Active
              <asp:CheckBox
                ID="MemberActiveCheckBox"
                runat="server"
                Checked="true"
                Text="Visible on site"
              />
            </label>
          </div>

          <div class="admin-actions">
            <asp:Button
              ID="MemberSaveButton"
              runat="server"
              CssClass="btn btn-primary"
              Text="Save Member"
              OnClick="MemberSaveButton_Click"
            />
            <asp:Button
              ID="MemberClearButton"
              runat="server"
              CssClass="btn btn-ghost"
              Text="Clear"
              OnClick="MemberClearButton_Click"
            />
          </div>

          <div class="admin-table-wrap">
            <asp:GridView
              ID="MembersGridView"
              runat="server"
              CssClass="admin-table"
              AutoGenerateColumns="False"
              DataKeyNames="MemberId"
              GridLines="None"
              OnSelectedIndexChanged="MembersGridView_SelectedIndexChanged"
              OnRowDeleting="MembersGridView_RowDeleting"
            >
              <Columns>
                <asp:CommandField ShowSelectButton="True" SelectText="Edit" />
                <asp:BoundField DataField="FullName" HeaderText="Full Name" />
                <asp:BoundField DataField="Position" HeaderText="Position" />
                <asp:BoundField
                  DataField="Department"
                  HeaderText="Department"
                />
                <asp:BoundField DataField="Email" HeaderText="Email" />
                <asp:CheckBoxField DataField="IsActive" HeaderText="Active" />
                <asp:CommandField ShowDeleteButton="True" DeleteText="Delete" />
              </Columns>
            </asp:GridView>
          </div>
        </section>

        <section class="admin-card" id="eventsSection">
          <div class="admin-card-head">
            <div class="admin-card-title">
              <h3>
                <i class="fa-solid fa-calendar-days"></i> Event Management
              </h3>
              <p>
                Control the public event pages and the upcoming events section.
              </p>
            </div>
          </div>

          <div class="admin-form-grid">
            <asp:HiddenField ID="EventIdHiddenField" runat="server" Value="0" />
            <label>
              Slug
              <asp:TextBox ID="EventSlugTextBox" runat="server" />
            </label>
            <label>
              Title
              <asp:TextBox ID="EventTitleTextBox" runat="server" />
            </label>
            <label>
              Event Date
              <asp:TextBox
                ID="EventDateTextBox"
                runat="server"
                TextMode="Date"
              />
            </label>
            <label>
              Venue
              <asp:TextBox ID="EventVenueTextBox" runat="server" />
            </label>
            <label>
              Format
              <asp:TextBox ID="EventFormatTextBox" runat="server" />
            </label>
            <label>
              Fee
              <asp:TextBox ID="EventFeeTextBox" runat="server" />
            </label>
            <label class="full-width">
              Tagline
              <asp:TextBox ID="EventTaglineTextBox" runat="server" />
            </label>
            <label class="full-width">
              Summary
              <asp:TextBox
                ID="EventSummaryTextBox"
                runat="server"
                TextMode="MultiLine"
                Rows="4"
              />
            </label>
            <label class="full-width">
              Eligibility
              <asp:TextBox ID="EventEligibilityTextBox" runat="server" />
            </label>
            <label class="full-width">
              Payment Note
              <asp:TextBox ID="EventPaymentNoteTextBox" runat="server" />
            </label>
            <label class="full-width">
              Guidelines (one per line)
              <asp:TextBox
                ID="EventGuidelinesTextBox"
                runat="server"
                TextMode="MultiLine"
                Rows="5"
              />
            </label>
            <label class="full-width">
              Background Image URL
              <asp:TextBox ID="EventBackgroundUrlTextBox" runat="server" />
            </label>
            <label>
              Upcoming Event
              <asp:CheckBox
                ID="EventUpcomingCheckBox"
                runat="server"
                Checked="true"
                Text="Show on homepage"
              />
            </label>
            <label>
              Active
              <asp:CheckBox
                ID="EventActiveCheckBox"
                runat="server"
                Checked="true"
                Text="Visible on site"
              />
            </label>
          </div>

          <div class="admin-actions">
            <asp:Button
              ID="EventSaveButton"
              runat="server"
              CssClass="btn btn-primary"
              Text="Save Event"
              OnClick="EventSaveButton_Click"
            />
            <asp:Button
              ID="EventClearButton"
              runat="server"
              CssClass="btn btn-ghost"
              Text="Clear"
              OnClick="EventClearButton_Click"
            />
          </div>

          <div class="admin-table-wrap">
            <asp:GridView
              ID="EventsGridView"
              runat="server"
              CssClass="admin-table"
              AutoGenerateColumns="False"
              DataKeyNames="EventId"
              GridLines="None"
              OnSelectedIndexChanged="EventsGridView_SelectedIndexChanged"
              OnRowDeleting="EventsGridView_RowDeleting"
            >
              <Columns>
                <asp:CommandField ShowSelectButton="True" SelectText="Edit" />
                <asp:BoundField DataField="Title" HeaderText="Title" />
                <asp:BoundField DataField="Slug" HeaderText="Slug" />
                <asp:BoundField DataField="Date" HeaderText="Date" />
                <asp:BoundField DataField="Format" HeaderText="Format" />
                <asp:CheckBoxField
                  DataField="IsUpcoming"
                  HeaderText="Upcoming"
                />
                <asp:CheckBoxField DataField="IsActive" HeaderText="Active" />
                <asp:CommandField ShowDeleteButton="True" DeleteText="Delete" />
              </Columns>
            </asp:GridView>
          </div>

          <p class="admin-note">
            Dates are stored in display format so the public event pages keep
            the same look and feel.
          </p>
        </section>
      </div>
    </div>
  </main>
</asp:Content>
