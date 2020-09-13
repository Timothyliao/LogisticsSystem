using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticsSystem.Tools
{
    public class Code39Standard
    {
        private Dictionary<char, string> charSet = new Dictionary<char, string>();
        private Dictionary<char, int> charDistribute = new Dictionary<char, int>();
        private double[][] zoomFactor = new double[5][];


        /// <summary>
        /// Code39条码字符与二进制对照表
        /// </summary>
        public Dictionary<char, string> CharSet { get { return charSet; } }

        /// <summary>
        /// Code39条形码字符值分配表
        /// </summary>
        public Dictionary<char, int> CharDistribute { get { return charDistribute; } }

        /// <summary>
        /// Code39条码的尺寸大小
        /// 窄单元宽度、宽单元宽度、条宽比
        /// </summary>
        public double[][] ZoomFactor { get { return zoomFactor; } }


        public Code39Standard()
        {
            SetValueForcharSet();
            SetValueForcharDistribute();
            SetValueForzoomFactor();
        }


        #region 初始化Code39标准

        private void SetValueForcharSet()
        {
            charSet.Add('0', "001100100"); charSet.Add('1', "100010100"); charSet.Add('2', "010010100"); charSet.Add('3', "110000100"); charSet.Add('4', "001010100");
            charSet.Add('5', "101000100"); charSet.Add('6', "011000100"); charSet.Add('7', "000110100"); charSet.Add('8', "100100100"); charSet.Add('9', "010100100");
            charSet.Add('A', "100010010"); charSet.Add('B', "010010010"); charSet.Add('C', "110000010"); charSet.Add('D', "001010010"); charSet.Add('E', "101000010");
            charSet.Add('F', "011000010"); charSet.Add('G', "000110010"); charSet.Add('H', "100100010"); charSet.Add('I', "010100010"); charSet.Add('J', "001100010");
            charSet.Add('K', "100010001"); charSet.Add('L', "010010001"); charSet.Add('M', "110000001"); charSet.Add('N', "001010001"); charSet.Add('O', "101000001");
            charSet.Add('P', "011000001"); charSet.Add('Q', "000110001"); charSet.Add('R', "100100001"); charSet.Add('S', "010100001"); charSet.Add('T', "001100001");
            charSet.Add('U', "100011000"); charSet.Add('V', "010011000"); charSet.Add('W', "110001000"); charSet.Add('X', "001011000"); charSet.Add('Y', "101001000");
            charSet.Add('Z', "011001000"); charSet.Add('-', "000111000"); charSet.Add('.', "100101000"); charSet.Add(' ', "010101000"); charSet.Add('*', "001101000");
            charSet.Add('$', "000001110"); charSet.Add('/', "000001101"); charSet.Add('+', "000001011"); charSet.Add('%', "000000111");
        }

        private void SetValueForcharDistribute()
        {
            charDistribute.Add('0', 0); charDistribute.Add('1', 1); charDistribute.Add('2', 2); charDistribute.Add('3', 3); charDistribute.Add('4', 4);
            charDistribute.Add('5', 5); charDistribute.Add('6', 6); charDistribute.Add('7', 7); charDistribute.Add('8', 8); charDistribute.Add('9', 9);
            charDistribute.Add('A', 10); charDistribute.Add('B', 11); charDistribute.Add('C', 12); charDistribute.Add('D', 13); charDistribute.Add('E', 14);
            charDistribute.Add('F', 15); charDistribute.Add('G', 16); charDistribute.Add('H', 17); charDistribute.Add('I', 18); charDistribute.Add('J', 19);
            charDistribute.Add('K', 20); charDistribute.Add('L', 21); charDistribute.Add('M', 22); charDistribute.Add('N', 23); charDistribute.Add('O', 24);
            charDistribute.Add('P', 25); charDistribute.Add('Q', 26); charDistribute.Add('R', 27); charDistribute.Add('S', 28); charDistribute.Add('T', 29);
            charDistribute.Add('U', 30); charDistribute.Add('V', 31); charDistribute.Add('W', 32); charDistribute.Add('X', 33); charDistribute.Add('Y', 34);
            charDistribute.Add('Z', 35); charDistribute.Add('-', 36); charDistribute.Add('.', 37); charDistribute.Add(' ', 38); charDistribute.Add('$', 39);
            charDistribute.Add('/', 40); charDistribute.Add('+', 41); charDistribute.Add('%', 42);
        }

        private void SetValueForzoomFactor()
        {
            zoomFactor[0] = new double[] { 0.191, 0.429, 2.25 };
            zoomFactor[1] = new double[] { 0.292, 0.876, 3.00 };
            zoomFactor[2] = new double[] { 0.508, 1.524, 3.00 };
            zoomFactor[3] = new double[] { 1.016, 2.540, 2.50 };
            zoomFactor[4] = new double[] { 2.032, 5.080, 2.50 };
        }

        #endregion


        public string CalcuCheckCode(string code)
        {
            char[] codeArray = code.ToArray();
            int sum = 0;

            for (int i = 0, len = codeArray.Length; i < len; i++)
                sum += charDistribute[codeArray[i]];
            int remainder = sum % 43;

            foreach (var key in charDistribute.Keys)
                if (charDistribute[key] == remainder)
                {
                    code += key.ToString();
                    break;
                }

            return code;
        }

        public string[] ConstructBinarySequence(string code39)
        {
            string[] binarySeq = new string[code39.Length + 2];
            char[] code39Array = code39.ToArray();
            int flag = 0;
            binarySeq[flag++] = charSet['*'];

            for (int i = 0, len = code39Array.Length; i < len; i++)
                binarySeq[flag++] = charSet[code39Array[i]];

            binarySeq[flag++] = charSet['*'];

            return binarySeq;
        }
    }
}
