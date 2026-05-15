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

    .registration-back {
      display: inline-block;
      margin-bottom: 1.5rem;
      padding: 0.5rem 1rem;
      background-color: #f0f0f0;
      color: #333;
      text-decoration: none;
      border-radius: 4px;
      font-size: 0.95rem;
      transition: background-color 0.3s ease;
    }

    .registration-back:hover {
      background-color: #e0e0e0;
    }

    .registration-card {
      background: white;
      border-radius: 8px;
      box-shadow: 0 2px 8px rgba(0, 0, 0, 0.1);
      padding: 2rem;
    }

    .registration-card h1 {
      color: #333;
      margin-bottom: 1rem;
      font-size: 1.8rem;
    }

    .event-title {
      font-size: 1.2rem;
      color: #555;
      margin: 0.5rem 0;
      font-weight: 600;
    }

    .event-meta {
      font-size: 0.95rem;
      color: #777;
      margin-bottom: 1.5rem;
    }

    .error-list {
      background-color: #fee;
      border: 1px solid #faa;
      border-radius: 4px;
      padding: 1rem;
      margin-bottom: 1.5rem;
      color: #c00;
    }

    .error-list h2 {
      color: #c00;
      font-size: 1.1rem;
      margin-top: 0;
    }

    .error-list ul {
      margin: 0.5rem 0 0 1.5rem;
      padding: 0;
    }

    .error-list li {
      margin: 0.3rem 0;
    }

    .success-box {
      background-color: #efe;
      border: 1px solid #afa;
      border-radius: 4px;
      padding: 1rem;
      margin-bottom: 1.5rem;
      color: #060;
    }

    .success-box p {
      margin: 0.5rem 0;
    }

    .grid {
      display: grid;
      grid-template-columns: 1fr 1fr;
      gap: 1.5rem;
      margin-bottom: 1.5rem;
    }

    .grid label {
      display: flex;
      flex-direction: column;
      font-weight: 600;
      color: #333;
    }

    .grid label.full {
      grid-column: 1 / -1;
    }

    .grid input,
    .grid select,
    .grid textarea {
      margin-top: 0.5rem;
      padding: 0.75rem;
      border: 1px solid #ddd;
      border-radius: 4px;
      font-family: inherit;
      font-size: 1rem;
    }

    .grid input:focus,
    .grid select:focus,
    .grid textarea:focus {
      outline: none;
      border-color: #4caf50;
      box-shadow: 0 0 5px rgba(76, 175, 80, 0.3);
    }

    .grid textarea {
      resize: vertical;
      min-height: 100px;
    }

    .submit-btn {
      background-color: #4caf50;
      color: white;
      padding: 0.75rem 2rem;
      border: none;
      border-radius: 4px;
      font-size: 1rem;
      font-weight: 600;
      cursor: pointer;
      transition: background-color 0.3s ease;
    }

    .submit-btn:hover {
      background-color: #45a049;
    }

    .submit-btn:active {
      background-color: #3d8b40;
    }

    dl {
      display: grid;
      grid-template-columns: 150px 1fr;
      gap: 1rem;
      margin: 1rem 0;
    }

    dt {
      font-weight: 600;
      color: #333;
    }

    dd {
      margin: 0;
      color: #555;
    }

    dd.motivation {
      white-space: pre-wrap;
      word-wrap: break-word;
    }

    .actions {
      margin-top: 2rem;
      padding-top: 1.5rem;
      border-top: 1px solid #eee;
    }

    .actions a {
      display: inline-block;
      padding: 0.75rem 1.5rem;
      background-color: #2196f3;
      color: white;
      text-decoration: none;
      border-radius: 4px;
      transition: background-color 0.3s ease;
    }

    .actions a:hover {
      background-color: #0b7dda;
    }

    @media (max-width: 680px) {
      .grid {
        grid-template-columns: 1fr;
      }
      dl {
        grid-template-columns: 1fr;
      }
      .registration-card {
        padding: 1rem;
      }
      .registration-card h1 {
        font-size: 1.5rem;
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
