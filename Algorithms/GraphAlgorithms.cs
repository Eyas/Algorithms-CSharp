using System;
using System.Linq;
using System.Collections.Generic;
using Graphs;

namespace Algorithms
{
    public static class GraphAlgorithms
    {
        public static IEnumerable<IEnumerable<Vertex>> GetConnectedComponents(
            this IGraph graph)
        {
            HashSet<Vertex> remainingVertices = new HashSet<Vertex>(graph.Vertices());

            while (remainingVertices.Count > 0)
            {
                IEnumerable<Vertex> componentVertices = graph.BreadthFirstTreeTraversal(remainingVertices.First());
                remainingVertices.ExceptWith(componentVertices);
                yield return componentVertices;
            }

        }
        /// <summary>
        /// Returns a topological sort of a given graph.
        /// </summary>
        /// <remarks>
        /// Overall complexity: O(|V|^2) for adjacency matrices, and
        /// O(|V|+|E|) for adjacency lists.
        /// </remarks>
        public static IEnumerable<Vertex> TopologicalSort(
            this IIncidentsGraph graph)
        {
         
            // O(|V|^2)   for Adjacency Matrix,
            // O(|V|)     for Adjacency-Incident Set with cached lengths
            // O(|V|+|E|) for Incidence Set w/o cached lengths
            // O(|V|+|E|) for gradual buildup
            Dictionary<Vertex, int> indeg = graph.Vertices()
                .ToDictionary(v => { return v; }, v => { return graph.InDegree(v); });

            // O(|V|)
            Queue<Vertex> toExplore =
                new Queue<Vertex>(indeg.Where(kv => { return kv.Value == 0; }).Select(x => { return x.Key; }));

            // iterated |V| times; indeg removed from once in each iteration
            while (indeg.Count > 0)
            {
                if (toExplore.Count == 0)
                    throw new Exception("Cycles detected");

                Vertex v = toExplore.Dequeue();
                yield return v;

                indeg.Remove(v);
                // either O(|V|) for adjacency matrix, of
                //     or O(|E|) over all iterations for adjacency list
                foreach (Vertex dest in graph.Neighbors(v))
                {
                    if (--indeg[dest] == 0)
                    {
                        toExplore.Enqueue(dest);
                    }
                }

            } // loop is either O(|V|^2) for adjacency matrix, of O(|V|+|E|) for adjacency list

        }
    }
}
