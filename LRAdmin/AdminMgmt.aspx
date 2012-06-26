<%@ Page Title="" Language="C#" MasterPageFile="~/Frames.Master" AutoEventWireup="true" CodeBehind="AdminMgmt.aspx.cs" Inherits="LRAdmin.AdminMgmt" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TabsHolder" runat="server">
  <li class="back blank" id="tabBack" runat="server"><a href="AdminMgmt.aspx">返回</a></li>
  <li><a href="AdminMgmt.aspx">用户列表</a></li>
  <li class="add"><a href="AdminMgmt.aspx?m=add">添加用户</a></li>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentHolder" runat="server">
  <asp:MultiView ID="CommandView" runat="server">
    <asp:View ID="ListView" runat="server">
      <h4>用户管理</h4>
      <fieldset>
        <legend>查找用户</legend>
        <p>列表中的为现有后台用户。</p>
      </fieldset>
      <div class="lineholder"></div>
      <asp:ListView ID="dataList" runat="server" onitemdatabound="dataList_ItemDataBound">
        <LayoutTemplate>
          <table cellpadding="0" cellspacing="0" class="list-action">
            <tr>
              <th scope="col" class="left">ID</th>
              <th scope="col" class="left">帐号</th>
              <th scope="col" class="left">昵称</th>
              <th scope="col" class="left">电子邮件</th>
              <th scope="col">所属角色</th>
              <th scope="col">经销商帐号</th>
              <th scope="col">创建时间</th>
              <th scope="col">上次登录时间</th>
              <th scope="col">上次登录IP</th>
              <th scope="col">状态</th>
              <th scope="col" class="left">操作</th>
            </tr>
            <tr runat="server" id="itemPlaceholder" />
          </table>
        </LayoutTemplate>
        <ItemTemplate>
            <tr class="odd">
              <td><asp:Literal ID="lbID" runat="server" Text='<%# Eval("ID")%>'></asp:Literal></td>
              <td><a href="?m=detail&id=<%# Eval("ID")%>"><%# Eval("Name")%></a></td>
              <td><%# Eval("Display")%></td>
              <td><%# Eval("Email")%></td>
              <td class="center"><%# Eval("Role")%></td>
              <td class="center">X</td>
              <td class="center"><%# Eval("AddTime")%></td>
              <td class="center"><%# Eval("LastLogin")%></td>
              <td class="center"><%# Eval("LastIP")%></td>
              <td class="center"><%# Eval("Status")%></td>
              <td class="command">
                <asp:HyperLink ID="linkEdit" runat="server"><span class="ui-icon ui-icon-pencil"></span>编辑</asp:HyperLink>
                <asp:HyperLink ID="linkDel" NavigateUrl="#" runat="server" CssClass="ui-state-error-text"><span class="ui-icon ui-icon-close"></span>删除</asp:HyperLink>
              </td>
            </tr>
        </ItemTemplate>
        <AlternatingItemTemplate>
            <tr>
              <td><asp:Literal ID="lbID" runat="server" Text='<%# Eval("ID")%>'></asp:Literal></td>
              <td><a href="?m=detail&id=<%# Eval("ID")%>"><%# Eval("Name")%></a></td>
              <td><%# Eval("Display")%></td>
              <td><%# Eval("Email")%></td>
              <td class="center"><%# Eval("Role")%></td>
              <td class="center">X</td>
              <td class="center"><%# Eval("AddTime")%></td>
              <td class="center"><%# Eval("LastLogin")%></td>
              <td class="center"><%# Eval("LastIP")%></td>
              <td class="center"><%# Eval("Status")%></td>
              <td class="command">
                <asp:HyperLink ID="linkEdit" runat="server"><span class="ui-icon ui-icon-pencil"></span>编辑</asp:HyperLink>
                <asp:HyperLink ID="linkDel" NavigateUrl="#" runat="server" CssClass="ui-state-error-text"><span class="ui-icon ui-icon-close"></span>删除</asp:HyperLink>
              </td>
             </tr>
        </AlternatingItemTemplate>
        <EmptyDataTemplate>
          暂无数据。
        </EmptyDataTemplate>
      </asp:ListView>
    </asp:View>
    <asp:View ID="DetailView" runat="server">
      <h4>编辑用户</h4>
      <table class="form-table">
        <tr class="form-field">
          <th scope="row"><asp:Label ID="Label6" runat="server">登录名<span class="desc">(不可修改)</span></asp:Label></th>
          <td>
            <asp:Label ID="edtUser" runat="server" CssClass="label"></asp:Label>
          </td>
        </tr>
        <tr class="form-field">
          <th scope="row"><asp:Label ID="Label7" runat="server" AssociatedControlID="edtEmail">电子邮件<span class="desc">(必填)</span></asp:Label></th>
          <td>
            <asp:TextBox ID="edtEmail" runat="server" CssClass="textbox"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="此为必填项" ControlToValidate="edtEmail" Display="Dynamic"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ErrorMessage="电子邮件格式非法" ControlToValidate="edtEmail" Display="Dynamic" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
          </td>
        </tr>
        <tr class="form-field">
          <th scope="row"><asp:Label ID="Label8" runat="server" AssociatedControlID="edtName">姓名<span class="desc">(必填)</span></asp:Label></th>
          <td>
            <asp:TextBox ID="edtName" runat="server" CssClass="textbox"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="此为必填项" ControlToValidate="edtName" Display="Dynamic"></asp:RequiredFieldValidator>
          </td>
        </tr>
        <tr class="form-field" id="linePass" runat="server">
          <th scope="row"><asp:Label ID="Label12" runat="server" AssociatedControlID="edtPass1">修改密码<span class="desc">(留空则不修改)</span></asp:Label></th>
          <td>
            <p class="field-line">
              <asp:TextBox ID="edtPass1" runat="server" TextMode="Password" CssClass="textbox"></asp:TextBox>
              <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ErrorMessage="密码至少需要8位字符" ValidationExpression=".{8,}" ControlToValidate="edtPass1" Display="Dynamic"></asp:RegularExpressionValidator>
            </p>
            <p class="field-line">
              <asp:TextBox ID="edtPass2" runat="server" TextMode="Password" CssClass="textbox"></asp:TextBox>
              <asp:CompareValidator ID="CompareValidator2" runat="server" ErrorMessage="密码不匹配" ControlToCompare="edtPass1" ControlToValidate="edtPass2" Display="Dynamic"></asp:CompareValidator>
            </p>
            <p class="indicator-hint"><span class="ui-icon ui-icon-info" style="float:left;margin-right:3px;"></span>提示：登录密码应至少8位字符。为了保证安全性，应包含大小写字母、数字以及符号，例如 ! " ? $ % ^ &。</p>
          </td>
        </tr>
        <tr class="form-field">
          <th scope="row"><asp:Label ID="Label10" runat="server" AssociatedControlID="edtRole">角色</asp:Label></th>
          <td>
            <asp:DropDownList ID="edtRole" runat="server" CssClass="dropdown">
            </asp:DropDownList>
          </td>
        </tr>
        <tr class="form-field">
          <th scope="row"><asp:Label ID="Label9" runat="server">状态</asp:Label></th>
          <td>
            <asp:Label ID="edtStatus" runat="server" CssClass="label"></asp:Label>
          </td>
        </tr>
        <tr class="form-field" id="lineQuick" runat="server">
          <th scope="row"><asp:Label ID="Label11" runat="server">快速操作</asp:Label></th>
          <td>
            <button type="button" class="btn_submit" id="btn_resetpass">重设登录密码</button>
            <asp:Button ID="edtResetPassword" runat="server" Text="Button" CssClass="" OnClick="edtResetPassword_Click" />
            <uc:ModernButton ID="edtBan" runat="server" CssClass="btn_submit" OnClick="toggleUserStatus"></uc:ModernButton>
          </td>
        </tr>
      </table>
      <p class="submit">
        <uc:ModernButton ID="edtSubmit" runat="server" CssClass="btn_submit" OnClick="edtSubmit_Click">提交</uc:ModernButton>
      </p>
      <div id="dialog-confirm" title="重设用户的登录密码？" class="hide">
        <p><span class="ui-icon ui-icon-alert" style="float:left; margin:0 7px 20px 0;"></span>系统将生成一个随机的8位密码，继续？</p>
      </div>
      <script type="text/javascript">
        $(document).ready(function() {
          $(".btn_submit").button();
          $('#btn_resetpass').click(startReset);
        });
        function startReset(e) {
          $('#dialog-confirm').dialog({
            resizable: false,
            height: 110,
            modal: true,
            buttons: {
              "确定": function() {
                $('#<%=edtResetPassword.ClientID %>').click();
                //__doPostBack('<%=edtResetPassword.ClientID %>', 'OnClick');
              },
              "取消": function() {
                $(this).dialog("close");
              }
            }
          });
        }
      </script>
    </asp:View>
    <asp:View ID="CreateView" runat="server">
      <h4>添加用户</h4>
      <table class="form-table" cellpadding="0" cellspacing="0">
        <tr class="form-field">
          <th scope="row"><asp:Label ID="Label1" runat="server" AssociatedControlID="newUser">登录名<span class="desc">(必填)</span></asp:Label></th>
          <td>
            <asp:TextBox ID="newUser" runat="server" CssClass="textbox"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="此为必填项" ControlToValidate="newUser" Display="Dynamic"></asp:RequiredFieldValidator>
          </td>
        </tr>
        <tr class="form-field">
          <th scope="row"><asp:Label ID="Label2" runat="server" AssociatedControlID="newEmail">电子邮件<span class="desc">(必填)</span></asp:Label></th>
          <td>
            <asp:TextBox ID="newEmail" runat="server" CssClass="textbox"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="此为必填项" ControlToValidate="newEmail" Display="Dynamic"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="电子邮件格式非法" ControlToValidate="newEmail" Display="Dynamic" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
          </td>
        </tr>
        <tr class="form-field">
          <th scope="row"><asp:Label ID="Label3" runat="server" AssociatedControlID="newName">姓名<span class="desc">(必填)</span></asp:Label></th>
          <td>
            <asp:TextBox ID="newName" runat="server" CssClass="textbox"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="此为必填项" ControlToValidate="newName" Display="Dynamic"></asp:RequiredFieldValidator>
          </td>
        </tr>
        <tr class="form-field">
          <th scope="row"><asp:Label ID="Label4" runat="server" AssociatedControlID="newPass1">登录密码<span class="desc">(重复两次，必填)</span></asp:Label></th>
          <td>
            <p class="field-line">
              <asp:TextBox ID="newPass1" runat="server" TextMode="Password" CssClass="textbox"></asp:TextBox>
              <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="此为必填项" ControlToValidate="newPass1" Display="Dynamic"></asp:RequiredFieldValidator>
              <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="密码至少需要8位字符" ValidationExpression=".{8,}" ControlToValidate="newPass1" Display="Dynamic"></asp:RegularExpressionValidator>
            </p>
            <p class="field-line">
              <asp:TextBox ID="newPass2" runat="server" TextMode="Password" CssClass="textbox"></asp:TextBox>
              <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="此为必填项" ControlToValidate="newPass2" Display="Dynamic"></asp:RequiredFieldValidator>
              <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="密码不匹配" ControlToCompare="newPass1" ControlToValidate="newPass2" Display="Dynamic"></asp:CompareValidator>
            </p>
            <p class="indicator-hint"><span class="ui-icon ui-icon-info" style="float:left;margin-right:3px;"></span>提示：登录密码应至少8位字符。为了保证安全性，应包含大小写字母、数字以及符号，例如 ! " ? $ % ^ &。</p>
          </td>
        </tr>
        <tr class="form-field">
          <th scope="row"><asp:Label ID="Label5" runat="server" AssociatedControlID="newRole">角色</asp:Label></th>
          <td>
            <asp:DropDownList ID="newRole" runat="server" CssClass="dropdown">
            </asp:DropDownList>
          </td>
        </tr>
      </table>
      <p class="submit">
        <uc:ModernButton ID="newSubmit" runat="server" CssClass="btn_submit" OnClick="newSubmit_Click">添加新用户</uc:ModernButton>
      </p>
      <script type="text/javascript">
        $(document).ready(function() {
          $(".btn_submit").button();
        });
      </script>
    </asp:View>
    <asp:View ID="ResetPassword" runat="server">
      <h4>重设密码</h4>
      <div class="form-reset">
        <p>用户 <asp:Literal ID="rstUser" runat="server"></asp:Literal> 的密码已经重设，请牢记！</p>
        <code><asp:Literal ID="rstCode" runat="server"></asp:Literal></code>
      </div>
    </asp:View>
  </asp:MultiView>
  <asp:Literal runat="server" ID="jsBlock"></asp:Literal>
</asp:Content>
