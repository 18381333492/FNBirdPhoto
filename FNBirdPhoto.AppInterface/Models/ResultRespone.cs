using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FNBirdPhoto.AppInterface
{
    /// <summary>
    /// 接口响应的结果
    /// </summary>
    public class ResultRespone
    {
        /// <summary>
        /// 请求的成功标识
        /// </summary>
        public ResultStatus code
        {
            get; set;
        } = ResultStatus.Fail;

        /// <summary>
        /// 信息描述
        /// </summary>
        public string info
        {
            get;set;
        }

        /// <summary>
        /// 返回的数据
        /// </summary>
        public object data
        {
            get;set;
        }
    }
}