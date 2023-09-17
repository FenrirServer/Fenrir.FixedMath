using System;
using System.Diagnostics;

namespace FixedMath
{
    /// <summary>
    /// Defines a viewing frustum for intersection operations.
    /// </summary>
    [DebuggerDisplay("{DebugDisplayString,nq}")]
    public struct FixedBoundingFrustum : IEquatable<FixedBoundingFrustum>
    {
        #region Private Fields

        private FixedMatrix _matrix;
        private readonly FixedVector3[] _corners = new FixedVector3[CornerCount];
        private readonly FixedPlane[] _planes = new FixedPlane[PlaneCount];

        #endregion

        #region Public Fields

        /// <summary>
        /// The number of planes in the frustum.
        /// </summary>
        public const int PlaneCount = 6;

        /// <summary>
        /// The number of corner points in the frustum.
        /// </summary>
        public const int CornerCount = 8;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the <see cref="Matrix"/> of the frustum.
        /// </summary>
        public FixedMatrix Matrix
        {
            get { return this._matrix; }
            set
            {
                this._matrix = value;
                this.CreatePlanes();    // FIXME: The odds are the planes will be used a lot more often than the matrix
                this.CreateCorners();   // is updated, so this should help performance. I hope ;)
            }
        }

        /// <summary>
        /// Gets the near plane of the frustum.
        /// </summary>
        public FixedPlane Near
        {
            get { return this._planes[0]; }
        }

        /// <summary>
        /// Gets the far plane of the frustum.
        /// </summary>
        public FixedPlane Far
        {
            get { return this._planes[1]; }
        }

        /// <summary>
        /// Gets the left plane of the frustum.
        /// </summary>
        public FixedPlane Left
        {
            get { return this._planes[2]; }
        }

        /// <summary>
        /// Gets the right plane of the frustum.
        /// </summary>
        public FixedPlane Right
        {
            get { return this._planes[3]; }
        }

        /// <summary>
        /// Gets the top plane of the frustum.
        /// </summary>
        public FixedPlane Top
        {
            get { return this._planes[4]; }
        }

        /// <summary>
        /// Gets the bottom plane of the frustum.
        /// </summary>
        public FixedPlane Bottom
        {
            get { return this._planes[5]; }
        }

        #endregion

        #region Internal Properties

        internal string DebugDisplayString
        {
            get
            {
                return string.Concat(
                    "Near( ", this._planes[0].DebugDisplayString, " )  \r\n",
                    "Far( ", this._planes[1].DebugDisplayString, " )  \r\n",
                    "Left( ", this._planes[2].DebugDisplayString, " )  \r\n",
                    "Right( ", this._planes[3].DebugDisplayString, " )  \r\n",
                    "Top( ", this._planes[4].DebugDisplayString, " )  \r\n",
                    "Bottom( ", this._planes[5].DebugDisplayString, " )  "
                    );
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Constructs the frustum by extracting the view planes from a matrix.
        /// </summary>
        /// <param name="value">Combined matrix which usually is (View * Projection).</param>
        public FixedBoundingFrustum(FixedMatrix value)
        {
            this._matrix = value;
            this.CreatePlanes();
            this.CreateCorners();
        }

        #endregion

        #region Operators

        /// <summary>
        /// Compares whether two <see cref="FixedBoundingFrustum"/> instances are equal.
        /// </summary>
        /// <param name="a"><see cref="FixedBoundingFrustum"/> instance on the left of the equal sign.</param>
        /// <param name="b"><see cref="FixedBoundingFrustum"/> instance on the right of the equal sign.</param>
        /// <returns><c>true</c> if the instances are equal; <c>false</c> otherwise.</returns>
        public static bool operator ==(FixedBoundingFrustum a, FixedBoundingFrustum b)
        {
            if (Equals(a, null))
                return (Equals(b, null));

            if (Equals(b, null))
                return (Equals(a, null));

            return a._matrix == (b._matrix);
        }

        /// <summary>
        /// Compares whether two <see cref="FixedBoundingFrustum"/> instances are not equal.
        /// </summary>
        /// <param name="a"><see cref="FixedBoundingFrustum"/> instance on the left of the not equal sign.</param>
        /// <param name="b"><see cref="FixedBoundingFrustum"/> instance on the right of the not equal sign.</param>
        /// <returns><c>true</c> if the instances are not equal; <c>false</c> otherwise.</returns>
        public static bool operator !=(FixedBoundingFrustum a, FixedBoundingFrustum b)
        {
            return !(a == b);
        }

        #endregion

        #region Public Methods

        #region Contains

        /// <summary>
        /// Containment test between this <see cref="FixedBoundingFrustum"/> and specified <see cref="FixedBoundingBox"/>.
        /// </summary>
        /// <param name="box">A <see cref="FixedBoundingBox"/> for testing.</param>
        /// <returns>Result of testing for containment between this <see cref="FixedBoundingFrustum"/> and specified <see cref="FixedBoundingBox"/>.</returns>
        public ContainmentType Contains(FixedBoundingBox box)
        {
            var result = default(ContainmentType);
            this.Contains(ref box, out result);
            return result;
        }

        /// <summary>
        /// Containment test between this <see cref="FixedBoundingFrustum"/> and specified <see cref="FixedBoundingBox"/>.
        /// </summary>
        /// <param name="box">A <see cref="FixedBoundingBox"/> for testing.</param>
        /// <param name="result">Result of testing for containment between this <see cref="FixedBoundingFrustum"/> and specified <see cref="FixedBoundingBox"/> as an output parameter.</param>
        public void Contains(ref FixedBoundingBox box, out ContainmentType result)
        {
            var intersects = false;
            for (var i = 0; i < PlaneCount; ++i)
            {
                var planeIntersectionType = default(FixedPlaneIntersectionType);
                box.Intersects(ref this._planes[i], out planeIntersectionType);
                switch (planeIntersectionType)
                {
                case FixedPlaneIntersectionType.Front:
                    result = ContainmentType.Disjoint; 
                    return;
                case FixedPlaneIntersectionType.Intersecting:
                    intersects = true;
                    break;
                }
            }
            result = intersects ? ContainmentType.Intersects : ContainmentType.Contains;
        }

        /// <summary>
        /// Containment test between this <see cref="FixedBoundingFrustum"/> and specified <see cref="FixedBoundingFrustum"/>.
        /// </summary>
        /// <param name="frustum">A <see cref="FixedBoundingFrustum"/> for testing.</param>
        /// <returns>Result of testing for containment between this <see cref="FixedBoundingFrustum"/> and specified <see cref="FixedBoundingFrustum"/>.</returns>
        public ContainmentType Contains(FixedBoundingFrustum frustum)
        {
            if (this == frustum)                // We check to see if the two frustums are equal
                return ContainmentType.Contains;// If they are, there's no need to go any further.

            var intersects = false;
            for (var i = 0; i < PlaneCount; ++i)
            {
                FixedPlaneIntersectionType planeIntersectionType;
                frustum.Intersects(ref _planes[i], out planeIntersectionType);
                switch (planeIntersectionType)
                {
                    case FixedPlaneIntersectionType.Front:
                        return ContainmentType.Disjoint;
                    case FixedPlaneIntersectionType.Intersecting:
                        intersects = true;
                        break;
                }
            }
            return intersects ? ContainmentType.Intersects : ContainmentType.Contains;
        }

        /// <summary>
        /// Containment test between this <see cref="FixedBoundingFrustum"/> and specified <see cref="FixedBoundingSphere"/>.
        /// </summary>
        /// <param name="sphere">A <see cref="FixedBoundingSphere"/> for testing.</param>
        /// <returns>Result of testing for containment between this <see cref="FixedBoundingFrustum"/> and specified <see cref="FixedBoundingSphere"/>.</returns>
        public ContainmentType Contains(FixedBoundingSphere sphere)
        {
            var result = default(ContainmentType);
            this.Contains(ref sphere, out result);
            return result;
        }

        /// <summary>
        /// Containment test between this <see cref="FixedBoundingFrustum"/> and specified <see cref="FixedBoundingSphere"/>.
        /// </summary>
        /// <param name="sphere">A <see cref="FixedBoundingSphere"/> for testing.</param>
        /// <param name="result">Result of testing for containment between this <see cref="FixedBoundingFrustum"/> and specified <see cref="FixedBoundingSphere"/> as an output parameter.</param>
        public void Contains(ref FixedBoundingSphere sphere, out ContainmentType result)
        {
            var intersects = false;
            for (var i = 0; i < PlaneCount; ++i) 
            {
                var planeIntersectionType = default(FixedPlaneIntersectionType);

                // TODO: we might want to inline this for performance reasons
                sphere.Intersects(ref this._planes[i], out planeIntersectionType);
                switch (planeIntersectionType)
                {
                case FixedPlaneIntersectionType.Front:
                    result = ContainmentType.Disjoint; 
                    return;
                case FixedPlaneIntersectionType.Intersecting:
                    intersects = true;
                    break;
                }
            }
            result = intersects ? ContainmentType.Intersects : ContainmentType.Contains;
        }

        /// <summary>
        /// Containment test between this <see cref="FixedBoundingFrustum"/> and specified <see cref="FixedVector3"/>.
        /// </summary>
        /// <param name="point">A <see cref="FixedVector3"/> for testing.</param>
        /// <returns>Result of testing for containment between this <see cref="FixedBoundingFrustum"/> and specified <see cref="FixedVector3"/>.</returns>
        public ContainmentType Contains(FixedVector3 point)
        {
            var result = default(ContainmentType);
            this.Contains(ref point, out result);
            return result;
        }

        /// <summary>
        /// Containment test between this <see cref="FixedBoundingFrustum"/> and specified <see cref="FixedVector3"/>.
        /// </summary>
        /// <param name="point">A <see cref="FixedVector3"/> for testing.</param>
        /// <param name="result">Result of testing for containment between this <see cref="FixedBoundingFrustum"/> and specified <see cref="FixedVector3"/> as an output parameter.</param>
        public void Contains(ref FixedVector3 point, out ContainmentType result)
        {
            for (var i = 0; i < PlaneCount; ++i)
            {
                // TODO: we might want to inline this for performance reasons
                if (PlaneHelper.ClassifyPoint(ref point, ref this._planes[i]) > Fixed.Zero)
                {   
                    result = ContainmentType.Disjoint;
                    return;
                }
            }
            result = ContainmentType.Contains;
        }

        #endregion

        /// <summary>
        /// Compares whether current instance is equal to specified <see cref="FixedBoundingFrustum"/>.
        /// </summary>
        /// <param name="other">The <see cref="FixedBoundingFrustum"/> to compare.</param>
        /// <returns><c>true</c> if the instances are equal; <c>false</c> otherwise.</returns>
        public bool Equals(FixedBoundingFrustum other)
        {
            return (this == other);
        }

        /// <summary>
        /// Compares whether current instance is equal to specified <see cref="FixedBoundingFrustum"/>.
        /// </summary>
        /// <param name="obj">The <see cref="Object"/> to compare.</param>
        /// <returns><c>true</c> if the instances are equal; <c>false</c> otherwise.</returns>
        public override bool Equals(object obj)
        {
            return (obj is FixedBoundingFrustum) && this == ((FixedBoundingFrustum)obj);
        }

        /// <summary>
        /// Returns a copy of internal corners array.
        /// </summary>
        /// <returns>The array of corners.</returns>
        public FixedVector3[] GetCorners()
        {
            return (FixedVector3[])this._corners.Clone();
        }

        /// <summary>
        /// Returns a copy of internal corners array.
        /// </summary>
        /// <param name="corners">The array which values will be replaced to corner values of this instance. It must have size of <see cref="FixedBoundingFrustum.CornerCount"/>.</param>
		public void GetCorners(FixedVector3[] corners)
        {
			if (corners == null) throw new ArgumentNullException("corners");
		    if (corners.Length < CornerCount) throw new ArgumentOutOfRangeException("corners");

            this._corners.CopyTo(corners, 0);
        }

        /// <summary>
        /// Gets the hash code of this <see cref="FixedBoundingFrustum"/>.
        /// </summary>
        /// <returns>Hash code of this <see cref="FixedBoundingFrustum"/>.</returns>
        public override int GetHashCode()
        {
            return this._matrix.GetHashCode();
        }

        /// <summary>
        /// Gets whether or not a specified <see cref="FixedBoundingBox"/> intersects with this <see cref="FixedBoundingFrustum"/>.
        /// </summary>
        /// <param name="box">A <see cref="FixedBoundingBox"/> for intersection test.</param>
        /// <returns><c>true</c> if specified <see cref="FixedBoundingBox"/> intersects with this <see cref="FixedBoundingFrustum"/>; <c>false</c> otherwise.</returns>
        public bool Intersects(FixedBoundingBox box)
        {
			var result = false;
			this.Intersects(ref box, out result);
			return result;
        }

        /// <summary>
        /// Gets whether or not a specified <see cref="FixedBoundingBox"/> intersects with this <see cref="FixedBoundingFrustum"/>.
        /// </summary>
        /// <param name="box">A <see cref="FixedBoundingBox"/> for intersection test.</param>
        /// <param name="result"><c>true</c> if specified <see cref="FixedBoundingBox"/> intersects with this <see cref="FixedBoundingFrustum"/>; <c>false</c> otherwise as an output parameter.</param>
        public void Intersects(ref FixedBoundingBox box, out bool result)
        {
			var containment = default(ContainmentType);
			this.Contains(ref box, out containment);
			result = containment != ContainmentType.Disjoint;
		}

        /// <summary>
        /// Gets whether or not a specified <see cref="FixedBoundingFrustum"/> intersects with this <see cref="FixedBoundingFrustum"/>.
        /// </summary>
        /// <param name="frustum">An other <see cref="FixedBoundingFrustum"/> for intersection test.</param>
        /// <returns><c>true</c> if other <see cref="FixedBoundingFrustum"/> intersects with this <see cref="FixedBoundingFrustum"/>; <c>false</c> otherwise.</returns>
        public bool Intersects(FixedBoundingFrustum frustum)
        {
            return Contains(frustum) != ContainmentType.Disjoint;
        }

        /// <summary>
        /// Gets whether or not a specified <see cref="FixedBoundingSphere"/> intersects with this <see cref="FixedBoundingFrustum"/>.
        /// </summary>
        /// <param name="sphere">A <see cref="FixedBoundingSphere"/> for intersection test.</param>
        /// <returns><c>true</c> if specified <see cref="FixedBoundingSphere"/> intersects with this <see cref="FixedBoundingFrustum"/>; <c>false</c> otherwise.</returns>
        public bool Intersects(FixedBoundingSphere sphere)
        {
            var result = default(bool);
            this.Intersects(ref sphere, out result);
            return result;
        }

        /// <summary>
        /// Gets whether or not a specified <see cref="FixedBoundingSphere"/> intersects with this <see cref="FixedBoundingFrustum"/>.
        /// </summary>
        /// <param name="sphere">A <see cref="FixedBoundingSphere"/> for intersection test.</param>
        /// <param name="result"><c>true</c> if specified <see cref="FixedBoundingSphere"/> intersects with this <see cref="FixedBoundingFrustum"/>; <c>false</c> otherwise as an output parameter.</param>
        public void Intersects(ref FixedBoundingSphere sphere, out bool result)
        {
            var containment = default(ContainmentType);
            this.Contains(ref sphere, out containment);
            result = containment != ContainmentType.Disjoint;
        }

        /// <summary>
        /// Gets type of intersection between specified <see cref="FixedPlane"/> and this <see cref="FixedBoundingFrustum"/>.
        /// </summary>
        /// <param name="plane">A <see cref="FixedPlane"/> for intersection test.</param>
        /// <returns>A plane intersection type.</returns>
        public FixedPlaneIntersectionType Intersects(FixedPlane plane)
        {
            FixedPlaneIntersectionType result;
            Intersects(ref plane, out result);
            return result;
        }

        /// <summary>
        /// Gets type of intersection between specified <see cref="FixedPlane"/> and this <see cref="FixedBoundingFrustum"/>.
        /// </summary>
        /// <param name="plane">A <see cref="FixedPlane"/> for intersection test.</param>
        /// <param name="result">A plane intersection type as an output parameter.</param>
        public void Intersects(ref FixedPlane plane, out FixedPlaneIntersectionType result)
        {
            result = plane.Intersects(ref _corners[0]);
            for (int i = 1; i < _corners.Length; i++)
                if (plane.Intersects(ref _corners[i]) != result)
                    result = FixedPlaneIntersectionType.Intersecting;
        }
        
        /// <summary>
        /// Gets the distance of intersection of <see cref="FixedRay"/> and this <see cref="FixedBoundingFrustum"/> or null if no intersection happens.
        /// </summary>
        /// <param name="ray">A <see cref="FixedRay"/> for intersection test.</param>
        /// <returns>Distance at which ray intersects with this <see cref="FixedBoundingFrustum"/> or null if no intersection happens.</returns>
        public Fixed? Intersects(FixedRay ray)
        {
            Fixed? result;
            Intersects(ref ray, out result);
            return result;
        }

        /// <summary>
        /// Gets the distance of intersection of <see cref="FixedRay"/> and this <see cref="FixedBoundingFrustum"/> or null if no intersection happens.
        /// </summary>
        /// <param name="ray">A <see cref="FixedRay"/> for intersection test.</param>
        /// <param name="result">Distance at which ray intersects with this <see cref="FixedBoundingFrustum"/> or null if no intersection happens as an output parameter.</param>
        public void Intersects(ref FixedRay ray, out Fixed? result)
        {
            ContainmentType ctype;
            this.Contains(ref ray.Position, out ctype);

            switch (ctype)
            {
                case ContainmentType.Disjoint:
                    result = null;
                    return;
                case ContainmentType.Contains:
                    result = Fixed.Zero;
                    return;
                case ContainmentType.Intersects:
                    throw new NotImplementedException();
                default:
                    throw new ArgumentOutOfRangeException();
            }
        } 

        /// <summary>
        /// Returns a <see cref="String"/> representation of this <see cref="FixedBoundingFrustum"/> in the format:
        /// {Near:[nearPlane] Far:[farPlane] Left:[leftPlane] Right:[rightPlane] Top:[topPlane] Bottom:[bottomPlane]}
        /// </summary>
        /// <returns><see cref="String"/> representation of this <see cref="FixedBoundingFrustum"/>.</returns>
        public override string ToString()
        {
            return "{Near: " + this._planes[0] +
                   " Far:" + this._planes[1] +
                   " Left:" + this._planes[2] +
                   " Right:" + this._planes[3] +
                   " Top:" + this._planes[4] +
                   " Bottom:" + this._planes[5] +
                   "}";
        }

        #endregion

        #region Private Methods

        private void CreateCorners()
        {
            IntersectionPoint(ref this._planes[0], ref this._planes[2], ref this._planes[4], out this._corners[0]);
            IntersectionPoint(ref this._planes[0], ref this._planes[3], ref this._planes[4], out this._corners[1]);
            IntersectionPoint(ref this._planes[0], ref this._planes[3], ref this._planes[5], out this._corners[2]);
            IntersectionPoint(ref this._planes[0], ref this._planes[2], ref this._planes[5], out this._corners[3]);
            IntersectionPoint(ref this._planes[1], ref this._planes[2], ref this._planes[4], out this._corners[4]);
            IntersectionPoint(ref this._planes[1], ref this._planes[3], ref this._planes[4], out this._corners[5]);
            IntersectionPoint(ref this._planes[1], ref this._planes[3], ref this._planes[5], out this._corners[6]);
            IntersectionPoint(ref this._planes[1], ref this._planes[2], ref this._planes[5], out this._corners[7]);
        }

        private void CreatePlanes()
        {            
            this._planes[0] = new FixedPlane(-this._matrix.M13, -this._matrix.M23, -this._matrix.M33, -this._matrix.M43);
            this._planes[1] = new FixedPlane(this._matrix.M13 - this._matrix.M14, this._matrix.M23 - this._matrix.M24, this._matrix.M33 - this._matrix.M34, this._matrix.M43 - this._matrix.M44);
            this._planes[2] = new FixedPlane(-this._matrix.M14 - this._matrix.M11, -this._matrix.M24 - this._matrix.M21, -this._matrix.M34 - this._matrix.M31, -this._matrix.M44 - this._matrix.M41);
            this._planes[3] = new FixedPlane(this._matrix.M11 - this._matrix.M14, this._matrix.M21 - this._matrix.M24, this._matrix.M31 - this._matrix.M34, this._matrix.M41 - this._matrix.M44);
            this._planes[4] = new FixedPlane(this._matrix.M12 - this._matrix.M14, this._matrix.M22 - this._matrix.M24, this._matrix.M32 - this._matrix.M34, this._matrix.M42 - this._matrix.M44);
            this._planes[5] = new FixedPlane(-this._matrix.M14 - this._matrix.M12, -this._matrix.M24 - this._matrix.M22, -this._matrix.M34 - this._matrix.M32, -this._matrix.M44 - this._matrix.M42);
            
            this.NormalizePlane(ref this._planes[0]);
            this.NormalizePlane(ref this._planes[1]);
            this.NormalizePlane(ref this._planes[2]);
            this.NormalizePlane(ref this._planes[3]);
            this.NormalizePlane(ref this._planes[4]);
            this.NormalizePlane(ref this._planes[5]);
        }

        private static void IntersectionPoint(ref FixedPlane a, ref FixedPlane b, ref FixedPlane c, out FixedVector3 result)
        {
            // Formula used
            //                d1 ( N2 * N3 ) + d2 ( N3 * N1 ) + d3 ( N1 * N2 )
            //P =   -------------------------------------------------------------------------
            //                             N1 . ( N2 * N3 )
            //
            // Note: N refers to the normal, d refers to the displacement. '.' means dot product. '*' means cross product
            
            FixedVector3 v1, v2, v3;
            FixedVector3 cross;
            
            FixedVector3.Cross(ref b.Normal, ref c.Normal, out cross);
            
            Fixed f;
            FixedVector3.Dot(ref a.Normal, ref cross, out f);
            f *= Fixed.NegativeOne;
            
            FixedVector3.Cross(ref b.Normal, ref c.Normal, out cross);
            FixedVector3.Multiply(ref cross, a.D, out v1);
            //v1 = (a.D * (Vector3.Cross(b.Normal, c.Normal)));
            
            
            FixedVector3.Cross(ref c.Normal, ref a.Normal, out cross);
            FixedVector3.Multiply(ref cross, b.D, out v2);
            //v2 = (b.D * (Vector3.Cross(c.Normal, a.Normal)));
            
            
            FixedVector3.Cross(ref a.Normal, ref b.Normal, out cross);
            FixedVector3.Multiply(ref cross, c.D, out v3);
            //v3 = (c.D * (Vector3.Cross(a.Normal, b.Normal)));
            
            result.X = (v1.X + v2.X + v3.X) / f;
            result.Y = (v1.Y + v2.Y + v3.Y) / f;
            result.Z = (v1.Z + v2.Z + v3.Z) / f;
        }
        
        private void NormalizePlane(ref FixedPlane p)
        {
            Fixed factor = Fixed.One / p.Normal.Length();
            p.Normal.X *= factor;
            p.Normal.Y *= factor;
            p.Normal.Z *= factor;
            p.D *= factor;
        }

        #endregion
    }
}

