<%@ Page Title="" Language="C#" MasterPageFile="~/UserManage.master" AutoEventWireup="true" CodeBehind="cpassowrd.aspx.cs" Inherits="Website.cpassowrd" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <table class="table table-bordered table-hover">
          <tr><td>原始密码</td><td>
              <asp:TextBox ID="TextBox1" runat="server" TextMode="Password"></asp:TextBox>
              </td></tr>
         <tr><td>新设密码</td><td>
             <asp:TextBox ID="TextBox2" runat="server" TextMode="Password"></asp:TextBox>
              </td></tr>
         <tr><td>密码重复</td><td>
             <asp:TextBox ID="TextBox3" runat="server" TextMode="Password"></asp:TextBox>
              </td></tr>
         <tr><td colspan="2">
               <asp:Button ID="Button1" runat="server" Text="修改" CssClass="btn btn-primary" OnClick="Button1_Click" />&nbsp;&nbsp;<a class="btn btn-primary" href="account.aspx" title="返回">返回</a></td></tr>
         </table>
    <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
</asp:Content>
