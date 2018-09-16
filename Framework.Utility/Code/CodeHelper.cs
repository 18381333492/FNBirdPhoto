using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using TraceLogs;
using System.Net;
using System.IO;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace Framework.Utility
{
    /// <summary>
    /// 发送短信验证码助手
    /// </summary>
    public class CodeHelper
    {
        private static ILogger logger = LoggerManager.Instance.GetSLogger("Code");
        //短信配置文件路径
        private static readonly string sPath = AppDomain.CurrentDomain.BaseDirectory + ConfigHelper.GetAppSetting("CodeConfigPath");
        //账户APPID
        private static string sAppId = string.Empty;
        //Key
        private static string SecretKey = string.Empty;
        //请求地址
        private static string sUrl = string.Empty;
        //模板子节点
        private static XmlNodeList nodeList;

        /// <summary>
        /// 构造函数
        /// </summary>
        static CodeHelper()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(sPath);
            sAppId = doc.SelectSingleNode("CodeConfig/AppId").InnerText;
            SecretKey= doc.SelectSingleNode("CodeConfig/SecretKey").InnerText;
            sUrl = doc.SelectSingleNode("CodeConfig/sUrl").InnerText;
            nodeList =doc.SelectNodes("CodeConfig/Templets/Templet");
        }

        /// <summary>
        /// 发送短信验证码
        /// </summary>
        /// <param name="sPhone"></param>
        /// <param name="Code"></param>
        /// <param name="codeType"></param>
        /// <returns></returns>
        public static bool Send(string sPhone, string Code, CodeType codeType)
        {

            string template = Enum.GetName(typeof(CodeType), codeType);
            logger.Info("手机号：" + sPhone);
            logger.Info("验证码：" + Code);
            string timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");
            string SignString = sAppId + SecretKey + timestamp;
            logger.Info("签名原串:" + SignString);
            string sign = SecurityHelper.md5(SignString);
            logger.Info("签名串:" + sign);
            string sContent = string.Format(GetInnerTextByAttr(template), Code);
            string param = string.Format("appId={0}&timestamp={1}&sign={2}&mobiles={3}&content={4}", sAppId, timestamp, sign, sPhone, sContent);
            string result = HttpPost(sUrl, param);
            if (!string.IsNullOrEmpty(result))
            {
                var job = JObject.Parse(result);
                if (job["code"].ToString() == "SUCCESS")
                {
                    logger.Info("发送成功");
                    return true;
                }
                else
                {
                    logger.Info("返回结果:" + result);
                    return false;
                }
            }
            else
                return false;
        }

        
        /// <summary>
        /// 发送自定义短信内容
        /// </summary>
        /// <param name="sPhone"></param>
        /// <param name="Content"></param>
        /// <returns></returns>
        public static bool Send(string sPhone,string sContent)
        {
            string timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");//时间撮
            string SignString = sAppId + SecretKey + timestamp;
            logger.Info("签名原串:" + SignString);
            string sign = SecurityHelper.md5(SignString);
            logger.Info("签名串:" + sign);
            string param = string.Format("appId={0}&timestamp={1}&sign={2}&mobiles={3}&content={4}", sAppId, timestamp, sign, sPhone, sContent);
            logger.Info("请求参数:"+param);
            string result = HttpPost(sUrl, param);
            if (!string.IsNullOrEmpty(result))
            {
                var job = JObject.Parse(result);
                if (job["code"].ToString() == "SUCCESS")
                {
                    logger.Info("发送成功");
                    return true;
                }
                else
                {
                    logger.Info("返回结果:" + result);
                    return false;
                }
            }
            else
                return false;
        }


        /// <summary>
        /// 根据属性值获取节点text
        /// </summary>
        /// <returns></returns>
        public static string GetInnerTextByAttr(string sAttrValue)
        {
            string sText = string.Empty;
            foreach(XmlNode item in nodeList)
            {
                if(item.Attributes["name"].Value == sAttrValue)
                {
                    sText = item.InnerText;
                    break;
                }
            }
            return sText.Trim();
        }

        /// <summary>
        /// post请求
        /// </summary>
        /// <param name="sUrl"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        private static string HttpPost(string sUrl, string data)
        {
            byte[] bPostData = System.Text.Encoding.UTF8.GetBytes(data);
            string sResult = string.Empty;
            try
            {
                HttpWebRequest webRequest = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(sUrl);
                webRequest.ProtocolVersion = HttpVersion.Version10;
                webRequest.Timeout = 30000;
                webRequest.Method = "POST";
                webRequest.ContentType = "application/x-www-form-urlencoded";
                if (bPostData != null)
                {
                    Stream postDataStream = webRequest.GetRequestStream();
                    postDataStream.Write(bPostData, 0, bPostData.Length);
                }
                HttpWebResponse webResponse = (System.Net.HttpWebResponse)webRequest.GetResponse();

                using (System.IO.Stream streamReceive = webResponse.GetResponseStream())
                {
                    using (System.IO.StreamReader sr = new System.IO.StreamReader(streamReceive))
                    {
                        sResult = sr.ReadToEnd();
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Info(ex.Message);
                logger.Fatal(ex.Message);
            }
            return sResult;
        }
    }
}
