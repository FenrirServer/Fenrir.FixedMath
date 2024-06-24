using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace FixedMath
{
    /// <summary>
    /// Represents an axis-aligned bounding box (AABB) in 3D space.
    /// </summary>
    [DataContract]
    [DebuggerDisplay("{DebugDisplayString,nq}")]
    public struct FixedBoundingBox : IEquatable<FixedBoundingBox>
    {

        #region Public Fields

        /// <summary>
        ///   The minimum extent of this <see cref="FixedBoundingBox"/>.
        /// </summary>
        [DataMember]
        public FixedVector3 Min;
      
        /// <summary>
        ///   The maximum extent of this <see cref="FixedBoundingBox"/>.
        /// </summary>
        [DataMember]
        public FixedVector3 Max;

        /// <summary>
        ///   The number of corners in a <see cref="FixedBoundingBox"/>. This is equal to 8.
        /// </summary>
        public const int CornerCount = 8;

        #endregion Public Fields


        #region Public Constructors

        /// <summary>
        ///   Create a <see cref="FixedBoundingBox"/>.
        /// </summary>
        /// <param name="min">The minimum extent of the <see cref="FixedBoundingBox"/>.</param>
        /// <param name="max">The maximum extent of the <see cref="FixedBoundingBox"/>.</param>
        public FixedBoundingBox(FixedVector3 min, FixedVector3 max)
        {
            this.Min = min;
            this.Max = max;
        }

        #endregion Public Constructors


        #region Public Methods

        /// <summary>
        ///   Check if this <see cref="FixedBoundingBox"/> contains another <see cref="FixedBoundingBox"/>.
        /// </summary>
        /// <param name="box">The <see cref="FixedBoundingBox"/> to test for overlap.</param>
        /// <returns>
        ///   A value indicating if this <see cref="FixedBoundingBox"/> contains,
        ///   intersects with or is disjoint with <paramref name="box"/>.
        /// </returns>
        public ContainmentType Contains(FixedBoundingBox box)
        {
            //test if all corner is in the same side of a face by just checking min and max
            if (box.Max.X < Min.X
                || box.Min.X > Max.X
                || box.Max.Y < Min.Y
                || box.Min.Y > Max.Y
                || box.Max.Z < Min.Z
                || box.Min.Z > Max.Z)
                return ContainmentType.Disjoint;


            if (box.Min.X >= Min.X
                && box.Max.X <= Max.X
                && box.Min.Y >= Min.Y
                && box.Max.Y <= Max.Y
                && box.Min.Z >= Min.Z
                && box.Max.Z <= Max.Z)
                return ContainmentType.Contains;

            return ContainmentType.Intersects;
        }

        /// <summary>
        ///   Check if this <see cref="FixedBoundingBox"/> contains another <see cref="FixedBoundingBox"/>.
        /// </summary>
        /// <param name="box">The <see cref="FixedBoundingBox"/> to test for overlap.</param>
        /// <param name="result">
        ///   A value indicating if this <see cref="FixedBoundingBox"/> contains,
        ///   intersects with or is disjoint with <paramref name="box"/>.
        /// </param>
        public void Contains(ref FixedBoundingBox box, out ContainmentType result)
        {
            result = Contains(box);
        }

        /// <summary>
        ///   Check if this <see cref="FixedBoundingBox"/> contains a <see cref="FixedBoundingFrustum"/>.
        /// </summary>
        /// <param name="frustum">The <see cref="FixedBoundingFrustum"/> to test for overlap.</param>
        /// <returns>
        ///   A value indicating if this <see cref="FixedBoundingBox"/> contains,
        ///   intersects with or is disjoint with <paramref name="frustum"/>.
        /// </returns>
        public ContainmentType Contains(FixedBoundingFrustum frustum)
        {
            //TODO: bad done here need a fix. 
            //Because question is not frustum contain box but reverse and this is not the same
            int i;
            ContainmentType contained;
            FixedVector3[] corners = frustum.GetCorners();

            // First we check if frustum is in box
            for (i = 0; i < corners.Length; i++)
            {
                this.Contains(ref corners[i], out contained);
                if (contained == ContainmentType.Disjoint)
                    break;
            }

            if (i == corners.Length) // This means we checked all the corners and they were all contain or instersect
                return ContainmentType.Contains;

            if (i != 0)             // if i is not equal to zero, we can fastpath and say that this box intersects
                return ContainmentType.Intersects;


            // If we get here, it means the first (and only) point we checked was actually contained in the frustum.
            // So we assume that all other points will also be contained. If one of the points is disjoint, we can
            // exit immediately saying that the result is Intersects
            i++;
            for (; i < corners.Length; i++)
            {
                this.Contains(ref corners[i], out contained);
                if (contained != ContainmentType.Contains)
                    return ContainmentType.Intersects;

            }

            // If we get here, then we know all the points were actually contained, therefore result is Contains
            return ContainmentType.Contains;
        }

        /// <summary>
        ///   Check if this <see cref="FixedBoundingBox"/> contains a <see cref="FixedBoundingSphere"/>.
        /// </summary>
        /// <param name="sphere">The <see cref="FixedBoundingSphere"/> to test for overlap.</param>
        /// <returns>
        ///   A value indicating if this <see cref="FixedBoundingBox"/> contains,
        ///   intersects with or is disjoint with <paramref name="sphere"/>.
        /// </returns>
        public ContainmentType Contains(FixedBoundingSphere sphere)
        {
            if (sphere.Center.X - Min.X >= sphere.Radius
                && sphere.Center.Y - Min.Y >= sphere.Radius
                && sphere.Center.Z - Min.Z >= sphere.Radius
                && Max.X - sphere.Center.X >= sphere.Radius
                && Max.Y - sphere.Center.Y >= sphere.Radius
                && Max.Z - sphere.Center.Z >= sphere.Radius)
                return ContainmentType.Contains;

            Fixed dmin = Fixed.Zero;

            Fixed e = sphere.Center.X - Min.X;
            if (e < Fixed.Zero)
            {
                if (e < -sphere.Radius)
                {
                    return ContainmentType.Disjoint;
                }
                dmin += e * e;
            }
            else
            {
                e = sphere.Center.X - Max.X;
                if (e > Fixed.Zero)
                {
                    if (e > sphere.Radius)
                    {
                        return ContainmentType.Disjoint;
                    }
                    dmin += e * e;
                }
            }

            e = sphere.Center.Y - Min.Y;
            if (e < Fixed.Zero)
            {
                if (e < -sphere.Radius)
                {
                    return ContainmentType.Disjoint;
                }
                dmin += e * e;
            }
            else
            {
                e = sphere.Center.Y - Max.Y;
                if (e > Fixed.Zero)
                {
                    if (e > sphere.Radius)
                    {
                        return ContainmentType.Disjoint;
                    }
                    dmin += e * e;
                }
            }

            e = sphere.Center.Z - Min.Z;
            if (e < Fixed.Zero)
            {
                if (e < -sphere.Radius)
                {
                    return ContainmentType.Disjoint;
                }
                dmin += e * e;
            }
            else
            {
                e = sphere.Center.Z - Max.Z;
                if (e > Fixed.Zero)
                {
                    if (e > sphere.Radius)
                    {
                        return ContainmentType.Disjoint;
                    }
                    dmin += e * e;
                }
            }

            if (dmin <= sphere.Radius * sphere.Radius)
                return ContainmentType.Intersects;

            return ContainmentType.Disjoint;
        }

        /// <summary>
        ///   Check if this <see cref="FixedBoundingBox"/> contains a <see cref="FixedBoundingSphere"/>.
        /// </summary>
        /// <param name="sphere">The <see cref="FixedBoundingSphere"/> to test for overlap.</param>
        /// <param name="result">
        ///   A value indicating if this <see cref="FixedBoundingBox"/> contains,
        ///   intersects with or is disjoint with <paramref name="sphere"/>.
        /// </param>
        public void Contains(ref FixedBoundingSphere sphere, out ContainmentType result)
        {
            result = this.Contains(sphere);
        }

        /// <summary>
        ///   Check if this <see cref="FixedBoundingBox"/> contains a point.
        /// </summary>
        /// <param name="point">The <see cref="FixedVector3"/> to test.</param>
        /// <returns>
        ///   <see cref="ContainmentType.Contains"/> if this <see cref="FixedBoundingBox"/> contains
        ///   <paramref name="point"/> or <see cref="ContainmentType.Disjoint"/> if it does not.
        /// </returns>
        public ContainmentType Contains(FixedVector3 point)
        {
            ContainmentType result;
            this.Contains(ref point, out result);
            return result;
        }

        /// <summary>
        ///   Check if this <see cref="FixedBoundingBox"/> contains a point.
        /// </summary>
        /// <param name="point">The <see cref="FixedVector3"/> to test.</param>
        /// <param name="result">
        ///   <see cref="ContainmentType.Contains"/> if this <see cref="FixedBoundingBox"/> contains
        ///   <paramref name="point"/> or <see cref="ContainmentType.Disjoint"/> if it does not.
        /// </param>
        public void Contains(ref FixedVector3 point, out ContainmentType result)
        {
            //first we get if point is out of box
            if (point.X < this.Min.X
                || point.X > this.Max.X
                || point.Y < this.Min.Y
                || point.Y > this.Max.Y
                || point.Z < this.Min.Z
                || point.Z > this.Max.Z)
            {
                result = ContainmentType.Disjoint;
            }
            else
            {
                result = ContainmentType.Contains;
            }
        }

        private static readonly FixedVector3 MaxVector3 = new FixedVector3(Fixed.MaxValue);
        private static readonly FixedVector3 MinVector3 = new FixedVector3(Fixed.MinValue);


        /// <summary>
        /// Create a bounding box from the given list of points.
        /// </summary>
        /// <param name="points">The array of Vector3 instances defining the point cloud to bound</param>
        /// <param name="index">The base index to start iterating from</param>
        /// <param name="count">The number of points to iterate</param>
        /// <returns>A bounding box that encapsulates the given point cloud.</returns>
        /// <exception cref="System.ArgumentException">Thrown if the given array is null or has no points.</exception>
        public static FixedBoundingBox CreateFromPoints(FixedVector3[] points, int index = 0, int count = -1)
        {
            if (points == null || points.Length == 0)
                throw new ArgumentException();

            if (count == -1)
                count = points.Length;

            var minVec = MaxVector3;
            var maxVec = MinVector3;
            for (int i = index; i < count; i++)
            {                
                minVec.X = (minVec.X < points[i].X) ? minVec.X : points[i].X;
                minVec.Y = (minVec.Y < points[i].Y) ? minVec.Y : points[i].Y;
                minVec.Z = (minVec.Z < points[i].Z) ? minVec.Z : points[i].Z;

                maxVec.X = (maxVec.X > points[i].X) ? maxVec.X : points[i].X;
                maxVec.Y = (maxVec.Y > points[i].Y) ? maxVec.Y : points[i].Y;
                maxVec.Z = (maxVec.Z > points[i].Z) ? maxVec.Z : points[i].Z;
            }

            return new FixedBoundingBox(minVec, maxVec);
        }


        /// <summary>
        /// Create a bounding box from the given list of points.
        /// </summary>
        /// <param name="points">The list of Vector3 instances defining the point cloud to bound</param>
        /// <param name="index">The base index to start iterating from</param>
        /// <param name="count">The number of points to iterate</param>
        /// <returns>A bounding box that encapsulates the given point cloud.</returns>
        /// <exception cref="System.ArgumentException">Thrown if the given list is null or has no points.</exception>
        public static FixedBoundingBox CreateFromPoints(List<FixedVector3> points, int index = 0, int count = -1)
        {
            if (points == null || points.Count == 0)
                throw new ArgumentException();

            if (count == -1)
                count = points.Count;

            var minVec = MaxVector3;
            var maxVec = MinVector3;
            for (int i = index; i < count; i++)
            {
                minVec.X = (minVec.X < points[i].X) ? minVec.X : points[i].X;
                minVec.Y = (minVec.Y < points[i].Y) ? minVec.Y : points[i].Y;
                minVec.Z = (minVec.Z < points[i].Z) ? minVec.Z : points[i].Z;

                maxVec.X = (maxVec.X > points[i].X) ? maxVec.X : points[i].X;
                maxVec.Y = (maxVec.Y > points[i].Y) ? maxVec.Y : points[i].Y;
                maxVec.Z = (maxVec.Z > points[i].Z) ? maxVec.Z : points[i].Z;
            }

            return new FixedBoundingBox(minVec, maxVec);
        }


        /// <summary>
        ///   Create the enclosing <see cref="FixedBoundingBox"/> from the given list of points.
        /// </summary>
        /// <param name="points">The list of <see cref="FixedVector3"/> instances defining the point cloud to bound.</param>
        /// <returns>A <see cref="FixedBoundingBox"/> that encloses the given point cloud.</returns>
        /// <exception cref="System.ArgumentException">Thrown if the given list has no points.</exception>
        public static FixedBoundingBox CreateFromPoints(IEnumerable<FixedVector3> points)
        {
            if (points == null)
                throw new ArgumentNullException();

            var empty = true;
            var minVec = MaxVector3;
            var maxVec = MinVector3;
            foreach (var ptVector in points)
            {
                minVec.X = (minVec.X < ptVector.X) ? minVec.X : ptVector.X;
                minVec.Y = (minVec.Y < ptVector.Y) ? minVec.Y : ptVector.Y;
                minVec.Z = (minVec.Z < ptVector.Z) ? minVec.Z : ptVector.Z;

                maxVec.X = (maxVec.X > ptVector.X) ? maxVec.X : ptVector.X;
                maxVec.Y = (maxVec.Y > ptVector.Y) ? maxVec.Y : ptVector.Y;
                maxVec.Z = (maxVec.Z > ptVector.Z) ? maxVec.Z : ptVector.Z;

                empty = false;
            }
            if (empty)
                throw new ArgumentException();

            return new FixedBoundingBox(minVec, maxVec);
        }

        /// <summary>
        ///   Create the enclosing <see cref="FixedBoundingBox"/> of a <see cref="FixedBoundingSphere"/>.
        /// </summary>
        /// <param name="sphere">The <see cref="FixedBoundingSphere"/> to enclose.</param>
        /// <returns>A <see cref="FixedBoundingBox"/> enclosing <paramref name="sphere"/>.</returns>
        public static FixedBoundingBox CreateFromSphere(FixedBoundingSphere sphere)
        {
            FixedBoundingBox result;
            CreateFromSphere(ref sphere, out result);
            return result;
        }

        /// <summary>
        ///   Create the enclosing <see cref="FixedBoundingBox"/> of a <see cref="FixedBoundingSphere"/>.
        /// </summary>
        /// <param name="sphere">The <see cref="FixedBoundingSphere"/> to enclose.</param>
        /// <param name="result">A <see cref="FixedBoundingBox"/> enclosing <paramref name="sphere"/>.</param>
        public static void CreateFromSphere(ref FixedBoundingSphere sphere, out FixedBoundingBox result)
        {
            var corner = new FixedVector3(sphere.Radius);
            result.Min = sphere.Center - corner;
            result.Max = sphere.Center + corner;
        }

        /// <summary>
        ///   Create the <see cref="FixedBoundingBox"/> enclosing two other <see cref="FixedBoundingBox"/> instances.
        /// </summary>
        /// <param name="original">A <see cref="FixedBoundingBox"/> to enclose.</param>
        /// <param name="additional">A <see cref="FixedBoundingBox"/> to enclose.</param>
        /// <returns>
        ///   The <see cref="FixedBoundingBox"/> enclosing <paramref name="original"/> and <paramref name="additional"/>.
        /// </returns>
        public static FixedBoundingBox CreateMerged(FixedBoundingBox original, FixedBoundingBox additional)
        {
            FixedBoundingBox result;
            CreateMerged(ref original, ref additional, out result);
            return result;
        }

        /// <summary>
        ///   Create the <see cref="FixedBoundingBox"/> enclosing two other <see cref="FixedBoundingBox"/> instances.
        /// </summary>
        /// <param name="original">A <see cref="FixedBoundingBox"/> to enclose.</param>
        /// <param name="additional">A <see cref="FixedBoundingBox"/> to enclose.</param>
        /// <param name="result">
        ///   The <see cref="FixedBoundingBox"/> enclosing <paramref name="original"/> and <paramref name="additional"/>.
        /// </param>
        public static void CreateMerged(ref FixedBoundingBox original, ref FixedBoundingBox additional, out FixedBoundingBox result)
        {
            result.Min.X = Fixed.Min(original.Min.X, additional.Min.X);
            result.Min.Y = Fixed.Min(original.Min.Y, additional.Min.Y);
            result.Min.Z = Fixed.Min(original.Min.Z, additional.Min.Z);
            result.Max.X = Fixed.Max(original.Max.X, additional.Max.X);
            result.Max.Y = Fixed.Max(original.Max.Y, additional.Max.Y);
            result.Max.Z = Fixed.Max(original.Max.Z, additional.Max.Z);
        }

        /// <summary>
        ///   Check if two <see cref="FixedBoundingBox"/> instances are equal.
        /// </summary>
        /// <param name="other">The <see cref="FixedBoundingBox"/> to compare with this <see cref="FixedBoundingBox"/>.</param>
        /// <returns>
        ///   <code>true</code> if <see cref="other"/> is equal to this <see cref="FixedBoundingBox"/>,
        ///   <code>false</code> if it is not.
        /// </returns>
        public bool Equals(FixedBoundingBox other)
        {
            return (this.Min == other.Min) && (this.Max == other.Max);
        }

        /// <summary>
        ///   Check if two <see cref="FixedBoundingBox"/> instances are equal.
        /// </summary>
        /// <param name="obj">The <see cref="Object"/> to compare with this <see cref="FixedBoundingBox"/>.</param>
        /// <returns>
        ///   <code>true</code> if <see cref="obj"/> is equal to this <see cref="FixedBoundingBox"/>,
        ///   <code>false</code> if it is not.
        /// </returns>
        public override bool Equals(object obj)
        {
            return (obj is FixedBoundingBox) && this.Equals((FixedBoundingBox)obj);
        }

        /// <summary>
        ///   Get an array of <see cref="FixedVector3"/> containing the corners of this <see cref="FixedBoundingBox"/>.
        /// </summary>
        /// <returns>An array of <see cref="FixedVector3"/> containing the corners of this <see cref="FixedBoundingBox"/>.</returns>
        public FixedVector3[] GetCorners()
        {
            return new FixedVector3[] {
                new FixedVector3(this.Min.X, this.Max.Y, this.Max.Z), 
                new FixedVector3(this.Max.X, this.Max.Y, this.Max.Z),
                new FixedVector3(this.Max.X, this.Min.Y, this.Max.Z), 
                new FixedVector3(this.Min.X, this.Min.Y, this.Max.Z), 
                new FixedVector3(this.Min.X, this.Max.Y, this.Min.Z),
                new FixedVector3(this.Max.X, this.Max.Y, this.Min.Z),
                new FixedVector3(this.Max.X, this.Min.Y, this.Min.Z),
                new FixedVector3(this.Min.X, this.Min.Y, this.Min.Z)
            };
        }

        /// <summary>
        ///   Fill the first 8 places of an array of <see cref="FixedVector3"/>
        ///   with the corners of this <see cref="FixedBoundingBox"/>.
        /// </summary>
        /// <param name="corners">The array to fill.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="corners"/> is <code>null</code>.</exception>
        /// <exception cref="ArgumentOutOfRangeException">
        ///   If <paramref name="corners"/> has a length of less than 8.
        /// </exception>
        public void GetCorners(FixedVector3[] corners)
        {
            if (corners == null)
            {
                throw new ArgumentNullException("corners");
            }
            if (corners.Length < 8)
            {
                throw new ArgumentOutOfRangeException("corners", "Not Enought Corners");
            }
            corners[0].X = this.Min.X;
            corners[0].Y = this.Max.Y;
            corners[0].Z = this.Max.Z;
            corners[1].X = this.Max.X;
            corners[1].Y = this.Max.Y;
            corners[1].Z = this.Max.Z;
            corners[2].X = this.Max.X;
            corners[2].Y = this.Min.Y;
            corners[2].Z = this.Max.Z;
            corners[3].X = this.Min.X;
            corners[3].Y = this.Min.Y;
            corners[3].Z = this.Max.Z;
            corners[4].X = this.Min.X;
            corners[4].Y = this.Max.Y;
            corners[4].Z = this.Min.Z;
            corners[5].X = this.Max.X;
            corners[5].Y = this.Max.Y;
            corners[5].Z = this.Min.Z;
            corners[6].X = this.Max.X;
            corners[6].Y = this.Min.Y;
            corners[6].Z = this.Min.Z;
            corners[7].X = this.Min.X;
            corners[7].Y = this.Min.Y;
            corners[7].Z = this.Min.Z;
        }

        /// <summary>
        ///   Get the hash code for this <see cref="FixedBoundingBox"/>.
        /// </summary>
        /// <returns>A hash code for this <see cref="FixedBoundingBox"/>.</returns>
        public override int GetHashCode()
        {
            return this.Min.GetHashCode() + this.Max.GetHashCode();
        }

        /// <summary>
        ///   Check if this <see cref="FixedBoundingBox"/> intersects another <see cref="FixedBoundingBox"/>.
        /// </summary>
        /// <param name="box">The <see cref="FixedBoundingBox"/> to test for intersection.</param>
        /// <returns>
        ///   <code>true</code> if this <see cref="FixedBoundingBox"/> intersects <paramref name="box"/>,
        ///   <code>false</code> if it does not.
        /// </returns>
        public bool Intersects(FixedBoundingBox box)
        {
            bool result;
            Intersects(ref box, out result);
            return result;
        }

        /// <summary>
        ///   Check if this <see cref="FixedBoundingBox"/> intersects another <see cref="FixedBoundingBox"/>.
        /// </summary>
        /// <param name="box">The <see cref="FixedBoundingBox"/> to test for intersection.</param>
        /// <param name="result">
        ///   <code>true</code> if this <see cref="FixedBoundingBox"/> intersects <paramref name="box"/>,
        ///   <code>false</code> if it does not.
        /// </param>
        public void Intersects(ref FixedBoundingBox box, out bool result)
        {
            if ((this.Max.X >= box.Min.X) && (this.Min.X <= box.Max.X))
            {
                if ((this.Max.Y < box.Min.Y) || (this.Min.Y > box.Max.Y))
                {
                    result = false;
                    return;
                }

                result = (this.Max.Z >= box.Min.Z) && (this.Min.Z <= box.Max.Z);
                return;
            }

            result = false;
            return;
        }

        /// <summary>
        ///   Check if this <see cref="FixedBoundingBox"/> intersects a <see cref="FixedBoundingFrustum"/>.
        /// </summary>
        /// <param name="frustum">The <see cref="FixedBoundingFrustum"/> to test for intersection.</param>
        /// <returns>
        ///   <code>true</code> if this <see cref="FixedBoundingBox"/> intersects <paramref name="frustum"/>,
        ///   <code>false</code> if it does not.
        /// </returns>
        public bool Intersects(FixedBoundingFrustum frustum)
        {
            return frustum.Intersects(this);
        }

        /// <summary>
        ///   Check if this <see cref="FixedBoundingBox"/> intersects a <see cref="FixedBoundingFrustum"/>.
        /// </summary>
        /// <param name="sphere">The <see cref="FixedBoundingFrustum"/> to test for intersection.</param>
        /// <returns>
        ///   <code>true</code> if this <see cref="FixedBoundingBox"/> intersects <paramref name="sphere"/>,
        ///   <code>false</code> if it does not.
        /// </returns>
        public bool Intersects(FixedBoundingSphere sphere)
        {
            bool result;
            Intersects(ref sphere, out result);
            return result;
        }

        /// <summary>
        ///   Check if this <see cref="FixedBoundingBox"/> intersects a <see cref="FixedBoundingFrustum"/>.
        /// </summary>
        /// <param name="sphere">The <see cref="FixedBoundingFrustum"/> to test for intersection.</param>
        /// <param name="result">
        ///   <code>true</code> if this <see cref="FixedBoundingBox"/> intersects <paramref name="sphere"/>,
        ///   <code>false</code> if it does not.
        /// </param>
        public void Intersects(ref FixedBoundingSphere sphere, out bool result)
        {
            var squareDistance = Fixed.Zero;
            var point = sphere.Center;
            if (point.X < Min.X) squareDistance += (Min.X - point.X) * (Min.X - point.X);
            if (point.X > Max.X) squareDistance += (point.X - Max.X) * (point.X - Max.X);
            if (point.Y < Min.Y) squareDistance += (Min.Y - point.Y) * (Min.Y - point.Y);
            if (point.Y > Max.Y) squareDistance += (point.Y - Max.Y) * (point.Y - Max.Y);
            if (point.Z < Min.Z) squareDistance += (Min.Z - point.Z) * (Min.Z - point.Z);
            if (point.Z > Max.Z) squareDistance += (point.Z - Max.Z) * (point.Z - Max.Z);
            result = squareDistance <= sphere.Radius * sphere.Radius;
        }

        /// <summary>
        ///   Check if this <see cref="FixedBoundingBox"/> intersects a <see cref="FixedPlane"/>.
        /// </summary>
        /// <param name="plane">The <see cref="FixedPlane"/> to test for intersection.</param>
        /// <returns>
        ///   <code>true</code> if this <see cref="FixedBoundingBox"/> intersects <paramref name="plane"/>,
        ///   <code>false</code> if it does not.
        /// </returns>
        public FixedPlaneIntersectionType Intersects(FixedPlane plane)
        {
            FixedPlaneIntersectionType result;
            Intersects(ref plane, out result);
            return result;
        }

        /// <summary>
        ///   Check if this <see cref="FixedBoundingBox"/> intersects a <see cref="FixedPlane"/>.
        /// </summary>
        /// <param name="plane">The <see cref="FixedPlane"/> to test for intersection.</param>
        /// <param name="result">
        ///   <code>true</code> if this <see cref="FixedBoundingBox"/> intersects <paramref name="plane"/>,
        ///   <code>false</code> if it does not.
        /// </param>
        public void Intersects(ref FixedPlane plane, out FixedPlaneIntersectionType result)
        {
            // See http://zach.in.tu-clausthal.de/teaching/cg_literatur/lighthouse3d_view_frustum_culling/index.html

            FixedVector3 positiveVertex;
            FixedVector3 negativeVertex;

            if (plane.Normal.X >= Fixed.Zero)
            {
                positiveVertex.X = Max.X;
                negativeVertex.X = Min.X;
            }
            else
            {
                positiveVertex.X = Min.X;
                negativeVertex.X = Max.X;
            }

            if (plane.Normal.Y >= Fixed.Zero)
            {
                positiveVertex.Y = Max.Y;
                negativeVertex.Y = Min.Y;
            }
            else
            {
                positiveVertex.Y = Min.Y;
                negativeVertex.Y = Max.Y;
            }

            if (plane.Normal.Z >= Fixed.Zero)
            {
                positiveVertex.Z = Max.Z;
                negativeVertex.Z = Min.Z;
            }
            else
            {
                positiveVertex.Z = Min.Z;
                negativeVertex.Z = Max.Z;
            }

            // Inline Vector3.Dot(plane.Normal, negativeVertex) + plane.D;
            var distance = plane.Normal.X * negativeVertex.X + plane.Normal.Y * negativeVertex.Y + plane.Normal.Z * negativeVertex.Z + plane.D;
            if (distance > Fixed.Zero)
            {
                result = FixedPlaneIntersectionType.Front;
                return;
            }

            // Inline Vector3.Dot(plane.Normal, positiveVertex) + plane.D;
            distance = plane.Normal.X * positiveVertex.X + plane.Normal.Y * positiveVertex.Y + plane.Normal.Z * positiveVertex.Z + plane.D;
            if (distance < Fixed.Zero)
            {
                result = FixedPlaneIntersectionType.Back;
                return;
            }

            result = FixedPlaneIntersectionType.Intersecting;
        }

        /// <summary>
        ///   Check if this <see cref="FixedBoundingBox"/> intersects a <see cref="FixedRay"/>.
        /// </summary>
        /// <param name="ray">The <see cref="FixedRay"/> to test for intersection.</param>
        /// <returns>
        ///   The distance along the <see cref="FixedRay"/> to the intersection point or
        ///   <code>null</code> if the <see cref="FixedRay"/> does not intesect this <see cref="FixedBoundingBox"/>.
        /// </returns>
        public Nullable<Fixed> Intersects(FixedRay ray)
        {
            return ray.Intersects(this);
        }

        /// <summary>
        ///   Check if this <see cref="FixedBoundingBox"/> intersects a <see cref="FixedRay"/>.
        /// </summary>
        /// <param name="ray">The <see cref="FixedRay"/> to test for intersection.</param>
        /// <param name="result">
        ///   The distance along the <see cref="FixedRay"/> to the intersection point or
        ///   <code>null</code> if the <see cref="FixedRay"/> does not intesect this <see cref="FixedBoundingBox"/>.
        /// </param>
        public void Intersects(ref FixedRay ray, out Nullable<Fixed> result)
        {
            result = Intersects(ray);
        }

        /// <summary>
        ///   Check if two <see cref="FixedBoundingBox"/> instances are equal.
        /// </summary>
        /// <param name="a">A <see cref="FixedBoundingBox"/> to compare the other.</param>
        /// <param name="b">A <see cref="FixedBoundingBox"/> to compare the other.</param>
        /// <returns>
        ///   <code>true</code> if <see cref="a"/> is equal to this <see cref="b"/>,
        ///   <code>false</code> if it is not.
        /// </returns>
        public static bool operator ==(FixedBoundingBox a, FixedBoundingBox b)
        {
            return a.Equals(b);
        }

        /// <summary>
        ///   Check if two <see cref="FixedBoundingBox"/> instances are not equal.
        /// </summary>
        /// <param name="a">A <see cref="FixedBoundingBox"/> to compare the other.</param>
        /// <param name="b">A <see cref="FixedBoundingBox"/> to compare the other.</param>
        /// <returns>
        ///   <code>true</code> if <see cref="a"/> is not equal to this <see cref="b"/>,
        ///   <code>false</code> if it is.
        /// </returns>
        public static bool operator !=(FixedBoundingBox a, FixedBoundingBox b)
        {
            return !a.Equals(b);
        }

        internal string DebugDisplayString
        {
            get
            {
                return string.Concat(
                    "Min( ", this.Min.DebugDisplayString, " )  \r\n",
                    "Max( ",this.Max.DebugDisplayString, " )"
                    );
            }
        }

        /// <summary>
        /// Get a <see cref="String"/> representation of this <see cref="FixedBoundingBox"/>.
        /// </summary>
        /// <returns>A <see cref="String"/> representation of this <see cref="FixedBoundingBox"/>.</returns>
        public override string ToString()
        {
            return "{{Min:" + this.Min.ToString() + " Max:" + this.Max.ToString() + "}}";
        }

        /// <summary>
        /// Deconstruction method for <see cref="FixedBoundingBox"/>.
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        public void Deconstruct(out FixedVector3 min, out FixedVector3 max)
        {
            min = Min;
            max = Max;
        }

        #endregion Public Methods
    }
}
