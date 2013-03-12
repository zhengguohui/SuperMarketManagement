<%@ Page Title="" Language="C#" MasterPageFile="~/MainSite.Master" AutoEventWireup="true" CodeBehind="news.aspx.cs" Inherits="Website.news" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="box"><div class="title">
        <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>（<asp:Label ID="Label3" runat="server" Text="Label"></asp:Label>）</div><div class="content">
            <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label></div></div>
</asp:Content>
