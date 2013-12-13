using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphs
{
    public interface IMutableGraph : IGraph
    {
        void AddVertex(Vertex v);
        void RemoveVertex(Vertex v);
    }
}
