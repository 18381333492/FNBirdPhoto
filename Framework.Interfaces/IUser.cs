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
    public abstract class IUser: IBase
    {
        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <param name="pageInfo"></param>
        /// <returns></returns>
        public abstract PageResult GetUserList(PageInfo pageInfo);

        /// <summary>
        /// 删除系统管理员用户
        /// </summary>
        /// <param name="Package"></param>
        /// <returns></returns>
        public abstract bool Cancel(string Ids);
        /// <summary>
        /// 禁用系统管理员用户
        /// </summary>
        /// <param name="Package"></param>
        /// <returns></returns>
        public abstract bool Disable(string Ids, int status);

        /// <summary>
        /// 审核管理员
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public abstract bool Verify(string ID);
    }
}
