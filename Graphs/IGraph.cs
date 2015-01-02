using System.Collections.Generic;

namespace Graphs
{
    public interface IGraph
    {
        bool HasEdge(Vertex u, Vertex v);
        IEnumerable<Vertex> Neighbors(Vertex u);
        IEnumerable<Vertex> Vertices();
    }
}
