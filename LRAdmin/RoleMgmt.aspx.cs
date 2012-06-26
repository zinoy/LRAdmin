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
    public partial class RoleMgmt : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string cmd = Request.QueryString["m"];
                switch (cmd)
                {
                    case "detail":
                        jsBlock.Text = "<script type=\"text/javascript\">$($('.content-tab li')[1]).addClass(\"cur\");</script>";
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
                        CreateView.DataBind();
                        jsBlock.Text = "<script type=\"text/javascript\">$($('.content-tab li')[2]).addClass(\"cur\");</script>";
                        CommandView.ActiveViewIndex = 2;
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

        protected void dataList_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            Literal id = (Literal)e.Item.FindControl("lbID");
            HyperLink edit = (HyperLink)e.Item.FindControl("linkEdit");
            edit.NavigateUrl = string.Format("?m=detail&id={0}", id.Text);
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
    }
}
