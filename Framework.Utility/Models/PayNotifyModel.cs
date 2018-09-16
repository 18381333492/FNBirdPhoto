using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Utility
{
    /// <summary>
    /// 微信支付通知实体
    /// </summary>
    public class PayNotifyModel
    {
        /// <summary>
        /// 充值账号
        /// </summary>
        public string Account
        {
            get;
            set;
        }

        /// <summary>
        /// 会员姓名
        /// </summary>
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// 充值渠道 0-扫码支付 1-微信公众号支付
        /// </summary>
        public int Type
        {
            get;set;
        }

        /// <summary>
        /// 购买的商品名称
        /// </summary>
        public string Goods
        {
            get;set;
        }

        /// <summary>
        /// 充值的金额
        /// </summary>
        public decimal Prices
        {
            get;
            set;
        }
    } 
}
