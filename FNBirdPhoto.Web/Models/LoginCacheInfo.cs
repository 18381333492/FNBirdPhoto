using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FNBirdPhoto.Web
{

    /// <summary>
    /// 后台登录缓存的信息
    /// </summary>
    [Serializable]
    public class LoginCacheInfo
    {
        /// <summary>
        /// 后台主键ID
        /// </summary>
        public Guid ID { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        public string sPhone { get; set; }
    }
}
