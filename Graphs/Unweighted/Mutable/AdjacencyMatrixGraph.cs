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

            _used++;

            // First, try to reuse a previously removed vertex, if any
            if (removedIndices.Count > 0)
            {
                int index = removedIndices.Pop();
                indices[v] = index;
                vertices[index] = v;

            }
            else
            {
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

                indices[v] = _used - 1;
                vertices[_used - 1] = v;
            }

        }
        public void RemoveVertex(Vertex v)
        {
            int toRemove = indices[v];
            removedIndices.Push(toRemove);
            indices.Remove(v);
            vertices[toRemove] = null;
            _used--;
            for (int i = 0; i < _capacity; i++)
            {
                adjacency[i, toRemove] = false;
                adjacency[toRemove, i] = false;
            }
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
