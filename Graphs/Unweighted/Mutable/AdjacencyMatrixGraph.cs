using System;
using System.Collections.Generic;
using System.Linq;

namespace Graphs.Unweighted.Mutable
{
    public class AdjacencyMatrixGraph : IUnweightedMutableGraph
    {
        private bool[,] adjacency;
        private Vertex[] vertices;
        private Dictionary<Vertex, int> indices = new Dictionary<Vertex, int>();
        private int _capacity;
        private int _used;
        private Stack<int> removedIndices = new Stack<int>();

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

            // First, try to reuse a previously removed vertex, if any
            if (removedIndices.Count > 0)
            {
                int index = removedIndices.Pop();
                indices[v] = index;
                vertices[index] = v;
                return;
            }

            indices[v] = _used;
            vertices[_used] = v;
            _used++;

            // Do we need to grow the adjacency matrix?
            if (_used > _capacity)
            {
                _capacity *= 2;
                bool[,] oldAdjacency = adjacency;
                adjacency = new bool[_capacity, _capacity];
                Vertex[] oldVertices = vertices;
                vertices = new Vertex[_capacity];

                for (int i = 0; i < oldVertices.Length; i++)
                {
                    vertices[i] = oldVertices[i];
                    for (int j = 0; j < oldVertices.Length; j++)
                    {
                        adjacency[i, j] = oldAdjacency[i, j];
                    }
                }
            }

        }
        public void RemoveVertex(Vertex v)
        {
            int _v = indices[v];

            for (int i = 0; i < _used; i++)
            {
                adjacency[i, _v] = false;
                adjacency[_v, i] = false;
            }

            removedIndices.Push(_v);
            indices.Remove(v);
            vertices[_v] = null;

        }
        public bool HasEdge(Vertex u, Vertex v)
        {
            return adjacency[indices[u], indices[v]];
        }
        public IEnumerable<Vertex> Neighbors(Vertex u)
        {
            int _u = indices[u];

            for (int i = 0; i < _used; i++ )
            {
                if (adjacency[_u, i])
                    yield return vertices[i];
            }

        }
        public IEnumerable<Vertex> Incidents(Vertex v)
        {
            int _v = indices[v];

            for (int i = 0; i < vertices.Length; i++)
            {
                if (adjacency[i, _v])
                    yield return vertices[i];
            }

        }
        public void SetEdge(Vertex u, Vertex v)
        {
            adjacency[indices[u], indices[v]] = true;
        }

        public void RemoveEdge(Vertex u, Vertex v)
        {
            adjacency[indices[u], indices[v]] = false;
        }
        public IEnumerable<Vertex> Vertices()
        {
            return vertices.Where((u) => { return u != null; }); ;
        }
    }
}
