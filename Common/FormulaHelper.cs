namespace System.Utilities.Common
{
    using System.Utilities.Enums;
    using System;

    public class FormulaHelper
    {
        private static double CalculateExExpress(string formulaString, Formula ExpressType)
        {
            double num = 0.0;
            switch (ExpressType)
            {
                case Formula.Sin:
                    num = Math.Sin(Convert.ToDouble(formulaString));
                    break;

                case Formula.Cos:
                    num = Math.Cos(Convert.ToDouble(formulaString));
                    break;

                case Formula.Tan:
                    num = Math.Tan(Convert.ToDouble(formulaString));
                    break;

                case Formula.ATan:
                    num = Math.Atan(Convert.ToDouble(formulaString));
                    break;

                case Formula.Sqrt:
                    num = Math.Sqrt(Convert.ToDouble(formulaString));
                    break;

                case Formula.Pow:
                    num = Math.Pow(Convert.ToDouble(formulaString), 2.0);
                    break;
            }
            if (num == 0.0)
            {
                return Convert.ToDouble(formulaString);
            }
            return num;
        }

        public static double CalculateExpress(string formulaString)
        {
            string str = "";
            string str2 = "";
            string str3 = "";
            string str4 = "";
            double num = 0.0;
            while (((formulaString.IndexOf("+") != -1) || (formulaString.IndexOf("-") != -1)) || ((formulaString.IndexOf("*") != -1) || (formulaString.IndexOf("/") != -1)))
            {
                if (formulaString.IndexOf("*") != -1)
                {
                    str = formulaString.Substring(formulaString.IndexOf("*") + 1, (formulaString.Length - formulaString.IndexOf("*")) - 1);
                    str2 = formulaString.Substring(0, formulaString.IndexOf("*"));
                    str3 = str2.Substring(GetPrivorPos(str2) + 1, (str2.Length - GetPrivorPos(str2)) - 1);
                    str4 = str.Substring(0, GetNextPos(str));
                    num = Convert.ToDouble(GetExpType(str3)) * Convert.ToDouble(GetExpType(str4));
                    formulaString = formulaString.Replace(str3 + "*" + str4, num.ToString());
                }
                else
                {
                    if (formulaString.IndexOf("/") != -1)
                    {
                        str = formulaString.Substring(formulaString.IndexOf("/") + 1, (formulaString.Length - formulaString.IndexOf("/")) - 1);
                        str2 = formulaString.Substring(0, formulaString.IndexOf("/"));
                        str3 = str2.Substring(GetPrivorPos(str2) + 1, (str2.Length - GetPrivorPos(str2)) - 1);
                        str4 = str.Substring(0, GetNextPos(str));
                        num = Convert.ToDouble(GetExpType(str3)) / Convert.ToDouble(GetExpType(str4));
                        formulaString = formulaString.Replace(str3 + "/" + str4, num.ToString());
                        continue;
                    }
                    if (formulaString.IndexOf("+") != -1)
                    {
                        str = formulaString.Substring(formulaString.IndexOf("+") + 1, (formulaString.Length - formulaString.IndexOf("+")) - 1);
                        str2 = formulaString.Substring(0, formulaString.IndexOf("+"));
                        str3 = str2.Substring(GetPrivorPos(str2) + 1, (str2.Length - GetPrivorPos(str2)) - 1);
                        str4 = str.Substring(0, GetNextPos(str));
                        num = Convert.ToDouble(GetExpType(str3)) + Convert.ToDouble(GetExpType(str4));
                        formulaString = formulaString.Replace(str3 + "+" + str4, num.ToString());
                        continue;
                    }
                    if (formulaString.IndexOf("-") != -1)
                    {
                        str = formulaString.Substring(formulaString.IndexOf("-") + 1, (formulaString.Length - formulaString.IndexOf("-")) - 1);
                        str2 = formulaString.Substring(0, formulaString.IndexOf("-"));
                        str3 = str2.Substring(GetPrivorPos(str2) + 1, (str2.Length - GetPrivorPos(str2)) - 1);
                        str4 = str.Substring(0, GetNextPos(str));
                        formulaString = formulaString.Replace(str3 + "-" + str4, (Convert.ToDouble(GetExpType(str3)) - Convert.ToDouble(GetExpType(str4))).ToString());
                    }
                }
            }
            return Convert.ToDouble(formulaString);
        }

        private static string GetExpType(string formulaString)
        {
            formulaString = formulaString.ToUpper();
            if (formulaString.IndexOf("SIN") != -1)
            {
                return CalculateExExpress(formulaString.Substring(formulaString.IndexOf("N") + 1, (formulaString.Length - 1) - formulaString.IndexOf("N")), Formula.Sin).ToString();
            }
            if (formulaString.IndexOf("COS") != -1)
            {
                return CalculateExExpress(formulaString.Substring(formulaString.IndexOf("S") + 1, (formulaString.Length - 1) - formulaString.IndexOf("S")), Formula.Cos).ToString();
            }
            if (formulaString.IndexOf("TAN") != -1)
            {
                return CalculateExExpress(formulaString.Substring(formulaString.IndexOf("N") + 1, (formulaString.Length - 1) - formulaString.IndexOf("N")), Formula.Tan).ToString();
            }
            if (formulaString.IndexOf("ATAN") != -1)
            {
                return CalculateExExpress(formulaString.Substring(formulaString.IndexOf("N") + 1, (formulaString.Length - 1) - formulaString.IndexOf("N")), Formula.ATan).ToString();
            }
            if (formulaString.IndexOf("SQRT") != -1)
            {
                return CalculateExExpress(formulaString.Substring(formulaString.IndexOf("T") + 1, (formulaString.Length - 1) - formulaString.IndexOf("T")), Formula.Sqrt).ToString();
            }
            if (formulaString.IndexOf("POW") != -1)
            {
                return CalculateExExpress(formulaString.Substring(formulaString.IndexOf("W") + 1, (formulaString.Length - 1) - formulaString.IndexOf("W")), Formula.Pow).ToString();
            }
            return formulaString;
        }

        private static int GetNextPos(string formulaString)
        {
            int[] numArray = new int[] { formulaString.IndexOf("+"), formulaString.IndexOf("-"), formulaString.IndexOf("*"), formulaString.IndexOf("/") };
            int length = formulaString.Length;
            for (int i = 1; i <= numArray.Length; i++)
            {
                if ((length > numArray[i - 1]) && (numArray[i - 1] != -1))
                {
                    length = numArray[i - 1];
                }
            }
            return length;
        }

        private static int GetPrivorPos(string formulaString)
        {
            int[] numArray = new int[] { formulaString.LastIndexOf("+"), formulaString.LastIndexOf("-"), formulaString.LastIndexOf("*"), formulaString.LastIndexOf("/") };
            int num = -1;
            for (int i = 1; i <= numArray.Length; i++)
            {
                if ((num < numArray[i - 1]) && (numArray[i - 1] != -1))
                {
                    num = numArray[i - 1];
                }
            }
            return num;
        }

        public static string SpiltExpression(string formulaString)
        {
            string str = "";
            string str2 = "";
            while (formulaString.IndexOf("(") != -1)
            {
                str = formulaString.Substring(formulaString.LastIndexOf("(") + 1, (formulaString.Length - formulaString.LastIndexOf("(")) - 1);
                str2 = str.Substring(0, str.IndexOf(")"));
                formulaString = formulaString.Replace("(" + str2 + ")", CalculateExpress(str2).ToString());
            }
            if (((formulaString.IndexOf("+") != -1) || (formulaString.IndexOf("-") != -1)) || ((formulaString.IndexOf("*") != -1) || (formulaString.IndexOf("/") != -1)))
            {
                formulaString = CalculateExpress(formulaString).ToString();
            }
            return formulaString;
        }
    }
}

