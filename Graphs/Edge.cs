using System.Text;

namespace Graphs
{
    public class Edge
    {
        public readonly Vertex u;
        public readonly Vertex v;
        public readonly int? weight;
        public Edge(Vertex u, Vertex v)
        {
            this.u = u;
            this.v = v;
        }
        public Edge(Vertex u, Vertex v, int weight)
        {
            this.weight = weight;
            this.u = u;
            this.v = v;
        }
        public override bool Equals(object obj)
        {
            Edge other = obj as Edge;
            if (other == null) return false;
            return u.Equals(other.u) && v.Equals(other.v);
        }
        public override int GetHashCode()
        {
            return u.GetHashCode() ^ v.GetHashCode();
        }
        public override string ToString()
        {
            return new StringBuilder().Append("Edge(").Append(u.Name).Append(',').Append(v.Name).Append(')').ToString();
        }
    }
}
