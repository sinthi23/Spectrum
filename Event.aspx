<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Event.aspx.cs"
Inherits="SpectrumWebForms.EventDetailsPage" MasterPageFile="~/Site.Master" %>
<asp:Content
  ID="TitleContent"
  ContentPlaceHolderID="TitleContent"
  runat="server"
>
  <asp:Literal ID="PageTitleLiteral" runat="server" />
</asp:Content>
<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" runat="server">
  <style>
    .event-shell {
      width: min(980px, 92%);
      margin: 2rem auto;
    }

    .event-panel {
      margin-top: 1rem;
      border-radius: 22px;
      padding: clamp(1.1rem, 2vw, 1.8rem);
      background: #0f2739;
      border: 1px solid rgba(255, 255, 255, 0.16);
      box-shadow: 0 22px 40px rgba(0, 0, 0, 0.24);
      color: #e7f2ff;
    }
    .date-chip {
      display: inline-block;
      background: rgba(255, 203, 132, 0.25);
      border: 1px solid rgba(255, 203, 132, 0.55);
      border-radius: 999px;
      padding: 0.28rem 0.7rem;
      font-weight: 700;
      margin-bottom: 0.8rem;
      color: #ffe7bd;
    }
    .event-panel h1 {
      margin: 0 0 0.35rem;
      font-family: "Sora", sans-serif;
      font-size: clamp(1.5rem, 1.1rem + 2vw, 2.4rem);
      color: #ffffff;
    }
    .tagline {
      margin: 0 0 0.8rem;
      color: #ffd89f;
      font-weight: 700;
    }

    @media (max-width: 720px) {
      .meta-grid {
        grid-template-columns: 1fr;
      }
    }
  </style>
</asp:Content>
<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
  <main class="event-shell">
    <a class="event-back" href="Default.aspx#upcoming">
      <i class="fa-solid fa-arrow-left"></i>
      Back to Upcoming Events
    </a>

    <section class="event-panel">
      <asp:Literal ID="DateLiteral" runat="server" />
      <h1><asp:Literal ID="TitleLiteral" runat="server" /></h1>
      <p class="tagline"><asp:Literal ID="TaglineLiteral" runat="server" /></p>
      <p class="summary"><asp:Literal ID="SummaryLiteral" runat="server" /></p>

      <div class="meta-grid">
        <article class="meta-item">
          <strong>Event Format</strong>
          <span><asp:Literal ID="FormatLiteral" runat="server" /></span>
        </article>
        <article class="meta-item">
          <strong>Eligibility</strong>
          <span><asp:Literal ID="EligibilityLiteral" runat="server" /></span>
        </article>
        <article class="meta-item">
          <strong>Registration Fee</strong>
          <span><asp:Literal ID="FeeLiteral" runat="server" /></span>
        </article>
        <article class="meta-item">
          <strong>Payment Guideline</strong>
          <span><asp:Literal ID="PaymentNoteLiteral" runat="server" /></span>
        </article>
      </div>

      <h2>Registration Guidelines</h2>
      <ul>
        <asp:Literal ID="GuidelinesLiteral" runat="server" />
      </ul>

      <a
        class="register-btn"
        href="Registration.aspx?event=<%= CurrentEventId %>"
      >
        <i class="fa-solid fa-user-plus"></i>
        Registration
      </a>
    </section>
  </main>
</asp:Content>
