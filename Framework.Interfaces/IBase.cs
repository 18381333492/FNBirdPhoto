using Framework.DBAccess;
using Framework.DI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Interfaces
{   
    /// <summary>
    /// 接口基类
    /// </summary>
    public class IBase
    {
        /// <summary>
        /// Dapper的查询
        /// </summary>
        protected static IDbQuery query
        {
            get;
            set;
        }

        /// <summary>
        /// Dapper的操作
        /// </summary>
        protected static IDbUpdate excute
        {
            get;
            set;
        }


        /// <summary>
        /// 静态构造函数
        /// 实例化DB操作
        /// </summary>
        static IBase()
        {
            query = DIEntity.Instance.GetImpl<IDbQuery>();

            excute = DIEntity.Instance.GetImpl<IDbUpdate>();
        }
    }
}
