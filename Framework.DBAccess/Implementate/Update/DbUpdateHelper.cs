using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.DBAccess
{
    /// <summary>
    /// DB的更新操作
    /// </summary>
    public class DbUpdateHelper : DbUpdateManager, IDbUpdate
    {
        /// <summary>
        /// 执行SQL语句
        /// </summary>
        /// <param name="Sql"></param>
        /// <param name="Parameters"></param>
        /// <returns></returns>
        public int ExcuteSql(string Sql, object Parameters = null)
        {
            IDbConnection conn = GetSqlConnection();
            try
            {
                return DoExcuteSql(conn, Sql, Parameters);
            }
            catch (Exception ex)
            {
                logger.Info(ex.Message);
                logger.Fatal(ex);
                return 0;
            }
            finally
            {
                CloseConnect(conn);
            }
        }

        /// <summary>
        /// 执行SQL语句
        /// </summary>
        /// <param name="Sql"></param>
        /// <param name="Parameters"></param>
        /// <returns></returns>
        public int ExcuteSql(string Sql, DbParameters Parameters)
        {
            IDbConnection conn = GetSqlConnection();
            try
            {
                return DoExcuteSql(conn, Sql, Parameters.GetParameters());
            }
            catch (Exception ex)
            {
                logger.Info(ex.Message);
                logger.Fatal(ex);
                return 0;
            }
            finally
            {
                CloseConnect(conn);
            }
        }

        /// <summary>
        /// 执行事务
        /// </summary>
        /// <param name="sSql"></param>
        /// <param name="Parameters"></param>
        /// <returns></returns>
        public int ExcuteTransaction(List<string> sSql, object Parameters=null)
        {
            IDbConnection conn = GetSqlConnection();
            IDbTransaction transaction = conn.BeginTransaction();//开启事务
            try
            {
                return DoExcuteTransaction(conn, transaction, sSql, Parameters);
            }
            catch (Exception ex)
            {
                transaction.Rollback();//事务回滚
                logger.Info(ex.Message);
                logger.Fatal(ex);
                return 0;
            }
            finally
            {
                CloseConnect(conn);
            }
        }

        /// <summary>
        /// 执行事务
        /// </summary>
        /// <param name="sSql"></param>
        /// <param name="DbParametersArray"></param>
        /// <returns></returns>
        public int ExcuteTransaction(List<string> sSql, List<object> DbParametersArray)
        {
            IDbConnection conn = GetSqlConnection();
            IDbTransaction transaction = conn.BeginTransaction();//开启事务
            try
            {
                return DoExcuteTransaction(conn, transaction, sSql, DbParametersArray);
            }
            catch (Exception ex)
            {
                transaction.Rollback();//事务回滚
                logger.Info(ex.Message);
                logger.Fatal(ex);
                return 0;
            }
            finally
            {
                CloseConnect(conn);
            }
        }

        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="sProcedureName"></param>
        /// <param name="Parameters"></param>
        /// <returns></returns>
        public int ExcuteProcedure(string sProcedureName)
        {
            IDbConnection conn = GetSqlConnection();
            try
            {
                return DoExcuteProcedure(conn, sProcedureName, null);       
            }
            catch (Exception ex)
            {
                logger.Info(ex.Message);
                logger.Fatal(ex);
                return 0;
            }
            finally
            {
                CloseConnect(conn);
            }
        }

        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="sProcedureName"></param>
        /// <param name="Parameters"></param>
        /// <returns></returns>
        public int ExcuteProcedure(string sProcedureName, DbParameters Parameters)
        {
            IDbConnection conn = GetSqlConnection();
            try
            {
                return DoExcuteProcedure(conn, sProcedureName, Parameters.GetParameters());
            }
            catch (Exception ex)
            {
                logger.Info(ex.Message);
                logger.Fatal(ex);
                return 0;
            }
            finally
            {
                CloseConnect(conn);
            }
        }

        /// <summary>
        /// 根据主键ID删除数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ID"></param>
        /// <returns></returns>
        public int Delete<T>(string ID)
        {
            IDbConnection conn = GetSqlConnection();
            try
            {
                string sSql = string.Format("DELETE {0} WHERE ID=@ID", typeof(T).Name);
                return DoExcuteSql(conn, sSql, new { ID = ID });
            }
            catch (Exception ex)
            {
                logger.Info(ex.Message);
                logger.Fatal(ex);
                return 0;
            }
            finally
            {
                CloseConnect(conn);
            }
        }

        /// <summary>
        /// 根据主键ID集合批量删除
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Ids"></param>
        /// <returns></returns>
        public int BulkDelete<T>(List<string> Ids)
        {
            IDbConnection conn = GetSqlConnection();
            try
            {
                List<string> paramArray = new List<string>();
                DbParameters Parameters = new DbParameters();
                for (var i=0;i< Ids.Count; i++)
                {
                    string name = "@Parameters_" + i.ToString();
                    paramArray.Add(name);
                    Parameters.Add(name, Ids[i]);
                }
                string paramStr = string.Join(",", paramArray);
                string sSql = string.Format("DELETE {0} WHERE ID IN({1})", typeof(T).Name, paramStr);
                return DoExcuteSql(conn, sSql, Parameters.GetParameters());
            }
            catch (Exception ex)
            {
                logger.Info(ex.Message);
                logger.Fatal(ex);
                return 0;
            }
            finally
            {
                CloseConnect(conn);
            }
        }

        /// <summary>
        /// 批量插入
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public bool BulkInsert<T>(DataTable dt)
        {
            IDbConnection conn = GetSqlConnection();
            try
            {
                 DoBulkInsert<T>(conn, dt);
                 return true;
            }
            catch (Exception ex)
            {
                logger.Info(ex.Message);
                logger.Fatal(ex);
                return false;
            }
            finally
            {
                CloseConnect(conn);
            }
        }

        /// <summary>
        /// 批量插入
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="conn"></param>
        /// <param name="dataTable"></param>
        /// <param name="TableName"></param>
        public bool BulkInsert(DataTable dt, string TableName)
        {
            IDbConnection conn = GetSqlConnection();
            try
            {
                DoBulkInsert(conn, dt, TableName);
                return true;
            }
            catch (Exception ex)
            {
                logger.Info(ex.Message);
                logger.Fatal(ex);
                return false;
            }
            finally
            {
                CloseConnect(conn);
            }
        }
    }
}
