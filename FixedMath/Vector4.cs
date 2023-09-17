using System;
using System.Runtime.Serialization;
using System.Diagnostics;

namespace FixedMath
{
    /// <summary>
    /// Describes a 4D-vector.
    /// </summary>
    [System.ComponentModel.TypeConverter(typeof(Vector4TypeConverter))]
    [DataContract]
    [DebuggerDisplay("{DebugDisplayString,nq}")]
    public struct Vector4 : IEquatable<Vector4>
    {
        #region Private Fields

        private static readonly Vector4 zero = new Vector4();
        private static readonly Vector4 one = new Vector4(Fixed.One, Fixed.One, Fixed.One, Fixed.One);
        private static readonly Vector4 unitX = new Vector4(Fixed.One, Fixed.Zero, Fixed.Zero, Fixed.Zero);
        private static readonly Vector4 unitY = new Vector4(Fixed.Zero, Fixed.One, Fixed.Zero, Fixed.Zero);
        private static readonly Vector4 unitZ = new Vector4(Fixed.Zero, Fixed.Zero, Fixed.One, Fixed.Zero);
        private static readonly Vector4 unitW = new Vector4(Fixed.Zero, Fixed.Zero, Fixed.Zero, Fixed.One);

        #endregion

        #region Public Fields

        /// <summary>
        /// The x coordinate of this <see cref="Vector4"/>.
        /// </summary>
        [DataMember]
        public Fixed X;

        /// <summary>
        /// The y coordinate of this <see cref="Vector4"/>.
        /// </summary>
        [DataMember]
        public Fixed Y;

        /// <summary>
        /// The z coordinate of this <see cref="Vector4"/>.
        /// </summary>
        [DataMember]
        public Fixed Z;

        /// <summary>
        /// The w coordinate of this <see cref="Vector4"/>.
        /// </summary>
        [DataMember]
        public Fixed W;

        #endregion

        #region Public Properties

        /// <summary>
        /// Returns a <see cref="Vector4"/> with components 0, 0, 0, 0.
        /// </summary>
        public static Vector4 Zero
        {
            get { return zero; }
        }

        /// <summary>
        /// Returns a <see cref="Vector4"/> with components 1, 1, 1, 1.
        /// </summary>
        public static Vector4 One
        {
            get { return one; }
        }

        /// <summary>
        /// Returns a <see cref="Vector4"/> with components 1, 0, 0, 0.
        /// </summary>
        public static Vector4 UnitX
        {
            get { return unitX; }
        }

        /// <summary>
        /// Returns a <see cref="Vector4"/> with components 0, 1, 0, 0.
        /// </summary>
        public static Vector4 UnitY
        {
            get { return unitY; }
        }

        /// <summary>
        /// Returns a <see cref="Vector4"/> with components 0, 0, 1, 0.
        /// </summary>
        public static Vector4 UnitZ
        {
            get { return unitZ; }
        }

        /// <summary>
        /// Returns a <see cref="Vector4"/> with components 0, 0, 0, 1.
        /// </summary>
        public static Vector4 UnitW
        {
            get { return unitW; }
        }

        #endregion

        #region Internal Properties

        internal string DebugDisplayString
        {
            get
            {
                return string.Concat(
                    this.X.ToString(), "  ",
                    this.Y.ToString(), "  ",
                    this.Z.ToString(), "  ",
                    this.W.ToString()
                );
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Constructs a 3d vector with X, Y, Z and W from four values.
        /// </summary>
        /// <param name="x">The x coordinate in 4d-space.</param>
        /// <param name="y">The y coordinate in 4d-space.</param>
        /// <param name="z">The z coordinate in 4d-space.</param>
        /// <param name="w">The w coordinate in 4d-space.</param>
        internal Vector4(float x, float y, float z, float w)
        {
            this.X = (Fixed)x;
            this.Y = (Fixed)y;
            this.Z = (Fixed)z;
            this.W = (Fixed)w;
        }

        /// <summary>
        /// Constructs a 3d vector with X, Y, Z and W from four values.
        /// </summary>
        /// <param name="x">The x coordinate in 4d-space.</param>
        /// <param name="y">The y coordinate in 4d-space.</param>
        /// <param name="z">The z coordinate in 4d-space.</param>
        /// <param name="w">The w coordinate in 4d-space.</param>
        public Vector4(Fixed x, Fixed y, Fixed z, Fixed w)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
            this.W = w;
        }

        /// <summary>
        /// Constructs a 3d vector with X and Z from <see cref="Vector2"/> and Z and W from the scalars.
        /// </summary>
        /// <param name="value">The x and y coordinates in 4d-space.</param>
        /// <param name="z">The z coordinate in 4d-space.</param>
        /// <param name="w">The w coordinate in 4d-space.</param>
        public Vector4(Vector2 value, Fixed z, Fixed w)
        {
            this.X = value.X;
            this.Y = value.Y;
            this.Z = z;
            this.W = w;
        }

        /// <summary>
        /// Constructs a 3d vector with X, Y, Z from <see cref="Vector3"/> and W from a scalar.
        /// </summary>
        /// <param name="value">The x, y and z coordinates in 4d-space.</param>
        /// <param name="w">The w coordinate in 4d-space.</param>
        public Vector4(Vector3 value, Fixed w)
        {
            this.X = value.X;
            this.Y = value.Y;
            this.Z = value.Z;
            this.W = w;
        }


        /// <summary>
        /// Constructs a 4d vector with X, Y, Z and W set to the same value.
        /// </summary>
        /// <param name="value">The x, y, z and w coordinates in 4d-space.</param>
        internal Vector4(float value)
        {
            this.X = (Fixed)value;
            this.Y = (Fixed)value;
            this.Z = (Fixed)value;
            this.W = (Fixed)value;
        }

        /// <summary>
        /// Constructs a 4d vector with X, Y, Z and W set to the same value.
        /// </summary>
        /// <param name="value">The x, y, z and w coordinates in 4d-space.</param>
        public Vector4(Fixed value)
        {
            this.X = value;
            this.Y = value;
            this.Z = value;
            this.W = value;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Performs vector addition on <paramref name="value1"/> and <paramref name="value2"/>.
        /// </summary>
        /// <param name="value1">The first vector to add.</param>
        /// <param name="value2">The second vector to add.</param>
        /// <returns>The result of the vector addition.</returns>
        public static Vector4 Add(Vector4 value1, Vector4 value2)
        {
            value1.X += value2.X;
            value1.Y += value2.Y;
            value1.Z += value2.Z;
            value1.W += value2.W;
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
        public static void Add(ref Vector4 value1, ref Vector4 value2, out Vector4 result)
        {
            result.X = value1.X + value2.X;
            result.Y = value1.Y + value2.Y;
            result.Z = value1.Z + value2.Z;
            result.W = value1.W + value2.W;
        }

        /// <summary>
        /// Creates a new <see cref="Vector4"/> that contains the cartesian coordinates of a vector specified in barycentric coordinates and relative to 4d-triangle.
        /// </summary>
        /// <param name="value1">The first vector of 4d-triangle.</param>
        /// <param name="value2">The second vector of 4d-triangle.</param>
        /// <param name="value3">The third vector of 4d-triangle.</param>
        /// <param name="amount1">Barycentric scalar <c>b2</c> which represents a weighting factor towards second vector of 4d-triangle.</param>
        /// <param name="amount2">Barycentric scalar <c>b3</c> which represents a weighting factor towards third vector of 4d-triangle.</param>
        /// <returns>The cartesian translation of barycentric coordinates.</returns>
        public static Vector4 Barycentric(Vector4 value1, Vector4 value2, Vector4 value3, Fixed amount1, Fixed amount2)
        {
            return new Vector4(
                Fixed.Barycentric(value1.X, value2.X, value3.X, amount1, amount2),
                Fixed.Barycentric(value1.Y, value2.Y, value3.Y, amount1, amount2),
                Fixed.Barycentric(value1.Z, value2.Z, value3.Z, amount1, amount2),
                Fixed.Barycentric(value1.W, value2.W, value3.W, amount1, amount2));
        }

        /// <summary>
        /// Creates a new <see cref="Vector4"/> that contains the cartesian coordinates of a vector specified in barycentric coordinates and relative to 4d-triangle.
        /// </summary>
        /// <param name="value1">The first vector of 4d-triangle.</param>
        /// <param name="value2">The second vector of 4d-triangle.</param>
        /// <param name="value3">The third vector of 4d-triangle.</param>
        /// <param name="amount1">Barycentric scalar <c>b2</c> which represents a weighting factor towards second vector of 4d-triangle.</param>
        /// <param name="amount2">Barycentric scalar <c>b3</c> which represents a weighting factor towards third vector of 4d-triangle.</param>
        /// <param name="result">The cartesian translation of barycentric coordinates as an output parameter.</param>
        public static void Barycentric(ref Vector4 value1, ref Vector4 value2, ref Vector4 value3, Fixed amount1, Fixed amount2, out Vector4 result)
        {
            result.X = Fixed.Barycentric(value1.X, value2.X, value3.X, amount1, amount2);
            result.Y = Fixed.Barycentric(value1.Y, value2.Y, value3.Y, amount1, amount2);
            result.Z = Fixed.Barycentric(value1.Z, value2.Z, value3.Z, amount1, amount2);
            result.W = Fixed.Barycentric(value1.W, value2.W, value3.W, amount1, amount2);
        }

        /// <summary>
        /// Creates a new <see cref="Vector4"/> that contains CatmullRom interpolation of the specified vectors.
        /// </summary>
        /// <param name="value1">The first vector in interpolation.</param>
        /// <param name="value2">The second vector in interpolation.</param>
        /// <param name="value3">The third vector in interpolation.</param>
        /// <param name="value4">The fourth vector in interpolation.</param>
        /// <param name="amount">Weighting factor.</param>
        /// <returns>The result of CatmullRom interpolation.</returns>
        public static Vector4 CatmullRom(Vector4 value1, Vector4 value2, Vector4 value3, Vector4 value4, Fixed amount)
        {
            return new Vector4(
                Fixed.CatmullRom(value1.X, value2.X, value3.X, value4.X, amount),
                Fixed.CatmullRom(value1.Y, value2.Y, value3.Y, value4.Y, amount),
                Fixed.CatmullRom(value1.Z, value2.Z, value3.Z, value4.Z, amount),
                Fixed.CatmullRom(value1.W, value2.W, value3.W, value4.W, amount));
        }

        /// <summary>
        /// Creates a new <see cref="Vector4"/> that contains CatmullRom interpolation of the specified vectors.
        /// </summary>
        /// <param name="value1">The first vector in interpolation.</param>
        /// <param name="value2">The second vector in interpolation.</param>
        /// <param name="value3">The third vector in interpolation.</param>
        /// <param name="value4">The fourth vector in interpolation.</param>
        /// <param name="amount">Weighting factor.</param>
        /// <param name="result">The result of CatmullRom interpolation as an output parameter.</param>
        public static void CatmullRom(ref Vector4 value1, ref Vector4 value2, ref Vector4 value3, ref Vector4 value4, Fixed amount, out Vector4 result)
        {
            result.X = Fixed.CatmullRom(value1.X, value2.X, value3.X, value4.X, amount);
            result.Y = Fixed.CatmullRom(value1.Y, value2.Y, value3.Y, value4.Y, amount);
            result.Z = Fixed.CatmullRom(value1.Z, value2.Z, value3.Z, value4.Z, amount);
            result.W = Fixed.CatmullRom(value1.W, value2.W, value3.W, value4.W, amount);
        }

        /// <summary>
        /// Round the members of this <see cref="Vector4"/> towards positive infinity.
        /// </summary>
        public void Ceiling()
        {
            X = Fixed.Ceiling(X);
            Y = Fixed.Ceiling(Y);
            Z = Fixed.Ceiling(Z);
            W = Fixed.Ceiling(W);
        }

        /// <summary>
        /// Creates a new <see cref="Vector4"/> that contains members from another vector rounded towards positive infinity.
        /// </summary>
        /// <param name="value">Source <see cref="Vector4"/>.</param>
        /// <returns>The rounded <see cref="Vector4"/>.</returns>
        public static Vector4 Ceiling(Vector4 value)
        {
            value.X = Fixed.Ceiling(value.X);
            value.Y = Fixed.Ceiling(value.Y);
            value.Z = Fixed.Ceiling(value.Z);
            value.W = Fixed.Ceiling(value.W);
            return value;
        }

        /// <summary>
        /// Creates a new <see cref="Vector4"/> that contains members from another vector rounded towards positive infinity.
        /// </summary>
        /// <param name="value">Source <see cref="Vector4"/>.</param>
        /// <param name="result">The rounded <see cref="Vector4"/>.</param>
        public static void Ceiling(ref Vector4 value, out Vector4 result)
        {
            result.X = Fixed.Ceiling(value.X);
            result.Y = Fixed.Ceiling(value.Y);
            result.Z = Fixed.Ceiling(value.Z);
            result.W = Fixed.Ceiling(value.W);
        }

        /// <summary>
        /// Clamps the specified value within a range.
        /// </summary>
        /// <param name="value1">The value to clamp.</param>
        /// <param name="min">The min value.</param>
        /// <param name="max">The max value.</param>
        /// <returns>The clamped value.</returns>
        public static Vector4 Clamp(Vector4 value1, Vector4 min, Vector4 max)
        {
            return new Vector4(
                Fixed.Clamp(value1.X, min.X, max.X),
                Fixed.Clamp(value1.Y, min.Y, max.Y),
                Fixed.Clamp(value1.Z, min.Z, max.Z),
                Fixed.Clamp(value1.W, min.W, max.W));
        }

        /// <summary>
        /// Clamps the specified value within a range.
        /// </summary>
        /// <param name="value1">The value to clamp.</param>
        /// <param name="min">The min value.</param>
        /// <param name="max">The max value.</param>
        /// <param name="result">The clamped value as an output parameter.</param>
        public static void Clamp(ref Vector4 value1, ref Vector4 min, ref Vector4 max, out Vector4 result)
        {
            result.X = Fixed.Clamp(value1.X, min.X, max.X);
            result.Y = Fixed.Clamp(value1.Y, min.Y, max.Y);
            result.Z = Fixed.Clamp(value1.Z, min.Z, max.Z);
            result.W = Fixed.Clamp(value1.W, min.W, max.W);
        }

        /// <summary>
        /// Returns the distance between two vectors.
        /// </summary>
        /// <param name="value1">The first vector.</param>
        /// <param name="value2">The second vector.</param>
        /// <returns>The distance between two vectors.</returns>
        public static Fixed Distance(Vector4 value1, Vector4 value2)
        {
            return Fixed.Sqrt(DistanceSquared(value1, value2));
        }

        /// <summary>
        /// Returns the distance between two vectors.
        /// </summary>
        /// <param name="value1">The first vector.</param>
        /// <param name="value2">The second vector.</param>
        /// <param name="result">The distance between two vectors as an output parameter.</param>
        public static void Distance(ref Vector4 value1, ref Vector4 value2, out Fixed result)
        {
            result = Fixed.Sqrt(DistanceSquared(value1, value2));
        }

        /// <summary>
        /// Returns the squared distance between two vectors.
        /// </summary>
        /// <param name="value1">The first vector.</param>
        /// <param name="value2">The second vector.</param>
        /// <returns>The squared distance between two vectors.</returns>
        public static Fixed DistanceSquared(Vector4 value1, Vector4 value2)
        {
              return (value1.W - value2.W) * (value1.W - value2.W) +
                     (value1.X - value2.X) * (value1.X - value2.X) +
                     (value1.Y - value2.Y) * (value1.Y - value2.Y) +
                     (value1.Z - value2.Z) * (value1.Z - value2.Z);
        }

        /// <summary>
        /// Returns the squared distance between two vectors.
        /// </summary>
        /// <param name="value1">The first vector.</param>
        /// <param name="value2">The second vector.</param>
        /// <param name="result">The squared distance between two vectors as an output parameter.</param>
        public static void DistanceSquared(ref Vector4 value1, ref Vector4 value2, out Fixed result)
        {
            result = (value1.W - value2.W) * (value1.W - value2.W) +
                     (value1.X - value2.X) * (value1.X - value2.X) +
                     (value1.Y - value2.Y) * (value1.Y - value2.Y) +
                     (value1.Z - value2.Z) * (value1.Z - value2.Z);
        }

        /// <summary>
        /// Divides the components of a <see cref="Vector4"/> by the components of another <see cref="Vector4"/>.
        /// </summary>
        /// <param name="value1">Source <see cref="Vector4"/>.</param>
        /// <param name="value2">Divisor <see cref="Vector4"/>.</param>
        /// <returns>The result of dividing the vectors.</returns>
        public static Vector4 Divide(Vector4 value1, Vector4 value2)
        {
            value1.W /= value2.W;
            value1.X /= value2.X;
            value1.Y /= value2.Y;
            value1.Z /= value2.Z;
            return value1;
        }

        /// <summary>
        /// Divides the components of a <see cref="Vector4"/> by a scalar.
        /// </summary>
        /// <param name="value1">Source <see cref="Vector4"/>.</param>
        /// <param name="divider">Divisor scalar.</param>
        /// <returns>The result of dividing a vector by a scalar.</returns>
        public static Vector4 Divide(Vector4 value1, Fixed divider)
        {
            Fixed factor = Fixed.One / divider;
            value1.W *= factor;
            value1.X *= factor;
            value1.Y *= factor;
            value1.Z *= factor;
            return value1;
        }

        /// <summary>
        /// Divides the components of a <see cref="Vector4"/> by a scalar.
        /// </summary>
        /// <param name="value1">Source <see cref="Vector4"/>.</param>
        /// <param name="divider">Divisor scalar.</param>
        /// <param name="result">The result of dividing a vector by a scalar as an output parameter.</param>
        public static void Divide(ref Vector4 value1, Fixed divider, out Vector4 result)
        {
            Fixed factor = Fixed.One / divider;
            result.W = value1.W * factor;
            result.X = value1.X * factor;
            result.Y = value1.Y * factor;
            result.Z = value1.Z * factor;
        }

        /// <summary>
        /// Divides the components of a <see cref="Vector4"/> by the components of another <see cref="Vector4"/>.
        /// </summary>
        /// <param name="value1">Source <see cref="Vector4"/>.</param>
        /// <param name="value2">Divisor <see cref="Vector4"/>.</param>
        /// <param name="result">The result of dividing the vectors as an output parameter.</param>
        public static void Divide(ref Vector4 value1, ref Vector4 value2, out Vector4 result)
        {
            result.W = value1.W / value2.W;
            result.X = value1.X / value2.X;
            result.Y = value1.Y / value2.Y;
            result.Z = value1.Z / value2.Z;
        }

        /// <summary>
        /// Returns a dot product of two vectors.
        /// </summary>
        /// <param name="value1">The first vector.</param>
        /// <param name="value2">The second vector.</param>
        /// <returns>The dot product of two vectors.</returns>
        public static Fixed Dot(Vector4 value1, Vector4 value2)
        {
            return value1.X * value2.X + value1.Y * value2.Y + value1.Z * value2.Z + value1.W * value2.W;
        }

        /// <summary>
        /// Returns a dot product of two vectors.
        /// </summary>
        /// <param name="value1">The first vector.</param>
        /// <param name="value2">The second vector.</param>
        /// <param name="result">The dot product of two vectors as an output parameter.</param>
        public static void Dot(ref Vector4 value1, ref Vector4 value2, out Fixed result)
        {
            result = value1.X * value2.X + value1.Y * value2.Y + value1.Z * value2.Z + value1.W * value2.W;
        }

        /// <summary>
        /// Compares whether current instance is equal to specified <see cref="Object"/>.
        /// </summary>
        /// <param name="obj">The <see cref="Object"/> to compare.</param>
        /// <returns><c>true</c> if the instances are equal; <c>false</c> otherwise.</returns>
        public override bool Equals(object obj)
        {
            return (obj is Vector4) ? this == (Vector4)obj : false;
        }

        /// <summary>
        /// Compares whether current instance is equal to specified <see cref="Vector4"/>.
        /// </summary>
        /// <param name="other">The <see cref="Vector4"/> to compare.</param>
        /// <returns><c>true</c> if the instances are equal; <c>false</c> otherwise.</returns>
        public bool Equals(Vector4 other)
        {
            return this.W == other.W
                && this.X == other.X
                && this.Y == other.Y
                && this.Z == other.Z;
        }

        /// <summary>
        /// Round the members of this <see cref="Vector4"/> towards negative infinity.
        /// </summary>
        public void Floor()
        {
            X = Fixed.Floor(X);
            Y = Fixed.Floor(Y);
            Z = Fixed.Floor(Z);
            W = Fixed.Floor(W);
        }

        /// <summary>
        /// Creates a new <see cref="Vector4"/> that contains members from another vector rounded towards negative infinity.
        /// </summary>
        /// <param name="value">Source <see cref="Vector4"/>.</param>
        /// <returns>The rounded <see cref="Vector4"/>.</returns>
        public static Vector4 Floor(Vector4 value)
        {
            value.X = Fixed.Floor(value.X);
            value.Y = Fixed.Floor(value.Y);
            value.Z = Fixed.Floor(value.Z);
            value.W = Fixed.Floor(value.W);
            return value;
        }

        /// <summary>
        /// Creates a new <see cref="Vector4"/> that contains members from another vector rounded towards negative infinity.
        /// </summary>
        /// <param name="value">Source <see cref="Vector4"/>.</param>
        /// <param name="result">The rounded <see cref="Vector4"/>.</param>
        public static void Floor(ref Vector4 value, out Vector4 result)
        {
            result.X = Fixed.Floor(value.X);
            result.Y = Fixed.Floor(value.Y);
            result.Z = Fixed.Floor(value.Z);
            result.W = Fixed.Floor(value.W);
        }

        /// <summary>
        /// Gets the hash code of this <see cref="Vector4"/>.
        /// </summary>
        /// <returns>Hash code of this <see cref="Vector4"/>.</returns>
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = W.GetHashCode();
                hashCode = (hashCode * 397) ^ X.GetHashCode();
                hashCode = (hashCode * 397) ^ Y.GetHashCode();
                hashCode = (hashCode * 397) ^ Z.GetHashCode();
                return hashCode;
            }
        }

        /// <summary>
        /// Creates a new <see cref="Vector4"/> that contains hermite spline interpolation.
        /// </summary>
        /// <param name="value1">The first position vector.</param>
        /// <param name="tangent1">The first tangent vector.</param>
        /// <param name="value2">The second position vector.</param>
        /// <param name="tangent2">The second tangent vector.</param>
        /// <param name="amount">Weighting factor.</param>
        /// <returns>The hermite spline interpolation vector.</returns>
        public static Vector4 Hermite(Vector4 value1, Vector4 tangent1, Vector4 value2, Vector4 tangent2, Fixed amount)
        {
            return new Vector4(Fixed.Hermite(value1.X, tangent1.X, value2.X, tangent2.X, amount),
                               Fixed.Hermite(value1.Y, tangent1.Y, value2.Y, tangent2.Y, amount),
                               Fixed.Hermite(value1.Z, tangent1.Z, value2.Z, tangent2.Z, amount),
                               Fixed.Hermite(value1.W, tangent1.W, value2.W, tangent2.W, amount));
        }

        /// <summary>
        /// Creates a new <see cref="Vector4"/> that contains hermite spline interpolation.
        /// </summary>
        /// <param name="value1">The first position vector.</param>
        /// <param name="tangent1">The first tangent vector.</param>
        /// <param name="value2">The second position vector.</param>
        /// <param name="tangent2">The second tangent vector.</param>
        /// <param name="amount">Weighting factor.</param>
        /// <param name="result">The hermite spline interpolation vector as an output parameter.</param>
        public static void Hermite(ref Vector4 value1, ref Vector4 tangent1, ref Vector4 value2, ref Vector4 tangent2, Fixed amount, out Vector4 result)
        {
            result.W = Fixed.Hermite(value1.W, tangent1.W, value2.W, tangent2.W, amount);
            result.X = Fixed.Hermite(value1.X, tangent1.X, value2.X, tangent2.X, amount);
            result.Y = Fixed.Hermite(value1.Y, tangent1.Y, value2.Y, tangent2.Y, amount);
            result.Z = Fixed.Hermite(value1.Z, tangent1.Z, value2.Z, tangent2.Z, amount);
        }

        /// <summary>
        /// Returns the length of this <see cref="Vector4"/>.
        /// </summary>
        /// <returns>The length of this <see cref="Vector4"/>.</returns>
        public Fixed Length()
        {
            return Fixed.Sqrt((X * X) + (Y * Y) + (Z * Z) + (W * W));
        }

        /// <summary>
        /// Returns the squared length of this <see cref="Vector4"/>.
        /// </summary>
        /// <returns>The squared length of this <see cref="Vector4"/>.</returns>
        public Fixed LengthSquared()
        {
            return (X * X) + (Y * Y) + (Z * Z) + (W * W);
        }

        /// <summary>
        /// Creates a new <see cref="Vector4"/> that contains linear interpolation of the specified vectors.
        /// </summary>
        /// <param name="value1">The first vector.</param>
        /// <param name="value2">The second vector.</param>
        /// <param name="amount">Weighting value(between 0.0 and 1.0).</param>
        /// <returns>The result of linear interpolation of the specified vectors.</returns>
        public static Vector4 Lerp(Vector4 value1, Vector4 value2, Fixed amount)
        {
            return new Vector4(
                Fixed.Lerp(value1.X, value2.X, amount),
                Fixed.Lerp(value1.Y, value2.Y, amount),
                Fixed.Lerp(value1.Z, value2.Z, amount),
                Fixed.Lerp(value1.W, value2.W, amount));
        }

        /// <summary>
        /// Creates a new <see cref="Vector4"/> that contains linear interpolation of the specified vectors.
        /// </summary>
        /// <param name="value1">The first vector.</param>
        /// <param name="value2">The second vector.</param>
        /// <param name="amount">Weighting value(between 0.0 and 1.0).</param>
        /// <param name="result">The result of linear interpolation of the specified vectors as an output parameter.</param>
        public static void Lerp(ref Vector4 value1, ref Vector4 value2, Fixed amount, out Vector4 result)
        {
            result.X = Fixed.Lerp(value1.X, value2.X, amount);
            result.Y = Fixed.Lerp(value1.Y, value2.Y, amount);
            result.Z = Fixed.Lerp(value1.Z, value2.Z, amount);
            result.W = Fixed.Lerp(value1.W, value2.W, amount);
        }

        /// <summary>
        /// Creates a new <see cref="Vector4"/> that contains linear interpolation of the specified vectors.
        /// Uses <see cref="Fixed.LerpPrecise"/> on MathHelper for the interpolation.
        /// Less efficient but more precise compared to <see cref="Vector4.Lerp(Vector4, Vector4, Fixed)"/>.
        /// See remarks section of <see cref="Fixed.LerpPrecise"/> on MathHelper for more info.
        /// </summary>
        /// <param name="value1">The first vector.</param>
        /// <param name="value2">The second vector.</param>
        /// <param name="amount">Weighting value(between 0.0 and 1.0).</param>
        /// <returns>The result of linear interpolation of the specified vectors.</returns>
        public static Vector4 LerpPrecise(Vector4 value1, Vector4 value2, Fixed amount)
        {
            return new Vector4(
                Fixed.LerpPrecise(value1.X, value2.X, amount),
                Fixed.LerpPrecise(value1.Y, value2.Y, amount),
                Fixed.LerpPrecise(value1.Z, value2.Z, amount),
                Fixed.LerpPrecise(value1.W, value2.W, amount));
        }

        /// <summary>
        /// Creates a new <see cref="Vector4"/> that contains linear interpolation of the specified vectors.
        /// Uses <see cref="Fixed.LerpPrecise"/> on MathHelper for the interpolation.
        /// Less efficient but more precise compared to <see cref="Vector4.Lerp(ref Vector4, ref Vector4, Fixed, out Vector4)"/>.
        /// See remarks section of <see cref="Fixed.LerpPrecise"/> on MathHelper for more info.
        /// </summary>
        /// <param name="value1">The first vector.</param>
        /// <param name="value2">The second vector.</param>
        /// <param name="amount">Weighting value(between 0.0 and 1.0).</param>
        /// <param name="result">The result of linear interpolation of the specified vectors as an output parameter.</param>
        public static void LerpPrecise(ref Vector4 value1, ref Vector4 value2, Fixed amount, out Vector4 result)
        {
            result.X = Fixed.LerpPrecise(value1.X, value2.X, amount);
            result.Y = Fixed.LerpPrecise(value1.Y, value2.Y, amount);
            result.Z = Fixed.LerpPrecise(value1.Z, value2.Z, amount);
            result.W = Fixed.LerpPrecise(value1.W, value2.W, amount);
        }

        /// <summary>
        /// Creates a new <see cref="Vector4"/> that contains a maximal values from the two vectors.
        /// </summary>
        /// <param name="value1">The first vector.</param>
        /// <param name="value2">The second vector.</param>
        /// <returns>The <see cref="Vector4"/> with maximal values from the two vectors.</returns>
        public static Vector4 Max(Vector4 value1, Vector4 value2)
        {
            return new Vector4(
               Fixed.Max(value1.X, value2.X),
               Fixed.Max(value1.Y, value2.Y),
               Fixed.Max(value1.Z, value2.Z),
               Fixed.Max(value1.W, value2.W));
        }

        /// <summary>
        /// Creates a new <see cref="Vector4"/> that contains a maximal values from the two vectors.
        /// </summary>
        /// <param name="value1">The first vector.</param>
        /// <param name="value2">The second vector.</param>
        /// <param name="result">The <see cref="Vector4"/> with maximal values from the two vectors as an output parameter.</param>
        public static void Max(ref Vector4 value1, ref Vector4 value2, out Vector4 result)
        {
            result.X = Fixed.Max(value1.X, value2.X);
            result.Y = Fixed.Max(value1.Y, value2.Y);
            result.Z = Fixed.Max(value1.Z, value2.Z);
            result.W = Fixed.Max(value1.W, value2.W);
        }

        /// <summary>
        /// Creates a new <see cref="Vector4"/> that contains a minimal values from the two vectors.
        /// </summary>
        /// <param name="value1">The first vector.</param>
        /// <param name="value2">The second vector.</param>
        /// <returns>The <see cref="Vector4"/> with minimal values from the two vectors.</returns>
        public static Vector4 Min(Vector4 value1, Vector4 value2)
        {
            return new Vector4(
               Fixed.Min(value1.X, value2.X),
               Fixed.Min(value1.Y, value2.Y),
               Fixed.Min(value1.Z, value2.Z),
               Fixed.Min(value1.W, value2.W));
        }

        /// <summary>
        /// Creates a new <see cref="Vector4"/> that contains a minimal values from the two vectors.
        /// </summary>
        /// <param name="value1">The first vector.</param>
        /// <param name="value2">The second vector.</param>
        /// <param name="result">The <see cref="Vector4"/> with minimal values from the two vectors as an output parameter.</param>
        public static void Min(ref Vector4 value1, ref Vector4 value2, out Vector4 result)
        {
            result.X = Fixed.Min(value1.X, value2.X);
            result.Y = Fixed.Min(value1.Y, value2.Y);
            result.Z = Fixed.Min(value1.Z, value2.Z);
            result.W = Fixed.Min(value1.W, value2.W);
        }

        /// <summary>
        /// Creates a new <see cref="Vector4"/> that contains a multiplication of two vectors.
        /// </summary>
        /// <param name="value1">Source <see cref="Vector4"/>.</param>
        /// <param name="value2">Source <see cref="Vector4"/>.</param>
        /// <returns>The result of the vector multiplication.</returns>
        public static Vector4 Multiply(Vector4 value1, Vector4 value2)
        {
            value1.W *= value2.W;
            value1.X *= value2.X;
            value1.Y *= value2.Y;
            value1.Z *= value2.Z;
            return value1;
        }

        /// <summary>
        /// Creates a new <see cref="Vector4"/> that contains a multiplication of <see cref="Vector4"/> and a scalar.
        /// </summary>
        /// <param name="value1">Source <see cref="Vector4"/>.</param>
        /// <param name="scaleFactor">Scalar value.</param>
        /// <returns>The result of the vector multiplication with a scalar.</returns>
        public static Vector4 Multiply(Vector4 value1, Fixed scaleFactor)
        {
            value1.W *= scaleFactor;
            value1.X *= scaleFactor;
            value1.Y *= scaleFactor;
            value1.Z *= scaleFactor;
            return value1;
        }

        /// <summary>
        /// Creates a new <see cref="Vector4"/> that contains a multiplication of <see cref="Vector4"/> and a scalar.
        /// </summary>
        /// <param name="value1">Source <see cref="Vector4"/>.</param>
        /// <param name="scaleFactor">Scalar value.</param>
        /// <param name="result">The result of the multiplication with a scalar as an output parameter.</param>
        public static void Multiply(ref Vector4 value1, Fixed scaleFactor, out Vector4 result)
        {
            result.W = value1.W * scaleFactor;
            result.X = value1.X * scaleFactor;
            result.Y = value1.Y * scaleFactor;
            result.Z = value1.Z * scaleFactor;
        }

        /// <summary>
        /// Creates a new <see cref="Vector4"/> that contains a multiplication of two vectors.
        /// </summary>
        /// <param name="value1">Source <see cref="Vector4"/>.</param>
        /// <param name="value2">Source <see cref="Vector4"/>.</param>
        /// <param name="result">The result of the vector multiplication as an output parameter.</param>
        public static void Multiply(ref Vector4 value1, ref Vector4 value2, out Vector4 result)
        {
            result.W = value1.W * value2.W;
            result.X = value1.X * value2.X;
            result.Y = value1.Y * value2.Y;
            result.Z = value1.Z * value2.Z;
        }

        /// <summary>
        /// Creates a new <see cref="Vector4"/> that contains the specified vector inversion.
        /// </summary>
        /// <param name="value">Source <see cref="Vector4"/>.</param>
        /// <returns>The result of the vector inversion.</returns>
        public static Vector4 Negate(Vector4 value)
        {
            value = new Vector4(-value.X, -value.Y, -value.Z, -value.W);
            return value;
        }

        /// <summary>
        /// Creates a new <see cref="Vector4"/> that contains the specified vector inversion.
        /// </summary>
        /// <param name="value">Source <see cref="Vector4"/>.</param>
        /// <param name="result">The result of the vector inversion as an output parameter.</param>
        public static void Negate(ref Vector4 value, out Vector4 result)
        {
            result.X = -value.X;
            result.Y = -value.Y;
            result.Z = -value.Z;
            result.W = -value.W;
        }

        /// <summary>
        /// Turns this <see cref="Vector4"/> to a unit vector with the same direction.
        /// </summary>
        public void Normalize()
        {
            Fixed factor = Fixed.Sqrt((X * X) + (Y * Y) + (Z * Z) + (W * W));
            factor = Fixed.One / factor;
            X *= factor;
            Y *= factor;
            Z *= factor;
            W *= factor;
        }

        /// <summary>
        /// Creates a new <see cref="Vector4"/> that contains a normalized values from another vector.
        /// </summary>
        /// <param name="value">Source <see cref="Vector4"/>.</param>
        /// <returns>Unit vector.</returns>
        public static Vector4 Normalize(Vector4 value)
        {
            Fixed factor = Fixed.Sqrt((value.X * value.X) + (value.Y * value.Y) + (value.Z * value.Z) + (value.W * value.W));
            factor = Fixed.One / factor;
            return new Vector4(value.X*factor,value.Y*factor,value.Z*factor,value.W*factor);
        }

        /// <summary>
        /// Creates a new <see cref="Vector4"/> that contains a normalized values from another vector.
        /// </summary>
        /// <param name="value">Source <see cref="Vector4"/>.</param>
        /// <param name="result">Unit vector as an output parameter.</param>
        public static void Normalize(ref Vector4 value, out Vector4 result)
        {
            Fixed factor = Fixed.Sqrt((value.X * value.X) + (value.Y * value.Y) + (value.Z * value.Z) + (value.W * value.W));
            factor = Fixed.One / factor;
            result.W = value.W * factor;
            result.X = value.X * factor;
            result.Y = value.Y * factor;
            result.Z = value.Z * factor;
        }

        /// <summary>
        /// Round the members of this <see cref="Vector4"/> to the nearest integer value.
        /// </summary>
        public void Round()
        {
            X = Fixed.Round(X);
            Y = Fixed.Round(Y);
            Z = Fixed.Round(Z);
            W = Fixed.Round(W);
        }

        /// <summary>
        /// Creates a new <see cref="Vector4"/> that contains members from another vector rounded to the nearest integer value.
        /// </summary>
        /// <param name="value">Source <see cref="Vector4"/>.</param>
        /// <returns>The rounded <see cref="Vector4"/>.</returns>
        public static Vector4 Round(Vector4 value)
        {
            value.X = Fixed.Round(value.X);
            value.Y = Fixed.Round(value.Y);
            value.Z = Fixed.Round(value.Z);
            value.W = Fixed.Round(value.W);
            return value;
        }

        /// <summary>
        /// Creates a new <see cref="Vector4"/> that contains members from another vector rounded to the nearest integer value.
        /// </summary>
        /// <param name="value">Source <see cref="Vector4"/>.</param>
        /// <param name="result">The rounded <see cref="Vector4"/>.</param>
        public static void Round(ref Vector4 value, out Vector4 result)
        {
            result.X = Fixed.Round(value.X);
            result.Y = Fixed.Round(value.Y);
            result.Z = Fixed.Round(value.Z);
            result.W = Fixed.Round(value.W);
        }

        /// <summary>
        /// Creates a new <see cref="Vector4"/> that contains cubic interpolation of the specified vectors.
        /// </summary>
        /// <param name="value1">Source <see cref="Vector4"/>.</param>
        /// <param name="value2">Source <see cref="Vector4"/>.</param>
        /// <param name="amount">Weighting value.</param>
        /// <returns>Cubic interpolation of the specified vectors.</returns>
        public static Vector4 SmoothStep(Vector4 value1, Vector4 value2, Fixed amount)
        {
            return new Vector4(
                Fixed.SmoothStep(value1.X, value2.X, amount),
                Fixed.SmoothStep(value1.Y, value2.Y, amount),
                Fixed.SmoothStep(value1.Z, value2.Z, amount),
                Fixed.SmoothStep(value1.W, value2.W, amount));
        }

        /// <summary>
        /// Creates a new <see cref="Vector4"/> that contains cubic interpolation of the specified vectors.
        /// </summary>
        /// <param name="value1">Source <see cref="Vector4"/>.</param>
        /// <param name="value2">Source <see cref="Vector4"/>.</param>
        /// <param name="amount">Weighting value.</param>
        /// <param name="result">Cubic interpolation of the specified vectors as an output parameter.</param>
        public static void SmoothStep(ref Vector4 value1, ref Vector4 value2, Fixed amount, out Vector4 result)
        {
            result.X = Fixed.SmoothStep(value1.X, value2.X, amount);
            result.Y = Fixed.SmoothStep(value1.Y, value2.Y, amount);
            result.Z = Fixed.SmoothStep(value1.Z, value2.Z, amount);
            result.W = Fixed.SmoothStep(value1.W, value2.W, amount);
        }

        /// <summary>
        /// Creates a new <see cref="Vector4"/> that contains subtraction of on <see cref="Vector4"/> from a another.
        /// </summary>
        /// <param name="value1">Source <see cref="Vector4"/>.</param>
        /// <param name="value2">Source <see cref="Vector4"/>.</param>
        /// <returns>The result of the vector subtraction.</returns>
        public static Vector4 Subtract(Vector4 value1, Vector4 value2)
        {
            value1.W -= value2.W;
            value1.X -= value2.X;
            value1.Y -= value2.Y;
            value1.Z -= value2.Z;
            return value1;
        }

        /// <summary>
        /// Creates a new <see cref="Vector4"/> that contains subtraction of on <see cref="Vector4"/> from a another.
        /// </summary>
        /// <param name="value1">Source <see cref="Vector4"/>.</param>
        /// <param name="value2">Source <see cref="Vector4"/>.</param>
        /// <param name="result">The result of the vector subtraction as an output parameter.</param>
        public static void Subtract(ref Vector4 value1, ref Vector4 value2, out Vector4 result)
        {
            result.W = value1.W - value2.W;
            result.X = value1.X - value2.X;
            result.Y = value1.Y - value2.Y;
            result.Z = value1.Z - value2.Z;
        }

        #region Transform

        /// <summary>
        /// Creates a new <see cref="Vector4"/> that contains a transformation of 2d-vector by the specified <see cref="Matrix"/>.
        /// </summary>
        /// <param name="value">Source <see cref="Vector2"/>.</param>
        /// <param name="matrix">The transformation <see cref="Matrix"/>.</param>
        /// <returns>Transformed <see cref="Vector4"/>.</returns>
        public static Vector4 Transform(Vector2 value, Matrix matrix)
        {
            Vector4 result;
            Transform(ref value, ref matrix, out result);
            return result;
        }

        /// <summary>
        /// Creates a new <see cref="Vector4"/> that contains a transformation of 2d-vector by the specified <see cref="Quaternion"/>.
        /// </summary>
        /// <param name="value">Source <see cref="Vector2"/>.</param>
        /// <param name="rotation">The <see cref="Quaternion"/> which contains rotation transformation.</param>
        /// <returns>Transformed <see cref="Vector4"/>.</returns>
        public static Vector4 Transform(Vector2 value, Quaternion rotation)
        {
            Vector4 result;
            Transform(ref value, ref rotation, out result);
            return result;
        }

        /// <summary>
        /// Creates a new <see cref="Vector4"/> that contains a transformation of 3d-vector by the specified <see cref="Matrix"/>.
        /// </summary>
        /// <param name="value">Source <see cref="Vector3"/>.</param>
        /// <param name="matrix">The transformation <see cref="Matrix"/>.</param>
        /// <returns>Transformed <see cref="Vector4"/>.</returns>
        public static Vector4 Transform(Vector3 value, Matrix matrix)
        {
            Vector4 result;
            Transform(ref value, ref matrix, out result);
            return result;
        }

        /// <summary>
        /// Creates a new <see cref="Vector4"/> that contains a transformation of 3d-vector by the specified <see cref="Quaternion"/>.
        /// </summary>
        /// <param name="value">Source <see cref="Vector3"/>.</param>
        /// <param name="rotation">The <see cref="Quaternion"/> which contains rotation transformation.</param>
        /// <returns>Transformed <see cref="Vector4"/>.</returns>
        public static Vector4 Transform(Vector3 value, Quaternion rotation)
        {
            Vector4 result;
            Transform(ref value, ref rotation, out result);
            return result;
        }

        /// <summary>
        /// Creates a new <see cref="Vector4"/> that contains a transformation of 4d-vector by the specified <see cref="Matrix"/>.
        /// </summary>
        /// <param name="value">Source <see cref="Vector4"/>.</param>
        /// <param name="matrix">The transformation <see cref="Matrix"/>.</param>
        /// <returns>Transformed <see cref="Vector4"/>.</returns>
        public static Vector4 Transform(Vector4 value, Matrix matrix)
        {
            Transform(ref value, ref matrix, out value);
            return value;
        }

        /// <summary>
        /// Creates a new <see cref="Vector4"/> that contains a transformation of 4d-vector by the specified <see cref="Quaternion"/>.
        /// </summary>
        /// <param name="value">Source <see cref="Vector4"/>.</param>
        /// <param name="rotation">The <see cref="Quaternion"/> which contains rotation transformation.</param>
        /// <returns>Transformed <see cref="Vector4"/>.</returns>
        public static Vector4 Transform(Vector4 value, Quaternion rotation)
        {
            Vector4 result;
            Transform(ref value, ref rotation, out result);
            return result;
        }

        /// <summary>
        /// Creates a new <see cref="Vector4"/> that contains a transformation of 2d-vector by the specified <see cref="Matrix"/>.
        /// </summary>
        /// <param name="value">Source <see cref="Vector2"/>.</param>
        /// <param name="matrix">The transformation <see cref="Matrix"/>.</param>
        /// <param name="result">Transformed <see cref="Vector4"/> as an output parameter.</param>
        public static void Transform(ref Vector2 value, ref Matrix matrix, out Vector4 result)
        {
            result.X = (value.X * matrix.M11) + (value.Y * matrix.M21) + matrix.M41;
            result.Y = (value.X * matrix.M12) + (value.Y * matrix.M22) + matrix.M42;
            result.Z = (value.X * matrix.M13) + (value.Y * matrix.M23) + matrix.M43;
            result.W = (value.X * matrix.M14) + (value.Y * matrix.M24) + matrix.M44;
        }

        /// <summary>
        /// Creates a new <see cref="Vector4"/> that contains a transformation of 2d-vector by the specified <see cref="Quaternion"/>.
        /// </summary>
        /// <param name="value">Source <see cref="Vector2"/>.</param>
        /// <param name="rotation">The <see cref="Quaternion"/> which contains rotation transformation.</param>
        /// <param name="result">Transformed <see cref="Vector4"/> as an output parameter.</param>
        public static void Transform(ref Vector2 value, ref Quaternion rotation, out Vector4 result)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Creates a new <see cref="Vector4"/> that contains a transformation of 3d-vector by the specified <see cref="Matrix"/>.
        /// </summary>
        /// <param name="value">Source <see cref="Vector3"/>.</param>
        /// <param name="matrix">The transformation <see cref="Matrix"/>.</param>
        /// <param name="result">Transformed <see cref="Vector4"/> as an output parameter.</param>
        public static void Transform(ref Vector3 value, ref Matrix matrix, out Vector4 result)
        {
            result.X = (value.X * matrix.M11) + (value.Y * matrix.M21) + (value.Z * matrix.M31) + matrix.M41;
            result.Y = (value.X * matrix.M12) + (value.Y * matrix.M22) + (value.Z * matrix.M32) + matrix.M42;
            result.Z = (value.X * matrix.M13) + (value.Y * matrix.M23) + (value.Z * matrix.M33) + matrix.M43;
            result.W = (value.X * matrix.M14) + (value.Y * matrix.M24) + (value.Z * matrix.M34) + matrix.M44;
        }

        /// <summary>
        /// Creates a new <see cref="Vector4"/> that contains a transformation of 3d-vector by the specified <see cref="Quaternion"/>.
        /// </summary>
        /// <param name="value">Source <see cref="Vector3"/>.</param>
        /// <param name="rotation">The <see cref="Quaternion"/> which contains rotation transformation.</param>
        /// <param name="result">Transformed <see cref="Vector4"/> as an output parameter.</param>
        public static void Transform(ref Vector3 value, ref Quaternion rotation, out Vector4 result)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Creates a new <see cref="Vector4"/> that contains a transformation of 4d-vector by the specified <see cref="Matrix"/>.
        /// </summary>
        /// <param name="value">Source <see cref="Vector4"/>.</param>
        /// <param name="matrix">The transformation <see cref="Matrix"/>.</param>
        /// <param name="result">Transformed <see cref="Vector4"/> as an output parameter.</param>
        public static void Transform(ref Vector4 value, ref Matrix matrix, out Vector4 result)
        {
            var x = (value.X * matrix.M11) + (value.Y * matrix.M21) + (value.Z * matrix.M31) + (value.W * matrix.M41);
            var y = (value.X * matrix.M12) + (value.Y * matrix.M22) + (value.Z * matrix.M32) + (value.W * matrix.M42);
            var z = (value.X * matrix.M13) + (value.Y * matrix.M23) + (value.Z * matrix.M33) + (value.W * matrix.M43);
            var w = (value.X * matrix.M14) + (value.Y * matrix.M24) + (value.Z * matrix.M34) + (value.W * matrix.M44);
            result.X = x;
            result.Y = y;
            result.Z = z;
            result.W = w;
        }

        /// <summary>
        /// Creates a new <see cref="Vector4"/> that contains a transformation of 4d-vector by the specified <see cref="Quaternion"/>.
        /// </summary>
        /// <param name="value">Source <see cref="Vector4"/>.</param>
        /// <param name="rotation">The <see cref="Quaternion"/> which contains rotation transformation.</param>
        /// <param name="result">Transformed <see cref="Vector4"/> as an output parameter.</param>
        public static void Transform(ref Vector4 value, ref Quaternion rotation, out Vector4 result)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Apply transformation on vectors within array of <see cref="Vector4"/> by the specified <see cref="Matrix"/> and places the results in an another array.
        /// </summary>
        /// <param name="sourceArray">Source array.</param>
        /// <param name="sourceIndex">The starting index of transformation in the source array.</param>
        /// <param name="matrix">The transformation <see cref="Matrix"/>.</param>
        /// <param name="destinationArray">Destination array.</param>
        /// <param name="destinationIndex">The starting index in the destination array, where the first <see cref="Vector4"/> should be written.</param>
        /// <param name="length">The number of vectors to be transformed.</param>
        public static void Transform
        (
            Vector4[] sourceArray,
            int sourceIndex,
            ref Matrix matrix,
            Vector4[] destinationArray,
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

            for (var i = 0; i < length; i++)
            {
                var value = sourceArray[sourceIndex + i];
                destinationArray[destinationIndex + i] = Transform(value, matrix);
            }
        }

        /// <summary>
        /// Apply transformation on vectors within array of <see cref="Vector4"/> by the specified <see cref="Quaternion"/> and places the results in an another array.
        /// </summary>
        /// <param name="sourceArray">Source array.</param>
        /// <param name="sourceIndex">The starting index of transformation in the source array.</param>
        /// <param name="rotation">The <see cref="Quaternion"/> which contains rotation transformation.</param>
        /// <param name="destinationArray">Destination array.</param>
        /// <param name="destinationIndex">The starting index in the destination array, where the first <see cref="Vector4"/> should be written.</param>
        /// <param name="length">The number of vectors to be transformed.</param>
        public static void Transform(
            Vector4[] sourceArray,
            int sourceIndex,
            ref Quaternion rotation,
            Vector4[] destinationArray,
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

            for (var i = 0; i < length; i++)
            {
                var value = sourceArray[sourceIndex + i];
                destinationArray[destinationIndex + i] = Transform(value, rotation);
            }
        }

        /// <summary>
        /// Apply transformation on all vectors within array of <see cref="Vector4"/> by the specified <see cref="Matrix"/> and places the results in an another array.
        /// </summary>
        /// <param name="sourceArray">Source array.</param>
        /// <param name="matrix">The transformation <see cref="Matrix"/>.</param>
        /// <param name="destinationArray">Destination array.</param>
        public static void Transform(Vector4[] sourceArray, ref Matrix matrix, Vector4[] destinationArray)
        {
            if (sourceArray == null)
                throw new ArgumentNullException("sourceArray");
            if (destinationArray == null)
                throw new ArgumentNullException("destinationArray");
            if (destinationArray.Length < sourceArray.Length)
                throw new ArgumentException("Destination array length is lesser than source array length");

            for (var i = 0; i < sourceArray.Length; i++)
            {
                var value = sourceArray[i];
                destinationArray[i] = Transform(value, matrix);
            }
        }

        /// <summary>
        /// Apply transformation on all vectors within array of <see cref="Vector4"/> by the specified <see cref="Quaternion"/> and places the results in an another array.
        /// </summary>
        /// <param name="sourceArray">Source array.</param>
        /// <param name="rotation">The <see cref="Quaternion"/> which contains rotation transformation.</param>
        /// <param name="destinationArray">Destination array.</param>
        public static void Transform(Vector4[] sourceArray, ref Quaternion rotation, Vector4[] destinationArray)
        {
            if (sourceArray == null)
                throw new ArgumentNullException("sourceArray");
            if (destinationArray == null)
                throw new ArgumentNullException("destinationArray");
            if (destinationArray.Length < sourceArray.Length)
                throw new ArgumentException("Destination array length is lesser than source array length");

            for (var i = 0; i < sourceArray.Length; i++)
            {
                var value = sourceArray[i];
                destinationArray[i] = Transform(value, rotation);
            }
        }

        #endregion

        /// <summary>
        /// Returns a <see cref="String"/> representation of this <see cref="Vector4"/> in the format:
        /// {X:[<see cref="X"/>] Y:[<see cref="Y"/>] Z:[<see cref="Z"/>] W:[<see cref="W"/>]}
        /// </summary>
        /// <returns>A <see cref="String"/> representation of this <see cref="Vector4"/>.</returns>
        public override string ToString()
        {
            return "{X:" + X + " Y:" + Y + " Z:" + Z + " W:" + W + "}";
        }

        /// <summary>
        /// Deconstruction method for <see cref="Vector4"/>.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <param name="w"></param>
        public void Deconstruct(out Fixed x, out Fixed y, out Fixed z, out Fixed w)
        {
            x = X;
            y = Y;
            z = Z;
            w = W;
        }

        /// <summary>
        /// Returns a <see cref="System.Numerics.Vector4"/>.
        /// </summary>
        public System.Numerics.Vector4 ToNumerics()
        {
            return new System.Numerics.Vector4((float)this.X, (float)this.Y, (float)this.Z, (float)this.W);
        }

        #endregion

        #region Operators

        /// <summary>
        /// Converts a <see cref="System.Numerics.Vector4"/> to a <see cref="Vector4"/>.
        /// </summary>
        /// <param name="value">The converted value.</param>
        public static implicit operator Vector4(System.Numerics.Vector4 value)
        {
            return new Vector4((Fixed)value.X, (Fixed)value.Y, (Fixed)value.Z, (Fixed)value.W);
        }

        /// <summary>
        /// Inverts values in the specified <see cref="Vector4"/>.
        /// </summary>
        /// <param name="value">Source <see cref="Vector4"/> on the right of the sub sign.</param>
        /// <returns>Result of the inversion.</returns>
        public static Vector4 operator -(Vector4 value)
        {
            return new Vector4(-value.X, -value.Y, -value.Z, -value.W);
        }

        /// <summary>
        /// Compares whether two <see cref="Vector4"/> instances are equal.
        /// </summary>
        /// <param name="value1"><see cref="Vector4"/> instance on the left of the equal sign.</param>
        /// <param name="value2"><see cref="Vector4"/> instance on the right of the equal sign.</param>
        /// <returns><c>true</c> if the instances are equal; <c>false</c> otherwise.</returns>
        public static bool operator ==(Vector4 value1, Vector4 value2)
        {
            return value1.W == value2.W
                && value1.X == value2.X
                && value1.Y == value2.Y
                && value1.Z == value2.Z;
        }

        /// <summary>
        /// Compares whether two <see cref="Vector4"/> instances are not equal.
        /// </summary>
        /// <param name="value1"><see cref="Vector4"/> instance on the left of the not equal sign.</param>
        /// <param name="value2"><see cref="Vector4"/> instance on the right of the not equal sign.</param>
        /// <returns><c>true</c> if the instances are not equal; <c>false</c> otherwise.</returns>	
        public static bool operator !=(Vector4 value1, Vector4 value2)
        {
            return !(value1 == value2);
        }

        /// <summary>
        /// Adds two vectors.
        /// </summary>
        /// <param name="value1">Source <see cref="Vector4"/> on the left of the add sign.</param>
        /// <param name="value2">Source <see cref="Vector4"/> on the right of the add sign.</param>
        /// <returns>Sum of the vectors.</returns>
        public static Vector4 operator +(Vector4 value1, Vector4 value2)
        {
            value1.W += value2.W;
            value1.X += value2.X;
            value1.Y += value2.Y;
            value1.Z += value2.Z;
            return value1;
        }

        /// <summary>
        /// Subtracts a <see cref="Vector4"/> from a <see cref="Vector4"/>.
        /// </summary>
        /// <param name="value1">Source <see cref="Vector4"/> on the left of the sub sign.</param>
        /// <param name="value2">Source <see cref="Vector4"/> on the right of the sub sign.</param>
        /// <returns>Result of the vector subtraction.</returns>
        public static Vector4 operator -(Vector4 value1, Vector4 value2)
        {
            value1.W -= value2.W;
            value1.X -= value2.X;
            value1.Y -= value2.Y;
            value1.Z -= value2.Z;
            return value1;
        }

        /// <summary>
        /// Multiplies the components of two vectors by each other.
        /// </summary>
        /// <param name="value1">Source <see cref="Vector4"/> on the left of the mul sign.</param>
        /// <param name="value2">Source <see cref="Vector4"/> on the right of the mul sign.</param>
        /// <returns>Result of the vector multiplication.</returns>
        public static Vector4 operator *(Vector4 value1, Vector4 value2)
        {
            value1.W *= value2.W;
            value1.X *= value2.X;
            value1.Y *= value2.Y;
            value1.Z *= value2.Z;
            return value1;
        }

        /// <summary>
        /// Multiplies the components of vector by a scalar.
        /// </summary>
        /// <param name="value">Source <see cref="Vector4"/> on the left of the mul sign.</param>
        /// <param name="scaleFactor">Scalar value on the right of the mul sign.</param>
        /// <returns>Result of the vector multiplication with a scalar.</returns>
        public static Vector4 operator *(Vector4 value, Fixed scaleFactor)
        {
            value.W *= scaleFactor;
            value.X *= scaleFactor;
            value.Y *= scaleFactor;
            value.Z *= scaleFactor;
            return value;
        }

        /// <summary>
        /// Multiplies the components of vector by a scalar.
        /// </summary>
        /// <param name="scaleFactor">Scalar value on the left of the mul sign.</param>
        /// <param name="value">Source <see cref="Vector4"/> on the right of the mul sign.</param>
        /// <returns>Result of the vector multiplication with a scalar.</returns>
        public static Vector4 operator *(Fixed scaleFactor, Vector4 value)
        {
            value.W *= scaleFactor;
            value.X *= scaleFactor;
            value.Y *= scaleFactor;
            value.Z *= scaleFactor;
            return value;
        }

        /// <summary>
        /// Divides the components of a <see cref="Vector4"/> by the components of another <see cref="Vector4"/>.
        /// </summary>
        /// <param name="value1">Source <see cref="Vector4"/> on the left of the div sign.</param>
        /// <param name="value2">Divisor <see cref="Vector4"/> on the right of the div sign.</param>
        /// <returns>The result of dividing the vectors.</returns>
        public static Vector4 operator /(Vector4 value1, Vector4 value2)
        {
            value1.W /= value2.W;
            value1.X /= value2.X;
            value1.Y /= value2.Y;
            value1.Z /= value2.Z;
            return value1;
        }

        /// <summary>
        /// Divides the components of a <see cref="Vector4"/> by a scalar.
        /// </summary>
        /// <param name="value1">Source <see cref="Vector4"/> on the left of the div sign.</param>
        /// <param name="divider">Divisor scalar on the right of the div sign.</param>
        /// <returns>The result of dividing a vector by a scalar.</returns>
        public static Vector4 operator /(Vector4 value1, Fixed divider)
        {
            Fixed factor = Fixed.One / divider;
            value1.W *= factor;
            value1.X *= factor;
            value1.Y *= factor;
            value1.Z *= factor;
            return value1;
        }

        #endregion
    }
}
