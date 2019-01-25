using HRMSys.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace toolWebsevice
{
    /// <summary>
    /// FootHandler 的摘要说明
    /// </summary>
    public class FootHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/html";
            string html = SqlHelper.WriteTemplate("", "FootPage.html");
            context.Response.Write(html);
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