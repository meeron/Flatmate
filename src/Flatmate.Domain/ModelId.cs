using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Flatmate.Domain
{
    public struct ModelId : IComparable<ModelId>, IEquatable<ModelId>, IConvertible
    {
        public const int SIZE = 6;
        private const int BASE64_LEN = 4 * SIZE / 3;

        private uint _value;
        private byte _control1;
        private byte _control2;

        private ModelId(ModelId id)
        {
            _value = id._value;
            _control1 = id._control1;
            _control2 = id._control2;
        }

        public ModelId(byte[] values)
        {
            byte[] intBytes = new byte[4];

            Array.Copy(values, 0, intBytes, 0, intBytes.Length);
            _value = (uint)BitConverter.ToInt32(intBytes, 0);
            _control1 = values[4];
            _control2 = values[5];
        }

        public static ModelId NewId()
        {
            using (var rng = RandomNumberGenerator.Create())
            {
                byte[] values = new byte[SIZE];

                rng.GetBytes(values);

                return new ModelId(values);
            }
        }

        public static ModelId Parse(string s)
        {
            if (string.IsNullOrWhiteSpace(s))
                throw new ArgumentNullException("s");

            ModelId idOut = ModelId.Empty;
            if (!TryParse(s, out idOut))
                throw new FormatException(string.Format("Text '{0}' is invalid for ModelId type.", s));

            return idOut;
        }

        public static bool TryParse(string s, out ModelId id)
        {
            id = ModelId.Empty;

            if (string.IsNullOrWhiteSpace(s))
                return false;

            if (s.Length != BASE64_LEN)
                return false;

            s = s.Replace('-', '/');
            id = new ModelId(Convert.FromBase64String(s));
            return true;
        }

        public int CompareTo(ModelId other)
        {
            int result = _value.CompareTo(other._value);
            if (result != 0) { return result; }
            result = _control1.CompareTo(other._control1);
            if (result != 0) { return result; }
            return _control2.CompareTo(other._control2);
        }

        public bool Equals(ModelId other)
        {
            return _value.Equals(other._value) &&
                    _control1.Equals(other._control1) &&
                    _control2.Equals(other._control2);
        }

        public override string ToString()
        {
            return Convert.ToBase64String(ToByteArray()).Replace('/','-');
        }

        public byte[] ToByteArray()
        {
            byte[] value = BitConverter.GetBytes(_value);
            Array.Resize(ref value, SIZE);
            value[4] = _control1;
            value[5] = _control2;

            return value;
        }

        public override bool Equals(object obj)
        {
            if (obj is ModelId)
            {
                return Equals((ModelId)obj);
            }
            return false;
        }

        public override int GetHashCode()
        {
            int hash = 31;
            hash = (hash * 85) + _value.GetHashCode();
            hash = (hash * 85) + _control1.GetHashCode();
            hash = (hash * 85) + _control2.GetHashCode();

            return hash;
        }

        public static bool operator ==(ModelId lhs, ModelId rhs)
        {
            return lhs.Equals(rhs);
        }

        public static bool operator !=(ModelId lhs, ModelId rhs)
        {
            return !(lhs == rhs);
        }

        public static bool operator <(ModelId lhs, ModelId rhs)
        {
            return lhs.CompareTo(rhs) < 0;
        }

        public static bool operator <=(ModelId lhs, ModelId rhs)
        {
            return lhs.CompareTo(rhs) <= 0;
        }

        public static bool operator >=(ModelId lhs, ModelId rhs)
        {
            return lhs.CompareTo(rhs) >= 0;
        }

        public static bool operator >(ModelId lhs, ModelId rhs)
        {
            return lhs.CompareTo(rhs) > 0;
        }

        public TypeCode GetTypeCode()
        {
            return TypeCode.Object;
        }

        public bool ToBoolean(IFormatProvider provider)
        {
            throw new InvalidCastException();
        }

        public byte ToByte(IFormatProvider provider)
        {
            throw new InvalidCastException();
        }

        public char ToChar(IFormatProvider provider)
        {
            throw new InvalidCastException();
        }

        public DateTime ToDateTime(IFormatProvider provider)
        {
            throw new InvalidCastException();
        }

        public decimal ToDecimal(IFormatProvider provider)
        {
            throw new InvalidCastException();
        }

        public double ToDouble(IFormatProvider provider)
        {
            throw new InvalidCastException();
        }

        public short ToInt16(IFormatProvider provider)
        {
            throw new InvalidCastException();
        }

        public int ToInt32(IFormatProvider provider)
        {
            throw new InvalidCastException();
        }

        public long ToInt64(IFormatProvider provider)
        {
            throw new InvalidCastException();
        }

        public sbyte ToSByte(IFormatProvider provider)
        {
            throw new InvalidCastException();
        }

        public float ToSingle(IFormatProvider provider)
        {
            throw new InvalidCastException();
        }

        public string ToString(IFormatProvider provider)
        {
            return ToString();
        }

        public object ToType(Type conversionType, IFormatProvider provider)
        {
            switch (Type.GetTypeCode(conversionType))
            {
                case TypeCode.String:
                    return ((IConvertible)this).ToString(provider);
                case TypeCode.Object:
                    if (conversionType == typeof(object) || conversionType == typeof(ModelId))
                    {
                        return this;
                    }
                    if (conversionType == typeof(ModelId))
                    {
                        return new ModelId(this);
                    }
                    break;
            }

            throw new InvalidCastException();
        }

        public ushort ToUInt16(IFormatProvider provider)
        {
            throw new InvalidCastException();
        }

        public uint ToUInt32(IFormatProvider provider)
        {
            throw new InvalidCastException();
        }

        public ulong ToUInt64(IFormatProvider provider)
        {
            throw new InvalidCastException();
        }

        public static ModelId Empty = new ModelId { _value = 0, _control1 = 0, _control2 = 0 };
    }
}
