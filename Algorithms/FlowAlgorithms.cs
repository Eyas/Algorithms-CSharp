using System;
using System.Collections.Generic;
using System.Linq;
using Graphs;
using Graphs.Weighted;

namespace Algorithms
{
    public static class FlowAlgorithms
    {
        /// <summary>
        /// Finds the maximum flow between a source and target node and returns a new weighted graph
        /// representing the legal flows between vertices to generate the maximum flow.
        /// </summary>
        /// <remarks>
        /// Edmonds-Karp Max Flow Algorithm, based on my C# port (git.io/KewoqQ)
        /// of the Wikipedia psuedocode found here: (en.wikipedia.org/wiki/Edmonds%E2%80%93Karp_algorithm)
        /// </remarks>
        /// <param name="graph">a weighted graph representing the flow network</param>
        /// <param name="source">the source node</param>
        /// <param name="target">the target node</param>
        /// <param name="legalFlows">an output graph representing legal flows</param>
        /// <returns>the maximum flow between source and target</returns>
        public static int FindMaxFlow(
            this IWeightedGraph graph,
            Vertex source,
            Vertex target,
            out IWeightedGraph legalFlows)
        {
            Graphs.Weighted.Mutable.IWeightedMutableGraph _legalFlows =
                new Graphs.Weighted.Mutable.AdjacencyMatrixGraph(graph.Vertices().Count());
            legalFlows = _legalFlows;
            foreach (Vertex v in graph.Vertices())
            {
                _legalFlows.AddVertex(v);
            }

            int f = 0; // initial flow is 0

            while (true)
            {
                IDictionary<Vertex, Vertex> path;
                int capacity = BreadthFirstSearch(graph, source, target, legalFlows, out path);

                if (capacity == 0) break;
                f += capacity;
                // backtrack search, and write flow
                Vertex v = target;

                while (!v.Equals(source))
                {
                    Vertex u = path[v];
                    _legalFlows.SetEdge(u, v, _legalFlows.GetCapacity(u, v) + capacity);
                    _legalFlows.SetEdge(v, u, _legalFlows.GetCapacity(v, u) - capacity);
                    v = u;
                }
            }


            return f;
        }

        private static int BreadthFirstSearch(
            IWeightedGraph graph,
            Vertex source,
            Vertex target,
            IWeightedGraph legalFlows,
            out IDictionary<Vertex, Vertex> path)
        {
            path = new Dictionary<Vertex, Vertex>();
            IDictionary<Vertex, int> pathCapacity = new Dictionary<Vertex, int>();

            path[source] = null; // make sure source is not rediscovered
            pathCapacity[source] = int.MaxValue;

            Queue<Vertex> queue = new Queue<Vertex>();
            queue.Enqueue(source);

            while(queue.Count > 0)
            {
                Vertex u = queue.Dequeue();
                foreach (Vertex v in graph.Neighbors(u))
                {
                    // if there is available capacity between u and v,
                    // ... and v is not seen before in search
                    if (graph.GetCapacity(u, v) - legalFlows.GetCapacity(u, v) > 0 &&
                        path.ContainsKey(v) == false)
                    {
                        path[v] = u;
                        pathCapacity[v] = Math.Min(
                            pathCapacity[u],
                            graph.GetCapacity(u, v) - legalFlows.GetCapacity(u ,v));

                        if (!v.Equals(target)) queue.Enqueue(v);
                        else return pathCapacity[target];
                    }
                }
            }

            return 0;

        }

        #region Helper Extension Methods
        private static int GetCapacity(
            this IWeightedGraph graph,
            Vertex u,
            Vertex v)
        {
            if (graph.HasEdge(u, v)) return graph.GetEdge(u, v);
            return 0;
        }
        #endregion

    }
}
