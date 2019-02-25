using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace toolWebsevice
{
    /// <summary>
    /// UserHandler 的摘要说明
    /// </summary>
    public class UserHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/html";
            context.Response.AddHeader("Access-Control-Allow-Origin", "*");
            string username = context.Request["username"];
            BLL bll = new BLL();
            if (!string.IsNullOrEmpty(username))
            {
                //公司/会员信息
                cmUserInfo uInfo = bll.GetUser(string.Format("where username='{0}'", username));
                context.Response.Write(uInfo);
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