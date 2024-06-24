using System;
using FixedMath.Tests;

namespace FixedMath
{
    [TestFixture]
    class Bounding
    {
        [Test]
        public void BoxContainsVector3Test()
        {
            var box = new FixedBoundingBox(FixedVector3.Zero, FixedVector3.One);

            Assert.AreEqual(ContainmentType.Disjoint, box.Contains(-FixedVector3.One));
            Assert.AreEqual(ContainmentType.Disjoint, box.Contains(new FixedVector3(0.5f, 0.5f, -1f)));
            Assert.AreEqual(ContainmentType.Contains, box.Contains(FixedVector3.Zero));
            Assert.AreEqual(ContainmentType.Contains, box.Contains(new FixedVector3(0f, 0, 0.5f)));
            Assert.AreEqual(ContainmentType.Contains, box.Contains(new FixedVector3(0f, 0.5f, 0.5f)));
            Assert.AreEqual(ContainmentType.Contains, box.Contains(FixedVector3.One));
            Assert.AreEqual(ContainmentType.Contains, box.Contains(new FixedVector3(1f, 1, 0.5f)));
            Assert.AreEqual(ContainmentType.Contains, box.Contains(new FixedVector3(1f, 0.5f, 0.5f)));
            Assert.AreEqual(ContainmentType.Contains, box.Contains(new FixedVector3(0.5f, 0.5f, 0.5f)));
        }

        [Test]
        public void BoxContainsIdenticalBox()
        {
            var b1 = new FixedBoundingBox(FixedVector3.Zero, FixedVector3.One);
            var b2 = new FixedBoundingBox(FixedVector3.Zero, FixedVector3.One);

            Assert.AreEqual(ContainmentType.Contains, b1.Contains(b2));
        }

        [Test]
        public void BoundingSphereTests()
        {
            var zeroPoint = FixedBoundingSphere.CreateFromPoints( new[] {FixedVector3.Zero} );
            Assert.AreEqual(new FixedBoundingSphere(), zeroPoint);

            var onePoint = FixedBoundingSphere.CreateFromPoints(new[] { FixedVector3.One });
            Assert.AreEqual(new FixedBoundingSphere(FixedVector3.One, 0), onePoint);

            var twoPoint = FixedBoundingSphere.CreateFromPoints(new[] { FixedVector3.Zero, FixedVector3.One });
            Assert.AreEqual(new FixedBoundingSphere(new FixedVector3(Fixed.Half, Fixed.Half, Fixed.Half), (Fixed)0.8660254039M), twoPoint);

            var threePoint = FixedBoundingSphere.CreateFromPoints(new[] { new FixedVector3(0, 0, 0), new FixedVector3(-1, 0, 0), new FixedVector3(1, 1, 1) });
            Assert.That(new FixedBoundingSphere(new FixedVector3(0, 0.5f, 0.5f), 1.224745f), Is.EqualTo(threePoint).Using(BoundingSphereComparer.Epsilon));

            var eightPointTestInput = new FixedVector3[]
            {
                new FixedVector3(54.58071f, 124.9063f, 56.0016f),
                new FixedVector3(54.52138f, 124.9063f, 56.13985f),
                new FixedVector3(54.52208f, 124.8235f, 56.14014f),
                new FixedVector3(54.5814f, 124.8235f, 56.0019f),
                new FixedVector3(1145.415f, 505.913f, -212.5173f),
                new FixedVector3(611.4731f, 505.9535f, 1031.893f),
                new FixedVector3(617.7462f, -239.7422f, 1034.584f),
                new FixedVector3(1151.687f, -239.7035f, -209.8246f)
            };
            var eightPoint = FixedBoundingSphere.CreateFromPoints(eightPointTestInput);
            for (int i = 0; i < eightPointTestInput.Length; i++)
            {
                Assert.That(eightPoint.Contains(eightPointTestInput[i]) != ContainmentType.Disjoint);
            }

            Assert.Throws<ArgumentException>(() => FixedBoundingSphere.CreateFromPoints(new FixedVector3[] {}));
        }

        [Test]
        public void BoundingBoxContainsBoundingSphere()
        {
            var testSphere = new FixedBoundingSphere(FixedVector3.Zero, 1);
            var testBox = new FixedBoundingBox(-FixedVector3.One, FixedVector3.One);

            Assert.AreEqual(testBox.Contains(testSphere), ContainmentType.Contains);

            testSphere.Center -= FixedVector3.One;

            Assert.AreEqual(testBox.Contains(testSphere), ContainmentType.Intersects);

            testSphere.Center -= FixedVector3.One;

            Assert.AreEqual(testBox.Contains(testSphere), ContainmentType.Disjoint);
        }

        [Test]
        public void BoundingFrustumToBoundingBoxTests()
        {
            var view = FixedMatrix.CreateLookAt(new FixedVector3(0, 0, 5), FixedVector3.Zero, FixedVector3.Up);
            var projection = FixedMatrix.CreatePerspectiveFieldOfView(Fixed.PiOver4, (Fixed)1, (Fixed)1, (Fixed)100);
            var testFrustum = new FixedBoundingFrustum(view * projection);

            var bbox1 = new FixedBoundingBox(new FixedVector3(0, 0, 0), new FixedVector3(1, 1, 1));
            Assert.That(testFrustum.Contains(bbox1), Is.EqualTo(ContainmentType.Contains));
            Assert.That(testFrustum.Intersects(bbox1), Is.True);

            var bbox2 = new FixedBoundingBox(new FixedVector3(-1000, -1000, -1000), new FixedVector3(1000, 1000, 1000));
            Assert.That(testFrustum.Contains(bbox2), Is.EqualTo(ContainmentType.Intersects));
            Assert.That(testFrustum.Intersects(bbox2), Is.True);

            var bbox3 = new FixedBoundingBox(new FixedVector3(-1000, -1000, -1000), new FixedVector3(0, 0, 0));
            Assert.That(testFrustum.Contains(bbox3), Is.EqualTo(ContainmentType.Intersects));
            Assert.That(testFrustum.Intersects(bbox3), Is.True);

            var bbox4 = new FixedBoundingBox(new FixedVector3(-1000, -1000, -1000), new FixedVector3(-500, -500, -500));
            Assert.That(testFrustum.Contains(bbox4), Is.EqualTo(ContainmentType.Disjoint));
            Assert.That(testFrustum.Intersects(bbox4), Is.False);
        }

        [Test]
        public void BoundingFrustumToBoundingFrustumTests()
        {
            var view = FixedMatrix.CreateLookAt(new FixedVector3(0, 0, 5), FixedVector3.Zero, FixedVector3.Up);
            var projection = FixedMatrix.CreatePerspectiveFieldOfView(Fixed.PiOver4, (Fixed)1, (Fixed)1, (Fixed)100);
            var testFrustum = new FixedBoundingFrustum(view * projection);

            // Same frustum.
            Assert.That(testFrustum.Contains(testFrustum), Is.EqualTo(ContainmentType.Contains));
            Assert.That(testFrustum.Intersects(testFrustum), Is.True);

            var otherFrustum = new FixedBoundingFrustum(FixedMatrix.Identity);

            // Smaller frustum contained entirely inside.
            var view2 = FixedMatrix.CreateLookAt(new FixedVector3(0, 0, 4), FixedVector3.Zero, FixedVector3.Up);
            var projection2 = FixedMatrix.CreatePerspectiveFieldOfView(Fixed.PiOver4, (Fixed)1, (Fixed)1, (Fixed)50);
            otherFrustum.Matrix = view2 * projection2;

            Assert.That(testFrustum.Contains(otherFrustum), Is.EqualTo(ContainmentType.Contains));
            Assert.That(testFrustum.Intersects(otherFrustum), Is.True);

            // Same size frustum, pointing in the same direction and offset by a small amount.
            otherFrustum.Matrix = view2 * projection;

            Assert.That(testFrustum.Contains(otherFrustum), Is.EqualTo(ContainmentType.Intersects));
            Assert.That(testFrustum.Intersects(otherFrustum), Is.True);

            // Same size frustum, pointing in the opposite direction and not overlapping.
            var view3 = FixedMatrix.CreateLookAt(new FixedVector3(0, 0, 6), new FixedVector3(0, 0, 7), FixedVector3.Up);
            otherFrustum.Matrix = view3 * projection;

            Assert.That(testFrustum.Contains(otherFrustum), Is.EqualTo(ContainmentType.Disjoint));
            Assert.That(testFrustum.Intersects(otherFrustum), Is.False);

            // Larger frustum, entirely containing test frustum.
            var view4 = FixedMatrix.CreateLookAt(new FixedVector3(0, 0, 10), FixedVector3.Zero, FixedVector3.Up);
            var projection4 = FixedMatrix.CreatePerspectiveFieldOfView(Fixed.PiOver4, (Fixed)1, (Fixed)1, (Fixed)1000);
            otherFrustum.Matrix = view4 * projection4;

            Assert.That(testFrustum.Contains(otherFrustum), Is.EqualTo(ContainmentType.Intersects));
            Assert.That(testFrustum.Intersects(otherFrustum), Is.True);

            var bf =
                new FixedBoundingFrustum(FixedMatrix.CreateLookAt(new FixedVector3(0, 1, 1), new FixedVector3(0, 0, 0), FixedVector3.Up) *
                                    FixedMatrix.CreatePerspectiveFieldOfView(Fixed.PiOver4,
                                        (Fixed)1.3M, (Fixed)0.1M, (Fixed)1000.0M));
            var ray = new FixedRay(new FixedVector3(0, 0.5f, 0.5f), new FixedVector3(0, 0, 0));
            var ray2 = new FixedRay(new FixedVector3(0, 1.0f, 1.0f), new FixedVector3(0, 0, 0));
            var value = bf.Intersects(ray);
            var value2 = bf.Intersects(ray2);
            Assert.AreEqual(Fixed.Zero, value);
            Assert.AreEqual(null, value2);
        }

        [Test]
        public void BoundingBoxDeconstruct()
        {
            FixedBoundingBox boundingBox = new FixedBoundingBox(new FixedVector3(255, 255, 255), new FixedVector3(0, 0, 0));

            FixedVector3 min, max;

            boundingBox.Deconstruct(out min, out max);

            Assert.AreEqual(min, boundingBox.Min);
            Assert.AreEqual(max, boundingBox.Max);
        }

        [Test]
        public void BoundingSphereDeconstruct()
        {
            FixedBoundingSphere boundingSphere = new FixedBoundingSphere(new FixedVector3(255, 255, 255), float.MaxValue);

            FixedVector3 center;
            Fixed radius;

            boundingSphere.Deconstruct(out center, out radius);

            Assert.AreEqual(center, boundingSphere.Center);
            Assert.AreEqual(radius, boundingSphere.Radius);
        }
    }
}
