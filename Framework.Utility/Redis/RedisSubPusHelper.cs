using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Utility
{
    /// <summary>
    /// Redis订阅和发布功能的封装
    /// </summary>
    public partial class RedisHelper
    {
        /// <summary>
        /// 获取RedisKey
        /// </summary>
        /// <param name="Type"></param>
        /// <param name="Key"></param>
        /// <returns></returns>
        private static string GetChannelKey(SubscribeType Type)
        {
            return "Subscribe_" + Type.ToString();
        }

        /// <summary>
        /// 发布订阅
        /// </summary>
        /// <param name="subscribeType"></param>
        /// <param name="callBack"></param>
        public static void Subscribe(SubscribeType subscribeType, Action<RedisChannel, RedisValue> callBack)
        {
            ISubscriber Sub = RedisMananger.GetSubscriber();
            string channelKey = GetChannelKey(subscribeType);
            Sub.Subscribe(channelKey, callBack);
        }


        /// <summary>
        /// 发布订阅
        /// </summary>
        /// <param name="subscribeType"></param>
        /// <param name="callBack"></param>
        public static void SubscribeAsync(SubscribeType subscribeType, Action<RedisChannel, RedisValue> callBack)
        {
            ISubscriber Sub = RedisMananger.GetSubscriber();
            string channelKey = GetChannelKey(subscribeType);
            Sub.SubscribeAsync(channelKey, callBack);
        }


        /// <summary>
        /// 推送消息
        /// </summary>
        /// <param name="subscribeType"></param>
        /// <param name="value"></param>
        public static long Publish(SubscribeType subscribeType,string value)
        {
            ISubscriber Sub = RedisMananger.GetSubscriber();
            string channelKey = GetChannelKey(subscribeType);
            return Sub.Publish(channelKey, value);
        }

        /// <summary>
        /// 异步推送消息
        /// </summary>
        /// <param name="subscribeType"></param>
        /// <param name="value"></param>
        public static Task<long> PublishAsync(SubscribeType subscribeType, string value)
        {
            ISubscriber Sub = RedisMananger.GetSubscriber();
            string channelKey = GetChannelKey(subscribeType);
            return Sub.PublishAsync(channelKey, value);
        }
    }
}
