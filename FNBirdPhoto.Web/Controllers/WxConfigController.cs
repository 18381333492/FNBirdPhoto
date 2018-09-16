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
    public class WxConfigController : BaseController<IWxConfig>
    {
        public ActionResult Index(CCY_WxConfig wxConfig)
        {
            if (Request.HttpMethod.ToUpper() == MethodType.GET)
            {
                var wechat = Manager.Get();
                if (wechat == null) wechat = new CCY_WxConfig();
                return View(wechat);
            }
            else
            {
                wxConfig.sAppId = wxConfig.sAppId.Trim();
                wxConfig.sAppSecret = wxConfig.sAppSecret.Trim();
                wxConfig.sMchId = wxConfig.sMchId.Trim();
                wxConfig.sPayKey = wxConfig.sPayKey.Trim();
                wxConfig.sNotifyUrl = wxConfig.sNotifyUrl.Trim();
                if (Manager.Save(wxConfig))
                {//保存成功写入Redis缓存
                    string sKey = "WxConfig";
                    RedisHelper.SetKey(RedisCacheType.SystemConfig, sKey, wxConfig);
                    result.success = true;
                }
                else
                    result.info = "保存失败";
                return Json(result);
            }
        }
    }
}