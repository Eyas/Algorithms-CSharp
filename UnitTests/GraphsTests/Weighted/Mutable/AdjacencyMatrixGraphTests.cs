using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Graphs;
using Graphs.Weighted.Mutable;

namespace UnitTests.GraphsTests.Weighted.Mutable
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
        public void MWAMGraphConstructor_Empty_Succeeds()
        {
            AdjacencyMatrixGraph graph = new AdjacencyMatrixGraph();
            Assert.IsNotNull(graph, "Empty graph was created");
            Assert.IsTrue(Enumerable.Empty<Vertex>().SequenceEqual(graph.Vertices()), "Empty vertices list");
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void MWAMGraphConstructor_InvalidCapacity_ThrowsException()
        {
            AdjacencyMatrixGraph graph = new AdjacencyMatrixGraph(0);
        }
        [TestMethod]
        public void MWAMGraphAddVertex_SimpleAdd_Succeeds()
        {
            AdjacencyMatrixGraph graph = new AdjacencyMatrixGraph(10);
            IEnumerable<Vertex> vertices;
            Assert.AreEqual(0, graph.Vertices().Count());

            graph.AddVertex(u);
            vertices = graph.Vertices();
            Assert.AreEqual(1, vertices.Count());
            Assert.IsTrue(vertices.Contains(u));

            graph.AddVertex(v);
            vertices = graph.Vertices();
            Assert.AreEqual(2, vertices.Count());
            Assert.IsTrue(vertices.Contains(u));
            Assert.IsTrue(vertices.Contains(v));
        }
        [TestMethod]
        public void MWAMGraphAddVertex_DuplicateAdd_Ignored()
        {
            AdjacencyMatrixGraph graph = new AdjacencyMatrixGraph(10);
            IEnumerable<Vertex> vertices;
            Assert.AreEqual(0, graph.Vertices().Count());

            graph.AddVertex(u);
            vertices = graph.Vertices();
            Assert.AreEqual(1, vertices.Count());
            Assert.IsTrue(vertices.Contains(u));

            graph.AddVertex(v);
            vertices = graph.Vertices();
            Assert.AreEqual(2, vertices.Count());
            Assert.IsTrue(vertices.Contains(u));
            Assert.IsTrue(vertices.Contains(v));

            graph.AddVertex(v);
            vertices = graph.Vertices();
            Assert.AreEqual(2, vertices.Count());
            Assert.IsTrue(vertices.Contains(u));
            Assert.IsTrue(vertices.Contains(v));

            graph.AddVertex(u);
            vertices = graph.Vertices();
            Assert.AreEqual(2, vertices.Count());
            Assert.IsTrue(vertices.Contains(u));
            Assert.IsTrue(vertices.Contains(v));
        }
        [TestMethod]
        public void MWAMGraphAddVertex_Growth_VerticesAndEdgesIntact()
        {
            AdjacencyMatrixGraph graph = new AdjacencyMatrixGraph(3);
            IEnumerable<Vertex> vertices;
            Assert.AreEqual(0, graph.Vertices().Count());

            graph.AddVertex(s);
            graph.AddVertex(t);
            graph.AddVertex(u);
            graph.SetEdge(s, t, 1);
            graph.SetEdge(t, u, 3);
            graph.SetEdge(u, s, 4);
            graph.SetEdge(t, t, 2);
            //   S T U
            // S 0 1 0
            // T 0 2 3
            // U 4 0 0

            graph.AddVertex(v);

            vertices = graph.Vertices();
            Assert.AreEqual(4, graph.Vertices().Count());
            Assert.IsTrue(vertices.Contains(s));
            Assert.IsTrue(vertices.Contains(t));
            Assert.IsTrue(vertices.Contains(u));
            Assert.IsTrue(vertices.Contains(v));

            Assert.IsTrue(graph.HasEdge(s, t));
            Assert.IsTrue(graph.HasEdge(t, u));
            Assert.IsTrue(graph.HasEdge(u, s));
            Assert.IsTrue(graph.HasEdge(t, t));

            Assert.AreEqual(1, graph.GetEdge(s, t));
            Assert.AreEqual(3, graph.GetEdge(t, u));
            Assert.AreEqual(4, graph.GetEdge(u, s));
            Assert.AreEqual(2, graph.GetEdge(t, t));

            Assert.IsFalse(graph.HasEdge(s, s));
            Assert.IsFalse(graph.HasEdge(s, u));
            Assert.IsFalse(graph.HasEdge(t, s));
            Assert.IsFalse(graph.HasEdge(u, t));
            Assert.IsFalse(graph.HasEdge(u, u));
        }
        [TestMethod]
        public void MWAMGraphRemoveVertex_SimpleRemove_Succeeds()
        {
            // u --> v --> w   x <---> y --> z
            // |           ^                 ^
            //  \---------/-----------------/
            //  will remove 'v'

            AdjacencyMatrixGraph graph = CreateGraph();

            graph.RemoveVertex(v);
            Assert.IsFalse(graph.Vertices().Contains(v));
            Assert.IsFalse(graph.Neighbors(u).Contains(v));
        }
        [TestMethod]
        public void MWAMGraphRemoveVertex_RemoveThenAddAnother_IsClean()
        {
            // An 'Add' after a 'Remove' reuses the old index, make
            // sure there is no leftover cruft.

            // u --> v --> w   x <---> y --> z
            // |           ^                 ^
            //  \---------/-----------------/
            //  will remove 'v'
            //  and add 't'

            AdjacencyMatrixGraph graph = CreateGraph();

            graph.RemoveVertex(v);
            graph.AddVertex(t);

            Assert.IsFalse(graph.Vertices().Contains(v));
            Assert.IsFalse(graph.Neighbors(u).Contains(t));
            Assert.AreEqual(0, graph.Neighbors(t).Count());
        }
        [TestMethod]
        public void MWAMGraphSetEdge_UnconnectedGraph_Succeeds()
        {
            AdjacencyMatrixGraph graph = new AdjacencyMatrixGraph();
            graph.AddVertex(u);
            graph.AddVertex(v);

            graph.SetEdge(u, v, 3);

            Assert.IsTrue(graph.HasEdge(u, v));
            Assert.AreEqual(3, graph.GetEdge(u, v));
            Assert.IsFalse(graph.HasEdge(v, u));
        }
        [TestMethod]
        [ExpectedException(typeof(KeyNotFoundException))]
        public void MWAMGraphSetEdge_MissingVertex_Fails()
        {
            AdjacencyMatrixGraph graph = new AdjacencyMatrixGraph();
            graph.AddVertex(u);
            graph.AddVertex(v);

            graph.SetEdge(u, t, 3);
        }
        [TestMethod]
        public void MWAMGraphRemoveEdge_SetAndRemove_Succeeds()
        {
            AdjacencyMatrixGraph graph = new AdjacencyMatrixGraph();
            graph.AddVertex(u);
            graph.AddVertex(v);

            graph.SetEdge(u, v, 3);
            graph.RemoveEdge(u, v);

            Assert.IsFalse(graph.HasEdge(u, v));
            Assert.IsFalse(graph.HasEdge(v, u));
        }
        [TestMethod]
        public void MWAMGraphHasEdge_RegularGraph_Test()
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
        public void MWAMGraphNeighbors_RegularGraph_Test()
        {
            AdjacencyMatrixGraph graph = CreateGraph();
            IEnumerable<Vertex> uNeighbors = graph.Neighbors(u);
            IEnumerable<Vertex> xNeighbors = graph.Neighbors(x);
            IEnumerable<Vertex> zNeighbors = graph.Neighbors(z);

            Assert.IsTrue(uNeighbors.Contains(v));
            Assert.IsTrue(uNeighbors.Contains(w));
            Assert.IsTrue(uNeighbors.Contains(z));
            Assert.IsFalse(uNeighbors.Contains(y));

            Assert.IsTrue(xNeighbors.Contains(y));
            Assert.IsFalse(xNeighbors.Contains(u));

            Assert.AreEqual(0, zNeighbors.Count());
        }
        [TestMethod]
        public void MWAMGraphVertices_RegularGraph_Test()
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
            //   1    4            5     7
            // u --> v --> w   x <---> y --> z
            // |           ^       5         ^
            //  \---------/-----------------/
            //        2           3
            HashSet<Vertex> V = new HashSet<Vertex>();
            AdjacencyMatrixGraph graph = new AdjacencyMatrixGraph(10);
            graph.AddVertex(u);
            graph.AddVertex(v);
            graph.AddVertex(w);
            graph.AddVertex(x);
            graph.AddVertex(y);
            graph.AddVertex(z);

            graph.SetEdge(u, v, 1);
            graph.SetEdge(u, w, 2);
            graph.SetEdge(u, z, 3);
            graph.SetEdge(v, w, 4);
            graph.SetEdge(x, y, 5);
            graph.SetEdge(y, x, 5);
            graph.SetEdge(y, z, 7);

            return graph;
        }
        #endregion
    }

}
