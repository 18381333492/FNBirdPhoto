using Framework.DI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TraceLogs;
using Framework.Utility;
using System.Text;

namespace FNBirdPhoto.AppInterface
{
    public class BaseController<T>: Controller
    {
        //日志组件
        protected static ILogger logger = LoggerManager.Instance.GetSLogger("Web");
        /// <summary>
        /// 返回结果的实体
        /// </summary>
        protected ResultRespone result;

        /// <summary>
        /// 会员缓存的用户信息
        /// </summary>
        protected ClientCacheInfo LoginStatus;

        /// <summary>
        /// 接口
        /// </summary>
        protected T Manager;

        /// <summary>
        /// 构造函数
        /// </summary>
        public BaseController()
        {
            result = new ResultRespone();
            Manager = DIEntity.Instance.GetImpl<T>();
        }

        /// <summary>
        /// 实例化接口
        /// </summary>
        /// <typeparam name="M"></typeparam>
        /// <returns></returns>
        public static M GetImpl<M>()
        {
            return DIEntity.Instance.GetImpl<M>();
        }


        /// <summary>
        /// action之前验证登录
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //是否需要验证登录
            bool needLogin = filterContext.ActionDescriptor.GetCustomAttributes(typeof(NoLogin), true).Length == 1 ? false : true;
            if (needLogin)
            {
                string ID = Request.QueryString["ID"];
                string sToken = Request.QueryString["sToken"];
                if(!string.IsNullOrEmpty(ID)&&!string.IsNullOrEmpty(sToken))
                {
                    LoginStatus = RedisHelper.GetValue<ClientCacheInfo>(RedisCacheType.ClientInfo, ID);
                    if(LoginStatus==null|| LoginStatus.sToken!= sToken)
                    {
                        result.code = ResultStatus.Over;
                        result.info = "sToken过期,请重新登录";
                        filterContext.Result = Content(JsonHelper.ToJsonString(result));
                    }
                    else
                    {
                        if (LoginStatus.iState == 1)
                        {
                            //账号被禁用
                            result.code = ResultStatus.Forbid;
                            result.info = "账号被禁用";
                            filterContext.Result = Content(JsonHelper.ToJsonString(result));
                        }          
                    }
                }
                else
                {//登录过期
                    result.code = ResultStatus.Over;
                    result.info ="登录过期,请重新登录";
                    filterContext.Result = Content(JsonHelper.ToJsonString(result));
                }
            }
        }

        /// <summary>
        /// Action执行后打印输出参数
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            logger.Info("请求链接:" + Request.Url.AbsolutePath);
            if (Request.HttpMethod.ToUpper() == "POST")
            {
                StringBuilder ParamStr = new StringBuilder();
                foreach(var key in Request.Form.AllKeys)
                {
                    ParamStr.AppendFormat("{0}={1},", key, Request.Form[key]);
                }
                logger.Info("请求参数:" + ParamStr.ToString());
            }
            logger.Info("请求返回参数:" + JsonHelper.ToJsonString(result));
        }

        /// <summary>
        /// 捕捉异常
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void OnException(ExceptionContext filterContext)
        {
            filterContext.ExceptionHandled = true;
            logger.Info("请求链接:" + filterContext.HttpContext.Request.Url.AbsolutePath);
            logger.Info("Api接口异常:" + filterContext.Exception.Message);
            logger.Fatal("Api接口异常:" + filterContext.Exception);
            result.code =ResultStatus.Exception;
            result.info = "服务错误!";
            filterContext.Result = Content(JsonHelper.ToJsonString(result));
        }
    }
}