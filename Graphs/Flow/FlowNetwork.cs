using System.Collections.Generic;

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
        private IGraph _graph;
        public FlowNetwork(IGraph graph, Vertex source, Vertex target)
        {

        }
    }
}
