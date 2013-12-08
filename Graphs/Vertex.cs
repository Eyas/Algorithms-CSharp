using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphs
{
    public class Vertex
    {
        public string Name { get; private set; }

        public Vertex(string name)
        {
            Name = name;
        }
        public override bool Equals(object obj)
        {
            Vertex other = obj as Vertex;
            if (other == null) return false;
            return Name.Equals(other.Name);
        }
        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }
    }
}
