using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace RationalNumberProject
{
    public class Rational : IComparable
    {
        public long Numerator { get; private set; }

        public long Denominator
        {
            get
            {
                return _denominator;
            }
            private set
            {
                if (value > 0) _denominator = value;
                else
                {
                    throw new Exception("Denominator must be positive");
                }
            }
        }
        private long _denominator;

        public Rational(long numerator, long denominator)
        {
            if (denominator < 0)
            {
                Numerator = -numerator;
                Denominator = -denominator;
            }
            else
            {
                Numerator = numerator;
                Denominator = denominator;
            }
            Reduce();
        }

        public Rational(long numerator)
        {
            Numerator = numerator;
            Denominator = 1;
        }

        private void Reduce()
        {
            long r = GCD(Math.Abs(Numerator), Denominator);
            Numerator /= r;
            Denominator /= r;
        }

        public static long GCD(long a, long b)
        {
            return b > 0 ? GCD(b, a % b) : a;
        }

        public static Rational operator -(Rational a)
        {
            return new Rational(-a.Numerator, a.Denominator);
        }

        public static Rational operator +(Rational a, Rational b)
        {
            return new Rational(a.Numerator * b.Denominator +
                                b.Numerator * a.Denominator,
                                a.Denominator * b.Denominator);
        }

        public static Rational operator -(Rational a, Rational b)
        {
            return a + (-b);
        }

        public static Rational operator ++(Rational a)
        {
            a += 1;
            return a;
        }

        public static Rational operator --(Rational a)
        {
            a -= 1;
            return a;
        }

        public static Rational Abs(Rational a)
        {
            return new Rational(Math.Abs(a.Numerator), a.Denominator);
        }

        public static Rational Pow(Rational a, int n)
        {
            Rational res = new Rational(1);
            for (int i = 0; i < n; i++)
            {
                res *= a;
            }
            return res;
        }

        public static Rational operator *(Rational a, Rational b)
        {
            return new Rational(a.Numerator * b.Numerator,
                                a.Denominator * b.Denominator);
        }

        public static Rational operator /(Rational a, Rational b)
        {
            if (b.Numerator == 0) throw new Exception("Division by zero");
            return new Rational(a.Numerator * b.Denominator,
                                a.Denominator * b.Numerator);
        }

        public bool Equals(Rational other)
        {
            return this == other;
        }

        public override bool Equals(object obj)
        {
            return obj is Rational && Equals((Rational)obj);
        }

        override public int GetHashCode()
        {
            return HashCode.Combine(Numerator, Denominator);
        }

        public int CompareTo(Rational other)
        {
            if (this == other) return 0;
            else if (this < other) return -1;
            else return 1;
        }

        public int CompareTo(object obj)
        {
            if (obj == null) return 1;
            if (obj is Rational) return CompareTo(obj as Rational);
            else throw new ArgumentException("Must be Rational");
        }

        public static bool operator ==(Rational a, Rational b)
        {
            return a.Numerator == b.Numerator && a.Denominator == b.Denominator;
        }

        public static bool operator !=(Rational a, Rational b)
        {
            return !(a == b);
        }

        public static bool operator >(Rational a, Rational b)
        {
            return a.Numerator * b.Denominator > a.Denominator * b.Numerator;
        }

        public static bool operator <(Rational a, Rational b)
        {
            return a.Numerator * b.Denominator < a.Denominator * b.Numerator;
        }

        public static bool operator >=(Rational a, Rational b)
        {
            return !(a < b);
        }

        public static bool operator <=(Rational a, Rational b)
        {
            return !(a > b);
        }

        static public Rational Parse(string s)
        {
            if (!TryParse(s, out Rational result)) throw new FormatException("Error parse");
            return result;
        }

        static public Rational Parse(string format, string s)
        {
            if (!TryParse(s, format, out Rational result)) throw new FormatException("Error parse");
            return result;
        }

        static public bool TryParse(string s, out Rational result)
        {
            return TryParse(s, "I", out result) || TryParse(s, "D", out result);
        }

        static public bool TryParse(string s, string format, out Rational result)
        {
            s = s.Trim();
            result = default;
            switch (format)
            {
                case "s":
                    {
                        string pattern = @"^(-?\d+)\s*[/]\s*(\d+)\z";
                        if (!Regex.IsMatch(s, pattern)) return false;
                        Match m = Regex.Match(s, pattern);
                        if (!long.TryParse(m.Groups[1].Value, out long value1)) return false;
                        if (!long.TryParse(m.Groups[2].Value, out long value2)) return false;
                        if (value2 == 0) return false;
                        result = new Rational(value1, value2);
                        return true;
                    }
                case "S":
                    {
                        if (!long.TryParse(s, out long value1)) return TryParse(s, "s", out result);
                        result = new Rational(value1);
                        return true;
                    }
                case "i":
                    {
                        string pattern = @"^(-?)(\d+)\s+(\d+)\s*[/]\s*(\d+)\z";
                        if (!Regex.IsMatch(s, pattern)) return false;
                        Match m = Regex.Match(s, pattern);
                        bool isPositive = m.Groups[1].Value != "-";
                        if (!long.TryParse(m.Groups[2].Value, out long value1)) return false;
                        if (!long.TryParse(m.Groups[3].Value, out long value2)) return false;
                        if (!long.TryParse(m.Groups[4].Value, out long value3)) return false;
                        result = value1 + new Rational(value2, value3);
                        if (!isPositive) result = -result;
                        return true;
                    }
                case "I":
                    {
                        return TryParse(s, "S", out result) || TryParse(s, "i", out result);
                    }
                case "d":
                    {
                        if (!decimal.TryParse(s, out decimal value)) return false;
                        result = value;
                        return true;
                    }
                case "D":
                    {
                        if (Rational.TryParse(s, "d", out result)) return true;
                        string pattern = @"^(-?)(\d+)[.](\d*)[(](\d+)[)]\z";
                        if (!Regex.IsMatch(s, pattern)) return false;
                        Match m = Regex.Match(s, pattern);
                        bool isPositive = m.Groups[1].Value != "-";
                        if (!long.TryParse(m.Groups[2].Value, out long intPart)) return false;
                        string sa = m.Groups[3].Value + m.Groups[4].Value;
                        string sb = m.Groups[3].Value;
                        long a, b;
                        if (!long.TryParse(sa, out a)) return false;
                        if (sb == "") b = 0;
                        else if (!long.TryParse(sb, out b)) return false;
                        int n = sa.Length;
                        int k = sa.Length - sb.Length;
                        if (n > 18) return false;
                        result = intPart + new Rational(a - b,
                            long.Parse(new string('9', k) + new string('0', n - k)));
                        if (!isPositive) result = -result;
                        return true;
                    }
                default:
                    throw new FormatException(string.Concat("Unknown format"));
            }
        }

        override public string ToString()
        {
            return ToString("S");
        }

        public string ToString(string format)
        {
            switch (format)
            {
                case "s": 
                    return string.Concat(Numerator, "/", Denominator);
                case "S":
                    if (Denominator == 1) return Numerator.ToString();
                    return ToString("s");
                case "i":
                    string res = string.Concat(Math.Abs(Numerator / Denominator), " ",
                        Math.Abs(Numerator % Denominator), "/", Denominator);
                    if (Numerator < 0) res = "-" + res;
                    return res;
                case "I":
                    if (Numerator / Denominator == 0 || Numerator % Denominator == 0) return ToString("S");
                    else return string.Concat(Numerator / Denominator, " ",
                        Math.Abs(Numerator % Denominator), "/", Denominator);
                case "d":
                    return ((decimal)Numerator / Denominator).ToString();
                case "D":
                    return FractionToString(Numerator, Denominator);
                default:
                    throw new FormatException("Unknown format");
            }
        }

        private static string FractionToString(long numerator, long denominator)
        {
            Dictionary<long, int> d = new Dictionary<long, int>();
            string res = "";
            if (numerator < 0)
            {
                res = "-";
                numerator = -numerator;
            }
            for (int i = 0; i < 100; i++)
            {
                res += numerator / denominator;
                long r = numerator % denominator;
                if (r == 0) return res;
                if (d.TryGetValue(r, out int ind)) return res.Insert(ind, "(") + ')';
                if (i == 0) res += ".";
                d.Add(r, res.Length);
                numerator = r * 10;
            }
            return res + "...";
        }

        public static implicit operator Rational(long value)
        {
            return new Rational(value);
        }

        public static implicit operator Rational(decimal value)
        {
            return new Rational((long)(value * (int)1e9), (int)1e9);
        }

        public static explicit operator decimal(Rational value)
        {
            return (decimal)value.Numerator / value.Denominator;
        }

        public static explicit operator long(Rational value)
        {
            return value.Numerator / value.Denominator;
        }
    }
}
