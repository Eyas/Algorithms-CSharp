using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Graphs;
using Graphs.Unweighted.Immutable;

namespace UnitTests.GraphsTests.Unweighted.Immutable
{
    [TestClass]
    public class AdjacencyMatrixGraphTests
    {
        #region Pre-defined vertices (s, t, u, v, w, x, y, z)
        private static Vertex s = new Vertex("s");
        private static Vertex t = new Vertex("t");
        private static Vertex u = new Vertex("u");
        private static Vertex v = new Vertex("v");
        private static Vertex w = new Vertex("w");
        private static Vertex x = new Vertex("x");
        private static Vertex y = new Vertex("y");
        private static Vertex z = new Vertex("z");
        #endregion

        [TestMethod]
        public void IUAMGraphEmptyConstructorTest()
        {
            AdjacencyMatrixGraph graph = new AdjacencyMatrixGraph(new HashSet<Vertex>(), new HashSet<Edge>());
            Assert.IsNotNull(graph, "Empty graph was not created");
        }
        [TestMethod]
        [ExpectedException(typeof(KeyNotFoundException))]
        public void IUAMGraphIllegalConstructorTest()
        {
            HashSet<Vertex> V = new HashSet<Vertex>();
            V.Add(u);
            V.Add(v);

            HashSet<Edge> E = new HashSet<Edge>();
            E.Add(new Edge(u, v));
            E.Add(new Edge(u, w));

            AdjacencyMatrixGraph graph = new AdjacencyMatrixGraph(V, E);
        }
        [TestMethod]
        public void IUAMGraphHasEdgeTest()
        {
            AdjacencyMatrixGraph graph = CreateGraph();

            Assert.IsTrue(graph.HasEdge(u, v));
            Assert.IsTrue(graph.HasEdge(u, w));
            Assert.IsTrue(graph.HasEdge(u, z));
            Assert.IsTrue(graph.HasEdge(v, w));
            Assert.IsTrue(graph.HasEdge(x, y));
            Assert.IsTrue(graph.HasEdge(y, x));
            Assert.IsTrue(graph.HasEdge(y, z));

            Assert.IsFalse(graph.HasEdge(v, u));
            Assert.IsFalse(graph.HasEdge(u, x));
        }
        [TestMethod]
        public void IUAMGraphNeighborsTest()
        {
            AdjacencyMatrixGraph graph = CreateGraph();
            List<Vertex> uNeighbors = graph.Neighbors(u);
            List<Vertex> xNeighbors = graph.Neighbors(x);
            List<Vertex> zNeighbors = graph.Neighbors(z);

            Assert.IsTrue(uNeighbors.Contains(v));
            Assert.IsTrue(uNeighbors.Contains(w));
            Assert.IsTrue(uNeighbors.Contains(z));
            Assert.IsFalse(uNeighbors.Contains(y));

            Assert.IsTrue(xNeighbors.Contains(y));
            Assert.IsFalse(xNeighbors.Contains(u));

            Assert.AreEqual(0, zNeighbors.Count);
        }
        [TestMethod]
        public void IUAMGraphVerticesTest()
        {
            AdjacencyMatrixGraph graph = CreateGraph();
            HashSet<Vertex> vertices = new HashSet<Vertex>(graph.Vertices());

            Assert.IsTrue(vertices.Contains(u));
            Assert.IsTrue(vertices.Contains(v));
            Assert.IsTrue(vertices.Contains(w));
            Assert.IsTrue(vertices.Contains(x));
            Assert.IsTrue(vertices.Contains(y));
            Assert.IsTrue(vertices.Contains(z));

            Assert.IsFalse(vertices.Contains(s));
            Assert.IsFalse(vertices.Contains(t));

            Assert.AreEqual(6, vertices.Count);
        }
        #region Helper Methods
        public AdjacencyMatrixGraph CreateGraph()
        {
            // u --> v --> w   x <---> y --> z
            // |           ^                 ^
            //  \---------/-----------------/
            HashSet<Vertex> V = new HashSet<Vertex>();
            V.Add(u);
            V.Add(v);
            V.Add(w);
            V.Add(x);
            V.Add(y);
            V.Add(z);

            HashSet<Edge> E = new HashSet<Edge>();
            E.Add(new Edge(u, v));
            E.Add(new Edge(u, w));
            E.Add(new Edge(u, z));
            E.Add(new Edge(v, w));
            E.Add(new Edge(x, y));
            E.Add(new Edge(y, x));
            E.Add(new Edge(y, z));

            return new AdjacencyMatrixGraph(V, E);
        }
        #endregion
    }
}
