using System;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI.WebControls;
using DataEntity;
using LRAdmin.Utility;
using LRAdmin.Entity;

namespace LRAdmin
{
    public partial class AdminMgmt : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string cmd = Request.QueryString["m"];
                switch (cmd)
                {
                    case "detail":
                        int uid;
                        if (!int.TryParse(Request.QueryString["id"], out uid))
                        {
                            Alert.ShowAlert(Page, "参数非法！", Alert.AlertState.OpenInThisWindow, "AdminMgmt.aspx");
                            return;
                        }
                        if ((int)Session["lr_admin_id"] == uid)
                        {
                            lineQuick.Visible = false;
                        }
                        else
                        {
                            linePass.Visible = false;
                        }
                        ShowDetailByUserID(uid);
                        //jsBlock.Text = "<script type=\"text/javascript\">$($('.content-tab li')[1]).addClass(\"cur\");</script>";
                        DetailView.DataBind();
                        CommandView.ActiveViewIndex = 1;
                        break;
                    case "profile":
                        int sid = (int)Session["lr_admin_id"];
                        lineQuick.Visible = false;
                        lineAuth.Visible = true;
                        tabBack.Visible = false;
                        Label12.Text = "新密码<span class=\"desc\">(留空则不修改)</span>";
                        lbDetail.Text = "修改个人资料";
                        ShowDetailByUserID(sid);
                        DetailView.DataBind();
                        CommandView.ActiveViewIndex = 1;
                        break;
                    case "add":
                        using (LandRoverDBDataContext ctx = new LandRoverDBDataContext())
                        {
                            var role = from r in ctx.L_Roles
                                       where r.Status == 1
                                       select new
                                       {
                                           ID = r.ID,
                                           Name = r.RoleName
                                       };
                            newRole.DataSource = role.ToList();
                            newRole.DataTextField = "Name";
                            newRole.DataValueField = "ID";
                        }
                        newRole.SelectedValue = "2";
                        CreateView.DataBind();
                        jsBlock.Text = "<script type=\"text/javascript\">$($('.content-tab li')[2]).addClass(\"cur\");</script>";
                        CommandView.ActiveViewIndex = 2;
                        break;
                    case "reset":
                        if (Session["lr_admin_reset"] == null)
                        {
                            Response.Redirect("~/AdminMgmt.aspx");
                            return;
                        }
                        string[] args = (string[])Session["lr_admin_reset"];
                        rstUser.Text = args[0];
                        rstCode.Text = args[1];
                        Session.Remove("lr_admin_reset");
                        CommandView.ActiveViewIndex = 3;
                        break;
                    default:
                        tabBack.Visible = false;
                        using (LandRoverDBDataContext ctx = new LandRoverDBDataContext())
                        {
                            var admin = from a in ctx.L_Administrators
                                        join r in ctx.L_Roles on a.RoleID equals r.ID
                                        where a.Status == 1
                                        select new
                                        {
                                            ID = a.ID,
                                            Name = a.Username,
                                            Display = a.DisplayName,
                                            Email = a.Email,
                                            Role = r.RoleName,
                                            AddTime = a.AddTime,
                                            LastLogin = a.LastLogin,
                                            LastIP = a.LastIP,
                                            Status = (UserStatus)a.Status
                                        };

                            dataList.DataSource = admin.ToList();
                            dataList.DataBind();
                        }
                        jsBlock.Text = "<script type=\"text/javascript\">$('.content-tab li:first').addClass(\"cur\");</script>";
                        CommandView.ActiveViewIndex = 0;
                        break;
                }
            }
        }

        private void ShowDetailByUserID(int uid)
        {
            using (LandRoverDBDataContext ctx = new LandRoverDBDataContext())
            {
                var user = (from a in ctx.L_Administrators
                            where a.ID == uid
                            select a).SingleOrDefault();

                var role = from r in ctx.L_Roles
                           where r.Status == 1
                           select new
                           {
                               ID = r.ID,
                               Name = r.RoleName
                           };
                if (user != null)
                {
                    edtRole.DataSource = role.ToList();
                    edtRole.DataTextField = "Name";
                    edtRole.DataValueField = "ID";
                    edtEmail.Text = user.Email;
                    edtName.Text = user.DisplayName;
                    edtRole.SelectedValue = user.RoleID.ToString();
                    edtUser.Text = user.Username;
                    edtStatus.Text = ((UserStatus)user.Status).ToString();
                    if ((UserStatus)user.Status == UserStatus.Banned)
                    {
                        edtBan.Text = "启用帐号";
                    }
                    else
                    {
                        edtBan.Text = "禁用帐号";
                    }
                }
                else
                {
                    Alert.ShowAlert(Page, "指定的用户不存在。", Alert.AlertState.OpenInThisWindow, "AdminMgmt.aspx");
                    return;
                }
            }
        }

        protected void dataList_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            Literal id = (Literal)e.Item.FindControl("lbID");
            HyperLink edit = (HyperLink)e.Item.FindControl("linkEdit");
            int uid = int.Parse(id.Text);
            edit.NavigateUrl = string.Format("?m=detail&id={0}", uid);

            HyperLink del = (HyperLink)e.Item.FindControl("linkDel");
            if ((int)Session["lr_admin_id"] == uid)
            {
                del.CssClass = "disable";
                del.Enabled = false;
                del.ToolTip = "不能删除当前登录的用户";
            }
            else
            {
                del.NavigateUrl = string.Format("m=del&id={0}", uid);
            }
        }

        protected void newSubmit_Click(object sender, EventArgs e)
        {
            if (IsValid)
            {
                using (LandRoverDBDataContext ctx = new LandRoverDBDataContext())
                {
                    var admin = from u in ctx.L_Administrators
                                where u.Username == newUser.Text.Trim()
                                select u.ID;

                    if (admin.Count() > 0)
                    {
                        Alert.ShowAlert(Page, "该登录名已存在，请重新输入!", Alert.AlertState.Nothing, string.Empty);
                        return;
                    }

                    L_Administrators ent = new L_Administrators();
                    ent.AddTime = DateTime.Now;
                    ent.DealerID = 0;
                    ent.DisplayName = newName.Text.Trim();
                    ent.Email = newEmail.Text.Trim();
                    ent.LastIP = Request.UserHostAddress;
                    ent.LastLogin = DateTime.Now;
                    ent.Username = newUser.Text.Trim();
                    ent.Password = Helper.GetPasswordString(ent.Username, newPass1.Text.Trim());
                    ent.RoleID = int.Parse(newRole.SelectedValue);
                    ent.Status = 1;

                    ctx.L_Administrators.InsertOnSubmit(ent);
                    ctx.SubmitChanges();
                }

                Alert.ShowAlert(HttpContext.Current, "添加成功!", Alert.AlertState.OpenInThisWindow, Request.Url.ToString());
            }
        }

        protected void edtSubmit_Click(object sender, EventArgs e)
        {
            if (IsValid)
            {
                int uid;
                if (!int.TryParse(Request.QueryString["id"], out uid))
                {
                    if (Request.QueryString["m"] == "profile")
                    {
                        uid = (int)Session["lr_admin_id"];
                    }
                    else
                    {
                        Alert.ShowAlert(Page, "参数非法！", Alert.AlertState.OpenInThisWindow, "AdminMgmt.aspx");
                        return;
                    }
                }
                using (LandRoverDBDataContext ctx = new LandRoverDBDataContext())
                {
                    L_Administrators admin;

                    if (lineAuth.Visible)
                    {
                        string oldpass = Helper.GetPasswordString((string)Session["lr_admin_user"], edtPass.Text.Trim());
                        admin = (from a in ctx.L_Administrators
                                 where a.ID == uid && a.Password == oldpass
                                 select a).SingleOrDefault();
                        if (admin == null)
                        {
                            Alert.ShowAlert(Page, "密码错误，请重试！", Alert.AlertState.Nothing, string.Empty);
                            return;
                        }
                    }
                    else
                    {
                        admin = (from u in ctx.L_Administrators
                                 where u.ID == uid
                                 select u).Single();
                    }

                    admin.DisplayName = edtName.Text.Trim();
                    if ((int)Session["lr_admin_id"] == uid && !string.IsNullOrEmpty(edtPass1.Text))
                    {
                        admin.Password = Helper.GetPasswordString(admin.Username, edtPass1.Text.Trim());
                    }
                    admin.Email = edtEmail.Text.Trim();
                    admin.RoleID = int.Parse(edtRole.SelectedValue);
                    ctx.SubmitChanges();
                }

                Alert.ShowAlert(HttpContext.Current, "修改成功!", Alert.AlertState.OpenInThisWindow, Request.Url.ToString());
            }
        }

        protected void edtResetPassword_Click(object sender, EventArgs e)
        {
            int uid;
            if (!int.TryParse(Request.QueryString["id"], out uid))
            {
                Alert.ShowAlert(Page, "参数非法！", Alert.AlertState.OpenInThisWindow, "AdminMgmt.aspx");
                return;
            }
            string newpass = Helper.GetRandString(8);

            using (LandRoverDBDataContext ctx = new LandRoverDBDataContext())
            {
                var admin = (from u in ctx.L_Administrators
                             where u.ID == uid
                             select u).Single();

                Session["lr_admin_reset"] = new string[] { admin.Username, newpass };
                admin.Password = Helper.GetPasswordString(admin.Username, newpass);
                ctx.SubmitChanges();
            }
            Response.Redirect(string.Format("~/AdminMgmt.aspx?m=reset&id={0}", uid));
        }

        protected void toggleUserStatus(object sender, EventArgs e)
        {
            int uid;
            if (!int.TryParse(Request.QueryString["id"], out uid))
            {
                Alert.ShowAlert(Page, "参数非法！", Alert.AlertState.OpenInThisWindow, "AdminMgmt.aspx");
                return;
            }
        }

        protected void CustomValidator1_Validate(object sender, ServerValidateEventArgs e)
        {
            if (!string.IsNullOrEmpty(edtPass1.Text) && string.IsNullOrEmpty(e.Value))
            {
                e.IsValid = false;
            }
            else
            {
                e.IsValid = true;
            }
        }
    }
}
