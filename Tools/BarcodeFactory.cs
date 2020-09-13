using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace LogisticsSystem.Tools
{
    public class BarcodeFactory
    {
        public static string GetBarcode()
        {
            //编码规则：代码(2)+年(2)+月(2)+日(2)+时(2)+分(2)+秒(2)+毫秒(2) + 随机(1)
            StringBuilder str = new StringBuilder();
            str.Append("96");
            str.Append(DateTime.Now.Year.ToString().Substring(2));
            str.Append((DateTime.Now.Month / 10 == 0) ? ("0" + DateTime.Now.Month.ToString()) : DateTime.Now.Month.ToString());
            str.Append((DateTime.Now.Day / 10 == 0) ? ("0" + DateTime.Now.Day.ToString()) : DateTime.Now.Day.ToString());
            str.Append((DateTime.Now.Hour / 10 == 0) ? ("0" + DateTime.Now.Hour.ToString()) : DateTime.Now.Hour.ToString());
            str.Append((DateTime.Now.Minute / 10 == 0) ? ("0" + DateTime.Now.Minute.ToString()) : DateTime.Now.Minute.ToString());
            str.Append((DateTime.Now.Second / 10 == 0) ? ("0" + DateTime.Now.Second.ToString()) : DateTime.Now.Second.ToString());
            Thread.Sleep(100);
            str.Append((DateTime.Now.Millisecond) / 100);

            //for (int i = str.Length; i <= 13; i++)
            //    str.Append(new Random((int)DateTime.Now.Ticks).Next(0, 9));
            return str.ToString();
        }
    }
}
