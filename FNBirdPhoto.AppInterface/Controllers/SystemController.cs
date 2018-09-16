using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FNBirdPhoto.AppInterface.Controllers
{       
    /// <summary>
    /// 获取系统的相关设置
    /// </summary>
    public class SystemController : Controller
    {
        // GET: System
        public ActionResult Get()
        {
            return View();
        }
    }
}