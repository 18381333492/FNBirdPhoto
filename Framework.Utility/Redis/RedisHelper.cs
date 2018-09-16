using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TraceLogs;

namespace Framework.Utility
{
    public partial class RedisHelper
    {
        /// <summary>
        /// 日志组件
        /// </summary>
        private static ILogger logger = LoggerManager.Instance.GetSLogger("Redis");

        /// <summary>
        /// 连接字符串
        /// </summary>
        private static readonly string ConnectionString;

        /// <summary>
        /// redis 连接对象
        /// </summary>
        private static IConnectionMultiplexer RedisMananger;

        /// <summary>
        /// 静态化构造函数
        /// </summary>
        static RedisHelper()
        {
            //获取Redis链接字符串
            ConnectionString = ConfigHelper.GetAppSetting("RedisConStr");
            //链接服务器
            RedisMananger = ConnectionMultiplexer.Connect(ConnectionString);
          
            //注册事件
            RedisMananger.ConfigurationChanged += ConfigurationChanged;
            RedisMananger.ConnectionRestored += ConnectionRestored;
            RedisMananger.ErrorMessage += ErrorMessage;
        }

        #region 事件的注册

        /// <summary>
        /// 配置更改的时候触发的方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void ConfigurationChanged(object sender, EndPointEventArgs e)
        {
            logger.Info("Redis配置更改为:" + e.EndPoint);
        }

        /// <summary>
        /// 重新建立连接之前的错误
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void ConnectionRestored(object sender, ConnectionFailedEventArgs e)
        {
            logger.Info("Redis EndPoint:" + e.EndPoint);
            logger.Info("Redis FailureType:" + e.FailureType);
            logger.Info("Redis重新建立连接之前的错误:" + e.Exception.Message);
        }

        /// <summary>
        /// 发生错误
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void ErrorMessage(object sender, RedisErrorEventArgs e)
        {
            logger.Info("Redis EndPoint:" + e.EndPoint);
            logger.Info("Redis 发生错误信息:" + e.Message);
        }

        #endregion

        #region Redis操作方法

        /// <summary>
        /// 获取RedisKey
        /// </summary>
        /// <param name="Type"></param>
        /// <param name="Key"></param>
        /// <returns></returns>
        private static string GetRedisKey(RedisCacheType Type, string Key)
        {
            return Type.ToString() + "_" + Key;
        }

        /// <summary>
        /// 设置Key
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expireSecond"></param>
        /// <returns></returns>
        public static bool SetKey(RedisCacheType Type, string Key, string Value, int ExpireMinutes = 0)
        {
           var db = RedisMananger.GetDatabase((int)Type);
            Key = GetRedisKey(Type, Key);
            if (ExpireMinutes > 0)
            {
                return db.StringSet(Key, Value,  TimeSpan.FromMinutes(ExpireMinutes));
            }
            else
            {
                return db.StringSet(Key, Value);
            }
        }

        /// <summary>
        ///  设置Key
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Type"></param>
        /// <param name="Key"></param>
        /// <param name="Value"></param>
        /// <param name="ExpireMinutes"></param>
        /// <returns></returns>
        public static bool SetKey<T>(RedisCacheType Type, string Key, T Value, int ExpireMinutes = 0)
        {
            var db = RedisMananger.GetDatabase((int)Type);
            Key = GetRedisKey(Type, Key);
            string jsonStr = JsonHelper.ToJsonString(Value);
            if (ExpireMinutes > 0)
            {
                return db.StringSet(Key, jsonStr, TimeSpan.FromMinutes(ExpireMinutes));
            }
            else
            {
                return db.StringSet(Key, jsonStr);
            }
        }

        /// <summary>
        /// 获取Key的Value
        /// </summary>
        /// <param name="Type"></param>
        /// <param name="Key"></param>
        /// <returns></returns>
        public static string GetValue(RedisCacheType Type, string Key)
        {
            var db = RedisMananger.GetDatabase((int)Type);
            Key = GetRedisKey(Type, Key);
            string value = db.StringGet(Key);
            return value;
        }

        /// <summary>
        /// 获取Key的Value
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Type"></param>
        /// <param name="Key"></param>
        /// <returns></returns>
        public static T GetValue<T>(RedisCacheType Type, string Key)
        {
            var db = RedisMananger.GetDatabase((int)Type);
            Key = GetRedisKey(Type, Key);
            string value = db.StringGet(Key);
            return JsonHelper.Deserialize<T>(value);
        }

        /// <summary>
        /// Key是否存在
        /// </summary>
        /// <param name="Type"></param>
        /// <param name="Key"></param>
        /// <returns></returns>
        public static bool ExistKey(RedisCacheType Type, string Key)
        {
            var db = RedisMananger.GetDatabase((int)Type);
            Key = GetRedisKey(Type, Key);
            return  db.KeyExists(Key);
        }


        /// <summary>
        /// 删除Key
        /// </summary>
        /// <param name="Type"></param>
        /// <param name="Key"></param>
        /// <returns></returns>
        public static bool RemoveKey(RedisCacheType Type, string Key)
        {
            var db = RedisMananger.GetDatabase((int)Type);
            Key = GetRedisKey(Type, Key);
            return db.KeyDelete(Key);
        }

        /// <summary>
        /// 设置Key的过期时间
        /// </summary>
        /// <param name="Type"></param>
        /// <param name="Key"></param>
        /// <returns></returns>
        public static bool SetKeyExpire(RedisCacheType Type, string Key, int ExpireMinutes)
        {
            var db = RedisMananger.GetDatabase((int)Type);
            Key = GetRedisKey(Type, Key);
            return db.KeyExpire(Key, TimeSpan.FromMinutes(ExpireMinutes));
        }

        /// <summary>
        /// 移除Key的过期时间
        /// </summary>
        /// <param name="Type"></param>
        /// <param name="Key"></param>
        /// <returns></returns>
        public static bool RemoveKeyExpire(RedisCacheType Type, string Key)
        {
            var db = RedisMananger.GetDatabase((int)Type);
            Key = GetRedisKey(Type, Key);
            return db.KeyPersist(Key);
        }


        /// <summary>
        ///  listRight的追加
        /// </summary>
        /// <param name="Type"></param>
        /// <param name="Key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static long PushListRight(RedisCacheType Type, string Key,string value)
        {
            var db = RedisMananger.GetDatabase((int)Type);
            Key = GetRedisKey(Type, Key);
            return db.ListRightPush(Key, value);
        }

        /// <summary>
        /// listLeft的Pop
        /// </summary>
        /// <param name="Type"></param>
        /// <param name="Key"></param>
        /// <returns></returns>
        public static string PopListLeft(RedisCacheType Type, string Key)
        {
            try
            {
                var db = RedisMananger.GetDatabase((int)Type);
                Key = GetRedisKey(Type, Key);
                return db.ListLeftPop(Key);
            }
            catch(Exception ex)
            {
                logger.Info(ex);
                return null;
            }
         
        }
        #endregion

    }
}
