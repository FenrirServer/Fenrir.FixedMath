using System;
using System.ComponentModel;
using System.Globalization;

namespace FixedMath
{
    public class FixedVector3TypeConverter : TypeConverter
    {
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            if (FixedVectorConversion.CanConvertTo(context, destinationType))
                return true;
            if (destinationType == typeof(string))
                return true;

            return base.CanConvertTo(context, destinationType);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            var vec = (FixedVector3)value;

            if (FixedVectorConversion.CanConvertTo(context, destinationType))
            {
                var vec4 = new FixedVector4(vec.X, vec.Y, vec.Z, Fixed.Zero);
                return FixedVectorConversion.ConvertToFromVector4(context, culture, vec4, destinationType);
            }

            if (destinationType == typeof(string))
            {                
                var terms = new string[3];
                terms[0] = vec.X.ToString("R", culture);
                terms[1] = vec.Y.ToString("R", culture);
                terms[2] = vec.Z.ToString("R", culture);

                return string.Join(culture.TextInfo.ListSeparator + " ", terms);
            }

            return base.ConvertTo(context, culture, value, destinationType);
        }

        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == typeof(string))
                return true;

            return base.CanConvertFrom(context, sourceType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            var sourceType = value.GetType();
            var vec = FixedVector3.Zero;

            if (sourceType == typeof(string))
            {
                var str = (string)value;
                var words = str.Split(culture.TextInfo.ListSeparator.ToCharArray());

                vec.X = Fixed.Parse(words[0], culture);
                vec.Y = Fixed.Parse(words[1], culture);
                vec.Z = Fixed.Parse(words[2], culture);

                return vec;
            }

            return base.ConvertFrom(context, culture, value);
        }
    }
}
