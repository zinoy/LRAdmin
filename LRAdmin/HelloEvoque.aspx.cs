using System;
using System.Linq;
using System.Text;
using DataEntity;
using LRAdmin.Utility;

namespace LRAdmin
{
    public partial class HelloEvoque : System.Web.UI.Page
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

            using (LandRoverDBDataContext ctx = new LandRoverDBDataContext())
            {
                var list = from u in ctx.R_HelloEvoque
                           where u.HDate >= dfrom && u.HDate < dto
                           select new
                           {
                               ID = u.ID,
                               Name = u.HName,
                               Email = u.HEmail,
                               Phone = u.HPhone,
                               Sub = u.HSubscription,
                               Time = u.HDate,
                               Source = u.HSource,
                               Province = u.HProvince,
                               City = u.HCity,
                               Way = u.HWay,
                               Title = u.HTitle
                           };
                dataList.DataSource = list.ToList();
                dataList.DataBind();
            }
        }

        protected void btnDown_Click(object sender, EventArgs e)
        {
            DateTime dfrom = DateTime.Parse(tbDateFrom.Text);
            DateTime dto = DateTime.Parse(tbDateTo.Text).AddDays(1);

            using (LandRoverDBDataContext ctx = new LandRoverDBDataContext())
            {
                var list = from u in ctx.R_HelloEvoque
                           where u.HDate >= dfrom && u.HDate < dto
                           select u;

                var data = list.ToList();
                if (data.Count == 0)
                    return;

                StringBuilder sb = new StringBuilder("ID,HName,HTitle,HPhone,HEmail,HProvince,HCity,HWay,HSubscription,HIp,HDate,HSource");
                foreach (var obj in data)
                {
                    sb.AppendLine();
                    sb.AppendFormat("{0},", obj.ID);
                    sb.AppendFormat("\"{0}\",", Helper.FormatForCSV(obj.HName));
                    sb.AppendFormat("{0},", obj.HTitle);
                    sb.AppendFormat("\"{0}\",", Helper.FormatForCSV(obj.HPhone));
                    sb.AppendFormat("\"{0}\",", Helper.FormatForCSV(obj.HEmail));
                    sb.AppendFormat("{0},", obj.HProvince);
                    sb.AppendFormat("{0},", obj.HCity);
                    sb.AppendFormat("{0},", obj.HWay);
                    sb.AppendFormat("{0},", obj.HSubscription);
                    sb.AppendFormat("{0},", obj.HIp);
                    sb.AppendFormat("{0},", obj.HDate);
                    sb.AppendFormat("{0},", obj.HSource);
                }
                string path = Helper.ExportAsCsvFile(sb);
                Response.Redirect(path);
            }
        }
    }
}
