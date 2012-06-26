<%@ Page Title="" Language="C#" MasterPageFile="~/Frames.Master" AutoEventWireup="true" CodeBehind="EvoqueEffect.aspx.cs" Inherits="LRAdmin.EvoqueEffect" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TabsHolder" runat="server">
  <li class="cur"><a href="KeepMeInformed.aspx">数据查询</a></li>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentHolder" runat="server">
  <h4>极光之城</h4>
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
          <tr id="Tr1" runat="server">
            <th>ID</th>
            <th>姓名</th>
            <th>电子邮件</th>
            <th>联系电话</th>
            <th>是否订阅</th>
            <th>提交时间</th>
            <th>来源</th>
          </tr>
        </thead>
        <tbody>
         <tr runat="server" id="itemPlaceholder" />
        </tbody>
      </table>
    </LayoutTemplate>
    <ItemTemplate>
        <tr id="Tr2" runat="server">
          <td><%# Eval("ID") %></td>
          <td class="center"><%# Eval("Name") %></td>
          <td><%# Eval("Email") %></td>
          <td class="center"><%# Eval("Phone") %></td>
          <td class="center"><%# Eval("Sub") %></td>
          <td class="center"><%# Eval("Time") %></td>
          <td class="center"><%# Eval("Source") %></td>
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
        //"aaSorting": [[9, 'asc']],
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
