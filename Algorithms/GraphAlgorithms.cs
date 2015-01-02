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
        public static IEnumerable<Vertex> TopologicalSort(
            this IIncidentsGraph graph)
        {
            // TODO: use min-heap here
            Dictionary<Vertex, int> indeg = graph.Vertices()
                .ToDictionary(v => { return v; }, v => { return graph.Incidents(v).Count(); });

            while (indeg.Count > 0)
            {
                if (indeg.Any(kv => { return kv.Value == 0; }) == false)
                    throw new Exception("Cycles detected");

                IEnumerable<Vertex> candidates = indeg.Where(kv => { return kv.Value == 0; }).Select(x => { return x.Key; });

                foreach (Vertex candidate in candidates)
                {
                    indeg.Remove(candidate);
                    foreach (Vertex dest in graph.Neighbors(candidate))
                    {
                        indeg[dest]--;
                    }

                    yield return candidate;
                }

            }

        }
    }
}
