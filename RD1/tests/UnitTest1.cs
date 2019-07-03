using Microsoft.VisualStudio.TestTools.UnitTesting;
using Matrixes;

namespace MatrixesTest
{
    [TestClass]
    public class ComaptibilityTest
    {
        [TestMethod]
        public void ComparingTest1()
        {
            Matrix a = new Matrix(new double[2, 2] { { 1, 0 },
                                                     { 0, 1 } });
            Matrix b = new Matrix(new double[2, 2] { { 1, 0 },
                                                     { 0, 1 } });
            Assert.IsTrue(a == b);
        }

        [TestMethod]
        public void ComparingTest2()
        {
            Matrix a = new Matrix(new double[2, 3] { { 1, 0, 0 },
                                                     { 0, 1, 0 } });
            Matrix b = new Matrix(new double[2, 2] { { 1, 0 },
                                                     { 0, 1 } });
            Assert.IsFalse(a == b);
        }

        [TestMethod]
        public void ComparingTest3()
        {
            Matrix a = new Matrix(new double[2, 3] { { 1, 0, 0 },
                                                     { 0, 1, 0 } });
            Matrix b = new Matrix(new double[2, 2] { { 1, 0 },
                                                     { 0, 1 } });
            Assert.IsTrue(a != b);
        }
    }

    [TestClass]
    public class MatrixArithmetic
    {
        [TestMethod]
        public void AddTest1()
        {
            Matrix a = new Matrix(new double[2, 2] { { 1, 2 },
                                                     { 3, 4 } });
            Matrix b = new Matrix(new double[2, 2] { { 5, 6 },
                                                     { 7, 8 } });
            Matrix c = a + b;
            Assert.IsTrue(c == new Matrix(new double[2, 2] { { 6, 8 }, { 10, 12 } }));
        }

        [TestMethod]
        public void AddTest2()
        {
            Matrix a = new Matrix(new double[3, 3] { { 1, 2, 3 },
                                                     { 4, 5, 6 },
                                                     { 7, 8, 9 } });
            Matrix b = new Matrix(new double[3, 3] { { 0, 0, 0 },
                                                     { 0, 0, 0 },
                                                     { 0, 0, 0 } });
            Matrix c = a + b;
            Assert.IsTrue(c == new Matrix(new double[3, 3]  { { 1, 2, 3 },
                                                              { 4, 5, 6 },
                                                              { 7, 8, 9 } }));
        }

        [TestMethod]
        public void SubtractTest1()
        {
            Matrix a = new Matrix(new double[2, 2] { { 1, 0 },
                                                     { 0, 1 } });
            Matrix b = new Matrix(new double[2, 2] { { 5, 6 },
                                                     { 7, 8 } });
            Matrix c = a - b;
            Assert.IsTrue(c == new Matrix(new double[2, 2] { { -4, -6 }, { -7, -7 } }));
        }

        [TestMethod]
        public void SubtractTest2()
        {
            Matrix a = new Matrix(new double[2, 2] { { 1, 0 },
                                                     { 0, 1 } });
            Matrix b = new Matrix(new double[2, 2] { { 5, 6 },
                                                     { 7, 8 } });
            Matrix c = a - b;
            Assert.IsTrue(c == new Matrix(new double[2, 2] { { -4, -6 }, { -7, -7 } }));
        }

        [TestMethod]
        public void MultiplyTest1()
        {
            Matrix a = new Matrix(new double[3, 3] { { 0.2, 0.1, 0.3 },
                                                     { 0.8, 0.4, 0.5 },
                                                     { 1, 2, 0.5 }});

            Matrix b = new Matrix(new double[3, 1] { { 0.4 },
                                                     { 0.5 },
                                                     { 1 } });
            Matrix c = a * b;
            Assert.IsTrue(c == new Matrix(new double[3, 1] { { 0.43 }, { 1.02 }, { 1.9 } }));
        }
    }


    [TestClass]
    public class BordersTest
    {
        [TestMethod]
        public void MultiplyMethod()
        {
            Matrix a = new Matrix(new double[3, 3] { { 0.2, 0.1, 0.3 },
                                                     { 0.8, 0.4, 0.5 },
                                                     { 1, 2, 0.5 }});

            Matrix b = new Matrix(new double[3, 1] { { 0.4 },
                                                     { 0.5 },
                                                     { 1 } });
            Matrix c = a * b;
            Assert.AreEqual<uint>(c.H, 3);
            Assert.AreEqual<uint>(c.W, 1);
        }
    }
}
