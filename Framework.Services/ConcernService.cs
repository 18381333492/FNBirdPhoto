using Framework.DBAccess;
using Framework.Entity;
using Framework.Interfaces;
using Framework.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Services
{
    public class ConcernService : IConcern
    {
        /// <summary>
        /// 分页获取我的关注
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="pageInfo"></param>
        /// <returns></returns>
        public override PageResult GetConcernListByPage(string clientId, PageInfo pageInfo)
        {
            StringBuilder sSql = new StringBuilder();
            DbParameters Parameters = new DbParameters();
            Parameters.Add("sClientId", clientId);
            sSql.Append(@"select a.sConcernId,
                                 a.bIsNotSee iIsSee,
                                 a.sRemarkName,
                                 a.dConcernTime,
                                 b.bIsVip,
                                 b.sNickName,
                                 b.sHeadImg,
                                 ImageTextCount=(select count(ID) from CCY_ImageText where sClientId=a.sConcernId) 
                                 from CCY_Concern a
                                 left join CCY_Client b on a.sConcernId=b.ID                
                                 where a.sClientId=@sClientId order by a.dConcernTime desc");
            if (!string.IsNullOrEmpty(pageInfo.keyword))
            {
                sSql.Append(" and b.sNickName=@keyword");
                Parameters.Add("keyword",pageInfo.keyword);
            }
            var pageResult = query.PaginationQuery(sSql.ToString(), pageInfo, Parameters);
            return pageResult;
        }

        /// <summary>
        /// 分页获取我的粉丝
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="pageInfo"></param>
        /// <returns></returns>
        public override PageResult GetFansListByPage(string clientId, PageInfo pageInfo)
        {
            StringBuilder sSql = new StringBuilder();
            DbParameters Parameters = new DbParameters();
            Parameters.Add("sClientId", clientId);
            sSql.Append(@"select a.sConcernId,
                                 a.bIsNotSee iIsSee, 
                                 a.sRemarkName,
                                 a.dConcernTime,
                                 b.sNickName,
                                 b.bIsVip,
                                 b.dExpireTime,
                                 b.sHeadImg
                                 from CCY_Concern a
                                 left join CCY_Client b on a.sClientId=b.ID                
                                 where a.sConcernId=@sClientId order by a.dConcernTime desc");
            if (!string.IsNullOrEmpty(pageInfo.keyword))
            {
                sSql.Append(" and b.sNickName=@keyword");
                Parameters.Add("keyword", pageInfo.keyword);
            }
            var pageResult = query.PaginationQuery(sSql.ToString(), pageInfo, Parameters);
            return pageResult;
        }

        /// <summary>
        /// 关注
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="sConcrenId"></param>
        /// <returns></returns>
        public override bool Follow(string clientId, string sConcrenId)
        {
            var concern = new CCY_Concern();
            concern.ID = GuidHelper.NewGuidString();
            concern.sClientId = clientId;
            concern.sConcernId = sConcrenId;
            concern.sRemarkName = string.Empty;
            concern.bIsNotSee = true;
            concern.dConcernTime = DateTime.Now;
            return excute.ExcuteSql(@"insert into CCY_Concern (ID,sClientId,sRemarkName,sConcernId,bIsNotSee,dConcernTime)
                                                    values(@ID,@sClientId,@sRemarkName,@sConcernId,@bIsNotSee,@dConcernTime)", concern) > 0;
        }

        /// <summary>
        /// 取消关注
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="sConcrenId"></param>
        /// <returns></returns>
        public override bool Cancel(string clientId, string sConcrenId)
        {
            return excute.ExcuteSql("delete CCY_Concern where sClientId=@sClientId and sConcrenId=@sConcrenId", new
            {
                sClientId = clientId,
                sConcrenId = sConcrenId
            })>0;
        }

        /// <summary>
        /// 设置备注姓名
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="sConcrenId"></param>
        /// <param name="sRemarkName"></param>
        public override bool SetRemarkName(string clientId, string sConcrenId, string sRemarkName)
        {
            return excute.ExcuteSql("update CCY_Concern set sRemarkName=@sRemarkName where sClientId=@sClientId and sConcrenId=@sConcrenId", new
            {
                sRemarkName = sRemarkName,
                sClientId = clientId,
                sConcrenId = sConcrenId
            }) > 0;
        }

        /// <summary>
        /// 权限设置
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="sConcrenId"></param>
        /// <param name="iIsSee"></param>
        /// <returns></returns>
        public override bool SetPermissions(string clientId, string sConcrenId, int iIsSee)
        {
            return excute.ExcuteSql("update CCY_Concern set bIsNotSee=@bIsNotSee sClientId=@sClientId and sConcrenId=@sConcrenId", new
            {
                bIsNotSee = iIsSee,
                sClientId = clientId,
                sConcrenId = sConcrenId
            })>0;
        }
    }
}
