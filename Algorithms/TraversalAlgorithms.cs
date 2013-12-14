using System;
using System.Collections.Generic;
using Graphs;

namespace Algorithms
{
    public static class TraversalAlgorithms
    {
        public static IEnumerable<Vertex> BreadthFirstGraphTraversal(
            this IGraph graph,
            Vertex source)
        {
            Queue<Vertex> toVisit = new Queue<Vertex>();
            toVisit.Enqueue(source);
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
            this IGraph graph,
            Vertex source)
        {
            Stack<Vertex> toVisit = new Stack<Vertex>();
            toVisit.Push(source);
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
            this IGraph graph,
            Vertex source)
        {
            Queue<Vertex> toVisit = new Queue<Vertex>();
            HashSet<Vertex> visited = new HashSet<Vertex>();
            toVisit.Enqueue(source);
            visited.Add(source);
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
        public static IEnumerable<Vertex> PreOrderTreeTraversal(
            this IGraph graph,
            Vertex source)
        {
            Stack<Vertex> toVisit = new Stack<Vertex>();
            HashSet<Vertex> visited = new HashSet<Vertex>();
            toVisit.Push(source);
            visited.Add(source);
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
