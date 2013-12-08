using System.Collections.Generic;

namespace Graphs.Mutable.Weighted
{
    /**
     * Interface for a Weighted Directed Graph
     */
    interface IWeightedGraph : IMutableGraph
    {
        int GetEdge(Vertex u, Vertex v);
        void SetEdge(Vertex u, Vertex v, int weight);
        void RemoveEdge(Vertex u, Vertex v);
    }
}
