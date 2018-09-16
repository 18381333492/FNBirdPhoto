using Framework.Entity;
using Framework.Interfaces;
using Framework.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Services
{
    public class VipPackageService:IVipPackage
    {
        /// <summary>
        /// 获取套餐列表
        /// </summary>
        /// <param name="pageInfo"></param>
        /// <returns></returns>
        public override PageResult getVipPackageList(PageInfo pageInfo)
        {
            return query.PaginationQuery(@"SELECT * FROM dbo.CCY_VipPackage", pageInfo);
        }

        /// <summary>
        /// 根据ID获取套餐
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public override CCY_VipPackage Get(string ID)
        {
            return query.Find<CCY_VipPackage>(ID);
        }

        /// <summary>
        /// 添加套餐
        /// </summary>
        /// <param name="Package"></param>
        /// <returns></returns>
        public override bool Insert(CCY_VipPackage Package)
        {
            Package.ID = GuidHelper.NewGuidString();
            return excute.ExcuteSql(@"insert into CCY_VipPackage 
                                                  (ID,sVipName,dVipPrices,dOldPrices,dSavePrices,dBonusMoney,sVipDays,iSort)
                                            values(@ID,@sVipName,@dVipPrices,@dOldPrices,@dSavePrices,@dBonusMoney,@sVipDays,@iSort)", Package) == 1;
        }

        /// <summary>
        /// 编辑套餐
        /// </summary>
        /// <param name="Package"></param>
        /// <returns></returns>
        public override bool Update(CCY_VipPackage Package)
        {
            return excute.ExcuteSql(@"update CCY_VipPackage set sVipName=@sVipName,
                                                                     dVipPrices=@dVipPrices,
                                                                     dOldPrices=@dOldPrices,
                                                                     dSavePrices=@dSavePrices,
                                                                     dBonusMoney=@dBonusMoney,
                                                                     sVipDays=@sVipDays,
                                                                     iSort=@iSort
                                                            where ID=@ID", Package) == 1;
        }

        /// <summary>
        /// 删除套餐
        /// </summary>
        /// <param name="Package"></param>
        /// <returns></returns>
        public override bool Cancel(string Ids)
        {
            return excute.ExcuteSql(string.Format(@"delete CCY_VipPackage where ID IN ({0})",Ids)) >= 1;
        }
    }
}
