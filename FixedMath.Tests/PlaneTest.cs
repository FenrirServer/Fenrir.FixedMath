using FixedMath.Tests;

namespace FixedMath
{
    public class PlaneTest
    {
        [Test]
        public void TransformByMatrix()
        {
            // Our test plane.
            var plane = FixedPlane.Normalize(new FixedPlane(new FixedVector3(0, 1, 1), 2.5f));

            // Our matrix.
            var matrix = FixedMatrix.CreateRotationX(Fixed.PiOver2);

            // Test transform.
            var expectedResult = new FixedPlane(new FixedVector3(0, -0.7071068f, 0.7071067f), 1.767767f);
            Assert.That(FixedPlane.Transform(plane, matrix), Is.EqualTo(expectedResult).Using(PlaneComparer.Epsilon));
        }

        [Test]
        public void TransformByRefMatrix()
        {
            // Our test plane.
            var plane = FixedPlane.Normalize(new FixedPlane(new FixedVector3(1, 0.8f, 0.5f), 2.5f));
            var originalPlane = plane;

            // Our matrix.
            var matrix = FixedMatrix.CreateRotationX(Fixed.PiOver2);
            var originalMatrix = matrix;

            // Test transform.
            FixedPlane result;
            FixedPlane.Transform(ref plane, ref matrix, out result);

            var expectedResult = new FixedPlane(new FixedVector3(0.7273929f, -0.3636965f, 0.5819144f), 1.818482f);
            Assert.That(result, Is.EqualTo(expectedResult).Using(PlaneComparer.Epsilon));

            Assert.That(plane, Is.EqualTo(originalPlane));
            Assert.That(matrix, Is.EqualTo(originalMatrix));
        }

        [Test]
        public void TransformByQuaternion()
        {
            // Our test plane.
            var plane = FixedPlane.Normalize(new FixedPlane(new FixedVector3(0, 1, 1), 2.5f));

            // Our quaternion.
            var quaternion = FixedQuaternion.CreateFromAxisAngle(FixedVector3.UnitX, Fixed.PiOver2);

            // Test transform.
            var expectedResult = new FixedPlane(new FixedVector3(0, -0.7071068f, 0.7071067f), 1.767767f);
            Assert.That(FixedPlane.Transform(plane, quaternion), Is.EqualTo(expectedResult).Using(PlaneComparer.Epsilon));
        }

        [Test]
        public void TransformByRefQuaternion()
        {
            // Our test plane.
            var plane = FixedPlane.Normalize(new FixedPlane(new FixedVector3(1, 0.8f, 0.5f), 2.5f));
            var originalPlane = plane;

            // Our quaternion.
            var quaternion = FixedQuaternion.CreateFromAxisAngle(FixedVector3.UnitX, Fixed.PiOver2);
            var originalQuaternion = quaternion;

            // Test transform.
            FixedPlane result;
            FixedPlane.Transform(ref plane, ref quaternion, out result);

            var expectedResult = new FixedPlane(new FixedVector3(0.7273929f, -0.3636965f, 0.5819144f), 1.818482f);
            Assert.That(result, Is.EqualTo(expectedResult).Using(PlaneComparer.Epsilon));

            Assert.That(plane, Is.EqualTo(originalPlane));
            Assert.That(quaternion, Is.EqualTo(originalQuaternion));
        }

        [Test]
        public void Deconstruct()
        {
            FixedPlane plane = new FixedPlane(new FixedVector3(255, 255, 255), float.MaxValue);

            FixedVector3 normal;
            Fixed d;

            plane.Deconstruct(out normal, out d);

            Assert.AreEqual(normal, plane.Normal);
            Assert.AreEqual(d, plane.D);
        }
    }
}
