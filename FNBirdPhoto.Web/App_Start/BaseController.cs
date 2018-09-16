using Framework.DI;
using Framework.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TraceLogs;

namespace FNBirdPhoto.Web
{
    public class BaseController<T>:Controller
    {
        //日志组件
        protected static ILogger logger = LoggerManager.Instance.GetSLogger("Web");

        //接口
        protected T Manager { get; }

        /// <summary>
        /// 返回的响应结果
        /// </summary>
        protected ResponeResult result { get; set; }
        /// <summary>
        /// 管理员的登录信息
        /// </summary>
        protected LoginCacheInfo LoginStatus;

        /// <summary>
        /// 构造函数实例化接口
        /// </summary>
        public BaseController()
        {
            Manager = GetImpl<T>();
            result = new ResponeResult();
        }

        /// <summary>
        /// 映射实现接口
        /// </summary>
        /// <typeparam name="M"></typeparam>
        /// <returns></returns>
        protected static M GetImpl<M>()
        {
            return DIEntity.Instance.GetImpl<M>();
        }
    }
}