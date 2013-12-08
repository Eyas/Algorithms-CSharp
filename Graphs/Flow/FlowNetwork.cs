using System.Collections.Generic;

namespace Graphs.Flow
{
    class FlowNetwork
    {
        public Vertex s
        {
            private set;
            public get;
        }
        public Vertex t
        {
            private set;
            public get;
        }
        private IGraph _graph;
        public FlowNetwork(IGraph graph, Vertex source, Vertex target)
        {

        }
    }
}
