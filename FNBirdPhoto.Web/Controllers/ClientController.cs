using Framework.Interfaces;
using Framework.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FNBirdPhoto.Web.Controllers
{
    public class ClientController:BaseController<IClient>
    {
        /// <summary>
        /// 会员数据列表
        /// </summary>
        /// <returns></returns>
        public ActionResult Index(PageInfo pageInfo,int iType=-1,int bIsVip=-1)
        {
            if (Request.HttpMethod.ToUpper() == MethodType.GET)
            {
                return View();
            }
            else
            {
                var result = JsonHelper.ToJsonString(Manager.GetClientList(pageInfo, iType,bIsVip));
                return Content(result);
            }
        }
        /// <summary>
        /// 启用会员
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public ActionResult Disable(string ID, int type)
        {
            string msg = type == 0 ? "禁用" : "启用";
            type = type == 0 ? 1 : 0;
            if (Manager.Disable(ID, type))
            {
                result.info = msg + "成功";
                result.success = true;
                return Json(result);
            }
            result.info = msg + "失败";
            return Json(result);
        }
    }
}