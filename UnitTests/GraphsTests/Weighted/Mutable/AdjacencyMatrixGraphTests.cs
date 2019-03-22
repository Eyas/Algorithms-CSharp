using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Graphs;
using Graphs.Weighted.Mutable;

namespace UnitTests.GraphsTests.Weighted.Mutable
{
    
    public class AdjacencyMatrixGraphTests
    {
        #region Pre-defined vertices (s, t, u, v, w, x, y, z)
        private static readonly Vertex s = new Vertex("s");
        private static readonly Vertex t = new Vertex("t");
        private static readonly Vertex u = new Vertex("u");
        private static readonly Vertex v = new Vertex("v");
        private static readonly Vertex w = new Vertex("w");
        private static readonly Vertex x = new Vertex("x");
        private static readonly Vertex y = new Vertex("y");
        private static readonly Vertex z = new Vertex("z");
        #endregion

        [Fact]
        public void MWAMGraphConstructor_Empty_Succeeds()
        {
            AdjacencyMatrixGraph graph = new AdjacencyMatrixGraph();
            Assert.True(Enumerable.Empty<Vertex>().SequenceEqual(graph.Vertices()), "Empty vertices list");
        }
        [Fact]
        public void MWAMGraphConstructor_InvalidCapacity_ThrowsException()
        {
            Assert.Throws(typeof(ArgumentException), () => new AdjacencyMatrixGraph(0));
        }
        [Fact]
        public void MWAMGraphAddVertex_SimpleAdd_Succeeds()
        {
            AdjacencyMatrixGraph graph = new AdjacencyMatrixGraph(10);
            IEnumerable<Vertex> vertices;
            Assert.Equal(0, graph.Vertices().Count());

            graph.AddVertex(u);
            vertices = graph.Vertices();
            Assert.Equal(1, vertices.Count());
            Assert.True(vertices.Contains(u));

            graph.AddVertex(v);
            vertices = graph.Vertices();
            Assert.Equal(2, vertices.Count());
            Assert.True(vertices.Contains(u));
            Assert.True(vertices.Contains(v));
        }
        [Fact]
        public void MWAMGraphAddVertex_DuplicateAdd_Ignored()
        {
            AdjacencyMatrixGraph graph = new AdjacencyMatrixGraph(10);
            IEnumerable<Vertex> vertices;
            Assert.Equal(0, graph.Vertices().Count());

            graph.AddVertex(u);
            vertices = graph.Vertices();
            Assert.Equal(1, vertices.Count());
            Assert.True(vertices.Contains(u));

            graph.AddVertex(v);
            vertices = graph.Vertices();
            Assert.Equal(2, vertices.Count());
            Assert.True(vertices.Contains(u));
            Assert.True(vertices.Contains(v));

            graph.AddVertex(v);
            vertices = graph.Vertices();
            Assert.Equal(2, vertices.Count());
            Assert.True(vertices.Contains(u));
            Assert.True(vertices.Contains(v));

            graph.AddVertex(u);
            vertices = graph.Vertices();
            Assert.Equal(2, vertices.Count());
            Assert.True(vertices.Contains(u));
            Assert.True(vertices.Contains(v));
        }
        [Fact]
        public void MWAMGraphAddVertex_Growth_VerticesAndEdgesIntact()
        {
            AdjacencyMatrixGraph graph = new AdjacencyMatrixGraph(3);
            IEnumerable<Vertex> vertices;
            Assert.Equal(0, graph.Vertices().Count());

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
            Assert.Equal(4, graph.Vertices().Count());
            Assert.True(vertices.Contains(s));
            Assert.True(vertices.Contains(t));
            Assert.True(vertices.Contains(u));
            Assert.True(vertices.Contains(v));

            Assert.True(graph.HasEdge(s, t));
            Assert.True(graph.HasEdge(t, u));
            Assert.True(graph.HasEdge(u, s));
            Assert.True(graph.HasEdge(t, t));

            Assert.Equal(1, graph.GetEdge(s, t));
            Assert.Equal(3, graph.GetEdge(t, u));
            Assert.Equal(4, graph.GetEdge(u, s));
            Assert.Equal(2, graph.GetEdge(t, t));

            Assert.False(graph.HasEdge(s, s));
            Assert.False(graph.HasEdge(s, u));
            Assert.False(graph.HasEdge(t, s));
            Assert.False(graph.HasEdge(u, t));
            Assert.False(graph.HasEdge(u, u));
        }
        [Fact]
        public void MWAMGraphRemoveVertex_SimpleRemove_Succeeds()
        {
            // u --> v --> w   x <---> y --> z
            // |           ^                 ^
            //  \---------/-----------------/
            //  will remove 'v'

            AdjacencyMatrixGraph graph = CreateGraph();

            graph.RemoveVertex(v);
            Assert.False(graph.Vertices().Contains(v));
            Assert.False(graph.Neighbors(u).Contains(v));
        }
        [Fact]
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

            Assert.False(graph.Vertices().Contains(v));
            Assert.False(graph.Neighbors(u).Contains(t));
            Assert.Equal(0, graph.Neighbors(t).Count());
        }
        [Fact]
        public void MWAMGraphSetEdge_UnconnectedGraph_Succeeds()
        {
            AdjacencyMatrixGraph graph = new AdjacencyMatrixGraph();
            graph.AddVertex(u);
            graph.AddVertex(v);

            graph.SetEdge(u, v, 3);

            Assert.True(graph.HasEdge(u, v));
            Assert.Equal(3, graph.GetEdge(u, v));
            Assert.False(graph.HasEdge(v, u));
        }
        [Fact]
        public void MWAMGraphSetEdge_MissingVertex_Fails()
        {
            AdjacencyMatrixGraph graph = new AdjacencyMatrixGraph();
            graph.AddVertex(u);
            graph.AddVertex(v);

            Assert.Throws(typeof(KeyNotFoundException), () =>
            graph.SetEdge(u, t, 3));
        }
        [Fact]
        public void MWAMGraphRemoveEdge_SetAndRemove_Succeeds()
        {
            AdjacencyMatrixGraph graph = new AdjacencyMatrixGraph();
            graph.AddVertex(u);
            graph.AddVertex(v);

            graph.SetEdge(u, v, 3);
            graph.RemoveEdge(u, v);

            Assert.False(graph.HasEdge(u, v));
            Assert.False(graph.HasEdge(v, u));
        }
        [Fact]
        public void MWAMGraphHasEdge_RegularGraph_Test()
        {
            AdjacencyMatrixGraph graph = CreateGraph();

            Assert.True(graph.HasEdge(u, v));
            Assert.True(graph.HasEdge(u, w));
            Assert.True(graph.HasEdge(u, z));
            Assert.True(graph.HasEdge(v, w));
            Assert.True(graph.HasEdge(x, y));
            Assert.True(graph.HasEdge(y, x));
            Assert.True(graph.HasEdge(y, z));

            Assert.False(graph.HasEdge(v, u));
            Assert.False(graph.HasEdge(u, x));
        }
        [Fact]
        public void MWAMGraphNeighbors_RegularGraph_Test()
        {
            AdjacencyMatrixGraph graph = CreateGraph();
            IEnumerable<Vertex> uNeighbors = graph.Neighbors(u);
            IEnumerable<Vertex> xNeighbors = graph.Neighbors(x);
            IEnumerable<Vertex> zNeighbors = graph.Neighbors(z);

            Assert.True(uNeighbors.Contains(v));
            Assert.True(uNeighbors.Contains(w));
            Assert.True(uNeighbors.Contains(z));
            Assert.False(uNeighbors.Contains(y));

            Assert.True(xNeighbors.Contains(y));
            Assert.False(xNeighbors.Contains(u));

            Assert.Equal(0, zNeighbors.Count());
        }
        [Fact]
        public void MWAMGraphVertices_RegularGraph_Test()
        {
            AdjacencyMatrixGraph graph = CreateGraph();
            HashSet<Vertex> vertices = new HashSet<Vertex>(graph.Vertices());

            Assert.True(vertices.Contains(u));
            Assert.True(vertices.Contains(v));
            Assert.True(vertices.Contains(w));
            Assert.True(vertices.Contains(x));
            Assert.True(vertices.Contains(y));
            Assert.True(vertices.Contains(z));

            Assert.False(vertices.Contains(s));
            Assert.False(vertices.Contains(t));

            Assert.Equal(6, vertices.Count);
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
