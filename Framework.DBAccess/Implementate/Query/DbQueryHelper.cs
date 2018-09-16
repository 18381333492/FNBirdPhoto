using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Framework.Utility;
using System.Data;

namespace Framework.DBAccess
{
    /// <summary>
    /// DB查询
    /// </summary>
    public class DbQueryHelper : DbQueryManager, IDbQuery
    {
        /// <summary>
        /// 根据主键ID查询实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ID"></param>
        /// <returns></returns>
        public T Find<T>(object ID) where T : new()
        {
            IDbConnection conn = GetSqlConnection();
            try
            {
                string sqlCommand = string.Format(@"SELECT TOP 1 * FROM {0} WHERE ID=@ID", typeof(T).Name);
                return DoSingleQuery<T>(conn, sqlCommand, new { ID = ID });
            }
            catch (Exception ex)
            {
                logger.Info(ex.Message);
                logger.Fatal(ex);
                return default(T);
            }
            finally
            {
                CloseConnect(conn);
            }
        }

        /// <summary>
        /// 根据条件查询是否存在相应的数据
        /// </summary>
        /// <param name="sqlCommand"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public bool? Any(string sqlCommand, object parameter = null)
        {
            IDbConnection conn = GetSqlConnection();
            try
            {
                return DoAny(conn, sqlCommand, parameter);
            }
            catch (Exception ex)
            {
                logger.Info(ex.Message);
                logger.Fatal(ex);
                return null;
            }
            finally
            {
                CloseConnect(conn);
            }
        }

        /// <summary>
        /// 查询单条数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sqlCommand"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public T SingleQuery<T>(string sqlCommand, object parameter = null) 
        {
            IDbConnection conn = GetSqlConnection();
            try
            { 
                return DoSingleQuery<T>(conn, sqlCommand, parameter);
            }
            catch (Exception ex)
            {
                logger.Info(ex.Message);
                logger.Fatal(ex);
                return default(T);
            }
            finally
            {
                CloseConnect(conn);
            }
        }

        /// <summary>
        /// 多条数据查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sqlCommand"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public IList<T> QueryList<T>(string sqlCommand, object parameter = null) where T : new()
        {
            IDbConnection conn = GetSqlConnection();
            try
            {
                return DoQueryList<T>(conn, sqlCommand, parameter);
            }
            catch (Exception ex)
            {
                logger.Info(ex.Message);
                logger.Fatal(ex);
                return default(IList<T>);
            }
            finally
            {
                CloseConnect(conn);
            }
        }

        /// <summary>
        ///  多条数据查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sqlCommand"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public IList<T> QueryList<T>(string sqlCommand, DbParameters parameter) where T : new()
        {
            IDbConnection conn = GetSqlConnection();
            try
            {
                return DoQueryList<T>(conn, sqlCommand, parameter.GetParameters());
            }
            catch (Exception ex)
            {
                logger.Info(ex.Message);
                logger.Fatal(ex);
                return default(IList<T>);
            }
            finally
            {
                CloseConnect(conn);
            }
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="sqlCommand"></param>
        /// <param name="pageInfo"></param>
        /// <returns></returns>
        public PageResult PaginationQuery(string sqlCommand, PageInfo pageInfo)
        {
            IDbConnection conn = GetSqlConnection();
            try
            {
                return DoPaginationQuery(conn, sqlCommand, pageInfo, null);
            }
            catch (Exception ex)
            {
                logger.Info(ex.Message);
                logger.Fatal(ex);
                return null;
            }
            finally
            {
                CloseConnect(conn);
            }
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="sqlCommand"></param>
        /// <param name="pageInfo"></param>
        /// <param name="Parameters"></param>
        /// <returns></returns>
        public PageResult PaginationQuery(string sqlCommand, PageInfo pageInfo, DbParameters Parameters=null)
        {
            IDbConnection conn = GetSqlConnection();
            try
            {
                if (Parameters == null)
                    return DoPaginationQuery(conn, sqlCommand, pageInfo, null);
                else
                    return DoPaginationQuery(conn, sqlCommand, pageInfo, Parameters.GetParameters());
            }
            catch (Exception ex)
            {
                logger.Info(ex.Message);
                logger.Fatal(ex);
                return null;
            }
            finally
            {
                CloseConnect(conn);
            }
        }

        /// <summary>
        /// 执行存储过程返回结果集
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sProcedureName"></param>
        /// <returns></returns>
        public IList<T> QueryProcedure<T>(string sProcedureName)
        {
            IDbConnection conn = GetSqlConnection();
            try
            {
                return DoQueryProcedure<T>(conn, sProcedureName,null);
            }
            catch (Exception ex)
            {
                logger.Info(ex.Message);
                logger.Fatal(ex);
                return default(IList<T>);
            }
            finally
            {
                CloseConnect(conn);
            }
        }

        /// <summary>
        /// 执行存储过程返回结果集
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sProcedureName"></param>
        /// <param name="Parameters"></param>
        /// <returns></returns>
        public IList<T> QueryProcedure<T>(string sProcedureName, DbParameters Parameters)
        {
            IDbConnection conn = GetSqlConnection();
            try
            {
                return DoQueryProcedure<T>(conn, sProcedureName, Parameters.GetParameters());
            }
            catch (Exception ex)
            {
                logger.Info(ex.Message);
                logger.Fatal(ex);
                return default(IList<T>);
            }
            finally
            {
                CloseConnect(conn);
            }
        }
    }
}
