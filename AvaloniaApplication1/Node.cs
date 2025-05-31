using System.Collections.Generic;

namespace AvaloniaApplication1;

public class Node
{
    public readonly double Latitude;
    
    public readonly double Longitude;
    
    public readonly List<string> Entries;
    
    public Node? RightSon { get; set; }
    
    public Node? LeftSon { get; set; }
    
    public Rectangle? Rectangle { get; set; }

    public Node(double latitude, double longitude, List<string> entries)
    {
        Latitude = latitude;
        Longitude = longitude;
        Entries = entries;
    }
}