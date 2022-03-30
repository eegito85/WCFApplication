<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="CashFlowClient.Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="mb-3">
        <label for="email" class="form-label">Email</label>
        <input type="email" class="form-control" id="email" runat="server" required>
    </div>
    <div class="mb-3">
        <label for="pwd" class="form-label">Password</label>
        <input type="password" class="form-control" id="pwd" runat="server" required>
    </div>
    <br />
    <asp:Button ID="loginBtn" CssClass="btn" runat="server" Text="Login" OnClick="loginBtn_Click" />
    <br />
    <asp:Label ID="errorLbl" runat="server" Text=""></asp:Label>
</asp:Content>
