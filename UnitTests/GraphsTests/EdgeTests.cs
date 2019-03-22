using System;
using Xunit;
using Graphs;
namespace UnitTests.GraphsTests
{
    public class EdgeTests
    {
        [Fact]
        public void EdgeConstructorTest()
        {
            Vertex u = new Vertex("vertex1");
            Vertex v = new Vertex("vertex2");
            const int w = 5;

            Edge e1 = new Edge(u, v);
            Edge e2 = new Edge(u, v, w);

            Assert.Equal(u, e1.u);
            Assert.Equal(v, e1.v);
            Assert.False(e1.weight.HasValue);

            Assert.Equal(u, e2.u);
            Assert.Equal(v, e2.v);
            Assert.True(e2.weight.HasValue);
            Assert.Equal(w, e2.weight);
        }
        [Fact]
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

            Assert.Equal(new Edge(u, v), uv);
            Assert.Equal(new Edge(u, v, five), uv);
            Assert.Equal(new Edge(u, v, five), uv5);
            Assert.Equal(new Edge(u, v, six), uv5);

            Assert.Equal(new Edge(new Vertex("vertex1"), new Vertex("vertex2")), uv);

            Assert.NotEqual(uv, vw);
            Assert.NotEqual(uv, vu);
            Assert.NotEqual(vw, vu);
            Assert.NotEqual(uv5, vw);
            Assert.NotEqual(uv5, vu);
        }
        [Fact]
        public void EdgeHashCodeTest()
        {
            Vertex u = new Vertex("vertex1");
            Vertex v = new Vertex("vertex2");

            Edge uv = new Edge(u, v);
            Edge vu = new Edge(v, u);

            Assert.Equal((new Edge(u, v)).GetHashCode(), uv.GetHashCode());
            Assert.Equal((new Edge(v, u)).GetHashCode(), vu.GetHashCode());
        }
        [Fact]
        public void EdgeToStringTest()
        {
            Vertex u = new Vertex("vertex1");
            Vertex v = new Vertex("vertex2");
            const int five = 5;

            Edge uv = new Edge(u, v);
            Edge vu = new Edge(v, u);
            Edge uv5 = new Edge(u, v, five);

            Assert.Equal("Edge(vertex1,vertex2)", uv.ToString());
            Assert.Equal("Edge(vertex1,vertex2)", uv5.ToString());
            Assert.Equal("Edge(vertex2,vertex1)", vu.ToString());
        }
    }
}
