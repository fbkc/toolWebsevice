﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace toolWebsevice
{
    /// <summary>
    /// ProductNewsHandler 的摘要说明
    /// </summary>
    public class ProductNewsHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/html";


            context.Response.Write("Hello World");
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