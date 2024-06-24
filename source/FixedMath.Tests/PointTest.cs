namespace FixedMath
{
    public class PointTest
    {
        [Test]
        public void Deconstruct()
        {
            FixedPoint point = new FixedPoint(int.MinValue, int.MaxValue);

            int x, y;

            point.Deconstruct(out x, out y);

            Assert.AreEqual(x, point.X);
            Assert.AreEqual(y, point.Y);
        }
    }
}
