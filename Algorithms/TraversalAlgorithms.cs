using System;
using System.Collections.Generic;
using Graphs;

namespace Algorithms
{
    public static class TraversalAlgorithms
    {
        public static IEnumerable<Vertex> BreadthFirstGraphTraversal(
            IGraph graph,
            Vertex start)
        {
            Queue<Vertex> toVisit = new Queue<Vertex>();
            toVisit.Enqueue(start);
            while (toVisit.Count > 0)
            {
                Vertex v = toVisit.Dequeue();
                yield return v;
                foreach (Vertex w in graph.Neighbors(v))
                {
                    toVisit.Enqueue(w);
                }
            }
        }
        public static IEnumerable<Vertex> DepthFirstGraphTraversal(
            IGraph graph,
            Vertex start)
        {
            Stack<Vertex> toVisit = new Stack<Vertex>();
            toVisit.Push(start);
            while (toVisit.Count > 0)
            {
                Vertex v = toVisit.Pop();
                yield return v;
                foreach (Vertex w in graph.Neighbors(v))
                {
                    toVisit.Push(w);
                }
            }
        }

        public static IEnumerable<Vertex> BreadthFirstTreeTraversal(
            IGraph graph,
            Vertex start)
        {
            Queue<Vertex> toVisit = new Queue<Vertex>();
            HashSet<Vertex> visited = new HashSet<Vertex>();
            toVisit.Enqueue(start);
            visited.Add(start);
            while (toVisit.Count > 0)
            {
                Vertex v = toVisit.Dequeue();
                yield return v;
                foreach (Vertex w in graph.Neighbors(v))
                {
                    if (!visited.Contains(w))
                    {
                        toVisit.Enqueue(w);
                        visited.Add(w);
                    }
                }
            }
        }
        public static IEnumerable<Vertex> DepthFirstTreeTraversal(
            IGraph graph,
            Vertex start)
        {
            Stack<Vertex> toVisit = new Stack<Vertex>();
            HashSet<Vertex> visited = new HashSet<Vertex>();
            toVisit.Push(start);
            visited.Add(start);
            while (toVisit.Count > 0)
            {
                Vertex v = toVisit.Pop();
                yield return v;
                foreach (Vertex w in graph.Neighbors(v))
                {
                    if (!visited.Contains(w))
                    {
                        toVisit.Push(w);
                        visited.Add(w);
                    }
                }
            }
        }
    }
}
