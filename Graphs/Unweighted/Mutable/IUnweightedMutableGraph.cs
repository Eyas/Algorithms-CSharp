using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphs.Unweighted.Mutable
{
    public interface IUnweightedMutableGraph : IMutableGraph, IUnweightedGraph
    {
        void SetEdge(Vertex u, Vertex v);
        void RemoveEdge(Vertex u, Vertex v);
    }
}
