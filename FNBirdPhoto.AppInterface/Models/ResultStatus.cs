using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FNBirdPhoto.AppInterface
{
    /// <summary>
    /// 返回的额状态码
    /// </summary>
    public enum ResultStatus
    {
        Success=100, //成功

        Fail=200,    //失败 

        Over=300,    //登录过期

        Forbid = 400,    //冻结

        Exception =500  //异常
    }
}