using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticsSystem.Tools
{
    public class MaverickCost
    {
        public static double CalcFreight(double weight, double volumn, double distance = 0)
        {
            /*暂行计算方法，小于1吨按体积收费（95元/立方），大于1吨按重量收费（350元/吨）*/
            if (weight < 1000)
                return 55 * volumn;
            else
                return weight * 0.35;
        }

        public static string CalcInSurenceFee(double w)
        {
            double NITA = 0.00010;
            double BEITEA0 = 0.00086;
            double BEITEA = 0.0022;

            double tao = 0.2 * BEITEA, af = 0.8 * BEITEA;
            double vMax = 0;
            if (w > 0 && w <= 1000) vMax = 1000;
            else vMax = w;

            /*----------------------------------------------------------------------*/
            double x = NITA * (1 - BEITEA) * BEITEA;

            //求A
            double a1 = 3 * (tao + BEITEA0) - 2 * tao * (af - BEITEA0 - BEITEA) - 2 * BEITEA0 * (af - BEITEA0 - BEITEA);
            double A = Math.Sqrt(x) * a1;

            //求B
            double b1 = -1 * (tao + BEITEA0);
            double b2 = af - BEITEA0 - 2 * vMax * x;
            double b3 = BEITEA * (af - BEITEA0 - BEITEA);
            double b4 = 2 * NITA * (1 - BEITEA) * tao * vMax - tao + 2 * NITA * (1 - BEITEA) * BEITEA0 * vMax;
            double B = b1 * b2 - b3 * b4;

            //求m
            double m = B / A;

            //求K
            double k1 = m * ((af - BEITEA0) * BEITEA - 2 * vMax * NITA * (1 - BEITEA) * BEITEA0 + 3 * Math.Sqrt(x) * BEITEA0 * m);
            double k2 = (af - BEITEA0 - BEITEA) * Math.Sqrt(x);
            double k = k1 / k2;

            //result
            double theta = BEITEA * k + m * m;

            double z = Math.Sqrt((theta - BEITEA * k) / x);

            double profit = af * w + tao * w + theta;

            return profit.ToString("f2");
        }
    }
}
