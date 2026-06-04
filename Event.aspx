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
    .event-back {
      display: inline-flex;
      align-items: center;
      gap: 0.45rem;
      text-decoration: none;
      color: #f5fbff;
      background: rgba(255, 255, 255, 0.14);
      border: 1px solid rgba(255, 255, 255, 0.25);
      padding: 0.5rem 0.8rem;
      border-radius: 999px;
      font-weight: 700;
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
    .summary {
      margin-bottom: 1rem;
      max-width: 64ch;
      color: #d6ecff;
    }
    .meta-grid {
      display: grid;
      grid-template-columns: repeat(2, minmax(0, 1fr));
      gap: 0.65rem;
      margin-bottom: 1rem;
    }
    .meta-item {
      background: rgba(255, 255, 255, 0.08);
      border: 1px solid rgba(255, 255, 255, 0.16);
      border-radius: 12px;
      padding: 0.6rem 0.75rem;
    }
    .meta-item strong {
      display: block;
      color: #ffd89f;
      margin-bottom: 0.2rem;
      font-size: 0.9rem;
    }
    .meta-item span {
      color: #e8f5ff;
    }
    .event-panel h2 {
      font-family: "Sora", sans-serif;
      margin: 1.1rem 0 0.5rem;
      font-size: 1.12rem;
      color: #ffffff;
    }
    .event-panel ul {
      margin: 0;
      padding-left: 1.1rem;
    }
    .event-panel li {
      margin-bottom: 0.45rem;
      color: #e8f5ff;
    }
    .register-btn {
      margin-top: 1.1rem;
      display: inline-flex;
      align-items: center;
      gap: 0.55rem;
      text-decoration: none;
      color: #fff;
      font-weight: 800;
      background: linear-gradient(135deg, #ff7f50, #ffad62);
      border-radius: 12px;
      padding: 0.72rem 1.05rem;
      box-shadow: 0 12px 24px rgba(255, 127, 80, 0.35);
    }
    @media (max-width: 720px) {
      .meta-grid {
        grid-template-columns: 1fr;
      }
    }
    .winners-grid {
      display: grid;
      grid-template-columns: repeat(3, minmax(0, 1fr));
      gap: 1rem;
      margin: 1rem 0;
    }
    .winner-card {
      background: rgba(255, 255, 255, 0.08);
      border: 1px solid rgba(255, 203, 132, 0.35);
      border-radius: 12px;
      padding: 1rem;
      text-align: center;
      transition: all 0.3s ease;
    }
    .winner-card:hover {
      background: rgba(255, 203, 132, 0.12);
      border-color: rgba(255, 203, 132, 0.65);
      transform: translateY(-4px);
    }
    .winner-badge {
      font-size: 2.5rem;
      margin-bottom: 0.5rem;
    }
    .winner-badge.gold {
      filter: drop-shadow(0 0 8px rgba(255, 203, 132, 0.6));
    }
    .winner-badge.silver {
      filter: drop-shadow(0 0 8px rgba(192, 192, 192, 0.4));
    }
    .winner-badge.bronze {
      filter: drop-shadow(0 0 8px rgba(205, 127, 50, 0.4));
    }
    .winner-card strong {
      color: #ffd89f;
      display: block;
      margin-bottom: 0.5rem;
      font-size: 0.95rem;
    }
    .winner-card p {
      margin: 0;
      color: #e8f5ff;
      font-size: 0.9rem;
      line-height: 1.5;
    }
    @media (max-width: 900px) {
      .winners-grid {
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

      <!-- Detailed Description Section -->
      <asp:Panel ID="DetailedDescriptionPanel" runat="server" Visible="false">
        <h2>Event Details</h2>
        <p style="color: #d6ecff; line-height: 1.6">
          <asp:Literal ID="DetailedDescriptionLiteral" runat="server" />
        </p>
      </asp:Panel>

      <!-- Highlights Section -->
      <asp:Panel ID="HighlightsPanel" runat="server" Visible="false">
        <h2>Event Highlights</h2>
        <ul>
          <asp:Literal ID="HighlightsLiteral" runat="server" />
        </ul>
      </asp:Panel>

      <!-- Sponsor Section -->
      <asp:Panel ID="SponsorPanel" runat="server" Visible="false">
        <h2>Sponsor</h2>
        <p style="color: #d6ecff">
          <strong style="color: #ffd89f">
            <asp:Literal ID="SponsorLiteral" runat="server" />
          </strong>
        </p>
      </asp:Panel>

      <!-- Winners Section -->
      <asp:Panel ID="WinnersPanel" runat="server" Visible="false">
        <h2>Event Winners & Achievements</h2>
        <div class="winners-grid">
          <article class="winner-card">
            <div class="winner-badge gold">🥇</div>
            <strong>Champion</strong>
            <p><asp:Literal ID="WinnerLiteral" runat="server" /></p>
          </article>
          <article class="winner-card">
            <div class="winner-badge silver">🥈</div>
            <strong>1st Runner-up</strong>
            <p><asp:Literal ID="RunnerUpFirstLiteral" runat="server" /></p>
          </article>
          <article class="winner-card">
            <div class="winner-badge bronze">🥉</div>
            <strong>2nd Runner-up</strong>
            <p><asp:Literal ID="RunnerUpSecondLiteral" runat="server" /></p>
          </article>
        </div>
      </asp:Panel>

      <!-- Participant Count Section -->
      <asp:Panel ID="ParticipantCountPanel" runat="server" Visible="false">
        <div
          style="
            margin-top: 1.5rem;
            padding: 1rem;
            background: rgba(255, 203, 132, 0.15);
            border-left: 4px solid rgba(255, 203, 132, 0.55);
            border-radius: 8px;
          "
        >
          <p style="margin: 0; color: #ffe7bd">
            <strong>Participants:</strong>
            <asp:Literal ID="ParticipantCountLiteral" runat="server" /> students
            attended
          </p>
        </div>
      </asp:Panel>

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
