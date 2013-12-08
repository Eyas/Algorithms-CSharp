using System.Collections.Generic;

namespace Graphs.Mutable.Weighted
{
    class UndirectedGraph : IWeightedGraph
    {
        private readonly IWeightedGraph _graph;
        public UndirectedGraph(IWeightedGraph graph)
        {
            _graph = graph;
        }
        public bool HasEdge(Vertex u, Vertex v)
        {
            return _graph.HasEdge(u, v);
        }
        public List<Vertex> Neighbors(Vertex u)
        {
            return _graph.Neighbors(u);
        }
        public int GetEdge(Vertex u, Vertex v)
        {
            return _graph.GetEdge(u, v);
        }
        public void SetEdge(Vertex u, Vertex v, int weight)
        {
            _graph.SetEdge(u, v, weight);
            _graph.SetEdge(v, u, weight);
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
    }
}
