using System;
using System.Linq;
using System.Collections.Generic;

namespace Graphs.Weighted.Immutable
{
    public class AdjacencyMatrixGraph : IWeightedGraph, IIncidentsGraph
    {
        private readonly int?[,] adjacency;
        private Vertex[] vertices;
        private readonly Dictionary<Vertex, int> indices = new Dictionary<Vertex, int>();

        public AdjacencyMatrixGraph(IEnumerable<Vertex> V, IEnumerable<Edge> E)
        {
            vertices = V.ToArray();
            adjacency = new int?[vertices.Length, vertices.Length];

            for (int i = 0; i < vertices.Length; i++)
            {
                indices[vertices[i]] = i;
            }

            foreach (Edge e in E)
            {
                if (!e.weight.HasValue)
                {
                    throw new ArgumentException("Unweighted edge provided in constructor of weighted graph.");
                }
                adjacency[indices[e.u], indices[e.v]] = e.weight;
            }
        }
        public bool HasEdge(Vertex u, Vertex v)
        {
            return adjacency[indices[u], indices[v]].HasValue;
        }
        public int GetEdge(Vertex u, Vertex v)
        {
            return adjacency[indices[u], indices[v]].Value;
        }
        public IEnumerable<Vertex> Neighbors(Vertex u)
        {
            int _u = indices[u];

            for (int i = 0; i < vertices.Length; i++ )
            {
                if (adjacency[_u, i].HasValue)
                    yield return vertices[i];
            }

        }
        public int Degree(Vertex u)
        {
            int _u = indices[u];
            int degree = 0;

            for (int i = 0; i < vertices.Length; i++)
            {
                if (adjacency[_u, i].HasValue)
                    ++degree;
            }
            return degree;
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
        public int InDegree(Vertex v)
        {
            int _v = indices[v];
            int indegree = 0;

            for (int i = 0; i < vertices.Length; i++)
            {
                if (adjacency[i, _v].HasValue)
                    ++indegree;
            }
            return indegree;
        }
        public IEnumerable<Vertex> Vertices()
        {
            return (IEnumerable<Vertex>)vertices.Clone();
        }
    }
}
