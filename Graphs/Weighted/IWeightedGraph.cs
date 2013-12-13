using System.Collections.Generic;

namespace Graphs.Weighted
{
    /**
     * Interface for a Weighted Directed Graph
     */
    public interface IWeightedGraph : IGraph
    {
        int GetEdge(Vertex u, Vertex v);
    }
}
