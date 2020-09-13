using LogisticsSystem.Tools;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;

namespace MaverickSystem
{
    /// <summary>
    /// DrawCode39Bar 的摘要说明
    /// </summary>
    public class DrawCode39Bar : IHttpHandler
    {
        private string code = string.Empty;//处理后的code39编码
        private float wideModleW = 0;//宽单元宽度
        private float narrowModleW = 0;//窄单元宽度
        private float modleH = 0;//单元高度
        private float lwRatio = 0.2F;//模块单元的长宽比，即长为宽的lwRatio倍
        private float blankW = 0;//空白区域的宽度
        private float blankRatio = 10;//空白区域为窄单元宽度的倍数
        private float fontEmSize = 0;//编码数字字体大小
        private float tbPadding = 10;//上下边距
        private float fontPadding = 4;//编码数字上边距
        private string[] binarySeq;//二进制序列
        private int interval = 1;//字符间隔
        private int barSizeIndex = 0;
        private bool haveCheck = true;

        public void ProcessRequest(HttpContext context)
        {
            string codenum = context.Request.QueryString["code"].ToString();
            HandleBarCodeNum(codenum, 2);
            float totalW = (code.Length + 2) * (3 * wideModleW + 6 * narrowModleW) + 2 * blankW + (code.Length + 1) * interval * narrowModleW;
            float totalH = modleH;
            Bitmap map = new Bitmap(Convert.ToInt32(totalW) + 1, Convert.ToInt32(totalH) + 1);
            Graphics g = Graphics.FromImage(map);
            g.Clear(Color.White);
            SolidBrush brush = new SolidBrush(Color.Black);

            float coordX = blankW, coordY = tbPadding;//起始坐标
            for (int i = 0; i < binarySeq.Length; i++)
            {
                char[] aim = binarySeq[i].ToArray();
                for (int j = 0; j < 5; j++)
                {
                    if (aim[j] == '0')
                    {
                        g.FillRectangle(brush, new RectangleF(coordX, coordY, narrowModleW, modleH));
                        coordX += narrowModleW;
                    }
                    else
                    {
                        g.FillRectangle(brush, new RectangleF(coordX, coordY, wideModleW, modleH));
                        coordX += wideModleW;
                    }
                    if (j == 4)
                        break;
                    if (aim[j + 5] == '0')
                        coordX += narrowModleW;
                    else
                        coordX += wideModleW;
                }

                coordX += interval * narrowModleW;
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
                map.Save(ms, ImageFormat.Bmp);
                context.Response.ContentType = "image/Bmp";
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

        public void HandleBarCodeNum(string _code, int _scale)
        {
            CodeAssist assist = new CodeAssist();
            Code39Standard standard = new Code39Standard();
            if (haveCheck)
                code = standard.CalcuCheckCode(_code);
            else
                code = _code;

            float scale = 0;
            switch (_scale)
            {
                case 0:
                    scale = 0.8F;
                    break;
                case 1:
                    scale = 0.85F;
                    break;
                case 2:
                    scale = 0.9F;
                    break;
                case 3:
                    scale = 1.00F;
                    break;
                case 4:
                    scale = 1.1F;
                    break;
                case 5:
                    scale = 1.2F;
                    break;
                case 6:
                    scale = 1.3F;
                    break;
                case 7:
                    scale = 1.4F;
                    break;
                case 8:
                    scale = 1.5F;
                    break;
                case 9:
                    scale = 1.6F;
                    break;
                case 10:
                    scale = 1.7F;
                    break;
                case 11:
                    scale = 1.8F;
                    break;
                case 12:
                    scale = 1.9F;
                    break;
                case 13:
                    scale = 2.0F;
                    break;
            }

            narrowModleW = assist.MillimeterToPixel(standard.ZoomFactor[barSizeIndex][0]) * scale;
            wideModleW = assist.MillimeterToPixel(standard.ZoomFactor[barSizeIndex][1]) * scale;

            //计算模块单元的高度
            modleH = (code.Length + 2) * (3 * wideModleW + 6 * narrowModleW) * lwRatio;
            if (modleH < assist.MillimeterToPixel(6.4))
                modleH = assist.MillimeterToPixel(6.4);

            //计算空白区域的宽度
            blankW = blankRatio * narrowModleW >= assist.MillimeterToPixel(2.5) ? blankRatio * narrowModleW : assist.MillimeterToPixel(2.5);

            binarySeq = standard.ConstructBinarySequence(code);
        }
    }
}