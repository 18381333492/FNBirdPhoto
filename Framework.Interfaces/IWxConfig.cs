using Framework.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Interfaces
{
    public abstract class IWxConfig:IBase
    {
        /// <summary>
        /// 获取微信公众号的配置
        /// </summary>
        /// <returns></returns>
        public abstract CCY_WxConfig Get();

        /// <summary>
        /// 保存微信供公众号设置
        /// </summary>
        /// <param name="WeChat"></param>
        /// <returns></returns>
        public abstract bool Save(CCY_WxConfig wxConfig);
    }
}
