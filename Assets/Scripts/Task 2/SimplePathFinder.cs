using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SimplePathFinder : IPathFinder
{
    public IEnumerable<Vector2> GetPath(Vector2 A, Vector2 C, IEnumerable<Edge> edges)
    {
        var graph = BuildGraph(edges);

        var startRect = graph.Keys.FirstOrDefault(r => r.Contains(A));
        var endRect = graph.Keys.FirstOrDefault(r => r.Contains(C));

        if (startRect.Equals(default(Rectangle)) || endRect.Equals(default(Rectangle)))
            return null;

        var pathRects = FindRectanglePath(startRect, endRect, graph);
        if (pathRects == null)
            return null;

        return BuildPath(A, C, pathRects, edges);
    }

    private Dictionary<Rectangle, List<Edge>> BuildGraph(IEnumerable<Edge> edges)
    {
        var graph = new Dictionary<Rectangle, List<Edge>>();
        foreach (var edge in edges)
        {
            if (!graph.ContainsKey(edge.First))
                graph[edge.First] = new List<Edge>();
            if (!graph.ContainsKey(edge.Second))
                graph[edge.Second] = new List<Edge>();

            graph[edge.First].Add(edge);
            graph[edge.Second].Add(edge);
        }
        return graph;
    }

    private List<Rectangle> FindRectanglePath(Rectangle start, Rectangle end, Dictionary<Rectangle, List<Edge>> graph)
    {
        var queue = new Queue<Rectangle>();
        var visited = new HashSet<Rectangle>();
        var parents = new Dictionary<Rectangle, Rectangle>();

        queue.Enqueue(start);
        visited.Add(start);

        while (queue.Count > 0)
        {
            var current = queue.Dequeue();
            if (current.Equals(end))
            {
                var path = new List<Rectangle>();
                while (!current.Equals(default(Rectangle)))
                {
                    path.Add(current);
                    parents.TryGetValue(current, out current);
                }
                path.Reverse();
                return path;
            }

            foreach (var edge in graph[current])
            {
                var neighbor = edge.First.Equals(current) ? edge.Second : edge.First;
                if (!visited.Contains(neighbor))
                {
                    visited.Add(neighbor);
                    parents[neighbor] = current;
                    queue.Enqueue(neighbor);
                }
            }
        }

        return null;
    }

    private List<Vector2> BuildPath(Vector2 start, Vector2 end, List<Rectangle> pathRects, IEnumerable<Edge> edges)
    {
        var path = new List<Vector2> { start };

        for (int i = 0; i < pathRects.Count - 1; i++)
        {
            var current = pathRects[i];
            var next = pathRects[i + 1];

            var edge = edges.FirstOrDefault(e => (e.First.Equals(current) && e.Second.Equals(next)) ||
                                                  (e.First.Equals(next) && e.Second.Equals(current)));
            if (edge.Equals(default(Edge)))
                continue;

            path.Add(current.Center);
            path.Add(next.Center);
        }

        path.Add(end);
        return path;
    }

}
