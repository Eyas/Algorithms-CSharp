using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Graphs;
using Graphs.Weighted.Immutable;

namespace UnitTests.GraphsTests.Weighted.Immutable
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
        public void IWAMGraphConstructor_Empty_Succeeds()
        {
            AdjacencyMatrixGraph graph = new AdjacencyMatrixGraph(new HashSet<Vertex>(), new HashSet<Edge>());
            Assert.IsNotNull(graph, "Empty graph was not created");
        }
        [TestMethod]
        [ExpectedException(typeof(KeyNotFoundException))]
        public void IWAMGraphConstructor_MissingVertex_Fails()
        {
            HashSet<Vertex> V = new HashSet<Vertex>();
            V.Add(u);
            V.Add(v);

            HashSet<Edge> E = new HashSet<Edge>();
            E.Add(new Edge(u, v, 5));
            E.Add(new Edge(u, w, 6));

            new AdjacencyMatrixGraph(V, E);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void IWAMGraphConstructor_UnweightedEdges_Fails()
        {
            HashSet<Vertex> V = new HashSet<Vertex>();
            V.Add(u);
            V.Add(v);

            HashSet<Edge> E = new HashSet<Edge>();
            E.Add(new Edge(u, v, 5));
            E.Add(new Edge(v, u));

            new AdjacencyMatrixGraph(V, E);
        }
        [TestMethod]
        public void IWAMGraphHasEdge_RegularGraph_Test()
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
        public void IWAMGraphGetEdge_LegalEdge_ReturnsCorrectValue()
        {
            AdjacencyMatrixGraph graph = CreateGraph();

            Assert.AreEqual(5, graph.GetEdge(u, v));
            Assert.AreEqual(1, graph.GetEdge(u, w));
            Assert.AreEqual(5, graph.GetEdge(u, z));
            Assert.AreEqual(3, graph.GetEdge(v, w));
            Assert.AreEqual(2, graph.GetEdge(x, y));
            Assert.AreEqual(3, graph.GetEdge(y, x));
            Assert.AreEqual(1, graph.GetEdge(y, z));
        }
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void IWAMGraphGetEdge_IlegalEdge_ThrowsException()
        {
            AdjacencyMatrixGraph graph = CreateGraph();

            graph.GetEdge(v, u);
        }
        [TestMethod]
        public void IWAMGraphNeighbors_RegularGraph_Test()
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
        public void IWAMGraphVertices_RegularGraph_Test()
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
            //   5     3           2     1
            // u --> v --> w   x ----> y --> z
            // |     1     ^    <---- 3      ^
            //  \---------/-----------------/
            //             5
            HashSet<Vertex> V = new HashSet<Vertex>();
            V.Add(u);
            V.Add(v);
            V.Add(w);
            V.Add(x);
            V.Add(y);
            V.Add(z);

            HashSet<Edge> E = new HashSet<Edge>();
            E.Add(new Edge(u, v, 5));
            E.Add(new Edge(u, w, 1));
            E.Add(new Edge(u, z, 5));
            E.Add(new Edge(v, w, 3));
            E.Add(new Edge(x, y, 2));
            E.Add(new Edge(y, x, 3));
            E.Add(new Edge(y, z, 1));

            return new AdjacencyMatrixGraph(V, E);
        }
        #endregion
    }
}
