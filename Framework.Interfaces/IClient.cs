using Framework.Entity;
using Framework.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Interfaces
{   
    /// <summary>
    /// 管理员的相关接口
    /// </summary>
    public abstract class IClient: IBase
    {
        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <param name="pageInfo"></param>
        /// <returns></returns>
        public abstract PageResult GetClientList(PageInfo pageInfo, int iType, int bIsVip);
        /// <summary>
        /// 禁用会员
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public abstract bool Disable(string Id, int type);

        /// <summary>
        /// 通过手机号码登录
        /// </summary>
        /// <param name="sPhone"></param>
        /// <param name="sPassword"></param>
        /// <returns></returns>
        public abstract CCY_Client LoginByPhone(string sPhone, string sPassword);

        /// <summary>
        /// 微信授权登录
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public abstract ProcedureResult Login(CCY_Client client);

        /// <summary>
        /// 获取会员Vip信息
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public abstract dynamic GetVipInfo(string ID);

        /// <summary>
        ///  绑定手机号
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="sPhone"></param>
        /// <param name="sPassword"></param>
        /// <returns></returns>
        public abstract bool BingMobile(string ID, string sPhone, string sPassword);

        /// <summary>
        /// 更新用户信息
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="sPhotoName"></param>
        /// <param name="sWeiXinNo"></param>
        /// <param name="sPhone"></param>
        /// <param name="sIntroduce"></param>
        /// <returns></returns>
        public abstract bool UpdatePersonalInfo(string ID, string sPhotoName, string sWeiXinNo, string sPhone, string sIntroduce);

        /// <summary>
        /// 是否存在该手机号码
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public abstract bool IsExistPhone(string sPhone);

    }
}
