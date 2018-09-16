using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TraceLogs;


namespace Framework.DBAccess
{
    /// <summary>
    /// DB的更新操作
    /// </summary>
    public class DbUpdateManager:DbBase
    {
        /// <summary>
        /// 执行SQL语句
        /// </summary>
        /// <param name="conn">数据库连接字符串对象</param>
        /// <param name="sqlCommand">sql语句</param>
        /// <param name="Parameters">参数</param>
        protected int DoExcuteSql(IDbConnection conn, string sqlCommand, object Parameters)
        {
            int res = conn.Execute(sqlCommand, Parameters, null, null, CommandType.Text);
            return res;
        }

        /// <summary>
        /// 执行事务
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="transaction"></param>
        /// <param name="sqlCommand"></param>
        /// <param name="Parameters"></param>
        /// <returns></returns>
        protected int DoExcuteTransaction(IDbConnection conn, IDbTransaction transaction, List<string> sqlCommand, object Parameters)
        {
            int iRow = 0;//受影响的行
            for (var i = 0; i < sqlCommand.Count; i++)
            {
                string sSql = sqlCommand[i];
                iRow += conn.Execute(sSql, Parameters, transaction, null, CommandType.Text);
            }
            transaction.Commit();
            return iRow;
        }

        /// <summary>
        /// 执行事务
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="transaction"></param>
        /// <param name="sqlCommand"></param>
        /// <param name="DbParametersArray"></param>
        /// <returns></returns>
        protected int DoExcuteTransaction(IDbConnection conn , IDbTransaction transaction,List<string> sqlCommand, List<object> DbParametersArray)
        {
            int iRow = 0;//受影响的行
            for(var i = 0; i < sqlCommand.Count; i++)
            {
                string sSql = sqlCommand[i];
                iRow += conn.Execute(sSql, DbParametersArray[i], transaction, null, CommandType.Text);
            }
            transaction.Commit();
            return iRow;
        }

        /// <summary>
        /// 执行储存过程
        /// </summary>
        /// <param name="conn">数据库连接字符串对象</param>
        /// <param name="sProcedureName">存储过程名称</param>
        /// <param name="Parameters">参数</param>
        protected int DoExcuteProcedure(IDbConnection conn, string sProcedureName, DynamicParameters Parameters)
        {
            int res = conn.Execute(sProcedureName, Parameters, null, null, CommandType.StoredProcedure);
            return res;
        }
    
        /// <summary>
        /// 批量插入
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="dataTable"></param>
        public void DoBulkInsert<T>(IDbConnection conn,DataTable dataTable)
        {
            SqlBulkCopy sqlBulkCopy = new SqlBulkCopy((SqlConnection)conn);
            sqlBulkCopy.DestinationTableName = typeof(T).Name;

            if (dataTable != null && dataTable.Rows.Count != 0)
            {
                sqlBulkCopy.WriteToServer(dataTable);
            }
            sqlBulkCopy.Close();
        }
        
        /// <summary>
        /// 批量插入
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="conn"></param>
        /// <param name="dataTable"></param>
        /// <param name="TableName"></param>
        public void DoBulkInsert(IDbConnection conn, DataTable dataTable,string TableName)
        {
            SqlBulkCopy sqlBulkCopy = new SqlBulkCopy((SqlConnection)conn);
            sqlBulkCopy.DestinationTableName = TableName;

            if (dataTable != null && dataTable.Rows.Count != 0)
            {
                sqlBulkCopy.WriteToServer(dataTable);
            }
            sqlBulkCopy.Close();
        }
    }
}
