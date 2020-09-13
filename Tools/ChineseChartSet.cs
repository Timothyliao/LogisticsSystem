using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticsSystem.Tools
{
    public class ChineseChartSet
    {
        public static string GetChineseMoney(string moneyStr)
        {
            ////壹 贰 叁 肆 伍 陆 柒 捌 玖 拾
            //double money = Convert.ToDouble(moneyStr);
            //int digit = moneyStr.Split('.')[0].Length;
            //StringBuilder builder = new StringBuilder();
            //if (digit > 8)
            //{
            //    string money1 = moneyStr.Remove(digit - 8);


            //}
            return null;
        }

        private static string ToChineseUpper(string character)
        {
            switch (character)
            {
                case "1": return "壹";
                case "2": return "贰";
                case "3": return "叁";
                case "4": return "肆";
                case "5": return "伍";
                case "6": return "陆";
                case "7": return "柒";
                case "8": return "捌";
                case "9": return "玖";
                default: return "拾";
            }
        }
    }
}
