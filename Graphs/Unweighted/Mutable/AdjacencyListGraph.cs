using System;
using System.Collections.Generic;
using System.Linq;

namespace Graphs.Unweighted.Mutable
{
    public class AdjacencyListGraph : IUnweightedMutableGraph
    {
        private class AdjacentVertices
        {
            private HashSet<Vertex> adjacents = new HashSet<Vertex>();
            public List<Vertex> Neighbors
            {
                get
                {
                    return adjacents.ToList<Vertex>();
                }
            }
            public bool HasEdge(Vertex v)
            {
                return adjacents.Contains(v);
            }
            public void SetEdge(Vertex v)
            {
                adjacents.Add(v);
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
            foreach (AdjacentVertices adjacencyList in vertices.Values)
            {
                if (adjacencyList.HasEdge(v))
                    adjacencyList.RemoveEdge(v);
            }
        }
        public bool HasEdge(Vertex u, Vertex v)
        {
            if (!vertices.ContainsKey(u)) throw new VertexNotFoundException();
            if (!vertices.ContainsKey(v)) throw new VertexNotFoundException();
            return vertices[u].HasEdge(v);
        }
        public List<Vertex> Neighbors(Vertex u)
        {
            if (!vertices.ContainsKey(u)) throw new VertexNotFoundException();
            return vertices[u].Neighbors;
        }
        public void SetEdge(Vertex u, Vertex v)
        {
            if (!vertices.ContainsKey(u)) throw new VertexNotFoundException();
            if (!vertices.ContainsKey(v)) throw new VertexNotFoundException();
            vertices[u].SetEdge(v);
        }
        public void RemoveEdge(Vertex u, Vertex v)
        {
            if (!vertices.ContainsKey(u)) throw new VertexNotFoundException();
            if (!vertices.ContainsKey(v)) throw new VertexNotFoundException();
            if (!vertices[u].HasEdge(v)) vertices[u].RemoveEdge(v);
        }

    }
}
