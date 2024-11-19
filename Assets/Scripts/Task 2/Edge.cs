using UnityEngine;

public struct Edge
{
    public Rectangle First;
    public Rectangle Second;
    public Vector2 Start;
    public Vector2 End;

    public Edge(Rectangle first, Rectangle second, Vector2 start, Vector2 end)
    {
        First = first;
        Second = second;
        Start = start;
        End = end;
    }
}

public struct Rectangle
{
    public Vector2 Min;
    public Vector2 Max;

    public Rectangle(Vector2 min, Vector3 max)
    {
        Min = min; 
        Max = max;
    }
    public Vector2 Center => (Min + Max) / 2;

    public bool Contains(Vector2 point)
    {
        return point.x >= Min.x && point.x <= Max.x && point.y >= Min.y && point.y <= Max.y;
    }
}
