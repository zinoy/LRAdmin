<%@ Page Title="" Language="C#" MasterPageFile="~/Frames.Master" AutoEventWireup="true" CodeBehind="Discover24Hours.aspx.cs" Inherits="LRAdmin.Discover24Hours" %>
<asp:Content ID="tabs" ContentPlaceHolderID="TabsHolder" runat="server">
  <li class="cur"><a href="Discover24Hours.aspx">数据查询</a></li>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentHolder" runat="server">
  <h4>发现无止境</h4>
  <fieldset>
    <legend>查找数据</legend>
    <p>
      <asp:Label ID="Label1" runat="server" Text="时间范围" AssociatedControlID="tbDateFrom"></asp:Label>
      <asp:TextBox ID="tbDateFrom" runat="server"></asp:TextBox>
      <asp:Label ID="Label2" runat="server" Text="-" AssociatedControlID="tbDateTo"></asp:Label>
      <asp:TextBox ID="tbDateTo" runat="server"></asp:TextBox>
    </p>
  </fieldset>
  <uc:ModernButton ID="btnFilter" runat="server" CssClass="btn_search" OnClick="btnFilter_Click">查询</uc:ModernButton>
  <uc:ModernButton ID="btnDown" runat="server" CssClass="btn_down" OnClick="btnDown_Click">下载数据</uc:ModernButton>
  <div class="lineholder"></div>
  <link href="css/jquery.dataTables.jui.css" rel="stylesheet" type="text/css" />
  <asp:ListView ID="dataList" runat="server">
    <LayoutTemplate>
      <table cellpadding="0" cellspacing="0" id="datatable" class="display">
        <thead>
          <tr runat="server">
            <th>姓名</th>
            <th>称呼</th>
            <th>电子邮件</th>
            <th>省份</th>
            <th>城市</th>
            <th>手机号</th>
            <th>最高分</th>
            <th>微博帐号</th>
            <th>活动站点</th>
            <th>注册时间</th>
          </tr>
        </thead>
        <tbody>
         <tr runat="server" id="itemPlaceholder" />
        </tbody>
      </table>
    </LayoutTemplate>
    <ItemTemplate>
        <tr runat="server">
          <td><%# Eval("Name") %></td>
          <td class="center"><%# Eval("Title") %></td>
          <td><%# Eval("Email") %></td>
          <td class="center"><%# Eval("Province") %></td>
          <td class="center"><%# Eval("City") %></td>
          <td class="center"><%# Eval("Mobile") %></td>
          <td class="center"><%# Eval("HighScore") %></td>
          <td class="center"><%# Eval("Weibo") %></td>
          <td class="center"><%# Eval("Place") %></td>
          <td class="center"><%# Eval("Time") %></td>
        </tr>
    </ItemTemplate>
    <EmptyDataTemplate>
      暂无数据。
    </EmptyDataTemplate>
  </asp:ListView>
  <script type="text/javascript">
    $(document).ready(function() {
      $(".btn_search").button({ icons: { primary: 'ui-icon-search'} });
      $(".btn_down").button({ icons: { primary: 'ui-icon-disk'} });
      oTable = $('#datatable').dataTable({
        "aaSorting": [[9, 'asc']],
        "bJQueryUI": true,
        "iDisplayLength": 20,
        "bLengthChange": false,
        "sPaginationType": "full_numbers",
        "oLanguage": {
          "sUrl": "dt_cn.txt"
        },
        "fnInitComplete": adjustHeight
      });
      bindRangeDatepicker('<%=tbDateFrom.ClientID %>', '<%=tbDateTo.ClientID %>');
    });
  </script>
</asp:Content>
