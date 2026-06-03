<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminRegister.aspx.cs"
Inherits="SpectrumWebForms.AdminRegisterPage" MasterPageFile="~/Site.Master" %>
<asp:Content
  ID="TitleContent"
  ContentPlaceHolderID="TitleContent"
  runat="server"
>
  Admin Register | Spectrum
</asp:Content>
<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" runat="server">
  <style>
    .auth-shell {
      width: min(920px, 92%);
      margin: 2rem auto 0;
    }

    .auth-card {
      background: rgba(250, 253, 255, 0.96);
      border: 1px solid #d8eaf6;
      border-radius: 26px;
      box-shadow: 0 20px 40px rgba(14, 55, 78, 0.16);
      padding: 1.4rem;
    }

    .auth-card h2 {
      font-family: "Sora", sans-serif;
      color: #0d334d;
      margin-bottom: 0.5rem;
    }

    .auth-card p {
      color: #567085;
      margin-bottom: 1rem;
    }

    .auth-error,
    .auth-success {
      border-radius: 14px;
      padding: 0.9rem 1rem;
      margin-bottom: 1rem;
      font-weight: 700;
    }

    .auth-error {
      background: #fff0f0;
      border: 1px solid #f0b3b3;
      color: #9a1f1f;
    }

    .auth-success {
      background: #eefcf2;
      border: 1px solid #b8e5c4;
      color: #196236;
    }

    .auth-form-grid {
      display: grid;
      grid-template-columns: repeat(2, minmax(0, 1fr));
      gap: 0.85rem;
    }

    .auth-form-grid label {
      display: flex;
      flex-direction: column;
      gap: 0.35rem;
      font-weight: 700;
      color: #17394f;
    }

    .auth-form-grid input {
      width: 100%;
      border: 1px solid #c9e0f1;
      border-radius: 12px;
      padding: 0.8rem 0.85rem;
      font: inherit;
      background: #fff;
    }

    .full-width {
      grid-column: 1 / -1;
    }

    .auth-actions {
      display: flex;
      gap: 0.6rem;
      flex-wrap: wrap;
      margin-top: 1rem;
    }

    .auth-link {
      color: #0c6090;
      text-decoration: none;
      font-weight: 700;
    }

    .auth-link:hover {
      text-decoration: underline;
    }

    @media (max-width: 720px) {
      .auth-form-grid {
        grid-template-columns: 1fr;
      }
    }
  </style>
</asp:Content>
<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
  <main class="auth-shell">
    <section class="auth-card">
      <h2>Admin Registration</h2>
      <p>Create a protected admin account with the required profile details.</p>

      <asp:Panel
        ID="MessagePanel"
        runat="server"
        Visible="false"
        CssClass="auth-error"
      >
        <asp:Literal ID="MessageLiteral" runat="server" />
      </asp:Panel>

      <asp:Panel
        ID="SuccessPanel"
        runat="server"
        Visible="false"
        CssClass="auth-success"
      >
        <asp:Literal ID="SuccessLiteral" runat="server" />
      </asp:Panel>

      <div class="auth-form-grid">
        <label class="full-width">
          Full Name
          <asp:TextBox ID="FullNameTextBox" runat="server" />
        </label>
        <label>
          Email
          <asp:TextBox ID="EmailTextBox" runat="server" TextMode="Email" />
        </label>
        <label>
          Username
          <asp:TextBox ID="UsernameTextBox" runat="server" />
        </label>
        <label>
          Password
          <asp:TextBox
            ID="PasswordTextBox"
            runat="server"
            TextMode="Password"
          />
        </label>
        <label>
          Confirm Password
          <asp:TextBox
            ID="ConfirmPasswordTextBox"
            runat="server"
            TextMode="Password"
          />
        </label>
        <label>
          Date of Birth
          <asp:TextBox ID="DateOfBirthTextBox" runat="server" TextMode="Date" />
        </label>
        <label>
          Phone
          <asp:TextBox ID="PhoneTextBox" runat="server" TextMode="Phone" />
        </label>
        <label>
          Admin Invite Code
          <asp:TextBox ID="InviteCodeTextBox" runat="server" />
        </label>
      </div>

      <div class="auth-actions">
        <asp:Button
          ID="RegisterButton"
          runat="server"
          CssClass="btn btn-primary"
          Text="Create account"
          OnClick="RegisterButton_Click"
        />
        <a class="auth-link" href="Login.aspx">Back to login</a>
      </div>
    </section>
  </main>
</asp:Content>
