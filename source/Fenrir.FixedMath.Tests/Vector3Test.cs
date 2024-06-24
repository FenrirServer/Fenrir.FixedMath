using FixedMath.Tests;
using System.ComponentModel;
using System.Globalization;

namespace FixedMath
{
    class Vector3Test
    {
        [Test]
        public void TypeConverter()
        {
            var converter = TypeDescriptor.GetConverter(typeof(FixedVector3));
            var invariantCulture = CultureInfo.InvariantCulture;

            Assert.AreEqual(new FixedVector3(32, 64, 128), converter.ConvertFromString(null, invariantCulture, "32, 64, 128"));
            Assert.AreEqual(new FixedVector3(0.5f, 2.75f, 4.125f), converter.ConvertFromString(null, invariantCulture, "0.5, 2.75, 4.125"));
            Assert.AreEqual(new FixedVector3(1024.5f, 2048.75f, 4096.125f), converter.ConvertFromString(null, invariantCulture, "1024.5, 2048.75, 4096.125"));
            Assert.AreEqual("32, 64, 128", converter.ConvertToString(null, invariantCulture, new FixedVector3(32, 64, 128)));
            Assert.AreEqual("0.5, 2.75, 4.125", converter.ConvertToString(null, invariantCulture, new FixedVector3(0.5f, 2.75f, 4.125f)));
            Assert.AreEqual("1024.5, 2048.75, 4096.125", converter.ConvertToString(null, invariantCulture, new FixedVector3(1024.5f, 2048.75f, 4096.125f)));

            var otherCulture = new CultureInfo("el-GR");
            var vectorStr = (1024.5f).ToString(otherCulture) + otherCulture.TextInfo.ListSeparator + " " +
                            (2048.75f).ToString(otherCulture) + otherCulture.TextInfo.ListSeparator + " " +
                            (4096.125f).ToString(otherCulture);
            Assert.AreEqual(new FixedVector3(1024.5f, 2048.75f, 4096.125f), converter.ConvertFromString(null, otherCulture, vectorStr));
            Assert.AreEqual(vectorStr, converter.ConvertToString(null, otherCulture, new FixedVector3(1024.5f, 2048.75f, 4096.125f)));
        }

        [Test]
        public void DistanceSquared()
        {
            var v1 = new FixedVector3(0.1f, 100.0f, -5.5f);
            var v2 = new FixedVector3(1.1f, -2.0f, 5.5f);
            var d = FixedVector3.DistanceSquared(v1, v2);
            Fixed expectedResult = (Fixed)10526.0000000448M;
            Assert.AreEqual(expectedResult, d);
        }

        [Test]
        public void Normalize()
        {
            FixedVector3 v1 = new FixedVector3(-10.5f, 0.2f, 1000.0f);
            FixedVector3 v2 = new FixedVector3(-10.5f, 0.2f, 1000.0f);
            v1.Normalize();
            var expectedResult = new FixedVector3(-0.0104994215f, 0.000199988979f, 0.999944866f);
            Assert.That(expectedResult, Is.EqualTo(v1).Using(Vector3Comparer.Epsilon));
            v2 = FixedVector3.Normalize(v2);
            Assert.That(expectedResult, Is.EqualTo(v2).Using(Vector3Comparer.Epsilon));
        }

        [Test]
        public void Transform()
        {
            // STANDART OVERLOADS TEST

            var expectedResult1 = new FixedVector3(51, 58, 65);
            var expectedResult2 = new FixedVector3(33, -14, -1);

            var v1 = new FixedVector3(1, 2, 3);
            var m1 = new FixedMatrix(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16);

            var v2 = new FixedVector3(1, 2, 3);
            var q1 = new FixedQuaternion(2, 3, 4, 5);

            FixedVector3 result1;
            FixedVector3 result2;

            Assert.That(expectedResult1, Is.EqualTo(FixedVector3.Transform(v1, m1)).Using(Vector3Comparer.Epsilon));
            Assert.That(expectedResult2, Is.EqualTo(FixedVector3.Transform(v2, q1)).Using(Vector3Comparer.Epsilon));

            // OUTPUT OVERLOADS TEST

            FixedVector3.Transform(ref v1, ref m1, out result1);
            FixedVector3.Transform(ref v2, ref q1, out result2);

            Assert.That(expectedResult1, Is.EqualTo(result1).Using(Vector3Comparer.Epsilon));
            Assert.That(expectedResult2, Is.EqualTo(result2).Using(Vector3Comparer.Epsilon));
        }

        [Test]
        public void HashCode() {
            // Checking for overflows in hash calculation.
            var max = new FixedVector3(float.MaxValue, float.MaxValue, float.MaxValue);
            var min = new FixedVector3(float.MinValue, float.MinValue, float.MinValue);
            Assert.AreNotEqual(max.GetHashCode(), FixedVector3.Zero.GetHashCode());
            Assert.AreNotEqual(min.GetHashCode(), FixedVector3.Zero.GetHashCode());

            // Common values
            var a = new FixedVector3(0f, 0f, 0f);
            Assert.AreEqual(a.GetHashCode(), FixedVector3.Zero.GetHashCode());
            Assert.AreNotEqual(a.GetHashCode(), FixedVector3.One.GetHashCode());

            // Individual properties alter hash
            var xa = new FixedVector3(2f, 1f, 1f);
            var xb = new FixedVector3(3f, 1f, 1f);
            var ya = new FixedVector3(1f, 2f, 1f);
            var yb = new FixedVector3(1f, 3f, 1f);
            var za = new FixedVector3(1f, 1f, 2f);
            var zb = new FixedVector3(1f, 1f, 3f);
            Assert.AreNotEqual(xa.GetHashCode(), xb.GetHashCode(), "Different properties should change hash.");
            Assert.AreNotEqual(ya.GetHashCode(), yb.GetHashCode(), "Different properties should change hash.");
            Assert.AreNotEqual(za.GetHashCode(), zb.GetHashCode(), "Different properties should change hash.");

            Assert.AreNotEqual(xa.GetHashCode(), ya.GetHashCode(), "Identical values on different properties should have different hashes.");
            Assert.AreNotEqual(xb.GetHashCode(), yb.GetHashCode(), "Identical values on different properties should have different hashes.");
            Assert.AreNotEqual(xb.GetHashCode(), zb.GetHashCode(), "Identical values on different properties should have different hashes.");
            Assert.AreNotEqual(yb.GetHashCode(), zb.GetHashCode(), "Identical values on different properties should have different hashes.");

            Assert.AreNotEqual(xa.GetHashCode(), yb.GetHashCode());
            Assert.AreNotEqual(ya.GetHashCode(), xb.GetHashCode());
            Assert.AreNotEqual(xa.GetHashCode(), zb.GetHashCode());
        }

        [Test]
        public void Deconstruct()
        {
            FixedVector3 vector3 = new FixedVector3(float.MinValue, float.MaxValue, float.MinValue);

            Fixed x, y, z;

            vector3.Deconstruct(out x, out y, out z);

            Assert.AreEqual(x, vector3.X);
            Assert.AreEqual(y, vector3.Y);
            Assert.AreEqual(z, vector3.Z);
        }

        [Test]
        public void Round()
        {
            FixedVector3 vector3 = new FixedVector3(0.4f, 0.6f, 1.0f);

            // CEILING

            FixedVector3 ceilMember = vector3;
            ceilMember.Ceiling();

            FixedVector3 ceilResult;
            FixedVector3.Ceiling(ref vector3, out ceilResult);

            Assert.AreEqual(new FixedVector3(1.0f, 1.0f, 1.0f), ceilMember);
            Assert.AreEqual(new FixedVector3(1.0f, 1.0f, 1.0f), FixedVector3.Ceiling(vector3));
            Assert.AreEqual(new FixedVector3(1.0f, 1.0f, 1.0f), ceilResult);

            // FLOOR

            FixedVector3 floorMember = vector3;
            floorMember.Floor();

            FixedVector3 floorResult;
            FixedVector3.Floor(ref vector3, out floorResult);

            Assert.AreEqual(new FixedVector3(0.0f, 0.0f, 1.0f), floorMember);
            Assert.AreEqual(new FixedVector3(0.0f, 0.0f, 1.0f), FixedVector3.Floor(vector3));
            Assert.AreEqual(new FixedVector3(0.0f, 0.0f, 1.0f), floorResult);

            // ROUND

            FixedVector3 roundMember = vector3;
            roundMember.Round();

            FixedVector3 roundResult;
            FixedVector3.Round(ref vector3, out roundResult);

            Assert.AreEqual(new FixedVector3(0.0f, 1.0f, 1.0f), roundMember);
            Assert.AreEqual(new FixedVector3(0.0f, 1.0f, 1.0f), FixedVector3.Round(vector3));
            Assert.AreEqual(new FixedVector3(0.0f, 1.0f, 1.0f), roundResult);
        }

    }
}
