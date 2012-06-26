using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace LRAdmin.Utility
{
    /// <summary>
    /// 向用户提示对话框的类
    /// </summary>
    public class Alert
    {
        /// <summary>
        /// 输出模式枚举
        /// </summary>
        public enum AlertState
        {
            /// <summary>
            /// 在父窗口中打开该连接
            /// </summary>
            OpenInParentWindow,
            /// <summary>
            /// 在当前窗口中打开该连接
            /// </summary>
            OpenInThisWindow,
            /// <summary>
            /// 返回到上一个页面
            /// </summary>
            Back,
            /// <summary>
            /// 关闭窗口
            /// </summary>
            CloseWindow,
            /// <summary>
            /// 不做任何操作
            /// </summary>
            Nothing
        }

        /// <summary>
        /// 输出JS提示信息对话框
        /// </summary>
        /// <param name="context">页面Context</param>
        /// <param name="Msg">消息内容</param>
        /// <param name="Astate">输出模式 * 枚举</param>
        /// <param name="ToUrl">提示后要转到的URL</param>
        public static void ShowAlert(HttpContext context, string Msg, AlertState Astate, string ToUrl)
        {
            System.Text.StringBuilder mySB = new System.Text.StringBuilder();

            mySB.AppendFormat("<script type=\"text/javascript\">alert(\"{0}\");", Msg);

            switch (Astate)
            {
                case AlertState.Back:
                    mySB.Append("history.go(-1);");
                    break;

                case AlertState.CloseWindow:
                    mySB.Append("top.window.close();");
                    break;

                case AlertState.OpenInParentWindow:
                    mySB.AppendFormat("top.location = '{0}';", ToUrl);
                    break;

                case AlertState.OpenInThisWindow:
                    mySB.AppendFormat("window.location = '{0}';", ToUrl);
                    break;
                case AlertState.Nothing:
                    break;
            }

            mySB.Append("</script>");

            context.Response.Write(mySB.ToString());

        }

        /// <summary>
        /// 输出JS提示信息对话框
        /// </summary>
        /// <param name="context">页面对象</param>
        /// <param name="Msg">消息内容</param>
        /// <param name="Astate">输出模式 * 枚举</param>
        /// <param name="ToUrl">提示后要转到的URL</param>
        public static void ShowAlert(Page context, string Msg, AlertState Astate, string ToUrl)
        {
            System.Text.StringBuilder mySB = new System.Text.StringBuilder();

            mySB.AppendFormat("<script type=\"text/javascript\">alert(\"{0}\");", Msg);

            switch (Astate)
            {
                case AlertState.Back:
                    mySB.Append("history.go(-1);");
                    break;

                case AlertState.CloseWindow:
                    mySB.Append("top.window.close();");
                    break;

                case AlertState.OpenInParentWindow:
                    mySB.AppendFormat("top.location = '{0}';", ToUrl);
                    break;

                case AlertState.OpenInThisWindow:
                    mySB.AppendFormat("window.location = '{0}';", ToUrl);
                    break;
                case AlertState.Nothing:
                    break;
            }

            mySB.Append("</script>");

            context.ClientScript.RegisterClientScriptBlock(typeof(string), "alertmsg", mySB.ToString());
        }
    }
}