using AutoSend;
using Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Web;
using System.Web.SessionState;

namespace toolWebsevice
{
    /// <summary>
    /// LoginHandler 的摘要说明
    /// </summary>
    public class LoginHandler : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            StringBuilder _strContent = new StringBuilder();
            if (_strContent.Length == 0)
            {
                string _strAction = context.Request.Params["action"];
                if (string.IsNullOrEmpty(_strAction))
                {
                    _strContent.Append(_strContent.Append("404.html"));
                }
                else
                {
                    switch (_strAction.Trim())
                    {
                        case "Login": _strContent.Append(Login(context)); break;
                        default: break;
                    }
                }
            }
            context.Response.Write(_strContent.ToString());
        }
        private BLL bll = new BLL();
        public string Login(HttpContext context)
        {
            DateTime s, n;
            s = DateTime.Now;
            n = DateTime.Now;
            try
            {
                string username = context.Request["username"];
                string password = context.Request["password"];
                string dosubmit = context.Request["dosubmit"];
                string key = context.Request["key"];
                string keyValue = NetHelper.GetMD5(username + "100dh888");
                if (dosubmit != "1")
                    return json.WriteJson(0, "参数错误", new { });
                if (key != keyValue)
                    return json.WriteJson(0, "值错误", new { });
                cmUserInfo userInfo = bll.GetUser(string.Format("where username='{0}'", username.Trim()));
                if (userInfo == null)
                    return json.WriteJson(0, "用户名不存在", new { });
                DateTime.TryParse(userInfo.expirationTime, out s);//到期时间
                if (userInfo.password != password)
                    return json.WriteJson(0, "密码错误", new { });
                if (userInfo.isStop)
                    return json.WriteJson(0, "该用户已被停用", new { });
                if (s <= n)
                    return json.WriteJson(0, "登录失败，账号已到期", new { });
                //Context.Session["SoftUser"] = userInfo;
                List<realmNameInfo> rList = bll.GetRealmList("");//获取所有域名
                if (rList.Count < 1)
                    return json.WriteJson(0, "登录失败，域名为空", new { });
                return json.WriteJson(1, "登录成功", new { cmUser = userInfo, realmList = rList });
            }
            catch (Exception ex)
            {
                return json.WriteJson(0, ex.ToString(), new { });
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