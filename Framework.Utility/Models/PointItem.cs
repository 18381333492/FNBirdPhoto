using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Utility
{   
    /// <summary>
    /// 坐标点的实体
    /// </summary>
    public class PointItem
    {
        /// <summary>
        /// x的值
        /// </summary>
        public string xAxis
        {
            get;set;
        }

        /// <summary>
        /// y的值
        /// </summary>
        public int yAxis
        {
            get; set;
        }

        /// <summary>
        /// 价格
        /// </summary>
        public decimal yPrice
        {
            get;set;
        }
    }
}
