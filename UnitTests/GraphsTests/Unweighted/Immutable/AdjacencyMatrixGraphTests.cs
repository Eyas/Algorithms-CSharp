using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Graphs;
using Graphs.Unweighted.Immutable;

namespace UnitTests.GraphsTests.Unweighted.Immutable
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
        public void IUAMGraphConstructor_Empty_Succeeds()
        {
            AdjacencyMatrixGraph graph = new AdjacencyMatrixGraph(new HashSet<Vertex>(), new HashSet<Edge>());
            Assert.NotNull(graph); // Empty graph was not created
        }
        [Fact]
        public void IUAMGraphConstructor_MissingVertex_Fails()
        {
            HashSet<Vertex> V = new HashSet<Vertex>
            {
                u,
                v
            };

            HashSet<Edge> E = new HashSet<Edge>
            {
                new Edge(u, v),
                new Edge(u, w)
            };

            Assert.Throws(typeof(KeyNotFoundException), () => new AdjacencyMatrixGraph(V, E));
        }
        [Fact]
        public void IUAMGraphHasEdge_RegularGraph_Test()
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
        public void IUAMGraphNeighbors_RegularGraph_Test()
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
        public void IUAMGraphVertices_RegularGraph_Test()
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
            // u --> v --> w   x <---> y --> z
            // |           ^                 ^
            //  \---------/-----------------/
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
                new Edge(u, v),
                new Edge(u, w),
                new Edge(u, z),
                new Edge(v, w),
                new Edge(x, y),
                new Edge(y, x),
                new Edge(y, z)
            };

            return new AdjacencyMatrixGraph(V, E);
        }
        #endregion
    }
}
