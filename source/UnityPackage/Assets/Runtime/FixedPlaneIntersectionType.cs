namespace FixedMath
{
    /// <summary>
    /// Defines the intersection between a <see cref="FixedPlane"/> and a bounding volume.
    /// </summary>
    public enum FixedPlaneIntersectionType
    {
        /// <summary>
        /// There is no intersection, the bounding volume is in the negative half space of the plane.
        /// </summary>
        Front,
        /// <summary>
        /// There is no intersection, the bounding volume is in the positive half space of the plane.
        /// </summary>
        Back,
        /// <summary>
        /// The plane is intersected.
        /// </summary>
        Intersecting
    }
}
