using AutoSend;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;

namespace toolWebsevice
{
    /// <summary>
    /// imgHandler 的摘要说明
    /// </summary>
    public class imgHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "image/Gif";
            context.Response.AddHeader("Access-Control-Allow-Origin", "*");
            System.Drawing.Bitmap image = new System.Drawing.Bitmap(150, 25);
            Graphics g = Graphics.FromImage(image);
            string key = "toolWebs";//密钥
            string iv = "100dh888";//偏移量
            string txt = context.Request["txt"];
            if (string.IsNullOrEmpty(txt))
                context.Response.Write("");
            try
            {
                txt =Tools.Decode(txt, key, iv);
                //得到Bitmap(传入Rectangle.Empty自动计算宽高)
                Font font = new System.Drawing.Font("Arial", 14, (System.Drawing.FontStyle.Regular));
                //System.Drawing.Drawing2D.LinearGradientBrush brush1 = new System.Drawing.Drawing2D.LinearGradientBrush(new Rectangle(0, 0, image.Width, image.Height), Color.Red, Color.Blue, 1.2F, true);
                g.FillRectangle(new SolidBrush(Color.White), new Rectangle(0, 0, image.Width, image.Height));
                SolidBrush brush = new SolidBrush(Color.Red);
                g.DrawString(txt, font, brush, 2, 2);
                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                image.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
                context.Response.BinaryWrite(ms.ToArray());
            }
            catch (Exception ex)
            {
                context.Response.Write("");
            }
            finally
            {
                g.Dispose();
                image.Dispose();
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}