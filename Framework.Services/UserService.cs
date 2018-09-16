using Framework.DBAccess;
using Framework.Entity;
using Framework.Interfaces;
using Framework.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Services
{
    public class UserService: IUser
    {
        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public override PageResult GetUserList(PageInfo info)
        {
            StringBuilder sSql = new StringBuilder();
            DbParameters Parameters = new DbParameters();
            sSql.Append("SELECT * FROM dbo.CCY_User");
            var result = query.PaginationQuery(sSql.ToString(), info, Parameters);
            return result;
        }
        /// <summary>
        /// 删除系统管理员用户
        /// </summary>
        /// <param name="Package"></param>
        /// <returns></returns>
        public override bool Cancel(string Ids)
        {
            return excute.BulkDelete<CCY_User>(Ids.Split(',').ToList()) > 0;
        }

        /// <summary>
        /// 禁用系统管理员用户
        /// </summary>
        /// <param name="Package"></param>
        /// <returns></returns>
        public override bool Disable(string Ids, int status)
        {
            status = status == 0 ? 1 : 0;
            return excute.ExcuteSql("update dbo.CCY_User set iStatus=@iStatus where ID=@ID", new { ID = Ids, iStatus = status }) == 1;
        }

        /// <summary>
        /// 审核管理员
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public override bool Verify(string ID)
        {
            return excute.ExcuteSql("update CCY_User set iStatus=0 where ID=@ID", new { ID = ID }) == 1;
        }
    }
}
