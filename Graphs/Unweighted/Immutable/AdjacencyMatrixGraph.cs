using System;
using System.Collections.Generic;
using System.Linq;

namespace Graphs.Unweighted.Immutable
{
    public class AdjacencyMatrixGraph : IUnweightedGraph
    {
        private bool[,] adjacency;
        private Vertex[] vertices;
        private Dictionary<Vertex, int> indices = new Dictionary<Vertex, int>();

        public AdjacencyMatrixGraph(ISet<Vertex> V, ISet<Edge> E)
        {
            adjacency = new bool[V.Count, V.Count];
            vertices = V.ToArray();

            for (int i = 0; i < vertices.Length; i++)
            {
                indices[vertices[i]] = i;
            }

            foreach (Edge e in E)
            {
                adjacency[indices[e.u], indices[e.v]] = true;
            }
        }
        public bool HasEdge(Vertex u, Vertex v)
        {
            return adjacency[indices[u], indices[v]];
        }
        public List<Vertex> Neighbors(Vertex u)
        {
            List<Vertex> neighbors = new List<Vertex>();
            int _u = indices[u];

            for (int i = 0; i < adjacency.Length; i++ )
            {
                if (adjacency[_u, i])
                    neighbors.Add(vertices[i]);
            }
            return neighbors;
        }

        public IEnumerable<Vertex> Vertices()
        {
            return (IEnumerable<Vertex>)vertices.Clone();
        }
    }
}
