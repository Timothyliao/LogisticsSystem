using LogisticsSystem.Tools;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;

namespace MaverickSystem
{
    /// <summary>
    /// DrawBarCode 的摘要说明
    /// </summary>
    public class DrawBarCode : IHttpHandler
    {
        private string code = string.Empty;
        private string stripAndSpace = string.Empty;//条空组合
        private int TotalOfStripAndSpace = 0;//条空数量
        private float tbPadding = 10;//上下边距
        private float leftBlankW = 0;//左侧空白区
        private float rightBlankW = 0;//右侧空白区
        private float modleW = 0.3F;//模块宽度(0.25mm~1.013mm)
        private float modleH = 32F;//模块高度
        public void ProcessRequest(HttpContext context)
        {
            string codenum = context.Request.QueryString["code"].ToString();
            HandleBarCodeNum(codenum);
            float boxW = TotalOfStripAndSpace * modleW + leftBlankW + rightBlankW;//pictureBox的宽度
            float boxH = tbPadding * 2 + modleH;

            Bitmap map = new Bitmap(Convert.ToInt32(boxW) + 1, Convert.ToInt32(boxH) + 1);
            Graphics g = Graphics.FromImage(map);
            g.Clear(Color.White);
            SolidBrush brush = new SolidBrush(Color.Black);
            float coordX = leftBlankW, coordY = tbPadding;
            char[] array = stripAndSpace.ToArray();
            for (int i = 0, len = array.Length; i < len; i++)
            {
                int num = Convert.ToInt32(array[i].ToString());
                if (i == 0 || i % 2 == 0)
                    g.FillRectangle(brush, new RectangleF(coordX, coordY, modleW * num, modleH));

                coordX += modleW * num;
            }

            //清除该页输出缓存，设置该页无缓存 
            context.Response.Buffer = true;
            context.Response.ExpiresAbsolute = DateTime.Now.AddMilliseconds(0);
            context.Response.Expires = 0;
            context.Response.CacheControl = "no-cache";
            context.Response.AppendHeader("Pragma", "No-Cache");
            //将验证码图片写入内存流，并将其以 "image/Png" 格式输出  
            MemoryStream ms = new MemoryStream();
            try
            {
                map.Save(ms, ImageFormat.Png);
                context.Response.ContentType = "image/Png";
                context.Response.BinaryWrite(ms.ToArray());
            }
            finally
            {
                //显式释放资源  
                map.Dispose();
                g.Dispose();
                brush.Dispose();
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        public void HandleBarCodeNum(string _code)
        {
            UCC_EAN_128Standard ean128Standard = new UCC_EAN_128Standard();
            List<string> encodingCode = ean128Standard.InsertCodingChar(_code);
            stripAndSpace = ean128Standard.CalcuCheckCode(encodingCode);
            code = ean128Standard.DistinguishIdentifier(_code);
            //计算条空数
            char[] array = stripAndSpace.ToArray();
            int sum = 0;
            for (int i = 0, len = array.Length; i < len; i++)
                sum += Convert.ToInt32(array[i].ToString());
            TotalOfStripAndSpace = sum;
            float scale = 0.85F;
            CodeAssist assist = new CodeAssist();
            modleW = assist.MillimeterToPixel(modleW);
            modleH = assist.MillimeterToPixel(modleH, 2);
            modleW *= scale;
            modleH *= scale;
            leftBlankW = 10 * modleW;
            rightBlankW = 10 * modleW;
        }
    }
}