<%@ Page Title="" Language="C#" MasterPageFile="~/UserManage.master" AutoEventWireup="true" CodeBehind="editaccount.aspx.cs" Inherits="Website.editaccount" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
      <table class="table table-bordered table-hover">
          <tr><td>姓名</td><td>
              <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox></td></tr>
          <tr><td>性别</td><td>
              <asp:DropDownList ID="DropDownList1" runat="server">
                  <asp:ListItem Value="1">男</asp:ListItem>
                  <asp:ListItem Value="0">女</asp:ListItem>
                  <asp:ListItem Value="2">未知</asp:ListItem>
              </asp:DropDownList>
              </td></tr>
          <tr><td>手机</td><td>
              <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox></td></tr>
          <tr><td>电话</td><td>
              <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox></td></tr>
          <tr><td>身份证号</td><td>
              <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox></td></tr>
          <tr><td>家庭住址</td><td>
              <asp:TextBox ID="TextBox5" runat="server"></asp:TextBox></td></tr>
           <tr><td colspan="2">
               <asp:Button ID="Button1" runat="server" Text="修改" CssClass="btn btn-primary" OnClick="Button1_Click" />&nbsp;&nbsp;<a class="btn btn-primary" href="account.aspx" title="返回">返回</a></td></tr>
          </table>
</asp:Content>
