using System;
using System.Collections.Generic;
using System.Linq;

namespace Graphs.Weighted.Mutable
{
    public class AdjacencyListGraph : IWeightedMutableGraph
    {
        private class AdjacentVertices
        {
            private Dictionary<Vertex, int> adjacents = new Dictionary<Vertex, int>();
            public IEnumerable<Vertex> Neighbors
            {
                get
                {
                    return adjacents.Keys;
                }
            }
            public int Count
            {
                get
                {
                    return adjacents.Count;
                }
            }
            public bool HasEdge(Vertex v)
            {
                return adjacents.ContainsKey(v);
            }
            public int GetEdge(Vertex v)
            {
                int weight;
                if (adjacents.TryGetValue(v, out weight))
                {
                    return weight;
                }
                else { throw new EdgeNotFoundException(); }
            }
            public void SetEdge(Vertex v, int weight)
            {
                adjacents.Add(v, weight);
            }
            public void RemoveEdge(Vertex v)
            {
                adjacents.Remove(v);
            }
        }

        private Dictionary<Vertex, AdjacentVertices> vertices = new Dictionary<Vertex, AdjacentVertices>();

        public void AddVertex(Vertex v)
        {
            vertices[v] = new AdjacentVertices();
        }
        public void RemoveVertex(Vertex v)
        {
            vertices.Remove(v);
            foreach(AdjacentVertices adjacencyList in vertices.Values)
            {
                if (adjacencyList.HasEdge(v))
                    adjacencyList.RemoveEdge(v);
            }
        }
        public bool HasEdge(Vertex u, Vertex v)
        {
            if (!vertices.ContainsKey(u)) throw new VertexNotFoundException();
            return vertices[u].HasEdge(v);
        }
        public IEnumerable<Vertex> Neighbors(Vertex u)
        {
            if (!vertices.ContainsKey(u)) throw new VertexNotFoundException();
            return vertices[u].Neighbors;
        }
        public int Degree(Vertex u)
        {
            if (!vertices.ContainsKey(u)) throw new VertexNotFoundException();
            return vertices[u].Count;
        }
        public void SetEdge(Vertex u, Vertex v, int weight)
        {
            if (!vertices.ContainsKey(u)) throw new VertexNotFoundException();
            vertices[u].SetEdge(v, weight);
        }
        public int GetEdge(Vertex u, Vertex v)
        {
            if (!vertices.ContainsKey(u)) throw new VertexNotFoundException();
            return vertices[u].GetEdge(v);
        }
        public void RemoveEdge(Vertex u, Vertex v)
        {
            if (!vertices.ContainsKey(u)) throw new VertexNotFoundException();
            if (!vertices[u].HasEdge(v)) vertices[u].RemoveEdge(v);
        }
        public IEnumerable<Vertex> Vertices()
        {
            return vertices.Keys;
        }

    }
}
