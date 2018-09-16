using Framework.Entity;
using Framework.Interfaces;
using Framework.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FNBirdPhoto.AppInterface.Controllers
{
    public class ClientController : BaseController<IClient>
    {
        // GET: Client
        private readonly static string sBingKey = "bing_";
        private readonly static string sForgetKey = "forget_";
        private readonly static string sOverKey = "over_";

        /// <summary>
        /// 微信授权登录
        /// </summary>
        /// <returns></returns>    
        [NoLogin]
        [HttpPost]
        public ActionResult Login(CCY_Client client)
        {
            if(string.IsNullOrEmpty(client.sUnionid)|| string.IsNullOrEmpty(client.sOpenId))
            {
                result.info = "参数错误";
                return Json(result); 
            }
            var procedureResult = Manager.Login(client);
            if (procedureResult.iCode == 100)
            {
                client = JsonHelper.Deserialize<CCY_Client>(JsonHelper.ToJsonString(procedureResult.sData));
                if (client.iStatus == 0)
                {
                    LoginStatus = new ClientCacheInfo();
                    LoginStatus.ID = client.ID;
                    LoginStatus.sToken = GuidHelper.NewGuidString();
                    RedisHelper.SetKey(RedisCacheType.ClientInfo, LoginStatus.ID, LoginStatus, 60 * 24 * 7);
                    result.code = ResultStatus.Success;
                    result.data = new
                    {
                        ID = LoginStatus.ID,
                        sToken = LoginStatus.sToken,
                        sNickName = client.sNickName,
                        sPhone = client.sPhone,
                        sHeadImg = client.sHeadImg,
                        sWeiXinNo = client.sWeiXinNo,
                        sPhotoName = client.sPhotoName,
                        sIntroduce = client.sIntroduce
                    };
                }
                else
                {
                    result.info = "该账号已被冻结,请联系管理员";
                }
            }
            else
                result.info = "登录失败";
            return Json(result);
        }

        /// <summary>
        /// 通过手机号登录
        /// </summary>
        /// <param name="sPhone"></param>
        /// <param name="sPassword"></param>
        /// <returns></returns>
        [NoLogin]
        [HttpPost]
        public ActionResult LoginByPhone(string sPhone, string sPassword)
        {
            if (string.IsNullOrEmpty(sPhone) || string.IsNullOrEmpty(sPassword))
            {
                result.info = "手机号和密码不能为空";
                return Json(result);
            }
            var client = Manager.LoginByPhone(sPhone, sPassword);
            if (client != null)
            {
                if (client.iStatus == 0)
                {
                    LoginStatus = new ClientCacheInfo();
                    LoginStatus.ID = client.ID;
                    LoginStatus.sToken = GuidHelper.NewGuidString();
                    RedisHelper.SetKey(RedisCacheType.ClientInfo, LoginStatus.ID, LoginStatus, 60 * 24 * 7);
                    result.code = ResultStatus.Success;
                    result.data = new
                    {
                        ID = LoginStatus.ID,
                        sToken = LoginStatus.sToken,
                        sNickName = client.sNickName,
                        sPhone = client.sPhone,
                        sHeadImg = client.sHeadImg,
                        sWeiXinNo = client.sWeiXinNo,
                        sPhotoName = client.sPhotoName,
                        sIntroduce = client.sIntroduce
                    };
                }
                else
                {
                    result.info = "该账号已被冻结,请联系管理员";
                }
            }
            else
                result.info = "用户名或密码错误";
            return Json(result);
        }

        /// <summary>
        /// 获取手机验证码
        /// </summary>
        /// <param name="sPhone"></param>
        /// <param name="iType">1-绑定手机,2-找回密码</param>
        /// <returns></returns>
        [NoLogin]
        public ActionResult GetMobileCode(string sPhone,int iType)
        {
            if(string.IsNullOrEmpty(sPhone)||(iType!=1&& iType != 2))
            {
                result.info = "参数错误";
                return Json(result,JsonRequestBehavior.AllowGet);
            }
            if (RedisHelper.ExistKey(RedisCacheType.ShortTimeCacheData, sOverKey+sPhone))
            {//防止刷短信
                result.info = "60s内已获取过验证码";
                return Json(result);
            }
            var IsExist = Manager.IsExistPhone(sPhone);
            if (iType == 1&& IsExist)
            {
                result.info = "手机号码已被绑定";
                return Json(result);
            }
            if(iType==2&& IsExist == false)
            {
                result.info = "手机号码不存在";
                return Json(result);
            }
            string sKey = (iType == 1 ? sBingKey : sForgetKey) + sPhone;
            string sCode = new Random().Next(10000, 99999).ToString();
            CodeType codeType = iType == 1 ? CodeType.Bing : CodeType.FindPwd;
            bool bStatus = CodeHelper.Send(sPhone, sCode, codeType);
            if (bStatus)
            {
                RedisHelper.SetKey(RedisCacheType.ShortTimeCacheData, sKey, sCode, 10);
                RedisHelper.SetKey(RedisCacheType.ShortTimeCacheData, sOverKey+ sPhone, sCode, 1);
                result.code = ResultStatus.Success;
            }
            else
                result.info = "获取验证码失败";
            return Json(result);
        }

        /// <summary>
        /// 绑定手机号
        /// </summary>
        /// <param name="sPhone"></param>
        /// <param name="sPassword"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult BingMobile(string sPhone, string sPassword)
        {
            if (string.IsNullOrEmpty(sPhone) || string.IsNullOrEmpty(sPassword))
            {
                result.info = "手机号和密码不能为空";
                return Json(result);
            }
            if (Manager.BingMobile(LoginStatus.ID, sPhone, sPassword))
            {
                result.code = ResultStatus.Success;
                result.info = "绑定成功";
            }
            else
                result.info = "绑定失败";
            return Json(result);
        }

        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <param name="sPhotoName"></param>
        /// <param name="sWeiXinNo"></param>
        /// <param name="sPhone"></param>
        /// <param name="sIntroduce"></param>
        /// <returns></returns>
        public ActionResult UpdatePersonalInfo(string sPhotoName, string sWeiXinNo, string sPhone, string sIntroduce)
        {
            if (Manager.UpdatePersonalInfo(LoginStatus.ID, sPhotoName, sWeiXinNo, sPhone, sIntroduce))
            {
                result.code = ResultStatus.Success;
                result.info = "修改成功";
            }
            else
                result.info = "修改失败";
            return Json(result);
        }

        /// <summary>
        /// 获取会员Vip的信息
        /// </summary>
        /// <returns></returns>
        public ActionResult GetVipInfo()
        {
            result.data=Manager.GetVipInfo(LoginStatus.ID);
            result.code = ResultStatus.Success;
            return Json(result, JsonRequestBehavior.AllowGet);
        }



    }
}