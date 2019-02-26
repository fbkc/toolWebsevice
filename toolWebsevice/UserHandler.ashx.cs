using AutoSend;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
                        case "GetUserByUsername": _strContent.Append(GetUserInformation(context)); break;
                        case "GetUserByUserId": _strContent.Append(GetUserByUserId(context)); break;
                        case "UpUserPubInformation": _strContent.Append(UpUserPubInformation(context)); break;
                        default: break;
                    }
                }
            }
            context.Response.Write(_strContent.ToString());
        }
        public string GetUserInformation(HttpContext context)
        {
            try
            {
                string username = context.Request["username"];
                BLL bll = new BLL();
                //公司/会员信息
                cmUserInfo userInfo = bll.GetUser(string.Format("where username='{0}'", username));
                return json.WriteJson(1, "成功", new { cmUser = userInfo });
            }
            catch (Exception ex)
            {
                return json.WriteJson(0, ex.ToString(), new { });
            }
        }
        public string GetUserByUserId(HttpContext context)
        {
            try
            {
                string userId = context.Request["userId"];
                BLL bll = new BLL();
                //公司/会员信息
                cmUserInfo userInfo = bll.GetUser(string.Format("where Id='{0}'", userId));
                return json.WriteJson(1, "成功", new { cmUser = userInfo });
            }
            catch (Exception ex)
            {
                return json.WriteJson(0, ex.ToString(), new { });
            }
        }
        public string UpUserPubInformation(HttpContext context)
        {
            try
            {
                string userId = context.Request["userId"].Trim();
                BLL bll = new BLL();
                bll.UpUserPubInformation(int.Parse(userId));
                return json.WriteJson(1, "成功", new { });
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