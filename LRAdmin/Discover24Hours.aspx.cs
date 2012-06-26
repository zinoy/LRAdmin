using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataEntity;
using LRAdmin.Entity;
using LRAdmin.Utility;

namespace LRAdmin
{
    public partial class Discover24Hours : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                /*Literal subTitle = (Literal)Master.FindControl("subTitle");
                subTitle.Text = "活动数据";
                using (LandRoverDBDataContext ctx = new LandRoverDBDataContext())
                {
                    var list = from u in ctx.NSD_User
                               select new
                               {
                                   Name = u.UName,
                                   Title = u.UTitle,
                                   Email = u.UEmail,
                                   Province = u.UProvince,
                                   City = u.UCity,
                                   Mobile = u.UMobile,
                                   HighScore = u.UScore,
                                   Weibo = u.Weibo,
                                   Place = u.Choose,
                                   Time = u.AddTime
                               };
                    dataList.DataSource = list.ToList();
                    dataList.DataBind();
                }*/

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
                var list = from u in ctx.NSD_User
                           where u.AddTime >= dfrom && u.AddTime < dto
                           select new Discover24HoursEntity
                           {
                               Name = u.UName,
                               Title = u.UTitle,
                               Email = u.UEmail,
                               Province = u.UProvince,
                               City = u.UCity,
                               Mobile = u.UMobile,
                               HighScore = u.UScore,
                               Weibo = u.Weibo,
                               Place = u.Choose,
                               Time = u.AddTime
                           };
                dataList.DataSource = list.ToList();
                dataList.DataBind();
                /*if (list.Count() > 0)
                    btnDown.Enabled = true;
                else
                    btnDown.Enabled = false;*/
            }
        }

        protected void btnDown_Click(object sender, EventArgs e)
        {
            DateTime dfrom = DateTime.Parse(tbDateFrom.Text);
            DateTime dto = DateTime.Parse(tbDateTo.Text).AddDays(1);

            using (LandRoverDBDataContext ctx = new LandRoverDBDataContext())
            {
                var list = from u in ctx.NSD_User
                           where u.AddTime >= dfrom && u.AddTime < dto
                           select new Discover24HoursEntity
                           {
                               ID = u.ID,
                               Name = u.UName,
                               Title = u.UTitle,
                               Email = u.UEmail,
                               Province = u.UProvince,
                               City = u.UCity,
                               Mobile = u.UMobile,
                               HighScore = u.UScore,
                               Weibo = u.Weibo,
                               Place = u.Choose,
                               Time = u.AddTime
                           };

                List<Discover24HoursEntity> data = list.ToList();
                if (data.Count == 0)
                    return;

                StringBuilder sb = new StringBuilder("ID,姓名,称呼,电子邮件,省份,城市,手机号,最高分,微博帐号,活动站点,注册时间");
                foreach (Discover24HoursEntity obj in data)
                {
                    sb.AppendLine();
                    sb.AppendFormat("{0},", obj.ID);
                    sb.AppendFormat("\"{0}\",", Helper.FormatForCSV(obj.Name));
                    sb.AppendFormat("{0},", obj.Title);
                    sb.AppendFormat("\"{0}\",", Helper.FormatForCSV(obj.Email));
                    sb.AppendFormat("{0},", obj.Province);
                    sb.AppendFormat("{0},", obj.City);
                    sb.AppendFormat("{0},", obj.Mobile);
                    sb.AppendFormat("{0},", obj.HighScore);
                    sb.AppendFormat("{0},", obj.Weibo);
                    sb.AppendFormat("{0},", obj.Place);
                    sb.AppendFormat("{0}", obj.Time);
                }
                string path = Helper.ExportAsCsvFile(sb);
                Response.Redirect(path);
            }
        }
    }
}
