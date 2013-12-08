using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphs.Weighted.Mutable
{
    interface IWeightedMutableGraph : IMutableGraph , IWeightedGraph
    {
        void SetEdge(Vertex u, Vertex v, int weight);
        void RemoveEdge(Vertex u, Vertex v);
    }
}
