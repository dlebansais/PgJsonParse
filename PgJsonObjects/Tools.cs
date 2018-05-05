using Presentation;
using System;

namespace PgJsonObjects
{
    public class Tools
    {
        public static bool TryParseFloat(string s, out float Value, out FloatFormat Format)
        {
            if (InvariantCulture.TryParseSingle(s, out Value))
            {
                if (s == InvariantCulture.SingleToString(Value))
                    Format = FloatFormat.Standard;

                else if (s == InvariantCulture.SingleToString(Value, "0.0#"))
                    Format = FloatFormat.WithEndingZero;

                else
                    Format = FloatFormat.Other;

                return true;
            }

            Format = FloatFormat.Other;
            return false;
        }

        public static string FloatToString(float Value, FloatFormat Format)
        {
            switch (Format)
            {
                default:
                case FloatFormat.Other:
                case FloatFormat.Standard:
                    return InvariantCulture.SingleToString(Value);

                case FloatFormat.WithEndingZero:
                    return InvariantCulture.SingleToString(Value, "0.0#");
            }
        }

        public static bool Scan(string s, string Format, params object[] args)
        {
            try
            {
                int argc = 0;
                int i = 0;
                int j = 0;

                while (i < s.Length && j < Format.Length)
                {
                    if (Format[j] == '%' && j + 1 < Format.Length && Format[j + 1] == '%')
                    {
                        if (s[i++] != Format[j++])
                            return false;
                        j++;
                    }

                    else if (Format[j] == '%' && j + 1 < Format.Length && Format[j + 1] == 's')
                    {
                        if (j + 3 >= Format.Length)
                            return false;
                        if (Format[j + 2] != '{')
                            return false;
                        int EndTerm = Format.IndexOf('}', j + 3);
                        if (EndTerm < 0)
                            return false;
                        string Term = Format.Substring(j + 3, EndTerm - j - 3);

                        int TermIndex = s.IndexOf(Term, i);
                        if (TermIndex < 0)
                            return false;

                        string Value = s.Substring(i, TermIndex - i - 1);
                        if (argc >= args.Length)
                            return false;
                        args[argc++] = Value;

                        i += Value.Length;
                        j = EndTerm + 1;
                    }

                    else if (Format[j] == '%' && j + 1 < Format.Length && Format[j + 1] == 'd')
                    {
                        if (argc >= args.Length)
                            return false;

                        int Value = 0;
                        int k = 0;
                        int Sign = 0;
                        while (i + k < s.Length && (Char.IsDigit(s[i + k]) || (k == 0 && (s[i + k] == '+' || s[i + k] == '-'))))
                        {
                            if (s[i + k] == '+')
                                Sign = 1;
                            else if (s[i + k] == '-')
                                Sign = -1;
                            else
                                Value = Value * 10 + s[i + k] - '0';
                            k++;
                        }

                        if (k == 0 || (k == 1 && Sign != 0))
                            return false;

                        if (k > 0 && i + k < s.Length && s[i + k] == '.')
                            if (argc >= args.Length)
                                return false;

                        if (Sign != 0)
                            Value *= Sign;

                        args[argc++] = Value;

                        i += k;
                        j += 2;
                    }

                    else if (Format[j] == '%' && j + 1 < Format.Length && Format[j + 1] == 'f')
                    {
                        if (argc >= args.Length)
                            return false;

                        int Value = 0;
                        bool IsDecimal = false;
                        int Decimal = 0;
                        int DecimalMax = 1;
                        int k = 0;
                        int Sign = 0;
                        while (i + k < s.Length && (Char.IsDigit(s[i + k]) || (s[i + k] == '.' && k > 0) || (k == 0 && (s[i + k] == '+' || s[i + k] == '-'))))
                        {
                            if (s[i + k] == '+')
                                Sign = 1;
                            else if (s[i + k] == '-')
                                Sign = -1;
                            else if (s[i + k] == '.')
                                IsDecimal = true;
                            else if (IsDecimal)
                            {
                                DecimalMax *= 10;
                                Decimal = Decimal * 10 + s[i + k] - '0';
                            }
                            else
                                Value = Value * 10 + s[i + k] - '0';
                            k++;
                        }

                        if (k == 0 || (k == 1 && Sign != 0))
                            return false;

                        if (Sign != 0)
                            Value *= Sign;

                        double FloatValue = Value + (double)Decimal / (double)DecimalMax;
                        args[argc++] = FloatValue;

                        i += k;
                        j += 2;
                    }

                    else if (Format[j] == '%' && j + 1 < Format.Length && Format[j + 1] == '{')
                    {
                        if (argc >= args.Length)
                            return false;

                        int k = Format.IndexOf('}', j);
                        string EnumTypeName = Format.Substring(j + 2, k - j - 2);

                        string AssemblyName = AssemblyTools.GetCurrentAssemblyName();
                        Type t = Type.GetType(AssemblyName + "." + EnumTypeName);

                        string[] Names = Enum.GetNames(t);

                        for (k = 0; k < Names.Length; k++)
                            if (s.Length >= i + Names[k].Length && String.Compare(s.Substring(i, Names[k].Length), Names[k], true) == 0)
                            {
                                Array Values = Enum.GetValues(t);
                                args[argc++] = Values.GetValue(k);
                                break;
                            }

                        if (k >= Names.Length)
                            return false;

                        i += Names[k].Length;
                        j += 3 + EnumTypeName.Length;
                    }

                    else if (s[i++] != Format[j++])
                        return false;
                }

                if (i < s.Length || j < Format.Length)
                    return false;

                return true;
            }
            catch
            {
                return false;
            }
        }

        public static string TimeSpanToString(TimeSpan Duration)
        {
            string Result = "";

            if (Duration.TotalDays >= 1)
            {
                int TotalDays = (int)Duration.TotalDays;

                if (Result.Length > 0)
                    Result += " ";

                if (TotalDays > 1)
                    Result += TotalDays.ToString() + " days";
                else
                    Result += TotalDays.ToString() + " day";

                Duration -= TimeSpan.FromDays(TotalDays);
            }

            if (Duration.TotalHours >= 1)
            {
                int TotalHours = (int)Duration.TotalHours;

                if (Result.Length > 0)
                    Result += " ";

                if (TotalHours > 1)
                    Result += TotalHours.ToString() + " hours";
                else
                    Result += TotalHours.ToString() + " hour";

                Duration -= TimeSpan.FromHours(TotalHours);
            }

            if (Duration.TotalMinutes >= 1)
            {
                int TotalMinutes = (int)Duration.TotalMinutes;

                if (Result.Length > 0)
                    Result += " ";

                if (TotalMinutes > 1)
                    Result += TotalMinutes.ToString() + " hours";
                else
                    Result += TotalMinutes.ToString() + " hour";

                Duration -= TimeSpan.FromMinutes(TotalMinutes);
            }

            return Result;
        }

        public static string NewLine { get; } = "\r\n";
    }
}
