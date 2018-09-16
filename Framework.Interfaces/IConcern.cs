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
    /// 关注相关接口
    /// </summary>
    public abstract class IConcern : IBase
    {

        /// <summary>
        /// 分页获取我的关注
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="pageInfo"></param>
        /// <returns></returns>
        public abstract PageResult GetConcernListByPage(string clientId, PageInfo pageInfo);

        /// <summary>
        /// 分页获取我的粉丝
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="pageInfo"></param>
        /// <returns></returns>
        public abstract PageResult GetFansListByPage(string clientId, PageInfo pageInfo);

        /// <summary>
        /// 关注
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="sConcrenId"></param>
        /// <returns></returns>
        public abstract bool Follow(string clientId, string sConcrenId);

        /// <summary>
        /// 取消关注
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="sConcrenId"></param>
        /// <returns></returns>
        public abstract bool Cancel(string clientId,string sConcrenId);

        /// <summary>
        /// 设置备注姓名
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="sConcrenId"></param>
        /// <param name="sRemarkName"></param>
        public abstract bool SetRemarkName(string clientId, string sConcrenId, string sRemarkName);


        /// <summary>
        /// 权限设置
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="sConcrenId"></param>
        /// <param name="iIsSee"></param>
        /// <returns></returns>
        public abstract bool SetPermissions(string clientId, string sConcrenId, int iIsSee);


    }                                                       
}
