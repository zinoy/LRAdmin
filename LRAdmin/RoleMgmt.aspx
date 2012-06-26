<%@ Page Title="" Language="C#" MasterPageFile="~/Frames.Master" AutoEventWireup="true" CodeBehind="RoleMgmt.aspx.cs" Inherits="LRAdmin.RoleMgmt" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TabsHolder" runat="server">
  <li class="back blank" id="tabBack" runat="server"><a href="AdminMgmt.aspx">返回</a></li>
  <li><a href="AdminMgmt.aspx">用户列表</a></li>
  <li class="add"><a href="AdminMgmt.aspx?m=add">添加用户</a></li>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentHolder" runat="server">
  <asp:MultiView ID="CommandView" runat="server">
    <asp:View ID="ListView" runat="server">
      <h4>角色管理</h4>
      <fieldset>
        <legend>查找用户</legend>
        <p>列表中的为现有后台用户。</p>
      </fieldset>
      <div class="lineholder"></div>
      <asp:ListView ID="dataList" runat="server" onitemdatabound="dataList_ItemDataBound">
        <LayoutTemplate>
          <table cellpadding="0" cellspacing="0">
            <tr>
              <th scope="col">ID</th>
              <th scope="col">帐号</th>
              <th scope="col">昵称</th>
              <th scope="col">电子邮件</th>
              <th scope="col">所属角色</th>
              <th scope="col">经销商帐号</th>
              <th scope="col">创建时间</th>
              <th scope="col">上次登录时间</th>
              <th scope="col">上次登录IP</th>
              <th scope="col">状态</th>
              <th scope="col">操作</th>
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
              <td><asp:HyperLink ID="linkEdit" runat="server">编辑</asp:HyperLink></td>
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
              <td><asp:HyperLink ID="linkEdit" runat="server">编辑</asp:HyperLink></td>
             </tr>
        </AlternatingItemTemplate>
        <EmptyDataTemplate>
          暂无数据。
        </EmptyDataTemplate>
      </asp:ListView>
    </asp:View>
    <asp:View ID="DetailView" runat="server">
    detail
    </asp:View>
    <asp:View ID="CreateView" runat="server">
      <h4>添加用户</h4>
      <table class="form-table">
        <tr class="form-field">
          <th scope="row"><asp:Label ID="Label1" runat="server" Text="Label" AssociatedControlID="newUser">登录名<span class="desc">(必填)</span></asp:Label></th>
          <td>
            <asp:TextBox ID="newUser" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="此为必填项" ControlToValidate="newUser" Display="Dynamic"></asp:RequiredFieldValidator>
          </td>
        </tr>
        <tr class="form-field">
          <th scope="row"><asp:Label ID="Label2" runat="server" Text="Label" AssociatedControlID="newEmail">电子邮件<span class="desc">(必填)</span></asp:Label></th>
          <td>
            <asp:TextBox ID="newEmail" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="此为必填项" ControlToValidate="newEmail" Display="Dynamic"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="电子邮件格式非法" ControlToValidate="newEmail" Display="Dynamic" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
          </td>
        </tr>
        <tr class="form-field">
          <th scope="row"><asp:Label ID="Label3" runat="server" Text="Label" AssociatedControlID="newName">姓名<span class="desc">(必填)</span></asp:Label></th>
          <td>
            <asp:TextBox ID="newName" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="此为必填项" ControlToValidate="newName" Display="Dynamic"></asp:RequiredFieldValidator>
          </td>
        </tr>
        <tr class="form-field">
          <th scope="row"><asp:Label ID="Label4" runat="server" Text="Label" AssociatedControlID="newPass1">登录密码<span class="desc">(重复两次，必填)</span></asp:Label></th>
          <td>
            <asp:TextBox ID="newPass1" runat="server" TextMode="Password"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="此为必填项" ControlToValidate="newPass1" Display="Dynamic"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="密码至少需要8位字符" ValidationExpression=".{8,}" ControlToValidate="newPass1" Display="Dynamic"></asp:RegularExpressionValidator>
            <br />
            <asp:TextBox ID="newPass2" runat="server" TextMode="Password"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="此为必填项" ControlToValidate="newPass2" Display="Dynamic"></asp:RequiredFieldValidator>
            <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="密码不匹配" ControlToCompare="newPass1" ControlToValidate="newPass2" Display="Dynamic"></asp:CompareValidator>
            <p class="indicator-hint">提示：登录密码应至少8位字符。为了保证安全性，应包含大小写字母、数字以及符号，例如 ! " ? $ % ^ &。</p>
          </td>
        </tr>
        <tr class="form-field">
          <th scope="row"><asp:Label ID="Label5" runat="server" Text="Label" AssociatedControlID="newRole">角色</asp:Label></th>
          <td>
            <asp:DropDownList ID="newRole" runat="server">
            </asp:DropDownList>
          </td>
        </tr>
      </table>
      <p class="submit">
        <uc:ModernButton ID="newSubmit" runat="server" CssClass="btn_newsubmit" OnClick="newSubmit_Click">添加新用户</uc:ModernButton>
      </p>
      <script type="text/javascript">
        $(document).ready(function() {
          $(".btn_newsubmit").button();
        });
      </script>
    </asp:View>
  </asp:MultiView>
  <asp:Literal runat="server" ID="jsBlock"></asp:Literal>
</asp:Content>
