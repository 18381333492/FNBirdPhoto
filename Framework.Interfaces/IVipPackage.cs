using Framework.Entity;
using Framework.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Interfaces
{
    public abstract class IVipPackage:IBase
    {
        /// <summary>
        /// 获取套餐列表
        /// </summary>
        /// <param name="pageInfo"></param>
        /// <returns></returns>
        public abstract PageResult getVipPackageList(PageInfo pageInfo);
        /// <summary>
        /// 根据ID获取套餐
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public abstract CCY_VipPackage Get(string ID);

        /// <summary>
        /// 添加套餐
        /// </summary>
        /// <param name="Package"></param>
        /// <returns></returns>
        public abstract bool Insert(CCY_VipPackage Package);

        /// <summary>
        /// 编辑套餐
        /// </summary>
        /// <param name="Package"></param>
        /// <returns></returns>
        public abstract bool Update(CCY_VipPackage Package);

        /// <summary>
        /// 删除套餐
        /// </summary>
        /// <param name="Package"></param>
        /// <returns></returns>
        public abstract bool Cancel(string Ids);
    }
}
