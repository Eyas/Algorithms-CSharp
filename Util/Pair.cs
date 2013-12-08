using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Util
{
    public class Pair<T1, T2>
    {
        public readonly T1 _1;
        public readonly T2 _2;
        public Pair(T1 k, T2 v)
        {
            _1 = k;
            _2 = v;
        }
        public void Extract(out T1 k, out T2 v)
        {
            k = _1;
            v = _2;
        }
        public override bool Equals(object obj)
        {
            Pair<T1, T2> other = obj as Pair<T1, T2>;
            return (other != null) && _1.Equals(other._1) && _2.Equals(other._2);
        }
        public override int GetHashCode()
        {
            return _1.GetHashCode() + _2.GetHashCode();
        }
        public override string ToString()
        {
            return (new StringBuilder()).
                Append("Pair<").
                Append(_1.ToString()).
                Append(",").
                Append(_2.ToString()).
                Append('>').
                ToString();
        }

    }
}
