using System.Collections.Generic;

namespace Graphs.Mutable.Unweighted
{
    /**
     * Interface for an Unweighted Directed Graph
     */
    interface IUnweightedGraph : IMutableGraph
    {
        void SetEdge(Vertex u, Vertex v);
        void RemoveEdge(Vertex u, Vertex v);
    }
}
