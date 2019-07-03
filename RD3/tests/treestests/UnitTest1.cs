using Microsoft.VisualStudio.TestTools.UnitTesting;
using Primitives;

namespace BTreeTests
{
    [TestClass]
    public class BuildingTreeTests
    {
        [TestMethod]
        public void BuildTest1()
        {
            BPrimitive<int> node = new BNode<int>(0);
            BPrimitive<int> node1 = new BNode<int>(3);
            BPrimitive<int> node2 = new BNode<int>(21);
            BPrimitive<int> node3 = new BNode<int>(12);

            node1.PushLeft(node3);
            node1.PushRight(new BNode<int>(43));
            node.PushLeft(node1);
            node.PushRight(node2);

            Assert.AreEqual(node.ToString(), "(((12) <- 3 -> (43)) <- 0 -> (21))");
        }

        [TestMethod]
        public void BuildTest2()
        {
            BPrimitive<int> node = new BNode<int>(0);
            BPrimitive<int> node1 = new BNode<int>(3);
            BPrimitive<int> node2 = new BNode<int>(21);
            BPrimitive<int> node3 = new BNode<int>(12);

            node1.PushLeft(node3);
            node1.PushRight(new BNode<int>(43));
            node.PushLeft(node1);
            node.PushRight(node2);
            node.PopLeft();

            Assert.AreEqual(node.ToString(), "(0 -> (21))");
        }

        [TestMethod]
        public void BuildTest3()
        {
            BPrimitive<int> node = new BNode<int>(0);
            BPrimitive<int> node1 = new BNode<int>(3);
            BPrimitive<int> node2 = new BNode<int>(21);
            BPrimitive<int> node3 = new BNode<int>(12);

            node1.PushLeft(node3);
            node1.PushRight(new BNode<int>(43));
            node.PushLeft(node1);
            node.PushRight(node2);
            node.PopLeft();
            node.PopRight();

            Assert.AreEqual(node.ToString(), "(0)");
        }
    }
}
