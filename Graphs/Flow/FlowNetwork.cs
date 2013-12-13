using System.Collections.Generic;
using Graphs.Weighted.Mutable;

namespace Graphs.Flow
{
    class FlowNetwork
    {
        public Vertex s
        {
            private set;
            get;
        }
        public Vertex t
        {
            private set;
            get;
        }
        private IWeightedMutableGraph _graph;
        public FlowNetwork(IWeightedMutableGraph graph, Vertex source, Vertex target)
        {
            _graph = graph;
            s = source;
            t = target;
        }

    }
}
