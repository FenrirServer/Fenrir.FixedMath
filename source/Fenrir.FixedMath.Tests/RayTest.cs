namespace FixedMath
{
    class RayTest
    {
        [Test]
        public void BoundingBoxIntersects()
        {
            // Our test box.
            FixedBoundingBox box;
            box.Min = new FixedVector3(-10,-20,-30);
            box.Max = new FixedVector3(10, 20, 30);
            var center = (box.Max + box.Min) * (Fixed)0.5f;

            // Test misses.
            Assert.IsNull(new FixedRay(center - FixedVector3.UnitX * (Fixed)40, -FixedVector3.UnitX).Intersects(box));
            Assert.IsNull(new FixedRay(center + FixedVector3.UnitX * (Fixed)40, FixedVector3.UnitX).Intersects(box));
            Assert.IsNull(new FixedRay(center - FixedVector3.UnitY * (Fixed)40, -FixedVector3.UnitY).Intersects(box));
            Assert.IsNull(new FixedRay(center + FixedVector3.UnitY * (Fixed)40, FixedVector3.UnitY).Intersects(box));
            Assert.IsNull(new FixedRay(center - FixedVector3.UnitZ * (Fixed)40, -FixedVector3.UnitZ).Intersects(box));
            Assert.IsNull(new FixedRay(center + FixedVector3.UnitZ * (Fixed)40, FixedVector3.UnitZ).Intersects(box));

            // Test middle of each face.
            Assert.AreEqual((Fixed)30.0f, new FixedRay(center - FixedVector3.UnitX * (Fixed)40, FixedVector3.UnitX).Intersects(box));
            Assert.AreEqual((Fixed)30.0f, new FixedRay(center + FixedVector3.UnitX * (Fixed)40, -FixedVector3.UnitX).Intersects(box));
            Assert.AreEqual((Fixed)20.0f, new FixedRay(center - FixedVector3.UnitY * (Fixed)40, FixedVector3.UnitY).Intersects(box));
            Assert.AreEqual((Fixed)20.0f, new FixedRay(center + FixedVector3.UnitY * (Fixed)40, -FixedVector3.UnitY).Intersects(box));
            Assert.AreEqual((Fixed)10.0f, new FixedRay(center - FixedVector3.UnitZ * (Fixed)40, FixedVector3.UnitZ).Intersects(box));
            Assert.AreEqual((Fixed)10.0f, new FixedRay(center + FixedVector3.UnitZ * (Fixed)40, -FixedVector3.UnitZ).Intersects(box));

            // Test the corners along each axis.
            Assert.AreEqual((Fixed)10.0f, new FixedRay(box.Min - FixedVector3.UnitX * (Fixed)10, FixedVector3.UnitX).Intersects(box));
            Assert.AreEqual((Fixed)10.0f, new FixedRay(box.Min - FixedVector3.UnitY * (Fixed)10, FixedVector3.UnitY).Intersects(box));
            Assert.AreEqual((Fixed)10.0f, new FixedRay(box.Min - FixedVector3.UnitZ * (Fixed)10, FixedVector3.UnitZ).Intersects(box));
            Assert.AreEqual((Fixed)10.0f, new FixedRay(box.Max + FixedVector3.UnitX * (Fixed)10, -FixedVector3.UnitX).Intersects(box));
            Assert.AreEqual((Fixed)10.0f, new FixedRay(box.Max + FixedVector3.UnitY * (Fixed)10, -FixedVector3.UnitY).Intersects(box));
            Assert.AreEqual((Fixed)10.0f, new FixedRay(box.Max + FixedVector3.UnitZ * (Fixed)10, -FixedVector3.UnitZ).Intersects(box));

            // Test inside out.
            Assert.AreEqual((Fixed)0.0f, new FixedRay(center, FixedVector3.UnitX).Intersects(box));
            Assert.AreEqual((Fixed)0.0f, new FixedRay(center, -FixedVector3.UnitX).Intersects(box));
            Assert.AreEqual((Fixed)0.0f, new FixedRay(center, FixedVector3.UnitY).Intersects(box));
            Assert.AreEqual((Fixed)0.0f, new FixedRay(center, -FixedVector3.UnitY).Intersects(box));
            Assert.AreEqual((Fixed)0.0f, new FixedRay(center, FixedVector3.UnitZ).Intersects(box));
            Assert.AreEqual((Fixed)0.0f, new FixedRay(center, -FixedVector3.UnitZ).Intersects(box));
        }

        [Test]
        public void Deconstruct()
        {
            FixedRay ray = new FixedRay(FixedVector3.Backward, FixedVector3.Right);

            FixedVector3 position, direction;

            ray.Deconstruct(out position, out direction);

            Assert.AreEqual(position, ray.Position);
            Assert.AreEqual(direction, ray.Direction);
        }
    }
}
