using System;
using System.Configuration;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace LRAdmin.Utility
{
    /// <summary>
    /// 项目的辅助类。
    /// </summary>
    public static class Helper
    {
        /// <summary>
        /// 返回配置文件中定义的验证码Session使用的键值。
        /// </summary>
        public static string CaptchaKey = ConfigurationManager.AppSettings["CaptchaKey"];
        /// <summary>
        /// 返回配置文件中定义的用户名Session使用的键值。
        /// </summary>
        public static string UserKey = ConfigurationManager.AppSettings["UserKey"];
        /// <summary>
        /// 返回配置文件中定义的下载文件临时目录。
        /// </summary>
        public static string TempFolder = ConfigurationManager.AppSettings["TempFolder"];
        /// <summary>
        /// 返回配置文件中定义的缓存超时时间（单位：天）。
        /// </summary>
        public static int CacheTimeout = int.Parse(ConfigurationManager.AppSettings["CacheTimeout"]);
        /// <summary>
        /// 将传入的字符串按照MD5进行加密。
        /// </summary>
        /// <param name="orignal">待加密的字符串。</param>
        /// <returns>返回加密后的字符串（小写）。</returns>
        public static string MD5(string orignal)
        {
            return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(orignal, "md5").ToLower();
        }
        /// <summary>
        /// 将指定的字符串用SHA1进行加密处理。
        /// </summary>
        /// <param name="orignal">待加密的字符串。</param>
        /// <returns>返回加密后的字符串（小写）。</returns>
        public static string SHA1(string orignal)
        {
            return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(orignal, "sha1").ToLower();
        }
        /// <summary>
        /// 获取加密后的密码字符串。
        /// </summary>
        /// <param name="name">用户名。</param>
        /// <param name="pass">原始密码。</param>
        /// <returns>返回用于数据存储的密码字符串。</returns>
        public static string GetPasswordString(string name, string pass)
        {
            StringBuilder p = new StringBuilder(Helper.MD5(pass));
            p.Insert(0, name);
            return Helper.SHA1(p.ToString());
        }
        /// <summary>
        /// 生成一个指定长度的随机字符串。
        /// </summary>
        /// <param name="len">长度。</param>
        /// <returns>返回生成的字符串。</returns>
        public static string GetRandString(int len)
        {
            string basestr = "234579defghjmnrtyBDEFGHMNQRTY";
            Random rand = new Random();
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < len; i++)
            {
                sb.Append(basestr[rand.Next(basestr.Length)]);
            }
            return sb.ToString();
        }
        /// <summary>
        /// 将指定的文本保存为逗号分割符（CSV）文件，并返回该文件的URL。
        /// </summary>
        /// <param name="builder">要保存的文本。</param>
        /// <returns>返回文件的URL。</returns>
        public static string ExportAsCsvFile(StringBuilder builder)
        {
            string user = (string)HttpContext.Current.Session[UserKey];
            string folder = string.Format("{0}/{1:0000}/{2:00}", TempFolder, DateTime.Today.Year, DateTime.Today.Month);

            string path = HttpContext.Current.Server.MapPath(folder);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            string url;
            if (user == null)
            {
                url = string.Format("{0}/export_{1:00}{2}.csv", folder, DateTime.Today.Day, GetRandString(6));
            }
            else
            {
                url = string.Format("{0}/{1}_export_{2:00}{3}.csv", folder, user, DateTime.Today.Day, GetRandString(6));
            }

            string filepath = HttpContext.Current.Server.MapPath(url);
            if (File.Exists(filepath))
            {
                File.Delete(filepath);
            }
            using (StreamWriter sw = new StreamWriter(filepath, false, Encoding.UTF8))
            {
                sw.Write(builder.ToString());
            }

            return url;
        }
        /// <summary>
        /// 将单个双引号转为两个连续的双引号以便在CSV中使用。
        /// </summary>
        /// <param name="input">需要格式化的字符串。</param>
        /// <returns>格式化后的字符串。</returns>
        public static string FormatForCSV(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return string.Empty;
            }
            Regex q = new Regex("\"");
            return q.Replace(input, "\"\"");
        }
    }
}
