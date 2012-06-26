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
    /// ���û���ʾ�Ի������
    /// </summary>
    public class Alert
    {
        /// <summary>
        /// ���ģʽö��
        /// </summary>
        public enum AlertState
        {
            /// <summary>
            /// �ڸ������д򿪸�����
            /// </summary>
            OpenInParentWindow,
            /// <summary>
            /// �ڵ�ǰ�����д򿪸�����
            /// </summary>
            OpenInThisWindow,
            /// <summary>
            /// ���ص���һ��ҳ��
            /// </summary>
            Back,
            /// <summary>
            /// �رմ���
            /// </summary>
            CloseWindow,
            /// <summary>
            /// �����κβ���
            /// </summary>
            Nothing
        }

        /// <summary>
        /// ���JS��ʾ��Ϣ�Ի���
        /// </summary>
        /// <param name="context">ҳ��Context</param>
        /// <param name="Msg">��Ϣ����</param>
        /// <param name="Astate">���ģʽ * ö��</param>
        /// <param name="ToUrl">��ʾ��Ҫת����URL</param>
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
        /// ���JS��ʾ��Ϣ�Ի���
        /// </summary>
        /// <param name="context">ҳ�����</param>
        /// <param name="Msg">��Ϣ����</param>
        /// <param name="Astate">���ģʽ * ö��</param>
        /// <param name="ToUrl">��ʾ��Ҫת����URL</param>
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