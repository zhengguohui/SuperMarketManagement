<%@ Page Title="" Language="C#" MasterPageFile="~/UserManage.master" AutoEventWireup="true" CodeBehind="account.aspx.cs" Inherits="Website.account" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="table table-bordered table-hover">
        <tr><td>会员卡号</td><td>
            <asp:Label ID="Label0" runat="server" Text="Label"></asp:Label></td></tr>
        <tr><td>积分</td><td>
            <asp:Label ID="Label00" runat="server" Text="Label"></asp:Label></td></tr>
        <tr><td>姓名</td><td>
            <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label></td></tr>
        <tr><td>性别</td><td>
            <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label></td></tr>
        <tr><td>手机</td><td>
            <asp:Label ID="Label3" runat="server" Text="Label"></asp:Label></td></tr>
        <tr><td>电话</td><td>
            <asp:Label ID="Label4" runat="server" Text="Label"></asp:Label></td></tr>
        <tr><td>身份证号</td><td>
            <asp:Label ID="Label5" runat="server" Text="Label"></asp:Label></td></tr>
        <tr><td>家庭住址</td><td>
            <asp:Label ID="Label6" runat="server" Text="Label"></asp:Label></td></tr>
        <tr><td colspan="2">
            <a class="btn btn-primary" href="editaccount.aspx" title="会员信息修改">会员信息修改</a>&nbsp;&nbsp;<a class="btn btn-primary" href="cpassowrd.aspx" title="修改密码">修改密码</a></td></tr>
    </table>

</asp:Content>
