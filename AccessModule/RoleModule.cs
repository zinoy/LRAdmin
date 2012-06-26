using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Caching;
using DataEntity;
using System.IO;

namespace AccessModule
{
    public class RoleModule : IHttpModule
    {

        #region IHttpModule 成员

        public void Dispose()
        {
            //throw new NotImplementedException();
        }

        public void Init(HttpApplication context)
        {
            context.AcquireRequestState += new EventHandler(OnAcquireRequestState);
            //context.PostAcquireRequestState += new EventHandler(app_PostAcquireRequestState);

        }

        #endregion

        public delegate void MyEventHandler(Object s, EventArgs e);
        private MyEventHandler _eventHandler = null;


        /// <summary>  
        /// 获取当前appdomain中的cache  
        /// </summary>  
        public static Cache CurrentCache
        {
            get { return HttpRuntime.Cache; }
        }

        public event MyEventHandler MyEvent
        {
            add { _eventHandler += value; }
            remove { _eventHandler -= value; }
        }

        public void OnAcquireRequestState(Object obj, EventArgs e)
        {
            HttpApplication app = obj as HttpApplication;
            HttpContext cnt = app.Context;
            FileInfo info = new FileInfo(cnt.Request.Path);
            if (info.Extension != ".aspx")
            {
                return;
            }
            if (info.Name.ToLower() == "default.aspx" || info.Name.ToLower() == "captcha.aspx")
            {
                return;
            }

            if (CurrentCache["lr_roleData"] == null)
            {
                using (LandRoverDBDataContext ctx = new LandRoverDBDataContext())
                {
                    var role = from r in ctx.L_Roles
                               where r.Status == 1
                               select r;

                    CurrentCache.Insert("lr_roleData", role.ToList(), null, DateTime.Now.AddDays(15), Cache.NoSlidingExpiration);
                }
            }

            List<L_Roles> list = (List<L_Roles>)CurrentCache["lr_roleData"];
            if (cnt.Session["lr_admin_id"] != null && cnt.Session["lr_admin_role"] != null)
            {
                int rid = (int)cnt.Session["lr_admin_role"];
                var robj = list.Where(r => r.ID == rid).SingleOrDefault();
                if (robj == null)
                {
                    cnt.Session.Remove("lr_admin_rolename");
                    cnt.Response.Write("invalid role id");
                }
                else
                {
                    cnt.Session["lr_admin_rolename"] = robj.RoleName;
                }
            }
            else
            {
                cnt.Response.Redirect(string.Format("Default.aspx?rt={0}", HttpUtility.UrlEncode(cnt.Request.Url.ToString())));
            }

            if (_eventHandler != null)
                _eventHandler(this, null);
        }
    }
}
