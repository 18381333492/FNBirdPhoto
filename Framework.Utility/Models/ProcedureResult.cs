using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Utility
{
    /// <summary>
    /// 存储过程返回参数model
    /// </summary>
    public class ProcedureResult
    {
        /// <summary>
        /// 返回处理结果code 100-成功 200-失败
        /// </summary>
        public int iCode { get; set; }

        /// <summary>
        /// 返回的消息提示
        /// </summary>
        public string sMsg { get; set; }

        /// <summary>
        /// 返回的额外数据
        /// </summary>
        public object sData { get; set; }
    }
}
