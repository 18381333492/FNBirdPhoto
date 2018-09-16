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
    public class ClientService : IClient
    {
        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public override PageResult GetClientList(PageInfo info, int iType, int bIsVip)
        {
            StringBuilder sSql = new StringBuilder();
            DbParameters Parameters = new DbParameters();
            sSql.Append("SELECT c.*,v.sVipName FROM dbo.CCY_Client c LEFT JOIN dbo.CCY_VipPackage v ON c.sVipTypeId=v.ID where 1=1");
            if (!string.IsNullOrEmpty(info.keyword))
            {
                sSql.Append(@" and (c.sNickName =@keyword or c.sPhone =@keyword)");
                Parameters.Add("keyword", info.keyword);
            }
            if (iType>=0)
            {
                sSql.Append(@" and c.iType=@iType");
                Parameters.Add("iType", iType);
            }
            if (bIsVip>=0)
            {
                sSql.Append(@" and c.bIsVip=@bIsVip");
                Parameters.Add("bIsVip", bIsVip);
            }
            var result = query.PaginationQuery(sSql.ToString(), info, Parameters);
            return result;
        }
        /// <summary>
        /// 启用\禁用会员
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public override bool Disable(string Id, int type)
        {
            string sSql = "Update CCY_Client set iStatus=@iStatus where ID=@Id";
            var result = excute.ExcuteSql(sSql, new { Id = Id, iStatus = type }) == 1;
            return result;
        }

        /// <summary>
        /// 通过手机号码登录
        /// </summary>
        /// <param name="sPhone"></param>
        /// <param name="sPassword"></param>
        /// <returns></returns>
        public override CCY_Client LoginByPhone(string sPhone, string sPassword)
        {
            return query.SingleQuery<CCY_Client>("select * from CCY_Client where sPhone=@sPhone and sPassword=@sPassword", new
            {
                sPhone = sPhone,
                sPassword = SecurityHelper.md5(sPassword)
            });
        }

        /// <summary>
        /// 微信授权登录
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public override ProcedureResult Login(CCY_Client client)
        {
            DbParameters Parameters = new DbParameters();
            Parameters.Add("@in_sNickName", client.sNickName, DbType.String);
            Parameters.Add("@in_sUnionid", client.sUnionid, DbType.String);
            Parameters.Add("@in_sOpenId", client.sOpenId, DbType.String);
            Parameters.Add("@@in_sHeadImg", client.sHeadImg, DbType.String);
            Parameters.Add("@in_code", 0, DbType.Int32, ParameterDirection.Output);
            Parameters.Add("@in_msg", null, DbType.String, ParameterDirection.Output, 100);
            var list = query.QueryProcedure<dynamic>("ProviderSystem_ProviderUser_Login", Parameters);
            var result = new ProcedureResult();
            result.iCode = Parameters.Get<int>("in_code");
            result.sMsg = Parameters.Get<string>("in_msg");
            if (result.iCode == 100) result.sData = list[0];
            return result;
        }

        /// <summary>
        /// 获取会员Vip信息
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public override dynamic GetVipInfo(string ID)
        {
            return query.SingleQuery<dynamic>("select sVipName,dExpireTime from CCY_Client where ID=@ID", new { ID = ID });
        }

        /// <summary>
        ///  绑定手机号
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="sPhone"></param>
        /// <param name="sPassword"></param>
        /// <returns></returns>
        public override bool BingMobile(string ID,string sPhone, string sPassword)
        {
            sPassword = SecurityHelper.md5(sPassword);
            return excute.ExcuteSql("update CCY_Client set sPhone=@sPhone,sPassword=@sPassword where ID=@ID", new {
                sPhone= sPhone,
                sPassword= sPassword,
                ID= ID
            })>0;   
        }

        /// <summary>
        /// 更新用户信息
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="sPhotoName"></param>
        /// <param name="sWeiXinNo"></param>
        /// <param name="sPhone"></param>
        /// <param name="sIntroduce"></param>
        /// <returns></returns>
        public override bool UpdatePersonalInfo(string ID, string sPhotoName, string sWeiXinNo, string sPhone, string sIntroduce)
        {
            string sSql = string.Empty;
            if (!string.IsNullOrEmpty(sPhotoName))
                sSql = " set sPhotoName=@sPhotoName";
            if (!string.IsNullOrEmpty(sPhotoName))
                sSql = " set sWeiXinNo=@sWeiXinNo";
            if (!string.IsNullOrEmpty(sPhotoName))
                sSql = " set sPhone=@sPhone";
            if (!string.IsNullOrEmpty(sPhotoName))
                sSql = " set sIntroduce=@sIntroduce";
            return excute.ExcuteSql(string.Format("update CCY_Client {0} where ID=@ID", sSql),
                new
                {
                    ID = ID,
                    sPhotoName = sPhotoName,
                    sWeiXinNo = sWeiXinNo,
                    sPhone = sPhone,
                    sIntroduce = sIntroduce
                }) > 0; ;
        }

        /// <summary>
        /// 是否存在该手机号码
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public override bool IsExistPhone(string sPhone)
        {
            return query.Any("select ID from CCY_Client where sPhone=@sPhone", new { sPhone = sPhone }).Value;
        }
    }
}
