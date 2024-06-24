using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace FixedMath
{
    /// <summary>
    /// Describes a 2D-point.
    /// </summary>
    [DataContract]
    [DebuggerDisplay("{DebugDisplayString,nq}")]
    public struct FixedPoint : IEquatable<FixedPoint>
    {
        #region Private Fields

        private static readonly FixedPoint zeroPoint = new FixedPoint();

        #endregion

        #region Public Fields

        /// <summary>
        /// The x coordinate of this <see cref="FixedPoint"/>.
        /// </summary>
        [DataMember]
        public int X;

        /// <summary>
        /// The y coordinate of this <see cref="FixedPoint"/>.
        /// </summary>
        [DataMember]
        public int Y;

        #endregion

        #region Properties

        /// <summary>
        /// Returns a <see cref="FixedPoint"/> with coordinates 0, 0.
        /// </summary>
        public static FixedPoint Zero
        {
            get { return zeroPoint; }
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
        /// Constructs a point with X and Y from two values.
        /// </summary>
        /// <param name="x">The x coordinate in 2d-space.</param>
        /// <param name="y">The y coordinate in 2d-space.</param>
        public FixedPoint(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        /// <summary>
        /// Constructs a point with X and Y set to the same value.
        /// </summary>
        /// <param name="value">The x and y coordinates in 2d-space.</param>
        public FixedPoint(int value)
        {
            this.X = value;
            this.Y = value;
        }

        #endregion

        #region Operators

        /// <summary>
        /// Adds two points.
        /// </summary>
        /// <param name="value1">Source <see cref="FixedPoint"/> on the left of the add sign.</param>
        /// <param name="value2">Source <see cref="FixedPoint"/> on the right of the add sign.</param>
        /// <returns>Sum of the points.</returns>
        public static FixedPoint operator +(FixedPoint value1, FixedPoint value2)
        {
            return new FixedPoint(value1.X + value2.X, value1.Y + value2.Y);
        }

        /// <summary>
        /// Subtracts a <see cref="FixedPoint"/> from a <see cref="FixedPoint"/>.
        /// </summary>
        /// <param name="value1">Source <see cref="FixedPoint"/> on the left of the sub sign.</param>
        /// <param name="value2">Source <see cref="FixedPoint"/> on the right of the sub sign.</param>
        /// <returns>Result of the subtraction.</returns>
        public static FixedPoint operator -(FixedPoint value1, FixedPoint value2)
        {
            return new FixedPoint(value1.X - value2.X, value1.Y - value2.Y);
        }

        /// <summary>
        /// Multiplies the components of two points by each other.
        /// </summary>
        /// <param name="value1">Source <see cref="FixedPoint"/> on the left of the mul sign.</param>
        /// <param name="value2">Source <see cref="FixedPoint"/> on the right of the mul sign.</param>
        /// <returns>Result of the multiplication.</returns>
        public static FixedPoint operator *(FixedPoint value1, FixedPoint value2)
        {
            return new FixedPoint(value1.X * value2.X, value1.Y * value2.Y);
        }

        /// <summary>
        /// Divides the components of a <see cref="FixedPoint"/> by the components of another <see cref="FixedPoint"/>.
        /// </summary>
        /// <param name="source">Source <see cref="FixedPoint"/> on the left of the div sign.</param>
        /// <param name="divisor">Divisor <see cref="FixedPoint"/> on the right of the div sign.</param>
        /// <returns>The result of dividing the points.</returns>
        public static FixedPoint operator /(FixedPoint source, FixedPoint divisor)
        {
            return new FixedPoint(source.X / divisor.X, source.Y / divisor.Y);
        }

        /// <summary>
        /// Compares whether two <see cref="FixedPoint"/> instances are equal.
        /// </summary>
        /// <param name="a"><see cref="FixedPoint"/> instance on the left of the equal sign.</param>
        /// <param name="b"><see cref="FixedPoint"/> instance on the right of the equal sign.</param>
        /// <returns><c>true</c> if the instances are equal; <c>false</c> otherwise.</returns>
        public static bool operator ==(FixedPoint a, FixedPoint b)
        {
            return a.Equals(b);
        }

        /// <summary>
        /// Compares whether two <see cref="FixedPoint"/> instances are not equal.
        /// </summary>
        /// <param name="a"><see cref="FixedPoint"/> instance on the left of the not equal sign.</param>
        /// <param name="b"><see cref="FixedPoint"/> instance on the right of the not equal sign.</param>
        /// <returns><c>true</c> if the instances are not equal; <c>false</c> otherwise.</returns>	
        public static bool operator !=(FixedPoint a, FixedPoint b)
        {
            return !a.Equals(b);
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Compares whether current instance is equal to specified <see cref="Object"/>.
        /// </summary>
        /// <param name="obj">The <see cref="Object"/> to compare.</param>
        /// <returns><c>true</c> if the instances are equal; <c>false</c> otherwise.</returns>
        public override bool Equals(object obj)
        {
            return (obj is FixedPoint) && Equals((FixedPoint)obj);
        }

        /// <summary>
        /// Compares whether current instance is equal to specified <see cref="FixedPoint"/>.
        /// </summary>
        /// <param name="other">The <see cref="FixedPoint"/> to compare.</param>
        /// <returns><c>true</c> if the instances are equal; <c>false</c> otherwise.</returns>
        public bool Equals(FixedPoint other)
        {
            return ((X == other.X) && (Y == other.Y));
        }

        /// <summary>
        /// Gets the hash code of this <see cref="FixedPoint"/>.
        /// </summary>
        /// <returns>Hash code of this <see cref="FixedPoint"/>.</returns>
        public override int GetHashCode()
        {
            unchecked
            {
                var hash = 17;
                hash = hash * 23 + X.GetHashCode();
                hash = hash * 23 + Y.GetHashCode();
                return hash;
            }

        }

        /// <summary>
        /// Returns a <see cref="String"/> representation of this <see cref="FixedPoint"/> in the format:
        /// {X:[<see cref="X"/>] Y:[<see cref="Y"/>]}
        /// </summary>
        /// <returns><see cref="String"/> representation of this <see cref="FixedPoint"/>.</returns>
        public override string ToString()
        {
            return "{X:" + X + " Y:" + Y + "}";
        }

        /// <summary>
        /// Gets a <see cref="FixedVector2"/> representation for this object.
        /// </summary>
        /// <returns>A <see cref="FixedVector2"/> representation for this object.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public FixedVector2 ToVector2()
        {
            return new FixedVector2((Fixed)X, (Fixed)Y);
        }

        /// <summary>
        /// Deconstruction method for <see cref="FixedPoint"/>.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void Deconstruct(out int x, out int y)
        {
            x = X;
            y = Y;
        }

        #endregion
    }
}


