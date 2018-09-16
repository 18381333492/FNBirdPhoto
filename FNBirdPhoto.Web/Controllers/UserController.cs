using Framework.Interfaces;
using Framework.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FNBirdPhoto.Web.Controllers
{
    public class UserController : BaseController<IUser>
    {
        // GET: User

        /// <summary>
        /// 管理员数据列表
        /// </summary>
        /// <returns></returns>
        public ActionResult Index(PageInfo pageInfo)
        {
            if (Request.HttpMethod.ToUpper() == MethodType.GET)
            {
                return View();
            }
            else
            {
                var result = JsonHelper.ToJsonString(Manager.GetUserList(pageInfo));
                return Content(result);
            }
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="Ids"></param>
        /// <returns></returns>
        public ActionResult Cancel(string Ids)
        {
            if (Manager.Cancel(Ids))
                result.success = true;
            else
                result.info = "删除失败";
            return Json(result);
        }

        /// <summary>
        /// 禁用
        /// </summary>
        /// <param name="Ids"></param>
        /// <returns></returns>
        public ActionResult Disable(string Ids, int status)
        {
            var msg = status == 0 ? "禁用" : "启用";
            if (Manager.Disable(Ids, status))
                result.success = true;
            else
                result.info = msg + "失败";
            return Json(result);
        }

        /// <summary>
        /// 审核通过
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public ActionResult Verify(string ID)
        {
            if (Manager.Verify(ID))
                result.success = true;
            else
                result.info = "审核失败";
            return Json(result);
        }
    }
}