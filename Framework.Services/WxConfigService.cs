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
    public class WxConfigService : IWxConfig
    {
        /// <summary>
        /// 获取微信公众号的配置
        /// </summary>
        /// <returns></returns>
        public override CCY_WxConfig Get()
        {
            var wechat = query.SingleQuery<CCY_WxConfig>("select * from CCY_WxConfig");
            return wechat;
        }

        /// <summary>
        /// 保存微信供公众号设置
        /// </summary>
        /// <param name="WeChat"></param>
        /// <returns></returns>
        public override bool Save(CCY_WxConfig wxConfig)
        {
            if (string.IsNullOrEmpty(wxConfig.ID))
            {//添加

                wxConfig.ID = GuidHelper.NewGuidString();
                string sSql = @"INSERT INTO dbo.CCY_WxConfig( ID ,sName ,sAppId ,sAppSecret  ,sMchId  ,sPayKey ,sNotifyUrl )
                                                                VALUES  ( @ID ,@sName ,@sAppId ,@sAppSecret  ,@sMchId  ,@sPayKey ,@sNotifyUrl )";
                return excute.ExcuteSql(sSql, wxConfig) == 1;
            }
            else
            {//编辑
                string sSql = @"Update dbo.CCY_WxConfig set sName=@sName,
                                                                sAppId=@sAppId,
                                                                sAppSecret=@sAppSecret,
                                                                sMchId=@sMchId,
                                                                sPayKey=@sPayKey,
                                                                sNotifyUrl=@sNotifyUrl
                                                           where ID=@ID";
                return excute.ExcuteSql(sSql, wxConfig) == 1;
            }
        }
    }
}
