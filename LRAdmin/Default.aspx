<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="LRAdmin.Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>路虎中国网站管理平台 | 登录</title>
  <link href="css/redmond/jquery-ui-1.8.18.custom.css" rel="stylesheet" type="text/css" />
  <link href="css/login.css" rel="stylesheet" type="text/css" />
</head>
<body>
  <form id="form1" runat="server">
  <div class="panels">
    <div class="ui-state-highlight ui-corner-all" id="pInfo" runat="server"> 
		  <p><span style="float: left; margin-right: 3px;" class="ui-icon ui-icon-info"></span>
		    <strong>您已经成功注销。</strong></p>
	  </div>
    <div class="ui-state-error ui-corner-all hide" id="pError"> 
		  <p><span style="float: left; margin-right: 3px;" class="ui-icon ui-icon-alert"></span>
        <span id="txtError"><asp:Literal ID="msgError" runat="server"></asp:Literal></span></p>
	  </div>
	</div>
  <div class="login-frame">
    <div class="from-frame">
      <div class="form-error">
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="登录名不能为空。" ControlToValidate="tbName"></asp:RequiredFieldValidator>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="密码不能为空。" ControlToValidate="tbPass"></asp:RequiredFieldValidator>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="验证码不能为空。" ControlToValidate="tbCode"></asp:RequiredFieldValidator>
      </div>
      <ul class="form-list">
        <li class="form-row">
          <asp:Label ID="Label1" runat="server" Text="登录名" AssociatedControlID="tbName" CssClass="form-label"></asp:Label>
          <asp:TextBox ID="tbName" runat="server" CssClass="form-text" TabIndex="1"></asp:TextBox>
        </li>
        <li class="form-row">
          <asp:Label ID="Label2" runat="server" Text="密码" AssociatedControlID="tbPass" CssClass="form-label"></asp:Label>
          <asp:TextBox ID="tbPass" runat="server" TextMode="Password" CssClass="form-text" TabIndex="2"></asp:TextBox>
        </li>
        <li class="form-row">
          <a href="#getnew" class="captcha">换一张</a><asp:Image ID="imgCaptcha" runat="server" CssClass="captcha" />
          <asp:Label ID="Label3" runat="server" Text="验证码" AssociatedControlID="tbCode" CssClass="form-label"></asp:Label>
          <asp:TextBox ID="tbCode" runat="server" CssClass="form-text form-text-short" TabIndex="3"></asp:TextBox>
        </li>
        <li>
          <uc:ModernButton ID="btnGo" runat="server" CssClass="btn_go" OnClick="btnGo_Click">登录</uc:ModernButton>
        </li>
      </ul>
    </div>
  </div>
  </form>
  <script src="js/jquery.js" type="text/javascript"></script>
  <script src="js/jquery-ui-1.8.18.custom.min.js" type="text/javascript"></script>
  <script type="text/javascript">
    $(document).ready(function() {
      $(".btn_go").button();
      $("#imgCaptcha").attr("src", "Captcha.aspx?" + Math.random());
      $("#tbName").select();
      $('a.captcha').click(function(e) {
        e.preventDefault();
        $("#imgCaptcha").attr("src", "Captcha.aspx?" + Math.random());
        $("#tbCode").val("").focus();
      });
      if ($('#txtError').text() != "") {
        $("#pError").removeClass("hide");
      }
    });
  </script>
</body>
</html>
