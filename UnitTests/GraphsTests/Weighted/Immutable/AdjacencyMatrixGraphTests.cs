using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Graphs;
using Graphs.Weighted.Immutable;

namespace UnitTests.GraphsTests.Weighted.Immutable
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
        public void IWAMGraphConstructor_Empty_Succeeds()
        {
            AdjacencyMatrixGraph graph = new AdjacencyMatrixGraph(new HashSet<Vertex>(), new HashSet<Edge>());
            Assert.NotNull(graph); // "Empty graph was not created"
        }
        [Fact]
        public void IWAMGraphConstructor_MissingVertex_Fails()
        {
            HashSet<Vertex> V = new HashSet<Vertex>
            {
                u,
                v
            };

            HashSet<Edge> E = new HashSet<Edge>
            {
                new Edge(u, v, 5),
                new Edge(u, w, 6)
            };

            Assert.Throws(typeof(KeyNotFoundException), () =>
            new AdjacencyMatrixGraph(V, E));
        }
        [Fact]
        public void IWAMGraphConstructor_UnweightedEdges_Fails()
        {
            HashSet<Vertex> V = new HashSet<Vertex>
            {
                u,
                v
            };

            HashSet<Edge> E = new HashSet<Edge>
            {
                new Edge(u, v, 5),
                new Edge(v, u)
            };

            Assert.Throws(typeof(ArgumentException),
            () => new AdjacencyMatrixGraph(V, E));
        }
        [Fact]
        public void IWAMGraphHasEdge_RegularGraph_Test()
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
        public void IWAMGraphGetEdge_LegalEdge_ReturnsCorrectValue()
        {
            AdjacencyMatrixGraph graph = CreateGraph();

            Assert.Equal(5, graph.GetEdge(u, v));
            Assert.Equal(1, graph.GetEdge(u, w));
            Assert.Equal(5, graph.GetEdge(u, z));
            Assert.Equal(3, graph.GetEdge(v, w));
            Assert.Equal(2, graph.GetEdge(x, y));
            Assert.Equal(3, graph.GetEdge(y, x));
            Assert.Equal(1, graph.GetEdge(y, z));
        }

        [Fact]
        public void IWAMGraphGetEdge_IlegalEdge_ThrowsException()
        {
            AdjacencyMatrixGraph graph = CreateGraph();

            Assert.Throws(typeof(InvalidOperationException), () =>
            graph.GetEdge(v, u));
        }
        [Fact]
        public void IWAMGraphNeighbors_RegularGraph_Test()
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
        public void IWAMGraphVertices_RegularGraph_Test()
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
            //   5     3           2     1
            // u --> v --> w   x ----> y --> z
            // |     1     ^    <---- 3      ^
            //  \---------/-----------------/
            //             5
            HashSet<Vertex> V = new HashSet<Vertex>
            {
                u,
                v,
                w,
                x,
                y,
                z
            };

            HashSet<Edge> E = new HashSet<Edge>
            {
                new Edge(u, v, 5),
                new Edge(u, w, 1),
                new Edge(u, z, 5),
                new Edge(v, w, 3),
                new Edge(x, y, 2),
                new Edge(y, x, 3),
                new Edge(y, z, 1)
            };

            return new AdjacencyMatrixGraph(V, E);
        }
        #endregion
    }
}
