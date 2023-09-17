using FixedMath.Tests;
using System.ComponentModel;
using System.Globalization;

namespace FixedMath
{
    class Vector2Test
    {
        [Test]
        public void CatmullRom()
        {
            var expectedResult = new FixedVector2(5.1944f, 6.1944f);
            var v1 = new FixedVector2(1, 2); var v2 = new FixedVector2(3, 4); var v3 = new FixedVector2(5, 6); var v4 = new FixedVector2(7, 8); Fixed value = (Fixed)1.0972f;

            FixedVector2 result;
            FixedVector2.CatmullRom(ref v1, ref v2, ref v3, ref v4, value, out result);

            Assert.That(expectedResult, Is.EqualTo(FixedVector2.CatmullRom(v1, v2, v3, v4, value)).Using(Vector2Comparer.Epsilon));
            Assert.That(expectedResult, Is.EqualTo(result).Using(Vector2Comparer.Epsilon));
        }

        [Test]
        public void Multiply()
        {
            var vector = new FixedVector2(1, 2);

            // Test 0.0 scale.
            Assert.AreEqual(FixedVector2.Zero, (Fixed)0 * vector);
            Assert.AreEqual(FixedVector2.Zero, vector * (Fixed)0);
            Assert.AreEqual(FixedVector2.Zero, FixedVector2.Multiply(vector, (Fixed)0));
            Assert.AreEqual(FixedVector2.Multiply(vector, (Fixed)0), vector * (Fixed)0.0f);

            // Test 1.0 scale.
            Assert.AreEqual(vector, (Fixed)1 * vector);
            Assert.AreEqual(vector, vector * (Fixed)1);
            Assert.AreEqual(vector, FixedVector2.Multiply(vector, (Fixed)1));
            Assert.AreEqual(FixedVector2.Multiply(vector, (Fixed)1), vector * (Fixed)1.0f);

            var scaledVec = vector * (Fixed)2;

            // Test 2.0 scale.
            Assert.AreEqual(scaledVec, (Fixed)2 * vector);
            Assert.AreEqual(scaledVec, vector * (Fixed)2);
            Assert.AreEqual(scaledVec, FixedVector2.Multiply(vector, (Fixed)2));
            Assert.AreEqual(vector * (Fixed)2.0f, scaledVec);
            Assert.AreEqual((Fixed)2 * vector, FixedVector2.Multiply(vector, (Fixed)2));

            scaledVec = vector * (Fixed)0.999f;

            // Test 0.999 scale.
            Assert.AreEqual(scaledVec, (Fixed)0.999f * vector);
            Assert.AreEqual(scaledVec, vector * (Fixed)0.999f);
            Assert.AreEqual(scaledVec, FixedVector2.Multiply(vector, (Fixed)0.999f));
            Assert.AreEqual(vector * (Fixed)0.999f, scaledVec);
            Assert.AreEqual((Fixed)0.999f * vector, FixedVector2.Multiply(vector, (Fixed)0.999f));

            var vector2 = new FixedVector2(2, 2);

            // Test two vectors multiplication.
            Assert.AreEqual(new FixedVector2(vector.X * vector2.X, vector.Y * vector2.Y), vector * vector2);
            Assert.AreEqual(vector2 * vector, new FixedVector2(vector.X * vector2.X, vector.Y * vector2.Y));
            Assert.AreEqual(vector * vector2, FixedVector2.Multiply(vector, vector2));
            Assert.AreEqual(FixedVector2.Multiply(vector, vector2), vector * vector2);

            FixedVector2 refVec;

            // Overloads comparsion
            var vector3 = FixedVector2.Multiply(vector, vector2);
            FixedVector2.Multiply(ref vector, ref vector2, out refVec);
            Assert.AreEqual(vector3, refVec);

            vector3 = FixedVector2.Multiply(vector, (Fixed)2);
            FixedVector2.Multiply(ref vector, ref vector2, out refVec);
            Assert.AreEqual(vector3, refVec);
        }

        [Test]
        public void Hermite()
        {
            var t1 = new FixedVector2(1.40625f, 1.40625f);
            var t2 = new FixedVector2(2.662375f, 2.26537514f);

            var v1 = new FixedVector2(1, 1); var v2 = new FixedVector2(2, 2); var v3 = new FixedVector2(3, 3); var v4 = new FixedVector2(4, 4);
            var v5 = new FixedVector2(4, 3); var v6 = new FixedVector2(2, 1); var v7 = new FixedVector2(1, 2); var v8 = new FixedVector2(3, 4);

            Assert.That(t1, Is.EqualTo(FixedVector2.Hermite(v1, v2, v3, v4, (Fixed)0.25f)).Using(Vector2Comparer.Epsilon));
            Assert.That(t2, Is.EqualTo(FixedVector2.Hermite(v5, v6, v7, v8, (Fixed)0.45f)).Using(Vector2Comparer.Epsilon));

            FixedVector2 result1;
            FixedVector2 result2;

            FixedVector2.Hermite(ref v1, ref v2, ref v3, ref v4, (Fixed)0.25f, out result1);
            FixedVector2.Hermite(ref v5, ref v6, ref v7, ref v8, (Fixed)0.45f, out result2);

            Assert.That(t1, Is.EqualTo(result1).Using(Vector2Comparer.Epsilon));
            Assert.That(t2, Is.EqualTo(result2).Using(Vector2Comparer.Epsilon));
        }

        [Test]
        public void Transform()
        {
            // STANDART OVERLOADS TEST

            var expectedResult1 = new FixedVector2(24, 28);
            var expectedResult2 = new FixedVector2(-0.0168301091f, 2.30964f);

            var v1 = new FixedVector2(1, 2);
            var m1 = new FixedMatrix(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16);

            var v2 = new FixedVector2(1.1f, 2.45f);
            var q2 = new FixedQuaternion(0.11f, 0.22f, 0.33f, 0.55f);

            var q3 = new FixedQuaternion(1, 2, 3, 4);

            Assert.That(expectedResult1, Is.EqualTo(FixedVector2.Transform(v1, m1)).Using(Vector2Comparer.Epsilon));
            Assert.That(expectedResult2, Is.EqualTo(FixedVector2.Transform(v2, q2)).Using(Vector2Comparer.Epsilon));

            // OUTPUT OVERLOADS TEST

            FixedVector2 result1;
            FixedVector2 result2;

            FixedVector2.Transform(ref v1, ref m1, out result1);
            FixedVector2.Transform(ref v2, ref q2, out result2);

            Assert.That(expectedResult1, Is.EqualTo(result1).Using(Vector2Comparer.Epsilon));
            Assert.That(expectedResult2, Is.EqualTo(result2).Using(Vector2Comparer.Epsilon));

            // TRANSFORM ON LIST (MATRIX)
            {
                var sourceList1 = new FixedVector2[10];
                var desinationList1 = new FixedVector2[10];

                for (int i = 0; i < 10; i++)
                {
                    sourceList1[i] = (new FixedVector2(1 + i, 1 + i));
                }

                FixedVector2.Transform(sourceList1, 0, ref m1, desinationList1, 0, 10);

                for (int i = 0; i < 10; i++)
                {
                    Assert.That(desinationList1[i], Is.EqualTo(new FixedVector2(19 + (6 * i), 22 + (8 * i))).Using(Vector2Comparer.Epsilon));
                }
            }
            // TRANSFORM ON LIST (MATRIX)(DESTINATION & SOURCE)
            {
                var sourceList2 = new FixedVector2[10];
                var desinationList2 = new FixedVector2[10];

                for (int i = 0; i < 10; i++)
                {
                    sourceList2[i] = (new FixedVector2(1 + i, 1 + i));
                }

                FixedVector2.Transform(sourceList2, 2, ref m1, desinationList2, 1, 3);

                Assert.That(FixedVector2.Zero, Is.EqualTo(desinationList2[0]).Using(Vector2Comparer.Epsilon));

                Assert.That(new FixedVector2(31, 38), Is.EqualTo(desinationList2[1]).Using(Vector2Comparer.Epsilon));
                Assert.That(new FixedVector2(37, 46), Is.EqualTo(desinationList2[2]).Using(Vector2Comparer.Epsilon));
                Assert.That(new FixedVector2(43, 54), Is.EqualTo(desinationList2[3]).Using(Vector2Comparer.Epsilon));

                for (int i = 4; i < 10; i++)
                {
                    Assert.That(FixedVector2.Zero, Is.EqualTo(desinationList2[i]).Using(Vector2Comparer.Epsilon));
                }
            }
            // TRANSFORM ON LIST (MATRIX)(SIMPLE)
            {
                var sourceList3 = new FixedVector2[10];
                var desinationList3 = new FixedVector2[10];

                for (int i = 0; i < 10; i++)
                {
                    sourceList3[i] = (new FixedVector2(1 + i, 1 + i));
                }

                FixedVector2.Transform(sourceList3, ref m1, desinationList3);

                for (int i = 0; i < 10; i++)
                {
                    Assert.That(desinationList3[i], Is.EqualTo(new FixedVector2(19 + (6 * i), 22 + (8 * i))).Using(Vector2Comparer.Epsilon));
                }
            }
            // TRANSFORM ON LIST (QUATERNION)
            {
                var sourceList4 = new FixedVector2[10];
                var desinationList4 = new FixedVector2[10];

                for (int i = 0; i < 10; i++)
                {
                    sourceList4[i] = (new FixedVector2(1 + i, 1 + i));
                }

                FixedVector2.Transform(sourceList4, 0, ref q3, desinationList4, 0, 10);

                for (int i = 0; i < 10; i++)
                {
                    Assert.That(new FixedVector2(-45 + (-45 * i), 9 + (9 * i)), Is.EqualTo(desinationList4[i]).Using(Vector2Comparer.Epsilon));
                }
            }
            // TRANSFORM ON LIST (QUATERNION)(DESTINATION & SOURCE)
            {
                var sourceList5 = new FixedVector2[10];
                var desinationList5 = new FixedVector2[10];

                for (int i = 0; i < 10; i++)
                {
                    sourceList5[i] = (new FixedVector2(1 + i, 1 + i));
                }

                FixedVector2.Transform(sourceList5, 2, ref q3, desinationList5, 1, 3);

                Assert.That(FixedVector2.Zero, Is.EqualTo(desinationList5[0]).Using(Vector2Comparer.Epsilon));

                Assert.That(new FixedVector2(-135, 27), Is.EqualTo(desinationList5[1]).Using(Vector2Comparer.Epsilon));
                Assert.That(new FixedVector2(-180, 36), Is.EqualTo(desinationList5[2]).Using(Vector2Comparer.Epsilon));
                Assert.That(new FixedVector2(-225, 45), Is.EqualTo(desinationList5[3]).Using(Vector2Comparer.Epsilon));

                for (int i = 4; i < 10; i++)
                {
                    Assert.That(FixedVector2.Zero, Is.EqualTo(desinationList5[i]).Using(Vector2Comparer.Epsilon));
                }
            }
            // TRANSFORM ON LIST (QUATERNION)(SIMPLE)
            {
                var sourceList6 = new FixedVector2[10];
                var desinationList6 = new FixedVector2[10];

                for (int i = 0; i < 10; i++)
                {
                    sourceList6[i] = (new FixedVector2(1 + i, 1 + i));
                }

                FixedVector2.Transform(sourceList6, ref q3, desinationList6);

                for (int i = 0; i < 10; i++)
                {
                    Assert.That(new FixedVector2(-45 + (-45 * i), 9 + (9 * i)), Is.EqualTo(desinationList6[i]).Using(Vector2Comparer.Epsilon));
                }
            }
        }

        [Test]
        public void TransformNormal()
        {
            var normal = new FixedVector2(1.5f, 2.5f);
            var matrix = new FixedMatrix(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16);

            var expectedResult1 = new FixedVector2(14, 18);
            var expectedResult2 = expectedResult1;

            Assert.That(expectedResult1, Is.EqualTo(FixedVector2.TransformNormal(normal, matrix)).Using(Vector2Comparer.Epsilon));

            FixedVector2 result;
            FixedVector2.TransformNormal(ref normal, ref matrix, out result);

            Assert.That(expectedResult2, Is.EqualTo(result).Using(Vector2Comparer.Epsilon));

            // TRANSFORM ON LIST
            {
                var sourceArray1 = new FixedVector2[10];
                var destinationArray1 = new FixedVector2[10];

                for (int i = 0; i < 10; i++)
                {
                    sourceArray1[i] = new FixedVector2(i, i);
                }

                FixedVector2.TransformNormal(sourceArray1, 0, ref matrix, destinationArray1, 0, 10);

                for (int i = 0; i < 10; i++)
                {
                    Assert.That(new FixedVector2(0 + (6 * i), 0 + (8 * i)), Is.EqualTo(destinationArray1[i]).Using(Vector2Comparer.Epsilon));
                }
            }
            // TRANSFORM ON LIST(SOURCE OFFSET)
            {
                var sourceArray2 = new FixedVector2[10];
                var destinationArray2 = new FixedVector2[10];

                for (int i = 0; i < 10; i++)
                {
                    sourceArray2[i] = new FixedVector2(i, i);
                }

                FixedVector2.TransformNormal(sourceArray2, 5, ref matrix, destinationArray2, 0, 5);

                for (int i = 0; i < 5; i++)
                {
                    Assert.That(new FixedVector2(30 + (6 * i), 40 + (8 * i)), Is.EqualTo(destinationArray2[i]).Using(Vector2Comparer.Epsilon));
                }

                for (int i = 5; i < 10; i++)
                {
                    Assert.That(FixedVector2.Zero, Is.EqualTo(destinationArray2[i]).Using(Vector2Comparer.Epsilon));
                }
            }
            // TRANSFORM ON LIST(DESTINATION OFFSET)
            {
                var sourceArray3 = new FixedVector2[10];
                var destinationArray3 = new FixedVector2[10];

                for (int i = 0; i < 10; ++i)
                {
                    sourceArray3[i] = new FixedVector2(i, i);
                }

                FixedVector2.TransformNormal(sourceArray3, 0, ref matrix, destinationArray3, 5, 5);

                for (int i = 0; i < 6; ++i)
                {
                    Assert.That(FixedVector2.Zero, Is.EqualTo(destinationArray3[i]).Using(Vector2Comparer.Epsilon));
                }

                Assert.That(new FixedVector2(6, 8), Is.EqualTo(destinationArray3[6]).Using(Vector2Comparer.Epsilon));
                Assert.That(new FixedVector2(12, 16), Is.EqualTo(destinationArray3[7]).Using(Vector2Comparer.Epsilon));
                Assert.That(new FixedVector2(18, 24), Is.EqualTo(destinationArray3[8]).Using(Vector2Comparer.Epsilon));
                Assert.That(new FixedVector2(24, 32), Is.EqualTo(destinationArray3[9]).Using(Vector2Comparer.Epsilon));
            }
            // TRANSFORM ON LIST(DESTINATION & SOURCE)
            {
                var sourceArray4 = new FixedVector2[10];
                var destinationArray4 = new FixedVector2[10];

                for (int i = 0; i < 10; ++i)
                {
                    sourceArray4[i] = new FixedVector2(i, i);
                }

                FixedVector2.TransformNormal(sourceArray4, 2, ref matrix, destinationArray4, 3, 6);

                for (int i = 0; i < 3; ++i)
                {
                    Assert.That(FixedVector2.Zero, Is.EqualTo(destinationArray4[i]).Using(Vector2Comparer.Epsilon));
                }

                Assert.That(new FixedVector2(12, 16), Is.EqualTo(destinationArray4[3]).Using(Vector2Comparer.Epsilon));
                Assert.That(new FixedVector2(18, 24), Is.EqualTo(destinationArray4[4]).Using(Vector2Comparer.Epsilon));
                Assert.That(new FixedVector2(24, 32), Is.EqualTo(destinationArray4[5]).Using(Vector2Comparer.Epsilon));
                Assert.That(new FixedVector2(30, 40), Is.EqualTo(destinationArray4[6]).Using(Vector2Comparer.Epsilon));
                Assert.That(new FixedVector2(36, 48), Is.EqualTo(destinationArray4[7]).Using(Vector2Comparer.Epsilon));
                Assert.That(new FixedVector2(42, 56), Is.EqualTo(destinationArray4[8]).Using(Vector2Comparer.Epsilon));

                Assert.That(FixedVector2.Zero, Is.EqualTo(destinationArray4[9]).Using(Vector2Comparer.Epsilon));
            }
            // TRANSFORM ON LIST(SIMPLE)
            {
                var sourceArray5 = new FixedVector2[10];
                var destinationArray5 = new FixedVector2[10];

                for (int i = 0; i < 10; ++i)
                {
                    sourceArray5[i] = new FixedVector2(i, i);
                }

                FixedVector2.TransformNormal(sourceArray5, ref matrix, destinationArray5);

                for (int i = 0; i < 10; ++i)
                {
                    Assert.That(new FixedVector2(0 + (6 * i), 0 + (8 * i)), Is.EqualTo(destinationArray5[i]).Using(Vector2Comparer.Epsilon));
                }
            }
        }

        [Test]
        public void TypeConverter()
        {
            var converter = TypeDescriptor.GetConverter(typeof(FixedVector2));
            var invariantCulture = CultureInfo.InvariantCulture;

            Assert.AreEqual(new FixedVector2(32, 64), converter.ConvertFromString(null, invariantCulture, "32, 64"));
            Assert.AreEqual(new FixedVector2(0.5f, 2.75f), converter.ConvertFromString(null, invariantCulture, "0.5, 2.75"));
            Assert.AreEqual(new FixedVector2(1024.5f, 2048.75f), converter.ConvertFromString(null, invariantCulture, "1024.5, 2048.75"));
            Assert.AreEqual("32, 64", converter.ConvertToString(null, invariantCulture, new FixedVector2(32, 64)));
            Assert.AreEqual("0.5, 2.75", converter.ConvertToString(null, invariantCulture, new FixedVector2(0.5f, 2.75f)));
            Assert.AreEqual("1024.5, 2048.75", converter.ConvertToString(null, invariantCulture, new FixedVector2(1024.5f, 2048.75f)));

            var otherCulture = new CultureInfo("el-GR");
            var vectorStr = (1024.5f).ToString(otherCulture) + otherCulture.TextInfo.ListSeparator + " " +
                            (2048.75f).ToString(otherCulture);
            Assert.AreEqual(new FixedVector2(1024.5f, 2048.75f), converter.ConvertFromString(null, otherCulture, vectorStr));
            Assert.AreEqual(vectorStr, converter.ConvertToString(null, otherCulture, new FixedVector2(1024.5f, 2048.75f)));
        }

        [Test, Ignore("Does not work for Fixed type since it's based on long for which MinValue.GetHashCode() == MaxValue.GetHashCode()")]
        public void HashCode()
        {
            // Checking for overflows in hash calculation.
            var max = new FixedVector2(Fixed.MaxValue, Fixed.MaxValue);
            var min = new FixedVector2(Fixed.MinValue, Fixed.MinValue);
            Assert.AreNotEqual(max.GetHashCode(), FixedVector2.Zero.GetHashCode());
            Assert.AreNotEqual(min.GetHashCode(), FixedVector2.Zero.GetHashCode());

            // Common values
            var a = new FixedVector2(0f, 0f);
            Assert.AreEqual(a.GetHashCode(), FixedVector2.Zero.GetHashCode());
            Assert.AreNotEqual(a.GetHashCode(), FixedVector2.One.GetHashCode());

            // Individual properties alter hash
            var xa = new FixedVector2(2f, 1f);
            var xb = new FixedVector2(3f, 1f);
            var ya = new FixedVector2(1f, 2f);
            var yb = new FixedVector2(1f, 3f);
            Assert.AreNotEqual(xa.GetHashCode(), xb.GetHashCode(), "Different properties should change hash.");
            Assert.AreNotEqual(ya.GetHashCode(), yb.GetHashCode(), "Different properties should change hash.");

            Assert.AreNotEqual(xa.GetHashCode(), ya.GetHashCode(), "Identical values on different properties should have different hashes.");
            Assert.AreNotEqual(xb.GetHashCode(), yb.GetHashCode(), "Identical values on different properties should have different hashes.");

            Assert.AreNotEqual(xa.GetHashCode(), yb.GetHashCode());
            Assert.AreNotEqual(ya.GetHashCode(), xb.GetHashCode());
        }


        [Test]
        public void ToPoint()
        {
            Assert.AreEqual(new FixedPoint(0, 0), new FixedVector2(0.1f, 0.1f).ToPoint());
            Assert.AreEqual(new FixedPoint(0, 0), new FixedVector2(0.5f, 0.5f).ToPoint());
            Assert.AreEqual(new FixedPoint(0, 0), new FixedVector2(0.55f, 0.55f).ToPoint());
            Assert.AreEqual(new FixedPoint(0, 0), new FixedVector2(1.0f - 0.1f, 1.0f - 0.1f).ToPoint());
            Assert.AreEqual(new FixedPoint(1, 1), new FixedVector2(1.0f - float.Epsilon, 1.0f - float.Epsilon).ToPoint());
            Assert.AreEqual(new FixedPoint(1, 1), new FixedVector2(1.0f, 1.0f).ToPoint());
            Assert.AreEqual(new FixedPoint(19, 27), new FixedVector2(19.033f, 27.1f).ToPoint());
        }

        [Test]
        public void Deconstruct()
        {
            FixedVector2 vector2 = new FixedVector2(float.MinValue, float.MaxValue);

            Fixed x, y;

            vector2.Deconstruct(out x, out y);

            Assert.AreEqual(x, vector2.X);
            Assert.AreEqual(y, vector2.Y);
        }

        [Test]
        public void Round()
        {
            FixedVector2 vector2 = new FixedVector2(0.4f, 0.6f);

            // CEILING

            FixedVector2 ceilMember = vector2;
            ceilMember.Ceiling();

            FixedVector2 ceilResult;
            FixedVector2.Ceiling(ref vector2, out ceilResult);

            Assert.AreEqual(new FixedVector2(1.0f, 1.0f), ceilMember);
            Assert.AreEqual(new FixedVector2(1.0f, 1.0f), FixedVector2.Ceiling(vector2));
            Assert.AreEqual(new FixedVector2(1.0f, 1.0f), ceilResult);

            // FLOOR

            FixedVector2 floorMember = vector2;
            floorMember.Floor();

            FixedVector2 floorResult;
            FixedVector2.Floor(ref vector2, out floorResult);

            Assert.AreEqual(new FixedVector2(0.0f, 0.0f), floorMember);
            Assert.AreEqual(new FixedVector2(0.0f, 0.0f), FixedVector2.Floor(vector2));
            Assert.AreEqual(new FixedVector2(0.0f, 0.0f), floorResult);

            // ROUND

            FixedVector2 roundMember = vector2;
            roundMember.Round();

            FixedVector2 roundResult;
            FixedVector2.Round(ref vector2, out roundResult);

            Assert.AreEqual(new FixedVector2(0.0f, 1.0f), roundMember);
            Assert.AreEqual(new FixedVector2(0.0f, 1.0f), FixedVector2.Round(vector2));
            Assert.AreEqual(new FixedVector2(0.0f, 1.0f), roundResult);
        }

    }
}
