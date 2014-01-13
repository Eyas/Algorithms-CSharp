using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Graphs;

namespace UnitTests.GraphsTests
{
    [TestClass]
    public class EdgeTests
    {
        [TestMethod]
        public void EdgeConstructorTest()
        {
            Vertex u = new Vertex("vertex1");
            Vertex v = new Vertex("vertex2");
            const int w = 5;

            Edge e1 = new Edge(u, v);
            Edge e2 = new Edge(u, v, w);

            Assert.AreEqual(u, e1.u);
            Assert.AreEqual(v, e1.v);
            Assert.IsFalse(e1.weight.HasValue);

            Assert.AreEqual(u, e2.u);
            Assert.AreEqual(v, e2.v);
            Assert.IsTrue(e2.weight.HasValue);
            Assert.AreEqual(w, e2.weight);
        }
        [TestMethod]
        public void EdgeEqualsTest()
        {
            Vertex u = new Vertex("vertex1");
            Vertex v = new Vertex("vertex2");
            Vertex w = new Vertex("vertex3");
            const int five = 5;
            const int six = 6;

            Edge uv = new Edge(u, v);
            Edge vw = new Edge(v, w);
            Edge wu = new Edge(w, u);
            Edge vu = new Edge(v, u);

            Edge uv5 = new Edge(u, v, five);
            Edge uv6 = new Edge(u, v, six);
            Edge vu5 = new Edge(v, u, five);
            Edge vu6 = new Edge(v, u, six);

            Assert.AreEqual(new Edge(u, v), uv);
            Assert.AreEqual(new Edge(u, v, five), uv);
            Assert.AreEqual(new Edge(u, v, five), uv5);
            Assert.AreEqual(new Edge(u, v, six), uv5);

            Assert.AreNotEqual(uv, vw);
            Assert.AreNotEqual(uv, vu);
            Assert.AreNotEqual(vw, vu);
            Assert.AreNotEqual(uv5, vw);
            Assert.AreNotEqual(uv5, vu);

            Assert.AreNotEqual(five, uv5);
            Assert.AreNotEqual(uv5, five);
        }
        [TestMethod]
        public void EdgeHashCodeTest()
        {
            Vertex u = new Vertex("vertex1");
            Vertex v = new Vertex("vertex2");

            Edge uv = new Edge(u, v);
            Edge vu = new Edge(v, u);

            Assert.AreEqual((new Edge(u, v)).GetHashCode(), uv.GetHashCode());
            Assert.AreEqual((new Edge(v, u)).GetHashCode(), vu.GetHashCode());
        }
        [TestMethod]
        public void EdgeToStringTest()
        {
            Vertex u = new Vertex("vertex1");
            Vertex v = new Vertex("vertex2");
            const int five = 5;

            Edge uv = new Edge(u, v);
            Edge vu = new Edge(v, u);
            Edge uv5 = new Edge(u, v, five);

            Assert.AreEqual("Edge(vertex1,vertex2)", uv.ToString());
            Assert.AreEqual("Edge(vertex1,vertex2)", uv5.ToString());
            Assert.AreEqual("Edge(vertex2,vertex1)", vu.ToString());
        }
    }
}
