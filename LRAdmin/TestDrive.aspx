<%@ Page Title="" Language="C#" MasterPageFile="~/Frames.Master" AutoEventWireup="true" CodeBehind="TestDrive.aspx.cs" Inherits="LRAdmin.TestDrive" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TabsHolder" runat="server">
  <li class="cur"><a href="TestDrive.aspx">数据下载</a></li>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentHolder" runat="server">
  <h4>试乘试驾</h4>
  <div class="ui-state-highlight ui-corner-all info"> 
		<p><span style="float: left; margin-right: 3px;" class="ui-icon ui-icon-info"></span>
		  <strong>5/30 更新：</strong> 表单调整，更新以后的数据会与之前有所不同。详细变更内容如下</p>
		<ul>
		  <li>删除了<code>称呼</code>、<code>您的出生日期</code>、<code>您希望的试驾时间</code>、<code>经销商省份</code>、<code>经销商城市</code>、<code>您的购车预算</code>和<code>您偏好的车型</code>等7项。</li>
		  <li>表单中<code>您的电话号码</code>与<code>您的移动电话号码</code>两项合并为<code>您的电话号码</code>，在数据表中使用<code>shouji</code>字段。</li>
		  <li>数据中<code>gcsj</code>(您预计购车的时间)字段不再保存时间范围的值，改为具体的年/月。</li>
		  <li>表单中<code>您所偏爱的联系方式</code>新增了一个选项<code>均可</code>，其对应的值为<code>5</code>。</li>
		  <li>新增了一个<code>同意申请手册</code>的单选框，在数据表中使用<code>brochure</code>字段。</li>
		  <li>更多内容参见官网<a href="http://www.landroverchina.com.cn/shijia.html" target="_blank">试乘试驾页面</a>。</li>
		</ul>
	</div>
  <fieldset>
    <legend>查找数据</legend>
    <p>
      <asp:Label ID="Label1" runat="server" Text="时间范围" AssociatedControlID="tbDateFrom"></asp:Label>
      <asp:TextBox ID="tbDateFrom" runat="server"></asp:TextBox>
      <asp:Label ID="Label2" runat="server" Text="-" AssociatedControlID="tbDateTo"></asp:Label>
      <asp:TextBox ID="tbDateTo" runat="server"></asp:TextBox>
    </p>
  </fieldset>
  <uc:ModernButton ID="btnDown" runat="server" CssClass="btn_down" OnClick="btnDown_Click">下载数据</uc:ModernButton>
  <div class="lineholder"></div>
  <script type="text/javascript">
    $(document).ready(function() {
      $(".btn_down").button({ icons: { primary: 'ui-icon-disk'} });
      bindRangeDatepicker('<%=tbDateFrom.ClientID %>', '<%=tbDateTo.ClientID %>');
    });
  </script>
</asp:Content>
