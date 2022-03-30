<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CashPage.aspx.cs" Inherits="CashFlowClient.CashPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="mb-3">
        <label for="amount" class="form-label">Amount</label>
        <input type="number" class="form-control" id="amount" runat="server" required>
    </div>
    <div class="mb-3">
        <label for="desc" class="form-label">Description</label>
        <textarea class="form-control" id="desc" rows="3" runat="server"></textarea>
    </div>
    <asp:Button ID="AddRecord" CssClass="btn" runat="server" Text="Add Record" OnClick="AddRecord_Click" />
    <br />
    <br />

    <table class="table">
        <thead>
            <tr>
                <th scope="col">#</th>
                <th scope="col">Description</th>
                <th scope="col">Amount</th>
                <th scope="col">Actions</th>
            </tr>
        </thead>
        <tbody>
            <asp:Repeater ID="rpt" runat="server" OnItemCommand="rpt_ItemCommand">
                <ItemTemplate>
                    <tr>
                        <th scope="row"><%# Eval("ID") %></th>
                        <td><%# Eval("Description") %></td>
                        <td><%# Eval("Amount") %></td>
                        <td>
                            <asp:LinkButton ID="Delete" runat="server" CssClass="btn" CommandName="Delete" CommandArgument='<%#Eval("ID") %>'>Delete</asp:LinkButton>
                            <br />
                            <a class="btn" href="edit.aspx?id=<%# Eval("ID") %>">Edit</a>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>

        </tbody>
    </table>
</asp:Content>
