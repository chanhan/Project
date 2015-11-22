/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： EncryptClass.cs
 * 檔功能描述： 加密解密算法
 * 
 * 版本：1.0
 * 創建標識： Lucky Lee 2011.09.20
 * 
 */

using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace GDSBG.MiABU.Attendance.Common
{
    /// <summary>
    /// 加密解密算法類
    /// </summary>
    public class EncryptClass
    {
        private string mKey = string.Empty;
        private SymmetricAlgorithm mCryptoService;
        private static EncryptClass encryptInstance;

        /// <summary>
        /// 構造函數，設置加密算法為Rijndael(AES)
        /// </summary> 
        private EncryptClass()
        {
            mCryptoService = new RijndaelManaged();
            mCryptoService.Mode = CipherMode.CBC;
            //默認密碼
            mKey = "wqdj~yriu!@*k0_^fa7431%p$#=@hd+&";
        }

        /// <summary>
        /// 獲得加密實例
        /// </summary>
        public static EncryptClass Instance
        {
            get
            {
                if (encryptInstance == null)
                {
                    encryptInstance = new EncryptClass();
                }
                return encryptInstance;
            }
        }

        /// <summary>
        /// 設置對稱算法的初始化向量
        /// </summary>
        private void SetLegalIV()
        {
            mCryptoService.IV = new byte[] { 0xf, 0x6f, 0x13, 0x2e, 0x35, 0xc2, 0xcd, 0xf9, 0x5, 0x46, 0x9c, 0xea, 0xa8, 0x4b, 0x73, 0xcc };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private byte[] GetLegalKey()
        {
            //對于Rijndael算法，密鑰長度為16，24或者32字節
            //可以人工輸入，也可以隨機生成，方法是: des.GenerateKey();
            //對于不符合要求的key,需調整其內容
            if (mCryptoService.LegalKeySizes.Length > 0)
            {

                int keySize = mKey.Length * 8;
                int minSize = mCryptoService.LegalKeySizes[0].MinSize;
                int maxSize = mCryptoService.LegalKeySizes[0].MaxSize;
                int skipSize = mCryptoService.LegalKeySizes[0].SkipSize;

                if (keySize > maxSize)
                {
                    mKey = mKey.Substring(0, maxSize / 8);
                }
                else if (keySize < maxSize)
                {

                    int validSize = (keySize <= minSize) ? minSize : (keySize - keySize % skipSize) + skipSize;
                    if (keySize < validSize)
                    {

                        mKey = mKey.PadRight(validSize / 8, '*');
                    }
                }
            }
            PasswordDeriveBytes key = new PasswordDeriveBytes(mKey, ASCIIEncoding.ASCII.GetBytes(string.Empty));
            return key.GetBytes(mKey.Length);
        }

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="plainText">明文</param>
        /// <returns>密文</returns>
        public string Encrypt(string plainText)
        {
            byte[] cryptoByte = null;
            byte[] plainByte = System.Text.UTF8Encoding.UTF8.GetBytes(plainText);
            try
            {
                byte[] keyByte = GetLegalKey();

                //設定key和向量
                mCryptoService.Key = keyByte;
                SetLegalIV();

                //加密對象
                ICryptoTransform cryptoTransform = mCryptoService.CreateEncryptor();
                //內存流對象
                MemoryStream ms = new MemoryStream();
                //初始化加密流
                CryptoStream cs = new CryptoStream(ms, cryptoTransform, CryptoStreamMode.Write);
                //將加密后的數據寫入加密流
                cs.Write(plainByte, 0, plainByte.Length);
                cs.FlushFinalBlock();
                cs.Close();

                //得到加密后的數據
                cryptoByte = ms.ToArray();
            }
            catch
            {
                return null;
            }

            //將數據轉換成base64字符串
            return Convert.ToBase64String(cryptoByte, 0, cryptoByte.GetLength(0));
        }

        /// <summary>
        /// 解密
        /// 解密的時候用的key和向量必須和加密的時候用的一樣
        /// </summary>
        /// <param name="cryptoText">密文</param>
        /// <returns>明文</returns>
        public string Decrypt(string cryptoText)
        {
            //從base64字符串轉換成字節
            if (cryptoText == null)
            {
                return null;
            }

            byte[] cryptoByte = Convert.FromBase64String(cryptoText);
            byte[] keyByte = GetLegalKey();

            //設定key和向量
            mCryptoService.Key = keyByte;
            SetLegalIV();

            //解密對象
            ICryptoTransform cryptoTransform = mCryptoService.CreateDecryptor();
            try
            {
                //內存流對象
                MemoryStream ms = new MemoryStream(cryptoByte, 0, cryptoByte.Length);
                //初始化一個解密流對象
                CryptoStream cs = new CryptoStream(ms, cryptoTransform, CryptoStreamMode.Read);
                //從解密流對象中得到解密后的數據
                StreamReader sr = new StreamReader(cs);
                return sr.ReadToEnd();
            }
            catch
            {
                return null;
            }
        }
    }
}