using System;
using System.Collections.Generic;
using System.Linq;

namespace Graphs.Weighted.Mutable
{
    public class AdjacencyMatrixGraph : IWeightedMutableGraph, IIncidentsGraph
    {
        private int?[,] adjacency;
        private Vertex[] vertices;
        private Dictionary<Vertex, int> indices = new Dictionary<Vertex, int>();
        private Stack<int> removed = new Stack<int>();
        private int _capacity;
        private int _used;

        public AdjacencyMatrixGraph(int capacity = 10)
        {
            if (capacity == 0) throw new ArgumentException();

            adjacency = new int?[capacity, capacity];
            vertices = new Vertex[capacity];
            _capacity = capacity;
            _used = 0;
        }
        public void AddVertex(Vertex v)
        {
            if (indices.ContainsKey(v)) return;

            if (removed.Count > 0)
            {
                int idx = removed.Pop();
                indices[v] = idx;
                vertices[idx] = v;

                return;
            }

            indices[v] = _used;
            vertices[_used] = v;
            _used++;

            if (_used > _capacity)
            {
                _capacity *= 2;
                int?[,] oldAdjacency = adjacency;
                adjacency = new int?[_capacity, _capacity];
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
                adjacency[i, _v] = null;
                adjacency[_v, i] = null;
            }

            indices.Remove(v);
            vertices[_v] = null;

        }
        public bool HasEdge(Vertex u, Vertex v)
        {
            return adjacency[indices[u], indices[v]].HasValue;
        }
        public IEnumerable<Vertex> Neighbors(Vertex u)
        {
            int _u = indices[u];

            for (int i = 0; i < _used; i++ )
            {
                if (adjacency[_u, i].HasValue)
                    yield return vertices[i];
            }

        }
        public IEnumerable<Vertex> Incidents(Vertex v)
        {
            int _v = indices[v];

            for (int i = 0; i < vertices.Length; i++)
            {
                if (adjacency[i, _v].HasValue)
                    yield return vertices[i];
            }

        }
        public void SetEdge(Vertex u, Vertex v, int weight)
        {
            adjacency[indices[u], indices[v]] = weight;
        }
        public int GetEdge(Vertex u, Vertex v)
        {
            if (!adjacency[indices[u], indices[v]].HasValue)
                throw new EdgeNotFoundException();

            return adjacency[indices[u], indices[v]].Value;
        }
        public void RemoveEdge(Vertex u, Vertex v)
        {
            adjacency[indices[u], indices[v]] = null;
        }
        public IEnumerable<Vertex> Vertices()
        {
            return vertices.Where(x => { return x != null; });
        }
    }
}
