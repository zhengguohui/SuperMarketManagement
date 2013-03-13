<%@ Page Title="" Language="C#" MasterPageFile="~/MainSite.Master" AutoEventWireup="true" CodeBehind="price.aspx.cs" Inherits="Website.price" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="table table-bordered table-hover">
        <tr><th colspan="3">
            <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label></th> </tr>
        <tr><td>日期</td><td>价格</td><td>比例</td></tr>
        <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>
        </table>
</asp:Content>
