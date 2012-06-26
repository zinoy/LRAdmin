using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataEntity;
using LRAdmin.Utility;

namespace LRAdmin
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string action = Request.QueryString["ac"];
                switch (action)
                {
                    case "logout":
                        Session.Remove(Helper.UserKey);
                        Session.Remove("lr_admin_id");
                        Session.Remove("lr_admin_name");
                        Session.Remove("lr_admin_role");
                        break;
                    default:
                        break;
                }
                pInfo.Visible = false;
            }
        }

        protected void btnGo_Click(object sender, EventArgs e)
        {
            if (IsValid)
            {
                if (tbCode.Text.Trim().ToUpper() != (string)Session[Helper.CaptchaKey])
                {
                    //Alert.ShowAlert(Page, "验证码错误。", Alert.AlertState.Nothing, string.Empty);
                    msgError.Text = "验证码错误。";
                    return;
                }

                string passStr = Helper.GetPasswordString(tbName.Text, tbPass.Text);

                using (LandRoverDBDataContext ctx = new LandRoverDBDataContext())
                {
                    var admin = (from a in ctx.L_Administrators
                                 where a.Username == tbName.Text && a.Password == passStr
                                 select a).SingleOrDefault();

                    if (admin != null)
                    {
                        switch (admin.Status)
                        {
                            case 1:
                                admin.LastIP = Request.UserHostAddress;
                                admin.LastLogin = DateTime.Now;
                                ctx.SubmitChanges();

                                Session[Helper.UserKey] = admin.Username;
                                Session["lr_admin_id"] = admin.ID;
                                Session["lr_admin_name"] = admin.DisplayName;
                                Session["lr_admin_role"] = admin.RoleID;

                                string redirect = Request.QueryString["rt"];
                                if (string.IsNullOrEmpty(redirect))
                                {
                                    Response.Redirect("~/KeepMeInformed.aspx");
                                }
                                else
                                {
                                    Response.Redirect(redirect);
                                }
                                break;
                            case 2:
                                //Alert.ShowAlert(Page, "帐号被禁用，请联系管理员。", Alert.AlertState.Nothing, string.Empty);
                                msgError.Text = "帐号被禁用，请联系管理员。";
                                break;
                            default:
                                break;
                        }
                    }
                    else
                    {
                        //Alert.ShowAlert(Page, "用户名或密码错误。", Alert.AlertState.Nothing, string.Empty);
                        msgError.Text = "用户名或密码错误。";
                        return;
                    }
                }
            }
        }
    }
}
