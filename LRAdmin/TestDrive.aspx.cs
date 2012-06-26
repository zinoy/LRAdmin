using System;
using System.Linq;
using System.Text;
using DataEntity;
using LRAdmin.Utility;

namespace LRAdmin
{
    public partial class TestDrive : System.Web.UI.Page
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

        protected void btnDown_Click(object sender, EventArgs e)
        {
            DateTime dfrom = DateTime.Parse(tbDateFrom.Text);
            DateTime dto = DateTime.Parse(tbDateTo.Text).AddDays(1);

            using (LandRoverOldDbDataContext ctx = new LandRoverOldDbDataContext())
            {
                var list = from u in ctx.shijia1
                           where u.times >= dfrom && u.times < dto
                           select u;

                var data = list.ToList();
                if (data.Count == 0)
                    return;

                StringBuilder sb = new StringBuilder("id,sex,username,date,sheng,city,address,youbian,email,dianhua,shouji,chexing,sjsj,jxssf,jxscs,jxs,lxfs,times,gcsj,gcys,phcx,phcx1,phcx2,phcx3,phcx4,owner,boughtdate,carbrand,carmodel,brochure");
                foreach (var obj in data)
                {
                    sb.AppendLine();
                    sb.AppendFormat("{0},", obj.id);
                    sb.AppendFormat("{0},", obj.sex);
                    sb.AppendFormat("\"{0}\",", Helper.FormatForCSV(obj.username));
                    sb.AppendFormat("{0},", obj.date);
                    sb.AppendFormat("{0},", obj.sheng);
                    sb.AppendFormat("{0},", obj.city);
                    sb.AppendFormat("\"{0}\",", Helper.FormatForCSV(obj.address));
                    sb.AppendFormat("\"{0}\",", Helper.FormatForCSV(obj.youbian));
                    sb.AppendFormat("\"{0}\",", Helper.FormatForCSV(obj.email));
                    sb.AppendFormat("\"{0}\",", Helper.FormatForCSV(obj.dianhua));
                    sb.AppendFormat("\"{0}\",", Helper.FormatForCSV(obj.shouji));
                    sb.AppendFormat("\"{0}\",", Helper.FormatForCSV(obj.chexing));
                    sb.AppendFormat("{0},", obj.sjsj);
                    sb.AppendFormat("{0},", obj.jxssf);
                    sb.AppendFormat("{0},", obj.jxscs);
                    sb.AppendFormat("{0},", obj.jxs);
                    sb.AppendFormat("{0},", obj.lxfs);
                    sb.AppendFormat("{0},", obj.times);
                    sb.AppendFormat("{0},", obj.gcsj);
                    sb.AppendFormat("{0},", obj.gcys);
                    sb.AppendFormat("{0},", obj.phcx);
                    sb.AppendFormat("{0},", obj.phcx1);
                    sb.AppendFormat("{0},", obj.phcx2);
                    sb.AppendFormat("{0},", obj.phcx3);
                    sb.AppendFormat("{0},", obj.phcx4);
                    sb.AppendFormat("{0},", obj.owner);
                    sb.AppendFormat("{0},", obj.boughtdate);
                    sb.AppendFormat("{0},", obj.carbrand);
                    sb.AppendFormat("\"{0}\",", Helper.FormatForCSV(obj.carmodel));
                    sb.AppendFormat("{0},", obj.brochure);
                }
                string path = Helper.ExportAsCsvFile(sb);
                Response.Redirect(path);
            }
        }
    }
}
