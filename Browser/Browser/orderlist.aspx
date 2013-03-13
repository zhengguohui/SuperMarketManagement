<%@ Page Title="" Language="C#" MasterPageFile="~/UserManage.master" AutoEventWireup="true" CodeBehind="orderlist.aspx.cs" Inherits="Website.history" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="table table-bordered table-hover">
        <tr>
            <td>起始时间：<asp:TextBox ID="TextBox1" runat="server" CssClass="aaa span2 search-query"></asp:TextBox>
                &nbsp;&nbsp;结束时间：<asp:TextBox ID="TextBox2" runat="server" CssClass="bbb span2 search-query"></asp:TextBox>
                &nbsp;&nbsp;<asp:Button ID="Button1" runat="server" Text="筛选" CssClass="btn btn-primary" OnClick="Button1_Click" /></td>
        </tr>
    </table>
    <table class="table table-bordered table-hover table-striped">
        <tr>
            <th>订单</th>
            <th>订单详情</th>
        </tr>
        <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
    </table>
    <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>
    <script>
        $(".aaa").datepicker({
            /* 区域化周名为中文 */
            dayNamesMin: ["日", "一", "二", "三", "四", "五", "六"],
            /* 每周从周一开始 */
            firstDay: 1,
            /* 区域化月名为中文习惯 */
            monthNames: ["1", "2", "3", "4", "5", "6",
                        "7", "8", "9", "10", "11", "12"],
            /* 月份显示在年后面 */
            showMonthAfterYear: true,
            /* 年份后缀字符 */
            yearSuffix: "年",
            /* 格式化中文日期  
            （因为月份中已经包含“月”字，所以这里省略） */
            dateFormat: "yy/MM/dd"
        });
        $(".bbb").datepicker({
            /* 区域化周名为中文 */
            dayNamesMin: ["日", "一", "二", "三", "四", "五", "六"],
            /* 每周从周一开始 */
            firstDay: 1,
            /* 区域化月名为中文习惯 */
            monthNames: ["1", "2", "3", "4", "5", "6",
                        "7", "8", "9", "10", "11", "12"],
            /* 月份显示在年后面 */
            showMonthAfterYear: true,
            /* 年份后缀字符 */
            yearSuffix: "年",
            /* 格式化中文日期  
            （因为月份中已经包含“月”字，所以这里省略） */
            dateFormat: "yy/MM/dd"
        });
    </script>
</asp:Content>
