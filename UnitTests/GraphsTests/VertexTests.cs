using System;
using Xunit;
using Graphs;

namespace UnitTests.GraphsTests
{

    public class VertexTests
    {
        [Fact]
        public void VertexConstructorTest()
        {
            Vertex v = new Vertex("vertex1");
            Assert.Equal("vertex1", v.Name); // "The created vertex does not have the specified name."
        }
        [Fact]
        public void VertexEqualsTest()
        {
            Vertex v1 = new Vertex("vertex1");
            Vertex v2 = new Vertex("vertex2");
            Assert.True(v1.Equals(new Vertex("vertex1")), "v1 should be equal to new vertex with same name.");
            Assert.True(v2.Equals(new Vertex("vertex2")), "v2 should be equal to new vertex with same name.");
            Assert.False(v1.Equals(v2), "v1 and v2 should not be equal since they have different names.");
            Assert.False(v1.Equals("string"), "Vertex should not equal object of different type");
        }
        [Fact]
        public void VertexHashCodeTest()
        {
            Vertex v1 = new Vertex("vertex1");
            Vertex v2 = new Vertex("vertex2");
            Assert.Equal((new Vertex("vertex1")).GetHashCode(), v1.GetHashCode()); // "Objects of same name have different hashCode (vertex1).");
            Assert.Equal((new Vertex("vertex2")).GetHashCode(), v2.GetHashCode()); // "Objects of same name have different hashCode (vertex2)."
        }
        [Fact]
        public void VertexToStringTest()
        {
            Vertex v1 = new Vertex("vertex1");
            Vertex v2 = new Vertex("vertex2");

            Assert.Equal("Vertex[vertex1]", v1.ToString()); // "ToString Method should generate Name (vertex1)."
            Assert.Equal("Vertex[vertex2]", v2.ToString()); // "ToString Method should generate Name (vertex2)."
        }
    }
}
