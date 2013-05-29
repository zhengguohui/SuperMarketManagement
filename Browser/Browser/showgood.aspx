<%@ Page Title="" Language="C#" MasterPageFile="~/MainSite.Master" AutoEventWireup="true" CodeBehind="showgood.aspx.cs" Inherits="Website.showgood" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="span3">
            <div class="thumbnail">
                <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
                <div class="caption">
                    购买数量：<asp:TextBox ID="TextBox1" runat="server" CssClass="input-small" Text="1"></asp:TextBox>
                    <asp:Button ID="Button1" runat="server" Text="立即购买" CssClass="btn btn-primary" OnClick="Button1_Click" />&nbsp;&nbsp;<asp:Button ID="Button2" runat="server" Text="放入购物车" CssClass="btn btn-warning" OnClick="Button2_Click" />
                    <p><asp:Label ID="Label3" runat="server" Text="Label"></asp:Label></p>

                </div>
          </div>

        </div>
        <div class="span6"><asp:Label ID="Label2" runat="server" Text="Label"></asp:Label></div>
        
    </div>
    <br /><br />
     <div class="row">
        <div class="span9">
            <script type='text/javascript' charset='utf-8' src='http://open.denglu.cc/connect/commentcode?appid=65919denR3U4re4yXvy61Aqclb0kx5'></script>
            </div>
         </div>
</asp:Content>
