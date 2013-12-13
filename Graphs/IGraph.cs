using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphs
{
    public interface IGraph
    {
        bool HasEdge(Vertex u, Vertex v);
        List<Vertex> Neighbors(Vertex u);
        IEnumerable<Vertex> Vertices();
    }
}
