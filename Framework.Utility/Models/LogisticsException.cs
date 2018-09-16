using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Utility
{
    /// <summary>
    /// 物流轨迹异常实体模型
    /// </summary>
    public class LogisticsException
    {
        /// <summary>
        /// 供货商ID
        /// </summary>
        public string sProviderId
        {
            get;
            set;
        }

        /// <summary>
        /// 订单主键ID
        /// </summary>
        public string sOrderId
        {
            get;
            set;
        }

        /// <summary>
        /// 运单号
        /// </summary>
        public string sExpressNo
        {
            get;
            set;
        }

        /// <summary>
        /// 异常原因
        /// </summary>
        public string sReason
        {
            get;
            set;
        }


        /// <summary>
        /// 收件人
        /// </summary>
        public string sReceiverName
        {
            get;set;
        }

        /// <summary>
        /// 收件人电话
        /// </summary>
        public string sReceiverMobile
        {
            get; set;
        }

        /// <summary>
        /// 商品名称
        /// </summary>
        public string sGoodsName
        {
            get; set;
        }

        /// <summary>
        /// 物流名称
        /// </summary>
        public string sExpressName
        {
            get; set;
        }

        /// <summary>
        /// 供货商OpenID
        /// </summary>
        public string sProviderOpenId
        {
            get;set;
        }
    }
}
