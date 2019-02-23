using AutoSend;
using Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace toolWebsevice
{
    /// <summary>
    /// UploadImgHandler 的摘要说明
    /// </summary>
    public class UploadImgHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            BLL bll = new BLL();
            context.Response.ContentType = "image/png";
            context.Response.AddHeader("Access-Control-Allow-Origin", "*");
            string pId = context.Request["productId"];//产品Id
            string username = context.Request["username"];
            string key = context.Request["key"];
            string keyValue = NetHelper.GetMD5(username + "100dh888");
            if (key != keyValue)
                context.Response.Write(json.WriteJson(0, "key值错误", new { }));
            cmUserInfo userInfo = bll.GetUser(string.Format("where username='{0}'", username.Trim()));
            if (userInfo == null)
                context.Response.Write(json.WriteJson(0, "该用户不存在", new { }));
            if (userInfo.isStop)
                context.Response.Write(json.WriteJson(0, "该用户已被停用", new { }));
            string fileUrl = "";
            try
            {
                HttpPostedFile _upfile = context.Request.Files["file"];
                if (_upfile == null)
                    throw new Exception("请先选择文件！");
                else
                {
                    string fileName = _upfile.FileName;/*获取文件名： C:\Documents and Settings\Administrator\桌面\123.jpg*/
                    string suffix = fileName.Substring(fileName.LastIndexOf(".") + 1).ToLower();/*获取后缀名并转为小写： jpg*/
                    int bytes = _upfile.ContentLength;//获取文件的字节大小  
                    if (!(suffix == "jpg" || suffix == "gif" || suffix == "png" || suffix == "jpeg"))
                        throw new Exception("只能上传JPE，GIF,PNG文件");
                    if (bytes > 1024 * 1024 * 2)
                        throw new Exception("图片最大只能传2M");
                    string newfileName = DateTime.Now.ToString("yyyyMMddHHmmss");
                    string fileDir = HttpContext.Current.Server.MapPath("~/upfiles/" + username + "/");
                    if (!Directory.Exists(fileDir))
                    {
                        Directory.CreateDirectory(fileDir);
                    }
                    string saveDir = fileDir + newfileName + "." + suffix;//文件服务器存放路径
                    fileUrl = "/upfiles/" + username + "/" + newfileName + "." + suffix;
                    _upfile.SaveAs(saveDir);//保存图片
                    #region 存到sql图片库
                    imageInfo img = new imageInfo();
                    img.imageId = newfileName;
                    img.imageURL = fileUrl;
                    img.userId = userInfo.Id;
                    img.productId = int.Parse(pId);
                    bll.AddImg(img);
                    #endregion
                }
            }
            catch (Exception ex)
            {
                context.Response.Write(ex.ToString());
            }
            context.Response.Write(json.WriteJson(1, "上传成功", new { imgUrl = "http://tool.100dh.cn" + fileUrl }));
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