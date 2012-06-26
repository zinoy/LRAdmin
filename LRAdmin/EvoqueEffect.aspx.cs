using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using DataEntity;
using LRAdmin.Utility;

namespace LRAdmin
{
    public partial class EvoqueEffect : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                double offset = DateTime.Today.DayOfWeek == DayOfWeek.Sunday ? -6.0 : (int)DateTime.Today.DayOfWeek * -1.0;

                DateTime dto = DateTime.Today.AddDays(offset);
                DateTime dfrom = dto.AddDays(-6);
                tbDateFrom.Text = dfrom.ToString("yyyy-MM-dd");
                tbDateTo.Text = dto.ToString("yyyy-MM-dd");
            }
        }

        protected void btnFilter_Click(object sender, EventArgs e)
        {
            DateTime dfrom = DateTime.Parse(tbDateFrom.Text);
            DateTime dto = DateTime.Parse(tbDateTo.Text).AddDays(1);

            using (LandRoverOldDbDataContext ctx = new LandRoverOldDbDataContext())
            {
                var list = from u in ctx.users
                           where u.times >= dfrom && u.times < dto && u.receive == "effect2011"
                           select new
                           {
                               ID = u.id,
                               Name = u.username,
                               Email = u.email,
                               Phone = u.phone,
                               Sub = u.enews,
                               Time = u.times,
                               Source = u.receive
                           };
                dataList.DataSource = list.ToList();
                dataList.DataBind();
            }
        }

        protected void btnDown_Click(object sender, EventArgs e)
        {
            DateTime dfrom = DateTime.Parse(tbDateFrom.Text);
            DateTime dto = DateTime.Parse(tbDateTo.Text).AddDays(1);

            using (LandRoverOldDbDataContext ctx = new LandRoverOldDbDataContext())
            {
                var list = from u in ctx.users
                           where u.times >= dfrom && u.times < dto && u.receive == "effect2011"
                           select new
                           {
                               ID = u.id,
                               Name = u.username,
                               Email = u.email,
                               Phone = u.phone,
                               Sub = u.enews,
                               Time = u.times,
                               Source = u.receive
                           };

                var data = list.ToList();
                if (data.Count == 0)
                    return;

                StringBuilder sb = new StringBuilder("id,username,email,phone,enews,times,receive");
                Regex quote = new Regex("\"");
                foreach (var obj in data)
                {
                    sb.AppendLine();
                    sb.AppendFormat("{0},", obj.ID);
                    sb.AppendFormat("\"{0}\",", Helper.FormatForCSV(obj.Name));
                    sb.AppendFormat("\"{0}\",", Helper.FormatForCSV(obj.Email));
                    sb.AppendFormat("{0},", obj.Phone);
                    sb.AppendFormat("{0},", obj.Sub);
                    sb.AppendFormat("{0},", obj.Time);
                    sb.AppendFormat("{0},", obj.Source);
                }
                string path = Helper.ExportAsCsvFile(sb);
                Response.Redirect(path);
            }
        }
    }
}
