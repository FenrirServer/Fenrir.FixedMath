using System;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace FixedMath
{
	internal class PlaneHelper
    {
        /// <summary>
        /// Returns a value indicating what side (positive/negative) of a plane a point is
        /// </summary>
        /// <param name="point">The point to check with</param>
        /// <param name="plane">The plane to check against</param>
        /// <returns>Greater than zero if on the positive side, less than zero if on the negative size, 0 otherwise</returns>
        public static Fixed ClassifyPoint(ref FixedVector3 point, ref FixedPlane plane)
        {
            return point.X * plane.Normal.X + point.Y * plane.Normal.Y + point.Z * plane.Normal.Z + plane.D;
        }

        /// <summary>
        /// Returns the perpendicular distance from a point to a plane
        /// </summary>
        /// <param name="point">The point to check</param>
        /// <param name="plane">The place to check</param>
        /// <returns>The perpendicular distance from the point to the plane</returns>
        public static Fixed PerpendicularDistance(ref FixedVector3 point, ref FixedPlane plane)
        {
            // dist = (ax + by + cz + d) / sqrt(a*a + b*b + c*c)
            return (Fixed)Fixed.Abs((plane.Normal.X * point.X + plane.Normal.Y * point.Y + plane.Normal.Z * point.Z)
                                    / Fixed.Sqrt(plane.Normal.X * plane.Normal.X + plane.Normal.Y * plane.Normal.Y + plane.Normal.Z * plane.Normal.Z));
        }
    }
	
    /// <summary>
    /// A plane in 3d space, represented by its normal away from the origin and its distance from the origin, D.
    /// </summary>
    [DataContract]
    [DebuggerDisplay("{DebugDisplayString,nq}")]
    public struct FixedPlane : IEquatable<FixedPlane>
    {
        #region Public Fields

        /// <summary>
        /// The distance of the <see cref="FixedPlane"/> to the origin.
        /// </summary>
        [DataMember]
        public Fixed D;

        /// <summary>
        /// The normal of the <see cref="FixedPlane"/>.
        /// </summary>
        [DataMember]
        public FixedVector3 Normal;

        #endregion Public Fields


        #region Constructors

        /// <summary>
        /// Create a <see cref="FixedPlane"/> with the first three components of the specified <see cref="FixedVector4"/>
        /// as the normal and the last component as the distance to the origin.
        /// </summary>
        /// <param name="value">A vector holding the normal and distance to origin.</param>
        public FixedPlane(FixedVector4 value)
            : this(new FixedVector3(value.X, value.Y, value.Z), value.W)
        {

        }

        /// <summary>
        /// Create a <see cref="FixedPlane"/> with the specified normal and distance to the origin.
        /// </summary>
        /// <param name="normal">The normal of the plane.</param>
        /// <param name="d">The distance to the origin.</param>
        internal FixedPlane(FixedVector3 normal, float d)
        {
            Normal = normal;
            D = (Fixed)d;
        }

        /// <summary>
        /// Create a <see cref="FixedPlane"/> with the specified normal and distance to the origin.
        /// </summary>
        /// <param name="normal">The normal of the plane.</param>
        /// <param name="d">The distance to the origin.</param>
        public FixedPlane(FixedVector3 normal, Fixed d)
        {
            Normal = normal;
            D = d;
        }

        /// <summary>
        /// Create the <see cref="FixedPlane"/> that contains the three specified points.
        /// </summary>
        /// <param name="a">A point the created <see cref="FixedPlane"/> should contain.</param>
        /// <param name="b">A point the created <see cref="FixedPlane"/> should contain.</param>
        /// <param name="c">A point the created <see cref="FixedPlane"/> should contain.</param>
        public FixedPlane(FixedVector3 a, FixedVector3 b, FixedVector3 c)
        {
            FixedVector3 ab = b - a;
            FixedVector3 ac = c - a;

            FixedVector3 cross = FixedVector3.Cross(ab, ac);
            FixedVector3.Normalize(ref cross, out Normal);
            D = -(FixedVector3.Dot(Normal, a));
        }

        /// <summary>
        /// Create a <see cref="FixedPlane"/> with the first three values as the X, Y and Z
        /// components of the normal and the last value as the distance to the origin.
        /// </summary>
        /// <param name="a">The X component of the normal.</param>
        /// <param name="b">The Y component of the normal.</param>
        /// <param name="c">The Z component of the normal.</param>
        /// <param name="d">The distance to the origin.</param>
        public FixedPlane(Fixed a, Fixed b, Fixed c, Fixed d)
            : this(new FixedVector3(a, b, c), d)
        {

        }

        /// <summary>
        /// Create a <see cref="FixedPlane"/> that contains the specified point and has the specified <see cref="Normal"/> vector.
        /// </summary>
        /// <param name="pointOnPlane">A point the created <see cref="FixedPlane"/> should contain.</param>
        /// <param name="normal">The normal of the plane.</param>
        public FixedPlane(FixedVector3 pointOnPlane, FixedVector3 normal)
        {
            Normal = normal;
            D = -(
                pointOnPlane.X * normal.X +
                pointOnPlane.Y * normal.Y +
                pointOnPlane.Z * normal.Z
            );
        }

        #endregion Constructors


        #region Public Methods

        /// <summary>
        /// Get the dot product of a <see cref="FixedVector4"/> with this <see cref="FixedPlane"/>.
        /// </summary>
        /// <param name="value">The <see cref="FixedVector4"/> to calculate the dot product with.</param>
        /// <returns>The dot product of the specified <see cref="FixedVector4"/> and this <see cref="FixedPlane"/>.</returns>
        public Fixed Dot(FixedVector4 value)
        {
            return ((((this.Normal.X * value.X) + (this.Normal.Y * value.Y)) + (this.Normal.Z * value.Z)) + (this.D * value.W));
        }

        /// <summary>
        /// Get the dot product of a <see cref="FixedVector4"/> with this <see cref="FixedPlane"/>.
        /// </summary>
        /// <param name="value">The <see cref="FixedVector4"/> to calculate the dot product with.</param>
        /// <param name="result">
        /// The dot product of the specified <see cref="FixedVector4"/> and this <see cref="FixedPlane"/>.
        /// </param>
        public void Dot(ref FixedVector4 value, out Fixed result)
        {
            result = (((this.Normal.X * value.X) + (this.Normal.Y * value.Y)) + (this.Normal.Z * value.Z)) + (this.D * value.W);
        }

        /// <summary>
        /// Get the dot product of a <see cref="FixedVector3"/> with
        /// the <see cref="Normal"/> vector of this <see cref="FixedPlane"/>
        /// plus the <see cref="D"/> value of this <see cref="FixedPlane"/>.
        /// </summary>
        /// <param name="value">The <see cref="FixedVector3"/> to calculate the dot product with.</param>
        /// <returns>
        /// The dot product of the specified <see cref="FixedVector3"/> and the normal of this <see cref="FixedPlane"/>
        /// plus the <see cref="D"/> value of this <see cref="FixedPlane"/>.
        /// </returns>
        public Fixed DotCoordinate(FixedVector3 value)
        {
            return ((((this.Normal.X * value.X) + (this.Normal.Y * value.Y)) + (this.Normal.Z * value.Z)) + this.D);
        }

        /// <summary>
        /// Get the dot product of a <see cref="FixedVector3"/> with
        /// the <see cref="Normal"/> vector of this <see cref="FixedPlane"/>
        /// plus the <see cref="D"/> value of this <see cref="FixedPlane"/>.
        /// </summary>
        /// <param name="value">The <see cref="FixedVector3"/> to calculate the dot product with.</param>
        /// <param name="result">
        /// The dot product of the specified <see cref="FixedVector3"/> and the normal of this <see cref="FixedPlane"/>
        /// plus the <see cref="D"/> value of this <see cref="FixedPlane"/>.
        /// </param>
        public void DotCoordinate(ref FixedVector3 value, out Fixed result)
        {
            result = (((this.Normal.X * value.X) + (this.Normal.Y * value.Y)) + (this.Normal.Z * value.Z)) + this.D;
        }

        /// <summary>
        /// Get the dot product of a <see cref="FixedVector3"/> with
        /// the <see cref="Normal"/> vector of this <see cref="FixedPlane"/>.
        /// </summary>
        /// <param name="value">The <see cref="FixedVector3"/> to calculate the dot product with.</param>
        /// <returns>
        /// The dot product of the specified <see cref="FixedVector3"/> and the normal of this <see cref="FixedPlane"/>.
        /// </returns>
        public Fixed DotNormal(FixedVector3 value)
        {
            return (((this.Normal.X * value.X) + (this.Normal.Y * value.Y)) + (this.Normal.Z * value.Z));
        }

        /// <summary>
        /// Get the dot product of a <see cref="FixedVector3"/> with
        /// the <see cref="Normal"/> vector of this <see cref="FixedPlane"/>.
        /// </summary>
        /// <param name="value">The <see cref="FixedVector3"/> to calculate the dot product with.</param>
        /// <param name="result">
        /// The dot product of the specified <see cref="FixedVector3"/> and the normal of this <see cref="FixedPlane"/>.
        /// </param>
        public void DotNormal(ref FixedVector3 value, out Fixed result)
        {
            result = ((this.Normal.X * value.X) + (this.Normal.Y * value.Y)) + (this.Normal.Z * value.Z);
        }

        /// <summary>
        /// Transforms a normalized plane by a matrix.
        /// </summary>
        /// <param name="plane">The normalized plane to transform.</param>
        /// <param name="matrix">The transformation matrix.</param>
        /// <returns>The transformed plane.</returns>
        public static FixedPlane Transform(FixedPlane plane, FixedMatrix matrix)
        {
            FixedPlane result;
            Transform(ref plane, ref matrix, out result);
            return result;
        }

        /// <summary>
        /// Transforms a normalized plane by a matrix.
        /// </summary>
        /// <param name="plane">The normalized plane to transform.</param>
        /// <param name="matrix">The transformation matrix.</param>
        /// <param name="result">The transformed plane.</param>
        public static void Transform(ref FixedPlane plane, ref FixedMatrix matrix, out FixedPlane result)
        {
            // See "Transforming Normals" in http://www.glprogramming.com/red/appendixf.html
            // for an explanation of how this works.

            FixedMatrix transformedMatrix;
            FixedMatrix.Invert(ref matrix, out transformedMatrix);
            FixedMatrix.Transpose(ref transformedMatrix, out transformedMatrix);

            var vector = new FixedVector4(plane.Normal, plane.D);

            FixedVector4 transformedVector;
            FixedVector4.Transform(ref vector, ref transformedMatrix, out transformedVector);

            result = new FixedPlane(transformedVector);
        }

        /// <summary>
        /// Transforms a normalized plane by a quaternion rotation.
        /// </summary>
        /// <param name="plane">The normalized plane to transform.</param>
        /// <param name="rotation">The quaternion rotation.</param>
        /// <returns>The transformed plane.</returns>
        public static FixedPlane Transform(FixedPlane plane, FixedQuaternion rotation)
        {
            FixedPlane result;
            Transform(ref plane, ref rotation, out result);
            return result;
        }

        /// <summary>
        /// Transforms a normalized plane by a quaternion rotation.
        /// </summary>
        /// <param name="plane">The normalized plane to transform.</param>
        /// <param name="rotation">The quaternion rotation.</param>
        /// <param name="result">The transformed plane.</param>
        public static void Transform(ref FixedPlane plane, ref FixedQuaternion rotation, out FixedPlane result)
        {
            FixedVector3.Transform(ref plane.Normal, ref rotation, out result.Normal);
            result.D = plane.D;
        }

        /// <summary>
        /// Normalize the normal vector of this plane.
        /// </summary>
        public void Normalize()
        {
            Fixed length = Normal.Length();
            Fixed factor =  Fixed.One / length;            
            FixedVector3.Multiply(ref Normal, factor, out Normal);
            D = D * factor;
        }

        /// <summary>
        /// Get a normalized version of the specified plane.
        /// </summary>
        /// <param name="value">The <see cref="FixedPlane"/> to normalize.</param>
        /// <returns>A normalized version of the specified <see cref="FixedPlane"/>.</returns>
        public static FixedPlane Normalize(FixedPlane value)
        {
			FixedPlane ret;
			Normalize(ref value, out ret);
			return ret;
        }

        /// <summary>
        /// Get a normalized version of the specified plane.
        /// </summary>
        /// <param name="value">The <see cref="FixedPlane"/> to normalize.</param>
        /// <param name="result">A normalized version of the specified <see cref="FixedPlane"/>.</param>
        public static void Normalize(ref FixedPlane value, out FixedPlane result)
        {
            Fixed length = value.Normal.Length();
            Fixed factor =  Fixed.One / length;            
            FixedVector3.Multiply(ref value.Normal, factor, out result.Normal);
            result.D = value.D * factor;
        }

        /// <summary>
        /// Check if two planes are not equal.
        /// </summary>
        /// <param name="plane1">A <see cref="FixedPlane"/> to check for inequality.</param>
        /// <param name="plane2">A <see cref="FixedPlane"/> to check for inequality.</param>
        /// <returns><code>true</code> if the two planes are not equal, <code>false</code> if they are.</returns>
        public static bool operator !=(FixedPlane plane1, FixedPlane plane2)
        {
            return !plane1.Equals(plane2);
        }

        /// <summary>
        /// Check if two planes are equal.
        /// </summary>
        /// <param name="plane1">A <see cref="FixedPlane"/> to check for equality.</param>
        /// <param name="plane2">A <see cref="FixedPlane"/> to check for equality.</param>
        /// <returns><code>true</code> if the two planes are equal, <code>false</code> if they are not.</returns>
        public static bool operator ==(FixedPlane plane1, FixedPlane plane2)
        {
            return plane1.Equals(plane2);
        }

        /// <summary>
        /// Check if this <see cref="FixedPlane"/> is equal to another <see cref="FixedPlane"/>.
        /// </summary>
        /// <param name="other">An <see cref="Object"/> to check for equality with this <see cref="FixedPlane"/>.</param>
        /// <returns>
        /// <code>true</code> if the specified <see cref="object"/> is equal to this <see cref="FixedPlane"/>,
        /// <code>false</code> if it is not.
        /// </returns>
        public override bool Equals(object other)
        {
            return (other is FixedPlane) ? this.Equals((FixedPlane)other) : false;
        }

        /// <summary>
        /// Check if this <see cref="FixedPlane"/> is equal to another <see cref="FixedPlane"/>.
        /// </summary>
        /// <param name="other">A <see cref="FixedPlane"/> to check for equality with this <see cref="FixedPlane"/>.</param>
        /// <returns>
        /// <code>true</code> if the specified <see cref="FixedPlane"/> is equal to this one,
        /// <code>false</code> if it is not.
        /// </returns>
        public bool Equals(FixedPlane other)
        {
            return ((Normal == other.Normal) && (D == other.D));
        }

        /// <summary>
        /// Get a hash code for this <see cref="FixedPlane"/>.
        /// </summary>
        /// <returns>A hash code for this <see cref="FixedPlane"/>.</returns>
        public override int GetHashCode()
        {
            return Normal.GetHashCode() ^ D.GetHashCode();
        }


        /// <summary>
        /// Check if this <see cref="FixedPlane"/> intersects a <see cref="FixedBoundingBox"/>.
        /// </summary>
        /// <param name="box">The <see cref="FixedBoundingBox"/> to test for intersection.</param>
        /// <returns>
        /// The type of intersection of this <see cref="FixedPlane"/> with the specified <see cref="FixedBoundingBox"/>.
        /// </returns>
        public FixedPlaneIntersectionType Intersects(FixedBoundingBox box)
        {
            return box.Intersects(this);
        }

        /// <summary>
        /// Check if this <see cref="FixedPlane"/> intersects a <see cref="FixedBoundingBox"/>.
        /// </summary>
        /// <param name="box">The <see cref="FixedBoundingBox"/> to test for intersection.</param>
        /// <param name="result">
        /// The type of intersection of this <see cref="FixedPlane"/> with the specified <see cref="FixedBoundingBox"/>.
        /// </param>
        public void Intersects(ref FixedBoundingBox box, out FixedPlaneIntersectionType result)
        {
            box.Intersects (ref this, out result);
        }

        /// <summary>
        /// Check if this <see cref="FixedPlane"/> intersects a <see cref="FixedBoundingFrustum"/>.
        /// </summary>
        /// <param name="frustum">The <see cref="FixedBoundingFrustum"/> to test for intersection.</param>
        /// <returns>
        /// The type of intersection of this <see cref="FixedPlane"/> with the specified <see cref="FixedBoundingFrustum"/>.
        /// </returns>
        public FixedPlaneIntersectionType Intersects(FixedBoundingFrustum frustum)
        {
            return frustum.Intersects(this);
        }

        /// <summary>
        /// Check if this <see cref="FixedPlane"/> intersects a <see cref="FixedBoundingSphere"/>.
        /// </summary>
        /// <param name="sphere">The <see cref="FixedBoundingSphere"/> to test for intersection.</param>
        /// <returns>
        /// The type of intersection of this <see cref="FixedPlane"/> with the specified <see cref="FixedBoundingSphere"/>.
        /// </returns>
        public FixedPlaneIntersectionType Intersects(FixedBoundingSphere sphere)
        {
            return sphere.Intersects(this);
        }

        /// <summary>
        /// Check if this <see cref="FixedPlane"/> intersects a <see cref="FixedBoundingSphere"/>.
        /// </summary>
        /// <param name="sphere">The <see cref="FixedBoundingSphere"/> to test for intersection.</param>
        /// <param name="result">
        /// The type of intersection of this <see cref="FixedPlane"/> with the specified <see cref="FixedBoundingSphere"/>.
        /// </param>
        public void Intersects(ref FixedBoundingSphere sphere, out FixedPlaneIntersectionType result)
        {
            sphere.Intersects(ref this, out result);
        }

        internal FixedPlaneIntersectionType Intersects(ref FixedVector3 point)
        {
            Fixed distance;
            DotCoordinate(ref point, out distance);

            if (distance > Fixed.Zero)
                return FixedPlaneIntersectionType.Front;

            if (distance < Fixed.Zero)
                return FixedPlaneIntersectionType.Back;

            return FixedPlaneIntersectionType.Intersecting;
        }

        internal string DebugDisplayString
        {
            get
            {
                return string.Concat(
                    this.Normal.DebugDisplayString, "  ",
                    this.D.ToString()
                    );
            }
        }

        /// <summary>
        /// Get a <see cref="String"/> representation of this <see cref="FixedPlane"/>.
        /// </summary>
        /// <returns>A <see cref="String"/> representation of this <see cref="FixedPlane"/>.</returns>
        public override string ToString()
        {
            return "{Normal:" + Normal + " D:" + D + "}";
        }

        /// <summary>
        /// Deconstruction method for <see cref="FixedPlane"/>.
        /// </summary>
        /// <param name="normal"></param>
        /// <param name="d"></param>
        public void Deconstruct(out FixedVector3 normal, out Fixed d)
        {
            normal = Normal;
            d = D;
        }

        /// <summary>
        /// Returns a <see cref="System.Numerics.Plane"/>.
        /// </summary>
        public System.Numerics.Plane ToNumerics()
        {
            return new System.Numerics.Plane((float)this.Normal.X, (float)this.Normal.Y, (float)this.Normal.Z, (float)this.D);
        }

        #endregion

        #region Operators

        /// <summary>
        /// Converts a <see cref="System.Numerics.Plane"/> to a <see cref="FixedPlane"/>.
        /// </summary>
        /// <param name="value">The converted value.</param>
        public static implicit operator FixedPlane(System.Numerics.Plane value)
        {
            return new FixedPlane(value.Normal, (Fixed)value.D);
        }

        #endregion
    }
}

