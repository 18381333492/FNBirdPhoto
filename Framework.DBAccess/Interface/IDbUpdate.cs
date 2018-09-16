using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.DBAccess
{
    /// <summary>
    /// DB查询更新接口
    /// </summary>
    public interface IDbUpdate
    {
        /// <summary>
        /// 执行Sql语句
        /// </summary>
        /// <param name="Sql"></param>
        /// <param name="Parameters"></param>
        /// <returns></returns>
        int ExcuteSql(string Sql, object Parameters = null);

        /// <summary>
        /// 执行Sql语句
        /// </summary>
        /// <param name="Sql"></param>
        /// <param name="Parameters"></param>
        /// <returns></returns>
        int ExcuteSql(string Sql, DbParameters Parameters);

        /// <summary>
        /// 执行事务
        /// </summary>
        /// <param name="sSql"></param>
        /// <param name="Parameters"></param>
        /// <returns></returns>
        int ExcuteTransaction(List<string> sSql, object Parameters = null);

        /// <summary>
        /// 执行事务
        /// </summary>
        /// <param name="sSql"></param>
        /// <param name="Parameters"></param>
        /// <returns></returns>
        int ExcuteTransaction(List<string> sSql, List<object> DbParametersArray);

        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="sProcedureName"></param>
        /// <returns></returns>
        int ExcuteProcedure(string sProcedureName);

        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="sProcedureName"></param>
        /// <param name="Parameters"></param>
        /// <returns></returns>
        int ExcuteProcedure(string sProcedureName, DbParameters Parameters);

        /// <summary>
        /// 根据主键ID删除数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ID"></param>
        /// <returns></returns>
        int Delete<T>(string ID);

        /// <summary>
        /// 根据主键ID集合批量删除
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Ids"></param>
        /// <returns></returns>
        int BulkDelete<T>(List<string> Ids);

        /// <summary>
        /// 批量插入
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        bool BulkInsert<T>(DataTable dt);

        /// <summary>
        /// 批量插入
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="conn"></param>
        /// <param name="dataTable"></param>
        /// <param name="TableName"></param>
        bool BulkInsert(DataTable dataTable, string TableName);
    }
}
