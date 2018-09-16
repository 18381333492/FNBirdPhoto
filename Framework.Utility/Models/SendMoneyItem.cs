using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Utility
{
    /// <summary>
    /// 发送奖金的实体
    /// </summary>
    public class SendMoneyItem
    {
        /// <summary>
        /// 提现记录ID
        /// </summary>
        public string ID
        {
            get;set;
        }

        /// <summary>
        /// 提现金额
        /// </summary>
        public decimal dDrawMoney
        {
            get;set;
        }

        /// <summary>
        /// 奖金支付状态 0-未支付 1-已支付 2-支付失败
        /// </summary>
        public int iIsPay
        {
            get;set;
        }

        /// <summary>
        /// 支付失败原因
        /// </summary>
        public string sFailReason
        {
            get;set;
        }

        /// <summary>
        /// 代理商的ID
        /// </summary>
        public string sAgentId
        {
            get;set;
        }

        /// <summary>
        /// 代理商的OpenId
        /// </summary>
        public  string sOpenId
        {
            get;set;
        }

        /// <summary>
        /// 奖金发放流水号
        /// </summary>
        public string sMoneyNo
        {
            get;set;
        }

    }
}
