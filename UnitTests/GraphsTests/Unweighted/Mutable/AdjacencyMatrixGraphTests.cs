﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Graphs;
using Graphs.Unweighted.Mutable;

namespace UnitTests.GraphsTests.Unweighted.Mutable
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
        public void MUAMGraphConstructor_Empty_Succeeds()
        {
            AdjacencyMatrixGraph graph = new AdjacencyMatrixGraph();
            Assert.IsNotNull(graph, "Empty graph was not created");
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void MUAMGraphConstructor_InvalidCapacity_ThrowsException()
        {
            AdjacencyMatrixGraph graph = new AdjacencyMatrixGraph(0);
        }
        [TestMethod]
        public void MUAMGraphAddVertex_SimpleAdd_Succeeds()
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
        public void MUAMGraphAddVertex_DuplicateAdd_Ignored()
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
        public void MUAMGraphAddVertex_Growth_VerticesAndEdgesIntact()
        {
            AdjacencyMatrixGraph graph = new AdjacencyMatrixGraph(3);
            IEnumerable<Vertex> vertices;
            Assert.AreEqual(0, graph.Vertices().Count());

            graph.AddVertex(s);
            graph.AddVertex(t);
            graph.AddVertex(u);
            graph.SetEdge(s, t);
            graph.SetEdge(t, u);
            graph.SetEdge(u, s);
            graph.SetEdge(t, t);
            //   S T U
            // S 0 1 0
            // T 0 1 1
            // U 1 0 0

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

            Assert.IsFalse(graph.HasEdge(s, s));
            Assert.IsFalse(graph.HasEdge(s, u));
            Assert.IsFalse(graph.HasEdge(t, s));
            Assert.IsFalse(graph.HasEdge(u, t));
            Assert.IsFalse(graph.HasEdge(u, u));
        }
        [TestMethod]
        public void MUAMGraphRemoveVertex_SimpleRemove_Succeeds()
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
        public void MUAMGraphRemoveVertex_RemoveThenAddAnother_IsClean()
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
            Assert.AreEqual(0, graph.Neighbors(t).Count);
        }
        [TestMethod]
        public void MUAMGraphSetEdge_UnconnectedGraph_Succeeds()
        {
            AdjacencyMatrixGraph graph = new AdjacencyMatrixGraph();
            graph.AddVertex(u);
            graph.AddVertex(v);

            graph.SetEdge(u, v);

            Assert.IsTrue(graph.HasEdge(u, v));
            Assert.IsFalse(graph.HasEdge(v, u));
        }
        [TestMethod]
        [ExpectedException(typeof(KeyNotFoundException))]
        public void MUAMGraphSetEdge_MissingVertex_Fails()
        {
            AdjacencyMatrixGraph graph = new AdjacencyMatrixGraph();
            graph.AddVertex(u);
            graph.AddVertex(v);

            graph.SetEdge(u, t);
        }
        [TestMethod]
        public void MUAMGraphRemoveEdge_SetAndRemove_Succeeds()
        {
            AdjacencyMatrixGraph graph = new AdjacencyMatrixGraph();
            graph.AddVertex(u);
            graph.AddVertex(v);

            graph.SetEdge(u, v);
            graph.RemoveEdge(u, v);

            Assert.IsFalse(graph.HasEdge(u, v));
            Assert.IsFalse(graph.HasEdge(v, u));
        }
        [TestMethod]
        public void MUAMGraphHasEdge_RegularGraph_Test()
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
        public void MUAMGraphNeighbors_RegularGraph_Test()
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
        public void MUAMGraphVertices_RegularGraph_Test()
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
            AdjacencyMatrixGraph graph = new AdjacencyMatrixGraph(10);
            graph.AddVertex(u);
            graph.AddVertex(v);
            graph.AddVertex(w);
            graph.AddVertex(x);
            graph.AddVertex(y);
            graph.AddVertex(z);

            graph.SetEdge(u, v);
            graph.SetEdge(u, w);
            graph.SetEdge(u, z);
            graph.SetEdge(v, w);
            graph.SetEdge(x, y);
            graph.SetEdge(y, x);
            graph.SetEdge(y, z);

            return graph;
        }
        #endregion
    }

}