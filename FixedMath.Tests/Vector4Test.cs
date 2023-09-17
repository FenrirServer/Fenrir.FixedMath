using FixedMath.Tests;
using System.ComponentModel;
using System.Globalization;

namespace FixedMath
{
    class Vector4Tests
    {
        [Test]
        public void TypeConverter()
        {
            var converter = TypeDescriptor.GetConverter(typeof(FixedVector4));
            var invariantCulture = CultureInfo.InvariantCulture;

            Assert.AreEqual(new FixedVector4(32, 64, 128, 255), converter.ConvertFromString(null, invariantCulture, "32, 64, 128, 255"));
            Assert.AreEqual(new FixedVector4(0.5f, 2.75f, 4.125f, 8.0625f), converter.ConvertFromString(null, invariantCulture, "0.5, 2.75, 4.125, 8.0625"));
            Assert.AreEqual(new FixedVector4(1024.5f, 2048.75f, 4096.125f, 8192.0625f), converter.ConvertFromString(null, invariantCulture, "1024.5, 2048.75, 4096.125, 8192.0625"));
            Assert.AreEqual("32, 64, 128, 255", converter.ConvertToString(null, invariantCulture, new FixedVector4(32, 64, 128, 255)));
            Assert.AreEqual("0.5, 2.75, 4.125, 8.0625", converter.ConvertToString(null, invariantCulture, new FixedVector4(0.5f, 2.75f, 4.125f, 8.0625f)));
            Assert.AreEqual("1024.5, 2048.75, 4096.125, 8192.0625", converter.ConvertToString(null, invariantCulture, new FixedVector4(1024.5f, 2048.75f, 4096.125f, 8192.0625f)));

            var otherCulture = new CultureInfo("el-GR");
            var vectorStr = (1024.5f).ToString(otherCulture) + otherCulture.TextInfo.ListSeparator + " " +
                            (2048.75f).ToString(otherCulture) + otherCulture.TextInfo.ListSeparator + " " +
                            (4096.125f).ToString(otherCulture) + otherCulture.TextInfo.ListSeparator + " " +
                            (2048.75f).ToString(otherCulture);
            Assert.AreEqual(new FixedVector4(1024.5f, 2048.75f, 4096.125f, 2048.75f), converter.ConvertFromString(null, otherCulture, vectorStr));
            Assert.AreEqual(vectorStr, converter.ConvertToString(null, otherCulture, new FixedVector4(1024.5f, 2048.75f, 4096.125f, 2048.75f)));
        }

        [Test]
        public void Constructors()
        {
            var expectedResult = new FixedVector4()
            {
                X = (Fixed)1,
                Y = (Fixed)2,
                Z = (Fixed)3,
                W = (Fixed)4
            };

            var expectedResult2 = new FixedVector4()
            {
                X = (Fixed)2.2f,
                Y = (Fixed)2.2f,
                Z = (Fixed)2.2f,
                W = (Fixed)2.2f
            };

            Assert.That(expectedResult, Is.EqualTo(new FixedVector4(1,2,3,4)).Using(Vector4Comparer.Epsilon));
            Assert.That(expectedResult, Is.EqualTo(new FixedVector4(new FixedVector2(1,2), (Fixed)3, (Fixed)4)).Using(Vector4Comparer.Epsilon));
            Assert.That(expectedResult, Is.EqualTo(new FixedVector4(new FixedVector3(1,2,3), (Fixed)4)).Using(Vector4Comparer.Epsilon));
            Assert.That(expectedResult2, Is.EqualTo(new FixedVector4(2.2f)).Using(Vector4Comparer.Epsilon));
        }

        [Test]
        public void Properties()
        {
            Assert.That(new FixedVector4(0,0,0,0), Is.EqualTo(FixedVector4.Zero).Using(Vector4Comparer.Epsilon));
            Assert.That(new FixedVector4(1,1,1,1), Is.EqualTo(FixedVector4.One).Using(Vector4Comparer.Epsilon));
            Assert.That(new FixedVector4(1,0,0,0), Is.EqualTo(FixedVector4.UnitX).Using(Vector4Comparer.Epsilon));
            Assert.That(new FixedVector4(0,1,0,0), Is.EqualTo(FixedVector4.UnitY).Using(Vector4Comparer.Epsilon));
            Assert.That(new FixedVector4(0,0,1,0), Is.EqualTo(FixedVector4.UnitZ).Using(Vector4Comparer.Epsilon));
            Assert.That(new FixedVector4(0,0,0,1), Is.EqualTo(FixedVector4.UnitW).Using(Vector4Comparer.Epsilon));
        }

        [Test]
        public void Dot()
        {
            var vector1 = new FixedVector4(1, 2, 3, 4);
            var vector2 = new FixedVector4(0.5f, 1.1f, -3.8f, 1.2f);
            Fixed expectedResult = (Fixed)(-3.89999962f);

            Assert.AreEqual(expectedResult, FixedVector4.Dot(vector1, vector2));

            Fixed result;
            FixedVector4.Dot(ref vector1, ref vector2, out result);

            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void Hermite()
        {
            var t1 = new FixedVector4(1.40625f, 1.40625f, 0.2f, 0.92f);
            var t2 = new FixedVector4(2.662375f, 2.26537514f,10.0f,2f);
            var v1 = new FixedVector4(1,2,3,4);
            var v2 = new FixedVector4(-1.3f,0.1f,30.0f,365.20f);
            Fixed a = (Fixed)2.234f;

            var result1 = FixedVector4.Hermite(v1, t1, v2, t2, a);
            var expected = new FixedVector4((Fixed)39.0311012268M, (Fixed)34.6555709839, (Fixed)(-132.5473022461), (Fixed)(-2626.859375));
            Assert.That(expected, Is.EqualTo(result1).Using(Vector4Comparer.Epsilon));

            FixedVector4 result2;

            // same as result1 ? - it must be same

            FixedVector4.Hermite(ref v1, ref t1, ref v2, ref t2, a, out result2);
            Assert.That(result1, Is.EqualTo(result2).Using(Vector4Comparer.Epsilon));
        }

        [Test]
        public void Length()
        {
            var vector1 = new FixedVector4(1, 2, 3, 4);
            Assert.AreEqual((Fixed)5.4772255752M, vector1.Length());
        }

        [Test]
        public void LengthSquared()
        {
            var vector1 = new FixedVector4(1, 2, 3, 4);
            Assert.AreEqual((Fixed)30, vector1.LengthSquared());
        }

        [Test]
        public void Normalize()
        {
            var vector1 = new FixedVector4(1, 2, 3, 4);
            vector1.Normalize();
            var expected = new FixedVector4(0.1825742f,0.3651484f,0.5477225f,0.7302967f);
            Assert.That(expected, Is.EqualTo(vector1).Using(Vector4Comparer.Epsilon));
            var vector2 = new FixedVector4(1, 2, 3, 4);
            var result = FixedVector4.Normalize(vector2);
            Assert.That(expected, Is.EqualTo(result).Using(Vector4Comparer.Epsilon));
        }

        [Test]
        public void ToStringTest()
        {
            StringAssert.IsMatch("{X:10 Y:20 Z:3.5 W:-100}", new FixedVector4(10, 20, 3.5f, -100).ToString());
        }

        [Test, Ignore("Does not work for Fixed type since it's based on long for which MinValue.GetHashCode() == MaxValue.GetHashCode()")]
        public void HashCode() {
            // Checking for overflows in hash calculation.
            var max = new FixedVector4(float.MaxValue, float.MaxValue, float.MaxValue, float.MaxValue);
            var min = new FixedVector4(float.MinValue, float.MinValue, float.MinValue, float.MinValue);
            Assert.AreNotEqual(max.GetHashCode(), FixedVector4.Zero.GetHashCode());
            Assert.AreNotEqual(min.GetHashCode(), FixedVector4.Zero.GetHashCode());

            // Common values
            var a = new FixedVector4(0f, 0f, 0f, 0f);
            Assert.AreEqual(a.GetHashCode(), FixedVector4.Zero.GetHashCode(), "Shouldn't do object id compare.");
            Assert.AreNotEqual(a.GetHashCode(), FixedVector4.One.GetHashCode());

            // Individual properties alter hash
            var xa = new FixedVector4(2f, 1f, 1f, 1f);
            var xb = new FixedVector4(3f, 1f, 1f, 1f);
            var ya = new FixedVector4(1f, 2f, 1f, 1f);
            var yb = new FixedVector4(1f, 3f, 1f, 1f);
            var za = new FixedVector4(1f, 1f, 2f, 1f);
            var zb = new FixedVector4(1f, 1f, 3f, 1f);
            var wa = new FixedVector4(1f, 1f, 1f, 2f);
            var wb = new FixedVector4(1f, 1f, 1f, 3f);
            Assert.AreNotEqual(xa.GetHashCode(), xb.GetHashCode(), "Different properties should change hash.");
            Assert.AreNotEqual(ya.GetHashCode(), yb.GetHashCode(), "Different properties should change hash.");
            Assert.AreNotEqual(za.GetHashCode(), zb.GetHashCode(), "Different properties should change hash.");
            Assert.AreNotEqual(wa.GetHashCode(), wb.GetHashCode(), "Different properties should change hash.");
            Assert.AreNotEqual(xa.GetHashCode(), ya.GetHashCode(), "Identical values on different properties should have different hashes.");
            Assert.AreNotEqual(xb.GetHashCode(), yb.GetHashCode(), "Identical values on different properties should have different hashes.");
            Assert.AreNotEqual(xb.GetHashCode(), zb.GetHashCode(), "Identical values on different properties should have different hashes.");
            Assert.AreNotEqual(yb.GetHashCode(), zb.GetHashCode(), "Identical values on different properties should have different hashes.");
            Assert.AreNotEqual(xb.GetHashCode(), wb.GetHashCode(), "Identical values on different properties should have different hashes.");
            Assert.AreNotEqual(yb.GetHashCode(), wb.GetHashCode(), "Identical values on different properties should have different hashes.");
            Assert.AreNotEqual(xa.GetHashCode(), yb.GetHashCode());
            Assert.AreNotEqual(ya.GetHashCode(), xb.GetHashCode());
            Assert.AreNotEqual(xa.GetHashCode(), zb.GetHashCode());
            Assert.AreNotEqual(xa.GetHashCode(), wb.GetHashCode());
        }

        [Test]
        public void Deconstruct()
        {
            FixedVector4 vector4 = new FixedVector4(float.MinValue, float.MaxValue, float.MinValue, float.MaxValue);

            Fixed x, y, z, w;

            vector4.Deconstruct(out x, out y, out z, out w);

            Assert.AreEqual(x, vector4.X);
            Assert.AreEqual(y, vector4.Y);
            Assert.AreEqual(z, vector4.Z);
            Assert.AreEqual(w, vector4.W);
        }

        [Test]
        public void Round()
        {
            FixedVector4 vector4 = new FixedVector4(0.0f, 0.4f, 0.6f, 1.0f);

            // CEILING

            FixedVector4 ceilMember = vector4;
            ceilMember.Ceiling();

            FixedVector4 ceilResult;
            FixedVector4.Ceiling(ref vector4, out ceilResult);

            Assert.AreEqual(new FixedVector4(0.0f, 1.0f, 1.0f, 1.0f), ceilMember);
            Assert.AreEqual(new FixedVector4(0.0f, 1.0f, 1.0f, 1.0f), FixedVector4.Ceiling(vector4));
            Assert.AreEqual(new FixedVector4(0.0f, 1.0f, 1.0f, 1.0f), ceilResult);

            // FLOOR

            FixedVector4 floorMember = vector4;
            floorMember.Floor();

            FixedVector4 floorResult;
            FixedVector4.Floor(ref vector4, out floorResult);

            Assert.AreEqual(new FixedVector4(0.0f, 0.0f, 0.0f, 1.0f), floorMember);
            Assert.AreEqual(new FixedVector4(0.0f, 0.0f, 0.0f, 1.0f), FixedVector4.Floor(vector4));
            Assert.AreEqual(new FixedVector4(0.0f, 0.0f, 0.0f, 1.0f), floorResult);

            // ROUND

            FixedVector4 roundMember = vector4;
            roundMember.Round();

            FixedVector4 roundResult;
            FixedVector4.Round(ref vector4, out roundResult);

            Assert.AreEqual(new FixedVector4(0.0f, 0.0f, 1.0f, 1.0f), roundMember);
            Assert.AreEqual(new FixedVector4(0.0f, 0.0f, 1.0f, 1.0f), FixedVector4.Round(vector4));
            Assert.AreEqual(new FixedVector4(0.0f, 0.0f, 1.0f, 1.0f), roundResult);
        }
    }
}
