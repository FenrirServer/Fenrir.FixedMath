using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace FixedMath
{
    /// <summary>
    /// Describes a 2D-vector.
    /// </summary>
    [TypeConverter(typeof(FixedVector2TypeConverter))]
    [DataContract]
    [DebuggerDisplay("{DebugDisplayString,nq}")]
    public struct FixedVector2 : IEquatable<FixedVector2>
    {
        #region Private Fields

        private static readonly FixedVector2 zeroVector = new FixedVector2(Fixed.Zero, Fixed.Zero);
        private static readonly FixedVector2 unitVector = new FixedVector2(Fixed.One, Fixed.One);
        private static readonly FixedVector2 unitXVector = new FixedVector2(Fixed.One, Fixed.Zero);
        private static readonly FixedVector2 unitYVector = new FixedVector2(Fixed.Zero, Fixed.One);

        #endregion

        #region Public Fields

        /// <summary>
        /// The x coordinate of this <see cref="FixedVector2"/>.
        /// </summary>
        [DataMember]
        public Fixed X;

        /// <summary>
        /// The y coordinate of this <see cref="FixedVector2"/>.
        /// </summary>
        [DataMember]
        public Fixed Y;

        #endregion

        #region Properties

        /// <summary>
        /// Returns a <see cref="FixedVector2"/> with components 0, 0.
        /// </summary>
        public static FixedVector2 Zero
        {
            get { return zeroVector; }
        }

        /// <summary>
        /// Returns a <see cref="FixedVector2"/> with components 1, 1.
        /// </summary>
        public static FixedVector2 One
        {
            get { return unitVector; }
        }

        /// <summary>
        /// Returns a <see cref="FixedVector2"/> with components 1, 0.
        /// </summary>
        public static FixedVector2 UnitX
        {
            get { return unitXVector; }
        }

        /// <summary>
        /// Returns a <see cref="FixedVector2"/> with components 0, 1.
        /// </summary>
        public static FixedVector2 UnitY
        {
            get { return unitYVector; }
        }

        #endregion

        #region Internal Properties

        internal string DebugDisplayString
        {
            get
            {
                return string.Concat(
                    this.X.ToString(), "  ",
                    this.Y.ToString()
                );
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Constructs a 2d vector with X and Y from two values.
        /// </summary>
        /// <param name="x">The x coordinate in 2d-space.</param>
        /// <param name="y">The y coordinate in 2d-space.</param>
        internal FixedVector2(float x, float y)
        {
            this.X = (Fixed)x;
            this.Y = (Fixed)y;
        }

        /// <summary>
        /// Constructs a 2d vector with X and Y from two values.
        /// </summary>
        /// <param name="x">The x coordinate in 2d-space.</param>
        /// <param name="y">The y coordinate in 2d-space.</param>
        public FixedVector2(Fixed x, Fixed y)
        {
            this.X = x;
            this.Y = y;
        }

        /// <summary>
        /// Constructs a 2d vector with X and Y set to the same value.
        /// </summary>
        /// <param name="value">The x and y coordinates in 2d-space.</param>
        public FixedVector2(Fixed value)
        {
            this.X = value;
            this.Y = value;
        }

        #endregion

        #region Operators

        /// <summary>
        /// Converts a <see cref="System.Numerics.Vector2"/> to a <see cref="FixedVector2"/>.
        /// </summary>
        /// <param name="value">The converted value.</param>
        public static implicit operator FixedVector2(System.Numerics.Vector2 value)
        {
            return new FixedVector2((Fixed)value.X, (Fixed)value.Y);
        }

        /// <summary>
        /// Inverts values in the specified <see cref="FixedVector2"/>.
        /// </summary>
        /// <param name="value">Source <see cref="FixedVector2"/> on the right of the sub sign.</param>
        /// <returns>Result of the inversion.</returns>
        public static FixedVector2 operator -(FixedVector2 value)
        {
            value.X = -value.X;
            value.Y = -value.Y;
            return value;
        }

        /// <summary>
        /// Adds two vectors.
        /// </summary>
        /// <param name="value1">Source <see cref="FixedVector2"/> on the left of the add sign.</param>
        /// <param name="value2">Source <see cref="FixedVector2"/> on the right of the add sign.</param>
        /// <returns>Sum of the vectors.</returns>
        public static FixedVector2 operator +(FixedVector2 value1, FixedVector2 value2)
        {
            value1.X += value2.X;
            value1.Y += value2.Y;
            return value1;
        }

        /// <summary>
        /// Subtracts a <see cref="FixedVector2"/> from a <see cref="FixedVector2"/>.
        /// </summary>
        /// <param name="value1">Source <see cref="FixedVector2"/> on the left of the sub sign.</param>
        /// <param name="value2">Source <see cref="FixedVector2"/> on the right of the sub sign.</param>
        /// <returns>Result of the vector subtraction.</returns>
        public static FixedVector2 operator -(FixedVector2 value1, FixedVector2 value2)
        {
            value1.X -= value2.X;
            value1.Y -= value2.Y;
            return value1;
        }

        /// <summary>
        /// Multiplies the components of two vectors by each other.
        /// </summary>
        /// <param name="value1">Source <see cref="FixedVector2"/> on the left of the mul sign.</param>
        /// <param name="value2">Source <see cref="FixedVector2"/> on the right of the mul sign.</param>
        /// <returns>Result of the vector multiplication.</returns>
        public static FixedVector2 operator *(FixedVector2 value1, FixedVector2 value2)
        {
            value1.X *= value2.X;
            value1.Y *= value2.Y;
            return value1;
        }

        /// <summary>
        /// Multiplies the components of vector by a scalar.
        /// </summary>
        /// <param name="value">Source <see cref="FixedVector2"/> on the left of the mul sign.</param>
        /// <param name="scaleFactor">Scalar value on the right of the mul sign.</param>
        /// <returns>Result of the vector multiplication with a scalar.</returns>
        public static FixedVector2 operator *(FixedVector2 value, Fixed scaleFactor)
        {
            value.X *= scaleFactor;
            value.Y *= scaleFactor;
            return value;
        }

        /// <summary>
        /// Multiplies the components of vector by a scalar.
        /// </summary>
        /// <param name="scaleFactor">Scalar value on the left of the mul sign.</param>
        /// <param name="value">Source <see cref="FixedVector2"/> on the right of the mul sign.</param>
        /// <returns>Result of the vector multiplication with a scalar.</returns>
        public static FixedVector2 operator *(Fixed scaleFactor, FixedVector2 value)
        {
            value.X *= scaleFactor;
            value.Y *= scaleFactor;
            return value;
        }

        /// <summary>
        /// Divides the components of a <see cref="FixedVector2"/> by the components of another <see cref="FixedVector2"/>.
        /// </summary>
        /// <param name="value1">Source <see cref="FixedVector2"/> on the left of the div sign.</param>
        /// <param name="value2">Divisor <see cref="FixedVector2"/> on the right of the div sign.</param>
        /// <returns>The result of dividing the vectors.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static FixedVector2 operator /(FixedVector2 value1, FixedVector2 value2)
        {
            value1.X /= value2.X;
            value1.Y /= value2.Y;
            return value1;
        }

        /// <summary>
        /// Divides the components of a <see cref="FixedVector2"/> by a scalar.
        /// </summary>
        /// <param name="value1">Source <see cref="FixedVector2"/> on the left of the div sign.</param>
        /// <param name="divider">Divisor scalar on the right of the div sign.</param>
        /// <returns>The result of dividing a vector by a scalar.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static FixedVector2 operator /(FixedVector2 value1, Fixed divider)
        {
            Fixed factor = Fixed.One / divider;
            value1.X *= factor;
            value1.Y *= factor;
            return value1;
        }

        /// <summary>
        /// Compares whether two <see cref="FixedVector2"/> instances are equal.
        /// </summary>
        /// <param name="value1"><see cref="FixedVector2"/> instance on the left of the equal sign.</param>
        /// <param name="value2"><see cref="FixedVector2"/> instance on the right of the equal sign.</param>
        /// <returns><c>true</c> if the instances are equal; <c>false</c> otherwise.</returns>
        public static bool operator ==(FixedVector2 value1, FixedVector2 value2)
        {
            return value1.X == value2.X && value1.Y == value2.Y;
        }

        /// <summary>
        /// Compares whether two <see cref="FixedVector2"/> instances are not equal.
        /// </summary>
        /// <param name="value1"><see cref="FixedVector2"/> instance on the left of the not equal sign.</param>
        /// <param name="value2"><see cref="FixedVector2"/> instance on the right of the not equal sign.</param>
        /// <returns><c>true</c> if the instances are not equal; <c>false</c> otherwise.</returns>	
        public static bool operator !=(FixedVector2 value1, FixedVector2 value2)
        {
            return value1.X != value2.X || value1.Y != value2.Y;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Performs vector addition on <paramref name="value1"/> and <paramref name="value2"/>.
        /// </summary>
        /// <param name="value1">The first vector to add.</param>
        /// <param name="value2">The second vector to add.</param>
        /// <returns>The result of the vector addition.</returns>
        public static FixedVector2 Add(FixedVector2 value1, FixedVector2 value2)
        {
            value1.X += value2.X;
            value1.Y += value2.Y;
            return value1;
        }

        /// <summary>
        /// Performs vector addition on <paramref name="value1"/> and
        /// <paramref name="value2"/>, storing the result of the
        /// addition in <paramref name="result"/>.
        /// </summary>
        /// <param name="value1">The first vector to add.</param>
        /// <param name="value2">The second vector to add.</param>
        /// <param name="result">The result of the vector addition.</param>
        public static void Add(ref FixedVector2 value1, ref FixedVector2 value2, out FixedVector2 result)
        {
            result.X = value1.X + value2.X;
            result.Y = value1.Y + value2.Y;
        }

        /// <summary>
        /// Creates a new <see cref="FixedVector2"/> that contains the cartesian coordinates of a vector specified in barycentric coordinates and relative to 2d-triangle.
        /// </summary>
        /// <param name="value1">The first vector of 2d-triangle.</param>
        /// <param name="value2">The second vector of 2d-triangle.</param>
        /// <param name="value3">The third vector of 2d-triangle.</param>
        /// <param name="amount1">Barycentric scalar <c>b2</c> which represents a weighting factor towards second vector of 2d-triangle.</param>
        /// <param name="amount2">Barycentric scalar <c>b3</c> which represents a weighting factor towards third vector of 2d-triangle.</param>
        /// <returns>The cartesian translation of barycentric coordinates.</returns>
        public static FixedVector2 Barycentric(FixedVector2 value1, FixedVector2 value2, FixedVector2 value3, Fixed amount1, Fixed amount2)
        {
            return new FixedVector2(
                Fixed.Barycentric(value1.X, value2.X, value3.X, amount1, amount2),
                Fixed.Barycentric(value1.Y, value2.Y, value3.Y, amount1, amount2));
        }

        /// <summary>
        /// Creates a new <see cref="FixedVector2"/> that contains the cartesian coordinates of a vector specified in barycentric coordinates and relative to 2d-triangle.
        /// </summary>
        /// <param name="value1">The first vector of 2d-triangle.</param>
        /// <param name="value2">The second vector of 2d-triangle.</param>
        /// <param name="value3">The third vector of 2d-triangle.</param>
        /// <param name="amount1">Barycentric scalar <c>b2</c> which represents a weighting factor towards second vector of 2d-triangle.</param>
        /// <param name="amount2">Barycentric scalar <c>b3</c> which represents a weighting factor towards third vector of 2d-triangle.</param>
        /// <param name="result">The cartesian translation of barycentric coordinates as an output parameter.</param>
        public static void Barycentric(ref FixedVector2 value1, ref FixedVector2 value2, ref FixedVector2 value3, Fixed amount1, Fixed amount2, out FixedVector2 result)
        {
            result.X = Fixed.Barycentric(value1.X, value2.X, value3.X, amount1, amount2);
            result.Y = Fixed.Barycentric(value1.Y, value2.Y, value3.Y, amount1, amount2);
        }

        /// <summary>
        /// Creates a new <see cref="FixedVector2"/> that contains CatmullRom interpolation of the specified vectors.
        /// </summary>
        /// <param name="value1">The first vector in interpolation.</param>
        /// <param name="value2">The second vector in interpolation.</param>
        /// <param name="value3">The third vector in interpolation.</param>
        /// <param name="value4">The fourth vector in interpolation.</param>
        /// <param name="amount">Weighting factor.</param>
        /// <returns>The result of CatmullRom interpolation.</returns>
        public static FixedVector2 CatmullRom(FixedVector2 value1, FixedVector2 value2, FixedVector2 value3, FixedVector2 value4, Fixed amount)
        {
            return new FixedVector2(
                Fixed.CatmullRom(value1.X, value2.X, value3.X, value4.X, amount),
                Fixed.CatmullRom(value1.Y, value2.Y, value3.Y, value4.Y, amount));
        }

        /// <summary>
        /// Creates a new <see cref="FixedVector2"/> that contains CatmullRom interpolation of the specified vectors.
        /// </summary>
        /// <param name="value1">The first vector in interpolation.</param>
        /// <param name="value2">The second vector in interpolation.</param>
        /// <param name="value3">The third vector in interpolation.</param>
        /// <param name="value4">The fourth vector in interpolation.</param>
        /// <param name="amount">Weighting factor.</param>
        /// <param name="result">The result of CatmullRom interpolation as an output parameter.</param>
        public static void CatmullRom(ref FixedVector2 value1, ref FixedVector2 value2, ref FixedVector2 value3, ref FixedVector2 value4, Fixed amount, out FixedVector2 result)
        {
            result.X = Fixed.CatmullRom(value1.X, value2.X, value3.X, value4.X, amount);
            result.Y = Fixed.CatmullRom(value1.Y, value2.Y, value3.Y, value4.Y, amount);
        }

        /// <summary>
        /// Round the members of this <see cref="FixedVector2"/> towards positive infinity.
        /// </summary>
        public void Ceiling()
        {
            X = Fixed.Ceiling(X);
            Y = Fixed.Ceiling(Y);
        }

        /// <summary>
        /// Creates a new <see cref="FixedVector2"/> that contains members from another vector rounded towards positive infinity.
        /// </summary>
        /// <param name="value">Source <see cref="FixedVector2"/>.</param>
        /// <returns>The rounded <see cref="FixedVector2"/>.</returns>
        public static FixedVector2 Ceiling(FixedVector2 value)
        {
            value.X = Fixed.Ceiling(value.X);
            value.Y = Fixed.Ceiling(value.Y);
            return value;
        }

        /// <summary>
        /// Creates a new <see cref="FixedVector2"/> that contains members from another vector rounded towards positive infinity.
        /// </summary>
        /// <param name="value">Source <see cref="FixedVector2"/>.</param>
        /// <param name="result">The rounded <see cref="FixedVector2"/>.</param>
        public static void Ceiling(ref FixedVector2 value, out FixedVector2 result)
        {
            result.X = Fixed.Ceiling(value.X);
            result.Y = Fixed.Ceiling(value.Y);
        }

        /// <summary>
        /// Clamps the specified value within a range.
        /// </summary>
        /// <param name="value1">The value to clamp.</param>
        /// <param name="min">The min value.</param>
        /// <param name="max">The max value.</param>
        /// <returns>The clamped value.</returns>
        public static FixedVector2 Clamp(FixedVector2 value1, FixedVector2 min, FixedVector2 max)
        {
            return new FixedVector2(
                Fixed.Clamp(value1.X, min.X, max.X),
                Fixed.Clamp(value1.Y, min.Y, max.Y));
        }

        /// <summary>
        /// Clamps the specified value within a range.
        /// </summary>
        /// <param name="value1">The value to clamp.</param>
        /// <param name="min">The min value.</param>
        /// <param name="max">The max value.</param>
        /// <param name="result">The clamped value as an output parameter.</param>
        public static void Clamp(ref FixedVector2 value1, ref FixedVector2 min, ref FixedVector2 max, out FixedVector2 result)
        {
            result.X = Fixed.Clamp(value1.X, min.X, max.X);
            result.Y = Fixed.Clamp(value1.Y, min.Y, max.Y);
        }

        /// <summary>
        /// Returns the distance between two vectors.
        /// </summary>
        /// <param name="value1">The first vector.</param>
        /// <param name="value2">The second vector.</param>
        /// <returns>The distance between two vectors.</returns>
        public static Fixed Distance(FixedVector2 value1, FixedVector2 value2)
        {
            Fixed v1 = value1.X - value2.X, v2 = value1.Y - value2.Y;
            return Fixed.Sqrt((v1 * v1) + (v2 * v2));
        }

        /// <summary>
        /// Returns the distance between two vectors.
        /// </summary>
        /// <param name="value1">The first vector.</param>
        /// <param name="value2">The second vector.</param>
        /// <param name="result">The distance between two vectors as an output parameter.</param>
        public static void Distance(ref FixedVector2 value1, ref FixedVector2 value2, out Fixed result)
        {
            Fixed v1 = value1.X - value2.X, v2 = value1.Y - value2.Y;
            result = Fixed.Sqrt((v1 * v1) + (v2 * v2));
        }

        /// <summary>
        /// Returns the squared distance between two vectors.
        /// </summary>
        /// <param name="value1">The first vector.</param>
        /// <param name="value2">The second vector.</param>
        /// <returns>The squared distance between two vectors.</returns>
        public static Fixed DistanceSquared(FixedVector2 value1, FixedVector2 value2)
        {
            Fixed v1 = value1.X - value2.X, v2 = value1.Y - value2.Y;
            return (v1 * v1) + (v2 * v2);
        }

        /// <summary>
        /// Returns the squared distance between two vectors.
        /// </summary>
        /// <param name="value1">The first vector.</param>
        /// <param name="value2">The second vector.</param>
        /// <param name="result">The squared distance between two vectors as an output parameter.</param>
        public static void DistanceSquared(ref FixedVector2 value1, ref FixedVector2 value2, out Fixed result)
        {
            Fixed v1 = value1.X - value2.X, v2 = value1.Y - value2.Y;
            result = (v1 * v1) + (v2 * v2);
        }

        /// <summary>
        /// Divides the components of a <see cref="FixedVector2"/> by the components of another <see cref="FixedVector2"/>.
        /// </summary>
        /// <param name="value1">Source <see cref="FixedVector2"/>.</param>
        /// <param name="value2">Divisor <see cref="FixedVector2"/>.</param>
        /// <returns>The result of dividing the vectors.</returns>
        public static FixedVector2 Divide(FixedVector2 value1, FixedVector2 value2)
        {
            value1.X /= value2.X;
            value1.Y /= value2.Y;
            return value1;
        }

        /// <summary>
        /// Divides the components of a <see cref="FixedVector2"/> by the components of another <see cref="FixedVector2"/>.
        /// </summary>
        /// <param name="value1">Source <see cref="FixedVector2"/>.</param>
        /// <param name="value2">Divisor <see cref="FixedVector2"/>.</param>
        /// <param name="result">The result of dividing the vectors as an output parameter.</param>
        public static void Divide(ref FixedVector2 value1, ref FixedVector2 value2, out FixedVector2 result)
        {
            result.X = value1.X / value2.X;
            result.Y = value1.Y / value2.Y;
        }

        /// <summary>
        /// Divides the components of a <see cref="FixedVector2"/> by a scalar.
        /// </summary>
        /// <param name="value1">Source <see cref="FixedVector2"/>.</param>
        /// <param name="divider">Divisor scalar.</param>
        /// <returns>The result of dividing a vector by a scalar.</returns>
        public static FixedVector2 Divide(FixedVector2 value1, Fixed divider)
        {
            Fixed factor = Fixed.One / divider;
            value1.X *= factor;
            value1.Y *= factor;
            return value1;
        }

        /// <summary>
        /// Divides the components of a <see cref="FixedVector2"/> by a scalar.
        /// </summary>
        /// <param name="value1">Source <see cref="FixedVector2"/>.</param>
        /// <param name="divider">Divisor scalar.</param>
        /// <param name="result">The result of dividing a vector by a scalar as an output parameter.</param>
        public static void Divide(ref FixedVector2 value1, Fixed divider, out FixedVector2 result)
        {
            Fixed factor = Fixed.One / divider;
            result.X = value1.X * factor;
            result.Y = value1.Y * factor;
        }

        /// <summary>
        /// Returns a dot product of two vectors.
        /// </summary>
        /// <param name="value1">The first vector.</param>
        /// <param name="value2">The second vector.</param>
        /// <returns>The dot product of two vectors.</returns>
        public static Fixed Dot(FixedVector2 value1, FixedVector2 value2)
        {
            return (value1.X * value2.X) + (value1.Y * value2.Y);
        }

        /// <summary>
        /// Returns a dot product of two vectors.
        /// </summary>
        /// <param name="value1">The first vector.</param>
        /// <param name="value2">The second vector.</param>
        /// <param name="result">The dot product of two vectors as an output parameter.</param>
        public static void Dot(ref FixedVector2 value1, ref FixedVector2 value2, out Fixed result)
        {
            result = (value1.X * value2.X) + (value1.Y * value2.Y);
        }

        /// <summary>
        /// Compares whether current instance is equal to specified <see cref="Object"/>.
        /// </summary>
        /// <param name="obj">The <see cref="Object"/> to compare.</param>
        /// <returns><c>true</c> if the instances are equal; <c>false</c> otherwise.</returns>
        public override bool Equals(object obj)
        {
            if (obj is FixedVector2)
            {
                return Equals((FixedVector2)obj);
            }

            return false;
        }

        /// <summary>
        /// Compares whether current instance is equal to specified <see cref="FixedVector2"/>.
        /// </summary>
        /// <param name="other">The <see cref="FixedVector2"/> to compare.</param>
        /// <returns><c>true</c> if the instances are equal; <c>false</c> otherwise.</returns>
        public bool Equals(FixedVector2 other)
        {
            return (X == other.X) && (Y == other.Y);
        }

        /// <summary>
        /// Round the members of this <see cref="FixedVector2"/> towards negative infinity.
        /// </summary>
        public void Floor()
        {
            X = Fixed.Floor(X);
            Y = Fixed.Floor(Y);
        }

        /// <summary>
        /// Creates a new <see cref="FixedVector2"/> that contains members from another vector rounded towards negative infinity.
        /// </summary>
        /// <param name="value">Source <see cref="FixedVector2"/>.</param>
        /// <returns>The rounded <see cref="FixedVector2"/>.</returns>
        public static FixedVector2 Floor(FixedVector2 value)
        {
            value.X = Fixed.Floor(value.X);
            value.Y = Fixed.Floor(value.Y);
            return value;
        }

        /// <summary>
        /// Creates a new <see cref="FixedVector2"/> that contains members from another vector rounded towards negative infinity.
        /// </summary>
        /// <param name="value">Source <see cref="FixedVector2"/>.</param>
        /// <param name="result">The rounded <see cref="FixedVector2"/>.</param>
        public static void Floor(ref FixedVector2 value, out FixedVector2 result)
        {
            result.X = Fixed.Floor(value.X);
            result.Y = Fixed.Floor(value.Y);
        }

        /// <summary>
        /// Gets the hash code of this <see cref="FixedVector2"/>.
        /// </summary>
        /// <returns>Hash code of this <see cref="FixedVector2"/>.</returns>
        public override int GetHashCode()
        {
            unchecked
            {
                return (X.GetHashCode() * 397) ^ Y.GetHashCode();
            }
        }

        /// <summary>
        /// Creates a new <see cref="FixedVector2"/> that contains hermite spline interpolation.
        /// </summary>
        /// <param name="value1">The first position vector.</param>
        /// <param name="tangent1">The first tangent vector.</param>
        /// <param name="value2">The second position vector.</param>
        /// <param name="tangent2">The second tangent vector.</param>
        /// <param name="amount">Weighting factor.</param>
        /// <returns>The hermite spline interpolation vector.</returns>
        public static FixedVector2 Hermite(FixedVector2 value1, FixedVector2 tangent1, FixedVector2 value2, FixedVector2 tangent2, Fixed amount)
        {
            return new FixedVector2(Fixed.Hermite(value1.X, tangent1.X, value2.X, tangent2.X, amount), Fixed.Hermite(value1.Y, tangent1.Y, value2.Y, tangent2.Y, amount));
        }

        /// <summary>
        /// Creates a new <see cref="FixedVector2"/> that contains hermite spline interpolation.
        /// </summary>
        /// <param name="value1">The first position vector.</param>
        /// <param name="tangent1">The first tangent vector.</param>
        /// <param name="value2">The second position vector.</param>
        /// <param name="tangent2">The second tangent vector.</param>
        /// <param name="amount">Weighting factor.</param>
        /// <param name="result">The hermite spline interpolation vector as an output parameter.</param>
        public static void Hermite(ref FixedVector2 value1, ref FixedVector2 tangent1, ref FixedVector2 value2, ref FixedVector2 tangent2, Fixed amount, out FixedVector2 result)
        {
            result.X = Fixed.Hermite(value1.X, tangent1.X, value2.X, tangent2.X, amount);
            result.Y = Fixed.Hermite(value1.Y, tangent1.Y, value2.Y, tangent2.Y, amount);
        }

        /// <summary>
        /// Returns the length of this <see cref="FixedVector2"/>.
        /// </summary>
        /// <returns>The length of this <see cref="FixedVector2"/>.</returns>
        public Fixed Length()
        {
            return Fixed.Sqrt((X * X) + (Y * Y));
        }

        /// <summary>
        /// Returns the squared length of this <see cref="FixedVector2"/>.
        /// </summary>
        /// <returns>The squared length of this <see cref="FixedVector2"/>.</returns>
        public Fixed LengthSquared()
        {
            return (X * X) + (Y * Y);
        }

        /// <summary>
        /// Creates a new <see cref="FixedVector2"/> that contains linear interpolation of the specified vectors.
        /// </summary>
        /// <param name="value1">The first vector.</param>
        /// <param name="value2">The second vector.</param>
        /// <param name="amount">Weighting value(between 0.0 and 1.0).</param>
        /// <returns>The result of linear interpolation of the specified vectors.</returns>
        public static FixedVector2 Lerp(FixedVector2 value1, FixedVector2 value2, Fixed amount)
        {
            return new FixedVector2(
                Fixed.Lerp(value1.X, value2.X, amount),
                Fixed.Lerp(value1.Y, value2.Y, amount));
        }

        /// <summary>
        /// Creates a new <see cref="FixedVector2"/> that contains linear interpolation of the specified vectors.
        /// </summary>
        /// <param name="value1">The first vector.</param>
        /// <param name="value2">The second vector.</param>
        /// <param name="amount">Weighting value(between 0.0 and 1.0).</param>
        /// <param name="result">The result of linear interpolation of the specified vectors as an output parameter.</param>
        public static void Lerp(ref FixedVector2 value1, ref FixedVector2 value2, Fixed amount, out FixedVector2 result)
        {
            result.X = Fixed.Lerp(value1.X, value2.X, amount);
            result.Y = Fixed.Lerp(value1.Y, value2.Y, amount);
        }

        /// <summary>
        /// Creates a new <see cref="FixedVector2"/> that contains linear interpolation of the specified vectors.
        /// Uses <see cref="Fixed.LerpPrecise"/> on MathHelper for the interpolation.
        /// Less efficient but more precise compared to <see cref="FixedVector2.Lerp(FixedVector2, FixedVector2, Fixed)"/>.
        /// See remarks section of <see cref="Fixed.LerpPrecise"/> on MathHelper for more info.
        /// </summary>
        /// <param name="value1">The first vector.</param>
        /// <param name="value2">The second vector.</param>
        /// <param name="amount">Weighting value(between 0.0 and 1.0).</param>
        /// <returns>The result of linear interpolation of the specified vectors.</returns>
        public static FixedVector2 LerpPrecise(FixedVector2 value1, FixedVector2 value2, Fixed amount)
        {
            return new FixedVector2(
                Fixed.LerpPrecise(value1.X, value2.X, amount),
                Fixed.LerpPrecise(value1.Y, value2.Y, amount));
        }

        /// <summary>
        /// Creates a new <see cref="FixedVector2"/> that contains linear interpolation of the specified vectors.
        /// Uses <see cref="Fixed.LerpPrecise"/> on MathHelper for the interpolation.
        /// Less efficient but more precise compared to <see cref="FixedVector2.Lerp(ref FixedVector2, ref FixedVector2, Fixed, out FixedVector2)"/>.
        /// See remarks section of <see cref="Fixed.LerpPrecise"/> on MathHelper for more info.
        /// </summary>
        /// <param name="value1">The first vector.</param>
        /// <param name="value2">The second vector.</param>
        /// <param name="amount">Weighting value(between 0.0 and 1.0).</param>
        /// <param name="result">The result of linear interpolation of the specified vectors as an output parameter.</param>
        public static void LerpPrecise(ref FixedVector2 value1, ref FixedVector2 value2, Fixed amount, out FixedVector2 result)
        { 
            result.X = Fixed.LerpPrecise(value1.X, value2.X, amount);
            result.Y = Fixed.LerpPrecise(value1.Y, value2.Y, amount);
        }

        /// <summary>
        /// Creates a new <see cref="FixedVector2"/> that contains a maximal values from the two vectors.
        /// </summary>
        /// <param name="value1">The first vector.</param>
        /// <param name="value2">The second vector.</param>
        /// <returns>The <see cref="FixedVector2"/> with maximal values from the two vectors.</returns>
        public static FixedVector2 Max(FixedVector2 value1, FixedVector2 value2)
        {
            return new FixedVector2(value1.X > value2.X ? value1.X : value2.X,
                               value1.Y > value2.Y ? value1.Y : value2.Y);
        }

        /// <summary>
        /// Creates a new <see cref="FixedVector2"/> that contains a maximal values from the two vectors.
        /// </summary>
        /// <param name="value1">The first vector.</param>
        /// <param name="value2">The second vector.</param>
        /// <param name="result">The <see cref="FixedVector2"/> with maximal values from the two vectors as an output parameter.</param>
        public static void Max(ref FixedVector2 value1, ref FixedVector2 value2, out FixedVector2 result)
        {
            result.X = value1.X > value2.X ? value1.X : value2.X;
            result.Y = value1.Y > value2.Y ? value1.Y : value2.Y;
        }

        /// <summary>
        /// Creates a new <see cref="FixedVector2"/> that contains a minimal values from the two vectors.
        /// </summary>
        /// <param name="value1">The first vector.</param>
        /// <param name="value2">The second vector.</param>
        /// <returns>The <see cref="FixedVector2"/> with minimal values from the two vectors.</returns>
        public static FixedVector2 Min(FixedVector2 value1, FixedVector2 value2)
        {
            return new FixedVector2(value1.X < value2.X ? value1.X : value2.X,
                               value1.Y < value2.Y ? value1.Y : value2.Y);
        }

        /// <summary>
        /// Creates a new <see cref="FixedVector2"/> that contains a minimal values from the two vectors.
        /// </summary>
        /// <param name="value1">The first vector.</param>
        /// <param name="value2">The second vector.</param>
        /// <param name="result">The <see cref="FixedVector2"/> with minimal values from the two vectors as an output parameter.</param>
        public static void Min(ref FixedVector2 value1, ref FixedVector2 value2, out FixedVector2 result)
        {
            result.X = value1.X < value2.X ? value1.X : value2.X;
            result.Y = value1.Y < value2.Y ? value1.Y : value2.Y;
        }

        /// <summary>
        /// Creates a new <see cref="FixedVector2"/> that contains a multiplication of two vectors.
        /// </summary>
        /// <param name="value1">Source <see cref="FixedVector2"/>.</param>
        /// <param name="value2">Source <see cref="FixedVector2"/>.</param>
        /// <returns>The result of the vector multiplication.</returns>
        public static FixedVector2 Multiply(FixedVector2 value1, FixedVector2 value2)
        {
            value1.X *= value2.X;
            value1.Y *= value2.Y;
            return value1;
        }

        /// <summary>
        /// Creates a new <see cref="FixedVector2"/> that contains a multiplication of two vectors.
        /// </summary>
        /// <param name="value1">Source <see cref="FixedVector2"/>.</param>
        /// <param name="value2">Source <see cref="FixedVector2"/>.</param>
        /// <param name="result">The result of the vector multiplication as an output parameter.</param>
        public static void Multiply(ref FixedVector2 value1, ref FixedVector2 value2, out FixedVector2 result)
        {
            result.X = value1.X * value2.X;
            result.Y = value1.Y * value2.Y;
        }

        /// <summary>
        /// Creates a new <see cref="FixedVector2"/> that contains a multiplication of <see cref="FixedVector2"/> and a scalar.
        /// </summary>
        /// <param name="value1">Source <see cref="FixedVector2"/>.</param>
        /// <param name="scaleFactor">Scalar value.</param>
        /// <returns>The result of the vector multiplication with a scalar.</returns>
        public static FixedVector2 Multiply(FixedVector2 value1, Fixed scaleFactor)
        {
            value1.X *= scaleFactor;
            value1.Y *= scaleFactor;
            return value1;
        }

        /// <summary>
        /// Creates a new <see cref="FixedVector2"/> that contains a multiplication of <see cref="FixedVector2"/> and a scalar.
        /// </summary>
        /// <param name="value1">Source <see cref="FixedVector2"/>.</param>
        /// <param name="scaleFactor">Scalar value.</param>
        /// <param name="result">The result of the multiplication with a scalar as an output parameter.</param>
        public static void Multiply(ref FixedVector2 value1, Fixed scaleFactor, out FixedVector2 result)
        {
            result.X = value1.X * scaleFactor;
            result.Y = value1.Y * scaleFactor;
        }

        /// <summary>
        /// Creates a new <see cref="FixedVector2"/> that contains the specified vector inversion.
        /// </summary>
        /// <param name="value">Source <see cref="FixedVector2"/>.</param>
        /// <returns>The result of the vector inversion.</returns>
        public static FixedVector2 Negate(FixedVector2 value)
        {
            value.X = -value.X;
            value.Y = -value.Y;
            return value;
        }

        /// <summary>
        /// Creates a new <see cref="FixedVector2"/> that contains the specified vector inversion.
        /// </summary>
        /// <param name="value">Source <see cref="FixedVector2"/>.</param>
        /// <param name="result">The result of the vector inversion as an output parameter.</param>
        public static void Negate(ref FixedVector2 value, out FixedVector2 result)
        {
            result.X = -value.X;
            result.Y = -value.Y;
        }

        /// <summary>
        /// Turns this <see cref="FixedVector2"/> to a unit vector with the same direction.
        /// </summary>
        public void Normalize()
        {
            Fixed val = Fixed.One / Fixed.Sqrt((X * X) + (Y * Y));
            X *= val;
            Y *= val;
        }

        /// <summary>
        /// Creates a new <see cref="FixedVector2"/> that contains a normalized values from another vector.
        /// </summary>
        /// <param name="value">Source <see cref="FixedVector2"/>.</param>
        /// <returns>Unit vector.</returns>
        public static FixedVector2 Normalize(FixedVector2 value)
        {
            Fixed val = Fixed.One / Fixed.Sqrt((value.X * value.X) + (value.Y * value.Y));
            value.X *= val;
            value.Y *= val;
            return value;
        }

        /// <summary>
        /// Creates a new <see cref="FixedVector2"/> that contains a normalized values from another vector.
        /// </summary>
        /// <param name="value">Source <see cref="FixedVector2"/>.</param>
        /// <param name="result">Unit vector as an output parameter.</param>
        public static void Normalize(ref FixedVector2 value, out FixedVector2 result)
        {
            Fixed val = Fixed.One / Fixed.Sqrt((value.X * value.X) + (value.Y * value.Y));
            result.X = value.X * val;
            result.Y = value.Y * val;
        }

        /// <summary>
        /// Creates a new <see cref="FixedVector2"/> that contains reflect vector of the given vector and normal.
        /// </summary>
        /// <param name="vector">Source <see cref="FixedVector2"/>.</param>
        /// <param name="normal">Reflection normal.</param>
        /// <returns>Reflected vector.</returns>
        public static FixedVector2 Reflect(FixedVector2 vector, FixedVector2 normal)
        {
            FixedVector2 result;
            Fixed val = Fixed.Two * ((vector.X * normal.X) + (vector.Y * normal.Y));
            result.X = vector.X - (normal.X * val);
            result.Y = vector.Y - (normal.Y * val);
            return result;
        }

        /// <summary>
        /// Creates a new <see cref="FixedVector2"/> that contains reflect vector of the given vector and normal.
        /// </summary>
        /// <param name="vector">Source <see cref="FixedVector2"/>.</param>
        /// <param name="normal">Reflection normal.</param>
        /// <param name="result">Reflected vector as an output parameter.</param>
        public static void Reflect(ref FixedVector2 vector, ref FixedVector2 normal, out FixedVector2 result)
        {
            Fixed val = Fixed.Two * ((vector.X * normal.X) + (vector.Y * normal.Y));
            result.X = vector.X - (normal.X * val);
            result.Y = vector.Y - (normal.Y * val);
        }

        /// <summary>
        /// Round the members of this <see cref="FixedVector2"/> to the nearest integer value.
        /// </summary>
        public void Round()
        {
            X = Fixed.Round(X);
            Y = Fixed.Round(Y);
        }

        /// <summary>
        /// Creates a new <see cref="FixedVector2"/> that contains members from another vector rounded to the nearest integer value.
        /// </summary>
        /// <param name="value">Source <see cref="FixedVector2"/>.</param>
        /// <returns>The rounded <see cref="FixedVector2"/>.</returns>
        public static FixedVector2 Round(FixedVector2 value)
        {
            value.X = Fixed.Round(value.X);
            value.Y = Fixed.Round(value.Y);
            return value;
        }

        /// <summary>
        /// Creates a new <see cref="FixedVector2"/> that contains members from another vector rounded to the nearest integer value.
        /// </summary>
        /// <param name="value">Source <see cref="FixedVector2"/>.</param>
        /// <param name="result">The rounded <see cref="FixedVector2"/>.</param>
        public static void Round(ref FixedVector2 value, out FixedVector2 result)
        {
            result.X = Fixed.Round(value.X);
            result.Y = Fixed.Round(value.Y);
        }

        /// <summary>
        /// Creates a new <see cref="FixedVector2"/> that contains cubic interpolation of the specified vectors.
        /// </summary>
        /// <param name="value1">Source <see cref="FixedVector2"/>.</param>
        /// <param name="value2">Source <see cref="FixedVector2"/>.</param>
        /// <param name="amount">Weighting value.</param>
        /// <returns>Cubic interpolation of the specified vectors.</returns>
        public static FixedVector2 SmoothStep(FixedVector2 value1, FixedVector2 value2, Fixed amount)
        {
            return new FixedVector2(
                Fixed.SmoothStep(value1.X, value2.X, amount),
                Fixed.SmoothStep(value1.Y, value2.Y, amount));
        }

        /// <summary>
        /// Creates a new <see cref="FixedVector2"/> that contains cubic interpolation of the specified vectors.
        /// </summary>
        /// <param name="value1">Source <see cref="FixedVector2"/>.</param>
        /// <param name="value2">Source <see cref="FixedVector2"/>.</param>
        /// <param name="amount">Weighting value.</param>
        /// <param name="result">Cubic interpolation of the specified vectors as an output parameter.</param>
        public static void SmoothStep(ref FixedVector2 value1, ref FixedVector2 value2, Fixed amount, out FixedVector2 result)
        {
            result.X = Fixed.SmoothStep(value1.X, value2.X, amount);
            result.Y = Fixed.SmoothStep(value1.Y, value2.Y, amount);
        }

        /// <summary>
        /// Creates a new <see cref="FixedVector2"/> that contains subtraction of on <see cref="FixedVector2"/> from a another.
        /// </summary>
        /// <param name="value1">Source <see cref="FixedVector2"/>.</param>
        /// <param name="value2">Source <see cref="FixedVector2"/>.</param>
        /// <returns>The result of the vector subtraction.</returns>
        public static FixedVector2 Subtract(FixedVector2 value1, FixedVector2 value2)
        {
            value1.X -= value2.X;
            value1.Y -= value2.Y;
            return value1;
        }

        /// <summary>
        /// Creates a new <see cref="FixedVector2"/> that contains subtraction of on <see cref="FixedVector2"/> from a another.
        /// </summary>
        /// <param name="value1">Source <see cref="FixedVector2"/>.</param>
        /// <param name="value2">Source <see cref="FixedVector2"/>.</param>
        /// <param name="result">The result of the vector subtraction as an output parameter.</param>
        public static void Subtract(ref FixedVector2 value1, ref FixedVector2 value2, out FixedVector2 result)
        {
            result.X = value1.X - value2.X;
            result.Y = value1.Y - value2.Y;
        }

        /// <summary>
        /// Returns a <see cref="String"/> representation of this <see cref="FixedVector2"/> in the format:
        /// {X:[<see cref="X"/>] Y:[<see cref="Y"/>]}
        /// </summary>
        /// <returns>A <see cref="String"/> representation of this <see cref="FixedVector2"/>.</returns>
        public override string ToString()
        {
            return "{X:" + X + " Y:" + Y + "}";
        }

        /// <summary>
        /// Gets a <see cref="FixedPoint"/> representation for this object.
        /// </summary>
        /// <returns>A <see cref="FixedPoint"/> representation for this object.</returns>
        public FixedPoint ToPoint()
        {
            return new FixedPoint((int) X,(int) Y);
        }

        /// <summary>
        /// Creates a new <see cref="FixedVector2"/> that contains a transformation of 2d-vector by the specified <see cref="FixedMatrix"/>.
        /// </summary>
        /// <param name="position">Source <see cref="FixedVector2"/>.</param>
        /// <param name="matrix">The transformation <see cref="FixedMatrix"/>.</param>
        /// <returns>Transformed <see cref="FixedVector2"/>.</returns>
        public static FixedVector2 Transform(FixedVector2 position, FixedMatrix matrix)
        {
            return new FixedVector2((position.X * matrix.M11) + (position.Y * matrix.M21) + matrix.M41, (position.X * matrix.M12) + (position.Y * matrix.M22) + matrix.M42);
        }

        /// <summary>
        /// Creates a new <see cref="FixedVector2"/> that contains a transformation of 2d-vector by the specified <see cref="FixedMatrix"/>.
        /// </summary>
        /// <param name="position">Source <see cref="FixedVector2"/>.</param>
        /// <param name="matrix">The transformation <see cref="FixedMatrix"/>.</param>
        /// <param name="result">Transformed <see cref="FixedVector2"/> as an output parameter.</param>
        public static void Transform(ref FixedVector2 position, ref FixedMatrix matrix, out FixedVector2 result)
        {
            var x = (position.X * matrix.M11) + (position.Y * matrix.M21) + matrix.M41;
            var y = (position.X * matrix.M12) + (position.Y * matrix.M22) + matrix.M42;
            result.X = x;
            result.Y = y;
        }

        /// <summary>
        /// Creates a new <see cref="FixedVector2"/> that contains a transformation of 2d-vector by the specified <see cref="FixedQuaternion"/>, representing the rotation.
        /// </summary>
        /// <param name="value">Source <see cref="FixedVector2"/>.</param>
        /// <param name="rotation">The <see cref="FixedQuaternion"/> which contains rotation transformation.</param>
        /// <returns>Transformed <see cref="FixedVector2"/>.</returns>
        public static FixedVector2 Transform(FixedVector2 value, FixedQuaternion rotation)
        {
            Transform(ref value, ref rotation, out value);
            return value;
        }

        /// <summary>
        /// Creates a new <see cref="FixedVector2"/> that contains a transformation of 2d-vector by the specified <see cref="FixedQuaternion"/>, representing the rotation.
        /// </summary>
        /// <param name="value">Source <see cref="FixedVector2"/>.</param>
        /// <param name="rotation">The <see cref="FixedQuaternion"/> which contains rotation transformation.</param>
        /// <param name="result">Transformed <see cref="FixedVector2"/> as an output parameter.</param>
        public static void Transform(ref FixedVector2 value, ref FixedQuaternion rotation, out FixedVector2 result)
        {
            var rot1 = new FixedVector3(rotation.X + rotation.X, rotation.Y + rotation.Y, rotation.Z + rotation.Z);
            var rot2 = new FixedVector3(rotation.X, rotation.X, rotation.W);
            var rot3 = new FixedVector3(Fixed.One, rotation.Y, rotation.Z);
            var rot4 = rot1*rot2;
            var rot5 = rot1*rot3;

            var v = new FixedVector2();
            v.X = (Fixed)((Fixed)value.X * (Fixed.One - (Fixed)rot5.Y - (Fixed)rot5.Z) + (Fixed)value.Y * ((Fixed)rot4.Y - (Fixed)rot4.Z));
            v.Y = (Fixed)((Fixed)value.X * ((Fixed)rot4.Y + (Fixed)rot4.Z) + (Fixed)value.Y * (Fixed.One - (Fixed)rot4.X - (Fixed)rot5.Z));
            result.X = v.X;
            result.Y = v.Y;
        }

        /// <summary>
        /// Apply transformation on vectors within array of <see cref="FixedVector2"/> by the specified <see cref="FixedMatrix"/> and places the results in an another array.
        /// </summary>
        /// <param name="sourceArray">Source array.</param>
        /// <param name="sourceIndex">The starting index of transformation in the source array.</param>
        /// <param name="matrix">The transformation <see cref="FixedMatrix"/>.</param>
        /// <param name="destinationArray">Destination array.</param>
        /// <param name="destinationIndex">The starting index in the destination array, where the first <see cref="FixedVector2"/> should be written.</param>
        /// <param name="length">The number of vectors to be transformed.</param>
        public static void Transform(
            FixedVector2[] sourceArray,
            int sourceIndex,
            ref FixedMatrix matrix,
            FixedVector2[] destinationArray,
            int destinationIndex,
            int length)
        {
            if (sourceArray == null)
                throw new ArgumentNullException("sourceArray");
            if (destinationArray == null)
                throw new ArgumentNullException("destinationArray");
            if (sourceArray.Length < sourceIndex + length)
                throw new ArgumentException("Source array length is lesser than sourceIndex + length");
            if (destinationArray.Length < destinationIndex + length)
                throw new ArgumentException("Destination array length is lesser than destinationIndex + length");

            for (int x = 0; x < length; x++)
            {
                var position = sourceArray[sourceIndex + x];
                var destination = destinationArray[destinationIndex + x];
                destination.X = (position.X * matrix.M11) + (position.Y * matrix.M21) + matrix.M41;
                destination.Y = (position.X * matrix.M12) + (position.Y * matrix.M22) + matrix.M42;
                destinationArray[destinationIndex + x] = destination;
            }
        }

        /// <summary>
        /// Apply transformation on vectors within array of <see cref="FixedVector2"/> by the specified <see cref="FixedQuaternion"/> and places the results in an another array.
        /// </summary>
        /// <param name="sourceArray">Source array.</param>
        /// <param name="sourceIndex">The starting index of transformation in the source array.</param>
        /// <param name="rotation">The <see cref="FixedQuaternion"/> which contains rotation transformation.</param>
        /// <param name="destinationArray">Destination array.</param>
        /// <param name="destinationIndex">The starting index in the destination array, where the first <see cref="FixedVector2"/> should be written.</param>
        /// <param name="length">The number of vectors to be transformed.</param>
        public static void Transform
        (
            FixedVector2[] sourceArray,
            int sourceIndex,
            ref FixedQuaternion rotation,
            FixedVector2[] destinationArray,
            int destinationIndex,
            int length
        )
        {
            if (sourceArray == null)
                throw new ArgumentNullException("sourceArray");
            if (destinationArray == null)
                throw new ArgumentNullException("destinationArray");
            if (sourceArray.Length < sourceIndex + length)
                throw new ArgumentException("Source array length is lesser than sourceIndex + length");
            if (destinationArray.Length < destinationIndex + length)
                throw new ArgumentException("Destination array length is lesser than destinationIndex + length");

            for (int x = 0; x < length; x++)
            {
                var position = sourceArray[sourceIndex + x];
                var destination = destinationArray[destinationIndex + x];

                FixedVector2 v;
                Transform(ref position,ref rotation,out v); 

                destination.X = v.X;
                destination.Y = v.Y;

                destinationArray[destinationIndex + x] = destination;
            }
        }

        /// <summary>
        /// Apply transformation on all vectors within array of <see cref="FixedVector2"/> by the specified <see cref="FixedMatrix"/> and places the results in an another array.
        /// </summary>
        /// <param name="sourceArray">Source array.</param>
        /// <param name="matrix">The transformation <see cref="FixedMatrix"/>.</param>
        /// <param name="destinationArray">Destination array.</param>
        public static void Transform(
            FixedVector2[] sourceArray,
            ref FixedMatrix matrix,
            FixedVector2[] destinationArray)
        {
            Transform(sourceArray, 0, ref matrix, destinationArray, 0, sourceArray.Length);
        }

        /// <summary>
        /// Apply transformation on all vectors within array of <see cref="FixedVector2"/> by the specified <see cref="FixedQuaternion"/> and places the results in an another array.
        /// </summary>
        /// <param name="sourceArray">Source array.</param>
        /// <param name="rotation">The <see cref="FixedQuaternion"/> which contains rotation transformation.</param>
        /// <param name="destinationArray">Destination array.</param>
        public static void Transform
        (
            FixedVector2[] sourceArray,
            ref FixedQuaternion rotation,
            FixedVector2[] destinationArray
        )
        {
            Transform(sourceArray, 0, ref rotation, destinationArray, 0, sourceArray.Length);
        }

        /// <summary>
        /// Creates a new <see cref="FixedVector2"/> that contains a transformation of the specified normal by the specified <see cref="FixedMatrix"/>.
        /// </summary>
        /// <param name="normal">Source <see cref="FixedVector2"/> which represents a normal vector.</param>
        /// <param name="matrix">The transformation <see cref="FixedMatrix"/>.</param>
        /// <returns>Transformed normal.</returns>
        public static FixedVector2 TransformNormal(FixedVector2 normal, FixedMatrix matrix)
        {
            return new FixedVector2((normal.X * matrix.M11) + (normal.Y * matrix.M21),(normal.X * matrix.M12) + (normal.Y * matrix.M22));
        }

        /// <summary>
        /// Creates a new <see cref="FixedVector2"/> that contains a transformation of the specified normal by the specified <see cref="FixedMatrix"/>.
        /// </summary>
        /// <param name="normal">Source <see cref="FixedVector2"/> which represents a normal vector.</param>
        /// <param name="matrix">The transformation <see cref="FixedMatrix"/>.</param>
        /// <param name="result">Transformed normal as an output parameter.</param>
        public static void TransformNormal(ref FixedVector2 normal, ref FixedMatrix matrix, out FixedVector2 result)
        {
            var x = (normal.X * matrix.M11) + (normal.Y * matrix.M21);
            var y = (normal.X * matrix.M12) + (normal.Y * matrix.M22);
            result.X = x;
            result.Y = y;
        }

        /// <summary>
        /// Apply transformation on normals within array of <see cref="FixedVector2"/> by the specified <see cref="FixedMatrix"/> and places the results in an another array.
        /// </summary>
        /// <param name="sourceArray">Source array.</param>
        /// <param name="sourceIndex">The starting index of transformation in the source array.</param>
        /// <param name="matrix">The transformation <see cref="FixedMatrix"/>.</param>
        /// <param name="destinationArray">Destination array.</param>
        /// <param name="destinationIndex">The starting index in the destination array, where the first <see cref="FixedVector2"/> should be written.</param>
        /// <param name="length">The number of normals to be transformed.</param>
        public static void TransformNormal
        (
            FixedVector2[] sourceArray,
            int sourceIndex,
            ref FixedMatrix matrix,
            FixedVector2[] destinationArray,
            int destinationIndex,
            int length
        )
        {
            if (sourceArray == null)
                throw new ArgumentNullException("sourceArray");
            if (destinationArray == null)
                throw new ArgumentNullException("destinationArray");
            if (sourceArray.Length < sourceIndex + length)
                throw new ArgumentException("Source array length is lesser than sourceIndex + length");
            if (destinationArray.Length < destinationIndex + length)
                throw new ArgumentException("Destination array length is lesser than destinationIndex + length");

            for (int i = 0; i < length; i++)
            {
                var normal = sourceArray[sourceIndex + i];

                destinationArray[destinationIndex + i] = new FixedVector2((normal.X * matrix.M11) + (normal.Y * matrix.M21),
                                                                     (normal.X * matrix.M12) + (normal.Y * matrix.M22));
            }
        }

        /// <summary>
        /// Apply transformation on all normals within array of <see cref="FixedVector2"/> by the specified <see cref="FixedMatrix"/> and places the results in an another array.
        /// </summary>
        /// <param name="sourceArray">Source array.</param>
        /// <param name="matrix">The transformation <see cref="FixedMatrix"/>.</param>
        /// <param name="destinationArray">Destination array.</param>
        public static void TransformNormal
            (
            FixedVector2[] sourceArray,
            ref FixedMatrix matrix,
            FixedVector2[] destinationArray
            )
        {
            if (sourceArray == null)
                throw new ArgumentNullException("sourceArray");
            if (destinationArray == null)
                throw new ArgumentNullException("destinationArray");
            if (destinationArray.Length < sourceArray.Length)
                throw new ArgumentException("Destination array length is lesser than source array length");

            for (int i = 0; i < sourceArray.Length; i++)
            {
                var normal = sourceArray[i];

                destinationArray[i] = new FixedVector2((normal.X * matrix.M11) + (normal.Y * matrix.M21),
                                                  (normal.X * matrix.M12) + (normal.Y * matrix.M22));
            }
        }

        /// <summary>
        /// Deconstruction method for <see cref="FixedVector2"/>.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void Deconstruct(out Fixed x, out Fixed y)
        {
            x = X;
            y = Y;
        }

        /// <summary>
        /// Returns a <see cref="System.Numerics.Vector2"/>.
        /// </summary>
        public System.Numerics.Vector2 ToNumerics()
        {
            return new System.Numerics.Vector2((float)this.X, (float)this.Y);
        }

        #endregion
    }
}
