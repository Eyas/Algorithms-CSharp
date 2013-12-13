using System;
using System.Collections.Generic;
using Graphs;
using Graphs.Weighted;

namespace Algorithms
{
    public static class FlowAlgorithms
    {
        public static int FindMaxFlow(
            this IWeightedGraph graph,
            Vertex source,
            Vertex target,
            out IWeightedGraph legalFlows)
        {
            legalFlows = null;
            return 0;
        }
    }
}
