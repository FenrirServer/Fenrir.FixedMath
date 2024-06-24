using System;
using System.ComponentModel;
using System.Globalization;

namespace FixedMath
{
    internal static class FixedVectorConversion
    {
        public static bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            if (destinationType == typeof(float))
                return true;
            if (destinationType == typeof(FixedVector2))
                return true;
            if (destinationType == typeof(FixedVector3))
                return true;
            if (destinationType == typeof(FixedVector4))
                return true;
            if (destinationType.GetInterface("IPackedVector") != null)
                return true;

            return false;
        }

        public static object ConvertToFromVector4(ITypeDescriptorContext context, CultureInfo culture, FixedVector4 value, Type destinationType)
        {
            if (destinationType == typeof(float))
                return value.X;
            if (destinationType == typeof(FixedVector2))
                return new FixedVector2(value.X, value.Y);
            if (destinationType == typeof(FixedVector3))
                return new FixedVector3(value.X, value.Y, value.Z);
            if (destinationType == typeof(FixedVector4))
                return new FixedVector4(value.X, value.Y, value.Z, value.W);

            return null;
        }         
    }
}
