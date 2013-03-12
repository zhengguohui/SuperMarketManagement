<%@ Page Title="" Language="C#" MasterPageFile="~/MainSite.Master" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="Website.login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="span2">
           &nbsp;
        </div>
         <div class="span5">
             <div class="box"><div class="title">登录</div><div class="content">
                 <p> <asp:TextBox ID="TextBoxLoginUsername" runat="server"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;用户名</p>
             <p>    <asp:TextBox ID="TextBoxLoginPassword" runat="server" TextMode="Password"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;密码</p>
               <p>  <asp:Button ID="ButtonLogin" runat="server" Text="登录" CssClass="btn btn-primary" OnClick="ButtonLogin_Click" /></p>
                 <p>
                     <asp:Label ID="LabelAlert" runat="server" Text=""></asp:Label></p>
                                                         </div></div>
               
                </div>
         <div class="span2">
           &nbsp;
        </div></div>
</asp:Content>
