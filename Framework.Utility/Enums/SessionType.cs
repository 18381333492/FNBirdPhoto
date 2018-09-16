using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Utility
{
    /// <summary>
    /// Session的类型
    /// </summary>
    public class SessionType
    {
        public static string USER = "@USER";

        /// <summary>
        /// 用户缓存的菜单
        /// </summary>
        public static string MENU = "@MENU";

        /// <summary>
        /// 用户缓存的按钮
        /// </summary>
        public static string BTN = "@BTN";

        /// <summary>
        /// 用户缓存的图形验证码
        /// </summary>
        public static string CODE = "@CODE";
    }
}
