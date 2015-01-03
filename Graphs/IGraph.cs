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
        /// For a vertex u, returns the number of vertices v such that there is an
        /// edge from u to v (the degree of u).
        /// </summary>
        int Degree(Vertex u);

        /// <summary>
        /// Returns the correction of Vertices v contained in the graph.
        /// </summary>
        IEnumerable<Vertex> Vertices();
    }

    public interface IIncidentsGraph : IGraph
    {
        /// <summary>
        /// Returns the set of vertices u such that there is an edge from u to v.
        /// </summary>
        IEnumerable<Vertex> Incidents(Vertex v);

        /// <summary>
        /// For a vertex v, returns the number of vertices u such that there is an
        /// edge from u to v (the indegree of u).
        /// </summary>
        int InDegree(Vertex v);
    }
}
