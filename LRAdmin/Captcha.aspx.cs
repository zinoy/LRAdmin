using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.IO;
using LRAdmin.Utility;

namespace LRAdmin
{
    public partial class Captcha : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string strkey = string.Empty;
            byte[] buffer = GenerateVerifyImage(5, ref strkey);
            //MemoryStream ms = new MemoryStream(buffer);
            //System.Drawing.Image img = System.Drawing.Image.FromStream(ms);
            Session[Helper.CaptchaKey] = strkey.ToUpper();
            Response.ContentType = "image/jpeg";
            Response.BinaryWrite(buffer);
            Response.End();
        }
        public byte[] GenerateVerifyImage(int nLen, ref string strKey)
        {
            int nBmpWidth = 15 * nLen + 5;
            int nBmpHeight = 35;
            System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(nBmpWidth, nBmpHeight);

            // 1. 生成随机背景颜色
            int nRed, nGreen, nBlue;  // 背景的三元色
            System.Random rd = new Random((int)System.DateTime.Now.Ticks);
            nRed = rd.Next(255) % 128 + 128;
            nGreen = rd.Next(255) % 128 + 128;
            nBlue = rd.Next(255) % 128 + 128;

            // 2. 填充位图背景
            System.Drawing.Graphics graph = System.Drawing.Graphics.FromImage(bmp);
            graph.FillRectangle(new SolidBrush(System.Drawing.Color.FromArgb(nRed, nGreen, nBlue))
             , 0
             , 0
             , nBmpWidth
             , nBmpHeight);


            // 3. 绘制干扰线条，采用比背景略深一些的颜色
            int nLines = 3;
            System.Drawing.Pen pen = new System.Drawing.Pen(System.Drawing.Color.FromArgb(nRed - 17, nGreen - 17, nBlue - 17), 2);
            for (int a = 0; a < nLines; a++)
            {
                int x1 = rd.Next() % nBmpWidth;
                int y1 = rd.Next() % nBmpHeight;
                int x2 = rd.Next() % nBmpWidth;
                int y2 = rd.Next() % nBmpHeight;
                graph.DrawLine(pen, x1, y1, x2, y2);
            }

            // 采用的字符集，可以随即拓展，并可以控制字符出现的几率
            string strCode = "23456789CDEFGHJKLMNPQRTWXY";

            // 4. 循环取得字符，并绘制
            string strResult = "";
            int offset = rd.Next(3, 12);
            for (int i = 0; i < nLen; i++)
            {
                int x = (i * 13 + rd.Next(3)) + 4;
                int y = rd.Next(4) + offset;

                // 确定字体
                System.Drawing.Font font = new System.Drawing.Font("Courier New",
                 14 + rd.Next() % 4,
                 System.Drawing.FontStyle.Bold);
                char c = strCode[rd.Next(strCode.Length)];  // 随机获取字符
                strResult += c.ToString();

                string charstr;
                if (rd.Next(3) == 0)
                {
                    charstr = c.ToString().ToLower();
                }
                else
                {
                    charstr = c.ToString();
                }

                // 绘制字符
                graph.DrawString(charstr,
                 font,
                 new SolidBrush(System.Drawing.Color.FromArgb(nRed - 60 + y * 3, nGreen - 60 + y * 3, nBlue - 40 + y * 3)),
                 x,
                 y);
            }

            // 5. 输出字节流
            System.IO.MemoryStream bstream = new System.IO.MemoryStream();
            bmp.Save(bstream, System.Drawing.Imaging.ImageFormat.Jpeg);
            bmp.Dispose();
            graph.Dispose();

            strKey = strResult.ToUpper();
            byte[] byteReturn = bstream.ToArray();
            bstream.Close();

            return byteReturn;
        }
    }
}
