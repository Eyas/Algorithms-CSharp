using System.Text;

namespace Graphs
{
    public class Vertex
    {
        public string Name { get { return _name; } }

        public Vertex(string name)
        {
            _name = name;
        }
        public override bool Equals(object obj)
        {
            Vertex other = obj as Vertex;
            if (other == null) return false;
            return _name.Equals(other._name);
        }
        public override int GetHashCode()
        {
            return _name.GetHashCode();
        }
        public override string ToString()
        {
            return (new StringBuilder()).Append("Vertex[").Append(_name.ToString()).Append(']').ToString();
        }

        private readonly string _name;
        
    }
}
