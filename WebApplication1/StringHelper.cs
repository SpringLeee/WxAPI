﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace WebApplication1
{
    public class StringHelper
    {
        #region   跨平台加解密（c# 安卓 IOS）
        public static string sKey = "ttkkll9#";
        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="pToDecrypt">要解密的以Base64</param>
        /// <param name="sKey">密钥，且必须为8位</param>
        /// <returns>已解密的字符串</returns>
        public static string DesDecrypt(string pToDecrypt)
        {
            //把空格转+
            pToDecrypt = pToDecrypt.Replace(" ", "+");

            //转义特殊字符
            pToDecrypt = pToDecrypt.Replace("-", "+");
            pToDecrypt = pToDecrypt.Replace("_", "/");
            pToDecrypt = pToDecrypt.Replace("~", "=");
            byte[] inputByteArray = Convert.FromBase64String(pToDecrypt);
            using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
            {
                des.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
                des.IV = ASCIIEncoding.ASCII.GetBytes(sKey);
                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                using (CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(inputByteArray, 0, inputByteArray.Length);
                    cs.FlushFinalBlock();
                    cs.Close();
                }
                string str = Encoding.UTF8.GetString(ms.ToArray());
                ms.Close();
                return str;
            }
        }

        /// <summary>
        /// 对字符串进行DES加密
        /// </summary>
        /// <param name="sourceString">待加密的字符串</param>
        /// <returns>加密后的BASE64编码的字符串</returns>
        public static string DesEncrypt(string sourceString)
        {
            if (sourceString == null)
            {
                sourceString = "";
            }
            byte[] btKey = Encoding.UTF8.GetBytes(sKey);
            byte[] btIV = Encoding.UTF8.GetBytes(sKey);
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            using (MemoryStream ms = new MemoryStream())
            {
                byte[] inData = Encoding.UTF8.GetBytes(sourceString);
                try
                {
                    using (CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(btKey, btIV), CryptoStreamMode.Write))
                    {
                        cs.Write(inData, 0, inData.Length);
                        cs.FlushFinalBlock();
                    }

                    return Convert.ToBase64String(ms.ToArray());
                }
                catch
                {
                    throw;
                }
            }
        }

        #endregion  
    }
}