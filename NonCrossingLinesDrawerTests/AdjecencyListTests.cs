using NonCrossingLinesDrawer;
using NUnit.Framework;

namespace NonCrossingLinesDrawerTests
{
    public class Tests
    {
        AdjacencyList list;

        [OneTimeSetUp]
        public void Setup()
        {
            list = new AdjacencyList(5);
        }

        [Test]
        public void AdjacencyListNumberOfPointsTest()
        {
            Assert.AreEqual(25, list.List.Count);
        }

        [Test]
        public void AdjacencyListInitalizeTest()
        {
            Assert.NotNull(list.List);
            for(int i=0;i<25;i++)
            {
                Assert.NotNull(list.List[0]);
            }
        }

        [Test]
        public void CorneresHaveTwoNeighoursTest()
        {
            Assert.AreEqual(2,list.List[0].Count);
            Assert.AreEqual(2, list.List[4].Count);
            Assert.AreEqual(2, list.List[20].Count);
            Assert.AreEqual(2, list.List[24].Count);
        }

        [Test]
        public void InMiddlePointHave4NeighoursTest()
        {
            Assert.AreEqual(4, list.List[6].Count);
        }

        [Test]
        public void BorderPointHave3NeighoursTest()
        {
            Assert.AreEqual(3, list.List[5].Count);
        }
    }
}