using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FNBirdPhoto.AppInterface
{
    /// <summary>
    /// 会员缓存的信息
    /// </summary>
    public class ClientCacheInfo
    {
        /// <summary>
        /// 会员主键ID
        /// </summary>
        public string ID
        {
            get;set;
        }

        /// <summary>
        /// Token
        /// </summary>
        public string sToken
        {
            get; set;
        }

        /// <summary>
        /// 状态 0-正常 1-禁用
        /// </summary>
        public int iState
        {
            get;set;
        } 
    }
}