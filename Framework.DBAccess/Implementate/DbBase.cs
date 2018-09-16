using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TraceLogs;
using Framework.Utility;

namespace Framework.DBAccess
{
    /// <summary>
    /// 数据库的链接
    /// </summary>
    public class DbBase
    {
        /// <summary>
        /// 日志记录
        /// </summary>
        protected static ILogger logger = LoggerManager.Instance.GetSLogger("dapper");

        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        protected static string sConnectionString = ConfigHelper.GetConnString("DbConnection");

        /// <summary>
        /// 打开数据库连接
        /// </summary>
        /// <returns></returns>
        protected virtual IDbConnection GetSqlConnection()
        {
            try
            {
                SqlConnection conn = new SqlConnection(sConnectionString);
                conn.Open();
                return conn;
            }
            catch (Exception ex)
            {
                logger.Info(ex.Message);
                logger.Fatal(ex);
                return null;
            }
        }
  
        /// <summary>
        /// 关闭数据库连接
        /// </summary>
        /// <param name="conn"></param>
        protected void CloseConnect(IDbConnection conn)
        {
            if (conn != null && conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
        }
    }
}
