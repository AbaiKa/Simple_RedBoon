using System.Collections.Generic;
using UnityEngine;

public class PathAssistant : MonoBehaviour
{
    private SimplePathFinder pathFinder;
    private List<Edge> edges = new List<Edge>();
    private IEnumerable<Vector2> path = new List<Vector2>();

    private void Start()
    {
        pathFinder = new SimplePathFinder();

        List<Edge> edges = new List<Edge>()
        {
            new Edge(
                new Rectangle(new Vector2(-15, 15), new Vector2(2, 25)),
                new Rectangle(new Vector2(-3, 25), new Vector2(17, 35)),
                new Vector2(-3, 25),
                new Vector2(2, 25)),

            new Edge(
                new Rectangle(new Vector2(-3, 25), new Vector2(17, 35)),
                new Rectangle(new Vector2(17, 20), new Vector2(37, 30)),
                new Vector2(17, 25),
                new Vector2(17, 30))
        };

        this.edges = edges;

        path = pathFinder.GetPath(new Vector2(-6.5f, 15), new Vector2(37, 25), edges);
    }

    private void OnDrawGizmosSelected()
    {
        if (edges.Count > 0)
        {
            Gizmos.color = Color.red;
            foreach (var edge in edges)
            {
                DrawRectangle(edge.First);
                DrawRectangle(edge.Second);

                Gizmos.DrawLine(edge.Start, edge.End);
            }

            if (path != null)
            {
                var pathList = new List<Vector2>(path);

                for (int i = 0; i < pathList.Count; i++)
                {
                    Gizmos.color = Color.green;
                    Gizmos.DrawWireSphere(pathList[i], 0.5f);
                }

                for (int i = 0; i < pathList.Count - 1; i++)
                {
                    Gizmos.color = Color.white;
                    Gizmos.DrawLine(new Vector3(pathList[i].x, pathList[i].y, 0), new Vector3(pathList[i + 1].x, pathList[i + 1].y, 0));
                }
            }
        }
    }

    private void DrawRectangle(Rectangle rect)
    {
        Vector3 bottomLeft = new Vector3(rect.Min.x, rect.Min.y, 0);
        Vector3 bottomRight = new Vector3(rect.Max.x, rect.Min.y, 0);
        Vector3 topLeft = new Vector3(rect.Min.x, rect.Max.y, 0);
        Vector3 topRight = new Vector3(rect.Max.x, rect.Max.y, 0);

        Gizmos.DrawLine(bottomLeft, bottomRight);
        Gizmos.DrawLine(bottomRight, topRight);
        Gizmos.DrawLine(topRight, topLeft);
        Gizmos.DrawLine(topLeft, bottomLeft);
    }
}
