using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticsSystem.Tools
{
    public class Region
    {
        public static string GetRegion(string province)
        {
            if (province.Contains("山东") || province.Contains("江苏") || province.Contains("安徽") || province.Contains("浙江") || province.Contains("福建") || province.Contains("上海") || province.Contains("台湾"))
                return "华东";
            else if (province.Contains("广东") || province.Contains("广西") || province.Contains("海南"))
                return "华南";
            else if (province.Contains("湖北") || province.Contains("湖南") || province.Contains("河南") || province.Contains("江西"))
                return "华中";
            else if (province.Contains("北京") || province.Contains("天津") || province.Contains("河北") || province.Contains("山西") || province.Contains("蒙古"))
                return "华北";
            else if (province.Contains("辽宁") || province.Contains("吉林") || province.Contains("黑龙江"))
                return "东北";
            else if (province.Contains("重庆") || province.Contains("四川") || province.Contains("云南") || province.Contains("西藏") || province.Contains("贵州"))
                return "西南";
            else
                return "西北";
        }
    }
}
