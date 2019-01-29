using AutoSend;
using HRMSys.DAL;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace toolWebsevice
{
    /// <summary>
    /// rightPageHandler 的摘要说明
    /// </summary>
    public class rightPageHandler : IHttpHandler
    {
        private BLL bll = new BLL();
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/html";
            context.Response.AddHeader("Access-Control-Allow-Origin", "*");
            string uname = context.Request["uname"];
            string realmId = context.Request["realmId"];
            if (!string.IsNullOrEmpty(uname))
            {
                //公司/会员信息
                cmUserInfo uInfo = bll.GetUser(string.Format("where username='{0}'", uname));
                string key = "toolWebs";//密钥
                string iv = "100dh888";//偏移量
                string userTel = Tools.Encode(uInfo.telephone, key, iv);
                var data = new
                {
                    userInfo = uInfo,
                    userTel//电话号码
                };
                string html = SqlHelper.WriteTemplate(data, "RightFloatPage.html");
                context.Response.Write(html);
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