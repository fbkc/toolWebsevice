using AutoSend;
using HRMSys.DAL;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace toolWebsevice
{
    public class BLL
    {
        /// <summary>
        /// 通过用户名找Id
        /// </summary>
        /// <param name="sqlstr"></param>
        /// <returns></returns>
        public string GetUserId(string username)
        {
            object ob = SqlHelper.ExecuteScalar("select Id from userInfo where username='" + username + "'");
            return ob.ToString();
        }
        /// <summary>
        /// 获取当前表最大Id
        /// </summary>
        /// <returns></returns>
        public int GetMaxId()
        {
            object ob = "";
            try
            {
                ob = SqlHelper.ExecuteScalar("select Id  from htmlInfo order by Id desc");
            }
            catch (Exception ex)
            { return 1; }
            return int.Parse(ob.ToString());
        }
        /// <summary>
        /// 将html内容参数存入数据库
        /// </summary>
        /// <param name="info"></param>
        public void AddHtml(htmlInfo info)
        {
            int a = SqlHelper.ExecuteNonQuery(@"INSERT INTO [AutouSend].[dbo].[htmlInfo]
           ([title]
           ,[titleURL]
           ,[articlecontent]
           ,[columnId]
           ,[pinpai]
           ,[xinghao]
           ,[price]
           ,[smallCount]
           ,[sumCount]
           ,[unit]
           ,[city]
           ,[titleImg]
           ,[addTime]
           ,[realmNameId]
           ,[userId])
     VALUES
           (@title
           ,@titleURL
           ,@articlecontent
           ,@columnId
           ,@pinpai
           ,@xinghao
           ,@price
           ,@smallCount
           ,@sumCount
           ,@unit
           ,@city
           ,@titleImg
           ,@addTime
           ,@realmNameId
           ,@userId)",
               new SqlParameter("@title", SqlHelper.ToDBNull(info.title)),
               new SqlParameter("@titleURL", SqlHelper.ToDBNull(info.titleURL)),
               new SqlParameter("@articlecontent", SqlHelper.ToDBNull(info.articlecontent)),
               new SqlParameter("@columnId", SqlHelper.ToDBNull(info.columnId)),
               new SqlParameter("@pinpai", SqlHelper.ToDBNull(info.pinpai)),
               new SqlParameter("@xinghao", SqlHelper.ToDBNull(info.xinghao)),
               new SqlParameter("@price", SqlHelper.ToDBNull(info.price)),
               new SqlParameter("@smallCount", SqlHelper.ToDBNull(info.smallCount)),
               new SqlParameter("@sumCount", SqlHelper.ToDBNull(info.sumCount)),
               new SqlParameter("@unit", SqlHelper.ToDBNull(info.unit)),
               new SqlParameter("@city", SqlHelper.ToDBNull(info.city)),
               new SqlParameter("@titleImg", SqlHelper.ToDBNull(info.titleImg)),
               new SqlParameter("@addTime", SqlHelper.ToDBNull(info.addTime)),
               new SqlParameter("@realmNameId", SqlHelper.ToDBNull(info.realmNameId)),
               new SqlParameter("@userId", SqlHelper.ToDBNull(info.userId)));
        }
        /// <summary>
        /// 查找单个用户
        /// </summary>
        /// <param name="sqlstr"></param>
        /// <returns></returns>
        public cmUserInfo GetUser(string sqlstr)
        {
            DataTable dt = SqlHelper.ExecuteDataSet("select * from userInfo " + sqlstr).Tables[0];
            if (dt.Rows.Count < 1)
                return null;
            DataRow row = dt.Rows[0];
            cmUserInfo userInfo = new cmUserInfo();
            userInfo.Id = (int)SqlHelper.FromDBNull( row["Id"]);
            userInfo.username = (string)SqlHelper.FromDBNull(row["username"]);
            userInfo.password = (string)SqlHelper.FromDBNull(row["password"]);
            userInfo.userType = (int)SqlHelper.FromDBNull(row["userType"]);
            userInfo.isStop = (bool)SqlHelper.FromDBNull(row["isStop"]);
            userInfo.gradeId = (int)SqlHelper.FromDBNull(row["gradeId"]);
            userInfo.canPubCount = (int)SqlHelper.FromDBNull(row["canPubCount"]);
            userInfo.realmNameInfo = (string)SqlHelper.FromDBNull(row["realmNameInfo"]);
            userInfo.expirationTime = ((DateTime)SqlHelper.FromDBNull(row["expirationTime"])).ToString("yyyy-MM-dd HH:mm:ss");
            userInfo.endPubCount = (int)SqlHelper.FromDBNull(row["endPubCount"]);
            userInfo.endTodayPubCount = (int)SqlHelper.FromDBNull(row["endTodayPubCount"]);
            userInfo.registerTime = ((DateTime)SqlHelper.FromDBNull(row["registerTime"])).ToString("yyyy-MM-dd HH:mm:ss");
            userInfo.registerIP = (string)SqlHelper.FromDBNull(row["registerIP"]);
            userInfo.companyName = (string)SqlHelper.FromDBNull(row["companyName"]);
            userInfo.columnInfoId = (int)SqlHelper.FromDBNull(row["columnInfoId"]);
            userInfo.person = (string)SqlHelper.FromDBNull(row["person"]);
            userInfo.telephone = (string)SqlHelper.FromDBNull(row["telephone"]);
            userInfo.modile = (string)SqlHelper.FromDBNull(row["modile"]);
            userInfo.ten_qq = (string)SqlHelper.FromDBNull(row["ten_qq"]);
            userInfo.address = (string)SqlHelper.FromDBNull(row["address"]);
            userInfo.com_web = (string)SqlHelper.FromDBNull(row["com_web"]);
            userInfo.companyRemark = (string)SqlHelper.FromDBNull(row["companyRemark"]);
            userInfo.yewu = (string)SqlHelper.FromDBNull(row["yewu"]);
            userInfo.beforePubTime = ((DateTime)SqlHelper.FromDBNull(row["beforePubTime"])).ToString("yyyy-MM-dd HH:mm:ss");
            return userInfo;
        }
        /// <summary>
        /// 获取域名
        /// </summary>
        /// <param name="sqlstr"></param>
        /// <returns></returns>
        public List<realmNameInfo> GetRealmList(string sqlstr)
        {
            List<realmNameInfo> rList = new List<realmNameInfo>();
            DataTable dt = SqlHelper.ExecuteDataSet("select * from realmNameInfo " + sqlstr).Tables[0];
            if (dt.Rows.Count < 1)
                return null;
            foreach (DataRow row in dt.Rows)
            {
                realmNameInfo rInfo = new realmNameInfo();
                rInfo.Id = (int)row["Id"];
                rInfo.realmName = (string)row["realmName"];
                rInfo.realmAddress = (string)row["realmAddress"];
                rInfo.isUseing = (bool)row["isUseing"];
                rList.Add(rInfo);
            }
            return rList;
        }

        /// <summary>
        /// 软件上传图片
        /// </summary>
        /// <param name="img"></param>
        public void AddImg(imageInfo img)
        {
            int a = SqlHelper.ExecuteNonQuery(@"INSERT INTO [AutouSend].[dbo].[imageInfo]
           ([imageId]
           ,[imageURL]
           ,addTime
           ,userId
           ,productId)
     VALUES
           (@imageId
           ,@imageURL
           ,getdate()
           ,@userId
           ,@productId)",
               new SqlParameter("@imageId", SqlHelper.ToDBNull(img.imageId)),
               new SqlParameter("@imageURL", SqlHelper.ToDBNull(img.imageURL)),
               new SqlParameter("@productId", SqlHelper.ToDBNull(img.productId)),
               new SqlParameter("@userId", SqlHelper.ToDBNull(img.userId)));
        }
        /// <summary>
        /// 更新会员 总的已发条数，今日已发条数，上一条发布时间
        /// </summary>
        /// <param name="sqlstr"></param>
        public void UpUserPubInformation(int Id)
        {
            int a = SqlHelper.ExecuteNonQuery(@"UPDATE [AutouSend].[dbo].[userInfo]
   SET [endPubCount] = endPubCount+1
      ,[endTodayPubCount] = endTodayPubCount+1
      ,[beforePubTime] = getdate() where Id=@Id",
      new SqlParameter("@Id", SqlHelper.ToDBNull(Id)));
        }
    }
}