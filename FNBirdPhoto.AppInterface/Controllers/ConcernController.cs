using Framework.Interfaces;
using Framework.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FNBirdPhoto.AppInterface.Controllers
{
    /// <summary>
    /// 关注相关的控制器
    /// </summary>
    public class ConcernController : BaseController<IConcern>
    {
        /// <summary>
        /// 分页获取我的关注
        /// </summary>
        /// <param name="pageInfo"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetConcernListByPage(PageInfo pageInfo)
        {
            var pageResult = Manager.GetConcernListByPage(LoginStatus.ID, pageInfo);
            result.code = ResultStatus.Success;
            result.data = pageResult;
            return Content(JsonHelper.ToJsonString(result));
        }

        /// <summary>
        /// 分页获取我的粉丝
        /// </summary>
        /// <param name="pageInfo"></param>
        /// <returns></returns>
        public ActionResult GetFansListByPage(PageInfo pageInfo)
        {
            var pageResult = Manager.GetFansListByPage(LoginStatus.ID, pageInfo);
            result.code = ResultStatus.Success;
            result.data = pageResult;
            return Content(JsonHelper.ToJsonString(result));
        }

        /// <summary>
        /// 关注
        /// </summary>
        /// <param name="sConcrenId"></param>
        /// <returns></returns>
        public ActionResult Follow(string sConcrenId)
        {
            if (Manager.Follow(LoginStatus.ID, sConcrenId))
            {
                result.code = ResultStatus.Success;
                result.info = "取消成功";
            }
            else
                result.info = "取消关注失败";
            return Json(result);
        }

        /// <summary>
        /// 取消关注
        /// </summary>
        /// <param name="sConcrenId"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Cancel(string sConcrenId)
        {
            if (Manager.Cancel(LoginStatus.ID, sConcrenId))
            {
                result.code = ResultStatus.Success;
                result.info = "取消成功";
            }
            else
                result.info = "取消关注失败";
            return Json(result);
        }

        /// <summary>
        /// 设置备注姓名
        /// </summary>
        /// <param name="sConcrenId"></param>
        /// <param name="sRemarkName"></param>
        [HttpPost]
        public ActionResult SetRemarkName(string clientId, string sConcrenId, string sRemarkName)
        {
            if (Manager.SetRemarkName(LoginStatus.ID, sConcrenId, sRemarkName))
            {
                result.code = ResultStatus.Success;
                result.info = "设置成功";
            }
            else
                result.info = "设置失败";
            return Json(result);
        }

        /// <summary>
        /// 权限设置
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="sConcrenId"></param>
        /// <param name="iIsSee">0-否 1-是 </param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SetPermissions(string clientId, string sConcrenId, int iIsSee)
        {
            if (Manager.SetPermissions(LoginStatus.ID, sConcrenId, iIsSee))
            {
                result.code = ResultStatus.Success;
                result.info = "设置成功";
            }
            else
                result.info = "设置失败";
            return Json(result);
        }
    }
}