using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticsSystem.Tools
{
    /// <summary>
    /// Dynamic Path Planning
    /// </summary>
    public class DPP
    {
        /// <summary>
        /// 计算节约值
        /// </summary>
        /// <param name="dist">各节点之间的距离</param>
        /// <param name="nodeNum">节点数量</param>
        /// <param name="orign">出发点</param>
        /// <param name="count">计算出来的节约弧的条数</param>
        /// <returns></returns>
        private Saving[] CalcuSavingV(double[,] dist, int nodeNum, int orign)
        {
            List<Saving> svList = new List<Saving>();
            for (int i = 1; i < nodeNum; i++)
                for (int j = 1; j < nodeNum; j++)
                {
                    double value = dist[orign, i] + dist[orign, j] - dist[i, j];
                    if (i != j && value > 0)
                        svList.Add(new Saving() { start = i, end = j, savingValue = value.ToString("F3") });
                }
            Saving[] _savingValue = svList.ToArray();
            return _savingValue;
        }


        /// <summary>
        /// 挑选最短路径
        /// </summary>
        /// <param name="savingV">节约值数组</param>
        /// <param name="dist">各节点之间的距离</param>
        /// <param name="nodeCount">节点的个数</param>
        /// <param name="num">计算出来的节约弧的条数</param>
        /// <returns></returns>
        public List<string> AnalyzingSPFA(Saving[] savingV, double[,] dist, int nodeCount, int begin)
        {
            int[] flag = new int[nodeCount];
            int num = savingV.Length;
            string res = savingV[0].start.ToString() + "/" + savingV[0].end.ToString();
            flag[savingV[0].start] += 1;
            flag[savingV[0].end] += 1;
            for (int i = 1; i < num; i++)
            {
                int start = savingV[i].start;
                int end = savingV[i].end;
                //1.两端点的值不能相等
                //2.已接成功的值（出现在两端弧中），不能出现
                if (flag[start] < 2 && flag[end] < 2)
                {
                    var list = res.Split('/');
                    int front = int.Parse(list[0].ToString()), rear = int.Parse(list[list.Length - 1].ToString());
                    if ((front == end && rear != start) ||
                        (front != end && rear == start))
                    {
                        if (end == front)
                            res = start.ToString() + "/" + res;
                        else if (start == rear)
                            res = res + "/" + end.ToString();
                        flag[start] += 1;
                        flag[end] += 1;
                    }
                }
            }
            return res.Split('/').ToList();
        }

        /// <summary>
        /// 得到所有的可能结果
        /// </summary>
        /// <param name="nodeNum">节点的个数</param>
        /// <returns></returns>
        public List<string> GetAllPosiblity(int nodeNum)
        {
            string temp = string.Empty;
            string result;
            List<string> possibleResult = new List<string>();
            for (int i = (int)(Math.Pow(2, nodeNum - 1) - 1); i >= 0; i--)
            {
                result = "";
                temp = Convert.ToString(i, 2);//得到二进制字符串
                for (var j = 0; j < nodeNum - temp.Length - 1; j++)
                {
                    result += "0";
                }
                result = result + temp;

                string num = string.Empty;
                for (var k = 0; k < result.Length; k++)
                {
                    num += result[k];
                }
                if (!possibleResult.Contains(num))
                    possibleResult.Add(num);
            }
            return possibleResult;
        }

        /// <summary>
        /// 计算最优解
        /// </summary>
        /// <param name="valueCoefficient">目标函数的价值参数</param>
        /// <param name="coefficients">约束方程的约束参数</param>
        /// <returns>key=订单，value=最优值</returns>
        private Dictionary<string, double> CacluOptimalSolution(double[] valueCoefficient, double[,] coefficients)
        {
            int nodeNum = valueCoefficient.Length;
            int coefColumn = coefficients.GetUpperBound(coefficients.Rank - 1) + 1;
            int coefRow = coefficients.Length / coefColumn;
            List<string> possibleResult = GetAllPosiblity(nodeNum);
            //满足条件的所有结果集
            Dictionary<string, double> feasibilityResult = new Dictionary<string, double>();
            //遍历所有可能结果集
            for (int t = 0; t < possibleResult.Count; t++)
            {
                //是否全部满足约束条件标识符
                int flag = 0;
                //遍历所有约束条件
                for (int i = 0; i < coefRow; i++)
                {
                    double resultValue = 0;
                    double limitValue = coefficients[i, coefColumn - 1];
                    for (int j = 1; j <= coefColumn - 1; j++)
                        resultValue += coefficients[i, j] * Convert.ToInt32(possibleResult[t][j - 1].ToString());

                    //判断左边是否大于右边,如果大于则退出当前循环
                    if (resultValue > limitValue)
                    {
                        flag = 1;
                        break;
                    }
                }
                //如果满足条件则计算目标值
                if (flag == 0)
                {
                    double sum = 0;
                    for (int m = 0; m < possibleResult[t].Length; m++)
                        sum = sum + Convert.ToDouble(possibleResult[t][m].ToString()) * valueCoefficient[m];
                    feasibilityResult.Add(possibleResult[t], sum);
                }
            }
            //拿到最优解
            var optimalResult = feasibilityResult.OrderByDescending(p => p.Value).FirstOrDefault();
            var result = new Dictionary<string, double>();
            result.Add(optimalResult.Key, optimalResult.Value);
            return result;
        }

        /// <summary>
        /// 计算路径规划解决方案
        /// </summary>
        /// <param name="distArray">各点之间的距离</param>
        /// <param name="valueCoefficient">目标函数的价值参数</param>
        /// <param name="coefficients">约束方程的约束参数(weight-volumn-hour),最后一位为约束值（一律改为小于或等于约束）</param>
        /// <param name="maxValue">最优解值</param>
        /// <returns>最优解</returns>
        public List<string> GetSulotion(double[,] distArray, double[] valueCoefficient, double[,] coefficients, out double maxValue)
        {
            int nodeNum = distArray.GetUpperBound(distArray.Rank - 1) + 1;
            Saving[] svArray = CalcuSavingV(distArray, nodeNum, 0);
            QuickSort(ref svArray, 0, svArray.Length - 1);
            List<string> road = AnalyzingSPFA(svArray, distArray, nodeNum, 0);

            Dictionary<string, double> optimalSolution = CacluOptimalSolution(valueCoefficient, coefficients);
            string sul = optimalSolution.Keys.ToList()[0];
            maxValue = optimalSolution[sul];
            for (int i = 0; i < sul.Length; i++)
                if (sul[i] == '0' && road.Contains((i + 1).ToString()))
                    road.Remove((i + 1).ToString());//删除容纳不下的货

            return road;
        }

        /// <summary>
        /// 快速排序
        /// </summary>
        /// <param name="savingV">所要排序的数组</param>
        /// <param name="low">开始的下标</param>
        /// <param name="high">结束的下标</param>
        public void QuickSort(ref Saving[] savingV, int low, int high)
        {
            if (low >= high)
                return;

            int i = low, j = high;
            Saving temp = savingV[i];
            while (i < j)
            {
                while (i < j && Convert.ToDouble(savingV[j].savingValue) > Convert.ToDouble(temp.savingValue))
                    --j;
                if (i < j)
                {
                    savingV[i] = savingV[j];
                    ++i;
                }

                while (i < j && Convert.ToDouble(savingV[i].savingValue) < Convert.ToDouble(temp.savingValue))
                    ++i;
                if (i < j)
                {
                    savingV[j] = savingV[i];
                    --j;
                }
            }
            savingV[i] = temp;
            QuickSort(ref savingV, low, i - 1);
            QuickSort(ref savingV, i + 1, high);
        }

    }
}