using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Graphs;

namespace UnitTests.GraphsTests
{
    [TestClass]
    public class VertexTests
    {
        [TestMethod]
        public void VertexConstructorTest()
        {
            Vertex v = new Vertex("vertex1");
            Assert.AreEqual("vertex1", v.Name, "The created vertex does not have the specified name.");
        }
        [TestMethod]
        public void VertexEqualsTest()
        {
            Vertex v1 = new Vertex("vertex1");
            Vertex v2 = new Vertex("vertex2");
            Assert.IsTrue(v1.Equals(new Vertex("vertex1")), "v1 should be equal to new vertex with same name.");
            Assert.IsTrue(v2.Equals(new Vertex("vertex2")), "v2 should be equal to new vertex with same name.");
            Assert.IsFalse(v1.Equals(v2), "v1 and v2 should not be equal since they have different names.");
            Assert.IsFalse(v1.Equals("string"), "Vertex should not equal object of different type");
        }
        [TestMethod]
        public void VertexHashCodeTest()
        {
            Vertex v1 = new Vertex("vertex1");
            Vertex v2 = new Vertex("vertex2");
            Assert.AreEqual((new Vertex("vertex1")).GetHashCode(), v1.GetHashCode(), "Objects of same name have different hashCode (vertex1).");
            Assert.AreEqual((new Vertex("vertex2")).GetHashCode(), v2.GetHashCode(), "Objects of same name have different hashCode (vertex2).");
        }
        [TestMethod]
        public void VertexToStringTest()
        {
            Vertex v1 = new Vertex("vertex1");
            Vertex v2 = new Vertex("vertex2");

            Assert.AreEqual("vertex1", v1.ToString(), "ToString Method should generate Name (vertex1).");
            Assert.AreEqual("vertex2", v2.ToString(), "ToString Method should generate Name (vertex2).");
        }
    }
}
