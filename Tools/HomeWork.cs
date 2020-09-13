using System;
using System.Collections.Generic;

namespace LogisticsSystem.Tools
{
    public class LogisticsSystemHomeWork
    {
        public string Centroid(List<string> location, List<double> demand, double rate)
        {
            List<double> dist = new List<double>();//重心到个点的直线距离
            double fee = 0;//总费用

            //初始化重心心坐标
            double initialX = 0, initialY = 0;
            foreach (var item in location)
            {
                initialX += double.Parse(item.Split(',')[0]);
                initialY += double.Parse(item.Split(',')[1]);
            }
            initialX /= location.Count;
            initialY /= location.Count;

            double tempFee = 0;
            do
            {
                double moleculeX = 0, moleculeY = 0;//分子
                double denominator = 0;//分母
                for (int i = 0; i < location.Count; i++)
                {
                    double diffX = Math.Pow(double.Parse(location[i].Split(',')[0]) - initialX, 2);
                    double diffY = Math.Pow(double.Parse(location[i].Split(',')[1]) - initialY, 2);
                    dist[i] = Math.Sqrt(diffX + diffY);//重心点到个点的距离

                    tempFee += Math.Sqrt(diffX + diffY) * demand[i] * rate;//单点费用

                    moleculeX += rate * demand[i] * double.Parse(location[i].Split(',')[0]) / Math.Sqrt(diffX + diffY);
                    moleculeY += rate * demand[i] * double.Parse(location[i].Split(',')[1]) / Math.Sqrt(diffX + diffY);
                    denominator += rate * demand[i] / Math.Sqrt(diffX + diffY);

                    fee = fee == 0 ? tempFee : fee;
                }
                //新的点
                initialX = moleculeX / denominator;
                initialY = moleculeY / denominator;

            } while (tempFee < fee);

            return $"{initialX},{initialY}";
        }
    }
}