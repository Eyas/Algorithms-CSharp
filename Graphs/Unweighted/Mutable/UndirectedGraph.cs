using System.Collections.Generic;

namespace Graphs.Unweighted.Mutable
{
    public class UndirectedGraph : IUnweightedMutableGraph, IIncidentsGraph
    {
        private readonly IUnweightedMutableGraph _graph;
        public UndirectedGraph(IUnweightedMutableGraph graph)
        {
            _graph = graph;
        }
        public bool HasEdge(Vertex u, Vertex v)
        {
            return _graph.HasEdge(u, v);
        }
        public IEnumerable<Vertex> Neighbors(Vertex u)
        {
            return _graph.Neighbors(u);
        }
        public int Degree(Vertex u)
        {
            return _graph.Degree(u);
        }
        public IEnumerable<Vertex> Incidents(Vertex v)
        {
            // undirected -> neighbors == incidents
            return _graph.Neighbors(v);
        }
        public int InDegree(Vertex v)
        {
            // undirected -> degree == indegree
            return _graph.Degree(v);
        }
        public void SetEdge(Vertex u, Vertex v)
        {
            _graph.SetEdge(u, v);
            _graph.SetEdge(v, u);
        }
        public void RemoveEdge(Vertex u, Vertex v)
        {
            _graph.RemoveEdge(u, v);
            _graph.RemoveEdge(v, u);
        }
        public void AddVertex(Vertex v)
        {
            _graph.AddVertex(v);
        }
        public void RemoveVertex(Vertex v)
        {
            _graph.RemoveVertex(v);
        }
        public IEnumerable<Vertex> Vertices()
        {
            return _graph.Vertices();
        }
    }
}
