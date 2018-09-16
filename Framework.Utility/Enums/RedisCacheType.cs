using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Utility
{
    /// <summary>
    /// Redis缓存的类型
    /// 不同的类型指向不同数据库Db
    /// </summary>
    public enum RedisCacheType
    {
        /// <summary>
        /// 系统相关配置
        /// </summary>
        SystemConfig=0,

        /// <summary>
        /// App用户信息
        /// </summary>
        ClientInfo=1,

        /// <summary>
        /// 缓存的临时数据
        /// </summary>
        ShortTimeCacheData=2

    }
}
