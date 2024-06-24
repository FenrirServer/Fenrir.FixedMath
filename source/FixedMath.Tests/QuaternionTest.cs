using FixedMath.Tests;

namespace FixedMath
{
    class QuaternionTest
    {
        private void Compare(FixedQuaternion expected, FixedQuaternion source)
        {
            Assert.That(expected, Is.EqualTo(source).Using(QuaternionComparer.Epsilon));
        }

        private void Compare(Fixed expected, Fixed source)
        {
            Assert.That(expected, Is.EqualTo(source).Using(FixedComparer.Epsilon));
        }

        [Test]
        public void Constructors()
        {
            FixedQuaternion expected;
            expected.X = (Fixed)1;
            expected.Y = (Fixed)2;
            expected.Z = (Fixed)3;
            expected.W = (Fixed)4;
            Compare(expected, new FixedQuaternion(1, 2, 3, 4));
            Compare(expected, new FixedQuaternion(new FixedVector3(1, 2, 3), 4));
            Compare(expected, new FixedQuaternion(new FixedVector4(1, 2, 3, 4)));
        }

        [Test]
        public void Properties()
        {
            Compare(new FixedQuaternion(0, 0, 0, 1), FixedQuaternion.Identity);
        }

        [Test]
        public void Add()
        {
            FixedQuaternion q1 = new FixedQuaternion(1, 2, 3, 4);
            FixedQuaternion q2 = new FixedQuaternion(1, 2, 3, 4);
            FixedQuaternion expected = new FixedQuaternion(2, 4, 6, 8);
            Compare(expected, FixedQuaternion.Add(q1, q2));

            FixedQuaternion result;
            FixedQuaternion.Add(ref q1, ref q2, out result);
            Compare(expected, result);
        }

        [Test]
        public void Concatenate()
        {
            FixedQuaternion q1 = new FixedQuaternion(1, 2.5f, 3, 4);
            FixedQuaternion q2 = new FixedQuaternion(1, 2, -3.8f, 2);
            FixedQuaternion expected = new FixedQuaternion(21.5f, 6.2f, -8.7f, 13.4f);
            Compare(expected, FixedQuaternion.Concatenate(q1, q2));

            FixedQuaternion result;
            FixedQuaternion.Concatenate(ref q1, ref q2, out result);
            Compare(expected, result);
        }

        [Test]
        public void Conjugate()
        {
            FixedQuaternion q = new FixedQuaternion(1, 2, 3, 4);
            FixedQuaternion expected = new FixedQuaternion(-1, -2, -3, 4);
            Compare(expected, FixedQuaternion.Conjugate(q));

            FixedQuaternion result;
            FixedQuaternion.Conjugate(ref q, out result);
            Compare(expected, result);

            q.Conjugate();
            Compare(expected, q);
        }

        [Test]
        public void CreateFromAxisAngle()
        {
            FixedVector3 axis = new FixedVector3(0.5f, 1.1f, -3.8f);
            float angle = 0.25f;
            FixedQuaternion expected = new FixedQuaternion(0.06233737f, 0.1371422f, -0.473764f, 0.9921977f);

            Compare(expected, FixedQuaternion.CreateFromAxisAngle(axis, (Fixed)angle));

            FixedQuaternion result;
            FixedQuaternion.CreateFromAxisAngle(ref axis, (Fixed)angle, out result);
            Compare(expected, result);
        }

        [Test]
        public void CreateFromRotationMatrix()
        {
            var matrix = FixedMatrix.CreateFromYawPitchRoll((Fixed)0.15f, (Fixed)1.18f, (Fixed)(-0.22f));
            FixedQuaternion expected = new FixedQuaternion(0.5446088f, 0.1227905f, -0.1323988f, 0.8190203f);
            Compare(expected, FixedQuaternion.CreateFromRotationMatrix(matrix));

            FixedQuaternion result;
            FixedQuaternion.CreateFromRotationMatrix(ref matrix, out result);
            Compare(expected, result);
        }

        [Test]
        public void CreateFromYawPitchRoll()
        {
            FixedQuaternion expected = new FixedQuaternion(0.5446088f, 0.1227905f, -0.1323988f, 0.8190203f);
            Compare(expected, FixedQuaternion.CreateFromYawPitchRoll((Fixed)0.15f, (Fixed)1.18f, (Fixed)(-0.22f)));

            FixedQuaternion result;
            FixedQuaternion.CreateFromYawPitchRoll((Fixed)0.15f, (Fixed)1.18f, (Fixed)(-0.22f), out result);
            Compare(expected, result);
        }

        [Test]
        public void Divide()
        {
            FixedQuaternion q1 = new FixedQuaternion(1, 2, 3, 4);
            FixedQuaternion q2 = new FixedQuaternion(0.2f, -0.6f, 11.9f, 0.01f);
            FixedQuaternion expected = new FixedQuaternion(-0.1858319f, 0.09661285f, -0.3279344f, 0.2446305f);
            Compare(expected, FixedQuaternion.Divide(q1, q2));

            FixedQuaternion result;
            FixedQuaternion.Divide(ref q1, ref q2, out result);
            Compare(expected, result);
        }

        [Test]
        public void Length()
        {
            FixedQuaternion q1 = new FixedQuaternion(1, 2, 3, 4);
            Compare((Fixed)5.477226f,q1.Length());
        }

        [Test]
        public void LengthSquared()
        {
            FixedQuaternion q1 = new FixedQuaternion(1, 2, 3, 4);
            Compare((Fixed)30.0f, q1.LengthSquared());
        }

        [Test]
        public void Normalize()
        {
            FixedQuaternion q = new FixedQuaternion(1, 2, 3, 4);
            FixedQuaternion expected = new FixedQuaternion(0.1825742f,0.3651484f,0.5477226f,0.7302967f);

            Compare(expected, FixedQuaternion.Normalize(q));

            FixedQuaternion result;
            FixedQuaternion.Normalize(ref q, out result);
            Compare(expected, result);


            q.Normalize();
            Compare(expected, q);
        }

        [Test]
        public void Deconstruct()
        {
            FixedQuaternion quaternion = new FixedQuaternion(float.MinValue, float.MaxValue, float.MinValue, float.MaxValue);

            Fixed x, y, z, w;

            quaternion.Deconstruct(out x, out y, out z, out w);

            Assert.AreEqual(x, quaternion.X);
            Assert.AreEqual(y, quaternion.Y);
            Assert.AreEqual(z, quaternion.Z);
            Assert.AreEqual(w, quaternion.W);
        }
    }
}
