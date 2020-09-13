using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LogisticsSystem.Tools
{
    public class UCC_EAN_128Standard
    {
        private Dictionary<string, string> charSetA = new Dictionary<string, string>();//字符集A
        private Dictionary<string, string> charSetB = new Dictionary<string, string>();//字符集B
        private Dictionary<string, string> charSetC = new Dictionary<string, string>();//字符集C
        private Dictionary<string, int> charValue = new Dictionary<string, int>();//字符值
        private string[] identifier = new string[50];//标识符

        public Dictionary<string, string> CharSetA { get { return charSetA; } }
        public Dictionary<string, string> CharSetB { get { return charSetB; } }
        public Dictionary<string, string> CharSetC { get { return charSetC; } }
        public Dictionary<string, int> CharValue { get { return charValue; } }
        public string[] Identifier { get { return identifier; } }

        public UCC_EAN_128Standard()
        {
            SetValueForcharSetA();
            SetValueForcharSetB();
            SetValueForcharSetC();
            SetValueForcharValue();
            SetValueForidentifier();
        }

        #region EAN128标准

        private void SetValueForcharSetA()
        {
            charSetA.Add(" ", "212222"); charSetA.Add("!", "222122"); charSetA.Add("\"", "222221"); charSetA.Add("#", "121223"); charSetA.Add("$", "121322");
            charSetA.Add("%", "131222"); charSetA.Add("&", "122213"); charSetA.Add("……", "122312"); charSetA.Add("(", "132212"); charSetA.Add(")", "221213");
            charSetA.Add("*", "221312"); charSetA.Add("+", "231212"); charSetA.Add(",", "112232"); charSetA.Add("-", "122132"); charSetA.Add(".", "122231");
            charSetA.Add("/", "113222"); charSetA.Add("0", "123122"); charSetA.Add("1", "123221"); charSetA.Add("2", "223211"); charSetA.Add("3", "221132");
            charSetA.Add("4", "221231"); charSetA.Add("5", "213212"); charSetA.Add("6", "223112"); charSetA.Add("7", "312131"); charSetA.Add("8", "311222");
            charSetA.Add("9", "321122"); charSetA.Add(":", "321221"); charSetA.Add(";", "312212"); charSetA.Add("<", "322112"); charSetA.Add("=", "322211");
            charSetA.Add(">", "212123"); charSetA.Add("?", "212321"); charSetA.Add("@", "232121"); charSetA.Add("A", "111323"); charSetA.Add("B", "131123");
            charSetA.Add("C", "131321"); charSetA.Add("D", "112313"); charSetA.Add("E", "132113"); charSetA.Add("F", "132311"); charSetA.Add("G", "211313");
            charSetA.Add("H", "231113"); charSetA.Add("I", "231311"); charSetA.Add("J", "112133"); charSetA.Add("K", "112331"); charSetA.Add("L", "132131");
            charSetA.Add("M", "113123"); charSetA.Add("N", "113321"); charSetA.Add("O", "133121"); charSetA.Add("P", "313121"); charSetA.Add("Q", "211331");
            charSetA.Add("R", "231131"); charSetA.Add("S", "213113"); charSetA.Add("T", "213311"); charSetA.Add("U", "213131"); charSetA.Add("V", "311123");
            charSetA.Add("W", "311321"); charSetA.Add("X", "331121"); charSetA.Add("Y", "312113"); charSetA.Add("Z", "312311"); charSetA.Add("[", "332111");
            charSetA.Add("\\", "314111"); charSetA.Add("]", "221411"); charSetA.Add("^", "431111"); charSetA.Add("_", "111224"); charSetA.Add("NUL", "111422");
            charSetA.Add("SOH", "121124"); charSetA.Add("STX", "121421"); charSetA.Add("ETX", "141122"); charSetA.Add("EOT", "141221"); charSetA.Add("ENQ", "112214");
            charSetA.Add("ACK", "112412"); charSetA.Add("BEL", "122114"); charSetA.Add("BS", "122411"); charSetA.Add("HT", "142112"); charSetA.Add("LF", "142211");
            charSetA.Add("VT", "241211"); charSetA.Add("FF", "221114"); charSetA.Add("CR", "413111"); charSetA.Add("SO", "241112"); charSetA.Add("SI", "134111");
            charSetA.Add("DLE", "111242"); charSetA.Add("DC1", "121142"); charSetA.Add("DC2", "121241"); charSetA.Add("DC3", "114212"); charSetA.Add("DC4", "124112");
            charSetA.Add("NAK", "124211"); charSetA.Add("SYN", "411212"); charSetA.Add("ETB", "421112"); charSetA.Add("CAN", "421211"); charSetA.Add("EM", "212141");
            charSetA.Add("SUB", "214121"); charSetA.Add("ESC", "412121"); charSetA.Add("FS", "111143"); charSetA.Add("GS", "111341"); charSetA.Add("RS", "131141");
            charSetA.Add("US", "114113"); charSetA.Add("FNC3", "114311"); charSetA.Add("FNC2", "411113"); charSetA.Add("SHIFT", "411311"); charSetA.Add("CODE C", "113141");
            charSetA.Add("CODE B", "114131"); charSetA.Add("FNC4", "311141"); charSetA.Add("FNC1", "411131"); charSetA.Add("Start A", "211412"); charSetA.Add("Start B", "211214");
            charSetA.Add("Start C", "211232"); charSetA.Add("stop", "2331112");
        }

        private void SetValueForcharSetB()
        {
            charSetB.Add(" ", "212222"); charSetB.Add("!", "222122"); charSetB.Add("\"", "222221"); charSetB.Add("#", "121223"); charSetB.Add("$", "121322");
            charSetB.Add("%", "131222"); charSetB.Add("&", "122213"); charSetB.Add("……", "122312"); charSetB.Add("(", "132212"); charSetB.Add(")", "221213");
            charSetB.Add("*", "221312"); charSetB.Add("+", "231212"); charSetB.Add(",", "112232"); charSetB.Add("-", "122132"); charSetB.Add(".", "122231");
            charSetB.Add("/", "113222"); charSetB.Add("0", "123122"); charSetB.Add("1", "123221"); charSetB.Add("2", "223211"); charSetB.Add("3", "221132");
            charSetB.Add("4", "221231"); charSetB.Add("5", "213212"); charSetB.Add("6", "223112"); charSetB.Add("7", "312131"); charSetB.Add("8", "311222");
            charSetB.Add("9", "321122"); charSetB.Add(":", "321221"); charSetB.Add(";", "312212"); charSetB.Add("<", "322112"); charSetB.Add("=", "322211");
            charSetB.Add(">", "212123"); charSetB.Add("?", "212321"); charSetB.Add("@", "232121"); charSetB.Add("A", "111323"); charSetB.Add("B", "131123");
            charSetB.Add("C", "131321"); charSetB.Add("D", "112313"); charSetB.Add("E", "132113"); charSetB.Add("F", "132311"); charSetB.Add("G", "211313");
            charSetB.Add("H", "231113"); charSetB.Add("I", "231311"); charSetB.Add("J", "112133"); charSetB.Add("K", "112331"); charSetB.Add("L", "132131");
            charSetB.Add("M", "113123"); charSetB.Add("N", "113321"); charSetB.Add("O", "133121"); charSetB.Add("P", "313121"); charSetB.Add("Q", "211331");
            charSetB.Add("R", "231131"); charSetB.Add("S", "213113"); charSetB.Add("T", "213311"); charSetB.Add("U", "213131"); charSetB.Add("V", "311123");
            charSetB.Add("W", "311321"); charSetB.Add("X", "331121"); charSetB.Add("Y", "312113"); charSetB.Add("Z", "312311"); charSetB.Add("[", "332111");
            charSetB.Add("\\", "314111"); charSetB.Add("]", "221411"); charSetB.Add("^", "431111"); charSetB.Add("_", "111224"); charSetB.Add("`", "111422");
            charSetB.Add("a", "121124"); charSetB.Add("b", "121421"); charSetB.Add("c", "141122"); charSetB.Add("d", "141221"); charSetB.Add("e", "112214");
            charSetB.Add("f", "112412"); charSetB.Add("g", "122114"); charSetB.Add("h", "122411"); charSetB.Add("i", "142112"); charSetB.Add("j", "142211");
            charSetB.Add("k", "241211"); charSetB.Add("l", "221114"); charSetB.Add("m", "413111"); charSetB.Add("n", "241112"); charSetB.Add("o", "134111");
            charSetB.Add("p", "111242"); charSetB.Add("q", "121142"); charSetB.Add("r", "121241"); charSetB.Add("s", "114212"); charSetB.Add("t", "124112");
            charSetB.Add("u", "124211"); charSetB.Add("v", "411212"); charSetB.Add("w", "421112"); charSetB.Add("x", "421211"); charSetB.Add("y", "212141");
            charSetB.Add("z", "214121"); charSetB.Add("{", "412121"); charSetB.Add("|", "111143"); charSetB.Add("}", "111341"); charSetB.Add("~", "131141");
            charSetB.Add("DEL", "114113"); charSetB.Add("FNC3", "114311"); charSetB.Add("FNC2", "411113"); charSetB.Add("SHIFT", "411311"); charSetB.Add("CODE C", "113141");
            charSetB.Add("FNC4", "114131"); charSetB.Add("CODE A", "311141"); charSetB.Add("FNC1", "411131"); charSetB.Add("Start A", "211412"); charSetB.Add("Start B", "211214");
            charSetB.Add("Start C", "211232"); charSetB.Add("stop", "2331112");
        }

        private void SetValueForcharSetC()
        {
            charSetC.Add("00", "212222"); charSetC.Add("01", "222122"); charSetC.Add("02", "222221"); charSetC.Add("03", "121223"); charSetC.Add("04", "121322");
            charSetC.Add("05", "131222"); charSetC.Add("06", "122213"); charSetC.Add("07", "122312"); charSetC.Add("08", "132212"); charSetC.Add("09", "221213");
            charSetC.Add("10", "221312"); charSetC.Add("11", "231212"); charSetC.Add("12", "112232"); charSetC.Add("13", "122132"); charSetC.Add("14", "122231");
            charSetC.Add("15", "113222"); charSetC.Add("16", "123122"); charSetC.Add("17", "123221"); charSetC.Add("18", "223211"); charSetC.Add("19", "221132");
            charSetC.Add("20", "221231"); charSetC.Add("21", "213212"); charSetC.Add("22", "223112"); charSetC.Add("23", "312131"); charSetC.Add("24", "311222");
            charSetC.Add("25", "321122"); charSetC.Add("26", "321221"); charSetC.Add("27", "312212"); charSetC.Add("28", "322112"); charSetC.Add("29", "322211");
            charSetC.Add("30", "212123"); charSetC.Add("31", "212321"); charSetC.Add("32", "232121"); charSetC.Add("33", "111323"); charSetC.Add("34", "131123");
            charSetC.Add("35", "131321"); charSetC.Add("36", "112313"); charSetC.Add("37", "132113"); charSetC.Add("38", "132311"); charSetC.Add("39", "211313");
            charSetC.Add("40", "231113"); charSetC.Add("41", "231311"); charSetC.Add("42", "112133"); charSetC.Add("43", "112331"); charSetC.Add("44", "132131");
            charSetC.Add("45", "113123"); charSetC.Add("46", "113321"); charSetC.Add("47", "133121"); charSetC.Add("48", "313121"); charSetC.Add("49", "211331");
            charSetC.Add("50", "231131"); charSetC.Add("51", "213113"); charSetC.Add("52", "213311"); charSetC.Add("53", "213131"); charSetC.Add("54", "311123");
            charSetC.Add("55", "311321"); charSetC.Add("56", "331121"); charSetC.Add("57", "312113"); charSetC.Add("58", "312311"); charSetC.Add("59", "332111");
            charSetC.Add("60", "314111"); charSetC.Add("61", "221411"); charSetC.Add("62", "431111"); charSetC.Add("63", "111224"); charSetC.Add("64", "111422");
            charSetC.Add("65", "121124"); charSetC.Add("66", "121421"); charSetC.Add("67", "141122"); charSetC.Add("68", "141221"); charSetC.Add("69", "112214");
            charSetC.Add("70", "112412"); charSetC.Add("71", "122114"); charSetC.Add("72", "122411"); charSetC.Add("73", "142112"); charSetC.Add("74", "142211");
            charSetC.Add("75", "241211"); charSetC.Add("76", "221114"); charSetC.Add("77", "413111"); charSetC.Add("78", "241112"); charSetC.Add("79", "134111");
            charSetC.Add("80", "111242"); charSetC.Add("81", "121142"); charSetC.Add("82", "121241"); charSetC.Add("83", "114212"); charSetC.Add("84", "124112");
            charSetC.Add("85", "124211"); charSetC.Add("86", "411212"); charSetC.Add("87", "421112"); charSetC.Add("88", "421211"); charSetC.Add("89", "212141");
            charSetC.Add("90", "214121"); charSetC.Add("91", "412121"); charSetC.Add("92", "111143"); charSetC.Add("93", "111341"); charSetC.Add("94", "131141");
            charSetC.Add("95", "114113"); charSetC.Add("96", "114311"); charSetC.Add("97", "411113"); charSetC.Add("98", "411311"); charSetC.Add("99", "113141");
            charSetC.Add("CODE B", "114131"); charSetC.Add("CODE A", "311141"); charSetC.Add("FNC1", "411131"); charSetC.Add("Start A", "211412"); charSetC.Add("Start B", "211214");
            charSetC.Add("Start C", "211232"); charSetC.Add("stop", "2331112");
        }

        private void SetValueForcharValue()
        {
            int i = 0;
            foreach (var item in charSetA.Values)
                charValue.Add(item, i++);
        }

        private void SetValueForidentifier()
        {
            identifier[0] = "00"; identifier[1] = "01"; identifier[2] = "10"; identifier[3] = "13"; identifier[4] = "15";
            identifier[5] = "17"; identifier[6] = "20"; identifier[7] = "21"; identifier[8] = "22"; identifier[9] = "23";
            identifier[10] = "240"; identifier[11] = "250"; identifier[12] = "30"; identifier[13] = "310"; identifier[14] = "311";
            identifier[15] = "312"; identifier[16] = "313"; identifier[17] = "314"; identifier[18] = "315"; identifier[19] = "316";
            identifier[20] = "320"; identifier[21] = "330"; identifier[22] = "331"; identifier[23] = "332"; identifier[24] = "333";
            identifier[25] = "334"; identifier[26] = "335"; identifier[27] = "336"; identifier[28] = "340"; identifier[29] = "356";
            identifier[30] = "400"; identifier[31] = "410"; identifier[32] = "411"; identifier[33] = "412"; identifier[34] = "414";
            identifier[35] = "420"; identifier[36] = "421"; identifier[37] = "8001"; identifier[38] = "8002"; identifier[39] = "8003";
            identifier[40] = "90"; identifier[41] = "91"; identifier[42] = "92"; identifier[43] = "93"; identifier[44] = "94";
            identifier[45] = "95"; identifier[46] = "96"; identifier[47] = "97"; identifier[48] = "98"; identifier[49] = "99";
        }

        #endregion

        private string EnSureStartCode(string code)
        {
            string sample = code.Substring(0, 4);
            bool isNum = true;
            //以4个或以上的数字开始时，使用Start C
            try
            {
                int sampleNum = Convert.ToInt32(sample);
            }
            catch (Exception)
            {
                isNum = false;
            }
            if (isNum)
                return "Start C";
            else
            {
                char[] sampleArray = sample.ToArray();
                int cf = -1, wf = -1;
                for (int i = 0, len = sampleArray.Length; i < len; i++)
                {
                    int ascii = Convert.ToInt32(sample[i]);
                    if (ascii >= 0 && ascii <= 31)
                    {
                        cf = i;
                        break;
                    }
                    if (ascii >= 97 && ascii <= 122)
                    {
                        wf = i;
                        break;
                    }
                }
                //当控制字符出现在小写字母前面时，使用Start A
                if (cf > wf)
                    return "Start A";
                //其他情况使用Start B
                else
                    return "Start B";
            }
        }

        private string ABToOther(string code)
        {
            string sample = code.Substring(0, 4);
            try
            {
                int num = Convert.ToInt32(sample);
                int flag = 4;
                char[] codeArray = code.Remove(0, 4).ToArray();
                for (int i = 0, len = codeArray.Length; i < len; i++, flag++)
                {
                    int ascii = Convert.ToInt32(codeArray[i]);
                    if (ascii < 48 || ascii > 57)
                        break;
                }
                if (flag % 2 == 0)
                    return "ac1";//B转C，数字个数为偶数
                else
                    return "ac0";//B转C，数字个数为奇数
            }
            catch (Exception)
            {

            }

            char[] ca = code.ToArray();
            int ascii0 = Convert.ToInt32(ca[0]);
            int ascii1 = Convert.ToInt32(ca[1]);
            if (Math.Abs(ascii1 - ascii0) >= 66)
                return "SHIFT";//加SHIFT
            else
                return "CODE";//加CODE A或者CODE B
        }

        private string CToAOB(string code)
        {
            char[] codeArray = code.ToArray();
            int cf = -1, wf = -1;
            for (int i = 0, len = codeArray.Length; i < len; i++)
            {
                int ascii = Convert.ToInt32(codeArray[i]);
                if (ascii >= 0 && ascii <= 31)
                {
                    cf = i;
                    break;
                }
                if (ascii >= 97 && ascii <= 122)
                {
                    wf = i;
                    break;
                }
            }
            //当控制字符出现在小写字母前面时，使用CODE A
            if (cf > wf)
                return "CODE A";
            //其他情况使用CODE B
            else
                return "CODE B";
        }

        private void StartCStandard(ref List<string> code)
        {
            int flag = 0;
            for (int i = 2; i < code.Count; i++, flag++)
            {
                try
                {
                    int ascii = Convert.ToInt32(Convert.ToChar(code[i]));
                    if (ascii < 48 || ascii > 57)
                        break;
                }
                catch (Exception)
                {
                    break;
                }
            }
            if (flag % 2 != 0)
            {
                int cf = -1, wf = -1;
                for (int i = flag, len = code.Count; i < len; i++)
                {
                    if (code[i] == "CODE A" || code[i] == "CODE B" || code[i] == "CODE C" || code[i] == "SHIFT" || code[i] == "Stop")
                        continue;
                    int ascii = Convert.ToInt32(Convert.ToChar(code[i]));
                    if (ascii >= 0 && ascii <= 31)
                    {
                        cf = i;
                        break;
                    }
                    if (ascii >= 97 && ascii <= 122)
                    {
                        wf = i;
                        break;
                    }
                }
                //当控制字符出现在小写字母前面时，使用Start A
                if (cf > wf)
                    code.Insert(flag + 1, "CODE A");
                //其他情况使用Start B
                else
                    code.Insert(flag + 1, "CODE B");
            }
        }

        /// <summary>
        /// 确定条形码字符的编码
        /// </summary>
        /// <param name="ean128Code"></param>
        /// <returns></returns>
        public List<string> InsertCodingChar(string ean128Code)
        {
            List<string> strB = new List<string>();
            string currentSet = EnSureStartCode(ean128Code);
            strB.Add(currentSet);
            strB.Add("FNC1");

            char[] codeArray = ean128Code.ToArray();
            for (int i = 0, len = codeArray.Length; i < len; i++)
            {
                bool addElement = true;//在一次循环结束之前是否添加当前元素至strB
                int ascii = Convert.ToInt32(codeArray[i]);

                //当前采用字符集C的情况
                if (currentSet == "Start C" && (ascii < 48 || ascii > 57))
                {
                    string toggle = CToAOB(ean128Code.Remove(0, i));
                    if (toggle == "CODE A")
                    {
                        strB.Add("CODE A");
                        currentSet = "Start A";
                    }
                    if (toggle == "CODE B")
                    {
                        strB.Add("CODE B");
                        currentSet = "Start B";
                    }
                }
                //当前采用字符集A或B的情况
                if (currentSet == "Start A" || currentSet == "Start B")
                {
                    bool isSwitch = false;//是否需要转化
                    try
                    {
                        string smaple = ean128Code.Substring(i, 4);
                        int num = Convert.ToInt32(smaple);
                        isSwitch = true;
                    }
                    catch (Exception)
                    {
                        if (currentSet == "Start A")
                        {
                            if (ascii >= 97 && ascii <= 122)
                                isSwitch = true;
                        }
                        else
                            if (ascii >= 0 && ascii <= 31)
                            isSwitch = true;
                    }

                    //判断是否需要进行转化
                    if (isSwitch)
                    {
                        string toggle = ABToOther(ean128Code.Remove(0, i));
                        //转化的情况
                        if (toggle == "ac0")
                        {
                            strB.Add(codeArray[i].ToString()); strB.Add("CODE C");
                            addElement = false;
                            currentSet = "Start C";
                        }
                        if (toggle == "ac1")
                        {
                            strB.Add("CODE C");
                            currentSet = "Start C";
                        }
                        if (toggle == "SHIFT")
                        {
                            strB.Add("SHIFT"); strB.Add(codeArray[i].ToString()); strB.Add("SHIFT");
                            addElement = false;
                        }
                        if (toggle == "CODE")
                        {
                            if (currentSet == "Start A")
                            {
                                strB.Add("CODE B");
                                currentSet = "Start B";
                            }
                            else
                            {
                                strB.Add("CODE C");
                                currentSet = "Start C";
                            }
                        }
                    }
                }

                if (addElement)
                    strB.Add(codeArray[i].ToString());
            }

            if (strB[0] == "Start C")
                StartCStandard(ref strB);

            return strB;
        }

        /// <summary>
        /// 计算校验码
        /// </summary>
        public string CalcuCheckCode(List<string> ean128Code)
        {
            //List<MyLog> log = new List<MyLog>();
            StringBuilder strB = new StringBuilder();
            string currentSet = ean128Code[0];
            string bar = charSetA[currentSet];
            int sum = charValue[bar] + CharValue[CharSetA[ean128Code[1]]];
            strB.Append(bar);
            strB.Append(CharSetA[ean128Code[1]]);
            int weight = 2;
            //log.Add(new MyLog() { _char = ean128Code[0], _bar = charSetA[currentSet], _value = "1*" + charValue[bar] });
            //log.Add(new MyLog() { _char = ean128Code[1], _bar = CharSetA[ean128Code[1]], _value = "1*" + CharValue[CharSetA[ean128Code[1]]] });
            for (int i = 2, len = ean128Code.Count; i < len; i++, weight++)
            {
                string aim = ean128Code[i];
                if (aim == "CODE A" || aim == "CODE B" || aim == "CODE C")
                {
                    bar = currentSet == "Start A" ? charSetA[aim] : currentSet == "Start B" ? charSetB[aim] : charSetC[aim];
                    strB.Append(bar);
                    sum += weight * charValue[bar];
                    currentSet = aim.Replace("CODE", "Start");
                    //log.Add(new MyLog() { _char = aim, _bar = bar, _value = weight + "*" + charValue[bar] });
                    continue;
                }
                if (aim == "SHIFT")
                {
                    //SHIFT
                    bar = currentSet == "Start A" ? charSetA[ean128Code[i++]] : charSetB[ean128Code[i++]];
                    strB.Append(bar);
                    sum += weight++ * charValue[bar];
                    //log.Add(new MyLog() { _char = ean128Code[i - 1], _bar = bar, _value = weight + "*" + charValue[bar] });
                    //中间字符
                    bar = currentSet == "Start A" ? charSetB[ean128Code[i++]] : charSetA[ean128Code[i++]];
                    strB.Append(bar);
                    sum += weight++ * charValue[bar];
                    //log.Add(new MyLog() { _char = ean128Code[i - 1], _bar = bar, _value = weight + "*" + charValue[bar] });
                    //SHIFT
                    bar = currentSet == "Start A" ? charSetA[ean128Code[i]] : charSetB[ean128Code[i]];
                    strB.Append(bar);
                    sum += weight * charValue[bar];
                    //log.Add(new MyLog() { _char = ean128Code[i - 1], _bar = bar, _value = weight + "*" + charValue[bar] });
                    continue;
                }
                bar = currentSet == "Start A" ? charSetA[aim] : currentSet == "Start B" ? charSetB[aim] : charSetC[aim + ean128Code[++i]];
                strB.Append(bar);
                sum += weight * charValue[bar];
                //log.Add(new MyLog() { _char = aim + ean128Code[i], _bar = bar, _value = weight + "*" + charValue[bar] });
            }

            int remainder = sum % 103;
            //var reBar = from value in charValue.Keys
            //         where charValue[value] == remainder
            //         select value;
            var reBar = charValue.Keys.Where(p => charValue[p] == remainder);
            strB.Append(reBar.First());
            //foreach (var item in reBar)
            //{
            //    strB.Append(item);
            //    break;
            //}
            strB.Append("2331112");
            return strB.ToString();
        }

        /// <summary>
        /// 识别标识符
        /// </summary>
        /// <param name="code"></param>
        public string DistinguishIdentifier(string code)
        {
            string res = string.Empty;
            string threeSample = code.Substring(0, 3);
            if (identifier.Where(p => p == threeSample).Any())
                res = code.Insert(3, ")");
            else
            {
                string twoSample = code.Substring(0, 2);
                if (identifier.Where(p => p == twoSample).Any())
                    res = code.Insert(2, ")");
                else
                    res = code.Insert(4, ")");
            }
            res = "(" + res;
            return res;
        }
    }
}
