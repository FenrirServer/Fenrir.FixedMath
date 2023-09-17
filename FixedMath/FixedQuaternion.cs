using System;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace FixedMath
{
    /// <summary>
    /// An efficient mathematical representation for three dimensional rotations.
    /// </summary>
    [DataContract]
    [DebuggerDisplay("{DebugDisplayString,nq}")]
    public struct FixedQuaternion : IEquatable<FixedQuaternion>
    {
        #region Private Fields

        private static readonly FixedQuaternion _identity = new FixedQuaternion(Fixed.Zero, Fixed.Zero, Fixed.Zero, Fixed.One);

        #endregion

        #region Public Fields

        /// <summary>
        /// The x coordinate of this <see cref="FixedQuaternion"/>.
        /// </summary>
        [DataMember]
        public Fixed X;

        /// <summary>
        /// The y coordinate of this <see cref="FixedQuaternion"/>.
        /// </summary>
        [DataMember]
        public Fixed Y;

        /// <summary>
        /// The z coordinate of this <see cref="FixedQuaternion"/>.
        /// </summary>
        [DataMember]
        public Fixed Z;

        /// <summary>
        /// The rotation component of this <see cref="FixedQuaternion"/>.
        /// </summary>
        [DataMember]
        public Fixed W;

        #endregion

        #region Constructors

        /// <summary>
        /// Constructs a quaternion with X, Y, Z and W from four values.
        /// </summary>
        /// <param name="x">The x coordinate in 3d-space.</param>
        /// <param name="y">The y coordinate in 3d-space.</param>
        /// <param name="z">The z coordinate in 3d-space.</param>
        /// <param name="w">The rotation component.</param>
        internal FixedQuaternion(float x, float y, float z, float w)
        {
            this.X = (Fixed)x;
            this.Y = (Fixed)y;
            this.Z = (Fixed)z;
            this.W = (Fixed)w;
        }

        /// <summary>
        /// Constructs a quaternion with X, Y, Z and W from four values.
        /// </summary>
        /// <param name="x">The x coordinate in 3d-space.</param>
        /// <param name="y">The y coordinate in 3d-space.</param>
        /// <param name="z">The z coordinate in 3d-space.</param>
        /// <param name="w">The rotation component.</param>
        public FixedQuaternion(Fixed x, Fixed y, Fixed z, Fixed w)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
            this.W = w;
        }

        /// <summary>
        /// Constructs a quaternion with X, Y, Z from <see cref="FixedVector3"/> and rotation component from a scalar.
        /// </summary>
        /// <param name="value">The x, y, z coordinates in 3d-space.</param>
        /// <param name="w">The rotation component.</param>
        internal FixedQuaternion(FixedVector3 value, float w)
        {
            this.X = value.X;
            this.Y = value.Y;
            this.Z = value.Z;
            this.W = (Fixed)w;
        }

        /// <summary>
        /// Constructs a quaternion with X, Y, Z from <see cref="FixedVector3"/> and rotation component from a scalar.
        /// </summary>
        /// <param name="value">The x, y, z coordinates in 3d-space.</param>
        /// <param name="w">The rotation component.</param>
        public FixedQuaternion(FixedVector3 value, Fixed w)
        {
            this.X = value.X;
            this.Y = value.Y;
            this.Z = value.Z;
            this.W = w;
        }

        /// <summary>
        /// Constructs a quaternion from <see cref="FixedVector4"/>.
        /// </summary>
        /// <param name="value">The x, y, z coordinates in 3d-space and the rotation component.</param>
        public FixedQuaternion(FixedVector4 value)
        {
            this.X = value.X;
            this.Y = value.Y;
            this.Z = value.Z;
            this.W = value.W;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Returns a quaternion representing no rotation.
        /// </summary>
        public static FixedQuaternion Identity
        {
            get{ return _identity; }
        }

        #endregion

        #region Internal Properties

        internal string DebugDisplayString
        {
            get
            {
                if (this == FixedQuaternion._identity)
                {
                    return "Identity";
                }

                return string.Concat(
                    this.X.ToString(), " ",
                    this.Y.ToString(), " ",
                    this.Z.ToString(), " ",
                    this.W.ToString()
                );
            }
        }

        #endregion

        #region Public Methods

        #region Add

        /// <summary>
        /// Creates a new <see cref="FixedQuaternion"/> that contains the sum of two quaternions.
        /// </summary>
        /// <param name="quaternion1">Source <see cref="FixedQuaternion"/>.</param>
        /// <param name="quaternion2">Source <see cref="FixedQuaternion"/>.</param>
        /// <returns>The result of the quaternion addition.</returns>
        public static FixedQuaternion Add(FixedQuaternion quaternion1, FixedQuaternion quaternion2)
        {
			FixedQuaternion quaternion;
			quaternion.X = quaternion1.X + quaternion2.X;
			quaternion.Y = quaternion1.Y + quaternion2.Y;
			quaternion.Z = quaternion1.Z + quaternion2.Z;
			quaternion.W = quaternion1.W + quaternion2.W;
			return quaternion;
        }

        /// <summary>
        /// Creates a new <see cref="FixedQuaternion"/> that contains the sum of two quaternions.
        /// </summary>
        /// <param name="quaternion1">Source <see cref="FixedQuaternion"/>.</param>
        /// <param name="quaternion2">Source <see cref="FixedQuaternion"/>.</param>
        /// <param name="result">The result of the quaternion addition as an output parameter.</param>
        public static void Add(ref FixedQuaternion quaternion1, ref FixedQuaternion quaternion2, out FixedQuaternion result)
        {
			result.X = quaternion1.X + quaternion2.X;
			result.Y = quaternion1.Y + quaternion2.Y;
			result.Z = quaternion1.Z + quaternion2.Z;
			result.W = quaternion1.W + quaternion2.W;
        }

        #endregion

        #region Concatenate

        /// <summary>
        /// Creates a new <see cref="FixedQuaternion"/> that contains concatenation between two quaternion.
        /// </summary>
        /// <param name="value1">The first <see cref="FixedQuaternion"/> to concatenate.</param>
        /// <param name="value2">The second <see cref="FixedQuaternion"/> to concatenate.</param>
        /// <returns>The result of rotation of <paramref name="value1"/> followed by <paramref name="value2"/> rotation.</returns>
        public static FixedQuaternion Concatenate(FixedQuaternion value1, FixedQuaternion value2)
		{
			FixedQuaternion quaternion;

            Fixed x1 = value1.X;
            Fixed y1 = value1.Y;
            Fixed z1 = value1.Z;
            Fixed w1 = value1.W;

            Fixed x2 = value2.X;
		    Fixed y2 = value2.Y;
		    Fixed z2 = value2.Z;
		    Fixed w2 = value2.W;

		    quaternion.X = ((x2 * w1) + (x1 * w2)) + ((y2 * z1) - (z2 * y1));
		    quaternion.Y = ((y2 * w1) + (y1 * w2)) + ((z2 * x1) - (x2 * z1));
		    quaternion.Z = ((z2 * w1) + (z1 * w2)) + ((x2 * y1) - (y2 * x1));
		    quaternion.W = (w2 * w1) - (((x2 * x1) + (y2 * y1)) + (z2 * z1));

		    return quaternion;
		}

        /// <summary>
        /// Creates a new <see cref="FixedQuaternion"/> that contains concatenation between two quaternion.
        /// </summary>
        /// <param name="value1">The first <see cref="FixedQuaternion"/> to concatenate.</param>
        /// <param name="value2">The second <see cref="FixedQuaternion"/> to concatenate.</param>
        /// <param name="result">The result of rotation of <paramref name="value1"/> followed by <paramref name="value2"/> rotation as an output parameter.</param>
        public static void Concatenate(ref FixedQuaternion value1, ref FixedQuaternion value2, out FixedQuaternion result)
		{
            Fixed x1 = value1.X;
            Fixed y1 = value1.Y;
            Fixed z1 = value1.Z;
            Fixed w1 = value1.W;

            Fixed x2 = value2.X;
            Fixed y2 = value2.Y;
            Fixed z2 = value2.Z;
            Fixed w2 = value2.W;

            result.X = ((x2 * w1) + (x1 * w2)) + ((y2 * z1) - (z2 * y1));
            result.Y = ((y2 * w1) + (y1 * w2)) + ((z2 * x1) - (x2 * z1));
            result.Z = ((z2 * w1) + (z1 * w2)) + ((x2 * y1) - (y2 * x1));
            result.W = (w2 * w1) - (((x2 * x1) + (y2 * y1)) + (z2 * z1));
        }

        #endregion

        #region Conjugate

        /// <summary>
        /// Transforms this quaternion into its conjugated version.
        /// </summary>
        public void Conjugate()
		{
			X = -X;
			Y = -Y;
			Z = -Z;
		}

        /// <summary>
        /// Creates a new <see cref="FixedQuaternion"/> that contains conjugated version of the specified quaternion.
        /// </summary>
        /// <param name="value">The quaternion which values will be used to create the conjugated version.</param>
        /// <returns>The conjugate version of the specified quaternion.</returns>
        public static FixedQuaternion Conjugate(FixedQuaternion value)
		{
			return new FixedQuaternion(-value.X,-value.Y,-value.Z,value.W);
		}

        /// <summary>
        /// Creates a new <see cref="FixedQuaternion"/> that contains conjugated version of the specified quaternion.
        /// </summary>
        /// <param name="value">The quaternion which values will be used to create the conjugated version.</param>
        /// <param name="result">The conjugated version of the specified quaternion as an output parameter.</param>
        public static void Conjugate(ref FixedQuaternion value, out FixedQuaternion result)
		{
			result.X = -value.X;
			result.Y = -value.Y;
			result.Z = -value.Z;
			result.W = value.W;
		}

        #endregion

        #region CreateFromAxisAngle

        /// <summary>
        /// Creates a new <see cref="FixedQuaternion"/> from the specified axis and angle.
        /// </summary>
        /// <param name="axis">The axis of rotation.</param>
        /// <param name="angle">The angle in radians.</param>
        /// <returns>The new quaternion builded from axis and angle.</returns>
        public static FixedQuaternion CreateFromAxisAngle(FixedVector3 axis, Fixed angle)
        {
		    Fixed half = angle * Fixed.Half;
		    Fixed sin = Fixed.Sin(half);
		    Fixed cos = Fixed.Cos(half);
		    return new FixedQuaternion(axis.X * sin, axis.Y * sin, axis.Z * sin, cos);
        }

        /// <summary>
        /// Creates a new <see cref="FixedQuaternion"/> from the specified axis and angle.
        /// </summary>
        /// <param name="axis">The axis of rotation.</param>
        /// <param name="angle">The angle in radians.</param>
        /// <param name="result">The new quaternion builded from axis and angle as an output parameter.</param>
        public static void CreateFromAxisAngle(ref FixedVector3 axis, Fixed angle, out FixedQuaternion result)
        {
            Fixed half = angle * Fixed.Half;
		    Fixed sin = Fixed.Sin(half);
		    Fixed cos = Fixed.Cos(half);
		    result.X = axis.X * sin;
		    result.Y = axis.Y * sin;
		    result.Z = axis.Z * sin;
		    result.W = cos;
        }

        #endregion

        #region CreateFromRotationMatrix

        /// <summary>
        /// Creates a new <see cref="FixedQuaternion"/> from the specified <see cref="FixedMatrix"/>.
        /// </summary>
        /// <param name="matrix">The rotation matrix.</param>
        /// <returns>A quaternion composed from the rotation part of the matrix.</returns>
        public static FixedQuaternion CreateFromRotationMatrix(FixedMatrix matrix)
        {
            FixedQuaternion quaternion;
            Fixed sqrt;
            Fixed half;
            Fixed scale = matrix.M11 + matrix.M22 + matrix.M33;

		    if (scale > Fixed.Zero)
		    {
                sqrt = Fixed.Sqrt(scale + Fixed.One);
		        quaternion.W = sqrt * Fixed.Half;
                sqrt = Fixed.Half / sqrt;

		        quaternion.X = (matrix.M23 - matrix.M32) * sqrt;
		        quaternion.Y = (matrix.M31 - matrix.M13) * sqrt;
		        quaternion.Z = (matrix.M12 - matrix.M21) * sqrt;

		        return quaternion;
		    }
		    if ((matrix.M11 >= matrix.M22) && (matrix.M11 >= matrix.M33))
		    {
                sqrt = Fixed.Sqrt(Fixed.One + matrix.M11 - matrix.M22 - matrix.M33);
                half = Fixed.Half / sqrt;

		        quaternion.X = Fixed.Half * sqrt;
		        quaternion.Y = (matrix.M12 + matrix.M21) * half;
		        quaternion.Z = (matrix.M13 + matrix.M31) * half;
		        quaternion.W = (matrix.M23 - matrix.M32) * half;

		        return quaternion;
		    }
		    if (matrix.M22 > matrix.M33)
		    {
                sqrt = Fixed.Sqrt(Fixed.One + matrix.M22 - matrix.M11 - matrix.M33);
                half = Fixed.Half / sqrt;

		        quaternion.X = (matrix.M21 + matrix.M12) * half;
		        quaternion.Y = Fixed.Half * sqrt;
		        quaternion.Z = (matrix.M32 + matrix.M23) * half;
		        quaternion.W = (matrix.M31 - matrix.M13) * half;

		        return quaternion;
		    }
            sqrt = Fixed.Sqrt(Fixed.One + matrix.M33 - matrix.M11 - matrix.M22);
		    half = Fixed.Half / sqrt;

		    quaternion.X = (matrix.M31 + matrix.M13) * half;
		    quaternion.Y = (matrix.M32 + matrix.M23) * half;
		    quaternion.Z = Fixed.Half * sqrt;
		    quaternion.W = (matrix.M12 - matrix.M21) * half;

		    return quaternion;
        }

        /// <summary>
        /// Creates a new <see cref="FixedQuaternion"/> from the specified <see cref="FixedMatrix"/>.
        /// </summary>
        /// <param name="matrix">The rotation matrix.</param>
        /// <param name="result">A quaternion composed from the rotation part of the matrix as an output parameter.</param>
        public static void CreateFromRotationMatrix(ref FixedMatrix matrix, out FixedQuaternion result)
        {
            Fixed sqrt;
            Fixed half;
            Fixed scale = matrix.M11 + matrix.M22 + matrix.M33;

            if (scale > Fixed.Zero)
            {
                sqrt = Fixed.Sqrt(scale + Fixed.One);
                result.W = sqrt * Fixed.Half;
                sqrt = Fixed.Half / sqrt;

                result.X = (matrix.M23 - matrix.M32) * sqrt;
                result.Y = (matrix.M31 - matrix.M13) * sqrt;
                result.Z = (matrix.M12 - matrix.M21) * sqrt;
            }
            else
            if ((matrix.M11 >= matrix.M22) && (matrix.M11 >= matrix.M33))
            {
                sqrt = Fixed.Sqrt(Fixed.One + matrix.M11 - matrix.M22 - matrix.M33);
                half = Fixed.Half / sqrt;

                result.X = Fixed.Half * sqrt;
                result.Y = (matrix.M12 + matrix.M21) * half;
                result.Z = (matrix.M13 + matrix.M31) * half;
                result.W = (matrix.M23 - matrix.M32) * half;
            }
            else if (matrix.M22 > matrix.M33)
            {
                sqrt = Fixed.Sqrt(Fixed.One + matrix.M22 - matrix.M11 - matrix.M33);
                half = Fixed.Half/sqrt;

                result.X = (matrix.M21 + matrix.M12)*half;
                result.Y = Fixed.Half*sqrt;
                result.Z = (matrix.M32 + matrix.M23)*half;
                result.W = (matrix.M31 - matrix.M13)*half;
            }
            else
            {
                sqrt = Fixed.Sqrt(Fixed.One + matrix.M33 - matrix.M11 - matrix.M22);
                half = Fixed.Half / sqrt;

                result.X = (matrix.M31 + matrix.M13) * half;
                result.Y = (matrix.M32 + matrix.M23) * half;
                result.Z = Fixed.Half * sqrt;
                result.W = (matrix.M12 - matrix.M21) * half;
            }
        }

        #endregion

        #region CreateFromYawPitchRoll

        /// <summary>
        /// Creates a new <see cref="FixedQuaternion"/> from the specified yaw, pitch and roll angles.
        /// </summary>
        /// <param name="yaw">Yaw around the y axis in radians.</param>
        /// <param name="pitch">Pitch around the x axis in radians.</param>
        /// <param name="roll">Roll around the z axis in radians.</param>
        /// <returns>A new quaternion from the concatenated yaw, pitch, and roll angles.</returns>
        public static FixedQuaternion CreateFromYawPitchRoll(Fixed yaw, Fixed pitch, Fixed roll)
		{
            Fixed halfRoll = roll * Fixed.Half;
            Fixed halfPitch = pitch * Fixed.Half;
            Fixed halfYaw = yaw * Fixed.Half;

            Fixed sinRoll = Fixed.Sin(halfRoll);
            Fixed cosRoll = Fixed.Cos(halfRoll);
            Fixed sinPitch = Fixed.Sin(halfPitch);
            Fixed cosPitch = Fixed.Cos(halfPitch);
            Fixed sinYaw = Fixed.Sin(halfYaw);
            Fixed cosYaw = Fixed.Cos(halfYaw);

            return new FixedQuaternion((cosYaw * sinPitch * cosRoll) + (sinYaw * cosPitch * sinRoll),
                                  (sinYaw * cosPitch * cosRoll) - (cosYaw * sinPitch * sinRoll),
                                  (cosYaw * cosPitch * sinRoll) - (sinYaw * sinPitch * cosRoll),
                                  (cosYaw * cosPitch * cosRoll) + (sinYaw * sinPitch * sinRoll));
        }

        /// <summary>
        /// Creates a new <see cref="FixedQuaternion"/> from the specified yaw, pitch and roll angles.
        /// </summary>
        /// <param name="yaw">Yaw around the y axis in radians.</param>
        /// <param name="pitch">Pitch around the x axis in radians.</param>
        /// <param name="roll">Roll around the z axis in radians.</param>
        /// <param name="result">A new quaternion from the concatenated yaw, pitch, and roll angles as an output parameter.</param>
 		public static void CreateFromYawPitchRoll(Fixed yaw, Fixed pitch, Fixed roll, out FixedQuaternion result)
		{
            Fixed halfRoll = roll * Fixed.Half;
            Fixed halfPitch = pitch * Fixed.Half;
            Fixed halfYaw = yaw * Fixed.Half;

            Fixed sinRoll = Fixed.Sin(halfRoll);
            Fixed cosRoll = Fixed.Cos(halfRoll);
            Fixed sinPitch = Fixed.Sin(halfPitch);
            Fixed cosPitch = Fixed.Cos(halfPitch);
            Fixed sinYaw = Fixed.Sin(halfYaw);
            Fixed cosYaw = Fixed.Cos(halfYaw);

            result.X = (cosYaw * sinPitch * cosRoll) + (sinYaw * cosPitch * sinRoll);
            result.Y = (sinYaw * cosPitch * cosRoll) - (cosYaw * sinPitch * sinRoll);
            result.Z = (cosYaw * cosPitch * sinRoll) - (sinYaw * sinPitch * cosRoll);
            result.W = (cosYaw * cosPitch * cosRoll) + (sinYaw * sinPitch * sinRoll);
        }

        #endregion

        #region Divide

        /// <summary>
        /// Divides a <see cref="FixedQuaternion"/> by the other <see cref="FixedQuaternion"/>.
        /// </summary>
        /// <param name="quaternion1">Source <see cref="FixedQuaternion"/>.</param>
        /// <param name="quaternion2">Divisor <see cref="FixedQuaternion"/>.</param>
        /// <returns>The result of dividing the quaternions.</returns>
        public static FixedQuaternion Divide(FixedQuaternion quaternion1, FixedQuaternion quaternion2)
        {
            FixedQuaternion quaternion;
		    Fixed x = quaternion1.X;
		    Fixed y = quaternion1.Y;
		    Fixed z = quaternion1.Z;
		    Fixed w = quaternion1.W;
		    Fixed num14 = (((quaternion2.X * quaternion2.X) + (quaternion2.Y * quaternion2.Y)) + (quaternion2.Z * quaternion2.Z)) + (quaternion2.W * quaternion2.W);
		    Fixed num5 = Fixed.One / num14;
		    Fixed num4 = -quaternion2.X * num5;
		    Fixed num3 = -quaternion2.Y * num5;
		    Fixed num2 = -quaternion2.Z * num5;
		    Fixed num = quaternion2.W * num5;
		    Fixed num13 = (y * num2) - (z * num3);
		    Fixed num12 = (z * num4) - (x * num2);
		    Fixed num11 = (x * num3) - (y * num4);
		    Fixed num10 = ((x * num4) + (y * num3)) + (z * num2);
		    quaternion.X = ((x * num) + (num4 * w)) + num13;
		    quaternion.Y = ((y * num) + (num3 * w)) + num12;
		    quaternion.Z = ((z * num) + (num2 * w)) + num11;
		    quaternion.W = (w * num) - num10;
		    return quaternion;
        }

        /// <summary>
        /// Divides a <see cref="FixedQuaternion"/> by the other <see cref="FixedQuaternion"/>.
        /// </summary>
        /// <param name="quaternion1">Source <see cref="FixedQuaternion"/>.</param>
        /// <param name="quaternion2">Divisor <see cref="FixedQuaternion"/>.</param>
        /// <param name="result">The result of dividing the quaternions as an output parameter.</param>
        public static void Divide(ref FixedQuaternion quaternion1, ref FixedQuaternion quaternion2, out FixedQuaternion result)
        {
            Fixed x = quaternion1.X;
		    Fixed y = quaternion1.Y;
		    Fixed z = quaternion1.Z;
		    Fixed w = quaternion1.W;
		    Fixed num14 = (((quaternion2.X * quaternion2.X) + (quaternion2.Y * quaternion2.Y)) + (quaternion2.Z * quaternion2.Z)) + (quaternion2.W * quaternion2.W);
		    Fixed num5 = Fixed.One / num14;
		    Fixed num4 = -quaternion2.X * num5;
		    Fixed num3 = -quaternion2.Y * num5;
		    Fixed num2 = -quaternion2.Z * num5;
		    Fixed num = quaternion2.W * num5;
		    Fixed num13 = (y * num2) - (z * num3);
		    Fixed num12 = (z * num4) - (x * num2);
		    Fixed num11 = (x * num3) - (y * num4);
		    Fixed num10 = ((x * num4) + (y * num3)) + (z * num2);
		    result.X = ((x * num) + (num4 * w)) + num13;
		    result.Y = ((y * num) + (num3 * w)) + num12;
		    result.Z = ((z * num) + (num2 * w)) + num11;
		    result.W = (w * num) - num10;
        }

        #endregion

        #region Dot

        /// <summary>
        /// Returns a dot product of two quaternions.
        /// </summary>
        /// <param name="quaternion1">The first quaternion.</param>
        /// <param name="quaternion2">The second quaternion.</param>
        /// <returns>The dot product of two quaternions.</returns>
        public static Fixed Dot(FixedQuaternion quaternion1, FixedQuaternion quaternion2)
        {
            return ((((quaternion1.X * quaternion2.X) + (quaternion1.Y * quaternion2.Y)) + (quaternion1.Z * quaternion2.Z)) + (quaternion1.W * quaternion2.W));
        }

        /// <summary>
        /// Returns a dot product of two quaternions.
        /// </summary>
        /// <param name="quaternion1">The first quaternion.</param>
        /// <param name="quaternion2">The second quaternion.</param>
        /// <param name="result">The dot product of two quaternions as an output parameter.</param>
        public static void Dot(ref FixedQuaternion quaternion1, ref FixedQuaternion quaternion2, out Fixed result)
        {
            result = (((quaternion1.X * quaternion2.X) + (quaternion1.Y * quaternion2.Y)) + (quaternion1.Z * quaternion2.Z)) + (quaternion1.W * quaternion2.W);
        }

        #endregion

        #region Equals

        /// <summary>
        /// Compares whether current instance is equal to specified <see cref="Object"/>.
        /// </summary>
        /// <param name="obj">The <see cref="Object"/> to compare.</param>
        /// <returns><c>true</c> if the instances are equal; <c>false</c> otherwise.</returns>
        public override bool Equals(object obj)
        {
            if (obj is FixedQuaternion)
                return Equals((FixedQuaternion)obj);
            return false;
        }

        /// <summary>
        /// Compares whether current instance is equal to specified <see cref="FixedQuaternion"/>.
        /// </summary>
        /// <param name="other">The <see cref="FixedQuaternion"/> to compare.</param>
        /// <returns><c>true</c> if the instances are equal; <c>false</c> otherwise.</returns>
        public bool Equals(FixedQuaternion other)
        {
			return X == other.X &&
                   Y == other.Y &&
                   Z == other.Z &&
                   W == other.W;
        }

        #endregion

        /// <summary>
        /// Gets the hash code of this <see cref="FixedQuaternion"/>.
        /// </summary>
        /// <returns>Hash code of this <see cref="FixedQuaternion"/>.</returns>
        public override int GetHashCode()
        {
            return X.GetHashCode() + Y.GetHashCode() + Z.GetHashCode() + W.GetHashCode();
        }

        #region Inverse

        /// <summary>
        /// Returns the inverse quaternion which represents the opposite rotation.
        /// </summary>
        /// <param name="quaternion">Source <see cref="FixedQuaternion"/>.</param>
        /// <returns>The inverse quaternion.</returns>
        public static FixedQuaternion Inverse(FixedQuaternion quaternion)
        {
            FixedQuaternion quaternion2;
		    Fixed num2 = (((quaternion.X * quaternion.X) + (quaternion.Y * quaternion.Y)) + (quaternion.Z * quaternion.Z)) + (quaternion.W * quaternion.W);
		    Fixed num = Fixed.One / num2;
		    quaternion2.X = -quaternion.X * num;
		    quaternion2.Y = -quaternion.Y * num;
		    quaternion2.Z = -quaternion.Z * num;
		    quaternion2.W = quaternion.W * num;
		    return quaternion2;
        }

        /// <summary>
        /// Returns the inverse quaternion which represents the opposite rotation.
        /// </summary>
        /// <param name="quaternion">Source <see cref="FixedQuaternion"/>.</param>
        /// <param name="result">The inverse quaternion as an output parameter.</param>
        public static void Inverse(ref FixedQuaternion quaternion, out FixedQuaternion result)
        {
            Fixed num2 = (((quaternion.X * quaternion.X) + (quaternion.Y * quaternion.Y)) + (quaternion.Z * quaternion.Z)) + (quaternion.W * quaternion.W);
		    Fixed num = Fixed.One / num2;
		    result.X = -quaternion.X * num;
		    result.Y = -quaternion.Y * num;
		    result.Z = -quaternion.Z * num;
		    result.W = quaternion.W * num;
        }

        #endregion

        /// <summary>
        /// Returns the magnitude of the quaternion components.
        /// </summary>
        /// <returns>The magnitude of the quaternion components.</returns>
        public Fixed Length()
        {
    		return Fixed.Sqrt((X * X) + (Y * Y) + (Z * Z) + (W * W));
        }

        /// <summary>
        /// Returns the squared magnitude of the quaternion components.
        /// </summary>
        /// <returns>The squared magnitude of the quaternion components.</returns>
        public Fixed LengthSquared()
        {
            return (X * X) + (Y * Y) + (Z * Z) + (W * W);
        }

        #region Lerp

        /// <summary>
        /// Performs a linear blend between two quaternions.
        /// </summary>
        /// <param name="quaternion1">Source <see cref="FixedQuaternion"/>.</param>
        /// <param name="quaternion2">Source <see cref="FixedQuaternion"/>.</param>
        /// <param name="amount">The blend amount where 0 returns <paramref name="quaternion1"/> and 1 <paramref name="quaternion2"/>.</param>
        /// <returns>The result of linear blending between two quaternions.</returns>
        public static FixedQuaternion Lerp(FixedQuaternion quaternion1, FixedQuaternion quaternion2, Fixed amount)
        {
            Fixed num = amount;
		    Fixed num2 = Fixed.One - num;
		    FixedQuaternion quaternion = new FixedQuaternion();
		    Fixed num5 = (((quaternion1.X * quaternion2.X) + (quaternion1.Y * quaternion2.Y)) + (quaternion1.Z * quaternion2.Z)) + (quaternion1.W * quaternion2.W);
		    if (num5 >= Fixed.Zero)
		    {
		        quaternion.X = (num2 * quaternion1.X) + (num * quaternion2.X);
		        quaternion.Y = (num2 * quaternion1.Y) + (num * quaternion2.Y);
		        quaternion.Z = (num2 * quaternion1.Z) + (num * quaternion2.Z);
		        quaternion.W = (num2 * quaternion1.W) + (num * quaternion2.W);
		    }
		    else
		    {
		        quaternion.X = (num2 * quaternion1.X) - (num * quaternion2.X);
		        quaternion.Y = (num2 * quaternion1.Y) - (num * quaternion2.Y);
		        quaternion.Z = (num2 * quaternion1.Z) - (num * quaternion2.Z);
		        quaternion.W = (num2 * quaternion1.W) - (num * quaternion2.W);
		    }
		    Fixed num4 = (((quaternion.X * quaternion.X) + (quaternion.Y * quaternion.Y)) + (quaternion.Z * quaternion.Z)) + (quaternion.W * quaternion.W);
		    Fixed num3 = Fixed.One / Fixed.Sqrt(num4);
		    quaternion.X *= num3;
		    quaternion.Y *= num3;
		    quaternion.Z *= num3;
		    quaternion.W *= num3;
		    return quaternion;
        }

        /// <summary>
        /// Performs a linear blend between two quaternions.
        /// </summary>
        /// <param name="quaternion1">Source <see cref="FixedQuaternion"/>.</param>
        /// <param name="quaternion2">Source <see cref="FixedQuaternion"/>.</param>
        /// <param name="amount">The blend amount where 0 returns <paramref name="quaternion1"/> and 1 <paramref name="quaternion2"/>.</param>
        /// <param name="result">The result of linear blending between two quaternions as an output parameter.</param>
        public static void Lerp(ref FixedQuaternion quaternion1, ref FixedQuaternion quaternion2, Fixed amount, out FixedQuaternion result)
        {
            Fixed num = amount;
		    Fixed num2 = Fixed.One - num;
		    Fixed num5 = (((quaternion1.X * quaternion2.X) + (quaternion1.Y * quaternion2.Y)) + (quaternion1.Z * quaternion2.Z)) + (quaternion1.W * quaternion2.W);
		    if (num5 >= Fixed.Zero)
		    {
		        result.X = (num2 * quaternion1.X) + (num * quaternion2.X);
		        result.Y = (num2 * quaternion1.Y) + (num * quaternion2.Y);
		        result.Z = (num2 * quaternion1.Z) + (num * quaternion2.Z);
		        result.W = (num2 * quaternion1.W) + (num * quaternion2.W);
		    }
		    else
		    {
		        result.X = (num2 * quaternion1.X) - (num * quaternion2.X);
		        result.Y = (num2 * quaternion1.Y) - (num * quaternion2.Y);
		        result.Z = (num2 * quaternion1.Z) - (num * quaternion2.Z);
		        result.W = (num2 * quaternion1.W) - (num * quaternion2.W);
		    }
		    Fixed num4 = (((result.X * result.X) + (result.Y * result.Y)) + (result.Z * result.Z)) + (result.W * result.W);
		    Fixed num3 = Fixed.One / Fixed.Sqrt(num4);
		    result.X *= num3;
		    result.Y *= num3;
		    result.Z *= num3;
		    result.W *= num3;

        }

        #endregion

        #region Slerp

        /// <summary>
        /// Performs a spherical linear blend between two quaternions.
        /// </summary>
        /// <param name="quaternion1">Source <see cref="FixedQuaternion"/>.</param>
        /// <param name="quaternion2">Source <see cref="FixedQuaternion"/>.</param>
        /// <param name="amount">The blend amount where 0 returns <paramref name="quaternion1"/> and 1 <paramref name="quaternion2"/>.</param>
        /// <returns>The result of spherical linear blending between two quaternions.</returns>
        public static FixedQuaternion Slerp(FixedQuaternion quaternion1, FixedQuaternion quaternion2, Fixed amount)
        {
            Fixed num2;
		    Fixed num3;
		    FixedQuaternion quaternion;
		    Fixed num = amount;
		    Fixed num4 = (((quaternion1.X * quaternion2.X) + (quaternion1.Y * quaternion2.Y)) + (quaternion1.Z * quaternion2.Z)) + (quaternion1.W * quaternion2.W);
		    bool flag = false;
		    if (num4 < Fixed.Zero)
		    {
		        flag = true;
		        num4 = -num4;
		    }
		    if (num4 > (Fixed)0.999999M)
		    {
		        num3 = Fixed.One - num;
		        num2 = flag ? -num : num;
		    }
		    else
		    {
		        Fixed num5 = Fixed.Acos(num4);
		        Fixed num6 = (Fixed) (Fixed.One / Fixed.Sin((Fixed) num5));
		        num3 = Fixed.Sin((Fixed.One - num) * num5) * num6;
		        num2 = flag ? (-Fixed.Sin(num * num5) * num6) : (Fixed.Sin(num * num5) * num6);
		    }
		    quaternion.X = (num3 * quaternion1.X) + (num2 * quaternion2.X);
		    quaternion.Y = (num3 * quaternion1.Y) + (num2 * quaternion2.Y);
		    quaternion.Z = (num3 * quaternion1.Z) + (num2 * quaternion2.Z);
		    quaternion.W = (num3 * quaternion1.W) + (num2 * quaternion2.W);
		    return quaternion;
        }

        /// <summary>
        /// Performs a spherical linear blend between two quaternions.
        /// </summary>
        /// <param name="quaternion1">Source <see cref="FixedQuaternion"/>.</param>
        /// <param name="quaternion2">Source <see cref="FixedQuaternion"/>.</param>
        /// <param name="amount">The blend amount where 0 returns <paramref name="quaternion1"/> and 1 <paramref name="quaternion2"/>.</param>
        /// <param name="result">The result of spherical linear blending between two quaternions as an output parameter.</param>
        public static void Slerp(ref FixedQuaternion quaternion1, ref FixedQuaternion quaternion2, Fixed amount, out FixedQuaternion result)
        {
            Fixed num2;
		    Fixed num3;
		    Fixed num = amount;
		    Fixed num4 = (((quaternion1.X * quaternion2.X) + (quaternion1.Y * quaternion2.Y)) + (quaternion1.Z * quaternion2.Z)) + (quaternion1.W * quaternion2.W);
		    bool flag = false;
		    if (num4 < Fixed.Zero)
		    {
		        flag = true;
		        num4 = -num4;
		    }
		    if (num4 > (Fixed)0.999999M)
		    {
		        num3 = Fixed.One - num;
		        num2 = flag ? -num : num;
		    }
		    else
		    {
		        Fixed num5 = Fixed.Acos(num4);
		        Fixed num6 = (Fixed) (Fixed.One / Fixed.Sin((Fixed) num5));
		        num3 = Fixed.Sin((Fixed.One - num) * num5) * num6;
		        num2 = flag ? (-Fixed.Sin(num * num5) * num6) : (Fixed.Sin(num * num5) * num6);
		    }
		    result.X = (num3 * quaternion1.X) + (num2 * quaternion2.X);
		    result.Y = (num3 * quaternion1.Y) + (num2 * quaternion2.Y);
		    result.Z = (num3 * quaternion1.Z) + (num2 * quaternion2.Z);
		    result.W = (num3 * quaternion1.W) + (num2 * quaternion2.W);
        }

        #endregion

        #region Subtract

        /// <summary>
        /// Creates a new <see cref="FixedQuaternion"/> that contains subtraction of one <see cref="FixedQuaternion"/> from another.
        /// </summary>
        /// <param name="quaternion1">Source <see cref="FixedQuaternion"/>.</param>
        /// <param name="quaternion2">Source <see cref="FixedQuaternion"/>.</param>
        /// <returns>The result of the quaternion subtraction.</returns>
        public static FixedQuaternion Subtract(FixedQuaternion quaternion1, FixedQuaternion quaternion2)
        {
            FixedQuaternion quaternion;
		    quaternion.X = quaternion1.X - quaternion2.X;
		    quaternion.Y = quaternion1.Y - quaternion2.Y;
		    quaternion.Z = quaternion1.Z - quaternion2.Z;
		    quaternion.W = quaternion1.W - quaternion2.W;
		    return quaternion;
        }

        /// <summary>
        /// Creates a new <see cref="FixedQuaternion"/> that contains subtraction of one <see cref="FixedQuaternion"/> from another.
        /// </summary>
        /// <param name="quaternion1">Source <see cref="FixedQuaternion"/>.</param>
        /// <param name="quaternion2">Source <see cref="FixedQuaternion"/>.</param>
        /// <param name="result">The result of the quaternion subtraction as an output parameter.</param>
        public static void Subtract(ref FixedQuaternion quaternion1, ref FixedQuaternion quaternion2, out FixedQuaternion result)
        {
            result.X = quaternion1.X - quaternion2.X;
		    result.Y = quaternion1.Y - quaternion2.Y;
		    result.Z = quaternion1.Z - quaternion2.Z;
		    result.W = quaternion1.W - quaternion2.W;
        }

        #endregion

        #region Multiply

        /// <summary>
        /// Creates a new <see cref="FixedQuaternion"/> that contains a multiplication of two quaternions.
        /// </summary>
        /// <param name="quaternion1">Source <see cref="FixedQuaternion"/>.</param>
        /// <param name="quaternion2">Source <see cref="FixedQuaternion"/>.</param>
        /// <returns>The result of the quaternion multiplication.</returns>
        public static FixedQuaternion Multiply(FixedQuaternion quaternion1, FixedQuaternion quaternion2)
        {
            FixedQuaternion quaternion;
		    Fixed x = quaternion1.X;
		    Fixed y = quaternion1.Y;
		    Fixed z = quaternion1.Z;
		    Fixed w = quaternion1.W;
		    Fixed num4 = quaternion2.X;
		    Fixed num3 = quaternion2.Y;
		    Fixed num2 = quaternion2.Z;
		    Fixed num = quaternion2.W;
		    Fixed num12 = (y * num2) - (z * num3);
		    Fixed num11 = (z * num4) - (x * num2);
		    Fixed num10 = (x * num3) - (y * num4);
		    Fixed num9 = ((x * num4) + (y * num3)) + (z * num2);
		    quaternion.X = ((x * num) + (num4 * w)) + num12;
		    quaternion.Y = ((y * num) + (num3 * w)) + num11;
		    quaternion.Z = ((z * num) + (num2 * w)) + num10;
		    quaternion.W = (w * num) - num9;
		    return quaternion;
        }

        /// <summary>
        /// Creates a new <see cref="FixedQuaternion"/> that contains a multiplication of <see cref="FixedQuaternion"/> and a scalar.
        /// </summary>
        /// <param name="quaternion1">Source <see cref="FixedQuaternion"/>.</param>
        /// <param name="scaleFactor">Scalar value.</param>
        /// <returns>The result of the quaternion multiplication with a scalar.</returns>
        public static FixedQuaternion Multiply(FixedQuaternion quaternion1, Fixed scaleFactor)
        {
            FixedQuaternion quaternion;
		    quaternion.X = quaternion1.X * scaleFactor;
		    quaternion.Y = quaternion1.Y * scaleFactor;
		    quaternion.Z = quaternion1.Z * scaleFactor;
		    quaternion.W = quaternion1.W * scaleFactor;
		    return quaternion;
        }

        /// <summary>
        /// Creates a new <see cref="FixedQuaternion"/> that contains a multiplication of <see cref="FixedQuaternion"/> and a scalar.
        /// </summary>
        /// <param name="quaternion1">Source <see cref="FixedQuaternion"/>.</param>
        /// <param name="scaleFactor">Scalar value.</param>
        /// <param name="result">The result of the quaternion multiplication with a scalar as an output parameter.</param>
        public static void Multiply(ref FixedQuaternion quaternion1, Fixed scaleFactor, out FixedQuaternion result)
        {
            result.X = quaternion1.X * scaleFactor;
		    result.Y = quaternion1.Y * scaleFactor;
		    result.Z = quaternion1.Z * scaleFactor;
		    result.W = quaternion1.W * scaleFactor;
        }

        /// <summary>
        /// Creates a new <see cref="FixedQuaternion"/> that contains a multiplication of two quaternions.
        /// </summary>
        /// <param name="quaternion1">Source <see cref="FixedQuaternion"/>.</param>
        /// <param name="quaternion2">Source <see cref="FixedQuaternion"/>.</param>
        /// <param name="result">The result of the quaternion multiplication as an output parameter.</param>
        public static void Multiply(ref FixedQuaternion quaternion1, ref FixedQuaternion quaternion2, out FixedQuaternion result)
        {
            Fixed x = quaternion1.X;
		    Fixed y = quaternion1.Y;
		    Fixed z = quaternion1.Z;
		    Fixed w = quaternion1.W;
		    Fixed num4 = quaternion2.X;
		    Fixed num3 = quaternion2.Y;
		    Fixed num2 = quaternion2.Z;
		    Fixed num = quaternion2.W;
		    Fixed num12 = (y * num2) - (z * num3);
		    Fixed num11 = (z * num4) - (x * num2);
		    Fixed num10 = (x * num3) - (y * num4);
		    Fixed num9 = ((x * num4) + (y * num3)) + (z * num2);
		    result.X = ((x * num) + (num4 * w)) + num12;
		    result.Y = ((y * num) + (num3 * w)) + num11;
		    result.Z = ((z * num) + (num2 * w)) + num10;
		    result.W = (w * num) - num9;
        }

        #endregion

        #region Negate

        /// <summary>
        /// Flips the sign of the all the quaternion components.
        /// </summary>
        /// <param name="quaternion">Source <see cref="FixedQuaternion"/>.</param>
        /// <returns>The result of the quaternion negation.</returns>
        public static FixedQuaternion Negate(FixedQuaternion quaternion)
        {
		    return new FixedQuaternion(-quaternion.X, -quaternion.Y, -quaternion.Z, -quaternion.W);
        }

        /// <summary>
        /// Flips the sign of the all the quaternion components.
        /// </summary>
        /// <param name="quaternion">Source <see cref="FixedQuaternion"/>.</param>
        /// <param name="result">The result of the quaternion negation as an output parameter.</param>
        public static void Negate(ref FixedQuaternion quaternion, out FixedQuaternion result)
        {
            result.X = -quaternion.X;
		    result.Y = -quaternion.Y;
		    result.Z = -quaternion.Z;
		    result.W = -quaternion.W;
        }

        #endregion

        #region Normalize

        /// <summary>
        /// Scales the quaternion magnitude to unit length.
        /// </summary>
        public void Normalize()
        {
		    Fixed num = Fixed.One / Fixed.Sqrt((X * X) + (Y * Y) + (Z * Z) + (W * W));
		    X *= num;
		    Y *= num;
		    Z *= num;
		    W *= num;
        }

        /// <summary>
        /// Scales the quaternion magnitude to unit length.
        /// </summary>
        /// <param name="quaternion">Source <see cref="FixedQuaternion"/>.</param>
        /// <returns>The unit length quaternion.</returns>
        public static FixedQuaternion Normalize(FixedQuaternion quaternion)
        {
            FixedQuaternion result;
		    Fixed num = Fixed.One / Fixed.Sqrt((quaternion.X * quaternion.X) + (quaternion.Y * quaternion.Y) + (quaternion.Z * quaternion.Z) + (quaternion.W * quaternion.W));
            result.X = quaternion.X * num;
            result.Y = quaternion.Y * num;
            result.Z = quaternion.Z * num;
            result.W = quaternion.W * num;
		    return result;
        }

        /// <summary>
        /// Scales the quaternion magnitude to unit length.
        /// </summary>
        /// <param name="quaternion">Source <see cref="FixedQuaternion"/>.</param>
        /// <param name="result">The unit length quaternion an output parameter.</param>
        public static void Normalize(ref FixedQuaternion quaternion, out FixedQuaternion result)
        {
		    Fixed num = Fixed.One / Fixed.Sqrt((quaternion.X * quaternion.X) + (quaternion.Y * quaternion.Y) + (quaternion.Z * quaternion.Z) + (quaternion.W * quaternion.W));
		    result.X = quaternion.X * num;
		    result.Y = quaternion.Y * num;
		    result.Z = quaternion.Z * num;
		    result.W = quaternion.W * num;
        }

        #endregion

        /// <summary>
        /// Returns a <see cref="String"/> representation of this <see cref="FixedQuaternion"/> in the format:
        /// {X:[<see cref="X"/>] Y:[<see cref="Y"/>] Z:[<see cref="Z"/>] W:[<see cref="W"/>]}
        /// </summary>
        /// <returns>A <see cref="String"/> representation of this <see cref="FixedQuaternion"/>.</returns>
        public override string ToString()
        {
            return "{X:" + X + " Y:" + Y + " Z:" + Z + " W:" + W + "}";
        }

        /// <summary>
        /// Gets a <see cref="FixedVector4"/> representation for this object.
        /// </summary>
        /// <returns>A <see cref="FixedVector4"/> representation for this object.</returns>
        public FixedVector4 ToVector4()
        {
            return new FixedVector4(X,Y,Z,W);
        }

        public void Deconstruct(out Fixed x, out Fixed y, out Fixed z, out Fixed w)
        {
            x = X;
            y = Y;
            z = Z;
            w = W;
        }

        /// <summary>
        /// Returns a <see cref="System.Numerics.Quaternion"/>.
        /// </summary>
        public System.Numerics.Quaternion ToNumerics()
        {
            return new System.Numerics.Quaternion((float)this.X, (float)this.Y, (float)this.Z, (float)this.W);
        }

        #endregion

        #region Operators

        /// <summary>
        /// Converts a <see cref="System.Numerics.Quaternion"/> to a <see cref="FixedQuaternion"/>.
        /// </summary>
        /// <param name="value">The converted value.</param>
        public static implicit operator FixedQuaternion(System.Numerics.Quaternion value)
        {
            return new FixedQuaternion((Fixed)value.X, (Fixed)value.Y, (Fixed)value.Z, (Fixed)value.W);
        }

        /// <summary>
        /// Adds two quaternions.
        /// </summary>
        /// <param name="quaternion1">Source <see cref="FixedQuaternion"/> on the left of the add sign.</param>
        /// <param name="quaternion2">Source <see cref="FixedQuaternion"/> on the right of the add sign.</param>
        /// <returns>Sum of the vectors.</returns>
        public static FixedQuaternion operator +(FixedQuaternion quaternion1, FixedQuaternion quaternion2)
        {
            FixedQuaternion quaternion;
		    quaternion.X = quaternion1.X + quaternion2.X;
		    quaternion.Y = quaternion1.Y + quaternion2.Y;
		    quaternion.Z = quaternion1.Z + quaternion2.Z;
		    quaternion.W = quaternion1.W + quaternion2.W;
		    return quaternion;
        }

        /// <summary>
        /// Divides a <see cref="FixedQuaternion"/> by the other <see cref="FixedQuaternion"/>.
        /// </summary>
        /// <param name="quaternion1">Source <see cref="FixedQuaternion"/> on the left of the div sign.</param>
        /// <param name="quaternion2">Divisor <see cref="FixedQuaternion"/> on the right of the div sign.</param>
        /// <returns>The result of dividing the quaternions.</returns>
        public static FixedQuaternion operator /(FixedQuaternion quaternion1, FixedQuaternion quaternion2)
        {
            FixedQuaternion quaternion;
		    Fixed x = quaternion1.X;
		    Fixed y = quaternion1.Y;
		    Fixed z = quaternion1.Z;
		    Fixed w = quaternion1.W;
		    Fixed num14 = (((quaternion2.X * quaternion2.X) + (quaternion2.Y * quaternion2.Y)) + (quaternion2.Z * quaternion2.Z)) + (quaternion2.W * quaternion2.W);
		    Fixed num5 = Fixed.One / num14;
		    Fixed num4 = -quaternion2.X * num5;
		    Fixed num3 = -quaternion2.Y * num5;
		    Fixed num2 = -quaternion2.Z * num5;
		    Fixed num = quaternion2.W * num5;
		    Fixed num13 = (y * num2) - (z * num3);
		    Fixed num12 = (z * num4) - (x * num2);
		    Fixed num11 = (x * num3) - (y * num4);
		    Fixed num10 = ((x * num4) + (y * num3)) + (z * num2);
		    quaternion.X = ((x * num) + (num4 * w)) + num13;
		    quaternion.Y = ((y * num) + (num3 * w)) + num12;
		    quaternion.Z = ((z * num) + (num2 * w)) + num11;
		    quaternion.W = (w * num) - num10;
		    return quaternion;
        }

        /// <summary>
        /// Compares whether two <see cref="FixedQuaternion"/> instances are equal.
        /// </summary>
        /// <param name="quaternion1"><see cref="FixedQuaternion"/> instance on the left of the equal sign.</param>
        /// <param name="quaternion2"><see cref="FixedQuaternion"/> instance on the right of the equal sign.</param>
        /// <returns><c>true</c> if the instances are equal; <c>false</c> otherwise.</returns>
        public static bool operator ==(FixedQuaternion quaternion1, FixedQuaternion quaternion2)
        {
            return ((((quaternion1.X == quaternion2.X) && (quaternion1.Y == quaternion2.Y)) && (quaternion1.Z == quaternion2.Z)) && (quaternion1.W == quaternion2.W));
        }

        /// <summary>
        /// Compares whether two <see cref="FixedQuaternion"/> instances are not equal.
        /// </summary>
        /// <param name="quaternion1"><see cref="FixedQuaternion"/> instance on the left of the not equal sign.</param>
        /// <param name="quaternion2"><see cref="FixedQuaternion"/> instance on the right of the not equal sign.</param>
        /// <returns><c>true</c> if the instances are not equal; <c>false</c> otherwise.</returns>
        public static bool operator !=(FixedQuaternion quaternion1, FixedQuaternion quaternion2)
        {
            if (((quaternion1.X == quaternion2.X) && (quaternion1.Y == quaternion2.Y)) && (quaternion1.Z == quaternion2.Z))
		    {
		        return (quaternion1.W != quaternion2.W);
		    }
		    return true;
        }

        /// <summary>
        /// Multiplies two quaternions.
        /// </summary>
        /// <param name="quaternion1">Source <see cref="FixedQuaternion"/> on the left of the mul sign.</param>
        /// <param name="quaternion2">Source <see cref="FixedQuaternion"/> on the right of the mul sign.</param>
        /// <returns>Result of the quaternions multiplication.</returns>
        public static FixedQuaternion operator *(FixedQuaternion quaternion1, FixedQuaternion quaternion2)
        {
            FixedQuaternion quaternion;
		    Fixed x = quaternion1.X;
		    Fixed y = quaternion1.Y;
		    Fixed z = quaternion1.Z;
		    Fixed w = quaternion1.W;
		    Fixed num4 = quaternion2.X;
		    Fixed num3 = quaternion2.Y;
		    Fixed num2 = quaternion2.Z;
		    Fixed num = quaternion2.W;
		    Fixed num12 = (y * num2) - (z * num3);
		    Fixed num11 = (z * num4) - (x * num2);
		    Fixed num10 = (x * num3) - (y * num4);
		    Fixed num9 = ((x * num4) + (y * num3)) + (z * num2);
		    quaternion.X = ((x * num) + (num4 * w)) + num12;
		    quaternion.Y = ((y * num) + (num3 * w)) + num11;
		    quaternion.Z = ((z * num) + (num2 * w)) + num10;
		    quaternion.W = (w * num) - num9;
		    return quaternion;
        }

        /// <summary>
        /// Multiplies the components of quaternion by a scalar.
        /// </summary>
        /// <param name="quaternion1">Source <see cref="FixedVector3"/> on the left of the mul sign.</param>
        /// <param name="scaleFactor">Scalar value on the right of the mul sign.</param>
        /// <returns>Result of the quaternion multiplication with a scalar.</returns>
        public static FixedQuaternion operator *(FixedQuaternion quaternion1, Fixed scaleFactor)
        {
            FixedQuaternion quaternion;
		    quaternion.X = quaternion1.X * scaleFactor;
		    quaternion.Y = quaternion1.Y * scaleFactor;
		    quaternion.Z = quaternion1.Z * scaleFactor;
		    quaternion.W = quaternion1.W * scaleFactor;
		    return quaternion;
        }

        /// <summary>
        /// Subtracts a <see cref="FixedQuaternion"/> from a <see cref="FixedQuaternion"/>.
        /// </summary>
        /// <param name="quaternion1">Source <see cref="FixedVector3"/> on the left of the sub sign.</param>
        /// <param name="quaternion2">Source <see cref="FixedVector3"/> on the right of the sub sign.</param>
        /// <returns>Result of the quaternion subtraction.</returns>
        public static FixedQuaternion operator -(FixedQuaternion quaternion1, FixedQuaternion quaternion2)
        {
            FixedQuaternion quaternion;
		    quaternion.X = quaternion1.X - quaternion2.X;
		    quaternion.Y = quaternion1.Y - quaternion2.Y;
		    quaternion.Z = quaternion1.Z - quaternion2.Z;
		    quaternion.W = quaternion1.W - quaternion2.W;
		    return quaternion;

        }

        /// <summary>
        /// Flips the sign of the all the quaternion components.
        /// </summary>
        /// <param name="quaternion">Source <see cref="FixedQuaternion"/> on the right of the sub sign.</param>
        /// <returns>The result of the quaternion negation.</returns>
        public static FixedQuaternion operator -(FixedQuaternion quaternion)
        {
            FixedQuaternion quaternion2;
		    quaternion2.X = -quaternion.X;
		    quaternion2.Y = -quaternion.Y;
		    quaternion2.Z = -quaternion.Z;
		    quaternion2.W = -quaternion.W;
		    return quaternion2;
        }

        #endregion
    }
}
