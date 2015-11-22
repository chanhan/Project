/*
 * Copyright (C) 2011 GDSBG MIABU 版權所有。
 * 
 * 檔案名： Validate.cs
 * 檔功能描述： 生成驗證碼圖片
 * 
 * 版本：1.0
 * 創建標識： Lucky Lee 2011.09.19
 * 
 */

using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using GDSBG.MiABU.Attendance.Common;

namespace GDSBG.MiABU.Attendance.Web
{
    /// <summary>
    /// 生成驗證碼圖片
    /// </summary>
    public partial class Validate : System.Web.UI.Page
    {
        private void Page_Load(object sender, System.EventArgs e)
        {
            DrawCheckCode(GetCheckCode());
        }

        /// <summary>
        /// 生成驗證碼
        /// </summary>
        /// <returns></returns>
        private string GetCheckCode()
        {
            int number;
            char code;
            string checkCode = string.Empty;
            Random oRandom = new Random();
            for (int i = 0; checkCode.Length < 1; i++)
            {
                number = oRandom.Next();
                if (number % 2 != 0)
                {
                    code = (char)('A' + (number % 26));
                    checkCode = checkCode + code.ToString();
                }
            }
            Session[GlobalData.ValidateCodeSessionKey] = checkCode.ToUpper();
            return checkCode;
        }

        /// <summary>
        /// 生成圖片
        /// </summary>
        /// <param name="code"></param>
        private void DrawCheckCode(string code)
        {
            if (code == null || code.Trim() == string.Empty)
                return;
            Bitmap oBitmap = new Bitmap((int)Math.Ceiling(code.Length * 15.5*5), 28);
            Graphics oGraphic;
            oGraphic = Graphics.FromImage(oBitmap);

            try
            {
                Random oRandom = new Random();
                oGraphic.Clear(Color.White);

                for (int i = 0; i < 56; i++)
                {
                    int x1 = oRandom.Next(oBitmap.Width);
                    int x2 = oRandom.Next(oBitmap.Width);
                    int y1 = oRandom.Next(oBitmap.Height);
                    int y2 = oRandom.Next(oBitmap.Height);
                    oGraphic.DrawLine(new Pen(Color.LightGreen), x1, y1, x2, y2);
                }
                Font oFont = new Font("Arial", 14.5f, FontStyle.Bold | FontStyle.Italic);
                LinearGradientBrush oBrush = new LinearGradientBrush(
                    new Rectangle(0, 0, oBitmap.Width, oBitmap.Height), Color.DeepPink, Color.Green, 1.2f, true);
                oGraphic.DrawString(code, oFont, oBrush, 2, 2);

                for (int i = 0; i < 220; i++)
                {
                    int x = oRandom.Next(oBitmap.Width);
                    int y = oRandom.Next(oBitmap.Height);
                    oBitmap.SetPixel(x, y, Color.FromArgb(oRandom.Next()));
                }
                oGraphic.DrawRectangle(new Pen(Color.LightGreen), 0, 0, oBitmap.Width - 1, oBitmap.Height - 1);
                MemoryStream ms = new MemoryStream();
                oBitmap.Save(ms, ImageFormat.Png);
                Response.ClearContent();
                Response.ContentType = "image/Png";
                Response.BinaryWrite(ms.ToArray());
            }
            catch
            {
            }
            finally
            {
                oGraphic.Dispose();
                oBitmap.Dispose();
            }
        }
    }
}
