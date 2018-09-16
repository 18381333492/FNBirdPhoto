using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Utility
{
    /// <summary>
    /// Redis的订阅类型
    /// </summary>
    public enum SubscribeType
    {
        Logistics,//物流

        LogisticsExceptionNotify,//物流轨迹异常通知

        AdminRechargeWeChatNotify,//充值通知

        ProviderRechargeMessageNotify,//供货商短信通知

        ProviderRechargeWeChatNotify//供货商微信模板通知


    }
}
