﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Utility
{
    /// <summary>
    /// Http封装
    /// </summary>
    public class HttpHelper
    {

        /// <summary>
        /// Get请求
        /// </summary>
        /// <param name="sUrl">请求的url</param>
        /// <returns></returns>
        public static string HttpGet(string sUrl)
        {
            string sResult = string.Empty;

            HttpWebRequest webRequest = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(sUrl);
            webRequest.ProtocolVersion = HttpVersion.Version10;
            webRequest.Timeout = 30000;
            webRequest.Method = WebRequestMethods.Http.Get;
            webRequest.Headers.Add("Accept-Encoding", "gzip, deflate");

            HttpWebResponse webResponse = (System.Net.HttpWebResponse)webRequest.GetResponse();
            if (webResponse.ContentEncoding.ToLower() == "gzip")//如果使用了GZip则先解压
            {
                using (System.IO.Stream streamReceive = webResponse.GetResponseStream())
                {
                    using (var zipStream = new System.IO.Compression.GZipStream(streamReceive, System.IO.Compression.CompressionMode.Decompress))
                    {
                        using (StreamReader sr = new System.IO.StreamReader(zipStream))
                        {
                            sResult = sr.ReadToEnd();
                        }
                    }
                }
            }
            else if (webResponse.ContentEncoding.ToLower() == "deflate")//如果使用了deflate则先解压
            {
                using (System.IO.Stream streamReceive = webResponse.GetResponseStream())
                {
                    using (var deflateStream = new System.IO.Compression.DeflateStream(streamReceive, System.IO.Compression.CompressionMode.Decompress))
                    {
                        using (StreamReader sr = new System.IO.StreamReader(deflateStream))
                        {
                            sResult = sr.ReadToEnd();
                        }
                    }
                }
            }
            else
            {
                using (System.IO.Stream streamReceive = webResponse.GetResponseStream())
                {
                    using (System.IO.StreamReader sr = new System.IO.StreamReader(streamReceive))
                    {
                        sResult = sr.ReadToEnd();
                    }
                }
            }
            return sResult;
        }

        /// <summary>
        /// Post请求
        /// </summary>
        /// <param name="sUrl">请求的链接</param>
        /// <param name="PostData">请求的参数</param>
        /// <returns></returns>
        public static string HttpPost(string sUrl, string PostData)
        {
            byte[] bPostData = System.Text.Encoding.UTF8.GetBytes(PostData);
            string sResult = string.Empty;

            HttpWebRequest webRequest = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(sUrl);
            webRequest.ProtocolVersion = HttpVersion.Version10;
            webRequest.Timeout = 30000;
            webRequest.Method = "POST";
            webRequest.Headers.Add("Accept-Encoding", "gzip, deflate");
            webRequest.ContentType ="application/x-www-form-urlencoded;charset=utf-8";
            if (bPostData != null)
            {
                Stream postDataStream = webRequest.GetRequestStream();
                postDataStream.Write(bPostData, 0, bPostData.Length);
            }
            HttpWebResponse webResponse = (System.Net.HttpWebResponse)webRequest.GetResponse();
            if (webResponse.ContentEncoding.ToLower() == "gzip")//如果使用了GZip则先解压
            {
                using (System.IO.Stream streamReceive = webResponse.GetResponseStream())
                {
                    using (var zipStream =
                        new System.IO.Compression.GZipStream(streamReceive, System.IO.Compression.CompressionMode.Decompress))
                    {
                        using (StreamReader sr = new System.IO.StreamReader(zipStream))
                        {
                            sResult = sr.ReadToEnd();
                        }
                    }
                }
            }
            else if (webResponse.ContentEncoding.ToLower() == "deflate")//如果使用了deflate则先解压
            {
                using (System.IO.Stream streamReceive = webResponse.GetResponseStream())
                {
                    using (var deflateStream = new System.IO.Compression.DeflateStream(streamReceive, System.IO.Compression.CompressionMode.Decompress))
                    {
                        using (StreamReader sr = new System.IO.StreamReader(deflateStream))
                        {
                            sResult = sr.ReadToEnd();
                        }
                    }
                }
            }
            else
            {
                using (System.IO.Stream streamReceive = webResponse.GetResponseStream())
                {
                    using (System.IO.StreamReader sr = new System.IO.StreamReader(streamReceive))
                    {
                        sResult = sr.ReadToEnd();
                    }
                }
            }
            return sResult;
        }
    }
}
