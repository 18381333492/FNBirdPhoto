using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Utility
{
    /// <summary>
    /// 物流订单订阅中心实体
    /// </summary>
    public class SubscribeModel
    {
       
        /// <summary>
        /// 物流订单ID
        /// </summary>
        public string sOrderId
        {
            get;set;
        }

        /// <summary>
        /// 供货商ID
        /// </summary>
        public string sProviderId
        {
            get;set;
        }

        /// <summary>
        /// 快递公司编码
        /// </summary>
        public string sExpressCode
        {
            get; set;
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
        /// 订阅状态 0-为订阅 1-订阅成功 2-订阅失败
        /// </summary>
        public int iSubscribeStatus
        {
            get;
            set;
        }

        /// <summary>
        /// 订阅返回的结果
        /// </summary>
        public string sSubscribeMsg
        {
            get;set;
        }


        /// <summary>
        /// 订阅时间
        /// </summary>
        public DateTime dSubscribeTime
        {
            get;set;
        }
    }
}
