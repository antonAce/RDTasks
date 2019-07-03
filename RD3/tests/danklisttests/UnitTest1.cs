using System;
using NUnit.Framework;
using RDTask2;

namespace Tests
{
    [TestFixture]
    public class DankListTest_Boundaries
    {
        DankList<int> testDankList;

        [SetUp]
        public void InitialDankListSetup()
        {
            testDankList = new DankList<int>();
            testDankList.Add(0);
            testDankList.Add(1);
            testDankList.Add(2);
            testDankList.Add(3);
        }

        [Test]
        public void IntInBoundaries1()
        {
            Assert.AreEqual(testDankList[1], 1);
        }

        [Test]
        public void IntInBoundaries2()
        {
            Assert.AreEqual(testDankList[0], 0);
        }

        [Test]
        public void IntOutOfBoundaries1()
        {
            Assert.AreEqual(testDankList[-1], 3);
        }

        [Test]
        public void StringInBoundaries1()
        {
            Assert.AreEqual(testDankList["0"], 0);
        }

        [Test]
        public void StringInBoundaries2()
        {
            Assert.AreEqual(testDankList["3"], 3);
        }

        [Test]
        public void StringOutOfBoundaries1()
        {
            Assert.AreEqual(testDankList["-1"], 3);
        }

        [Test]
        public void StringOutOfBoundaries2()
        {
            Assert.AreEqual(testDankList["-2"], 2);
        }

        [Test]
        public void DoubleInBoundaries1()
        {
            Assert.AreEqual(testDankList[1.2], new int[2] { 1, 2 });
        }

        [Test]
        public void DoubleInBoundaries2()
        {
            Assert.AreEqual(testDankList[1.3], new int[3] { 1, 2, 3 });
        }

        [Test]
        public void DoubleInBoundaries3()
        {
            Assert.AreEqual(testDankList[0.2], new int[3] { 0, 1, 2 });
        }
    }

    [TestFixture]
    public class DankListTest_Boundaries_Exceptions
    {
        DankList<int> testDankList;

        [SetUp]
        public void InitialDankListSetup()
        {
            testDankList = new DankList<int>();
            testDankList.Add(0);
            testDankList.Add(0);
        }

        [Test]
        public void IntOutOfRange1()
        {
            Assert.Throws<IndexOutOfRangeException>(() => { int sillyChoice = testDankList[2]; });
        }

        [Test]
        public void IntOutOfRange2()
        {
            Assert.Throws<IndexOutOfRangeException>(() => { int sillyChoice = testDankList[-3]; });
        }

        [Test]
        public void StringOutOfRange1()
        {
            Assert.Throws<IndexOutOfRangeException>(() => { int sillyChoice = testDankList["2"]; });
        }

        [Test]
        public void StringOutOfRange2()
        {
            Assert.Throws<IndexOutOfRangeException>(() => { int sillyChoice = testDankList["-3"]; });
        }

        [Test]
        public void DoubleOutOfRange1()
        {
            Assert.Throws<IndexOutOfRangeException>(() => { int[] sillyChoice = testDankList[0.3]; });
        }

        [Test]
        public void DoubleOutOfRange2()
        {
            Assert.Throws<IndexOutOfRangeException>(() => { int[] sillyChoice = testDankList[2.5]; });
        }

        [Test]
        public void DoubleOutOfRange3()
        {
            Assert.Throws<IndexOutOfRangeException>(() => { int[] sillyChoice = testDankList[-6.0]; });
        }
    }

    [TestFixture]
    public class DankListTest_Methods
    {
        DankList<int> filledDankList, emptyDankList;

        [SetUp]
        public void InitialDankListSetup()
        {
            filledDankList = new DankList<int>();
            emptyDankList = new DankList<int>();
            filledDankList.Add(0);
            filledDankList.Add(1);
            filledDankList.Add(2);
            filledDankList.Add(3);
        }

        [Test]
        public void CountFilledDankListTest()
        {
            Assert.AreEqual(filledDankList.Count, 4);
        }

        [Test]
        public void CountEmptyDankListTest()
        {
            Assert.AreEqual(emptyDankList.Count, 0);
        }

        [Test]
        public void ClearDankListTest1()
        {
            emptyDankList.Add(0);
            emptyDankList.Add(1);
            emptyDankList.Add(2);
            emptyDankList.Add(3);

            emptyDankList.Clear();

            Assert.AreEqual(emptyDankList.Count, 0);
        }

        [Test]
        public void ContainsDankListTest1()
        {
            Assert.IsTrue(filledDankList.Contains(2));
        }

        [Test]
        public void ContainsDankListTest2()
        {
            Assert.IsFalse(filledDankList.Contains(42));
        }

        [Test]
        public void InsertDankListTest1()
        {
            filledDankList.Insert(2, 5);
            int resCount = filledDankList.Count;

            Assert.AreEqual(resCount, 5);
        }

        [Test]
        public void RemoveElementFromDankListAt0()
        {
            int[] result = new int[3];
            int i = 0;
            filledDankList.RemoveAt(0);

            foreach (var item in filledDankList)
            {
                result[i] = item;
                i++;
            }

            Assert.AreEqual(result, new int[3] { 1, 2, 3 });
        }

        [Test]
        public void RemoveElementFromDankListAt2()
        {
            int[] result = new int[3];
            int i = 0;
            filledDankList.RemoveAt(2);

            foreach (var item in filledDankList)
            {
                result[i] = item;
                i++;
            }

            Assert.AreEqual(result, new int[3] { 0, 1, 3 });
        }

        [Test]
        public void RemoveElementFromDankListAtInaccessablePosition()
        {
            Assert.Throws<IndexOutOfRangeException>(() => { filledDankList.RemoveAt(42); });
        }

        [Test]
        public void RemoveElementFromDankListAtInaccessablePosition2()
        {
            Assert.Throws<IndexOutOfRangeException>(() => { filledDankList.RemoveAt(4); });
        }
    }
}