using System.Collections.Generic;

namespace Graphs.Immutable.Weighted
{
    /**
     * Interface for a Weighted Directed Graph
     */
    interface IWeightedGraph : IGraph
    {
        int GetEdge(Vertex u, Vertex v);
    }
}
