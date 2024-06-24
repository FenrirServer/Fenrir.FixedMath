namespace FixedMath
{
    class MatrixTest
    {
        [Test]
        public void Add()
        {
            FixedMatrix test = new FixedMatrix(1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16);
            FixedMatrix expected = new FixedMatrix(2,4,6,8,10,12,14,16,18,20,22,24,26,28,30,32);
            FixedMatrix result;
            FixedMatrix.Add(ref test,ref test, out result);
            Assert.AreEqual(expected, result);
            Assert.AreEqual(expected, FixedMatrix.Add(test,test));
            Assert.AreEqual(expected, test+test);
        }
    }
}
