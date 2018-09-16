using Framework.Entity;
using Framework.Interfaces;
using Framework.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FNBirdPhoto.Web.Controllers
{
    public class VipPackageController:BaseController<IVipPackage>
    {
        /// <summary>
        /// 套餐列表
        /// </summary>
        /// <param name="pageInfo"></param>
        /// <returns></returns>
        public ActionResult Index(PageInfo pageInfo)
        {
            if (Request.HttpMethod.ToUpper()==MethodType.GET)
            {
                return View();
            }
            else
            {
                var list = JsonHelper.ToJsonString(Manager.getVipPackageList(pageInfo));
                return Content(list);
            }
        }
        /// <summary>
        /// 添加套餐
        /// </summary>
        /// <param name="Package"></param>
        /// <returns></returns>
        public ActionResult Add(CCY_VipPackage Package)
        {
            if (Request.HttpMethod.ToUpper() == MethodType.GET)
            {
                return View();
            }
            else
            {
                if (Manager.Insert(Package))
                    result.success = true;
                else
                    result.info = "添加失败";
                return Json(result);
            }
        }

        /// <summary>
        /// 编辑套餐
        /// </summary>
        /// <param name="Package"></param>
        /// <returns></returns>
        public ActionResult Edit(CCY_VipPackage Package)
        {
            if (Request.HttpMethod.ToUpper() == MethodType.GET)
            {
                return View(Manager.Get(Package.ID));
            }
            else
            {
                if (Manager.Update(Package))
                    result.success = true;
                else
                    result.info = "编辑失败";
                return Json(result);
            }
        }

        /// <summary>
        /// 删除套餐
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

    }
}