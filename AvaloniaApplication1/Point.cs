using System;
using System.Collections.Generic;

namespace AvaloniaApplication1;

public class Point
{
    public readonly double Latitude;
    public readonly double Longitude;
    public readonly List<string> Entries;
    
    public Point(List<string> entries)
    {
        Entries = entries;
    }
    
    public Point(double latitude, double longitude)
    {
        Latitude = latitude;
        Longitude = longitude;
    }
    
    public Point(double latitude, double longitude, List<string> entries)
    {
        Latitude = latitude;
        Longitude = longitude;
        Entries = entries;
    }
    
    public override string ToString()
    {
        return $"{Latitude};{Longitude};" + string.Join(";", Entries);
    }

    public string NewToString()
    {
        return $"{Latitude};{Longitude};";
    }

    public override bool Equals(object? obj)
    {
        Point point = (Point)obj;
        if (Latitude.Equals(point.Latitude) && Longitude.Equals(point.Longitude))
        {
            return true;
        }

        return false;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Latitude, Longitude);
    }
}