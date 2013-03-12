<%@ Page Title="" Language="C#" MasterPageFile="~/MainSite.Master" AutoEventWireup="true" CodeBehind="list.aspx.cs" Inherits="Website.list" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label><asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>
    <asp:TextBox CssClass="search-query" ID="TextBox1" runat="server"></asp:TextBox>&nbsp;&nbsp;<asp:Button ID="Button1" runat="server" Text="修改" CssClass="btn btn-primary" OnClick="Button1_Click" /><asp:Label ID="Label3" runat="server" Text=""></asp:Label>
    <div style="text-align:right;"><asp:Label ID="Label4" runat="server" Text="Label"></asp:Label>&nbsp;<asp:Button ID="Button2" runat="server" CssClass="btn btn-primary" Text="确定订单" OnClick="Button2_Click" /></div></asp:Content>
