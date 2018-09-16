using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Utility
{
    public class ResponeResult
    {

        /// <summary>
        /// 本次请求的成功的标识
        /// </summary>
        public bool success
        {
            get;
            set;
        } = false;

        /// <summary>
        /// 消息描述
        /// </summary>
        public string info
        {
            get;
            set;
        }
 
        /// <summary>
        /// 返回的数据
        /// </summary>
        public object data
        {
            get;
            set;
        }

    }
}
