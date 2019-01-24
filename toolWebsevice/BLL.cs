﻿using AutoSend;
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
            userInfo.Id = (int)row["Id"];
            userInfo.username = (string)row["username"];
            userInfo.password = (string)row["password"];
            userInfo.userType = (int)row["userType"];
            userInfo.isStop = (bool)row["isStop"];
            userInfo.gradeId = (int)row["gradeId"];
            userInfo.canPubCount = (int)row["canPubCount"];
            userInfo.realmNameInfo = (string)row["realmNameInfo"];
            userInfo.expirationTime = ((DateTime)row["expirationTime"]).ToString("yyyy-MM-dd HH:mm:ss");
            userInfo.endPubCount = (int)row["endPubCount"];
            userInfo.endTodayPubCount = (int)row["endTodayPubCount"];
            userInfo.registerTime = ((DateTime)row["registerTime"]).ToString("yyyy-MM-dd HH:mm:ss");
            userInfo.registerIP = (string)row["registerIP"];
            userInfo.companyName = (string)row["companyName"];
            userInfo.columnInfoId = (int)row["columnInfoId"];
            userInfo.person = (string)row["person"];
            userInfo.telephone = (string)row["telephone"];
            userInfo.modile = (string)row["modile"];
            userInfo.ten_qq = (string)row["ten_qq"];
            userInfo.keyword = (string)row["keyword"];
            userInfo.pinpai = (string)row["pinpai"];
            userInfo.xinghao = (string)row["xinghao"];
            userInfo.price = (string)row["price"];
            userInfo.smallCount = (string)row["smallCount"];
            userInfo.sumCount = (string)row["sumCount"];
            userInfo.unit = (string)row["unit"];
            userInfo.city = (string)row["city"];
            userInfo.address = (string)row["address"];
            userInfo.com_web = (string)row["com_web"];
            userInfo.companyRemark = (string)row["companyRemark"];
            userInfo.yewu = (string)row["yewu"];
            userInfo.ziduan1 = (string)row["ziduan1"];
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
    }
}