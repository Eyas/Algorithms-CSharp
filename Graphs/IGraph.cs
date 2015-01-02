using System.Collections.Generic;

namespace Graphs
{
    public interface IGraph
    {
        /// <summary>
        /// Returns true if and only if an edge exists from Vertex u to Vertex v.
        /// </summary>
        bool HasEdge(Vertex u, Vertex v);

        /// <summary>
        /// Returns the set of vertices v such that there is an edge from u to v.
        /// </summary>
        IEnumerable<Vertex> Neighbors(Vertex u);

        /// <summary>
        /// Returns the correction of Vertices v contained in the graph.
        /// </summary>
        IEnumerable<Vertex> Vertices();
    }

    public interface IIncidentsGraph
    {
        /// <summary>
        /// Returns the set of vertices u such that there is an edge from u to v.
        /// </summary>
        IEnumerable<Vertex> Incidents(Vertex v);
    }
}
