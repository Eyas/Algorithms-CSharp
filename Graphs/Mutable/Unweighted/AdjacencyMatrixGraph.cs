using System;
using System.Collections.Generic;
using System.Linq;

namespace Graphs.Mutable.Unweighted
{
    public class AdjacencyMatrixGraph : IUnweightedGraph
    {
        private bool[,] adjacency;
        private Vertex[] vertices;
        private Dictionary<Vertex, int> indices = new Dictionary<Vertex, int>();
        private int _capacity;
        private int _used;

        public AdjacencyMatrixGraph(int capacity = 10)
        {
            if (capacity == 0) throw new ArgumentException();

            adjacency = new bool[capacity, capacity];
            vertices = new Vertex[capacity];
            _capacity = capacity;
            _used = 0;
        }
        public void AddVertex(Vertex v)
        {
            if (indices.ContainsKey(v)) return;

            indices[v] = _used;
            vertices[_used] = v;
            _used++;

            if (_used > _capacity)
            {
                _capacity *= 2;
                bool[,] oldAdjacency = adjacency;
                adjacency = new bool[_capacity, _capacity];
                Vertex[] oldVertices = vertices;
                vertices = new Vertex[_capacity];

                for (int i = 0; i < oldAdjacency.Length; i++)
                {
                    vertices[i] = oldVertices[i];
                    for (int j = 0; j < oldAdjacency.Length; j++)
                    {
                        adjacency[i, j] = oldAdjacency[i, j];
                    }
                }
            }
        }
        public void RemoveVertex(Vertex v)
        {
            // TODO
        }
        public bool HasEdge(Vertex u, Vertex v)
        {
            return adjacency[indices[u], indices[v]];
        }
        public List<Vertex> Neighbors(Vertex u)
        {
            List<Vertex> neighbors = new List<Vertex>();
            int _u = indices[u];

            for (int i = 0; i < _used; i++ )
            {
                if (adjacency[_u, i])
                    neighbors.Add(vertices[i]);
            }
            return neighbors;
        }
        public void SetEdge(Vertex u, Vertex v)
        {
            adjacency[indices[u], indices[v]] = true;
        }

        public void RemoveEdge(Vertex u, Vertex v)
        {
            adjacency[indices[u], indices[v]] = false;
        }

    }
}
