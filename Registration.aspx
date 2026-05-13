<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Registration.aspx.cs"
Inherits="SpectrumWebForms.RegistrationPage" MasterPageFile="~/Site.Master" %>
<asp:Content
  ID="TitleContent"
  ContentPlaceHolderID="TitleContent"
  runat="server"
>
  Registration | <asp:Literal ID="PageTitleLiteral" runat="server" />
</asp:Content>
<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" runat="server">
  <style>
    .registration-shell {
      width: min(920px, 92%);
      margin: 2rem auto;
    }

    @media (max-width: 680px) {
      .grid {
        grid-template-columns: 1fr;
      }
      dl {
        grid-template-columns: 1fr;
      }
    }
  </style>
</asp:Content>
<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
  <main class="registration-shell">
    <a class="registration-back" href="Event.aspx?event=<%= CurrentEventId %>"
      >Back to Event Details</a
    >
    <section class="registration-card">
      <h1>Event Registration Form</h1>
      <p class="event-title">
        <asp:Literal ID="EventTitleLiteral" runat="server" />
      </p>
      <p class="event-meta">
        <strong>Format:</strong>
        <asp:Literal ID="EventFormatLabelLiteral" runat="server" /> |
        <strong>Eligible Students:</strong>
        <asp:Literal ID="EligibilityLabelLiteral" runat="server" />
      </p>

      <asp:Panel
        ID="ErrorPanel"
        runat="server"
        Visible="false"
        CssClass="error-list"
      >
        <asp:Literal ID="ErrorMessageLiteral" runat="server" />
      </asp:Panel>

      <asp:Panel ID="SuccessPanel" runat="server" Visible="false">
        <div class="success-box">
          <p>
            <strong>Registration ID:</strong>
            <asp:Literal ID="RegistrationIdLiteral" runat="server" />
          </p>
          <p>
            <strong>Submitted At:</strong>
            <asp:Literal ID="SubmittedAtLiteral" runat="server" />
          </p>
        </div>

        <p>
          Your registration has been received successfully with the details
          below:
        </p>
        <dl>
          <dt>Event</dt>
          <dd><asp:Literal ID="EventLiteral" runat="server" /></dd>
          <dt>Event Format</dt>
          <dd><asp:Literal ID="EventFormatLiteral" runat="server" /></dd>
          <dt>Full Name</dt>
          <dd><asp:Literal ID="FullNameLiteral" runat="server" /></dd>
          <dt>Email</dt>
          <dd><asp:Literal ID="EmailLiteral" runat="server" /></dd>
          <dt>Institution Name</dt>
          <dd><asp:Literal ID="InstitutionLiteral" runat="server" /></dd>
          <dt>Department</dt>
          <dd><asp:Literal ID="DepartmentLiteral" runat="server" /></dd>
          <dt>Academic Year</dt>
          <dd><asp:Literal ID="AcademicYearLiteral" runat="server" /></dd>
          <dt>Payment Method</dt>
          <dd><asp:Literal ID="PaymentMethodLiteral" runat="server" /></dd>
          <dt>Payment Transaction ID</dt>
          <dd><asp:Literal ID="PaymentReferenceLiteral" runat="server" /></dd>
          <dt>Motivation</dt>
          <dd class="motivation">
            <asp:Literal ID="MotivationLiteral" runat="server" />
          </dd>
        </dl>
      </asp:Panel>

      <asp:Panel ID="FormPanel" runat="server">
        <div class="grid">
          <label class="full">
            Full Name
            <asp:TextBox ID="FullNameTextBox" runat="server" />
          </label>
          <label>
            Email
            <asp:TextBox ID="EmailTextBox" runat="server" TextMode="Email" />
          </label>
          <label>
            Department
            <asp:TextBox ID="DepartmentTextBox" runat="server" />
          </label>
          <label>
            Academic Year
            <asp:DropDownList ID="AcademicYearDropDown" runat="server">
              <asp:ListItem Text="Select" Value="" />
              <asp:ListItem Text="1st Year" Value="1st Year" />
              <asp:ListItem Text="2nd Year" Value="2nd Year" />
              <asp:ListItem Text="3rd Year" Value="3rd Year" />
              <asp:ListItem Text="4th Year" Value="4th Year" />
            </asp:DropDownList>
          </label>
          <label class="full">
            Why are you interested?
            <asp:TextBox
              ID="MotivationTextBox"
              runat="server"
              TextMode="MultiLine"
              Rows="4"
            />
          </label>
          <asp:Panel ID="InstitutionWrap" runat="server" CssClass="full">
            <label>
              Institution Name
              <asp:TextBox ID="InstitutionNameTextBox" runat="server" />
            </label>
          </asp:Panel>
          <label>
            Payment Method
            <asp:DropDownList ID="PaymentMethodDropDown" runat="server">
              <asp:ListItem Text="Select" Value="" />
              <asp:ListItem Text="bKash" Value="bKash" />
              <asp:ListItem Text="Nagad" Value="Nagad" />
              <asp:ListItem Text="Rocket" Value="Rocket" />
              <asp:ListItem Text="Cash (On Campus)" Value="Cash (On Campus)" />
            </asp:DropDownList>
          </label>
          <label>
            Payment Transaction ID
            <asp:TextBox ID="PaymentReferenceTextBox" runat="server" />
          </label>
        </div>

        <asp:Button
          ID="SubmitButton"
          runat="server"
          CssClass="submit-btn"
          Text="Submit Registration"
          OnClick="SubmitRegistration_Click"
        />
      </asp:Panel>

      <div class="actions">
        <a href="Default.aspx#upcoming">Back to Upcoming Events</a>
      </div>
    </section>
  </main>
</asp:Content>
