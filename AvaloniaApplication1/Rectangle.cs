using System.Collections.Generic;

namespace AvaloniaApplication1;

public class Rectangle
{
    public double MaxLatitude { get; set; }
    
    public double MinLatitude { get; set; }
    
    public double MaxLongitude { get; set; }
    
    public double MinLongitude { get; set; }

    public Rectangle(double maxLatitude, double minLatitude, double maxLongitude, double minLongitude)
    {
        MaxLatitude = maxLatitude;
        MinLatitude = minLatitude;
        MaxLongitude = maxLongitude;
        MinLongitude = minLongitude;
    }

    public static (Rectangle, Rectangle) DivideByLongitude(Point medianPoint, Rectangle rectangle)
    {
        return (
            new Rectangle(rectangle.MaxLatitude, rectangle.MinLatitude, medianPoint.Longitude, rectangle.MinLongitude),
            new Rectangle(rectangle.MaxLatitude, rectangle.MinLatitude,rectangle.MaxLongitude,medianPoint.Longitude));
    }
    
    public static (Rectangle, Rectangle) DivideByLatitude(Point medianPoint, Rectangle rectangle)
    {
        return (
            new Rectangle(medianPoint.Latitude, rectangle.MinLatitude, rectangle.MaxLongitude, rectangle.MinLongitude),
            new Rectangle(rectangle.MaxLatitude, medianPoint.Latitude,rectangle.MaxLongitude, rectangle.MinLongitude));
    }

    public (double, double) GetWidthHeight()
    {
        return (MaxLatitude - MinLatitude, MaxLongitude - MinLongitude);
    }
    
    public bool IntersectsRectangle(Rectangle other)
    {
        
        if (this.MaxLongitude < other.MinLongitude || this.MinLongitude > other.MaxLongitude)
            return false;
        
        if (this.MinLatitude > other.MaxLatitude || this.MaxLatitude < other.MinLatitude)
            return false;

        return true;
    }

    public List<Point> GetVertexes()
    {
        var topLeft = new Point(MaxLatitude, MinLongitude);
        var topRight = new Point(MaxLatitude, MaxLongitude);
        var bottomRight = new Point(MinLatitude, MaxLongitude);
        var bottomLeft = new Point(MinLatitude, MinLongitude);
        return new List<Point>
        {
            topRight, bottomRight, bottomLeft, topLeft
        };
    }

    public bool RectangleContainsPoint(Point point)
    {
        if (this.MaxLatitude >= point.Latitude && this.MinLatitude <= point.Latitude &&
            this.MaxLongitude >= point.Longitude && this.MinLongitude <= point.Longitude)
        {
            return true;
        }
        return false;
    }
}