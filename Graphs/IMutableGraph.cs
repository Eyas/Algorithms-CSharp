namespace Graphs
{
    public interface IMutableGraph : IGraph
    {
        void AddVertex(Vertex v);
        void RemoveVertex(Vertex v);
    }
}
