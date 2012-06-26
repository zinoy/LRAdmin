using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LRAdmin
{
    public partial class Frames : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["lr_admin_name"] != null && Session["lr_admin_rolename"] != null)
            {
                lbUser.Text = (string)Session["lr_admin_name"];
                lbRole.Text = (string)Session["lr_admin_rolename"];
            }
        }
    }
}
