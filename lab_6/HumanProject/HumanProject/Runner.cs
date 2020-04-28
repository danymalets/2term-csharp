using System;

namespace HumanProject
{
    public class Runner : Sportsman, IComparable<Runner>, IConvertible
    {
        // km/h
        public double Speed 
        {
            get
            {
                return _speed;
            }
            private set
            {
                if (value > 40) _speed = 40;
                else if (value < 0) _speed = 0;
                else _speed = value;
            }
        }
        double _speed;

        public Runner() : base()
        {
            Speed = 20;
        }

        public Runner(string fullName, string identifier) : base(fullName, identifier)
        {
            Speed = 20;
        }

        public Runner(string fullName, string identifier, double speed) : base(fullName, identifier)
        {
            Speed = speed;
        }

        public Runner(string fullName, string identifier,
            double speed, double weight, double height)
            : base(fullName, identifier, weight, height)
        {
            Speed = speed;
        }

        public override string ToString()
        {
            return string.Format("{0} id={1} speed={2}", FullName, Identifier, Speed);
        }

        public int Run(int metres)
        {
            return (int)((double)3600 * metres / Speed);
        }

        public override void Train(int minutes)
        {
            Random random = new Random();
            Speed += 0.001 * minutes * random.Next(1, 11);
            base.Train(minutes);
        }

        public int CompareTo(Runner other)
        {
            return Speed.CompareTo(other.Speed);
        }

        public TypeCode GetTypeCode()
        {
            return Speed.GetTypeCode();
        }

        public bool ToBoolean(IFormatProvider provider)
        {
            return Convert.ToBoolean(Speed);
        }

        public byte ToByte(IFormatProvider provider)
        {
            return Convert.ToByte(Speed);
        }

        public char ToChar(IFormatProvider provider)
        {
            return Convert.ToChar(Speed);
        }

        public DateTime ToDateTime(IFormatProvider provider)
        {
            return Convert.ToDateTime(Speed);
        }

        public decimal ToDecimal(IFormatProvider provider)
        {
            return Convert.ToDecimal(Speed);
        }

        public double ToDouble(IFormatProvider provider)
        {
            return Speed;
        }

        public short ToInt16(IFormatProvider provider)
        {
            return Convert.ToInt16(Speed);
        }

        public int ToInt32(IFormatProvider provider)
        {
            return Convert.ToInt32(Speed);
        }

        public long ToInt64(IFormatProvider provider)
        {
            return Convert.ToInt64(Speed);
        }

        public sbyte ToSByte(IFormatProvider provider)
        {
            return Convert.ToSByte(Speed);
        }

        public float ToSingle(IFormatProvider provider)
        {
            return Convert.ToSingle(Speed);
        }

        public string ToString(IFormatProvider provider)
        {
            return Convert.ToString(Speed);
        }

        public object ToType(Type conversionType, IFormatProvider provider)
        {
            return Convert.ChangeType(Speed, conversionType);
        }

        public ushort ToUInt16(IFormatProvider provider)
        {
            return Convert.ToUInt16(Speed);
        }

        public uint ToUInt32(IFormatProvider provider)
        {
            return Convert.ToUInt32(Speed);
        }

        public ulong ToUInt64(IFormatProvider provider)
        {
            return Convert.ToUInt64(Speed);
        }
    }
}