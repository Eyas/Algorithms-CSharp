using System;
using System.Collections.Generic;
using System.Linq;
using Graphs;

namespace Algorithms
{
    public static class GraphAlgorithms
    {
        public static ICollection<IEnumerable<Vertex>> GetConnectedComponents(
            this IGraph graph)
        {
            LinkedList<IEnumerable<Vertex>> connectedComponents = new LinkedList<IEnumerable<Vertex>>();
            HashSet<Vertex> remainingVertices = new HashSet<Vertex>();
            remainingVertices.UnionWith(graph.Vertices());

            while (remainingVertices.Count > 0)
            {
                IEnumerable<Vertex> componentVertices = graph.BreadthFirstTreeTraversal(remainingVertices.ElementAt(0)).ToList();
                connectedComponents.AddFirst(componentVertices);
                remainingVertices.ExceptWith(componentVertices);
            }
            return connectedComponents;
        }
    }
}
